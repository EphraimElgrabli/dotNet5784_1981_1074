using Dal;
using BlApi;
using Microsoft.VisualBasic.FileIO;
using BlImplementation;
using BO;
using DO;

namespace BlTest;

internal class Program
{
    static readonly BlApi.IBl? s_bl = BlApi.Factory.Get();

    static void Main(string[] args)
    {
        Console.Write("Would you like to create Initial data? (Y/N)");
        string? ans = Console.ReadLine() ?? throw new FormatException("Wrong input");
        if (ans == "Y")
            DalTest.Initialization.Do();

        //  Functions To handle The printing of stuff
        
        //  Print the main menu
        int MainMenu()
        {
            Console.WriteLine("To exit, press 0");
            Console.WriteLine("To the Engineer menu, press 1");
            Console.WriteLine("To the Task menu, press 2");
            Console.WriteLine("To update the dates, press 3");
            int choice = int.Parse(Console.ReadLine()!);
            return choice;
        }

        //  Print the main menu that will appear after dates updating
        int UpdatedMainMenu()
        {
            Console.WriteLine("To exit, press 0");
            Console.WriteLine("To the updated Engineer menu, press 1");
            Console.WriteLine("To the updated Task menu, press 2");
            int choice = int.Parse(Console.ReadLine()!);
            return choice;
        }

        //  Print the menu of the Engineer
        int EngineerMenu()
        {
            Console.WriteLine("To exit this current menu , press 1");
            Console.WriteLine("To delete an Engineer, press 2");
            Console.WriteLine("To create an Engineer, press 3");
            Console.WriteLine("To update an Engineer, press 4");
            Console.WriteLine("To print all the Engineers, press 5");
            Console.WriteLine("To search for an Engineer, press 6");
            int choice = int.Parse(Console.ReadLine()!);
            return choice;
        }

        //  Print the menu of the Engineer
        int TaskMenu()
        {
            Console.WriteLine("To exit this current menu, press 1");
            Console.WriteLine("To delete an Task, press 2");
            Console.WriteLine("To create an Task, press 3");
            Console.WriteLine("To update an Task, press 4");
            Console.WriteLine("To print all the Tasks, press 5");
            Console.WriteLine("To search for an Task, press 6");
            int choice = int.Parse(Console.ReadLine()!);
            return choice;
        }
    }
}