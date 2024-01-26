namespace Shopfy.Models.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProduct();
        Product? GetProductById(Guid id);
        Product? GetProductByName(string name);
        
        Product UpdateProduct(Product product);
        void DeleteProduct(Product product);
        Product CreateProduct(Product product);


    }
}
