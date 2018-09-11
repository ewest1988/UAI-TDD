using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Microsoft.VisualBasic;

public class Acceso
{
    private SqlConnection cn;
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
        object fa;
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
            fa = command.ExecuteScalar();

            ConfirmarTX();
        }
        catch (Exception ex)
        {
            fa = null;
            CancelarTX();
            return fa.ToString();
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
        object fa = 0;
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
            if (!scalar)
                fa = command.ExecuteNonQuery();
            else
                fa = command.ExecuteScalar();

            ConfirmarTX();
        }
        catch (Exception ex)
        {
            fa = -1;
            CancelarTX();
            return (Int32)fa;
        }
        finally
        {
            Cerrar();
        }

        return (Int32)fa;
    }

    public int MasterQuery(string comando)
    {
        int fa = 0;
        try
        {
            if (cn == null)
            {
                cn = new SqlConnection(@"Data Source=.\EWEST;Initial Catalog=editorial;Integrated Security=True;");
                cn.Open();
            }
            command = new SqlCommand(comando, cn);
            fa = command.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            fa = -1;
            return fa;
        }
        finally
        {
            Cerrar();
        }
        return fa;
    }

    private void Abrir()
    {
        try
        {
            if (cn == null)
            {
                cn = new SqlConnection("strConnection");
                cn.Open();
            }
        }
        catch (Exception ex)
        {
            Interaction.MsgBox(ex.ToString());
        }
    }

    private void Cerrar()
    {
        if (tx == null)
        {
            cn.Close();
            cn = null;
            GC.Collect();
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
