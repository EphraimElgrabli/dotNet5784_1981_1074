namespace DalTest;

using System.Data.Common;
using DalApi;
using DO;
public static class Initialization
{
    private static IDependency? s_dalDependency;//stage 1
    private static IUser? s_dalUser;//stage 1
    private static ITask? s_dalTask;//stage 1
    private static readonly Random s_rand=new();
    private static void createTask()
    {
        string[] arr = new string[] {
           
        }; ///we need choose a name for the Task  i
        s_dalTask.Create()
    }

    private static void createUser()
    {
        
        User Motherofgroom= new User();
        User Motherofbride= new User();
        User Fatherofgroom = new User();
        User Fatherofbride = new User();
        User Groom= new User();
        User Bride= new User();
    }
    private static void createDependency() { 
    
    }
}
