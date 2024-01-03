namespace DalApi;
using DO;

public interface IEngineer
{
    int Create(Engineer item); //Creates new entity object in DAL
    Engineer? Read(int id); //Reads entity object by its ID 
    List<Engineer> ReadAll(); //stage 1 only, Reads all entity objects
    void Update(Engineer item); //Updates entity object
    void Delete(int id); //Deletes an object by its Id
}
