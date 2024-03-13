namespace BlImplementation;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// Implementation of the IUser interface for handling user-related operations.
/// </summary>
internal class UserImplementation : BlApi.IUser
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    /// <summary>
    /// Creates a new user.
    /// </summary>
    /// <param name="user">The user object to create.</param>
    /// <returns>The ID of the created user.</returns>
    public int Create(BO.User user)
    {
        try
        {
            int idUser;
            if (user.Id < 99999999 && user.Id > 999999999 && user.Name != "" && !new EmailAddressAttribute().IsValid(user.Email))
            {
                throw new BO.BlValueInvalidException($"value Invalid can not create user");
            }

            if(user.Password.Length>8 && user.Password.Length<5)
            {
                throw new BO.BlValueInvalidException($"Password must be at least 5-8 characters long");
            }
            idUser = _dal.User.Create(new DO.User()
            {
                Id = user.Id,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                password= user.Password,
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

    /// <summary>
    /// Deletes a user by ID.
    /// </summary>
    /// <param name="id">The ID of the user to delete.</param>
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
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException(ex.Message, ex);
        }
    }

    /// <summary>
    /// Reads a user by ID.
    /// </summary>
    /// <param name="id">The ID of the user to read.</param>
    /// <returns>The user object.</returns>
    public BO.User? Read(int id)
    {
        DO.User? douser = _dal.User.Read(id);
        if (douser == null)
        {
            throw new BO.BlDoesNotExistException($"User with ID={id} does Not exist");
        }
        return new BO.User()
        {
            Id = douser.Id,
            Email = douser.Email,
            PhoneNumber = douser.PhoneNumber,
            Password = douser.password,
            Name = douser.Name,
            Level = (BO.UserLevel)douser.Level

        };
    }

    /// <summary>
    /// Reads all users optionally filtered by a predicate.
    /// </summary>
    /// <param name="filter">Optional filter predicate.</param>
    /// <returns>A collection of users.</returns>
    public IEnumerable<BO.UserInTask> ReadAll(Func<BO.UserInTask, bool>? filter = null)
    {
        if (filter == null)
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
                   let doi = new BO.UserInTask
                   {
                       Id = user.Id,
                       Name = user.Name,
                   }
                   where filter(doi)
                   select doi;
        }
    }
    public IEnumerable<BO.User> ReadAllUser(Func<BO.User, bool>? filter = null)
    {
        if(filter==null)
        {
            return from user in _dal.User.ReadAll()
                   select new BO.User
                   {

                       Id = user.Id,
                       Email = user.Email,
                       PhoneNumber = user.PhoneNumber,
                       Password = user.password,
                       Name = user.Name,
                       Level = (BO.UserLevel)user.Level
                   };
        }
        else
        {
            return from user in _dal.User.ReadAll()
                   let doi = new BO.User
                   {
                       Id = user.Id,
                       Email = user.Email,
                       PhoneNumber = user.PhoneNumber,
                       Password = user.password,
                       Name = user.Name,
                       Level = (BO.UserLevel)user.Level
                   }
                   where filter(doi)
                   select doi;
        }
    }
    /// <summary>
    /// Updates an existing user.
    /// </summary>
    /// <param name="user">The updated user object.</param>
    public void Update(BO.User user)
    {
        try
        {
            if (user.Id < 99999999 || user.Id > 999999999 || user.Name == "") //|| !new EmailAddressAttribute().IsValid(user.Email))
            {
                throw new BO.BlValueInvalidException($"value Invalid, can not update user");
            }
            if (user.Password.Length > 8 && user.Password.Length < 5)
            {
                throw new BO.BlValueInvalidException($"Password must be at least 5-8 characters long");
            }
            _dal.User.Update(new DO.User()
            {
                Id = user.Id,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                password = user.Password,
                Name = user.Name,
                Level = (int)user.Level > (int)(Read(user.Id)).Level ? (DO.UserLevel)user.Level : (DO.UserLevel)(Read(user.Id)).Level
            });
            if (user.Tasks != null)
            {
                Read(user.Id).Tasks.Clear();
                Read(user.Id).Tasks = user.Tasks;
            }
            else
                Read(user.Id).Tasks = user.Tasks;
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException(ex.Message, ex);
        }
    }
}
