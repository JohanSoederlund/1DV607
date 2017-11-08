# 1DV607
portfolio - 
@author Johan Söderlund, lnu username: js223zs


## Run instructions WS2 implementation
Download executable file Yahtzee.exe from folder EXECUTABLE to a folder of your choice on your computer
Start application by dubble click the file in exploerer or command "run Yahtzee.exe" from a command window.
When game is finished or if ended during an unfinished game, the score will be saved in a subfolder named Database under 
the folder you placed the Yahtzee.exe program.

## DEBUG-mode

To be able to run the application in visual studio, you have to edit line 9 in class DataBase from:
    private static string pathToDB = $"{Environment.CurrentDirectory}";
    to
    private static string pathToDB = $"{Environment.CurrentDirectory.Substring(0, (Environment.CurrentDirectory.Length - 9))}DataBase\\";
        