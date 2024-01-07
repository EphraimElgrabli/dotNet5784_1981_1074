namespace DalApi;
using DO;

public interface IUser
{
    int Create(User item); //Creates new entity object in DAL
    User? Read(int id); //Reads entity object by its ID 
    List<User> ReadAll(); //stage 1 only, Reads all entity objects
    void Update(User item); //Updates entity object
    void Delete(int id); //Deletes an object by its Id
}
