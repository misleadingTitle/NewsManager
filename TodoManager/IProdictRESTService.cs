using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace TodoManager
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IProdictRESTService" in both code and config file together.
    [ServiceContract]
    public interface IProdictRESTService
    {
        [OperationContract]
        [WebInvoke(Method="GET",
            ResponseFormat=WebMessageFormat.Xml,
            BodyStyle=WebMessageBodyStyle.Bare,
            UriTemplate="GetProductList/")]
        List<Product> GetProductList();
    }
}
