using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Swashbuckle.Application;

namespace DrawPub.ImageService
{

    /// <summary>
    /// Swagger configuration
    /// </summary>
    public class SwaggerConfig
    {

        public static void Register(HttpConfiguration configuration)
        {


            var serviceVersion = string.Empty;

          
            configuration.EnableSwagger(
                c =>
                {
                    c.SingleApiVersion(string.IsNullOrEmpty(serviceVersion) ? "1" : serviceVersion, "Concept Search API")

                     .Description("Save drawing and share on social medias")
                     .Contact(cc => cc
                                  .Name("DrawPub Team")
                                  .Email("Amir.Akhoundpour@gmail.com"))
                     .License(lc => lc
                                  .Name("DrawPub License"));



                    c.RootUrl(req => new Uri(req.RequestUri, req.GetRequestContext().VirtualPathRoot).ToString());
                    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                    c.IgnoreObsoleteActions();
                    c.IncludeXmlComments(GetXmlCommentsPath());


                })
                .EnableSwaggerUi(
                    c =>
                    {
                        c.EnableDiscoveryUrlSelector();
                        c.DocExpansion(DocExpansion.None);
                        c.EnableOAuth2Support("client_id", "realm", "Swagger UI");

                    });
        }

        /// <summary>
        /// The get xml comments path.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private static string GetXmlCommentsPath()
        {
            return string.Format(@"{0}\bin\DrawPub.ImageService.XML", AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}