   M S B A M L   `   `   `  ����Q  MTooLazyToPressArrowKey, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Touchpad2ArrowKeyTouchpad2ArrowKey  5   	   "    Touchpad2ArrowKey.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:Touchpad2ArrowKey     5   	     � Title$  
MainWindow��5   	   $	��600��6   $	��800��6)   .��5	      � .+�5
   
   �� -   5         ��Name$ DebuggingLogTextBox��5
      $
��Left=�6/   $	��157��6J   $��395,400,0,0q�6W    �� TextWrapping$
 Wrap=�6l   $	��Top=�6�   $	��337��6�   $��8(�6�   $��DebuggingLog:
��6�   �� 5   
   -   5         ��Name$ AngleSensitivitySlider��5      $
��Left=�61   $��
475,38,0,0q�6L   $	��Top=�6`   $	��250��6x    � Minimum$ 10Q�6�    � Maximum$ 90Q�6�    � Value$ 35Q�6�    � LargeChange$ 5Q�6�    � SmallChange$ 1Q�6�   	 �� TickPlacement$	 BottomRight=�6�   
 �� TickFrequency$
 2Q�6�   �� 5   
   -   5        $ SensitivitySlider��5      $
��Left=�6,   $��475,103,0,0q�6G   $	��Top=�6\   $	��250��6t   $ 1Q�6�   $ 10Q�6�   $ 20Q�6�   $ 5Q�6�   $ 1Q�6�   $	 BottomRight=�6�   �� 5   
   -   5         ��Name$ AngleSensitivityLabel��5      $��Angle��6/   $
��Left=�6?   $��Right=�6Z   $��
377,33,0,0q�6}   $	��Top=�6�   $��30��6�   $��87��6�   �� 5   
   -   5        $ SensitivityLabel��5      $��Sensitivity��6*   $
��Left=�6@   $��Right=�6[   $��
377,96,0,0q�6~   $	��Top=�6�   $��30��6�   $��87��6�   �� 5   
   -   5         ��Name$ DebugIsActivated��5      $��IsActivated��6-   $
��Left=�6C   $��395,379,0,0q�6^   $	��Top=�6s   {�.  6�   �� 5   
   -   5         ��Name$ ActivateButton��5      $��Activate��6)   $
��Left=�6<   $��
91,183,0,0q�6W   $	��Top=�6k   $	��120��6�   $��40��6�   6�   �� 5   
   -   5        $ DeactivateButton��5      $��
Deactivate��6+   $
��Left=�6@   $��
91,238,0,0q�6[   $	��Top=�6o   $	��120��6�   $��40��6�   6�   �� 5   
   -	   5        $  AngleSensitivityValueLabel��5      $��AngleSensitivity: 30��64   $
��Left=�6S   $��
553,56,0,0q�6n   $	��Top=�6�   $��10(�6�   $��27��6�   $	��135��6�   �� 5   
   -
   5        $ SensitivityValueLabel��5      $��Sensitivity: 10��6/   $
��Left=�6I   $��553,121,0,0q�6d   $	��Top=�6y   $��10(�6�   $��27��6�   $	��135��6�   �� 5   
   -   5        $ FreezeCursorCheckbox��5      $��Freeze Cursor��61   $
��Left=�6I   $��395,183,0,0q�6d   $	��Top=�6y   {�.  6�   5      5      