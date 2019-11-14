using MarketPracticingPlatform.Data.DataBaseModels;
using MarketPracticingPlatform.Service.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarketPracticingPlatform.Service.Interface
{
    public interface IProductDataService
    {
        ProductEditDTO EditProduct(ProductDTO prdDTO);

        ProductCreationDTO CreatProduct(ProductDTO prdDTO);

        Product GetProductByID(int id);

        List<Product> GetProductsByMainProductId(int mainPrdId, int productsNumber);

        void DeleteProduct(int productId);
    }
}
