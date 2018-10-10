using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    class SQLHelper
    {
        private SqlConnection cn = new SqlConnection(@"Data Source=N1075001\SQLEXPRESS;Initial Catalog=editorial;User ID=administrador;Password=Admin2018");
        private SqlTransaction tx;
        private SqlCommand command;

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
                fa = command.ExecuteScalar().ToString(); 

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
