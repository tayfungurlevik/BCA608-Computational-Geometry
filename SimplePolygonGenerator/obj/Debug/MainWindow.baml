   M S B A M L   `   `   `  ����Q  MSimplePolygonGenerator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:SimplePolygonGeneratorSimplePolygonGenerator  5   	   '    !SimplePolygonGenerator.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:SimplePolygonGenerator     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)   .��5	      �  � RowDefinitions 5
   
   �� 5      $��2* �6   6*   �� 5      $��8* �6   6*    � ColumnDefinitions 5   
   �� 5      $��1* �6   6,   �� 5      $��5* �6   6,   �� 5      $��1* �6   6,   .+�5   
   �� -   5        $��txtNoktaSayisi��5      $��0��6*   $��1��67   $��Poligon nokta sayısı:��6G   $��	10,10,0,0q�6d   $	��Top=�6w   6�   �� 5   
   -   5         ��Name$ sliderNokta��5      $
��Left=�6&   $	��Top=�6A   $��1��6Y   $��	10,30,0,0q�6i   $	��300��6|    � Maximum$	 500Q�6�    � Minimum$ 3Q�6�    � SmallChange$ 1Q�6�    � Value$ 3Q�6�   6�   �� 5   
   $��	Genişlik��6   $��1��6$   $��	10,50,0,0q�64   6I   �� 5   
   -   5         ��Name$ txtGenislik��5      $��1��6'   $��60.667,50,426,13q�67   $��80��6Q   $��20��6\   $
��Left=�6h   $	��Top=�6�   6�   �� 5   
   $��1��6   $��157.667,50,55.333,0q�6$   $��2��6A   $
��Left=�6U   $	��Top=�6p   �� 6�   $�	Yüksekik��6�   5      �� 5   
   -   5        $ txtYukseklik��5      $��1��6(   $��216.667,50,270,13q�68   	 J� RenderTransformOrigin$	 1.042,0.492&�6S   $��80��6w   $��20��6�   $	��Top=�6�   $
��Left=�6�   6�   �� 5   
   -   5        
 ��Name$
 
btnOlustur��5      $��Oluştur��6%   $��75��67   $��1��6B   $��30��6R   $��344.667,44,147,10q�6^   $
��Left=�6�   $	��Top=�6�   �� 5   
   -   5        $
 btnDosyayaYaz��5      $��Dosyaya Yaz��6(   $��75��6>   $��1��6I   $��30��6Y   $��424.667,44,0,0q�6e   $
��Left=�6}   $	��Top=�6�   6�   �� 5   
   -   5         ��Name$ CanvasPoligon��5      $��1��6(   $��3��65   $
��auto��6I   $
��auto��6V   ���	#FFC5F9FB5      5!      5"      