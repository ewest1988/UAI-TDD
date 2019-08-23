using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class documento
    {
        DAL.documento Documento = new DAL.documento();
        digitoVerificador gestorDV = new digitoVerificador();

        public List<BE.tipoDocumento> listarTiposDocumentos()
        {

            return Documento.listarTiposDocumentos();
        }

        public bool guardarDocumento(BE.documento documento) {

            try
            {
                Documento.guardarDocumento(documento);
                gestorDV.modificarVerificador(gestorDV.CacularDVV("documento"), "documento");
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public string exportar(List<string> files)
        {
            return Documento.exportar(files);
        }

        public bool eliminarDocumento(BE.documento documento)
        {

            try
            {
                Documento.eliminarDocumento(documento);
                gestorDV.modificarVerificador(gestorDV.CacularDVV("documento"), "documento");
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public BE.documento listarDocumento(BE.documento documento)
        {

            return Documento.listarDocumento(documento);
        }

        public List<BE.documento> listarDocumentos()
        {

            return Documento.listarDocumentos();
        }

        public List<BE.documento> listarDocumentos(BE.filtroDocumento filtro)
        {

            return Documento.listarDocumentos(filtro);
        }

        public string concatenarCampos(BE.documento documento) {

            return documento.idTipo + documento.fechaCreacion.ToString() + documento.usuario.IdUsuario.ToString() + documento.nombre + documento.extension + documento.contenido;

        }
    }
}
