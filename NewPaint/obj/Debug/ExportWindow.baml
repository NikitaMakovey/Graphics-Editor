   M S B A M L   `   `   `  ����C  ?NewPaint, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:NewPaintNewPaint  5   	       NewPaint.ExportWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:NewPaint     5   	     � Title$  
Window SLE��5   	   $	��100��5	   	   $	��500��6   $	��400��5
   	   $	��100��6    � 
ResizeMode$ NoResize=�5   	   .��5      � -   5        $��mainGrid��5       � ColumnDefinitions 5   
   �� 5      $	��15* �6   6-   �� 5      $	��30* �6   6-   �� 5      $	��30* �6   6-   �� 5      $	��15* �6   6-    � RowDefinitions 5   
   �� 5      6   �� 5      6   .+�5   
   �� $��0��6   $��0��6"   $��Stretch=�6/    �� TextAlignment$ Center=�6K    �� 
IsReadOnly .  J� RenderTransformOrigin$ 0.487,0.701&�6t   .��6�   
File name:6�   �� 5   
   -   5         ��Name$ pathBox��5      $��1��6#   $��2��63   $��0��6G    .  6i   �� 5   
   -   5        $ nameBox��5      $��3��6#   $��0��63   6B   �� 5   
   -   5        $��0��5      $��1��6!   .��6D   Save6J   �� 5   
   -   5        $��1��5      $��1��6!   .��6C   Export to SVG6R   �� 5   
   -   5        $��2��5      $��1��6!   .��6C   Export to PNG6R   �� 5   
   -   5        $��3��5      $��1��6!   .��6D   Load6J   5       5!      