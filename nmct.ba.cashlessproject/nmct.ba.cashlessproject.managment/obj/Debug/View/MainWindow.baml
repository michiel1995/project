   M S B A M L   `   `   `  ����\  Xnmct.ba.cashlessproject.managment, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null_4clr-namespace:nmct.ba.cashlessproject.managment.View&nmct.ba.cashlessproject.managment.View  5   	   i9clr-namespace:nmct.ba.cashlessproject.managment.ViewModel+nmct.ba.cashlessproject.managment.ViewModel  5   	   k:clr-namespace:nmct.ba.cashlessproject.managment.Converters,nmct.ba.cashlessproject.managment.Converters  5   	   2    ,nmct.ba.cashlessproject.managment.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Bv4clr-namespace:nmct.ba.cashlessproject.managment.View     5   	   Hvm9clr-namespace:nmct.ba.cashlessproject.managment.ViewModel     5   	   Ico:clr-namespace:nmct.ba.cashlessproject.managment.Converters     5   	     � Title$  
Management��5   	   $	��700��6   $
��1250��6)    � DataContext 5	      ?  9nmct.ba.cashlessproject.managment.ViewModel.ApplicationVM  5
   
    � 	Resources 5      ?  9nmct.ba.cashlessproject.managment.Converters.NavConverter   nav �� DataType?  9nmct.ba.cashlessproject.managment.ViewModel.BeheerKassaVM4   .nmct.ba.cashlessproject.managment.View.PageOneA  ;nmct.ba.cashlessproject.managment.ViewModel.BeheerKlantenVM:   4nmct.ba.cashlessproject.managment.View.BeheerKlantenE  ?nmct.ba.cashlessproject.managment.ViewModel.BeheerMedewerkersVM>   8nmct.ba.cashlessproject.managment.View.BeheerMedewerkersC	  =nmct.ba.cashlessproject.managment.ViewModel.BeheerProductenVM<
   6nmct.ba.cashlessproject.managment.View.BeheerProducten9  3nmct.ba.cashlessproject.managment.ViewModel.LoginVM2   ,nmct.ba.cashlessproject.managment.View.Login@  :nmct.ba.cashlessproject.managment.ViewModel.statistiekenVM9   3nmct.ba.cashlessproject.managment.View.statistieken>  8nmct.ba.cashlessproject.managment.ViewModel.ChangePassVM7   1nmct.ba.cashlessproject.managment.View.ChangePass��%*  &	        (��      5      *, +)(�� E     5      *, +)(�� w     5      *, +)(�� �     5      *,	 +)(�� �     5      *, +)(��     5      *, +)(�� ?    5       *, +)  5   
   6   �� 5   
   "  6   .R�5        5      �� 5   
   "  6   .R�5        5      �� 5   
   "  6   .R�5        5      �� 5   
   " 	 6   .R�5      
  5      �� 5   
   "  6   .R�5        5      �� 5   
   "  6   .R�5        5      �� 5    
   "  6   .R�5!        5"      5#      .��5$      �  � ColumnDefinitions 5*   
   �� 5+      $	��200 �6   �� 5,      $��* �6   .+�5.   
   � ���#eeey�6!   �� *Login+ �� 	Converter# [   � RowDefinitions 5/      �� 50      $��* �6    �� 51      $��50 �6    �� 52      $��50 �6    .+�56   
   ��   ItemsPanelTemplate#��[ 65   ��6   �� *Pages+��57      �� 58      .R�59      ��   Menu#��[ 6!   ��6?   �� *Name+��6X   �� *DataContext.ChangePageCommand+ �� RelativeSource ��  �� AncestorType" ���6�   �� 5:      5<      �� 5=      $��1��6   ��6#   �� *
Afmelden+.��6@   
Afmelden6J   �� 5>      $��2��6     Link#��[ 6"   ��6@   �� *
Changepass+.��6_   verander paswoord6r   5?      �� 5@   
   $��1��6   ��6)   �� *CurrentPage+5A      5B      