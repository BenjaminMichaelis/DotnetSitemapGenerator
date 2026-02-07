using System;
using Microsoft.AspNetCore.Mvc;
using DotnetSitemapGenerator.Routing;


namespace DotnetSitemapGenerator
{
    /// <inheritdoc/>
    public class SitemapProvider : ISitemapProvider
    {
        private readonly IBaseUrlProvider? baseUrlProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="SitemapProvider"/> class.
        /// </summary>
        public SitemapProvider()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="SitemapProvider"/> class.
        /// </summary>
        public SitemapProvider(IBaseUrlProvider baseUrlProvider)
        {
            this.baseUrlProvider = baseUrlProvider;
        }


        /// <inheritdoc/>
        public ActionResult CreateSitemap(SitemapModel sitemapModel)
        {
            if (sitemapModel == null)
            {
                throw new ArgumentNullException(nameof(sitemapModel));
            }

            return new XmlResult<SitemapModel>(sitemapModel, baseUrlProvider!);
        }

        /// <inheritdoc/>
        public ActionResult CreateSitemap(SitemapModel sitemapModel, string fileLocation)
        {
            if (sitemapModel == null)
            {
                throw new ArgumentNullException(nameof(sitemapModel));
            }

            return new XmlResult<SitemapModel>(sitemapModel, baseUrlProvider!, fileLocation);
        }


        /// <inheritdoc/>
        public ActionResult CreateSitemapIndex(SitemapIndexModel sitemapIndexModel)
        {
            if (sitemapIndexModel == null)
            {
                throw new ArgumentNullException(nameof(sitemapIndexModel));
            }

            return new XmlResult<SitemapIndexModel>(sitemapIndexModel, baseUrlProvider!);
        }
    }
}