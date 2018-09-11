using System;
using System.Data;
using Microsoft.VisualBasic;

public class MP_Bitacora
{
    private Acceso _acceso = new Acceso();

    public DataTable Listar_Bitacora()
    {
        DataTable datos;
        try
        {
            datos = _acceso.ObtenerDatos("SELECT * FROM Bitacora");
            return datos;
        }
        catch (Exception ex)
        {
            Interaction.MsgBox(ex.ToString());
            return null/* TODO Change to default(_) if this is not a reference type */;
        }
    }

    public DataTable Obtener_Bitacora(int id)
    {
        DataTable datos;
        try
        {
            datos = _acceso.ObtenerDatos("SELECT * FROM Bitacora WHERE ID_Bitacora = " + id);
            return datos;
        }
        catch (Exception ex)
        {
            Interaction.MsgBox(ex.ToString());
            return null/* TODO Change to default(_) if this is not a reference type */;
        }
    }

    public bool Eliminar_Bitacora(int id)
    {
        bool res;
        try
        {
            res = _acceso.Ejecutar("DELETE FROM Bitacora WHERE ID_Bitacora = " + id, 0);
            return res;
        }
        catch (Exception ex)
        {
            Interaction.MsgBox(ex.ToString());
            return false;
        }
    }

    public bool Agregar_Bitacora(string usuario, string descripcion, DateTime fecha)
    {
        bool res;
        try
        {
            
            res = _acceso.Ejecutar("INSERT INTO Bitacora(Usuario,Descripcion,Fecha) VALUES('" + usuario + "','" + descripcion + "'," + fecha + ")", 0);
            return res;
        }
        catch (Exception ex)
        {
            Interaction.MsgBox(ex.ToString());
            return false;
        }
    }
}
