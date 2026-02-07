using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DotnetSitemapGenerator.Routing;
using DotnetSitemapGenerator.Serialization;


namespace DotnetSitemapGenerator
{
    class XmlResult<T> : ActionResult
    {
        private readonly IBaseUrlProvider? baseUrlProvider;
        private readonly T data;
        private readonly IUrlValidator urlValidator;
        private string? FilePath { get; }
        private bool Readable { get; } = false;


        internal XmlResult(T data, IUrlValidator urlValidator)
        {
            this.data = data;
            this.urlValidator = urlValidator;
        }

        internal XmlResult(T data, IBaseUrlProvider baseUrlProvider) : this(data, new UrlValidator(new ReflectionHelper()))
        {
            this.baseUrlProvider = baseUrlProvider;
        }

        internal XmlResult(T data, IBaseUrlProvider baseUrlProvider, string fileLocation, bool readable = false) : this(data, new UrlValidator(new ReflectionHelper()))
        {
            this.baseUrlProvider = baseUrlProvider;
            FilePath = fileLocation;
            Readable = readable;
        }


        public override async Task ExecuteResultAsync(ActionContext context)
        {
            if (data is not null)
            {
                urlValidator.ValidateUrls(data, baseUrlProvider ?? new BaseUrlProvider(context.HttpContext.Request));
            }

            var response = context.HttpContext.Response;
            response.ContentType = "application/xml";
            if (string.IsNullOrEmpty(FilePath)) await response.WriteAsync(new XmlSerializer().Serialize(data), Encoding.UTF8);
            else await response.WriteAsync(new XmlSerializer().Serialize(data, FilePath, Readable), Encoding.UTF8);

            await base.ExecuteResultAsync(context);
        }
    }
}