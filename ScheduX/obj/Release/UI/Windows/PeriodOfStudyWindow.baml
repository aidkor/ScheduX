   M S B A M L   `   `   `  ���� O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089I2clr-namespace:System.Collections;assembly=mscorlibSystem.Collections  B >ScheduX, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:ScheduXScheduX 2   ,ScheduX.UI.PeriodOfStudy.PeriodOfStudyWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     Bcol2clr-namespace:System.Collections;assembly=mscorlib     'localclr-namespace:ScheduX     -   $	��500��$	��700��  � WindowStartupLocation$  CenterScreen=� � 
ResizeMode$ NoResize=�.���  � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� -    ��Name$ Header��$��0��$��PERIOD OF STUDY�����#3A36F4$
��Bold"�$��16(�$��25,0,0,0q��� -    ��Name$ Toolbar��$��1�� �� Orientation$ 
Horizontal=�.+��� -    ��Name$ Select��$��90��$��26��$��	25,26,0,0q�$��SELECT��$
��Bold"����#3A36F4
����������#3A36F4   RoundedCorners#���   �� -   $	 New��$��90��$��26��$��8,26,0,0q�$	��NEW��$
��Bold"����#3A36F4
����������#3A36F4#���   �� -   $ ImportExcel��$��29��$��26��$��8,26,0,0q�$
��Bold"�
����������#3A36F4  ImportButtonStyle#���  �� -   $ TrashBin��{�. $��29��$��26��$��8,26,0,0q�$
��Bold"�
����������#3A36F4  DeleteButtonStyle#���  �� -    ��Name$ PeriodsList��$��2��$��25,12,25,15q����#595959$	��350�� � Cursor$ Arrow��	 � ContextMenu	 �� .@�m� -	   
 m�Name$
 ContextMenuSelectButton��$��select�����	#FF343434$	��0,1q� m� Icon �� $4��./ScheduX;component/Resources/Images/select.png��m� -
   $
 ContextMenuEditButton��$
��edit�����	#FF343434$��0,0,0,1q� �� $2��,/ScheduX;component/Resources/Images/edit.png��m� -   $
 ContextMenuCopyButton��$
��copy�����	#FF343434$��0,0,0,1q� �� $2��,/ScheduX;component/Resources/Images/copy.png��m� -   $
 ContextMenuDeleteButton��$��delete�����	#FF343434 �� $3��-/ScheduX;component/Resources/Images/trash.png�� �� View ��  �� ColumnHeaderContextMenu �� ���� .R��� 
������ 
������ .P���  �� Width$	 140�� �� DisplayMemberBinding �� *Name+.���� -   $
��NAME���� $	 160�� �� *Start+.���� -   $��STARTS���� $	 160�� �� *End+.���� -   $
��ENDS��