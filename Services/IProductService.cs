using ExamineDupes.Indexing;

namespace ExamineDupes.Services;

public interface IProductService
{
    IEnumerable<Product> GetProductsPaged();
}

public class ProductService : IProductService
{
    public IEnumerable<Product> GetProductsPaged()
    {
        var products = new List<Product>();

        for (int i = 1; i < 5; i++)
        {
            products.Add(new Product()
            {
                Id = i,
                Name = "Name " + i
            });
        }

        return products;
    }
}