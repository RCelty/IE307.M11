   M S B A M L   `   `   `  ����J  FPhoneStoreAdmin, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:PhoneStoreAdminPhoneStoreAdmin  5          PhoneStoreAdmin.BillPage   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      /localclr-namespace:PhoneStoreAdmin     5      -   5          N�Name$  
MyBillPage��5
       N� Title$ 	Bill Page��5      .��5      �  � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      .+�5   
   �� $
��Bill��6   $��Center=�6    $��40(�6=   $��0��6K   � 5   
   $��1��6   $��	20,0,20,0q�6    � ColumnDefinitions 5      �� 5      $��50 �6#   �� 5      $	��150 �6#   �� 5      $	��300 �6#   �� 5      $	��100 �6#   �� 5      �� 5      .+�5       �� $��0��6   $��ID��6$   $��Center=�61   $
��Bold"�6L   �� 5!      $��1��6   $��Tên người dùng��6$   $��Center=�6=   $
��Bold"�6X   �� 5"      $��2��6   $��Tổng cộng��6$   $��Center=�68   $
��Bold"�6S   �� 5#      $��3��6   $��Tình trạng��6$   $��Center=�69   $
��Bold"�6T   5$      �� 5&   
   $
��Auto=�61   $��2��6a   ��6   �� *Bills+��5'      �� 5(      .R�5)      �� 
���   �$��1q�61    �� CornerRadius$ 5��6E   ._�5*      � $��10q�6    5+      �� 5,   "   $��50 �63   �� 5-   "   $	��150 �63   �� 5.   "   $	��300 �63   �� 5/   "   $	��100 �63   �� 50   "   �� 51   "   .+�54      ��  ��Name$ ID��68   $��0��6(   $��Center=�6X   ��6D   �� *ID+6X   �� 55      $ DisplayName��68   $��1��6(   $��Center=�6k   $
��Bold"�6�   ��6M   �� *CustomerName+6�   �� 56      $ 
TotalPrice��68   $��2��6(   $��Center=�57   (   ��56   L   �� *
TotalPrice+   PriceToStringConverter �� 	Converter# [  57   (   �� 58      $ Status��68   $��3��6(   $��Center=�59   (   
����  ���58   H   �� *
IsCheckOut+  IsCheckoutConverter# [ 59   C   �� 5:      $��4��6%   $��Xác nhận thanh toán��65   $��	10,0,10,0q�6S   
����p��$	��100��5<   %   $��Center=�64   {�5:   �   �� *
IsCheckOut+  IsNotCheckoutConverter# [ ��5;   %   �� *DataContext.CheckoutCommand+ �� Source %  System.Windows.Markup.Reference     Name$ 
MyBillPage����6{   �� *.+5<   4   �� 5=      $��5��6%   $��
Chi tiết��65   $��	10,0,10,0q�6H   
������$	��100��5?   %   $��Center=�64   ��5>   %   �� *DataContext.DetailCommand+   $ 
MyBillPage����6y   �� *.+5?   4   5@      5A      5B      5D      5E      5H      