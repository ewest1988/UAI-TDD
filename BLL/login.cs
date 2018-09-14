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
        public BE.user usuario { get; set; }

        public bool loginUser(BE.user user)
        {
            var login = new DAL.login();

            return login.validarUsuario(user);
        }
    }
}
