using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using libtcod;
using HackAndSlash;
using HackAndSlash.Session;
using HackAndSlash.Actor;

namespace HackAndSlash
{
    class Program
    {

        static void Main(string[] args)
        {
            const int ScreenHeight = 35;
            const int ScreenWidth = 80;

            // Set up the main window          
            //Window rootWindow = new Window("Md_curses_16x16.png", ScreenWidth, ScreenHeight);
            Window rootWindow = new Window("font_16x16.png", ScreenWidth, ScreenHeight, "Hack and Slash");
            char menuOption = 'A';

            // Display the title screen and wait for a keypress
            rootWindow.ClearScreen();
            rootWindow.DisplayTitleScreen();
            rootWindow.WaitForAnyKeyPress();

            // Then display the main menu
            rootWindow.ClearScreen();
            rootWindow.DisplayMainMenu();

            // Handle main menu keypresses
            bool canQuit = false;

            do
            {
                TCODConsole.flush();
                char keypress = rootWindow.HandleMainMenu();

                switch (keypress)
                {
                    case 'S':
                        rootWindow.ClearScreen();
                        rootWindow.DisplayIntro();
                        rootWindow.WaitForAnyKeyPress();
                        //Display class menu
                        rootWindow.ClearScreen();
                        rootWindow.DisplayClassChoice();

                            TCODConsole.flush();
                            keypress = rootWindow.HandleClassMenu();

                            switch (keypress)
                            {
                                case 'R':
                                    rootWindow.DisplayClassChoice();
                                    menuOption = 'R';
                                    rootWindow.ClearScreen();
                                    Game newgame = new Game(rootWindow, true, menuOption);
                                    newgame.Start(rootWindow);
                                    break;
                                case 'F':
                                    rootWindow.DisplayClassChoice();
                                    menuOption = 'F';
                                    rootWindow.ClearScreen();
                                    newgame = new Game(rootWindow, true, menuOption);
                                    newgame.Start(rootWindow);
                                    break;
                                case 'W':
                                    rootWindow.DisplayClassChoice();
                                    menuOption = 'W';
                                    rootWindow.ClearScreen();
                                    newgame = new Game(rootWindow, true, menuOption);
                                    newgame.Start(rootWindow);
                                    break;
                            }
                            break;
                    case 'C':
                        rootWindow.ClearScreen();
                        Game oldgame = new Game(rootWindow, false, menuOption);
                        oldgame.Start(rootWindow);
                        rootWindow.DisplayMainMenu();
                        break;
                    case 'Q':
                        rootWindow.ClearScreen();
                        canQuit = true;
                        break;
                }
            }
            while (!TCODConsole.isWindowClosed() && !canQuit);

        }
    }
}
