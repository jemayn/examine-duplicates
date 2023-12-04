using Examine;
using ExamineDupes.Services;
using Umbraco.Cms.Infrastructure.Examine;

namespace ExamineDupes.Indexing;

public class ProductIndexPopulator : IndexPopulator
{
    private readonly ProductIndexValueSetBuilder _productIndexValueSetBuilder;
    private readonly IProductService _productService;

    public ProductIndexPopulator(
        ProductIndexValueSetBuilder productIndexValueSetBuilder, 
        IProductService productService)
    {
        _productIndexValueSetBuilder = productIndexValueSetBuilder;
        _productService = productService;
        RegisterIndex(IndexingConstants.IndexName);
    }

    protected override void PopulateIndexes(IReadOnlyList<IIndex> indexes)
    {
        foreach (var index in indexes)
        {
            if (index.Name is not IndexingConstants.IndexName)
            {
                continue;
            }
            
            var productResult = _productService.GetProductsPaged();
            
            index.IndexItems(_productIndexValueSetBuilder.GetValueSets(productResult.ToArray()));
        }
    }
}