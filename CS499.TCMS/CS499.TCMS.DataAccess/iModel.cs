using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS499.TCMS.DataAccess
{
    /// <summary>
    /// This interface will contain generic methods each model will implement
    /// </summary>
    public interface IModel
    {

        long ID { get; }

    }
}