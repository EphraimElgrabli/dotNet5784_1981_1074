namespace DalApi;
using DO;

public interface IConfig
{
    int Create(Config item); //Creates new entity object in DAL
    Config? Read(int id); //Reads entity object by its ID 
    List<Config> ReadAll(); //stage 1 only, Reads all entity objects
    void Update(Config item); //Updates entity object
    void Delete(int id); //Deletes an object by its Id
}
