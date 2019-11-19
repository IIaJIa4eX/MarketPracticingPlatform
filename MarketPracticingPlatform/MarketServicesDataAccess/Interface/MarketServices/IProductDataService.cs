using MarketPracticingPlatform.Data.DataBaseModels;
using MarketPracticingPlatform.Service.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarketPracticingPlatform.Service.Interface
{
    public interface IProductDataService
    {
        ProductRespondDTO EditProduct(ProductDTO prdDTO);

        ProductRespondDTO CreatProduct(ProductDTO prdDTO);

        Product GetProductByID(int productId);

        List<Product> GetProductsByMainProductId(int mainPrdId, int numberOfProducts);

        List<Product> GetRelativeProductsByProductId(int productId, int numberOfProducts);

        void DeleteProduct(int productId);

        ProductDTO GetProductInfoForEdit(int productId);
    }
}
