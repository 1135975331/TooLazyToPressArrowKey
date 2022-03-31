// ReSharper disable InconsistentNaming

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Extensions;
using static TooLazyToPressArrowKey.PressingKeysField;  //todo 디버깅용 실시간 키 모니터링 창 만들기

namespace TooLazyToPressArrowKey
{
	public static class PressingKeys
	{
		public static bool?[] PressingKeysArray;

		
		public static void InitializePressedKeysArray()
		{
			PressingKeysArray = new bool?[] {
				null, LeftMouseButton_1, RightMouseButton_2, CtrlBreakProc_3, MiddleMouseButton_4,
				X1MouseButton_5, X2MouseButton_6, null, Backspace_8, Tab_9,
				null, null, Clear_12, Enter_13, null,
				null, Shift_16, Ctrl_17, Alt_18, Pause_19,
				CapsLock_20, ImeHanguel_21, ImeJunja_22, ImeFinal_23, ImeHanja_24,
				ImeKanji_25, null, Esc_27, ImeConvert_28, ImeNonconvert_29,
				ImeAccept_30, ImeModeChangeReq_31, Space_32, PageUp_33, PageDown_34,
				End_35, Home_36, LeftArrow_37, UpArrow_38, RightArrow_39,
				DownArrow_40, Select_41, Print_42, Execute_43, PrintScreen_44,
				Insert_45, Delete_46, Help_47, Main0_48, Main1_49,
				Main2_50, Main3_51, Main4_52, Main5_53, Main6_54,
				Main7_55, Main8_56, Main9_57, null, null,
				null, null, null, null, null,
				A_65, B_66, C_67, D_68, E_69,
				F_70, G_71, H_72, I_73, J_74,
				K_75, L_76, M_77, N_78, O_79,
				P_80, Q_81, R_82, S_83, T_84,
				U_85, V_86, W_87, X_88, Y_89,
				Z_90, LWindows_91, RWindows_92, Applications_93, null,
				ComputerSleep_95, Numpad0_96, Numpad1_97, Numpad2_98, Numpad3_99,
				Numpad4_100, Numpad5_101, Numpad6_102, Numpad7_103, Numpad8_104,
				Numpad9_105, NumpadMultiply_106, NumpadAdd_107, NumpadSeparator_108, NumpadSubtract_109,
				NumpadDecimal_110, NumpadDivide_111, F1_112, F2_113, F3_114,
				F4_115, F5_116, F6_117, F7_118, F8_119,
				F9_120, F10_121, F11_122, F12_123, F13_124,
				F14_125, F15_126, F16_127, F17_128, F18_129,
				F19_130, F20_131, F21_132, F22_133, F23_134,
				F24_135, null, null, null, null,
				null, null, null, null, NumLock_144,
				ScrollLock_145, null, null, null, null,
				null, null, null, null, null,
				null, null, null, null, null,
				LShift_160, RShift_161, LCtrl_162, RCtrl_163, LAlt_164,
				RAlt_165, BrowserBack_166, BrowserForward_167, BrowserRefresh_168, BrowserStop_169,
				BrowserSearch_170, BrowserFavorites_171, BrowserStartHome_172, VolumeMute_173, VolumeDown_174,
				VolumeUp_175, NextTrack_176, PrevTrack_177, StopMedia_178, PlayPauseMedia_179,
				StartMail_180, SelectMedia_181, StartApp1_182, StartApp2_183, null,
				null, Semicolon_186, Plus_187, Comma_188, Subtract_189,
				Period_190, Slash_191, Tilde_192, LBracket_193, Backslash_194,
				RBracket_195, Quote_196
			};

			for(var index = 0; index < PressingKeysArray.Length; index++) {
				if(PressingKeysArray[index] != null)
					PressingKeysArray[index] = false;
			}
			
			// Console.WriteLine(PressedKeysArray[1]);
			// PressedKeysArray[VkCodeTable.LEFT_MOUSE_BUTTON] = true;
			// Console.WriteLine(PressedKeysArray[1]);
			
			
		}

		public static bool SetKeyPressStatus_Down(int vkCode)
		{
			PressingKeysArray[vkCode] = true;
			return true;
		}


		public static bool SetKeyPressStatus_Up(int vkCode)
		{
			PressingKeysArray[vkCode] = false;
			return true;
		}

		public static int GetNumberOfPressingKeys()
		{
			return PressingKeysArray.Count(key => key != null && (bool) key);
		}

		public static void FixPressedKeysStatus()
		{
			for(var index = 0; index < PressingKeysArray.Length; index++) {
				if(PressingKeysArray[index] != null)
					PressingKeysArray[index] = false;
			}
		}
		
		
	}
}