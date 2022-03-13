using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Forms;
using System.Windows.Threading;
using Extensions;
using TooLazyToPressArrowKey;
using WindowsInput;
using WindowsInput.Native;
using Point = System.Drawing.Point;
using Timer = System.Timers.Timer;

namespace Touchpad2ArrowKey
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow
	{
		private bool firstActivateAfterExecution;
		private bool isActivated;
		
		private int threadSleepTime;

		private bool isDebugging;
		
		private readonly InputSimulator inputSim;
		private Thread t2AThread;
		
		private Timer activationTimer;

		private readonly int[] vkCodeULDR;                    //방향키의 vkCode
		private int[] mainTypingVkCodeULDR;   //방향키 역할을 할 메인키
		
		public MainWindow()
		{
			firstActivateAfterExecution = false;
			isActivated = false;

			isDebugging = true;  // todo 디버깅 여부 체크박스 만들기

			inputSim = new InputSimulator();

			activationTimer = new Timer {
				AutoReset = false, Interval = 750
			};

			vkCodeULDR = new[] {38, 37, 40, 39};  //각각 방향키 상, 좌, 하, 우의 vkCode
			mainTypingVkCodeULDR = new[] {73, 74, 75, 76};  //각각 i, j, k, l의 vkCode  각각 Up, Left, Down. Right키의 역할을 한다.
			

			InitializeComponent();
			
			DebugIsActivated.IsChecked = isActivated;
			DeactivateButton.IsEnabled = false;



			Hook.KeyboardHook.KeyDown += KeyboardHook_KeyDown;
			Hook.KeyboardHook.KeyUp += KeyboardHook_KeyUp;
			Hook.KeyboardHook.HookStart();
		}
		
		~MainWindow() {
			Hook.KeyboardHook.HookEnd();
		}
		
		private bool KeyboardHook_KeyDown(int vkCode) {
			if(isDebugging)
				DebuggingLogTextBox.AppendText($"{vkCode}|");
			
			if(vkCode.ToString() == "164") { //vkCode 164 == ALT
				if(activationTimer.Enabled) {
					activationTimer.Stop();

					if(isActivated)
						M2ADeactivate();
					else
						M2AActivate();
				}
				else { activationTimer.Start(); }
			}
			else if(isActivated) {
				MainTypingToArrowKey(vkCode);
				if(vkCodeULDR.All(i => i != vkCode))
					return false;
			}
			
			return true;
		}

		private static bool KeyboardHook_KeyUp(int vkCode) {
			return true;
		}
		

		private void ActivateButton_Click(object sender, RoutedEventArgs e)
			=> M2AActivate();

		private void DeactivateButton_Click(object sender, RoutedEventArgs e) 
			=> M2ADeactivate();
		
		private void M2AActivate()
		{
			isActivated = true;
			DebugIsActivated.IsChecked = isActivated;
			ActivateButton.IsEnabled = false;
			DeactivateButton.IsEnabled = true;

			if(firstActivateAfterExecution) {
				firstActivateAfterExecution = false;
				M2ADeactivate();
				M2AActivate();
			}
		}
		
		private void M2ADeactivate()
		{
			isActivated = false;
			DebugIsActivated.IsChecked = isActivated;
			DeactivateButton.IsEnabled = false;
			ActivateButton.IsEnabled = true;
		}
		
		private void MainTypingToArrowKey(int vkCode)
		{
			VirtualKeyCode? arrowKeyToBePressed;
			if(vkCode == mainTypingVkCodeULDR[(int)VkCodeULDREnum.UP])
				arrowKeyToBePressed = VirtualKeyCode.UP;
			else if(vkCode == mainTypingVkCodeULDR[(int)VkCodeULDREnum.LEFT])
				arrowKeyToBePressed = VirtualKeyCode.LEFT;
			else if(vkCode == mainTypingVkCodeULDR[(int)VkCodeULDREnum.DOWN])
				arrowKeyToBePressed = VirtualKeyCode.DOWN;
			else if(vkCode == mainTypingVkCodeULDR[(int)VkCodeULDREnum.RIGHT])
				arrowKeyToBePressed = VirtualKeyCode.RIGHT;
			else
				arrowKeyToBePressed = null;

			if(arrowKeyToBePressed != null)
				inputSim.Keyboard.KeyPress((VirtualKeyCode)arrowKeyToBePressed);
		}
	}
}