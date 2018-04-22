using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BasicWebAPI1.Controllers;
using BasicWebAPI1.Models;
using System.Collections.Generic;

namespace BasicWebAPI1.Test
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// Test Initialization
        /// </summary>
        ProductsController controller;
        Product mazda = new Product() { Id = 6, Name = "Mazda 2", Description = "Small compact model" };
        Product test = new Product() { Id = 6, Name = "test produce", Description = "I created this to do test on" };

        [TestInitialize]
        public void TestInitialize()
        {
            controller = new ProductsController();            
        }

        /// <summary
        /// Gets list of products
        /// </summary
        [TestMethod]
        [TestCategory ("Unit")]
        public void GetProductReturnsProducts()
        {
            //Convert to list so we can count products returned
            List<Product> productList = (List<Product>)controller.GetProducts();
            //Assert we have 5 products
            Assert.IsTrue(productList.Count == 5, "Should have 5 products returned.");
        }

        /// <summary
        /// Gets products by id passed and confirms correct product was returned
        /// </summary
        [TestMethod]
        [TestCategory("Unit")]
        public void GetProductByIdReturnsCorrectProduct()
        {
            int id = 1;
            var hondaCivic = controller.GetProductById(id);

            Assert.IsTrue(hondaCivic.Id == 1, "Id returned should be 1");
            Assert.IsTrue(hondaCivic.Name == "Honda Civic", "Product with ID of 1 should be Honda Civic");
            Assert.IsTrue(hondaCivic.Description == "Luxury Model 2013", "Description should be \"Luxury Model 2013\"");
        }

        /// <summary
        /// Searches for product based on description and verifies correct product was returned
        /// </summary
        [TestMethod]
        [TestCategory("Unit")]
        public void GetProductBySearchReturnsCorrectProduct()
        {
            string searchText = "Luxury Model 2013";
            var productList = controller.GetProductsBySearch(searchText);

            try
            {
                //Probably not the most efficient way of doing this, but its an enumerable so I needed a way to iterate through. 
                //This does however introduce a scenario where the asserts could never be hit. Hence I wrapped it in an try catch
                foreach (Product product in productList)
                {
                    Assert.IsTrue(product.Name == "Honda Civic", "Incorrect car model returned");
                    Assert.IsTrue(product.Id == 1, "Incorrect car model returned");
                }
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }

        /// <summary
        /// Adds product to the list
        /// </summary
        [TestMethod]
        [TestCategory("Unit")]
        public void PostProductAddsProductToProducts()
        {
            var newProduct = controller.PostProduct(mazda);
            List<Product> productList = (List<Product>)controller.GetProducts();

            Assert.IsTrue(productList.Count == 6, "We should now have 6 products in our product list");
        }

        /// <summary
        /// Update product and verify the updated description and name is correct
        /// </summary
        [TestMethod]
        [TestCategory("Unit")]
        public void PutProductUpdatesProduct()
        {
            string newName = "Mazda 3";
            string newDescription = "Similar to Mazda 2 but with more features";
            mazda.Name = newName;
            mazda.Description = newDescription;
            var updateMazda = controller.PutProduct(mazda);
            var updatedMazda = controller.GetProductById(mazda.Id);

            Assert.IsTrue(updatedMazda.Id == 6, "Id returned should be 6");
            Assert.IsTrue(updatedMazda.Name == newName, "Id returned should be 6");
            Assert.IsTrue(updatedMazda.Description == newDescription, "Id returned should be 6");

        }

        /// <summary
        /// Delete the previously added product
        /// </summary
        [TestMethod]
        [TestCategory("Unit")]
        public void DeleteProductDeletsProduct()
        {
            int id = 6;
            var deletedProduct = controller.DeleteProduct(id);
            List<Product> productList = (List<Product>)controller.GetProducts();

            Assert.IsTrue(productList.Count == 5, "Should have 5 products returned.");
        }

        /// <summary
        /// Adds product to the list
        /// </summary
        [TestMethod]
        [TestCategory("Unit")]
        public void GetProductById()
        {
            int idNum = 7;

            List<Product> productList = (List<Product>)controller.GetProducts();
            try
            {
                bool foundId = false;
                foreach (Product product in productList)
                {
                    if (product.Id == idNum)
                    {
                        Assert.Fail();
                        foundId = true;
                    }
                }
                Assert.IsTrue(foundId == false);
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }
        //Matthew Brady
        /// <summary
        /// GetProducts returns correct number of products when more than one are added (do a post first)
        /// </summary
        [TestMethod]
        [TestCategory("Unit")]
        public void PostProductAddsProductToProducts2()
        {
            var newProduct = controller.PostProduct(mazda);
            var newProduct2 = controller.PostProduct(test);
            List<Product> productList = (List<Product>)controller.GetProducts();

            Assert.IsTrue(productList.Count == 7, "We should now have 6 products in our product list");
        }
        //Matthew Brady
        /// <summary
        /// PutProduct updates newly created product.
        /// </summary
        [TestMethod]
        [TestCategory("Unit")]
        public void PutProductUpdatesNewProduct()
        {
            string newName = null;
            string newDescription = null;
            test.Name = newName;
            test.Description = newDescription;
            var updatetest = controller.PutProduct(test);
            var updatedtest = controller.GetProductById(test.Id);

            Assert.IsTrue(updatedtest.Id == 7, "Id returned should be 7");
            Assert.IsTrue(updatedtest.Name == newName, "Id returned should be 7");
            Assert.IsTrue(updatedtest.Description == newDescription, "Id returned should be 7");

        }
        //Matthew Brady
        /// <summary
        /// DeleteProduct Does not delete a product if a name or description is passed in
        /// </summary
        [TestMethod]
        [TestCategory("Unit")]
        public void DeletePoductIfNoNameDescription()
        {
            List<Product> productList = (List<Product>)controller.GetProducts();
            int startCount = productList.Count;
            try
            {
                foreach (Product p in productList)
                {
                    if ((p.Name == null) || (p.Description == null))
                    {
                        var deletedProduct = controller.DeleteProduct(p.Id);
                    }
                }

                Assert.IsTrue(productList.Count == 5, "Should have 5 products returned.");
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }
    }
    
}
