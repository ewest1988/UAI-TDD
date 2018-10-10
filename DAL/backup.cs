using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAL
{
    public class backup {

        SQLHelper sqlHelper = new SQLHelper();

        public void exportar(string database, string path) {

            try {

                sqlHelper.MasterQuery("BACKUP DATABASE " + database + " TO disk='" + path + "-original.zip'");
                zIpDatabseFile(path + "-original.zip", path + ".zip");
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public void importar(string database, string path) {

            string pathDest = path.Substring(0, path.Length - 5);

            try {
                uNzIpDatabaseFile(path, pathDest);
                sqlHelper.MasterQuery("USE MASTER ALTER DATABASE " + database + " SET SINGLE_USER WITH ROLLBACK IMMEDIATE");
                sqlHelper.MasterQuery("USE MASTER RESTORE DATABASE " + database + " FROM disk='" + pathDest + "' WITH REPLACE");
                sqlHelper.MasterQuery("USE MASTER ALTER DATABASE " + database + " SET MULTI_USER");
            }
            catch (Exception ex) {

                throw ex;
            }
        }



        private void zIpDatabseFile(string srcPath, string destPath) {

            byte[] bufferWrite;
            FileStream fsSource;
            FileStream fsDest;
            GZipStream gzCompressed;

            fsSource = new FileStream(srcPath, FileMode.Open, FileAccess.Read, FileShare.Read);
            bufferWrite = new byte[fsSource.Length];

            fsSource.Read(bufferWrite, 0, bufferWrite.Length);
            fsDest = new FileStream(destPath, FileMode.OpenOrCreate, FileAccess.Write);
            gzCompressed = new GZipStream(fsDest, CompressionMode.Compress, true);
            gzCompressed.Write(bufferWrite, 0, bufferWrite.Length);

            fsSource.Close();
            gzCompressed.Close();
            fsDest.Close();

            File.Delete(srcPath);
        }

        private void uNzIpDatabaseFile(string SrcPath, string DestPath) {

            byte[] bufferWrite;
            FileStream fsSource;
            FileStream fsDest;
            GZipStream gzDecompressed;

            fsSource = new FileStream(SrcPath, FileMode.Open, FileAccess.Read, FileShare.Read);
            gzDecompressed = new GZipStream(fsSource, CompressionMode.Decompress, true);

            bufferWrite = new byte[4];
            fsSource.Position = (int)fsSource.Length - 4;
            fsSource.Read(bufferWrite, 0, 4);
            fsSource.Position = 0;

            int bufferLength = BitConverter.ToInt32(bufferWrite, 0);
            byte[] buffer = new byte[bufferLength + 100];

            int readOffset = 0;
            int totalBytes = 0;

            while (true) {

                int bytesRead = gzDecompressed.Read(buffer, readOffset, 100);

                if (bytesRead == 0) break;

                readOffset += bytesRead;
                totalBytes += bytesRead;
            }

            fsDest = new FileStream(DestPath, FileMode.Create);
            fsDest.Write(buffer, 0, totalBytes);

            fsSource.Close();
            gzDecompressed.Close();
            fsDest.Close();
        }
    }
}
