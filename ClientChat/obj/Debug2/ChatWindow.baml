   M S B A M L   `   `   `  ���� E  AClientChat, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:ClientChat
ClientChat      ClientChat.ChatWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     *localclr-namespace:ClientChat       � Title$
  Chat��$	��542��$��870.5��.��� -    �Name$ labelforserverlist�����	#FFCFE8AA � RowDefinitions �� $
��381* ��� $	��69* ��� $	��61* � � ColumnDefinitions �� .+��� -    ��Name$ chatView��$��28,84,234,10q�$
��Wrap=�$��Chat Window:�����	#FFF3F5F1�� -    ��Name$ 
messageBox��$��47��$��
28,10,26,0q�$��1�� �� TextWrapping$
 Wrap=�$��Enter Your Message��$	��Top=��� -    ��Name$ 
sendMsgBtn��$��Send Message��$��	28,13,0,0q�$��2�����	#FFE88430$
��Bold"�$��14(�$
��Left=�$	��260��$��34��$	��Top=� J� RenderTransformOrigin$ 0.5,0.5&�z�V� .~��� �� �� 	 �� Angle$	 0.113Q�T� �� -   $ leaveBtn��$��
Leave Chat��$��682,13,58,0q�$��2��
 �� IsCancel
 . $ -3.907,11.75&����	#FF8D7A6B$
��Bold"�$��14(�$��34��$	��Top=��� -   $ 
welcomeMsg��$��69��$��28,10,234,0q�$
��Wrap=�$	��Top=�$��14(�$
��Bold"��� -   $ connectedUsersBox��$
��Left=�$	��287��$��
652,84,0,0q�$
��Wrap=�$	��Top=�$	��184�����	#FFEEE7BE�� ���	#FFD1A2A2$� ��