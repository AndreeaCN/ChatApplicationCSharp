   M S B A M L   `   `   `  ����E  AClientChat, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:ClientChat
ClientChat  5   	       ClientChat.ChatWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:ClientChat     5   	     � Title$
  Chat��5   	   $	��542��6   $��870.5��6#   .��5	      � -   5         �Name$ labelforserverlist��5	      ���	#FFCFE8AA � RowDefinitions 5
   
   �� 5      $
��381* �6   �� 5      $	��69* �6   �� 5      $	��61* �6    � ColumnDefinitions 5   
   �� 5      .+�5   
   �� -   5         ��Name$ chatView��5      $��28,84,234,10q�6&   $
��Wrap=�6<   $��Chat Window:��6P   ���	#FFF3F5F16d   �� 5   
   -   5         ��Name$ 
messageBox��5      $��47��6&   $��
28,10,26,0q�62   $��1��6F    �� TextWrapping$
 Wrap=�6S   $��Enter Your Message��6g   $	��Top=�6�   �� 5   
   -   5         ��Name$ 
sendMsgBtn��5      $��Send Message��6%   $��	28,13,0,0q�6<   $��2��6O   ���	#FFE88430$
��Bold"�6�   $��14(�6�   $
��Left=�6�   $	��260��6�   $��34��6�   $	��Top=�6�    J� RenderTransformOrigin$ 0.5,0.5&�6�   z�5      V� 5      .~�5      �� �� 5      �� 5      	 �� Angle$	 0.113Q�6&   T� 5      5      5      �� 5   
   -   5        $ leaveBtn��5      $��
Leave Chat��6#   $��682,13,58,0q�68   $��2��6M   
 �� IsCancel
 . $ -3.907,11.75&�6�   ���	#FF8D7A6B$
��Bold"�6�   $��14(�6�   $��34��6�   $	��Top=�6�   �� 5   
   -   5        $ 
welcomeMsg��5      $��69��6(   $��28,10,234,0q�64   $
��Wrap=�6I   $	��Top=�6]   $��14(�6u   $
��Bold"�6�   �� 5    
   -   5        $ connectedUsersBox��5       $
��Left=�6/   $	��287��6J   $��
652,84,0,0q�6W   $
��Wrap=�6k   $	��Top=�6   $	��184��6�   ���	#FFEEE7BE�� 6�   ���	#FFD1A2A2$� ��6�   6�   5"      5#      