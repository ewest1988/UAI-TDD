using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

public class MP_Backup
{
    private Acceso _acceso = new Acceso();

    public void Exportar(string database, string path)
    {
        try
        {
            _acceso.MasterQuery("BACKUP DATABASE " + database + " TO disk='" + path + "'");
        }
        catch (Exception ex)
        {
            Interaction.MsgBox(ex.ToString());
        }
    }

    public void Importar(string database, string path)
    {
        try
        {
            _acceso.MasterQuery("ALTER DATABASE " + database + " SET SINGLE_USER WITH ROLLBACK IMMEDIATE");
            _acceso.MasterQuery("RESTORE DATABASE " + database + " FROM disk='" + path + "' WITH REPLACE");
            _acceso.MasterQuery("ALTER DATABASE " + database + " SET MULTI_USER");
        }
        catch (Exception ex)
        {
            Interaction.MsgBox(ex.ToString());
        }
    }
}

