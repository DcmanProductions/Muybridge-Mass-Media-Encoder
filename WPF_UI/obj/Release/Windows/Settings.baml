   M S B A M L   `   `   `  ���� [  WMuybridge Mass Media Encoder, Version=1911.23.3.0, Culture=neutral, PublicKeyToken=null+clr-namespace:MMME.WindowsMMME.Windows      MMME.Windows.Settings   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     ,localclr-namespace:MMME.Windows     Y UMahApps.Metro.IconPacks.Modern, Version=2.3.0.4, Culture=neutral, PublicKeyToken=nullR NMahApps.Metro.IconPacks, Version=2.3.0.4, Culture=neutral, PublicKeyToken=null` \MahApps.Metro.IconPacks.MaterialLight, Version=2.3.0.4, Culture=neutral, PublicKeyToken=null[ WMahApps.Metro.IconPacks.Material, Version=2.3.0.4, Culture=neutral, PublicKeyToken=nullC	iconPacks-http://metro.mahapps.com/winfx/xaml/iconpacks     -     �Name$  SettingsWindow��
������� � AllowsTransparency .  � WindowStyle$
 None=� � 
ResizeMode$ NoResize=�
������ $��0q�
������� � Title$ Settings��$	��720��$
��1280��$	��720��$
��1280��.��� .+���  �� CornerRadius$ 10��$��3��$��2������ .���  � Color$ #7be9f6�� � Offset$	 0.0Q�� $ #f083da��$ 1Q��  � RowDefinitions �� $��50 ��� 	 � ColumnDefinitions	 �� �� .+�� $��2��
���   �
 J� Opacity$
 .3Q�� $��0��.+��� $��Settings��$
��Left=�$��18(�$��
28,10,0,10q�]� -    ]�Name$ NavigationBar��$��177,0,-525,0q��� $��1��.+��� -    ��Name$ CloseBtn��$��X��$��35��$��35��$��Right=�$��5q�$��25(��� -   $ MinimizeBtn��$��Center=�$��Center=�$��35��$��36��$��Right=�$��0,-75,55,-27q�.���� $��-��$��45(�$	��Top=�$
��0,-5q�$��Light"�� $��1��$��2��$��10q� �� $
��339* ��� $	��41* �	 �� $	��326 ��� �� $	��258 ��� -    ��Name$ contentTabController��
������ $
��auto��$
��auto��$��0,51,0,0q�$��0q�.@��� $��General��$��0��$��0��$
 0Q�
������ 
�������.���  �� $
��444* ��� $
��217* �.+��� $#��Overwrite Original Media File��$
��Left=�$��
36,187,0,0q�$��45��$	��301��$��24(�$	��Top=��� -    ��Name$ Overwrite_CKBX��$��342,198,0,0q�$��Right=�$��17��
�������
������ 
�������$��16��$	��Top=�$
��Left=��� $��Enclosed Folder��$
��Left=�$��
36,236,0,0q�$��45��$
��auto��$��24(�$	��Top=��� -   $ TempEncodeFolder_CKBX��
������� �� TextAlignment$
 Left=� d� 	IsChecked$
 TrueY�$��220,249,0,0q�$��Right=�$
��auto��
�������
������ 
�������$��16��$	��Top=�$
��Left=��� -    ��Name$ EnclosedFolderTxtBox��$��Source��$
��auto��$
��auto��$��24(�$��282,239,551,165q�{�.  �� -	   $ EnclosedFolderOpenBtn��$��40��$��40��$��0q�$��742,241,494,163q�.��,  &MahApps.Metro.IconPacks.PackIconEntypo  E	 AControlzEx, Version=3.0.2.4, Culture=neutral, PublicKeyToken=null� 	 �ControlzEx.PackIconBase`1[[MahApps.Metro.IconPacks.PackIconEntypoKind, MahApps.Metro.IconPacks, Version=2.3.0.4, Culture=neutral, PublicKeyToken=null]]   Kind$ Folder=�$��25��$��25���� $��Show Encoder Console��$
��Left=�$��
36,285,0,0q�$��45��$
��auto��$��24(�$	��Top=��� -
   $ EncoderConsole_CKBX��
�������$
 Left=�$
 TrueY�$��282,296,0,0q�$��Right=�$
��auto��
�������
������ 
�������$��16��$	��Top=�$
��Left=��� $��Is Network Path��$
��Left=�$��
36,333,0,0q�$��45��$
��auto��$��24(�$	��Top=��� -   $ IsNetworkPath_CKBX��
�������$
 Left=�$
 TrueY�$��251,347,0,0q�$��Right=�$
��auto��
�������
������ 
�������$��16��$	��Top=�$
��Left=��� $��	Sort Type��$
��Left=�$��
36,383,0,0q�$��45��$
��auto��$��24(�$	��Top=��� -   $��sortType_comboBox��$��25��$��220,392,816,27q� �� Text$ Tddest���� $��Directories��$��0��$��0��$
 0Q�
������ 
�������.��� .+��� $��Root Location��$��34(�$��10,200,125,0q�$��45��$	��Top=��� -   $ RootLocation_TxtBx��{�.  $��314,211,289,0q�$��Center=�$��24(�$��32��$
��Auto��$��Source��$	��Top=��� -   $ OpenRootLocation��$��0,208,151,0q�$��Right=�$��37��$��37��$	��Top=�.��.  (MahApps.Metro.IconPacks.PackIconMaterial  � 	 �ControlzEx.PackIconBase`1[[MahApps.Metro.IconPacks.PackIconMaterialKind, MahApps.Metro.IconPacks, Version=2.3.0.4, Culture=neutral, PublicKeyToken=null]]   Kind$ 
FolderOpen=�$��24��$��24���� $��Log Location��$��
10,249,0,0q�$
��Left=�$��45��$	��184��$��34(�$	��Top=��� -   $ LogLocation_TxtBx��{�.  $��314,258,289,0q�$��Center=�$��Source��$��24(�$��32��$
��Auto��$	��Top=��� -   $ OpenLogLocation��$��0,258,151,0q�$��37��$��37��$	��Top=�$��Right=�.��  $ 
FolderOpen=�$��24��$��24���� -   $ OpenLogFile��$��0,258,109,0q�$��37��$��37��$	��Top=�$��Right=�.��  $
 File=�$��24��$��24���� $��Config Location��$
��Left=�$��
10,302,0,0q�$��34(�$��45��$	��224��$	��Top=��� -   $ ConfigLocation_TxtBx��{�.  $��314,310,289,0q�$��Center=�$��Source��$��24(�$��32��$
��Auto��$	��Top=��� -   $ OpenConfigLocation��$��0,310,150,0q�$��Right=�$��37��$��37��$	��Top=�.��  $ 
FolderOpen=�$��24��$��24���� $��Installation Location��$
��Left=�$��
10,356,0,0q�$��34(�$��45��$	��281��$	��Top=��� -   $  InstallationLocation_TxtBx��{�.  $��314,367,289,0q�$��Center=�$��Source��$��24(�$��32��$
��Auto��$	��Top=��� -   $ OpenInstallationLocation��$��0,356,150,0q�$��Right=�$��37��$��37��$	��Top=�.��  $ 
FolderOpen=�$��24��$��24���� -   $ OpenConfigFile��$��0,310,108,0q�$��37��$��37��$	��Top=�$��Right=�.��  $
 File=�$��24��$��24���� $��About��$��0��$��0��$
 0Q�
������ 
�������.��� 	 �� $	��29* ��� $��1245* �.+��� $��Created by Drew Chase��$
��Auto��$
��Auto��$��24(�$��
31,185,0,0q�$
��Left=�$	��Top=�$��1���� $5��/Utilizes FFMPEG Codec for the media compression��$
��Auto��$
��Auto��$��24(�$��
31,222,0,0q�$
��Left=�$	��Top=�$��1��� .+��� -   $ UninstallBtn��$��	Uninstall��$y�Hidden=�$��48��$
��Auto��$��24(�$��Bottom=�$��875,0,240,9q�$��1���� -   $ 	UpdateBtn��$��Update��$��48��$
��Auto��$��Bottom=�$��24(�$��669,0,441,7q��� -   $ Clean_Logs_Btn��$��
Clean Logs��$��48��$
��Auto��$��24(�$��10,663,1079,9q��� -    ��Name$ build_txtblk��$��Build Placement��$��24(�$ Right=�$	
 0.6Q�$	��196��$��39��$��Right=�$��Bottom=�$��	0,0,10,12q�