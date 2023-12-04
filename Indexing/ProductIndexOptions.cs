using Examine;
using Examine.Lucene;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Index;
using Lucene.Net.Util;
using Microsoft.Extensions.Options;
using Umbraco.Cms.Core.Configuration.Models;
using Umbraco.Cms.Infrastructure.Examine;

namespace ExamineDupes.Indexing;

public class ProductIndexOptions : IConfigureNamedOptions<LuceneDirectoryIndexOptions>
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IOptions<IndexCreatorSettings> _settings;

    public ProductIndexOptions(IServiceProvider serviceProvider, IOptions<IndexCreatorSettings> settings)
    {
        _serviceProvider = serviceProvider;
        _settings = settings;
    }

    public void Configure(string? name, LuceneDirectoryIndexOptions options)
    {
        if (name?.Equals(IndexingConstants.IndexName) is false)
        {
            return;
        }

        options.Analyzer = new StandardAnalyzer(LuceneVersion.LUCENE_48);

        options.FieldDefinitions = new FieldDefinitionCollection(
            new FieldDefinition(UmbracoExamineFieldNames.ItemIdFieldName, FieldDefinitionTypes.Integer),
            new FieldDefinition(UmbracoExamineFieldNames.NodeNameFieldName, FieldDefinitionTypes.FullText),
            new FieldDefinition("id", FieldDefinitionTypes.Integer)
        );

        options.UnlockIndex = true;
        
        if (_settings.Value.LuceneDirectoryFactory == LuceneDirectoryFactory.SyncedTempFileSystemDirectoryFactory)
        {
            // if this directory factory is enabled then a snapshot deletion policy is required
            options.IndexDeletionPolicy = new SnapshotDeletionPolicy(new KeepOnlyLastCommitDeletionPolicy());
            options.DirectoryFactory = _serviceProvider.GetRequiredService<global::Examine.Lucene.Directories.SyncedFileSystemDirectoryFactory>();
        }
        else
        {
            options.DirectoryFactory = _serviceProvider.GetRequiredService<global::Examine.Lucene.Directories.TempEnvFileSystemDirectoryFactory>();
        }
    }

    // not used
    public void Configure(LuceneDirectoryIndexOptions options) => throw new NotImplementedException();
}