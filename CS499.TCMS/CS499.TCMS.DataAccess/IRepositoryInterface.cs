using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace CS499.TCMS.DataAccess
{
    /// <summary>
    /// Exposes a generic interface that all interfaces will inherit from
    /// </summary>
    public interface IRepository
    {
        T getSingle<T>();
        IEnumerator getAll();
        bool Update();                      //return true on success
        bool Delete<T>(T obj);              //return true on success
        bool Insert<T>(T obj);              //return true on success


        void Map();                         //NOT FINISHED. WILL HAVE AN IDATAREADER PARAMETER WHEN FINISHED.


        
    }
}
