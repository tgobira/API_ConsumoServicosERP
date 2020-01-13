using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Web;
using System.Xml;

namespace API_ConsumoServicosERP.Models
{
    public class WebServices
    {
        public string LerRegistro(string userName, string password, string dataServer, string parameters, string context)
        {
            var ret = string.Empty;

            wsDataServer.IwsDataServerClient servico = new wsDataServer.IwsDataServerClient();

            servico.ClientCredentials.UserName.UserName = userName;
            servico.ClientCredentials.UserName.Password = password;

            string retorno = string.Empty;

            try
            {
                using (new OperationContextScope(servico.InnerChannel))
                {
                    string auth = "Basic " + Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(servico.ClientCredentials.UserName.UserName + ":" + servico.ClientCredentials.UserName.Password));
                    HttpRequestMessageProperty requestMessage = new HttpRequestMessageProperty();
                    requestMessage.Headers["Authorization"] = auth;
                    OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = requestMessage;
                    retorno = servico.ReadRecord(dataServer, parameters, context);
                }

                XmlTextReader xtr = new XmlTextReader(new System.IO.StringReader(retorno));

                ret = xtr.ToString();
            }
            catch (Exception ex)
            {
                ret = "Erro na chamada de serviço. Mensagem: " + ex.Message;
            }

            return ret;
        }

        public DataSet LerVisao(string userName, string password, string dataServer, string filter, string context)
        {
            var ret = new DataSet();

            wsDataServer.IwsDataServerClient servico = new wsDataServer.IwsDataServerClient();

            servico.ClientCredentials.UserName.UserName = userName.ToString();
            servico.ClientCredentials.UserName.Password = password.ToString();

            string retorno = string.Empty;

            try
            {
                using (new OperationContextScope(servico.InnerChannel))
                {
                    string auth = "Basic " + Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(servico.ClientCredentials.UserName.UserName + ":" + servico.ClientCredentials.UserName.Password));
                    HttpRequestMessageProperty requestMessage = new HttpRequestMessageProperty();
                    requestMessage.Headers["Authorization"] = auth;
                    OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = requestMessage;
                    retorno = servico.ReadView(dataServer, filter, context);
                }

                XmlTextReader xtr = new XmlTextReader(new System.IO.StringReader(retorno));

                //ret = retorno.Substring(0, retorno.LastIndexOf("</GUSUARIO>"));

                ret.ReadXml(xtr);
            }
            catch (Exception ex)
            {
                DataTable dt = new DataTable("MyTable");
                dt.Columns.Add(new DataColumn("id", typeof(int)));
                dt.Columns.Add(new DataColumn("message", typeof(string)));

                DataRow errorLine = dt.NewRow();

                errorLine["id"] = 1;
                errorLine["message"] = "Erro na chamada de serviço. Mensagem: " + ex.Message;
                dt.Rows.Add(errorLine);
                ret.Tables.Add(dt);
            }

            return ret;
        }
    }
}