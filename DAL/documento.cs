using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ionic.Zip;

namespace DAL
{
   
    public class documento
    {
        public int SQLParameter { get; private set; }

        public List<BE.tipoDocumento> listarTiposDocumentos() {

            List<BE.tipoDocumento> tiposDocumento = new List<BE.tipoDocumento>();
            DataTable datos = new DataTable();

            try
            {
                datos = SQLHelper.GetInstance().ObtenerDatos("select * from tipo_documento ");

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

        public bool eliminarDocumento(BE.documento doc)
        {

            int respuesta = 0;

            try
            {

                respuesta = SQLHelper.GetInstance().Ejecutar("delete from documento where id_documento = " + doc.idDocumento, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (respuesta > 0) return true;
            else return false;
        }

        public BE.documento listarDocumento(BE.documento doc)
        {
            try {

                return mapperDoc(SQLHelper.GetInstance().ObtenerDatos("select * from documento where id_documento = " + doc.idDocumento));
            }

            catch (Exception ex) {

                throw ex;
            }
        }

        public List<BE.documento> listarDocumentos(BE.filtroDocumento filtro)
        {
            string filterUser = "";
            string filterType = "";
            string filterName = "";
            string filterDate = "";

            filterDate = "WHERE FEC_CREACION = '" + filtro.fecCreacion.ToString("yyyy-MM-dd") + "' ";

            if (filtro.idUsuario != 0) filterUser = "AND ID_USUARIO = " + filtro.idUsuario + " ";
            if (filtro.idTipo != 0) filterType = "AND ID_TIPO = " + filtro.idTipo + " ";
            if (filtro.name != null) filterName= "AND DESC_DOCUMENTO like '%" + filtro.name + "%' ";

            return mapper(SQLHelper.GetInstance().ObtenerDatos("SELECT * FROM DOCUMENTO " + filterDate + filterUser + filterType + filterName));
        }

        public List<BE.documento> mapper(DataTable tablaDoc)
        {

            List<BE.documento> documentos = new List<BE.documento>();

            if (tablaDoc.Rows.Count > 0)
            {
                foreach (DataRow reg in tablaDoc.Rows)
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

            return documentos;
        }

        public BE.documento mapperDoc(DataTable tablaDoc)
        {
            BE.documento documento = new BE.documento();

            if (tablaDoc.Rows.Count > 0)
            {
                foreach (DataRow reg in tablaDoc.Rows)
                {
                    documento.idDocumento = Convert.ToInt32(reg["id_documento"]);
                    documento.idTipo = Convert.ToInt32(reg["id_tipo"]);
                    documento.nombre = reg["desc_documento"].ToString();
                    documento.extension = reg["ext_documento"].ToString();
                    documento.digitoVerificador = reg["digito_verificador"].ToString();
                    documento.contenido = (Byte[])reg["cont_documento"];

                }
            }

            return documento;
        }
        public List<BE.documento> listarDocumentos()
        {
            try {

                return mapper(SQLHelper.GetInstance().ObtenerDatos("select * from documento"));
            }

            catch (Exception ex) {

                throw ex;
            }
        }

        public bool guardarDocumento(BE.documento documento) {

            int respuesta = 0;

            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(new SqlParameter { ParameterName = "@idTipo", SqlDbType = SqlDbType.Int, Value = documento.idTipo });
            parametros.Add(new SqlParameter { ParameterName = "@fecCreacion", SqlDbType = SqlDbType.Date, Value = DateTime.Now.ToString("yyyy-MM-dd") });
            parametros.Add(new SqlParameter { ParameterName = "@idUsuario", SqlDbType = SqlDbType.Int, Value = documento.usuario.IdUsuario });
            parametros.Add(new SqlParameter { ParameterName = "@desc", SqlDbType = SqlDbType.VarChar, Value = documento.nombre });
            parametros.Add(new SqlParameter { ParameterName = "@ext", SqlDbType = SqlDbType.VarChar, Value = documento.extension });
            parametros.Add(new SqlParameter { ParameterName = "@cont", SqlDbType = SqlDbType.VarBinary, Value = documento.contenido });
            parametros.Add(new SqlParameter { ParameterName = "@dv", SqlDbType = SqlDbType.VarChar, Value = documento.digitoVerificador });

            try
            {

                respuesta = SQLHelper.GetInstance().EjecutarSP("SaveDocument", parametros);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (respuesta > 0) return true;
            else return false;
        }

        public string exportar(List<string> files)
        {
            try
            {
                ZipFile zip = new ZipFile();

                foreach (var f in files)
                {

                    zip.AddFile(f);
                }

                string filename = "Exportacion" + DateTime.Now.ToString("yyyy-MM-dd-HHmmss") + ".zip";
                zip.Save(filename);

                return filename;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
    }
}
