   M S B A M L   `   `   `  ����J  FPhoneStoreAdmin, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:PhoneStoreAdminPhoneStoreAdmin  5          PhoneStoreAdmin.BrandPage   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      /localclr-namespace:PhoneStoreAdmin     5      -   5          N�Name$  MyBrandPage��5	       N� Title$ 
Brand Page��5
      .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      .+�5   
   �� $��Brand��6   $��Center=�6!   $��40(�6>   $��0��5      �� 5   
   -   5         ��Name$ add_btn��5      $��Thêm��6"   $��80��61   $
��Left=�6<   $��15q�6W   $��1��5      ��5   c   �� *OpenAddBrandCommand+5      � 5   
   $��2��6   $��	20,0,20,0q�6    � ColumnDefinitions 5      �� 5      $	��100 �6#   �� 5      $	��100 �6#   �� 5      $	��200 �6#   �� 5      �� 5      .+�5"      �� $��0��6   $��ID��6$   $��Center=�61   $
��Bold"�6L   �� 5#      $��1��6   $
��Logo��6$   $��Center=�63   $
��Bold"�6N   �� 5$      $��2��6   $��Tên nhãn hiệu��6$   $��Center=�6<   $
��Bold"�6W   5%      �� 5'   
   ��. $
��Auto=�6W   $��3��6�   ��6   �� *Brands+��5(      �� 5)      .R�5*      �� 
���   �$��1q�61    �� CornerRadius$ 5��6E   ._�5+      � $��10q�6    5,      �� 5-   "   $	��100 �63   �� 5.   "   $	��100 �63   �� 5/   "   $	��200 �63   �� 50   "   �� 51   "   .+�54      ��  ��Name$ ID��68   $��0��6(   $��Center=�6X   ��6D   �� *ID+6X   �� 55      $��1��6$   $��Uniform=�6M    �� StretchDirection$ DownOnly=�6_   ��64   �� *Image+6_   �� 56      $ DisplayName��68   $��2��6(   $��Center=�6j   ��6M   �� *DisplayName+6j   �� 57      $��3��6%   $��Sửa��65   $��	10,0,10,0q�6C   
��������6n   �� *" DataContext.OpenEditBrandCommand+ �� Source %  System.Windows.Markup.Reference  	   Name$	 MyBrandPage����6�   �� *.+�� 58      $��4��6%   $
��Xóa��65   $��	10,0,10,0q�6C   
���  ����6g   �� * DataContext.DeleteBrandCommand+   $	 MyBrandPage����6�   �� *.+59      5:      5;      5=      5>      5@      