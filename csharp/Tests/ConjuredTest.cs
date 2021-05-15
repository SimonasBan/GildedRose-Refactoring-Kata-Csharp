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
    public class ConjuredTest
    {
        public string ItemName = "Conjured Mana Cake";
        [Test]
        public void Conjured_NormalCaseBehaviour()
        {
            IList<Item> Items = new List<Item>{
                new Item {Name = ItemName, SellIn = 15, Quality = 50 },
            };
            var app = new GildedRose(Items);

            app.UpdateQuality();
            Assert.AreEqual(14, Items[0].SellIn);
            Assert.AreEqual(48, Items[0].Quality);

            app.UpdateQuality();
            Assert.AreEqual(13, Items[0].SellIn);
            Assert.AreEqual(46, Items[0].Quality);
        }

        [Test]
        public void Conjured_WhenQualityIsNegative_ShouldThrowArgumentOutOfRange()
        {
            IList<Item> Items = new List<Item>{
                new Item {Name = ItemName, SellIn = 15, Quality = -1 },
            };
            var app = new GildedRose(Items);

            try
            {
                app.UpdateQuality();
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                StringAssert.Contains("Quality can not be less than 0", e.Message);
            }
        }

        [Test]
        public void Conjured_WhenQualityIsMoreThanFifty_ShouldThrowArgumentOutOfRange()
        {
            IList<Item> Items = new List<Item>{
                new Item {Name = ItemName, SellIn = 15, Quality = 80 },
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
        public void Conjured_LowSellInLowQuality_ShouldStopDecreasingQuality()
        {
            IList<Item> Items = new List<Item>{
                new Item {Name = ItemName, SellIn = 1, Quality = 5 },
                new Item {Name = ItemName, SellIn = 1, Quality = 4 },
                new Item {Name = ItemName, SellIn = 1, Quality = 6 },
                new Item {Name = ItemName, SellIn = 1, Quality = 7 }
            };
            var app = new GildedRose(Items);

            app.UpdateQuality();
            Assert.AreEqual(0, Items[0].SellIn);
            Assert.AreEqual(3, Items[0].Quality);
            Assert.AreEqual(2, Items[1].Quality);
            Assert.AreEqual(4, Items[2].Quality);
            Assert.AreEqual(5, Items[3].Quality);


            app.UpdateQuality();
            Assert.AreEqual(-1, Items[0].SellIn);
            Assert.AreEqual(0, Items[0].Quality);
            Assert.AreEqual(0, Items[1].Quality);
            Assert.AreEqual(0, Items[2].Quality);
            Assert.AreEqual(1, Items[3].Quality);


            app.UpdateQuality();
            Assert.AreEqual(0, Items[3].Quality);


        }

        [Test]
        public void Conjured_LowQuality_ShouldStopDecreasingQuality()
        {
            IList<Item> Items = new List<Item>{
                new Item {Name = ItemName, SellIn = 10, Quality = 2 },
                new Item {Name = ItemName, SellIn = 10, Quality = 3 },
            };
            var app = new GildedRose(Items);

            app.UpdateQuality();
            Assert.AreEqual(0, Items[0].Quality);
            Assert.AreEqual(1, Items[1].Quality);

            app.UpdateQuality();
            Assert.AreEqual(0, Items[1].Quality);

        }
    }
}