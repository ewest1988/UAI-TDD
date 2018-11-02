using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   
    public class documento
    {
        SQLHelper sqlHelper = new SQLHelper();

        public int SQLParameter { get; private set; }

        public List<BE.tipoDocumento> listarTiposDocumentos() {

            List<BE.tipoDocumento> tiposDocumento = new List<BE.tipoDocumento>();
            DataTable datos = new DataTable();

            try
            {
                datos = sqlHelper.ObtenerDatos("select * from tipo_documento ");

                foreach (DataRow reg in datos.Rows)
                {
                    BE.tipoDocumento tipoDocumento = new BE.tipoDocumento();
                    tipoDocumento.idTipo = Convert.ToInt32(reg["id_tipo_documento"]);
                    tipoDocumento.descTipo = reg["descripcion"].ToString();

                    tiposDocumento.Add(tipoDocumento);
                }
            }

            catch (Exception ex)
            {
                throw ex;

            }

            return tiposDocumento;
        }

        public BE.documento listarDocumento(BE.documento doc)
        {

            BE.documento documento = new BE.documento();
            DataTable datos = new DataTable();

            try
            {
                datos = sqlHelper.ObtenerDatos("select * from documento where id_documento = " + doc.idDocumento);

                foreach (DataRow reg in datos.Rows)
                {
                    documento.idDocumento = Convert.ToInt32(reg["id_documento"]);
                    documento.idTipo = Convert.ToInt32(reg["id_tipo"]);
                    documento.nombre = reg["desc_documento"].ToString();
                    documento.extension = reg["ext_documento"].ToString();
                    documento.digitoVerificador = reg["digito_verificador"].ToString();
                    documento.contenido = (Byte[])reg["cont_documento"];

                }
            }

            catch (Exception ex)
            {
                throw ex;

            }

            return documento;
        }

        public List<BE.documento> listarDocumentos()
        {

            List<BE.documento> documentos = new List<BE.documento>();
            DataTable datos = new DataTable();

            try
            {
                datos = sqlHelper.ObtenerDatos("select * from documento");

                foreach (DataRow reg in datos.Rows)
                {
                    BE.documento documento = new BE.documento();
                    documento.idDocumento = Convert.ToInt32(reg["id_documento"]);
                    documento.idTipo = Convert.ToInt32(reg["id_tipo"]);
                    documento.nombre = reg["desc_documento"].ToString();
                    documento.extension = reg["ext_documento"].ToString();
                    documento.digitoVerificador = reg["digito_verificador"].ToString();
                    documento.contenido = (Byte[])reg["cont_documento"];

                    documentos.Add(documento);
                }
            }

            catch (Exception ex)
            {
                throw ex;

            }

            return documentos;
        }

        public bool guardarDocumento(BE.documento documento) {

            int respuesta = 0;

            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(new SqlParameter { ParameterName = "@idTipo", SqlDbType = SqlDbType.Int, Value = documento.idTipo });
            parametros.Add(new SqlParameter { ParameterName = "@fecCreacion", SqlDbType = SqlDbType.Date, Value = documento.fechaCreacion });
            parametros.Add(new SqlParameter { ParameterName = "@idUsuario", SqlDbType = SqlDbType.Int, Value = documento.usuario.IdUsuario });
            parametros.Add(new SqlParameter { ParameterName = "@desc", SqlDbType = SqlDbType.VarChar, Value = documento.nombre });
            parametros.Add(new SqlParameter { ParameterName = "@ext", SqlDbType = SqlDbType.VarChar, Value = documento.extension });
            parametros.Add(new SqlParameter { ParameterName = "@cont", SqlDbType = SqlDbType.VarBinary, Value = documento.contenido });
            parametros.Add(new SqlParameter { ParameterName = "@dv", SqlDbType = SqlDbType.VarChar, Value = documento.digitoVerificador });

            try
            {

                respuesta = sqlHelper.EjecutarSP("SaveDocument", parametros);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (respuesta > 0) return true;
            else return false;
        }
    }
}
