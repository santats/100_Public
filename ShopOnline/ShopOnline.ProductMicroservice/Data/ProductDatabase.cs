using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace ShopOnline.ProductMicroservice.Data
{
    public class ProductDatabase
    {
        public List<Entities.Product> Products;

        private ProductDatabase() { }
        private static string _dataFilePath = Environment.CurrentDirectory + @"\Data\ProductDatabase.xml";

        public static ProductDatabase Initialize()
        {
            ProductDatabase _productDatabase;
            var serializer = new XmlSerializer(typeof(ProductDatabase));

            if (!File.Exists(_dataFilePath))
            {
                throw new Exception("Could not find Product Database file!");
            }

            using (StreamReader reader = new StreamReader(_dataFilePath))
            {
                _productDatabase = (ProductDatabase)serializer.Deserialize(reader);
            }

            return _productDatabase;
        }

        internal async Task<Entities.Product> GetProductByDisplayName(string displayName)
        {
            return Products.Where(a => a.DisplayName == displayName).FirstOrDefault();
        }

        public Task<List<Entities.Product>> GetAllProducts()
        {
            return Task.FromResult(Products);
        }

        public async Task AddProduct(Entities.Product product)
        {
            product.Id = Guid.NewGuid();

            Products.Add(product);
        }

        internal async Task<Entities.Product> GetProductById(Guid id)
        {
            return Products.Where(a => a.Id == id).FirstOrDefault();
        }

        public Task SaveChanges()
        {
            var serializer = new XmlSerializer(typeof(ProductDatabase));

            using (XmlWriter writer = XmlWriter.Create(_dataFilePath, new XmlWriterSettings() { Indent = true }))
            {
                serializer.Serialize(writer, this);
            }

            return Task.CompletedTask;
        }

        internal void DeleteProduct(Entities.Product product)
        {
            Products.Remove(product);
        }
    }
}
