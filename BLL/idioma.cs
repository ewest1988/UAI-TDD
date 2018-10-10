using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class idioma {

        DAL.idioma Idioma = new DAL.idioma();

        public List<BE.idioma> listarIdioma(BE.idioma idioma) {

            return Idioma.listarIdioma(idioma);
        }
    }
}
