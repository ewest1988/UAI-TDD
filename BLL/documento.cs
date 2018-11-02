using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class documento
    {
        DAL.documento Documento = new DAL.documento();

        public List<BE.tipoDocumento> listarTiposDocumentos()
        {

            return Documento.listarTiposDocumentos();
        }

        public bool guardarDocumento(BE.documento documento) {

            return Documento.guardarDocumento(documento);
        }

        public BE.documento listarDocumento(BE.documento documento)
        {

            return Documento.listarDocumento(documento);
        }

        public List<BE.documento> listarDocumentos()
        {

            return Documento.listarDocumentos();
        }

        public string concatenarCampos(BE.documento documento) {

            return documento.idTipo + documento.fechaCreacion.ToString() + documento.usuario.IdUsuario.ToString() + documento.nombre + documento.extension + documento.contenido;

        }


    }
}
