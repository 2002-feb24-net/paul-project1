using System;
using Xunit;
using PaulsUsedGoods.Domain.Logic;
using PaulsUsedGoods.Domain.Model;

namespace PaulsUsedGoods.Test
{
    public class DomainTests
    {
// ! LOGIC
        [Theory]
        [InlineData ("hello")]
        [InlineData ("a")]
        [InlineData ("George")]
        [InlineData ("blarg456@$^&*")]
        [InlineData ("My House")]
        public void BadWordChecker_Test_True(string arg)
        {
            foreach(var val in arg)
            {
                Assert.True(BadWordChecker.CheckWord(arg));
            }
        }

        [Theory]
        [InlineData ("@$$wipe")]
        [InlineData ("penislover69")]
        [InlineData ("BooBooksLibrary")]
        public void BadWordChecker_Test_False(string arg)
        {
            foreach(var val in arg)
            {
                Assert.False(BadWordChecker.CheckWord(arg));
            }
        }

        [Fact]
        public void CaseConverter_Test_Equals()
        {
            string arg = "fOObaR";
            Assert.Equal("Foobar", CaseConverter.Convert(arg));
        }

// ! MODEL
        [Fact]
        public void ItemId_Test_get_set()
        {
            Item item = new Item();
            item.Id = 5;
            Assert.Equal(5,item.Id);
        }

        [Fact]
        public void ItemName_Test_get_set()
        {
            Item item = new Item();
            try
            {
                item.Name = "";
            }
            catch (Exception ex)
            {
                Assert.IsType<ArgumentException>(ex);
            }
        }

        [Fact]
        public void ItemName_Test_get_set2()
        {
            Item item = new Item();
            item.Name = "bLARG";
            Assert.Equal("Blarg",item.Name);
        }

        [Fact]
        public void ItemDescription_Test_get_set()
        {
            Item item = new Item();
            item.Description = "This is a desCRiption";
            Assert.Equal("This is a desCRiption",item.Description);
        }

        [Fact]
        public void ItemDescription_Test_get_set2()
        {
            Item item = new Item();
            try
            {
                item.Description = "";
            }
            catch (Exception ex)
            {
                Assert.IsType<ArgumentException>(ex);
            }
        }

        [Fact]
        public void ItemPrice_Test_get_set()
        {
            Item item = new Item();
            item.Price = 50.88;
            Assert.Equal(50.88,item.Price);
        }

        [Fact]
        public void ItemPrice_Test_get_set2()
        {
            Item item = new Item();
            try
            {
                item.Price = 0;
            }
            catch (Exception ex)
            {
                Assert.IsType<ArgumentException>(ex);
            }
        }

        [Fact]
        public void OrderId_Test_get_set()
        {
            Order order = new Order();
            order.Id = 5;
            Assert.Equal(5,order.Id);
        }

        [Fact]
        public void OrderName_Test_get_set()
        {
            Order order = new Order();
            try
            {
                order.Username = "";
            }
            catch (Exception ex)
            {
                Assert.IsType<ArgumentException>(ex);
            }
        }

        [Fact]
        public void OrderName_Test_get_set2()
        {
            Order order = new Order();
            order.Username = "Bobby";
            Assert.Equal("Bobby",order.Username);
        }

        [Fact]
        public void OrderItemsInItInt_Test_get_set()
        {
            Order order = new Order();
            order.itemsInOrder.Add(1);
            order.itemsInOrder.Add(3);
            order.itemsInOrder.Add(2);
            string output = order.itemsInOrder[0].ToString();
            for (int i = 1; i < order.itemsInOrder.Count; i++)
            {
                output = output + " " + order.itemsInOrder[i];
            }
            Assert.Equal("1 3 2",output);
        }
    }
}
