using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SQLHelper
    {
        public SqlConnection cn { get; set; }

        public SqlConnection con = new SqlConnection(@"Data Source=N1075002\SQLEXPRESS;Initial Catalog=editorial;User ID=administrador;Password=Admin2018");
        private SqlTransaction tx;
        private SqlCommand command;

        private static SQLHelper instance = null;

        private SQLHelper() {

            if (!File.Exists("conexion.txt"))
            {
                File.Create("conexion.txt");
            }
            StreamReader SR = File.OpenText("conexion.txt");

            try
            {
                string s = SR.ReadLine();
                con.ConnectionString.ToString();
                cn = new SqlConnection(@"");
                cn.ConnectionString = s;
                SR.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            } finally
            {
                SR.Close();
            }
        }

        public static SQLHelper GetInstance()
        {
            if (instance == null)
                instance = new SQLHelper();

            return instance;
        }

        public bool modificarStringConexion(string strCon) {

            try
            {
                cn.ConnectionString = strCon;

                if (File.Exists("conexion.txt"))
                {
                    File.Delete("conexion.txt");
                }

                FileStream F = File.Create("conexion.txt");
                F.Close();

                StreamWriter SW = File.AppendText("conexion.txt");
                SW.WriteLine(strCon);
                SW.Close();

                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public string obtenerStringConexion()
        {

            return cn.ConnectionString;
        }

        public DataTable ObtenerDatos(string comando)
        {
            Abrir();
            DataTable datos = new DataTable();
            command = new SqlCommand() { CommandText = comando, Connection = cn };
            SqlDataAdapter da = new SqlDataAdapter(command);
            IniciarTX();
            if (tx != null)
                command.Transaction = tx;
            try
            {

                da.Fill(datos);
                ConfirmarTX();
            }
            catch (Exception ex)
            {
                CancelarTX();
                throw ex;
            }
            finally
            {
                Cerrar();
            }
            return datos;
        }

        public int EjecutarSP(string procedure, List<SqlParameter> parameters)
        {
            Abrir();
            int fa = 0;
            command = new SqlCommand();

            {
                var withBlock = command;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = procedure;

                foreach (var p in parameters) {
                    command.Parameters.Add(p);
                }

                command.Connection = cn;
                IniciarTX();
                if (tx != null)
                    withBlock.Transaction = tx;
            }

            try
            {
                fa = command.ExecuteNonQuery();

                ConfirmarTX();
            }
            catch (Exception ex)
            {
                CancelarTX();
                throw ex;
            }
            finally
            {
                Cerrar();
            }

            return fa;
        }

        public string EjecutarScalar(string comando)
        {
            Abrir();
            string fa = " ";
            command = new SqlCommand();

            {
                var withBlock = command;
                command.CommandText = comando;
                command.Connection = cn;
                IniciarTX();
                if (tx != null)
                    withBlock.Transaction = tx;
            }

            try
            {
                object result = command.ExecuteScalar();
                if (result != null)
                    fa = result.ToString();
                //fa = command.ExecuteScalar().ToString() + ""; 

                ConfirmarTX();
            }
            catch (Exception ex)
            {
                CancelarTX();
                throw ex;    
            }
            finally
            {
                Cerrar();
            }

            return fa.ToString();
        }

        public int Ejecutar(string comando, bool scalar)
        {
            Abrir();
            int fa = 0;
            command = new SqlCommand();

            {
                var withBlock = command;
                command.CommandText = comando;
                command.Connection = cn;
                IniciarTX();
                if (tx != null)
                    withBlock.Transaction = tx;
            }

            try {

                fa = command.ExecuteNonQuery();
                ConfirmarTX();
            }
            catch (Exception ex) {

                CancelarTX();
                throw ex;
            }
            finally
            {
                Cerrar();
            }

            return fa;
        }

        public int MasterQuery(string comando)
        {
            int fa = 0;
            try
            {
                cn.Open();
                command = new SqlCommand(comando, cn);
                fa = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cn.Close();
            }
            return fa;
        }

        private void Abrir()
        {
            try
            {
                cn.Open();
            
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Cerrar()
        {
            if (tx != null)
            {
                cn.Close();
            }
        }

        public void IniciarTX()
        {
            if (cn == null || cn.State != ConnectionState.Open)
                Abrir();
            tx = cn.BeginTransaction();
        }

        public void ConfirmarTX()
        {
            tx.Commit();
            Cerrar();
        }

        public void CancelarTX()
        {
            tx.Rollback();
            Cerrar();
        }
    }
}
