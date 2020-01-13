using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace API_ConsumoServicosERP.Models
{
    public class Usuario
    {
        #region Dados do Usuário
        public string CodUsuario { get; set; }
        public string NomeUsuario { get; set; }
        public string Email { get; set; }
        public int Status { get; set; }
        #endregion

        public Usuario(string usuario, string nome, string email, int status)
        {
            this.CodUsuario = usuario;
            this.NomeUsuario = nome;
            this.Email = email;
            this.Status = status;
        }

        public static List<Usuario> ListaUsuarios(string filter = "")
        {
            var ret = new List<Usuario>();

            WebServices ws = new WebServices();

            var wsReturn = ws.LerVisao("tulio.silva", "tgss123", "GlbUsuarioData", filter, "codcoligada=1;codusuario=tulio.silva");

            foreach (DataTable dt in wsReturn.Tables)
            {
                if (dt.TableName == "GUSUARIO")
                {
                    foreach (DataRow dr in dt.Rows)
                    {

                        ret.Add(
                            new Usuario(
                                dr["CODUSUARIO"].ToString(),
                                dr["NOME"].ToString(),
                                dr["EMAIL"].ToString(),
                                Convert.ToInt32(dr["STATUS"])
                                ));
                    }

                }
                else
                {
                    throw new Exception(wsReturn.Tables[0].Rows[0]["message"].ToString());
                }
            }
            return ret;
        }

    }
}