namespace DalTest;

using System.Diagnostics;
using Dal;
using DalApi;
using DO;

internal class Program
{
    private static IDependency? s_dalDependency = new DependencyImplementation(); //stage 1
    private static IUser? s_dalUser = new UserImplementation(); //stage 1
    private static ITask? s_dalTask = new TaskImplementation(); //stage 1
    static int funcentity()
    {
        Console.WriteLine("\nCheck entity\n");
        Console.WriteLine("Choose one of the following:");
        Console.WriteLine("0: exit");
        Console.WriteLine("1: Dependency");
        Console.WriteLine("2: Task");
        Console.WriteLine("3: User");
        int entity = int.Parse(Console.ReadLine()!);
        return entity;
    }
    static int functask(string s)
    {
        Console.WriteLine("\nchoose operation\n");
        Console.WriteLine("Choose one of the following:");
        Console.WriteLine("0: exit");
        Console.WriteLine("1: Create new ", s);
        Console.WriteLine("2: Read ", s);
        Console.WriteLine("3: ReadAll ", s);
        Console.WriteLine("4: Update ", s);
        Console.WriteLine("5: Delete ", s);
        int entity = int.Parse(Console.ReadLine()!);
        return entity;
    }
    public static string GetString(string s)
    {
        Console.WriteLine(s);
        return Console.ReadLine();
    }
    private static void User()
    {

        int numtask = functask("User");
        try
        {
            switch (numtask)
            {
                case 0: return;
                case 1:
                    Console.WriteLine("\nPlease Enter Details\n");
                    int Id = int.Parse(GetString("4 Last Digit Of Id: "));
                    string Email = GetString("Email: ");
                    string PhoneNumber = GetString("PhoneNumber: ");
                    string Name = GetString("Name: ");
                    DO.UserLevel Level = (DO.UserLevel)Int16.Parse(GetString("0: supportes\r\n  1: closeFriends\r\n  2: bride\r\n  3: groom\r\n  4: producer "));
                    User newUser = new(Id, Email, PhoneNumber.ToString(), Name, Level);

                    Console.WriteLine(s_dalUser!.Create(newUser));
                    break;
                case 2:
                    Id = int.Parse(GetString("Id: "));
                    User? temPrint = s_dalUser!.Read(Id);
                    if (temPrint != null) {
                        Console.WriteLine(temPrint);
                    }
                    break;
                case 3:
                    List<User> temPrintAll = s_dalUser!.ReadAll();
                    foreach (User p in temPrintAll)
                    {
                        Console.WriteLine(p);
                    }
                    break;
                case 4:
                    Id = int.Parse(GetString("Id: "));

            }
        

        



           

        }
    }

    static void Main(string[] args)
    {
        try
        {
            int entity = funcentity();
        }
        catch ( Exception e)
        {
            Console.WriteLine(e.Message);
        }

        User? temp = s_dalUser!.Read(entity);
        entity=functask();

        Initialization.Do(s_dalDependency,s_dalUser, s_dalTask);
        
      

    }
}