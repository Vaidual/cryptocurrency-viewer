   M S B A M L   `   `   `  ����P  Lcryptocurrency-viewer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullS.clr-namespace:cryptocurrency_viewer.Converters cryptocurrency_viewer.Converters  5   	   &     cryptocurrency_viewer.MainWindow   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   E
converters.clr-namespace:cryptocurrency_viewer.Converters     5   	     � Title$  Cryptocurrency Viewer��5   	   $	��700��6'   $
��1000��64    � 	Resources 5	      9   3cryptocurrency_viewer.Converters.AssetNameConverter   AssetNameConverterC   =cryptocurrency_viewer.Converters.BooleanToVisibilityConverter   BooleanToVisibilityConverter��%:   &	        &	        5
   
   6(     5   
   62   5      .��5      � $	��Top=�6   $��10q�6#   .+�5   
   � $	��Top=�6   $
��80 0q�6'    � ColumnDefinitions 5      �� 5      �� 5      .+�5      �� $��0��6   $
��Left=�6)   $	��Top=�6D    �� Orientation$ 
Horizontal=�6\   .+�5      �� $��60��6;   $��0 0 6 0q�6F   ��6   �� *GoBackCommand+.��6W   Back6]   �� 5      $��60��6>   ��6   �� *GoForwardCommand+.��6I   	Forward6R   5      �� 5      $	��150��6   $��1��6&   $��Right=�66   $��Center=�6R   .+�5      �� -   5        $��22��5   8   $��14(�6D   ��6   �� *SearchQuery+6R   � 5      .+�5      �� ��6   �� *SearchResults+y�5      �� *IsListBoxVisible+ �� 	Converter# [ ��5      �� 5      .R�5   "   �� ��5   &   `� 5    *    `� 	Converter# [  67   .1�5!   .   ��  �� Path$
 Name�66   �� 5"   .   $ Symbol�66   5#   +   5%   #   5&      ��5(      �� 5)       �� 
TargetType" ��6$   .�5*   "   ��  �� Property � ��	 �� Value	 5+   &   �� 5,   *   
 �� 
TargetType"
 ��6:   .R�5-   .   �� 
������ $��20��5/      $��0q�50      $
��Left=�51      ��52      ��  �� RelativeSource ��  �� AncestorType ListBox$3 -DataContext.NavigateToAssetDetailsPageCommand���56      ��  ��  ListBox$ SelectedValue�.��59   2   �� 6E   5:   /   5;   +   5=   #    �� Triggers 5>   "   P� 5?   &    P� Property �@J�IsKeyboardFocusWithin P� Value$
 True��6O   . �5@   *   ��  �@��
IsSelected$
	 True��6G   6V   5A   '   5C      5E      5F      5G      5H       � 5I   
   -   5          �Name$ 	MainFrame��5I   "    K� ZIndex$ -1��6     � NavigationUIVisibility$ Hidden=�65   $��0,42,0,0q�6U   5J      5K      