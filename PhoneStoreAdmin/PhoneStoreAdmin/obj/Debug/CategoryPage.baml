   M S B A M L   `   `   `  ����J  FPhoneStoreAdmin, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:PhoneStoreAdminPhoneStoreAdmin  5      "    PhoneStoreAdmin.CategoryPage   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      /localclr-namespace:PhoneStoreAdmin     5      -   5          N�Name$  MyCategoryPage��5	       N� Title$ CategoryPage��5
      .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      .+�5   
   �� $��Category��6   $��Center=�6$   $��40(�6A   $��0��6O   �� 5   
   -   5         ��Name$ add_btn��5      $��1��6   $��Thêm��6/   $��80��6>   $
��Left=�6I   $��15q�6d   ��6p   �� *OpenAddCategoryCommand+� 5   
   $��2��6   $��	20,0,20,0q�6    � ColumnDefinitions 5      �� 5      $	��100 �6#   �� 5      $	��200 �6#   �� 5      �� 5      .+�5      �� $��ID��6   $��Center=�6!   $��0��6<   $
��Bold"�6L   �� 5       $��Tên loại��6   $��Center=�6'   $��1��6B   $
��Bold"�6R   5!      �� 5#   
   $��3��6   ��. $
��Auto=�6h   ��6    �� *
Categories+��5$      �� 5%      .R�5&      �� 
���   �$��1q�61    �� CornerRadius$ 5��6E   ._�5'      � $��10q�6    5(      �� 5)   "   $	��100 �63   �� 5*   "   $	��200 �63   �� 5+   "   �� 5,   "   .+�5/      ��  ��Name$ ID��68   $��0��6(   $��Center=�6X   ��6D   �� *ID+6X   �� 50      $ DisplayName��68   $��1��6(   $��Center=�6j   ��6M   �� *DisplayName+6j   �� 51      $��2��6%   $��Sửa��65   $��	10,0,10,0q�6C   
��������6n   �� *%#DataContext.OpenEditCategoryCommand+ �� Source %  System.Windows.Markup.Reference     Name$ MyCategoryPage����6�   �� *.+�� 52      $��3��6%   $
��Xóa��65   $��	10,0,10,0q�6C   
���  ����6g   �� *#!DataContext.DeleteCategoryCommand+   $ MyCategoryPage����6�   �� *.+53      54      55      57      58      5:      