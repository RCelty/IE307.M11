   M S B A M L   `   `   `  ����J  FPhoneStoreAdmin, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:PhoneStoreAdminPhoneStoreAdmin  5      "    PhoneStoreAdmin.CustomerPage   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      /localclr-namespace:PhoneStoreAdmin     5      -   5          N�Name$  MyCustomerPage��5      ��.  N� Title$ CustomerPage��5      .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      .+�5   
   �� $��0��6   $��Customer��6!   $��Center=�61   $��40(�6N   � 5   
   $��1��6   $��	20,0,20,0q�6    � ColumnDefinitions 5      �� 5      $	��100 �6#   �� 5      $	��100 �6#   �� 5      $	��200 �6#   �� 5      $	��200 �6#   �� 5      �� 5      .+�5!      �� $��0��6   $��ID��6$   $��Center=�61   $
��Bold"�6L   �� 5"      $��1��6   $��Ảnh��6$   $��Center=�62   $
��Bold"�6M   �� 5#      $��2��6   $��Tên người dùng��6$   $��Center=�6=   $
��Bold"�6X   �� 5$      $��3��6   $��Quyền��6$   $��Center=�64   $
��Bold"�6O   5%      �� 5'   
   $��2��6   ��. $
��Auto=�6g   ��6    �� *	Customers+��5(      �� 5)      .R�5*      �� 
���   �$��1q�61    �� CornerRadius$ 5��6E   ._�5+      � $��10q�6    5,      �� 5-   "   $	��100 �63   �� 5.   "   $	��100 �63   �� 5/   "   $	��200 �63   �� 50   "   $	��200 �63   �� 51   "   �� 52   "   .+�55      �� $��Center=�6<   $��0��56   (   ��55   (   �� *ID+56   (   �� 57      $��50��6o   $��Uniform=�6{    �� StretchDirection$ DownOnly=�6�   $��1��58   $   ��57   $   �� *Avatar+   ImageSourceConverter �� 	Converter# [  58   $   �� 59      $��Center=�6E   $
��Bold"�6`   $��2��5:   (   ��59   (   �� *DisplayName+5:   (   �� 5;      $��Center=�6n   
����  �$��3��5<   (   ��5;   (   �� *	IsAdmin+  IsAdminConverter# [ 5<   (   �� 5=      $��Đổi quyền��6%   $��	10,0,10,0q�69   
����p��$	��100��5>   %   $��Center=�64   $��4��6Q   ��5?   )   �� *DataContext.ChangeRoleCommand+ �� Source %  System.Windows.Markup.Reference     Name$ MyCustomerPage����5@   )   �� *.+�� 5A      $��
Chi tiết��6%   $��	10,0,10,0q�68   
������$	��100��5B   %   $��Center=�64   $��5��6Q   ��5C   )   �� *DataContext.DetailCommand+   $ MyCustomerPage����5D   )   �� *.+5E      5F      5G      5I      5J      5M      