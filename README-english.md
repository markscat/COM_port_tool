#V4.11
4/11
1. An error occurred due to unknown reasons, causing the program to fail to receive messages.
2. Under Program.cs, add a global exception handler
3. Added private void WriteDebugLog(string fileName, string message) method; records error data to the ../debuglog directory

Untested items
1. Hardware flow control is only confirmed to work with None and Xon/Xoff. RTS/CTS and DTR/DSR have not been tested. I am too lazy to test them because they are not used.

Note:
-Because the hardware has been touched, a virus scan must be performed the first time it is executed.
- The source code is open source. Unless Microsoft has a backdoor, theoretically this program will not have any malicious vulnerabilities.
-A loophole is a bug

To-Do List
- HEX output

#2nd version released 2025/03/27
* Git is so smart. After I upload it, it can help me update the previous version of the program to the second version! ! !
Software engineers may think it’s nothing, but I’m a hardware engineer~~ and this is my first time using git!
* Basically, the first version of the program can meet most of the functions.
*I use this program to read the debugging information of the MCU by default. If you want to record the time, a log file must exist.
The big flow control, transmit bits, parity checks, etc., that doesn't matter to me.
* I originally wanted to add an external TextBox, but how to do it without throwing away all the data in the TextBox? After a week of working on it, I got angry and directly added a TextBox for input
#To-do List
1. Put all the frames into the tabs
2. The first tab is like this
3. The second tab is to analyze the received message. If there is an adc, it will be displayed. If it is related to time, it will display the time.
4. Combine with STM32f407 to make a universal test fixture.


#COM_port_tool
Basic RS232 terminal

This thing should be very common and many people used to use it.
Basically, I want a comm software that I can easily modify myself.
This way I can modify the software according to my needs.
The biggest reason why I couldn’t write it before was that AI hadn’t come out yet. When I asked the software engineer, he just said, “Don’t ask, I’ll write it for you.”

The problem now is that the functions are very basic and other RS232 related settings cannot be set.
In addition, I hope to be able to archive the captured messages and then click a button.
Currently, you can still type in the message box displayed in the middle, but I will find a way to modify it later!
Don’t ask me if it’s cross-platform, because it’s impossible! Can't do it!
Not only can the plan not be carried out, but I can't write it! ! !

Function introduction:
The interface is so simple that it makes you want to laugh. What else do you need a manual for?
We’ll talk about adding more functions (like the settings of 8, nn, 1) later!
