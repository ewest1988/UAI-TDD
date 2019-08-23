using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAL
{
    public class backup {

        public string ObtenerBackup() {

            var rutaBackup = "";
            var nombreBaseDeDatos = SQLHelper.GetInstance().cn.Database.ToString();
            var nombreBackup = nombreBaseDeDatos + "-" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".bak";

            try {

                SQLHelper.GetInstance().MasterQuery("BACKUP DATABASE " + nombreBaseDeDatos + " TO disk='" + nombreBackup + "'");

                var dtDest = SQLHelper.GetInstance().ObtenerDatos("SELECT top 1 physical_device_name as ruta ,backup_start_date, backup_finish_date, backup_size AS tamaño FROM msdb.dbo.backupset b JOIN msdb.dbo.backupmediafamily m ON b.media_set_id = m.media_set_id WHERE physical_device_name like '%" + nombreBackup + "%' ORDER BY backup_finish_date DESC");

                if (dtDest.Rows.Count > 0)
                {
                    foreach (DataRow reg in dtDest.Rows)
                    {
                        rutaBackup = Convert.ToString(reg["ruta"]);
                    }
                }

                return rutaBackup;
                //zIpDatabseFile(path + "-original.zip", path + ".zip");
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public bool RealizarRestore(String rutaOrigen)
        {
            try
            {
                if(!File.Exists("backup.txt"))
                {
                    FileStream f = File.Create("backup.txt");
                    f.Close();
                }

                StreamReader SR = File.OpenText("backup.txt");

                var rutaBackup = SR.ReadLine();

                SR.Close();
                //var rutaBackup = "C:\\Program Files\\Microsoft SQL Server\\MSSQL14.SQLEXPRESS\\MSSQL\\Backup\\";

                using (ZipFile zipFile = new ZipFile(rutaOrigen))
                {
                    rutaBackup = rutaBackup + "\\Backup-" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
                    zipFile.ExtractAll(rutaBackup);

                    string[] zipFiles = Directory.GetFiles(rutaBackup, "*.zip*", SearchOption.AllDirectories);

                    if (zipFiles.Length > 0)
                    {
                        var zipFile2 = new ZipFile(zipFiles[0]);
                        zipFile2.ExtractAll(rutaBackup);
                    }

                    string[] backFiles = Directory.GetFiles(rutaBackup, "*.bak*", SearchOption.AllDirectories);

                    if (backFiles.Length == 1)
                    {
                        Restore(backFiles[0]);
                    } else { return false; }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        public string Restore( string path) {

            var nombreBaseDeDatos = SQLHelper.GetInstance().cn.Database.ToString();
            //string pathDest = path.Substring(0, path.Length - 5);

            try {
                SQLHelper.GetInstance().MasterQuery("USE MASTER ALTER DATABASE " + nombreBaseDeDatos + " SET SINGLE_USER WITH ROLLBACK IMMEDIATE");
                SQLHelper.GetInstance().MasterQuery("USE MASTER RESTORE DATABASE " + nombreBaseDeDatos + " FROM disk='" + path + "' WITH REPLACE");
                SQLHelper.GetInstance().MasterQuery("USE MASTER ALTER DATABASE " + nombreBaseDeDatos + " SET MULTI_USER");

                return path;
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public bool RealizarBackup(String rutaDestino, int cantidadVolumenes)
        {
            try
            {
                using (ZipFile zip = new ZipFile())
                {
                    var backupPath = ObtenerBackup();
                    var ruta = backupPath;
                    var multiplesVolumenes = cantidadVolumenes > 1;
                    var rutaDestinoTemp = rutaDestino + "\\Editorial-" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".zip";

                    zip.AddFile(ruta, "");
                    zip.TempFileFolder = Path.GetTempPath();
                    zip.Save(rutaDestinoTemp);

                    if (cantidadVolumenes > 1)
                    {
                        FileInfo fileInfo = new FileInfo(rutaDestinoTemp);
                        var tamañoDeVolumen = fileInfo.Length / cantidadVolumenes;

                        using (ZipFile zip2 = new ZipFile())
                        {
                            zip2.MaxOutputSegmentSize = (int)tamañoDeVolumen;
                            zip2.AddFile(rutaDestinoTemp, "");
                            zip2.TempFileFolder = Path.GetTempPath();
                            zip2.Save(rutaDestinoTemp);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }
    }
}
