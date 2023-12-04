using Examine;
using ExamineDupes.Indexing;
using Umbraco.Cms.Web.Common.Controllers;

namespace ExamineDupes.Controllers;

public class TestController : UmbracoApiController
{
    private readonly IExamineManager _examineManager;
    private readonly ProductIndexValueSetBuilder _productIndexValueSetBuilder;

    public TestController(IExamineManager examineManager, ProductIndexValueSetBuilder productIndexValueSetBuilder)
    {
        _examineManager = examineManager;
        _productIndexValueSetBuilder = productIndexValueSetBuilder;
    }

    public void UpdateValues()
    {
        if (!_examineManager.TryGetIndex(IndexingConstants.IndexName, out var index))
        {
            return;
        }

        var product = new Product
        {
            Id = 1,
            Name = "Name with update 1"
        };
        
        index.IndexItems(_productIndexValueSetBuilder.GetValueSets(product));
    }
    
}