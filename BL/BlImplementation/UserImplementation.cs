using System.ComponentModel.DataAnnotations;
using BlApi;
using BO;

namespace BlImplementation;

internal class UserImplementation : IUser
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    public int Create(BO.User user)
    {
        try
        {
            int idUser;
            if (user.Id > 0 && user.Name != "" && !new EmailAddressAttribute().IsValid(user.Email))
            {
                throw new BO.BlValueInvalidException($"value Invalid can not create user");
            }
            else
            {
                idUser = _dal.User.Create(new DO.User()
                {
                    Id = user.Id,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Name = user.Name,
                    Level = (DO.UserLevel)user.Level
                });
            }
            return idUser;


        }
        catch (DO.DalAlreadyExistException ex)
        {
            throw new BO.BlAlreadyExistException(ex.Message, ex);
        }
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public User? Read(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<UserInTask> ReadAll()
    {
        throw new NotImplementedException();
    }

    public void Update(User user)
    {
        throw new NotImplementedException();
    }
}
