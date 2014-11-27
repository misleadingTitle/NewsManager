using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SecurityModule
{
    class SWTModule : IHttpModule
    {
        string serviceNamespace = "restfulproject";
        string acsHostName = "accesscontrol.windows.net";
        string trustedTokenPolicyKey = "h8hyCWwzKHgJpbQff2sKJ2thQu1MdsgMUTSnMGwWCao=";
        string trustedAudience = @"http://k31:57614/NewsRESTService.svc";

        void IHttpModule.Dispose() { }

        void IHttpModule.Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(context_BeginRequest);
        }

        void context_BeginRequest(object sender, EventArgs e)
        {
            //HNDLE SWT TOKEN VALIDATION
            //GET the authorization header
            if (true)
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
                        this.acsHostName,
                        this.serviceNamespace,
                        this.trustedAudience,
                        this.trustedTokenPolicyKey);

                    if (!validator.Validate(token))
                    {
                        throw new ApplicationException("SWTModule: unauthorized");
                    }
                }
                catch (ApplicationException ex)
                {
                    ((HttpApplication)sender).Response.Status = "403 Forbidden";
                    ((HttpApplication)sender).Response.StatusCode = 403;
                    ((HttpApplication)sender).Response.StatusDescription = "Forbidden";
                }
            }
        }



    }
}
