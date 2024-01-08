namespace DalTest;
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
        Console.WriteLine("0: Task");
        Console.WriteLine("0: User");
        int entity = int.Parse(Console.ReadLine()!);
        return entity;
    }
    static int functask(string s)
    {
        Console.WriteLine("\nchoose operation\n");
        Console.WriteLine("Choose one of the following:");
        Console.WriteLine("0: exit");
        Console.WriteLine("1: Create new " ,s);
        Console.WriteLine("2: Read ",s);
        Console.WriteLine("3: ReadAll ",s);
        Console.WriteLine("4: Update ",s);
        Console.WriteLine("5: Delete ",s);
        int entity = int.Parse(Console.ReadLine()!);
        return entity;
    }

    private static void User()
    {
        int numtask = functask("User");

    }

    static void Main(string[] args)
    {
        int entity = funcentity();
        User? temp = s_dalUser!.Read(entity);
        entity=functask();

        Initialization.Do(s_dalDependency,s_dalUser, s_dalTask);
        
      

    }
}