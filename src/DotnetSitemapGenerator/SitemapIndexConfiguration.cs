using System.Collections.Generic;
using System.Linq;
using DotnetSitemapGenerator.StyleSheets;

namespace DotnetSitemapGenerator
{
    /// <inheritdoc/>
    public abstract class SitemapIndexConfiguration<T> : ISitemapIndexConfiguration<T>
    {
        /// <summary>
        /// Base constructor for the abstract SitemapIndexConfiguration class.
        /// </summary>
        /// <param name="dataSource">Data source that will be queried for paging.</param>
        /// <param name="currentPage">Current page for paged sitemap items.</param>
        protected SitemapIndexConfiguration(IQueryable<T> dataSource, int? currentPage)
        {
            DataSource = dataSource;
            CurrentPage = currentPage;
            Size = 50000;
        }

        /// <inheritdoc/>
        public IQueryable<T> DataSource { get; }

        /// <inheritdoc/>
        public int? CurrentPage { get; }

        /// <inheritdoc/>
        public int Size { get; protected set; }

        /// <inheritdoc/>
        public abstract SitemapIndexNode CreateSitemapIndexNode(int currentPage);

        /// <inheritdoc/>
        public abstract SitemapNode CreateNode(T source);

        /// <inheritdoc/>
        public List<XmlStyleSheet>? SitemapStyleSheets { get; protected set; }

        /// <inheritdoc/>
        public List<XmlStyleSheet>? SitemapIndexStyleSheets { get; protected set; }

        /// <inheritdoc/>
        public bool UseReverseOrderingForSitemapIndexNodes { get; protected set; }
    }
}