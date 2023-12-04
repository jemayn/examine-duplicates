using Examine;
using Umbraco.Cms.Infrastructure.Examine;

namespace ExamineDupes.Indexing;

public class ProductIndexValueSetBuilder : IValueSetBuilder<Product>
{
    public IEnumerable<ValueSet> GetValueSets(params Product[] products)
    {
        foreach (var product in products)
        {
            var indexValues = new Dictionary<string, object>
            {
                [UmbracoExamineFieldNames.NodeNameFieldName] = product.Name,
                ["id"] = product.Id
            };

            yield return new ValueSet(
                product.Id.ToString(),
                IndexingConstants.IndexType,
                IndexingConstants.IndexType,
                indexValues);
        }
    }
}