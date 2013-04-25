using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HackAndSlash.Actor;
using libtcod;
///
/// Screen Class Definition
///
namespace HackAndSlash
{
    public enum KeyMode
    {
        None = -1, Exploration = 0, Inventory = 1, Drop = 2, Use = 3, ItemDetails = 4, Wear = 5,
        Takeoff = 6, Activate = 7
    };

    // Define the Class
    public class Window
    {
        // Store the console parameters
        public TCODConsole rootConsole;
        public int consoleWidth;
        public int consoleHeight;

        // Define menu parameters
        int topOfMenu = 8;
        double colourInterpolate = 0.5;
        double colourInterpolateStep = 0.01;
        public enum MenuOption { Start = 0, Continue = 1, Quit = 2 }
        public enum ClassOption { Rogue = 0, Fighter = 1, Wizard = 2 }
        public string[] Menu = new string[3];
        public string[] Intro = new string[23];
        public string[] ClassChoice = new string[3];
        public string classChoice;

        public MenuOption currentMenuOption = MenuOption.Start;
        public ClassOption currentClassOption = ClassOption.Rogue;

        public ClassOption CurrentClassOption
        {
            get { return currentClassOption; }
            set { currentClassOption = value; }
        }

        public string ChosenClass
        {
            get { return classChoice; }
            set { classChoice = value; }
        }

        // Constructor
        public Window(string fontBitmap, int numberCharsHorz, int numberCharsVert, string windowTitle)
        {
            // Initialise the custom default font
            int fontNumberCharsHorz = 16;
            int fontNumberCharsVert = 16;
            TCODFontFlags flags = TCODFontFlags.LayoutAsciiInRow;
            TCODConsole.setCustomFont(fontBitmap, (int)flags, fontNumberCharsHorz, fontNumberCharsVert);

            // Set up the console dimensions
            TCODConsole.initRoot(numberCharsHorz, numberCharsVert, windowTitle, false, TCODRendererType.SDL);
            consoleWidth = numberCharsHorz;
            consoleHeight = numberCharsVert;

            // Get the console handle
            rootConsole = TCODConsole.root;

            // Set the framerate;
            TCODSystem.setFps(30);

            // Initialise the Menu
            Menu[0] = "[S]tart a new Adventure ";
            Menu[1] = "[C]ontinue an existing Adventure";
            Menu[2] = "[Q]uit";

            //Initialize Class Choice menu
            ClassChoice[0] = "[R]ogue";
            ClassChoice[1] = "[F]ighter";
            ClassChoice[2] = "[W]izard";

            // Initialise the Intro
            Intro[0] = "A time was when we could live above ground,";
            Intro[1] = "without the threat of violence and savagery.";
            Intro[2] = "Mankind lived in peace with no worries.";
            Intro[3] = String.Empty;
            Intro[4] = "But that time exists no more.  There is an evil about.";
            Intro[5] = "Early on, the church cursed mankind for their sins.";
            Intro[6] = "We must atone for our sins, they said, it was our only";
            Intro[7] = "  chance at redemption.";
            Intro[8] = String.Empty;
            Intro[9] = "Warlocks and sorcerers, they sought other means of surviving.";
            Intro[10] = "Scrolls and spells were sought after or mechanical miracles";
            Intro[11] = "  from the minds of men.";
            Intro[12] = "That would stem the tide of evil from the depths.";
            Intro[13] = "That would bring back peace to the Earth.";
            Intro[14] = String.Empty;
            Intro[15] = "That peace is only a memory now and remains a distant hope";
            Intro[16] = "  as men continue to fall.";
            Intro[17] = "Evil continues to force man into the dark depths where";
            Intro[18] = "monsters hide around every corner.";
            Intro[19] = "And the shadows hide an evil unknown.";
            Intro[20] = String.Empty;
            Intro[21] = "Welcome to the end of our time.";
            Intro[22] = "Only the strong will survive...";
        }

        // Set up the title screen
        public void DisplayTitleScreen()
        {
            // Setup the logo image
            TCODImage titleLogoImage = new TCODImage("titleImage.bmp");

            // Blit the image to the background
            titleLogoImage.blit2x(rootConsole, 0, -1);

            // Set up the title text#
            int yloc = 25;
            DisplayText("created by Andrew Bell", -1, yloc++, TCODColor.white, TCODColor.black, -1);
            DisplayText("Using Libtcod 1.5", -1, yloc++, TCODColor.grey, TCODColor.black, -1);
            yloc++;
            DisplayText("Press any key to continue...", -1, yloc, TCODColor.white, TCODColor.black, -1);
        }

        // Set up and display the main menu
        public void DisplayMainMenu()
        {
            // Setup the logo image
            TCODImage titleLogoImage = new TCODImage("titleImage.bmp");

            // Blit the image to the background
            titleLogoImage.blit2x(rootConsole, 0, -1);

            topOfMenu = 18;
            PrintMainMenu();
            topOfMenu = 25;
        }

        // Display the main menu text
        public void PrintMainMenu()
        {
            DisplayText(Menu[0], 22, 25, TCODColor.white, TCODColor.black, 0);
            DisplayText(Menu[1], 22, 26, TCODColor.white, TCODColor.black, 1);
            DisplayText(Menu[2], 22, 27, TCODColor.white, TCODColor.black, 2);

            rootConsole.setForegroundColor(TCODColor.celadon);
            //rootConsole.printFrame(22, 24, 38, 5, false);

            topOfMenu = topOfMenu + 6;
            DisplayText("[UP]/[DOWN] to choose an option, [SCQ, ENTER] to select", -1, 30, TCODColor.grey, TCODColor.black, -1);
        }

        public void DisplayClassChoice()
        {
            // Setup the logo image
            TCODImage titleLogoImage = new TCODImage("titleImage.bmp");

            // Blit the image to the background
            titleLogoImage.blit2x(rootConsole, 0, -1);

            topOfMenu = 18;
            PrintClassChoice();
            topOfMenu = 25;
        }

        public void PrintClassChoice()
        {
            DisplayText(ClassChoice[0], 22, 25, TCODColor.white, TCODColor.black, 0);
            DisplayText(ClassChoice[1], 22, 26, TCODColor.white, TCODColor.black, 1);
            DisplayText(ClassChoice[2], 22, 27, TCODColor.white, TCODColor.black, 2);

            rootConsole.setForegroundColor(TCODColor.celadon);
            //rootConsole.printFrame(22, 24, 38, 5, false);

            topOfMenu = topOfMenu + 6;
            DisplayText("[UP]/[DOWN] to choose an option, [RFW, ENTER] to select", -1, 30, TCODColor.grey, TCODColor.black, -1);
        }

        // Display the intro
        public void DisplayIntro()
        {
            int XLoc = 1;

            foreach (string s in Intro)
            {
                DisplayText(s, 1, ++XLoc, TCODColor.white, TCODColor.black, -1);
            }

            DisplayText("Press any key to start...", -1, 30, TCODColor.grey, TCODColor.black, -1);
        }

        // Death
        public void DisplayFailure()
        {
            //

        }

        // Handle the main menu keypresses
        public char HandleMainMenu()
        {
            // Wait for a keypress
            TCODKey key = new TCODKey();

            do
            {
                // Get the keypress
                TCODConsole.flush();
                key = TCODConsole.checkForKeypress((int)TCODKeyStatus.KeyPressed);

                if (key.Character == 'S' || key.Character == 's' ||
                    key.Character == 'C' || key.Character == 'c' ||
                    key.Character == 'Q' || key.Character == 'q')
                {
                    string characterPressed = key.Character.ToString().ToUpper();
                    return characterPressed[0];
                }
                else if (key.KeyCode == TCODKeyCode.Up || key.KeyCode == TCODKeyCode.KeypadEight)
                {
                    if (currentMenuOption != MenuOption.Start)
                    {
                        currentMenuOption--;
                        return ' ';
                    }
                    else
                    {
                        currentMenuOption = MenuOption.Quit;
                        return ' ';
                    }
                }
                else if (key.KeyCode == TCODKeyCode.Down || key.KeyCode == TCODKeyCode.KeypadTwo)
                {
                    if (currentMenuOption != MenuOption.Quit)
                    {
                        currentMenuOption++;
                        return ' ';
                    }
                    else
                    {
                        currentMenuOption = MenuOption.Start;
                        return ' ';
                    }
                }
                else if (key.KeyCode == TCODKeyCode.KeypadEnter || key.KeyCode == TCODKeyCode.Enter)
                {
                    string KeyCodes = "SCQ";
                    return KeyCodes[(int)currentMenuOption];
                }

                PrintMainMenu();
            }
            while (!TCODConsole.isWindowClosed());
            return ' ';
        }

        //Handle Class Menu
        public char HandleClassMenu()
        {
            // Wait for a keypress
            TCODKey key = new TCODKey();

            do
            {
                // Get the keypress
                TCODConsole.flush();
                key = TCODConsole.checkForKeypress((int)TCODKeyStatus.KeyPressed);

                if (key.Character == 'R' || key.Character == 'r' ||
                    key.Character == 'F' || key.Character == 'f' ||
                    key.Character == 'W' || key.Character == 'w')
                {
                    classChoice = key.Character.ToString().ToUpper();
                    return classChoice[0];
                }
                else if (key.KeyCode == TCODKeyCode.Up || key.KeyCode == TCODKeyCode.KeypadEight)
                {
                    if (currentClassOption != ClassOption.Rogue)
                    {
                        currentClassOption--;
                        return ' ';
                    }
                    else
                    {
                        currentClassOption = ClassOption.Wizard;
                        return ' ';
                    }
                }
                else if (key.KeyCode == TCODKeyCode.Down || key.KeyCode == TCODKeyCode.KeypadTwo)
                {
                    if (currentClassOption != ClassOption.Wizard)
                    {
                        currentClassOption++;
                        return ' ';
                    }
                    else
                    {
                        currentClassOption = ClassOption.Rogue;
                        return ' ';
                    }
                }
                else if (key.KeyCode == TCODKeyCode.KeypadEnter || key.KeyCode == TCODKeyCode.Enter)
                {
                    string KeyCodes = "RFW";
                    return KeyCodes[(int)currentClassOption];
                }

                PrintClassChoice();
            }
            while (!TCODConsole.isWindowClosed());
            return ' ';
        }

        // Handle standard keypresses
        public char WaitForKeyPress(KeyMode keymode)
        {
            // Wait for a keypress
            TCODKey key = new TCODKey();

            // This needs to be rewritten!
            if (keymode == KeyMode.Exploration)
            {
                do
                {
                    // Get the keypress
                    TCODConsole.flush();
                    key = TCODConsole.checkForKeypress((int)TCODKeyStatus.KeyPressed);

                    if (key.Character == 'H' || key.Character == 'h' ||
                        key.Character == 'J' || key.Character == 'j' ||
                        key.Character == 'K' || key.Character == 'k' ||
                        key.Character == 'L' || key.Character == 'l' ||
                        key.Character == 'Y' || key.Character == 'y' ||
                        key.Character == 'U' || key.Character == 'u' ||
                        key.Character == 'B' || key.Character == 'b' ||
                        key.Character == 'N' || key.Character == 'n' ||
                        key.Character == 'S' || key.Character == 's' ||
                        key.Character == 'G' || key.Character == 'g' ||
                        key.Character == 'I' || key.Character == 'i' ||
                        key.Character == 'D' || key.Character == 'd' ||
                        key.Character == 'W' || key.Character == 'w' ||
                        key.Character == 'T' || key.Character == 't' ||
                        key.Character == 'X' || key.Character == 'x' ||
                        key.Character == 'V' || key.Character == 'v' ||
                        key.Character == 'Q' || key.Character == 'q' ||
                        key.Character == '>' ||
                        key.Character == '?' ||
                        key.Character == '.' ||
                        key.KeyCode == TCODKeyCode.Up ||
                        key.KeyCode == TCODKeyCode.Down ||
                        key.KeyCode == TCODKeyCode.Left ||
                        key.KeyCode == TCODKeyCode.Right ||
                        key.KeyCode == TCODKeyCode.Escape ||
                        key.KeyCode == TCODKeyCode.KeypadOne ||
                        key.KeyCode == TCODKeyCode.KeypadTwo ||
                        key.KeyCode == TCODKeyCode.KeypadThree ||
                        key.KeyCode == TCODKeyCode.KeypadFour ||
                        key.KeyCode == TCODKeyCode.KeypadSix ||
                        key.KeyCode == TCODKeyCode.KeypadSeven ||
                        key.KeyCode == TCODKeyCode.KeypadEight ||
                        key.KeyCode == TCODKeyCode.KeypadNine
                        )
                    {
                        if (key.KeyCode == TCODKeyCode.Up || key.KeyCode == TCODKeyCode.KeypadEight) { key.Character = 'K'; }
                        else if (key.KeyCode == TCODKeyCode.Down || key.KeyCode == TCODKeyCode.KeypadTwo) { key.Character = 'J'; }
                        else if (key.KeyCode == TCODKeyCode.Left || key.KeyCode == TCODKeyCode.KeypadFour) { key.Character = 'H'; }
                        else if (key.KeyCode == TCODKeyCode.Right || key.KeyCode == TCODKeyCode.KeypadSix) { key.Character = 'L'; }
                        else if (key.KeyCode == TCODKeyCode.KeypadSeven) { key.Character = 'Y'; }
                        else if (key.KeyCode == TCODKeyCode.KeypadNine) { key.Character = 'U'; }
                        else if (key.KeyCode == TCODKeyCode.KeypadOne) { key.Character = 'B'; }
                        else if (key.KeyCode == TCODKeyCode.KeypadThree) { key.Character = 'N'; }

                        string characterPressed = key.Character.ToString().ToUpper();
                        return characterPressed[0];
                    }
                }
                while (!TCODConsole.isWindowClosed());
                return ' ';
            }
            else if (keymode == KeyMode.Inventory)
            {
                do
                {
                    // Get the keypress
                    TCODConsole.flush();
                    key = TCODConsole.checkForKeypress((int)TCODKeyStatus.KeyPressed);

                    if (key.KeyCode == TCODKeyCode.Escape)
                    {
                        string characterPressed = key.Character.ToString().ToUpper();
                        return '#';
                    }
                    else
                    {
                        char keyPressed = Convert.ToChar(key.Character.ToString());
                        if (char.IsLetter(keyPressed))
                        {
                            string characterPressed = key.Character.ToString().ToUpper();
                            return characterPressed[0];
                        }
                    }
                }
                while (!TCODConsole.isWindowClosed());
                return ' ';
            }
            else if (keymode == KeyMode.Drop || keymode == KeyMode.Wear || keymode == KeyMode.Takeoff || keymode == KeyMode.Activate)
            {
                do
                {
                    // Get the keypress
                    TCODConsole.flush();
                    key = TCODConsole.checkForKeypress((int)TCODKeyStatus.KeyPressed);

                    if (key.KeyCode == TCODKeyCode.Escape)
                    {
                        string characterPressed = key.Character.ToString().ToUpper();
                        return '#';
                    }
                    else
                    {
                        char keyPressed = Convert.ToChar(key.Character.ToString());
                        if (char.IsLetter(keyPressed))
                        {
                            string characterPressed = key.Character.ToString().ToUpper();
                            return characterPressed[0];
                        }
                    }
                }
                while (!TCODConsole.isWindowClosed());
                return ' ';
            }
            else if (keymode == KeyMode.ItemDetails)
            {
                do
                {
                    // Get the keypress
                    TCODConsole.flush();
                    key = TCODConsole.checkForKeypress((int)TCODKeyStatus.KeyPressed);

                    if (key.KeyCode == TCODKeyCode.Escape)
                    {
                        return ' ';
                    }
                }
                while (!TCODConsole.isWindowClosed());
                return ' ';
            }
            else
            {
                return ' ';
            }
        }

        // Display a line of text
        public void DisplayText(string textToDisplay, int x, int y, TCODColor foregroundColor, TCODColor backgroundColor, int Index)
        {
            // Handle mainmenu colour-swapping
            if (Index == (int)currentMenuOption)
            {
                foregroundColor = TCODColor.black;
                colourInterpolate = colourInterpolate + colourInterpolateStep;
                if (colourInterpolate >= 0.91) { colourInterpolateStep = -0.01; }
                else if (colourInterpolate <= 0.11) { colourInterpolateStep = 0.01; }
                backgroundColor = TCODColor.Interpolate(TCODColor.yellow, TCODColor.red, (float)colourInterpolate);
            }

            rootConsole.setBackgroundColor(backgroundColor);
            rootConsole.setForegroundColor(foregroundColor);

            if (Index != -1)
            {
                System.Text.StringBuilder OffSetString = new System.Text.StringBuilder();
                OffSetString.Append(' ', (36 - textToDisplay.Length) / 2);
                textToDisplay = OffSetString + textToDisplay + OffSetString;
            }
            else { if (x == -1) { x = (consoleWidth - textToDisplay.Length) / 2; } }

            int offset = 0;
            foreach (char value in textToDisplay)
            {
                if (value == '[') { rootConsole.setForegroundColor(foregroundColor); }
                else if (value == ']') { rootConsole.setForegroundColor(foregroundColor); }
                else if (offset == 1 && textToDisplay[0] == '[') { rootConsole.setForegroundColor(foregroundColor); }
                else { rootConsole.setForegroundColor(foregroundColor); }
                offset++;
                rootConsole.setCharBackground(x + offset, y, backgroundColor, TCODBackgroundFlag.Set);
                rootConsole.print(x + offset, y, value.ToString());
            }
        }

        public void ClearScreen()
        {
            rootConsole.clear();
            rootConsole.setBackgroundColor(TCODColor.black);
        }

        public void WaitForAnyKeyPress()
        {
            // Wait for a keypress
            TCODConsole.flush();
            TCODConsole.waitForKeypress(true);
        }
    }
}
