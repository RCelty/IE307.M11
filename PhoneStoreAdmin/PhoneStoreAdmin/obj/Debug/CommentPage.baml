   M S B A M L   `   `   `  ����J  FPhoneStoreAdmin, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:PhoneStoreAdminPhoneStoreAdmin  5      !    PhoneStoreAdmin.CommentPage   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      /localclr-namespace:PhoneStoreAdmin     5      -   5          N�Name$  MyCommentPage��5	       N� Title$ CommentPage��5
      .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      .+�5   
   �� $��0��6   $��Comment��6!   $��Center=�60   $��40(�6M   � 5   
   $��1��6   $��	20,0,20,0q�6    � ColumnDefinitions 5      �� 5      $��50 �6#   �� 5      $	��150 �6#   �� 5      $	��300 �6#   �� 5      $	��100 �6#   �� 5      $
��auto �6#   �� 5      $
��auto �6#   .+�5      �� $��0��6   $��ID��6$   $��Center=�61   $
��Bold"�6L   �� 5       $��1��6   $��Tên người dùng��6$   $��Center=�6=   $
��Bold"�6X   �� 5!      $��2��6   $��
Nội dung��6$   $��Center=�67   $
��Bold"�6R   �� 5"      $��3��6   $��Đánh giá��6$   $��Center=�67   $
��Bold"�6R   5#      �� 5%   
   $��2��6   ��. $
��Auto=�6f   ��6    �� *
Comments+��5&      �� 5'      .R�5(      �� 
���   �$��1q�61    �� CornerRadius$ 5��6E   ._�5)      � $��10q�6    5*      �� 5+   "   $��50 �63   �� 5,   "   $	��150 �63   �� 5-   "   $	��300 �63   �� 5.   "   $	��100 �63   �� 5/   "   $
��auto �63   �� 50   "   $
��auto �63   .+�53      ��  ��Name$ ID��68   $��0��6(   $��Center=�6X   ��6D   �� *ID+6X   �� 54      $ DisplayName��68   $��1��6(   $��Center=�6k   $
��Bold"�6�   ��6M   �� *CustomerName+6�   �� 55      $ Content��68   $��2��6(   $��Center=�6b   ��6I   �� *	Content+6b   �� 56      $ Rating��68   $��3��6(   $��Center=�6y   ��6H   �� *Rating+ �� StringFormat$  {0} sao��6y   �� 57      $��4��6%   $��
Chi tiết��65   $��	10,0,10,0q�6H   
����p��$	��100��58   %   $��Center=�64   ��57   u   �� *DataContext.DetailCommand+ �� Source %  System.Windows.Markup.Reference     Name$ MyCommentPage����6�   �� *.+58   4   �� 59      $��5��6%   $
��Xóa��65   $��	10,0,10,0q�6C   
���  ��$	��100��5:   %   $��Center=�64   ��59   g   �� *DataContext.DeleteCommand+   $ MyCommentPage����6�   �� *.+5:   4   5;      5<      5=      5?      5@      5B      