using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithBrandsAndTypesSpecification : BaseSpecification<Product>
    {
       public ProductsWithBrandsAndTypesSpecification() : base()
       {
           AddInclude(p => p.ProductBrand);
           AddInclude(p => p.ProductType);
       }
       public ProductsWithBrandsAndTypesSpecification(int id) : base(p => p.Id == id)
       {
           AddInclude(p => p.ProductBrand);
           AddInclude(p => p.ProductType);
       }
    }
}