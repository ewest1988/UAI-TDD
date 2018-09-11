using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public interface ICRUD<T>
    {
        bool Create(T objAlta);
        List<T> Retrieve();
        bool Delete(T objDel);
        bool Update(T objUpd);
    }
}
