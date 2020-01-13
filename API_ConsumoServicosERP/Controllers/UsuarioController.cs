using API_ConsumoServicosERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API_ConsumoServicosERP.Controllers
{
    public class UsuarioController : ApiController
    {
        private static List<Usuario> userList = new List<Usuario>();

        [HttpGet]
        public List<Usuario> GetUsuario([FromBody]string filter)
        {
            if (string.IsNullOrEmpty(filter))
                filter = "1=1";

            var list = Usuario.ListaUsuarios(filter);

            return list;
            //return userList;
        }

        [HttpPost]
        public string PostUsuario(string codUsuario, string nomeUsuario, string email, int status)
        {
            var ret = string.Empty;

            try
            {
                if (string.IsNullOrEmpty(codUsuario) || string.IsNullOrEmpty(nomeUsuario))
                    ret = "Código do Usuário ou Nome não foram preenchidos.";
                else
                {
                    userList.Add(new Usuario(codUsuario, nomeUsuario, email, status));
                    ret = "Usuario adicionado com sucesso. Usuário: " + codUsuario + " | Nome: " + nomeUsuario;
                }
            }
            catch (Exception ex)
            {
                ret = "Erro ao tentar incluir novo Usuario. Mensagem: " + ex.Message;
            }

            return ret;
        }

        [HttpPost]
        public string PostUsuarioBody([FromBody]Usuario dados)
        {
            var ret = string.Empty;

            try
            {
                if (string.IsNullOrEmpty(dados.CodUsuario) || string.IsNullOrEmpty(dados.NomeUsuario))
                    ret = "Nome ou Telefone não foram preenchidos.";
                else
                {
                    userList.Add(new Usuario(dados.CodUsuario, dados.NomeUsuario, dados.Email, dados.Status));
                    ret = "Usuario adicionado com sucesso. Usuário: " + dados.CodUsuario + " | Nome: " + dados.NomeUsuario;
                }
            }
            catch (Exception ex)
            {
                ret = "Erro ao tentar incluir novo Usuario. Mensagem: " + ex.Message;
            }

            return ret;
        }

        [HttpDelete]
        public string DeleteUsuario(string usr)
        {
            var ret = string.Empty;

            try
            {
                if (userList.Count == 0)
                {
                    ret = "Lista de Usuarios está vazia";
                }
                else
                {
                    //Remove primeiro registro encontrado. Localização por ID da lista
                    //userList.RemoveAt(userList.IndexOf(userList.First(x => x.Nome.Contains(nomePar))));

                    //Remove todos os registros que contenham essa informação
                    userList.RemoveAll(x => x.CodUsuario.Contains(usr));
                    ret = "Usuarios com nome [" + usr + "] excluídos com sucesso.";

                }
            }
            catch (Exception ex)
            {
                ret = "Erro ao tentar excluir Usuario. Mensagem: " + ex.Message;
            }

            return ret;
        }

        [HttpDelete]
        public string DeleteUsuarioBody([FromBody]string usr)
        {
            var ret = string.Empty;

            try
            {
                if (userList.Count == 0)
                {
                    ret = "Lista de Usuarios está vazia";
                }
                else
                {
                    //Remove primeiro registro encontrado. Localização por ID da lista
                    //userList.RemoveAt(userList.IndexOf(userList.First(x => x.Nome.Contains(nomePar))));

                    //Remove todos os registros que contenham essa informação
                    userList.RemoveAll(x => x.CodUsuario.Contains(usr));
                    ret = "Usuarios com nome [" + usr + "] excluídos com sucesso.";

                }
            }
            catch (Exception ex)
            {
                ret = "Erro ao tentar excluir Usuario. Mensagem: " + ex.Message;
            }

            return ret;
        }
    }
}
