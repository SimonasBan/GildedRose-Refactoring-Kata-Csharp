using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace csharp
{
    /*All tests pass with the old and new UpdareQuality methods*/
    [TestFixture]
    public class BasicItemTest
    {
        public string ItemName = "Basic item";
        [Test]
        public void BasicItem_NormalCaseBehaviour()
        {
            IList<Item> Items = new List<Item>{
                new Item {Name = ItemName, SellIn = 15, Quality = 50 },
            };
            var app = new GildedRose(Items);

            app.UpdateQuality();
            Assert.AreEqual(14, Items[0].SellIn);
            Assert.AreEqual(49, Items[0].Quality);

            app.UpdateQuality();
            Assert.AreEqual(13, Items[0].SellIn);
            Assert.AreEqual(48, Items[0].Quality);
        }

        [Test]
        public void BasicItem_WhenQualityIsNegative_ShouldThrowArgumentOutOfRange()
        {
            IList<Item> Items = new List<Item>{
                new Item {Name = ItemName, SellIn = 15, Quality = -1 },
            };
            var app = new GildedRose(Items);

            try
            {
                app.UpdateQuality();
            }
            catch(System.ArgumentOutOfRangeException e)
            {
                StringAssert.Contains("Quality can not be less than 0", e.Message);                
            }
        }

        [Test]
        public void BasicItem_WhenQualityIsMoreThanFifty_ShouldThrowArgumentOutOfRange()
        {
            IList<Item> Items = new List<Item>{
                new Item {Name = ItemName, SellIn = 15, Quality = 51 },
            };
            var app = new GildedRose(Items);

            try
            {
                app.UpdateQuality();
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                StringAssert.Contains("Quality can not be more than 50", e.Message);
            }
        }


        [Test]
        public void BasicItem_LowSellInLowQuality_ShouldStopDecreasingQuality()
        {
            IList<Item> Items = new List<Item>{
                new Item {Name = ItemName, SellIn = 1, Quality = 4 },
                new Item {Name = ItemName, SellIn = 1, Quality = 3 },
            };
            var app = new GildedRose(Items);

            app.UpdateQuality();
            Assert.AreEqual(0, Items[0].SellIn);
            Assert.AreEqual(3, Items[0].Quality);
            Assert.AreEqual(2, Items[1].Quality);

            app.UpdateQuality();
            Assert.AreEqual(-1, Items[0].SellIn);
            Assert.AreEqual(1, Items[0].Quality);
            Assert.AreEqual(0, Items[1].Quality);

            app.UpdateQuality();
            Assert.AreEqual(-2, Items[0].SellIn);
            Assert.AreEqual(0, Items[0].Quality);
            Assert.AreEqual(0, Items[1].Quality);

            app.UpdateQuality();
            Assert.AreEqual(-3, Items[0].SellIn);
            Assert.AreEqual(0, Items[0].Quality);
        }

        [Test]
        public void BasicItem_LowQuality_ShouldStopDecreasingQuality()
        {
            IList<Item> Items = new List<Item>{
                new Item {Name = ItemName, SellIn = 10, Quality = 2 },
            };
            var app = new GildedRose(Items);

            app.UpdateQuality();
            Assert.AreEqual(1, Items[0].Quality);

            app.UpdateQuality();
            Assert.AreEqual(0, Items[0].Quality);

            app.UpdateQuality();
            Assert.AreEqual(0, Items[0].Quality);
        }
    }
}