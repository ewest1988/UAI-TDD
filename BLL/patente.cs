﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class patente
    {
        DAL.patente Patente = new DAL.patente();
        digitoVerificador gestorDV = new digitoVerificador();
        seguridad gestorSeguridad = new seguridad();

        public List<BE.patente> listarPatentes()
        {

            return Patente.listarPatentes();
        }

        public DataTable listarTablaUsuariosPatente()
        {
            DataTable usuariosPatentesTabla = Patente.listarTablaUsuariosPatentes();

            return usuariosPatentesTabla;
        }

        public List<BE.patente> listarPatentes(BE.familia familia)
        {

            return Patente.listarPatentes(familia);
        }

        public bool validarZonaDeNadie(int idPatente, int idUsuario)
        {

            return Patente.validarZonaDeNadie(idPatente, idUsuario);
        }

        public bool validarZonaDeNadiePN(int idPatente, int idUsuario)
        {

            return Patente.validarZonaDeNadiePN(idPatente, idUsuario);
        }

        public bool validarZonaDeNadieF(int idPatente, int idFamilia)
        {

            return Patente.validarZonaDeNadieF(idPatente, idFamilia);
        }

        public bool validarZonaDeNadieFU(int idPatente, int idFamilia)
        {

            return Patente.validarZonaDeNadieFU(idPatente, idFamilia);
        }

        public bool eliminarPatentesUsuario(BE.usuario usuario)
        {

            return Patente.eliminarPatentesUsuario(usuario);
        }

        public bool eliminarPatentesNegadasUsuario(BE.usuario usuario)
        {

            return Patente.eliminarPatentesNegadaUsuario(usuario);
        }

        public bool modificarPatentes(List<int> nuevasPatentes, BE.usuario usuario)
        {

            try
            {
                eliminarPatentesUsuario(usuario);

                foreach (int p in nuevasPatentes)
                {
                    BE.patente patente = new BE.patente();
                    patente.id_patente = p;
                    string dv = gestorSeguridad.ObtenerHash(p.ToString() + usuario.IdUsuario.ToString());
                    Patente.asignarPatenteUsuario(patente, usuario, dv);
                }

                gestorDV.modificarVerificador(gestorDV.CacularDVV(Patente.listarTablaUsuariosPatentes()), "Usuario_Patente");
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return true;
        }

        public bool modificarPatentesNegadas(List<int> nuevasPatentes, BE.usuario usuario)
        {

            try {

                eliminarPatentesNegadasUsuario(usuario);

                foreach (int p in nuevasPatentes) {

                    BE.patente patente = new BE.patente();
                    patente.id_patente = p;
                    Patente.asignarPatenteNegadaUsuario(patente, usuario);
                }
            }
            catch (Exception ex) {

                throw ex;
            }

            return true;
        }
    }
}
