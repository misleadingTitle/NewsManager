using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Web;

namespace SecurityModule
{
    class SWTModule : IHttpModule
    {
        void IHttpModule.Dispose() { }

        void IHttpModule.Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(context_BeginRequest);
        }

        void context_BeginRequest(object sender, EventArgs e)
        {
            HttpContext.Current.User = new GenericPrincipal(new GenericIdentity("prova"),new string[]{"user"});
            //HNDLE SWT TOKEN VALIDATION
            //GET the authorization header
            if (System.Configuration.ConfigurationManager.AppSettings["token"].ToString()=="1")
            {
                try
                {
                    string headerValue = HttpContext.Current.Request.Headers.Get("Authorization");

                    //check the value is present
                    if (string.IsNullOrEmpty(headerValue))
                    {
                        throw new ApplicationException("SWTModule: unauthorized");
                    }

                    //must start with 'wrap'
                    if (!headerValue.StartsWith("WRAP "))
                    {
                        throw new ApplicationException("SWTModule: unauthorized");
                    }

                    string[] nameValuePair = headerValue.Substring("WRAP ".Length).Split(new char[] { '=' }, 2);
                    if (nameValuePair.Length != 2 ||
                        nameValuePair[0] != "access_token" ||
                        !nameValuePair[1].StartsWith("\"") ||
                        !nameValuePair[1].EndsWith("\""))
                    {
                        throw new ApplicationException("SWTModule: unauthorized");
                    }

                    //trim the double quotes
                    string token = nameValuePair[1].Substring(1, nameValuePair[1].Length - 2);

                    //create a token validator
                    TokenValidator validator = new TokenValidator(
                        AccessData.acsHostName,
                        AccessData.serviceNamespace,
                        AccessData.trustedAudience,
                        AccessData.trustedTokenPolicyKey);

                    if (!validator.Validate(token))
                    {
                        throw new ApplicationException("SWTModule: unauthorized");
                    }
                }
                catch (ApplicationException ex)
                {
                    ((HttpApplication)sender).Response.Status = "403 Forbidden";
                }
            }
        }



    }
}
