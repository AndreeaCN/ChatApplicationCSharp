   M S B A M L   `   `   `  ���� E  AClientChat, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:ClientChat
ClientChat      ClientChat.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     *localclr-namespace:ClientChat       � Title$  
MainWindow��$	��450��$	��400��.���  � RowDefinitions �� $	��54* ��� $	��64* ��� $
��179* ��� $	��68* ��� $	��55* � � ColumnDefinitions �� $
��157* ��� $
��236* �.+��� $��Enter your nickname��$��2��$��5���� -    ��Name$ nickBox��$��
31,10,26,9q� �� TextWrapping$
 Wrap=�$��1��$��nickname���� -    ��Name$ 
connectBtn��$��Connect to 127.0.0.1��$��26��$��10,10,204,0q�$	��Top=�$��1��$��2���� -    ��Name$ chatView��$��2��$��	10,0,26,0q�$��2��$
��Wrap=�$��Chat Window:��$	��179��$��Bottom=����	#FFF1F7F7�� -   $ 
messageBox��$��2��$
��Left=�$��47��$��	10,10,0,0q�$��3��$
 Wrap=�$��Enter Your Message��$	��Top=�$	��356���� -   $ 
sendMsgBtn��$��Send Message��$��1��$
��Left=�$��34��$��	31,10,0,0q�$��4��$	��Top=�$	��178���� -   $ leaveBtn��$��
Leave Chat��$��1��$
��Left=�$��26��$��
112,10,0,0q�$��1��$	��Top=�$��97�� �� IsCancel . 