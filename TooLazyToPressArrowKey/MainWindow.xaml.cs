using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Forms;
using System.Windows.Threading;
using Extensions;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Lifetime.Clear;
using ToastNotifications.Messages;
using ToastNotifications.Position;
using TooLazyToPressArrowKey;
using WindowsInput;
using WindowsInput.Native;
using static Extensions.VkCodeTable;
using static TooLazyToPressArrowKey.PressingKeys;
using Application = System.Windows.Application;
using Timer = System.Timers.Timer;


namespace Touchpad2ArrowKey
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow
	{
		private bool firstActivateAfterExecution;
		private bool isEnabled;
		
		private int threadSleepTime;
		
		private bool pressedMoreThan2KeysFlag;
		
		private readonly InputSimulator inputSim;
		private Thread t2AThread;
		
		private readonly Timer timerToEnable;

		private readonly int[] vkCodeULDR;    //방향키의 vkCode
		private readonly int[] altVkCodeULDR; //방향키 역할을 할 메인키
		private readonly int[] keysNotToDisable;  //활성화 상태에서 방향키 이외에 

		private int lastKey;  //특정 키가 계속 눌리는 것을 막기 위한 변수

		private Notifier notifier;  //윈도우 알림창
		
		public MainWindow()
		{
			firstActivateAfterExecution = false;
			isEnabled = false;
			
			pressedMoreThan2KeysFlag = false;

			inputSim = new InputSimulator();

			timerToEnable = new Timer {
				AutoReset = false, Interval = 750
			};

			vkCodeULDR = new[] {UP_ARROW, LEFT_ARROW, DOWN_ARROW, RIGHT_ARROW};
			altVkCodeULDR = new[] {I, J, K, L};  //각각 Up, Left, Down. Right키의 역할을 한다.
			keysNotToDisable = new[] { L_CTRL, L_SHIFT, L_ALT, R_CTRL, R_SHIFT, R_ALT, SPACE, BACKSPACE, ENTER };  //활성화 이후에도 입력을 허용할 키
			
			InitializePressedKeysArray();

			notifier = NotifierInit();
			

			InitializeComponent();
			
			DebugIsEnabled.IsChecked = isEnabled;
			DisableButton.IsEnabled = false;
			

			Hook.KeyboardHook.KeyDown += SetKeyPressStatus_Down;
			Hook.KeyboardHook.KeyDown += KeyboardHook_KeyDown;
			
			Hook.KeyboardHook.KeyUp += KeyboardHook_KeyUp;
			Hook.KeyboardHook.KeyUp += SetKeyPressStatus_Up;
			
			Hook.KeyboardHook.HookStart();
		}
		
		~MainWindow() {
			Hook.KeyboardHook.HookEnd();
		}
		
		private bool KeyboardHook_KeyDown(int vkCode)
		{
			//SetKeyPressStatus_Down(vkCode);



			if(isEnabled) {
				MainTypingToArrowKey(vkCode);
				if(vkCodeULDR.Any(i => i == vkCode) || keysNotToDisable.Any(i => i == vkCode)) return true;
				
				if(altVkCodeULDR.All(i => i != vkCode)) {
					notifier.ClearMessages(new ClearByMessage("These keys are currently disabled."));
					notifier.ShowInformation("These keys are currently disabled.");
				}
				
				return false;
			}
			
			return true;
		}


		private int maxKeyPressCount = 0;
		private bool KeyboardHook_KeyUp(int vkCode)
		{
			if(IsDebuggingEnabled.IsChecked != null && IsDebuggingEnabled != null && (bool)IsDebuggingEnabled.IsChecked)
				DebuggingLogTextBox.AppendText($"{vkCode}|");
			

			var isNull = PressingKeysArray[L_CTRL] == null || PressingKeysArray[L_SHIFT] == null; 
			if(!isNull && (bool)PressingKeysArray[L_CTRL] && (bool)PressingKeysArray[L_SHIFT]) {  //CTRL과 SHIFT가 눌린 상태일때
				maxKeyPressCount = Math.Max(maxKeyPressCount, GetNumberOfPressingKeys());
				var otherKeyWasPressingFlag = maxKeyPressCount > 2;
				if(otherKeyWasPressingFlag) {
					if(vkCode is L_CTRL or L_SHIFT)
						maxKeyPressCount = 0;
					
					return true;
				}
				
				/*if(timerToEnable.Enabled) {
					timerToEnable.Stop();

					if(isEnabled)
						M2ADisable();
					else
						M2AEnable();
				}
				else { timerToEnable.Start(); }*/
				
				if(isEnabled)
					M2ADisable();
				else
					M2AEnable();
			}
			
			//SetKeyPressStatus_Up(vkCode);
			return true;
		}
		

		private void EnableButton_Click(object sender, RoutedEventArgs e)
			=> M2AEnable();

		private void DisableButton_Click(object sender, RoutedEventArgs e) 
			=> M2ADisable();
		
		private void M2AEnable()
		{
			isEnabled = true;
			DebugIsEnabled.IsChecked = isEnabled;
			EnableButton.IsEnabled = false;
			DisableButton.IsEnabled = true;
			notifier.ClearMessages(new ClearByMessage("Disabled"));
			notifier.ShowInformation("Enabled");

			if(firstActivateAfterExecution) {
				firstActivateAfterExecution = false;
				M2ADisable();
				M2AEnable();
			}
		}
		
		private void M2ADisable()
		{
			isEnabled = false;
			DebugIsEnabled.IsChecked = isEnabled;
			DisableButton.IsEnabled = false;
			EnableButton.IsEnabled = true;
			notifier.ClearMessages(new ClearByMessage("Enabled"));
			notifier.ShowInformation("Disabled");
		}
		
		private void MainTypingToArrowKey(int vkCode)
		{
			VirtualKeyCode? arrowKeyToBePressed;
			if(vkCode == altVkCodeULDR[(int)VkCodeULDREnum.UP])
				arrowKeyToBePressed = VirtualKeyCode.UP;
			else if(vkCode == altVkCodeULDR[(int)VkCodeULDREnum.LEFT])
				arrowKeyToBePressed = VirtualKeyCode.LEFT;
			else if(vkCode == altVkCodeULDR[(int)VkCodeULDREnum.DOWN])
				arrowKeyToBePressed = VirtualKeyCode.DOWN;
			else if(vkCode == altVkCodeULDR[(int)VkCodeULDREnum.RIGHT])
				arrowKeyToBePressed = VirtualKeyCode.RIGHT;
			else
				arrowKeyToBePressed = null;

			if(arrowKeyToBePressed != null)
				inputSim.Keyboard.KeyPress((VirtualKeyCode)arrowKeyToBePressed);
		}

		private static Notifier NotifierInit()
		{
			return new Notifier(cfg => {
				cfg.PositionProvider = new PrimaryScreenPositionProvider(
					corner: Corner.TopRight,
					offsetX: 10, offsetY: 100);

				cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
					notificationLifetime: TimeSpan.FromSeconds(2),
					maximumNotificationCount: MaximumNotificationCount.FromCount(3));

				cfg.DisplayOptions.TopMost = true;
				cfg.DisplayOptions.Width = 250;
				
				cfg.Dispatcher = Application.Current.Dispatcher;
			});
		}

		private void FixButton_Click(object sender, RoutedEventArgs e) 
			=> FixPressedKeysStatus();
	}
}