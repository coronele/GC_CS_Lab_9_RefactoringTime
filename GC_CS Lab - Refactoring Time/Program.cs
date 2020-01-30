using System;
using System.Collections.Generic;

namespace GC_CS_Lab___Refactoring_Time
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> studentName = new List<string> { "Aaron", "Blake", "Denise", "Jacqueline", "Jessica", "Timothy" };
            List<string> studentFood = new List<string> { "broasted chicken", "steak", "mac & cheese", "shrimp alfredo", "sushi", "pizza" };
            List<string> studentHomeTown = new List<string> { "Allentown, PA", "Rochester Hills", "Troy", "Livonia", "Southfield", "Detroit" };
            List<string> studentAge = new List<string> { "30", "41", "35", "38", "23", "27" };
            List<string> studentColor = new List<string> { "blue", "green", "pink", "purple", "black", "gold" };

            string continueProg = "y";

            // This shows the program title
            ShowTitle("Get to Know Your Classmates!");

            // Program loop
            do
            {
                DisplayProgOptions();
                // Prompt for and receive program option
                int progOption = GetOptNum($"Please select program option [1-3]", "Invalid Program option. ");

                if (progOption == 1)
                {
                    DisplayMenu(studentName);
                    // Prompt for and receive student info input
                    int stuNum = GetStuNum($"Please enter a student number: [1-{studentName.Count}]", "Incorrect input! Please enter a valid student number: ", studentName.Count);
                    // Get information on that student
                    GetStuInfo(stuNum - 1, studentName, studentFood, studentHomeTown, studentAge, studentColor);
                }
                else if (progOption == 2)
                {
                    AddNewUser(studentName, studentFood, studentHomeTown, studentAge, studentColor);
                }
                else
                {
                    continueProg = "n";
                }

                // Prompt to repeat program
                if (progOption != 3)
                {
                    continueProg = TryAgain("Would you like to return to main menu? [y/n]");
                }
            }
            while (continueProg == "y");  // change
        }

        public static void GetStuInfo(int studentSelect, List<string> stuName, List<string> stuFood, List<string> stuHomeTown, List<string> stuAge, List<string> stuColor)
        {

            List<string> stuChoices = new List<string> { "Favorite food", "Hometown", "Age", "Favorite Color" };
            string stuContinue = "y";
            string studentTrait;

            // Loop to get trait for specific student
            do
            {
                studentTrait = GetUserInput($"What information would you like to know about {stuName[studentSelect]}? [Favorite food, Hometown, Age, Favorite Color]").Trim().ToLower();

                // output user selected trait or error for invalid options
                SetOutputColor();
                switch (studentTrait)
                {
                    case "favorite food":
                        Console.WriteLine($"{stuName[studentSelect]}'s favorite food is {stuFood[studentSelect]}.\n");
                        break;
                    case "hometown":
                        Console.WriteLine($"{stuName[studentSelect]} is from {stuHomeTown[studentSelect]}.\n");
                        break;
                    case "age":
                        Console.WriteLine($"{stuName[studentSelect]} is {stuAge[studentSelect]} years old.\n");
                        break;
                    case "favorite color":
                        Console.WriteLine($"{stuName[studentSelect]}'s favorite color is {stuColor[studentSelect]}\n");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please enter \"Favorite food\", \"Hometown\", \"Age\", or \"Favorite Color\".\n");
                        break;
                }
                // Prompt to repeat program
                stuContinue = TryAgain($"Would you like more info on {stuName[studentSelect]}? [y/n]");
            }
            while (stuContinue == "y");
        }

        public static void AddNewUser(List<string> stuName, List<string> stuFood, List<string> stuHomeTown, List<string> stuAge, List<string> stuColor)
        {
            SetOutputColor();
            Console.WriteLine("Enter data for the new student:");
            AddNewItemToList("Enter student's name:", "name", stuName);
            AddNewItemToList("Enter student's favorite food:", "favorite food", stuFood);
            AddNewItemToList("Enter student's hometown:", "hometown", stuHomeTown);
            AddNewItemToList("Enter student's age:", "age", stuAge);
            AddNewItemToList("Enter student's favorite color", "color", stuColor);
        }
        
        public static void AddNewItemToList(string prompt, string itemName, List<string> userList)
        {
            string userItem = GetUserInput(prompt);

            if (userItem == "")
            {
                Console.WriteLine($"You may not enter a blank item for {itemName}.");
                AddNewItemToList(prompt, itemName, userList);
            }
            else
            {
                userList.Add(userItem);
            }
        }

        public static string TryAgain(string message)
        {
            // Method for running program again.  Passes back to do while loop in main.
            string userChoice = GetUserInput(message).Trim().ToLower();
            while ((userChoice != "y") && (userChoice != "n"))
            {
                userChoice = GetUserInput("Please enter 'y' or 'n'.  [y/n]").Trim().ToLower();
            }
            return userChoice;
        }

        public static int GetOptionNum(string optionMessage,string errorMsg)
        {
            // Accepts and checks integer value.
            int number;

            SetOutputColor();
            try
            {
                number = int.Parse(GetUserInput(optionMessage));
                return number;
            }
            catch
            {
                SetOutputColor();
                Console.Write($"{errorMsg} ");
                number = GetOptionNum(optionMessage,errorMsg);
                return number;
            }
        }
        public static int GetStuNum(string optionMessage, string errorMsg, int stuCount)
        {
            // Method for checking student number
            int userInput = GetOptionNum(optionMessage, errorMsg);

            if ((userInput <= 0) || (userInput > stuCount))
            {
                SetOutputColor();
                Console.Write($"{errorMsg} ");
                userInput = GetStuNum(optionMessage, errorMsg, stuCount);
                return userInput;
            }
            else
            {
                return userInput;
            }
        }

        public static int GetOptNum(string optionMessage, string errorMsg)
        {
            // Method for checking program option number
            int userInput = GetOptionNum(optionMessage, errorMsg);

            if ((userInput <= 0) || (userInput > 3))
            {
                SetOutputColor();
                Console.Write($"{errorMsg} ");
                userInput = GetOptNum(optionMessage, errorMsg);
                return userInput;
            }
            else
            {
                return userInput;
            }
        }

        public static void DisplayProgOptions()
        {
            SetOutputColor();
            Console.WriteLine($"1.\tView a current student");
            Console.WriteLine($"2.\tAdd a new student");
            Console.WriteLine($"3.\tExit");
        }

        public static void DisplayMenu(List <string> studentList)
        {
            SetOutputColor();
            for (int i = 0; i < studentList.Count; i++)
            {
                Console.WriteLine((i + 1).ToString()+". \t"+studentList[i]);
            }
        }
        
        public static string GetUserInput(string message)
        {
            // Allows for program prompt and user input (string)
            SetOutputColor();
            Console.WriteLine(message);
            SetInputColor();
            return Console.ReadLine();
        }

        public static void ShowTitle(string title)
        {
            // This method simply shows the title
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"{title} \n\n");
        }

        public static void SetInputColor()
        {
            // Method for setting colors for user inputted text
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public static void SetOutputColor()
        {
            // Method for setting colors for default display text
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }
    }
}
