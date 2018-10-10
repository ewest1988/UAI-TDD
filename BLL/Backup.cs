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
        public void exportar(string Database, string path) {

            backup.exportar(Database, path);
        }

        public void importar(string Database, string path) {

            backup.importar(Database, path);
        }
    }
}
