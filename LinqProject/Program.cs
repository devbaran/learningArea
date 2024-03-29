﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqProject
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Category> categories = new List<Category>
            {
                new Category{CategoryId=1,CategoryName="Bilgisayar"},
                new Category{CategoryId=2,CategoryName="Telefon"}
            };

            List<Product> products = new List<Product>
            {
                new Product{ProductId=1,CategoryId=1,ProductName="Acer Laptop",QuantityPerUnit="32GB Ram",UnitPrice=10000,UnitsInStock=5 },
                new Product{ProductId=2,CategoryId=1,ProductName="Asus Laptop",QuantityPerUnit="16GB Ram",UnitPrice=8000,UnitsInStock=3 },
                new Product{ProductId=3,CategoryId=1,ProductName="Hp Laptop",QuantityPerUnit="8GB Ram",UnitPrice=6000,UnitsInStock=2 },
                new Product{ProductId=4,CategoryId=2,ProductName="Samsung Telefon",QuantityPerUnit="4GB Ram",UnitPrice=5000,UnitsInStock=15 },
                new Product{ProductId=5,CategoryId=2,ProductName="Apple Telefon",QuantityPerUnit="4GB Ram",UnitPrice=8000,UnitsInStock=0 }

            };
            //Test(products);
            //GetProducts(products);
            //AnyTest(products);
            //FindTest(products);
            //FindAllTest(products);
            //AscDescTest(products);
            //ClassicLinqTest(products);

            var result = from p in products
                         join c in categories //her bir p ile her bir c join edilir, yan yana getirilir, AMA NEYE GÖRE YAN YANA GETİRİLİR ?
      on p.CategoryId equals c.CategoryId  //BUNA GÖRE YAN YANA GETİRİLİR
                         where p.UnitPrice > 5000 //aranan veri
                         orderby p.UnitPrice descending //sıralama
                         select new ProductDto { ProductId = p.ProductId, CategoryName = c.CategoryName, ProductName = p.ProductName, UnitPrice = p.UnitPrice };

            foreach (var productDto in result)
            {
                Console.WriteLine("Ürün adı: {0} \n Kategori: {1}",productDto.ProductName,productDto.CategoryName); //farklı bir kullanım
            }


        }

        private static void ClassicLinqTest(List<Product> products)
        {
            //farklı bir yazım stili ile linq
            var result = from p in products
                         where p.UnitPrice > 1000 && p.UnitsInStock > 0
                         orderby p.UnitPrice descending, p.ProductName ascending
                         select new ProductDto { ProductName = p.ProductName, ProductId = p.ProductId, UnitPrice = p.UnitPrice };

            foreach (var product in result)
            {
                Console.WriteLine(product.ProductName);
            }
        }

        private static void AscDescTest(List<Product> products)
        {
            //Single Line Query- yani tek satır sorgu tipi
            var result = products.Where(p => p.ProductName.Contains("top")).OrderBy(p => p.UnitPrice).ThenByDescending(p => p.ProductName);
            //orderby veya OrderByDescending ile istediğimiz sıralamayı yapabiliriz.
            foreach (var product in result)                                             //thenby ise diğer bir sıralama istiyorsak, önce fiyat sonra fiyatları kendi arasında alfabetik örneğin...
            {
                Console.WriteLine(product.ProductName);
            }
        }

        private static void FindAllTest(List<Product> products)
        {
            //belirlenen şarta uyan bütün elementleri getirir, where gibi çalışır
            var result = products.FindAll(p => p.ProductName.Contains("top")); //contains içeriyorsa demektir. birkaç harf kelime de aratılabilir nokta atışı olmasına gerek yok
            Console.WriteLine(result);
            //where bize IEnumerable bir liste döner her yola çekilebilir, ancak burda sadece liste döner
        }

        private static void FindTest(List<Product> products)
        {
            //aranan kritere uyan nesnenin kendisini getirir.
            //ürün detayına gitmek için kullanılabilir
            //3 banka kredisi var ise bir banka kredisinin detayını görmek için kullanılabilir örneğin...
            var result = products.Find(p => p.ProductId == 3);
            Console.WriteLine(result.ProductName);
        }

        private static void AnyTest(List<Product> products)
        {
            //any ile liste içinde o eleman var mı yok mu arayabiliriz, bool döner: true veya false.
            var result = products.Any(p => p.ProductName == "False Laptop");
            Console.WriteLine(result);
        }

        private static void Test(List<Product> products)
        {
            Console.WriteLine("-- -- -- ALGORITMIK - - - - -");
            foreach (var product in products)
            {
                if (product.UnitPrice > 5000 && product.UnitsInStock > 3)
                {
                    Console.WriteLine(product.ProductName);
                }
            }
            Console.WriteLine("--- --- --- --- --- LİNQ --- --- --- --- ---");

            var result = products.Where(p => p.UnitPrice > 5000 && p.UnitsInStock > 3);
            foreach (var product in result)
            {
                Console.WriteLine(product.ProductName);
            }
        }

        //iki farklı metod yazalım aynı işi yapan
        static List<Product> GetProducts(List<Product> products)
        {
            List<Product> filteredProduct = new List<Product>();

            foreach (var product in products)
            {
                if (product.UnitPrice > 5000 && product.UnitsInStock > 3)
                {
                    filteredProduct.Add(product);
                }
            }
            return filteredProduct;
        }

        static List<Product> GetProductsLinq(List<Product> products)
        {
            return products.Where(p=> p.UnitPrice>5000 && p.UnitsInStock>3).ToList();
        }

        class ProductDto
        {
            public int ProductId { get; set; }
            public string CategoryName { get; set; } //category name başka class'ta join ile çalışmamız gerek
            public string ProductName { get; set; }
            public decimal UnitPrice { get; set; }
        }


    }
    class Product
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string ProductName { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
    }
    class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
