using System;

namespace Fullstack.NET.Services.Products
{
    public class ProductModel
    {
        public readonly Guid Id;
        public readonly string Name;

        public ProductModel(Guid id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }
}