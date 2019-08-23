using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;


namespace DAL
{
    public class login 
    {
        public bool validarUsuario(BE.usuario user)
        {
            string passbase = null;

            try
            {
                passbase = SQLHelper.GetInstance().EjecutarScalar("SELECT contraseña FROM Usuario WHERE Usuario = '" + user.uss + "' AND ID_ESTADO <> 3");
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (passbase == user.pass)
                return true;
            else
                return false;
        }

        public bool Delete(login objDel)
        {
            throw new NotImplementedException();
        }

        public List<login> Retrieve()
        {
            throw new NotImplementedException();
        }

        public bool Update(login objUpd)
        {
            throw new NotImplementedException();
        }
    }
}
