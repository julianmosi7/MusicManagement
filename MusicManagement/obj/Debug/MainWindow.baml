   M S B A M L   `   `   `  ����J  FMusicManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:MusicManagementMusicManagement  5   	   G CViewModelLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA0clr-namespace:ViewModelLib;assembly=ViewModelLibViewModelLib 5   	        MusicManagement.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:MusicManagement     5   	   :xctk)http://schemas.xceed.com/wpf/xaml/toolkit     5   	   F	viewModel0clr-namespace:ViewModelLib;assembly=ViewModelLib     5   	   -   5          � Title$  
MainWindow��5
   	   $	��450��6   $	��800��6)    � DataContext 5        ViewModelLib.ViewModel  5   
   .��5      �  � ColumnDefinitions 5   
   �� 5      6!   �� 5      $
��0.7* �6   6-   �� 5      6!    � RowDefinitions 5   
   �� 5      $
��0.1* �6   6,   �� 5      6   .+�5   
   � $��0��6   $��0��6    5      �� 5      6%   �� 5      6%   .+�5       �� $��0��6   $��6q�6'   $ �� IsSynchronizedWithCurrentItem$
 TrueY�6R   ��62   �� *	Artists+ �� SelectedItem 6w   �� *SelectedArtist+ �� Mode$ TwoWay=�6�   �� 5!      $��1��6   $��8q�6%   62   5"      � 5#   
   $��1��6   $��0��6    5$      �� 5%      6%   �� 5&      6%   .+�5)      �� $��8q�6   $��0��6#   ��63   �� *Interpreten+6R   �� 5*      $��Interpreten��6   $��6q�6*   $��1��65   6G   5+      �� 5-   
   $��Artist��6   $��2��6!   $��0��61   6@   � 5/   
   $��0��6   $��1��6    50      �� 51      6"   �� 52      6"   .+�55      &   System.Windows.Controls.DataGrid  -   5        $��dataGridRecords��55      $��0��6.      AutoGenerateColumns .  $
 TrueY�57      ��56      �� *	Records+ 58      �� *SelectedRecord+$ TwoWay=�   Columns 5:      0  *System.Windows.Controls.DataGridTextColumn  5;      ,  &System.Windows.Controls.DataGridColumn	   Width5  /System.Windows.Controls.DataGridLengthConverter$	 * 6)   
   Header$

 Year��63   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6A   ��  �� Path$
 Year�6a     5<      $	 4* 6)   $
 Titel��64    6C   �� $ RecordTitle�6j     5=      $	 * 6)   $	
 Typ��63    6@   �� $ RecordType.Descr�6l     5>      $	 * 6)   $
 Id��63    6?   �� $ Id�6]   5@      �� 5B      -   5        $��
lstArtists��5B      $��1��6(   $
 TrueY�6U   ��65   �� *	Artists+ 6z   �� *SelectedArtist+$ TwoWay=���5C      �� 5D       �� 
TargetType" ��6   .�5E      -   5        5F      5H      5I        5J   
   $��1��6   $��1��6#    .  ��5K      �� *Songs+ 5L        5M      $	 * 6%   $
 #��6/    6:   �� $ Id�6X     5N      $	 4* 6%   $
 Titel��60    6?   �� $ 	SongTitle�6d   5P      R� 5R   
   -   5        $��treeView��5R      $��2��6#   $��1��63   6�   5S      5T      