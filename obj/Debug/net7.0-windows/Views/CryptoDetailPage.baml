   M S B A M L   `   `   `  ����P  Lcryptocurrency-viewer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:cryptocurrency_viewer.Viewscryptocurrency_viewer.Views  5      g cLiveChartsCore.SkiaSharpView.WPF, Version=2.0.0.0, Culture=neutral, PublicKeyToken=cc92a3e13d40c573}Xclr-namespace:LiveChartsCore.SkiaSharpView.WPF;assembly=LiveChartsCore.SkiaSharpView.WPF LiveChartsCore.SkiaSharpView.WPF 5      S.clr-namespace:cryptocurrency_viewer.Converters cryptocurrency_viewer.Converters  5      2    ,cryptocurrency_viewer.Views.CryptoDetailPage   5      W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ;local)clr-namespace:cryptocurrency_viewer.Views     5      hlvcXclr-namespace:LiveChartsCore.SkiaSharpView.WPF;assembly=LiveChartsCore.SkiaSharpView.WPF     5      E
converters.clr-namespace:cryptocurrency_viewer.Converters     5        N� Title$  CryptoDetailPage��5       � 	Resources 5      ;   5cryptocurrency_viewer.Converters.ChangeColorConverter   ChangeColorConverterA   ;cryptocurrency_viewer.Converters.AbbreviatedNumberConverter  AbbreviatedNumberConverter@  :cryptocurrency_viewer.Converters.DecimalFormatterConverter  DecimalFormatterConverter �� 
TargetType �� Property �� Value��%�   &	        &	      &	 &     '�� 9       5   
   6*     5   
   60     5   
   6/   �� 5   
   " ��6   .�5      ��  � ��$ 18(�6)   5      5      .��5      �� 
�������$��Center=�6$   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��36q�6.   .+�5      ��  �� CornerRadius$ 50��6   $��0 0 10 0q�6'   $��40��5      $��40��5      
����ح�._�5      �� $��Center=�6   $��Center=�69   $
��Bold"�6T   
���������6f   �� *
Asset.Rank+6�   5      �� 5      $��0 0 68 0q�6   .+�5      �� -   5         ��Name$ currencyNameTextBlock��5   .   $
��Bold"�6   $��0 0 0 12q�6M   ��5      `� 5       �� StringFormat$ 	{0} ({1})��6'   .1�5       �� 	 �� Path$	 
Asset.Name�6&   �� 5!      $	 Asset.Symbol�6&   5"      5$      �� 5%      $ 
Horizontal=�6   .+�5&      �� $
��Bold"�6    ��5'       �� *Asset.PriceUsd+
 �� 	Converter#
 [ �� 5(      $
��Bold"�6    $��14 0 0 0q�5+       ��5)       �� *Asset.ChangePercent24Hr+$ {0:F2}%����5*       �� *Asset.ChangePercent24Hr+#
 [  5+       5,      5-      �� 5.      .+�5/      �� $��0 0 0 2q�6   Volume (24Hr)6<   �� 50      $
��Bold"�6   ��51      �� *Asset.VolumeUsd24Hr+#
 [ 52      53      �� 54   
   Last day statistic6(   5  /LiveChartsCore.SkiaSharpView.WPF.CartesianChart  55   
   -   5          Name$ 	mainChart��55         TooltipFindingStrategy$ CompareOnlyXTakeClosest=�56   M   $	��308��6~   $	��749��6�      Series 55   0   �� *Series+   YAxes 56      �� *YAxes+   XAxes 65   �� *XAxes+58      �� 59   
   .��5:      �� $	��213��67   ��6   �� *	Markets+ �� View 5;      �� 5<      .P�5=      �� $��Exchange��6)    �� DisplayMemberBinding 6;   �� *
ExchangeId+�� 5>      $
��Pair��6)    �� CellTemplate 5?      �� 5@   "   .R�5A   &   �� ��5B   *   `� 5C   .   $ {0}/{1}��6;   .1�5D   2   �� $	 
BaseSymbol�6:   �� 5E   2   $	 QuoteSymbol�6:   5F   /   5H   '   5I   #   5K      �� 5L      $��Price��6)    68   �� *
PriceUsd+ 5M      �� 5N   "   .R�5O   &   �� ��5P   *   �� 5Q   .   $	 PriceUsd�66   #
 [ 6F   5S   '   5T   #   5V      �� 5W      $��Volume (24Hr)��6*    6A   �� *VolumeUsd24Hr+#
 [ �� 5X      $��
Volume (%)��6)    6=   �� *VolumePercent+$ {0:F2}%���� 5Y      $
��Link��6)    5Z      �� 5[   "   .R�5\   &   �� 
����  � � Cursor$
 Hand��6_   ��60   �� *ExchangeUrl+ J� InputBindings 5]   *   d� 5^   .    d� Gesture$ 	LeftClicka�6;    �� Command 6O   �� *DataContext.OpenLinkCommand+ �� RelativeSource ��  �� AncestorType" N� �� CommandParameter 6�   �� *ExchangeUrl+5`   '   5a   #   5c      5d      5f      5g      5h      5i      