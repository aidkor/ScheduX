   M S B A M L   `   `   `  ���� B  >ScheduX, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:ScheduX.UI.ClassesScheduX.UI.Classes  &     ScheduX.UI.Lessons.LessonsWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     2local clr-namespace:ScheduX.UI.Classes     -   $	��500��$	��700��  � WindowStartupLocation$  CenterScreen=� � 
ResizeMode$ NoResize=�.���  � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� -    ��Name$ Header��$��0��$��LESSONS�����#3A36F4$
��Bold"�$��16(�$��25,0,0,0q��� -    ��Name$ Toolbar��$��1�� �� Orientation$ 
Horizontal=�.+��� -    ��Name$	 New��$��90��$��26��$��	25,26,0,0q�$	��NEW��$
��Bold"����#3A36F4
����������#3A36F4   RoundedCorners#���   �� -   $ TrashBin��{�. $��29��$��26��$��8,26,0,0q�$
��Bold"�
����������#3A36F4  DeleteButtonStyle#���  �� -    ��Name$ LessonsList��$��2��$��25,12,25,15q����#595959$	��350�� � Cursor$ Arrow��$	��Top=�$��Visible=���.  ���� 	 �� Path$	 ReportEntries�
 � ContextMenu
 �� .@�m� -   $
��edit�����	#FF343434$��0,0,0,1q� m� Icon �� $2��,/ScheduX;component/Resources/Images/edit.png��m� -   $
��copy�����	#FF343434$��0,0,0,1q� �� $2��,/ScheduX;component/Resources/Images/copy.png��m� -	   $��delete�����	#FF343434 �� $3��-/ScheduX;component/Resources/Images/trash.png�� �� View ��  �� ColumnHeaderContextMenu �� ���� .R��� 
������ 
������ P���  �� Width$	 100��$
��NAME�� �� DisplayMemberBinding �� *Name+�� $	 120��$��SUBJECT�� �� $	 Subject.Name��� $	 170��$��TEACHER�� �� $	 Teacher.FullName��� $	 170��$��AUDIENCE�� �� $	 Audience.FullName��� $	 100��$��GROUP�� �� $	 
Group.Name��� $	 100��$��GROUP�� �� $	 
Group.Name�