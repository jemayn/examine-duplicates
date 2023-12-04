using Examine;
using Examine.Lucene;
using Microsoft.Extensions.Options;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Infrastructure.Examine;
using IHostingEnvironment = Umbraco.Cms.Core.Hosting.IHostingEnvironment;

namespace ExamineDupes.Indexing;

public class ProductIndex : UmbracoExamineIndex, IUmbracoContentIndex
{
    public ProductIndex(
        ILoggerFactory loggerFactory,
        string name,
        IOptionsMonitor<LuceneDirectoryIndexOptions> indexOptions,
        IHostingEnvironment hostingEnvironment,
        IRuntimeState runtimeState)
        : base(loggerFactory,
            name,
            indexOptions,
            hostingEnvironment,
            runtimeState)
    {
    }
    
    void IIndex.IndexItems(IEnumerable<ValueSet> values)
    {
        PerformIndexItems(values.Where(x => x.ItemType == IndexingConstants.IndexType), OnIndexOperationComplete);
    }
}
