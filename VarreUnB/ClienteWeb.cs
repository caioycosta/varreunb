using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Security;

namespace VarreUnB
{
    public class ClienteWeb : WebClient
    {
        protected override WebRequest GetWebRequest(Uri address)
        {


            var r =  base.GetWebRequest(address);


            r.Timeout = 180000;
            //ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;

            //if (r is HttpWebRequest)
            //((HttpWebRequest)r).ProtocolVersion = HttpVersion.Version10;

            ////Change SSL checks so that all checks pass            
            ServicePointManager.ServerCertificateValidationCallback =
                new RemoteCertificateValidationCallback(
                    delegate
                    { return true; }
                );


            return r;
        }
    }
}
