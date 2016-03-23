using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace First_Last_List.Tests
{
    [TestClass]
    public class PerformanceTestsFirstLastList
    {
        private IFirstLastList<Product> products = 
            FirstLastListFactory.Create<Product>();

        private int addCounter = 0;

        private void AddProducts(int count)
        {
            for (int i = 0; i < count; i++)
            {
                ++this.addCounter;
                this.products.Add(
                    new Product(this.addCounter % 1000, "Product" + this.addCounter));
            }
        }

        [TestMethod]
        [Timeout(200)]
        public void TestPerformance_Add()
        {
            // Act
            this.AddProducts(25000);
            Assert.AreEqual(25000, this.products.Count);
        }

        [TestMethod]
        [Timeout(200)]
        public void TestPerformance_First()
        {
            // Arrange
            this.AddProducts(10000);

            // Act
            for (int i = 0; i < 500; i++)
            {
                this.AddProducts(1);
                var returnedProducts = this.products.First(i).ToList();
                Assert.AreEqual(i, returnedProducts.Count);
            }
        }

        [TestMethod]
        [Timeout(200)]
        public void TestPerformance_Last()
        {
            // Arrange
            this.AddProducts(10000);

            // Act
            for (int i = 0; i < 500; i++)
            {
                this.AddProducts(1);
                var returnedProducts = this.products.Last(i).ToList();
                Assert.AreEqual(i, returnedProducts.Count);
            }
        }

        [TestMethod]
        [Timeout(200)]
        public void TestPerformance_Min()
        {
            // Arrange
            this.AddProducts(10000);

            // Act
            for (int i = 0; i < 230; i++)
            {
                this.AddProducts(1);
                var returnedProducts = this.products.Min(i).ToList();
                Assert.AreEqual(i, returnedProducts.Count);
            }
        }

        [TestMethod]
        [Timeout(200)]
        public void TestPerformance_Max()
        {
            // Arrange
            this.AddProducts(10000);

            // Act
            for (int i = 0; i < 230; i++)
            {
                this.AddProducts(1);
                var returnedProducts = this.products.Max(i).ToList();
                Assert.AreEqual(i, returnedProducts.Count);
            }        
        }

        [TestMethod]
        [Timeout(200)]
        public void TestPerformance_RemoveAll()
        {
            // Arrange
            this.AddProducts(12000);

            // Act
            while (this.products.Count > 0)
            {
                this.AddProducts(1);
                var first = this.products.First(1).FirstOrDefault();
                this.products.RemoveAll(first);
            }
        }
    }
}
