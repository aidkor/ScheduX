   M S B A M L   `   `   `  ����B  >ScheduX, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:ScheduX.UI.ClassesScheduX.UI.Classes  5   	   %    ScheduX.UI.Classes.GroupsWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:ScheduX.UI.Classes     5   	   -   5        $	��500��5   	   $	��700��6     � WindowStartupLocation$  CenterScreen=�6"    � 
ResizeMode$ NoResize=�6G   .��5	      �  � RowDefinitions 5
   
   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   .+�5   
   �� -   5         ��Name$ Header��5      $��0��6!   $��GROUPS��6.   ���#3A36F4$
��Bold"�6T   $��16(�6f   $��25,0,0,0q�6t   �� 5   
   -   5         ��Name$ Toolbar��5      $��1��6&    �� Orientation$ 
Horizontal=�63   .+�5      �� -   5         ��Name$	 New��5      $��90��6#   $��26��6.   $��	25,26,0,0q�6:   $	��NEW��6M   $
��Bold"�6[   ���#3A36F4
����������#3A36F4   RoundedCorners#���   6�   �� 5      -   5        $ ImportExcel��5      $��29��6*   $��26��65   $��	12,26,0,0q�6B   $
��Bold"�6U   
����������#3A36F4#���   6�   .��5      �� $+��%../../Resourses/Images/down-arrow.png��6   $��14��6G   5      5      �� 5   
   -   5         ��Name$ 
GroupsList��5      $��2��6(   $��25,12,25,15q�65   ���#595959$	��350��6`    � Cursor$ Arrow��6n   	 � ContextMenu	 5      �� 5      .@�5      m� -   5        $
��edit��5      ���	#FF343434$��0,0,0,1q�6D   
 m� Icon
 5       �� 5!      $4��.../../Resourses/Images/ContextMenuImg/edit.png��6$   5#      m� 5$      -   5        $
��copy��5$      ���	#FF343434$��0,0,0,1q�6D   
 5%      �� 5&      $4��.../../Resourses/Images/ContextMenuImg/copy.png��6$   5(      m� 5)      -	   5        $��delete��5)      ���	#FF343434
 5*      �� 5+      $5��/../../Resourses/Images/ContextMenuImg/trash.png��6$   5-      5.       �� View 50      �� 51       �� ColumnHeaderContextMenu 52      �� 53      ��54      �� 55   "   .R�56   &   �� 
������ 
������ 6G   57   #   59      .P�5<      ��  �� Width$	 140��6%    �� DisplayMemberBinding 62   �� *Name+.��5=      �� -
   5        $
��NAME��5=   /   6>   5>      �� 5?      $	 120��6%    61   �� *StudentQuantity+.��5@      �� -   5        $��STUDENTS��5@   /   6B   5A      5B      5D      5E      5F      