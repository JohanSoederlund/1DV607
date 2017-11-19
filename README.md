

# Yahtzee Console Game

<p> Language    C#
<p> portfolio   Course 1DV607, Objektorienterad analys och design med UML, LNU
<p> Author      Johan Söderlund, js223zs

## Changes in WS3

### New Components
1. Rules
    * IRules
    * BaseRules
    * YahtzeeRules
    * YatzyRules
    * GameType
2. Categories
    * Category
    * CategoryYahtzee
    * CategoryYatzy
3. Factory
4. IDieObserver

Naming of files in folder Database is named based on game type, i.e. Yatzy or Yahtzee, which also enables correct viewing and resuming at application start. 

#### Factory Pattern
Factory creates rules and categories based on selected user choice, regarding Yahtzee or Yatzy game type. 

#### Observer Pattern
IDieObserver distributes rolled die events from CollectionOfDie to ViewController which in turn calls for rendering methods in RoundView.



## Run instructions WS3 implementation
Download executable file Yahtzee.exe from folder Yahtzee/WS3/EXECUTABLE to a folder of your choice on your computer
Start application by dubble click the file in explorer or command "run Yahtzee.exe" from a command window.
When game is finished or if ended during an unfinished game, the score will be saved in a subfolder named Database under 
the folder you placed the Yahtzee.exe program.

## Run instructions WS2 implementation
Download executable file Yahtzee.exe from folder Yahtzee/WS2/EXECUTABLE to a folder of your choice on your computer
Start application by dubble click the file in explorer or command "run Yahtzee.exe" from a command window.
When game is finished or if ended during an unfinished game, the score will be saved in a subfolder named Database under 
the folder you placed the Yahtzee.exe program.

