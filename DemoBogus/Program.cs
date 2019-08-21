using System;
using Bogus;
using DemoBogus.Models;

namespace DemoBogus
{
    class Program
    {
        public enum StatusEnum
        {
            InStock,
            Sold
        }

        private static void GenerateCategory()
        {
            var category = new Faker<Category>()
                .RuleFor(p => p.Id, p => p.Random.Guid())
                .RuleFor(p => p.Name, p => p.Commerce.Department())
                .Generate();

            Console.WriteLine(JsonUtils.Serialize(category));
        }

        public static void CreateVendor()
        {
            var vendor = new Faker<Vendor>("fr")
                .RuleFor(p => p.Id, p => p.Random.Guid())
                .RuleFor(p => p.Name, p => p.Company.CompanyName())
                .GenerateLazy(3);

            Console.WriteLine(JsonUtils.Serialize(vendor));
        }

        public static void CreateVendorWithConstructor()
        {
            var vendor = new Faker<Vendor>("fr")
                .CustomInstantiator(p => new Vendor(Guid.NewGuid(), p.Company.CompanyName()))
                .GenerateLazy(3);

            Console.WriteLine(JsonUtils.Serialize(vendor));
        }

        public static void CreateProduct()
        {
            var product = new Faker<Product>()
                .RuleFor(p => p.Id, Guid.NewGuid())
                .RuleFor(p => p.Name, p => p.Commerce.Product())
                .RuleFor(p => p.Image, p => p.Image.PicsumUrl(250, 150))
                .RuleFor(p => p.Price, p => p.Commerce.Price(100, 1000, 2, "U$ "))
                .RuleFor(p => p.Stock, p => p.Random.Int(1, 10))
                .RuleFor(p => p.Tags, p => p.Commerce.Categories(3))
                .RuleFor(p => p.Status, p => p.PickRandom<StatusEnum>().ToString())
                .RuleFor(p => p.Category, p => new Category(Guid.NewGuid(), p.Commerce.Department()))
                .RuleFor(p => p.Vendor, p => new Vendor(Guid.NewGuid(), p.Company.CompanyName()))
                .Generate();

            Console.WriteLine(JsonUtils.Serialize(product));
        }


        static void Main(string[] args)
        {
            //GenerateCategory();

            //CreateVendor();

            //CreateVendorWithConstructor();

            CreateProduct();

            Console.ReadKey();
        }
    }
}
