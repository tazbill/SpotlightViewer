   M S B A M L   `   `   `  ����J  FSpotlightViewer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:SpotlightViewerSpotlightViewer  5   	   A%clr-namespace:SpotlightViewer.HelpersSpotlightViewer.Helpers  5   	        SpotlightViewer.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   /localclr-namespace:SpotlightViewer     5   	   6conv%clr-namespace:SpotlightViewer.Helpers     5   	   -   5          � Title$  SpotlightViewer��5   	   $
��1050��6!   $
��1600��6/    � 
ResizeMode$ NoResize=�6<      ApplicationBackgroundBrush#��[  5   	    � 	Resources 5	      :   4SpotlightViewer.Helpers.ImageToBitmapSourceConverter  ImageConverter7   1SpotlightViewer.Helpers.BoolToVisibilityConverter  boolToVisConverter��%:   &	       &	        5
   
   6,     5   
   6)   5      .��5      � -   5         �Name$ 	container��5      $
��1050��6   $
��1600��6-    � RowDefinitions 5   
   �� 5      $	��800 �6   �� 5      $	��248 �6   .+�5   
   � $��0��6    5      �� 5      $��* �6    �� 5      $��40 �6    .+�5      ��  J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5         
BlurRadius$ 25Q�6'      Color$ 	#FF552B15��67      	Direction$	 315Q�6I   ._�5      �� -   5        	 ��Name$	 PreviewViewer��5      $��0��6/   $��Center=�6<   $	��768��6Y   $��24q�6f   ��6r   �� *SelectedImage.FullImage+
 �� 	Converter#
 [ 5      5      �� 5      -   5         ��Name$ 
SaveButton��5      $��1��6)   $
��Save��66     RoundCornerButton#��[ 6^   $��4q�6�   $	��125��6�   y�6�   �� *ShowSaveButton+#
 [ 6�   5       �� 5!   
   -   5         ��Name$ ThumbnailViewer��5!      $��1��6,   $	��250��69   
������ $��0q�6_    �� SelectionMode$ Single=�6�   ��6s   �� *SpotlightImages+��5"      �� 5#      .R�5$      ��  �� Orientation$ 
Horizontal=�6!   6<   5%      ��5'      �� 5(      .R�5)      �� $	��200��6d   $��Uniform=�6p   ��6   �� *	Thumbnail+#
 [ 6p   5*      5,      5-      5.      