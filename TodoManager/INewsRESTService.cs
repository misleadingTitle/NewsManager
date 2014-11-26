using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Shared;

namespace NewsManager
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IProdictRESTService" in both code and config file together.
    [ServiceContract]
    public interface INewsRESTService
    {
        [OperationContract]
        [WebInvoke(Method="GET",
            ResponseFormat=WebMessageFormat.Xml,
            BodyStyle=WebMessageBodyStyle.Bare,
            UriTemplate="GetArticleList/")]
        List<Article> GetArticlesList();

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Xml,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "GetArticleList/{Id}")]
        Article GetArticleById(string Id);

        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat= WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "InsertArticle/{Id}")]
        string InsertArticle(Article article,string Id);

        [OperationContract]
        [WebInvoke(Method = "PUT",
           ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "UpdateArticle/{Id}")]
        string UpdateArticle(Article article, string Id);

        [OperationContract]
        [WebInvoke(Method = "DELETE",
            ResponseFormat = WebMessageFormat.Xml,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "DeleteArticle/{Id}")]
        string DeleteArticle(string Id);
    }
}
