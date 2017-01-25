
namespace SecurityFixModule
{


    public class CustomHttpHeaderModule : System.Web.IHttpModule
    {
    	
        protected static System.Text.RegularExpressions.Regex m_bannedUserAgentsRegex = null;
        protected static string m_strRedirectURL = null;
        
        
        public CustomHttpHeaderModule()
        {
            string regex = @"(libwww-perl|libwww|libcurl|urllib|PYcurl|Java/|PHP|PECL|Snoopy|nmap|nikto|paros|WPscan|synapse|SQLmap|wGet|msnbot|Purebot|Lipperhey|MaMa CaSpEr|Mail.Ru|gold crawler|Borg|MSIE 3.|MSIE 4.|MSIE 5.|MSIE 6.|MSIE 7.|MSIE 8.)";
            
            if (regex != null && regex != "")
                m_bannedUserAgentsRegex = new System.Text.RegularExpressions.Regex(regex, System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Compiled);
        }
        
        
        public void Dispose()
        {
            // CustomHttpHeaders.CustomHttpHeaderModule mod = new CustomHttpHeaderModule();
        }


        public void Init(System.Web.HttpApplication context)
        {
            // context.BeginRequest += new System.EventHandler(context_BeginRequest);
            context.EndRequest += new System.EventHandler(context_EndRequest);
            context.PreRequestHandlerExecute += new System.EventHandler(RedirectMatchedUserAgents);
        }
        
        
        
        protected void RedirectMatchedUserAgents(object sender, System.EventArgs e)
        {
            System.Web.HttpApplication app = sender as System.Web.HttpApplication;

            if (m_bannedUserAgentsRegex != null && app != null && app.Request != null && (app.Request.UserAgent != null && app.Request.UserAgent != ""))
            {

                if (m_bannedUserAgentsRegex.Match(app.Request.UserAgent).Success)
                {

                    if (m_strRedirectURL == null)
                    {
                        //var cbAppContextBase = new HttpContextWrapper(app.Context);
                        //string strRedirectURL = System.Web.Mvc.UrlHelper.GenerateContentUrl("~/Ban/UserAgentBanned", cbAppContextBase);

                        // m_strRedirectURL = uh.Action("UserAgentBanned", "Ban");
                        
                    } // End if (m_strRedirectURL == null)
                    
                    app.CompleteRequest();
                    
                    //if (!System.StringComparer.OrdinalIgnoreCase.Equals(m_strRedirectURL, app.Request.Url.LocalPath))
                    //{
                    //    app.Response.Redirect(m_strRedirectURL);
                    //} // End if (!StringComparer.OrdinalIgnoreCase.Equals(m_strRedirectURL, app.Request.Url.LocalPath))
                	
            	} // End if (m_bannedUserAgentsRegex.Match(app.Request.UserAgent).Success)
                
            } // End if (_bannedUserAgentsRegex != null && app != null && app.Request != null && !String.IsNullOrEmpty(app.Request.UserAgent))
            
        } // End Sub RedirectMatchedUserAgents
        
        void context_BeginRequest(object sender, System.EventArgs e)
        {
            if (System.Web.HttpContext.Current != null && System.Web.HttpContext.Current.Request != null)
            {
                System.Web.HttpRequest request = System.Web.HttpContext.Current.Request;
                // request.Headers.Add("name", "value");
            } // End if (System.Web.HttpContext.Current != null && System.Web.HttpContext.Current.Response != null)
        }


        void context_EndRequest(object sender, System.EventArgs e)
        {
            if (System.Web.HttpContext.Current != null && System.Web.HttpContext.Current.Response != null)
            {
                System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;

                try
                {

                    // response.Headers["P3P"] = "CP=\\\"IDC DSP COR ADM DEVi TAIi PSA PSD IVAi IVDi CONi HIS OUR IND CNT\\\"":
                    // response.Headers.Set("P3P", "CP=\\\"IDC DSP COR ADM DEVi TAIi PSA PSD IVAi IVDi CONi HIS OUR IND CNT\\\"");
                    // response.AddHeader("P3P", "CP=\\\"IDC DSP COR ADM DEVi TAIi PSA PSD IVAi IVDi CONi HIS OUR IND CNT\\\"");
                    if (string.IsNullOrEmpty(response.Headers["P3P"]))
                        response.AppendHeader("P3P", "CP=\\\"IDC DSP COR ADM DEVi TAIi PSA PSD IVAi IVDi CONi HIS OUR IND CNT\\\"");

                    

                    if(string.IsNullOrEmpty(response.Headers["X-Frame-Options"]))
                        response.AppendHeader("X-Frame-Options", "SAMEORIGIN");
                        // response.AppendHeader("X-Frame-Options", "DENY");
                        // response.AppendHeader("X-Frame-Options", "AllowAll");



                    // https://gist.github.com/cemerson/3906944
                    if (string.IsNullOrEmpty(response.Headers["cache-control"]))
                        response.AppendHeader("cache-control", "no-cache, no-store, must-revalidate, private, max-age=0");

                    // response.AppendHeader("expires", "Tue, 01 Jan 1980 1:00:00 GMT, 0");
                    if (string.IsNullOrEmpty(response.Headers["expires"]))
                        response.AppendHeader("expires", "0");

                    if (string.IsNullOrEmpty(response.Headers["pragma"]))
                        response.AppendHeader("pragma", "no-cache");




                    if (string.IsNullOrEmpty(response.Headers["X-Content-Type-Options"]))
                        response.AppendHeader("X-Content-Type-Options", "nosniff");

                    if (string.IsNullOrEmpty(response.Headers["X-XSS-Protection"]))
                        response.AppendHeader("X-XSS-Protection", "1; mode=block");

                    // Caution - IIS explodes with SAML/shibboleth on if you add this line to customHeaders in web.config->system.webServer...
                    // Thus, append strict-transport-security here - DO NOT ADD TO web.config - IIS explodes with SAML/shibboleth on...
                    // if(false) response.AppendHeader("Strict-Transport-Security", "max-age=86400; includeSubDomains; preload");

                    // response.AppendHeader("Options", "SAMEORIGIN");
                }
                catch (System.Exception ex)
                {
                    // WTF ? 
                    System.Console.WriteLine(ex.Message); // Suppress warning
                }
                
            } // End if (System.Web.HttpContext.Current != null && System.Web.HttpContext.Current.Response != null)

        } // End Using context_EndRequest


    } // End Class myHTTPHeaderModule


} // End Namespace CustomHttpHeaders
