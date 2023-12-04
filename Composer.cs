using Examine;
using Examine.Lucene;
using ExamineDupes.Indexing;
using ExamineDupes.Services;
using Microsoft.Extensions.Options;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Infrastructure.Examine;

namespace ExamineDupes;

public class Composer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        builder.Services.AddTransient<IProductService, ProductService>();
        builder.Services.AddExamineLuceneIndex<ProductIndex, ConfigurationEnabledDirectoryFactory>(IndexingConstants.IndexName);
        builder.Services.AddSingleton<IConfigureOptions<LuceneDirectoryIndexOptions>, ProductIndexOptions>();
        builder.Services.AddSingleton<ProductIndexValueSetBuilder>();
        builder.Services.AddTransient<IIndexPopulator, ProductIndexPopulator>();
    }
}