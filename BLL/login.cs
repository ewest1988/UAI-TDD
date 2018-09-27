using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace BLL
{
    public class login
    {
        public BE.usuario usuario { get; set; }

        public bool loginUser(BE.usuario user)
        {
            var login = new DAL.login();

            return login.validarUsuario(user);
        }
    }
}
