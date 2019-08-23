using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Backup
    {
        DAL.backup backup = new DAL.backup();
        public void exportar(string path, int volumenes) {

            backup.RealizarBackup(path, volumenes);
        }

        public bool importar(string path) {

            return backup.RealizarRestore(path);
        }
    }
}
