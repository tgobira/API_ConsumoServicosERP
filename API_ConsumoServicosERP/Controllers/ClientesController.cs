using API_ConsumoServicosERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API_ConsumoServicosERP.Controllers
{
    public class ClientesController : ApiController
    {
        private static List<Cliente> clientList = new List<Cliente>();

        [HttpGet]
        public List<Cliente> GetCliente()
        {
            return clientList;
        }

        [HttpPost]
        public string PostCliente(string nomePar, string telefonePar)
        {
            var ret = string.Empty;

            try
            {
                if (string.IsNullOrEmpty(nomePar) || string.IsNullOrEmpty(telefonePar))
                    ret = "Nome ou Telefone não foram preenchidos.";
                else
                {
                    clientList.Add(new Cliente(nomePar, telefonePar));
                    ret = "Cliente adicionado com sucesso. Nome: "+nomePar+" | Telefone: "+telefonePar;
                }
            }
            catch (Exception ex)
            {
                ret = "Erro ao tentar incluir novo cliente. Mensagem: " + ex.Message;
            }

            return ret;
        }

        [HttpPost]
        public string PostClienteBody([FromBody]Cliente dados)
        {
            var ret = string.Empty;

            try
            {
                if (string.IsNullOrEmpty(dados.Nome) || string.IsNullOrEmpty(dados.Telefone))
                    ret = "Nome ou Telefone não foram preenchidos.";
                else
                {
                    clientList.Add(new Cliente(dados.Nome, dados.Telefone));
                    ret = "Cliente adicionado com sucesso. Nome: " + dados.Nome + " | Telefone: " + dados.Telefone;
                }
            }
            catch (Exception ex)
            {
                ret = "Erro ao tentar incluir novo cliente. Mensagem: " + ex.Message;
            }

            return ret;
        }

        [HttpDelete]
        public string DeleteCliente(string nomePar)
        {
            var ret = string.Empty;

            try
            {
                if (clientList.Count == 0)
                {
                    ret = "Lista de clientes está vazia";
                }
                else
                {
                    //Remove primeiro registro encontrado. Localização por ID da lista
                    //clientList.RemoveAt(clientList.IndexOf(clientList.First(x => x.Nome.Contains(nomePar))));

                    //Remove todos os registros que contenham essa informação
                    clientList.RemoveAll(x => x.Nome.Contains(nomePar));
                    ret = "Clientes com nome [" + nomePar + "] excluídos com sucesso.";

                }
            }
            catch (Exception ex)
            {
                ret = "Erro ao tentar excluir cliente. Mensagem: "+ex.Message;
            }

            return ret;
        }

        [HttpDelete]
        public string DeleteClienteBody([FromBody]string nomePar)
        {
            var ret = string.Empty;

            try
            {
                if (clientList.Count == 0)
                {
                    ret = "Lista de clientes está vazia";
                }
                else
                {
                    //Remove primeiro registro encontrado. Localização por ID da lista
                    //clientList.RemoveAt(clientList.IndexOf(clientList.First(x => x.Nome.Contains(nomePar))));

                    //Remove todos os registros que contenham essa informação
                    clientList.RemoveAll(x => x.Nome.Contains(nomePar));
                    ret = "Clientes com nome [" + nomePar + "] excluídos com sucesso.";

                }
            }
            catch (Exception ex)
            {
                ret = "Erro ao tentar excluir cliente. Mensagem: " + ex.Message;
            }

            return ret;
        }
    }
}
