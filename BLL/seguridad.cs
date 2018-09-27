using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BLL
{
    public class seguridad
    {
        public bool TextValid(string text)
        {
            if (!string.IsNullOrWhiteSpace(text))
                return true;
            return false;
        }

        public bool IntValid(string text)
        {
            if (!string.IsNullOrWhiteSpace(text) & text.All(char.IsNumber))
                return true;
            return false;
        }

        public bool DateValid(string text)
        {
            if (!string.IsNullOrWhiteSpace(text) & CheckDate(text))
                return true;

            return false;
        }

        public bool MailValido(string text)
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                var regex = new Regex(@"^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,3})$");
                var match = regex.Match(text);
                if (match.Success)
                    return true;
                else
                    return false;
            }
            return false;
        }

        // Telefono valido de 10 caracteres (0-9)
        public bool TelefonoValido(string text)
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                var regex = new Regex("^[0-9]{10}$");
                var match = regex.Match(text);
                if (match.Success)
                    return true;
                else
                    return false;
            }
            return false;
        }


        // Clave valida de 8 caracteres minimo 1 mayuscul, 1 minuscula, 1 numero o simbolo
        public bool ClaveValida(string text)
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                var regex = new Regex(@"(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$");
                var match = regex.Match(text);
                if (match.Success)
                    return true;
                else
                    return false;
            }
            return false;
        }

        // Usuario valido minimo 6, maximo 18 caracteres (a-z,0-9)
        public bool UsuarioValido(string text)
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                var regex = new Regex("^[a-z0-9_-]{6,18}$");
                var match = regex.Match(text);
                if (match.Success)
                    return true;
                else
                    return false;
            }
            return false;
        }

        // Numero decimal valido en el formato *.xx (2 decimales)
        public bool DoubleValido(string text)
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                // Valida numeros en el formato *.11
                var regex = new Regex("^[0-9]*([,][0-9]{1,2})?$");
                var match = regex.Match(text);
                if (match.Success)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public string ObtenerHash(string theInput)
        {
            using (MD5 hasher = MD5.Create())
            {
                byte[] dbytes = hasher.ComputeHash(Encoding.UTF8.GetBytes(theInput));
                StringBuilder sBuilder = new StringBuilder();

                for (int n = 0; n <= dbytes.Length - 1; n++)
                    sBuilder.Append(dbytes[n].ToString("X2"));

                return sBuilder.ToString();
            }
        }

        protected bool CheckDate(String date) {

            try {

                DateTime dt = DateTime.Parse(date);
                return true;
            }
            catch {

                return false;
            }
        }
    }
}
