using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithBrandsAndTypesSpecification : BaseSpecification<Product>
    {
       public ProductsWithBrandsAndTypesSpecification(ProductSpecParams productParams) 
       : base(p => 
       (string.IsNullOrWhiteSpace(productParams.Search) || p.Name.ToLower().Contains(productParams.Search)) && 
       (!productParams.BrandId.HasValue || p.ProductBrandId == productParams.BrandId) && 
       (!productParams.TypeId.HasValue || p.ProductTypeId == productParams.TypeId))
       {
           AddInclude(p => p.ProductBrand);
           AddInclude(p => p.ProductType);

           AddOrderBy(p => p.Name);

           if(!string.IsNullOrWhiteSpace(productParams.SortBy))
           {
               switch (productParams.SortBy)
               {
                    case "priceAsc":
                    AddOrderBy(p => p.Price);
                    break;

                    case "priceDesc":
                    AddOrderByDesc(p => p.Price);
                    break;

                    case "nameDesc" :
                    AddOrderByDesc(p => p.Name);
                    break;

                    default:
                    AddOrderBy(p => p.Name);
                    break;
               }
           }

           ApplyPaging(productParams.PageSize * (productParams.PageIndex - 1), productParams.PageSize);

       }
       public ProductsWithBrandsAndTypesSpecification(int id) : base(p => p.Id == id)
       {
           AddInclude(p => p.ProductBrand);
           AddInclude(p => p.ProductType);
       }
    }
}