   M S B A M L   `   `   `  ����[  WMuybridge Mass Media Encoder, Version=1911.23.3.0, Culture=neutral, PublicKeyToken=nullclr-namespace:WPF_UIWPF_UI  5   	       WPF_UI.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:WPF_UI     5   	   Y UMahApps.Metro.IconPacks.Modern, Version=2.3.0.4, Culture=neutral, PublicKeyToken=nullR NMahApps.Metro.IconPacks, Version=2.3.0.4, Culture=neutral, PublicKeyToken=null` \MahApps.Metro.IconPacks.MaterialLight, Version=2.3.0.4, Culture=neutral, PublicKeyToken=null[ WMahApps.Metro.IconPacks.Material, Version=2.3.0.4, Culture=neutral, PublicKeyToken=nullC	iconPacks-http://metro.mahapps.com/winfx/xaml/iconpacks     5   	   -   5        
�������  � AllowsTransparency  .  � WindowStyle$
 None=�5   	    � 
ResizeMode$ CanResizeWithGrip=�5   	   
������  � Title$! Muybridge: MassMediaEncoder��5   	   $	��490��5   	   $	��865��5   	   $	��490��5   	   $	��865��5   	   .��5      � .+�5   
   �  � RowDefinitions 5      �� 5      $��45 �6    �� 5       �� 5!       � ColumnDefinitions 5#      �� 5$      �� 5%      .+�5(      ��  �� CornerRadius$ 10��6   $��3��6'   $��2��68   ��5)      �� 5*      .��5+      �  � Color$ #7E59FF��6'    � Offset$	 0.0Q�67   � 5,      $ #FF2FCD��6'   $ 1Q�67   5-      5/      � 51      
���   �	 J� Opacity$	 .3Q�6&   $��2��63   � 52      
���   �$	 .1Q�6&   $��2��63   $��3��6G   �� 54      $��0��6   $��2��6&   .+�56      �� -   5        $��CloseBtn��56      $��Right=�6)   $��Stretch=�6E   $��25(�6a   $��X��6o   $��35��6{   $��35��6�   $��5q�6�   6�   �� 57      -   5        
 ��Name$
 MinimizeBtn��57      $��Center=�6.   $��Center=�6R   $��35��6t   $��36��6   $��Right=�6�   $��0,-75,55,-27q�6�   .��58      �� $��-��6    $��45(�6)   $	��Top=�67   $
��0,-5q�6O   $��Light"�6]   59      5:      �� 5<      $	��Top=�6   $��0,25,0,0q�61    J� RenderTransformOrigin$ 0.501,1.308&�6C   $��1��6g   .+�5>      �� $	��261��6   $��55��6(   $��WrapWithOverflow=�64    �� TextAlignment$ Center=�6T   $"��Muybridge Mass Media Encoder��6k   $��Light"�6�   $��	helvetica)�6�   $��22(�6�   $��Center=�6�   $��
98,40,74,0q�6�   5?      �� 5@      $��2��6   $��0��6&   .+�5A      �� -   5         ��Name$ FileLocationTxb��5A      {�.   �� TextWrapping$ NoWrap=�6E   $��	helvetica)�6[   $��Light"�6r   $��Source��6�   $��20(�6�   
�������
������ $��0q�6�   $��Center=�6�   $	 1Q�6  $��35��6  $��
99,0,110,0q�6  �� 5C      -   5        $
 OpenFolderBtn��5C      
������ $	��Top=�6I   $��0q�6a   $��Right=�6�   $��40��6�   $��40��6�   $��
0,-35,65,0q�6�   .��5D      .  (MahApps.Metro.IconPacks.PackIconMaterial  E	 AControlzEx, Version=3.0.2.4, Culture=neutral, PublicKeyToken=null� 	 �ControlzEx.PackIconBase`1[[MahApps.Metro.IconPacks.PackIconMaterialKind, MahApps.Metro.IconPacks, Version=2.3.0.4, Culture=neutral, PublicKeyToken=null]]   Kind$ 
FolderOpen=�61   $��25��6C   $��25��6N   5E      �� 5F      -   5        $
 
ProcessBtn��5F      $��Process��6-   $��25(�6?   $	��109��6M   $��36��6Y   $��0,25,0,0q�6e   6w   � 5H      $��50q�6   $��50��6#   .+�5I      �� -   5        $
 SettingsBtn��5I      $��36��62   $��36��6=   $��
45,7,252,7q�6I   $��Center=�6]   .��5J        $ Settings=�65   5K      �� 5L      -   5        $
 StopBtn��5L      $��36��6.   $��36��69   $��139,7,158,7q�6E   $��Center=�6Z   .��5M        $
 Stop=�65   5N      �� 5P      -	   5        $
 SkipBtn��5P      $��36��6.   $��36��69   $��
224,7,73,7q�6E   $��Center=�6Y   .��5Q        $ SkipNext=�65   5R      5S      5T      � 5U      $��1��6   $��2��6#   $��1��64   $��Stretch=�6A   $��Stretch=�6]   .+�5V      �� -
   5        $��ConsoleOutputScrView��5V      $��Hidden=�6;   $��Hidden=�6o   $��Stretch=�6�   $��Stretch=�6�   $��	5,10,5,32q�6�   $��0q�6�   .��5W      � $
��auto��6   $��Stretch=�6)   $��Stretch=�6E   .+�5X      �� $ 10��6!   $��3��63   $��2��6D   $	 .5Q�6X   ��5Y      �� 5Z   "   .��5[   &   � $
 #FFF��63   $ 0Q�6@   � 5\   &   $ #FFDF00��63   $ 1Q�6C   5]   #   5_      �� 5`      -   5         ��Name$ ConsoleOutputTxtBlk��5`   $   $��Stretch=�6A   $��Stretch=�6]   $
��Wrap=�6{   $��0,0,0,0q�6�   ���#9c9c9c
�������$		 0.5Q�6�   $
��Auto��6�   $
��auto��6�   $��12(�6�   5a      5b      � 5c      $��Bottom=�6   $��Stretch=�62   .+�5d      ]� $��Stretch=�6    $��Stretch=�6<   $��27��6Z   .+�5e      �� -   5        $ CurrentSize_Txb��5e   $   $��Current Size: N/A��6=   $	��198��6V   �� 5f      -   5        $ OriginalSize_Txb��5f   $   $��Original Size: N/A��6>   $	��198��6X   5g      5h      5i      5l      5m      5n      