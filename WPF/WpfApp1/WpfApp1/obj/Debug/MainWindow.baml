   M S B A M L   `   `   `  ����B  >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp1WpfApp1  5   	       WpfApp1.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:WpfApp1     5   	   -   5          �Name$  MainWind��5   	    � Title$ 
MainWindow��5
   	   $	��768��6   $
��1280��6)   $	��768��66   $
��1024��6F    � 	Resources 5       �� 
TargetType��%+   '	�       �� 5   
   " 	�6   5      5      .��5      �  � ShowGridLines .   � RowDefinitions 5   
   �� 5      $	��10* �6   5      �� 5      $	��57* �6   6+    � ColumnDefinitions 5   
   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!   .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ 	dataGrid1��5      $��1��6&   $��3��63   $y�Hidden=�6G      
IsReadOnly . 	   SelectionMode$	 Single=�6�   5         5"   
   -   5        $ dataGridJournal��5"      $��1��6,   $��5��69    . $y�Hidden=�6_   
 � ContextMenu
 5#      �� 5$      .@�5%      m� -   5        $��Провести��5%      6J   m� 5&      -   5        $0��*Сделать не проведённым��5&      6X   m� 5'      $��Удалить��6   62   5(      5*        5,   
   -   5        $ dataGridSellJournal��5,      $��1��60   $��5��6=    . $y�Hidden=�6c   
 5-      �� 5.      .@�5/      m� -   5        $��Провести��5/      6N   m� 50      -   5        $0��*Сделать не проведённым��50      6\   52      54      	� 56   
   -	   5         	� 	GroupName$ 	mainChose��56      $��0��6,   $��0��69   $��
10 5 10 80q�6I   $%��Журнал Расходных��6|   	� 57   
   -
   5        $ 	mainChose��57      $��0��6,   $��1��69   $��
10 5 10 80q�6I   $%��Журнал Приходных��6{   6�   	� 58   
   -   5         	�Name$ rbPriceList��58      $ 	mainChose��6+   $��0��6A   $��2��6N   $��
10 5 10 80q�6^   $��Номенклатура��6r   6�   	� 59   
   -   5        $ 	mainChose��59      $��0��6,   $��3��69   $��
10 5 10 80q�6I   $��Контрагенты��6]   6�   � 5;   
   -   5         �Name$ wrapPanelDocBtns��5;      $��0��6.   $��0��6>   $��3��6K   .+�5<      �� -   5         ��Name$ stacDocBtns��5<       �� Orientation$ 
Horizontal=�6.   $��	0 30 0 0 q�6G   $y�Hidden=�6Z   .+�5=      �� -   5         ��Name$ AddSelDocBtn��5=      $��20��61   $��5q�6=   .��6c   20Новая Расходная накладная6~   �� 5>      -   5        $ AddPurDocBtn��5>      $��20��6/   $��5q�6;   .��6a   20Новая Приходная накладная6|   �� 5?      -   5        $ 	DelDocBtn��5?      $��20��6,   $��5q�68   .��6\   Удалить6e   5@      �� 5A      -   5        $ labelsStackPanelFilters��5A      $ 
Horizontal=�6:   .+�5B      �� $	��100��6   $��23��6$   $��1q�60   .��6;   штрихкод6E   �� 5C      $	��100��6   $��23��6$   $��1q�60   .��6;   Наименование6I   �� 5D      $	��100��6   $��23��6$   $��1q�60   .��6;   
автор6B   �� 5E      $	��100��6   $��23��6$   $��1q�60   .��6;   
серия6B   �� 5F      $	��100��6   $��23��6$   $��1q�60   .��6;   секция6C   5G      �� 5H      -   5        $ stackPanelFilters��5H      $ 
Horizontal=�64   .+�5I      �� -   5         ��Name$ tbBarcodeFilter��5I      $	��100��63   $��3q�6?   6p   �� 5J      -   5        $ tbTitleFilter��5J      $	��100��61   $��3q�6=   6n   �� 5K      -   5        $ tbAuthorFilter��5K      $	��100��62   $��3q�6>   6o   �� 5L      -   5        $ tbSerieFilter��5L      $	��100��61   $��3q�6=   6n   �� 5M      -   5        $ tbSectionFilter��5M      $	��100��63   $��3q�6?   6p   5N      5O      � 5P   
   -   5        $ wrapPanelClientFilter��5P      $��0��63   $��0��6C   $��2��6P   $��
109 59 0 0q�6d   .+�5Q      �� -   5        $ stacClientFilter��5Q      .+�5R      �� $��1q�6   .��6#   имя6(   �� 5S      -   5        $ tbCustTitleFilter��5S      $��1q�65   $	��100��6@   6z   5T      5V      � 5Y   
   $��3��6   $��2��6$   $��0 30 0 0q�68   .+�5Z   
   �� -   5        $ spBook��5Z      $��1��6%   $ 
Horizontal=�65   $��Center=�6O   $��2��6l   .+�5[      �� -   5        $ 
AddBookBtn��5[      $��20��6*   $��0��66   $��2��6C   $��5q�6S   .��6x   Добавить Книгу6�   �� 5\      -   5        $ EditBookBtn��5\      $��20��6*   $��0��66   $��2��6C   $��5q�6S   .��6y   Редактировать6�   �� 5]      -   5        $ 
DelBookBtn��5]      $��20��6.   $��0��6:   $��2��6G   $��5q�6W   .��6w   Удалить6�   5^      �� 5`   
   -    5        $ spCust��5`      $��0��6%   $��3��62   $��2��6B   $ 
Horizontal=�6V   .+�5a      �� -!   5        $ 
AddCustBtn��5a      $��20��6*   $��5q�66   .��6Z   !Добавить Клиента6l   �� 5b      -"   5        $ EditCustBtn��5b      $��20��6.   $��5q�6:   .��6`   Редактировать6o   �� 5c      -#   5        $ 
DelCustBtn��5c      $��20��6.   $��5q�6:   .��6[   Удалить6d   5d      5e      �� 5f   
   -$   5         ��Name$ imgDesc��5f      $��1��6"   $��4��6/   $	��300��6?   $	��150��6L   $��25,20,25,310q�6X   6p   �� 5g   
   -%   5        $ descStackPanel��5g      $y�Hidden=�6-   $��1��6A   $��3��6N   $��2��6^   $��5q�6r   .+�5h      �� .��6   Наименование6"   �� 5i      -&   5        $ tbBookTitle��5i       �� TextWrapping$
 Wrap=�6+    �� 
IsReadOnly . $��2q�6Q   $	��200��6\   $
��Left=�6h   6�   �� 5j      .��6   Штрихкод6   �� 5k      -'   5        $ 	tbBarcode��5k       . $��2q�6;   $	��200��6F   $
��Left=�6R   6o   �� 5l      .��6   &$Год первого издания6)   �� 5m      -(   5        $ tbFirstYear��5m       . $��2q�6=   $	��200��6H   $
��Left=�6T   6q   �� 5n      .��6   ,*Год последнего издания6,   �� 5o      -)   5        $ 
tbLastYear��5o       . $��2q�6<   $	��200��6G   $
��Left=�6S   6p   �� 5p      .��6   
Серия6   �� 5q      -*   5        $ tbSeria��5q       . $��2q�69   $	��200��6D   $
��Left=�6P   6m   �� 5r      .��6   Секция6   �� 5s      -+   5        $ 	tbSection��5s       . $��2q�6;   $	��200��6F   $
��Left=�6R   6o   �� 5t      .��6   
Автор6   �� 5u      -,   5        $ tbAuthor��5u       . $��2q�6:   $	��200��6E   $
��Left=�6Q   6n   �� 5v      .��6   Издатель6   �� 5w      --   5        $ tbPublisher��5w       . $��2q�6=   $	��200��6H   $
��Left=�6T   6q   �� 5x      .��6   Закупочная цена6%   �� 5y      -.   5        $ tbPurchasePrice��5y       . $��2q�6A   $	��200��6L   $
��Left=�6X   6u   �� 5z      .��6   Розничная цена6$   �� 5{      -/   5        $ tbRetailPrice��5{       . $��2q�6?   $	��200��6J   $
��Left=�6V   6s   �� 5|      .��6   Описание6   �� 5}      -0   5        $ tbDescription��5}      " �� VerticalScrollBarVisibility$ Visible=�6-   $
 Wrap=�6T   $��Center=�6h    . $
��auto��6�   6�   5�      �� 5�   
   -1   5        $ CustDescStackPanel��5�      $��1��61   $��3��6>   $��2��6N   $y�Hidden=�6b   .+�5�      �� .��6   )'Наименование Клиента6*   �� 5�      -2   5        $ tbCustTitle��5�       . $��	10 2 10 2q�6=   6R   �� 5�      .��6   Полное имя6    �� 5�      -3   5        $ tbCustFullName��5�       . $��	10 2 10 2q�6@   6U   �� 5�      .��6   Адресс6   �� 5�      -4   5        $ tbCustAddress��5�       . $��	10 2 10 2q�6?   6T   �� 5�      .��6   Телефон6   �� 5�      -5   5        $ tbCustPhone��5�       . $��	10 2 10 2q�6=   6R   �� 5�      .��6   #!Электронный адрес6'   �� 5�      -6   5        $ tbCustEmail��5�       . $��	10 2 10 2q�6=   6R   �� 5�      -7   5         ��Name$ labelCustType��5�      6-   �� 5�      .��6   Баланс6   �� 5�      -8   5        $ tbCustBalance��5�       . $��	10 2 10 2q�6?   .��6R   6U   5�      5�      5�      