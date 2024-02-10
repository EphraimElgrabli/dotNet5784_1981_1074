namespace BlImplementation;
using System.ComponentModel.DataAnnotations;



internal class UserImplementation : BlApi.IUser
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    public int Create(BO.User user)
    {
        try
        {
            int idUser;
            if (user.Id < 99999999&&user.Id>999999999 && user.Name != "" && !new EmailAddressAttribute().IsValid(user.Email))
            {
                throw new BO.BlValueInvalidException($"value Invalid can not create user");
            }

            idUser = _dal.User.Create(new DO.User()
            {
                Id = user.Id,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Name = user.Name,
                Level = (DO.UserLevel)user.Level
            });


            return idUser;


        }
        catch (DO.DalAlreadyExistException ex)
        {
            throw new BO.BlAlreadyExistException(ex.Message, ex);
        }
    }

    public void Delete(int id)
    {
        try
        {
            if (_dal.User.Read(id) == null)
            {

            }
            if (Read(id).Tasks == null)
            {
                _dal.User.Delete(id);
            }
            else
                throw new BO.CanNotDeletedException($"User with ID={id} can not deleted");
        }catch(DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException(ex.Message, ex);
        }
    }   

    public BO.User? Read(int id)
    {
       DO.User? douser= _dal.User.Read(id);
        if(douser==null)
        {
            throw new BO.BlDoesNotExistException($"User with ID={id} does Not exist");
        }
        return new BO.User()
        {
            Id = douser.Id,
            Email = douser.Email,
            PhoneNumber = douser.PhoneNumber,
            Name = douser.Name,
            Level = (BO.UserLevel)douser.Level
           
        };
    }

    public IEnumerable<BO.UserInTask> ReadAll(Func<BO.UserInTask, bool>? filter = null)
    {
        if(filter!=null)
        {
            return from user in _dal.User.ReadAll()
                   select new BO.UserInTask
                   {
                       Id = user.Id,
                       Name = user.Name,

                   };
        }
        else
        {
            return from user in _dal.User.ReadAll()
                   let doi= new BO.UserInTask
                   {
                       Id = user.Id,
                       Name = user.Name,
                   }
                   where filter(doi)
                   select doi;
        }
    }

    public void Update(BO.User user)
    {
        try
        {
            if (user.Id < 99999999 && user.Id > 999999999 && user.Name != "" && !new EmailAddressAttribute().IsValid(user.Email))
            {
                throw new BO.BlValueInvalidException($"value Invalid can not create user");
            }
            _dal.User.Update(new DO.User()
            {
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Name = user.Name,
                Level = (int)user.Level > (int)(Read(user.Id)).Level? (DO.UserLevel)user.Level : (DO.UserLevel)(Read(user.Id)).Level
            });
            Read(user.Id).Tasks.Clear();
            Read(user.Id).Tasks =user.Tasks;
        }
        catch(DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException(ex.Message, ex);
        }
    }
}
