using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace csharp
{
    [TestFixture]
    public class BasicItemTest
    {
        [Test]
        public void BasicItem_NormalCaseBehaviour()
        {
            IList<Item> Items = new List<Item>{
                new Item {Name = "Basic item", SellIn = 15, Quality = 50 },
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
                new Item {Name = "Basic item", SellIn = 15, Quality = -1 },
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
                new Item {Name = "Basic item", SellIn = 15, Quality = 51 },
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
        public void BasicItem_LowSellIn_QualityStopsDecreasing()
        {
            IList<Item> Items = new List<Item>{
                new Item {Name = "Basic item", SellIn = 1, Quality = 4 },
            };
            var app = new GildedRose(Items);

            app.UpdateQuality();
            Assert.AreEqual(0, Items[0].SellIn);
            Assert.AreEqual(3, Items[0].Quality);

            app.UpdateQuality();
            Assert.AreEqual(-1, Items[0].SellIn);
            Assert.AreEqual(1, Items[0].Quality);

            app.UpdateQuality();
            Assert.AreEqual(-2, Items[0].SellIn);
            Assert.AreEqual(0, Items[0].Quality);

            app.UpdateQuality();
            Assert.AreEqual(-3, Items[0].SellIn);
            Assert.AreEqual(0, Items[0].Quality);
        }
    }
}