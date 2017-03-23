using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CS499.TCMS.DataAccess;
using CS499.TCMS.Model;
using System.Diagnostics;

namespace CS499.TCMS.ViewUnitTest.DummyClasses
{
    public class DummyUserRepository : IUserRepository
    {
        void IRepository<User>.Delete(User model)
        {
            Debug.Print("Delete user with EmployeeID = {0}", model.EmployeeID);
        }

        IEnumerable<User> IRepository<User>.GetAll()
        {
            Debug.Print("GetAll users");
            return new List<User>();
        }

        User IRepository<User>.GetSingle(object id)
        {
            Debug.Print("GetSingle user with EmployeeID = {0}", id);
            return new User((long)id, string.Empty, string.Empty, string.Empty, string.Empty,
                string.Empty, string.Empty, string.Empty, 0, string.Empty, string.Empty, string.Empty,
                0, DateTime.Now, 0, string.Empty, string.Empty, false, string.Empty, string.Empty);
        }

        User IUserRepository.getSingleByName(string firstname, string middlename, string lastname)
        {
            throw new NotImplementedException();
        }

        User IUserRepository.getUserByUserName(string username)
        {
            throw new NotImplementedException();
        }

        IEnumerable<User> IUserRepository.getUsersByHomeStore(string HomeStore)
        {
            throw new NotImplementedException();
        }

        IEnumerable<User> IUserRepository.getUsersByJobAssignment(int JobAssignment)
        {
            throw new NotImplementedException();
        }

        IEnumerable<User> IUserRepository.getUsersByZipCode(int zip)
        {
            throw new NotImplementedException();
        }

        void IRepository<User>.Insert(User model)
        {
            Debug.Print("Insert user with EmployeeID = {0}", model.EmployeeID);
        }

        void IRepository<User>.Update(User model)
        {
            Debug.Print("Update user with EmployeeID = {0}", model.EmployeeID);
        }

        void IUserRepository.updatePassword(User model, string newHash)
        {
            throw new NotImplementedException();
        }
    }
}
