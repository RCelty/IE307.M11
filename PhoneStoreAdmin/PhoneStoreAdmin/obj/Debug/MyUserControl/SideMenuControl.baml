   M S B A M L   `   `   `  ����J  FPhoneStoreAdmin, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM+clr-namespace:PhoneStoreAdmin.MyUserControlPhoneStoreAdmin.MyUserControl  5      3    -PhoneStoreAdmin.MyUserControl.SideMenuControl   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      =local+clr-namespace:PhoneStoreAdmin.MyUserControl     5      -   5          >�Name$  
MySideMenu��5   -   $
��auto��6    � 	Resources 5	         tb_style �� 
TargetType �� Property �� Value �� 
TargetType ��Name J� SnapsToDevicePixels��%�   &	        �� 5
   
   " d�6!   .�5      ��  � �� 5      �� 5      " d�6&   .R�5      �� $ border��6!   #��z��5   !   #��z��5   !    . 5   >   5      5      5      5      .��5      � .+�5   
   � .+�5      � -   5         �Name$ nav_pn1��5      $
��Left=�6$   $��65��6?   ���#fafafa.+�5      �� -   5        	 ��Name$	 st_pn1��5      .+�5      � $��90��6   .+�5       �� $��1��6$   $��0��5!   $   $��
73,-20,0,0q�5"   $   $
��MENU��5#   $   
�������$��22(�5%   $   
������ $
��Left=�5'   $   $��Center=�5(   $   
 J� Effect
 5)      3  -System.Windows.Media.Effects.DropShadowEffect  5*   "      
BlurRadius$ 10Q�63      ShadowDepth$ 1Q�5+   3      	Direction$ 90Q�5,   3      Color$ White��5-   3   5/      d� 51      -   5         d�Name$ tg_btn��51   '   $��0��52   '   $��Center=�53   '   $
��Left=�54   '   $��
18,-20,0,0q�55   '   $��30��56   '   $��30��57   '   $��0q�58   '   .��59      �� $,��&/Assets/Images/Icons/toggle_button.png��6$    � Triggers 5;      :� 5<   "    :� RoutedEvent$ ToggleButton.Unchecked��6/   .[�5=   &   �� .��5>   *   �� -   5         ��Name$ HideStackPanel��5>   5   .��5?   .   W�  �� 
TargetName$ nav_pn1��6>    �� TargetProperty$ Width�5@   >    g� 	BeginTime$ 0:0:0X�5A   >    W� From$	 230X�5B   >   	 W� To$ 65X�6I    g� Duration$ 0:0:0:0D�5C   >   5D   /   5E   +   5F   '   5G   #   :� 5I   "   $ ToggleButton.Checked��6/   .[�5J   &   �� .��5K   *   �� -   5        $ ShowStackPanel��5K   5   .��5L   .   W� $ nav_pn1��6>   $ Width�5M   >   $ 0:0:0X�5N   >   $ 65X�5O   >   $	 230X�6H   $ 0:0:0:0D�5P   >   6S   5Q   +   5R   '   5S   #   5U      5V      �� 5Z      -   5         ��Name$ lstView��5Z      
������ 
������ $��Disabled=�5]       �� SelectedIndex 5^      �� *SelectedIndex+ �� ElementName$ 
MySideMenu��.@�5a      �� $
��Left=�6'   $��0,0,0,15q�6B   .��5b      ��  �� Orientation$ 
Horizontal=�6)   $	��230��6B   .+�5c   "   �� $)��#/Assets/Images/Icons/smartphone.png��6(   $��40��6U   $��0,0,0,0q�6a   $��Uniform=�6r    �� StretchDirection$ DownOnly=�6�   �� 5d   "   $��PRODUCTS��6,   $��25,0,0,0q�6<   $��Center=�6N   $
��Bold"�6i   5e      5f      �� 5h      $
��Left=�6'   $��0,0,0,15q�6B   .��5i      �� $ 
Horizontal=�6)   $	��230��6B   .+�5j   "   �� $'��!/Assets/Images/Icons/category.png��6(   $��40��6S   $��0,0,0,0q�6_   $��Uniform=�6p   $ DownOnly=�6�   �� 5k   "   $��
CATEGORIES��6,   $��25,0,0,0q�6>   $��Center=�6P   $
��Bold"�6k   5l      5m      �� 5o      $
��Left=�6'   $��0,0,0,15q�6B   .��5p      �� $ 
Horizontal=�6)   $	��230��6B   .+�5q   "   �� $$��/Assets/Images/Icons/brand.png��6(   $��40��6P   $��0,0,0,0q�6\   $��Uniform=�6m   $ DownOnly=�6   �� 5r   "   $��BRANDS��6,   $��25,0,0,0q�6:   $��Center=�6L   $
��Bold"�6g   5s      5t      �� 5v      $
��Left=�6'   $��0,0,0,15q�6B   .��5w      �� $ 
Horizontal=�6)   $	��230��6B   .+�5x   "   �� $&�� /Assets/Images/Icons/comment.png��6(   $��40��6R   $��0,0,0,0q�6^   $��Uniform=�6o   $ DownOnly=�6�   �� 5y   "   $��COMMENTS��6,   $��25,0,0,0q�6<   $��Center=�6N   $
��Bold"�6i   5z      5{      �� 5}      $
��Left=�6'   $��0,0,0,15q�6B   .��5~      �� $ 
Horizontal=�6)   $	��230��6B   .+�5   "   �� $#��/Assets/Images/Icons/bill.png��6(   $��40��6O   $��0,0,0,0q�6[   $��Uniform=�6l   $ DownOnly=�6~   �� 5�   "   $��BILLS��6,   $��25,0,0,0q�69   $��Center=�6K   $
��Bold"�6f   5�      5�      �� 5�      $
��Left=�6'   $��0,0,0,15q�6B   .��5�      �� $ 
Horizontal=�6)   $	��230��6B   .+�5�   "   �� $#��/Assets/Images/Icons/user.png��6(   $��40��6O   $��0,0,0,0q�6[   $��Uniform=�6l   $ DownOnly=�6~   �� 5�   "   $��USERS��6,   $��25,0,0,0q�69   $��Center=�6K   $
��Bold"�6f   5�      5�      5�      5�      5�      5�      5�      5�      