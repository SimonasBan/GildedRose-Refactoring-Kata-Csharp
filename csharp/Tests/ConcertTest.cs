using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace csharp
{
    [TestFixture]
    public class ConcertTest
    {
        public string ItemName = "Backstage passes to a TAFKAL80ETC concert";

        [Test]
        public void Concert_SellInAboveTen_ShouldIncreaseQualityByOne()
        {
            IList<Item> Items = new List<Item>{
                new Item {Name = ItemName, SellIn = 15, Quality = 0 }
            };
            var app = new GildedRose(Items);

            app.UpdateQuality();
            Assert.AreEqual(14, Items[0].SellIn);
            Assert.AreEqual(1, Items[0].Quality);

            app.UpdateQuality();
            Assert.AreEqual(13, Items[0].SellIn);
            Assert.AreEqual(2, Items[0].Quality);
        }

        [Test]
        public void Concert_SellInBelowEleven_ShouldIncreaseQualityByTwo()
        {
            IList<Item> Items = new List<Item>{
                new Item {Name = ItemName, SellIn = 11, Quality = 30 }
            };
            var app = new GildedRose(Items);

            app.UpdateQuality();            
            Assert.AreEqual(31, Items[0].Quality);
            Assert.AreEqual(10, Items[0].SellIn);


            app.UpdateQuality();
            Assert.AreEqual(33, Items[0].Quality);
            Assert.AreEqual(9, Items[0].SellIn);

            app.UpdateQuality();
            Assert.AreEqual(35, Items[0].Quality);
            Assert.AreEqual(8, Items[0].SellIn);
        }

        [Test]
        public void Concert_SellInBelowSix_ShouldIncreaseQualityByThree()
        {
            IList<Item> Items = new List<Item>{
                new Item {Name = ItemName, SellIn = 6, Quality = 30 }
            };
            var app = new GildedRose(Items);

            app.UpdateQuality();
            Assert.AreEqual(32, Items[0].Quality);
            Assert.AreEqual(5, Items[0].SellIn);


            app.UpdateQuality();
            Assert.AreEqual(35, Items[0].Quality);
            Assert.AreEqual(4, Items[0].SellIn);

            app.UpdateQuality();
            Assert.AreEqual(38, Items[0].Quality);
            Assert.AreEqual(3, Items[0].SellIn);
        }

        [Test]
        public void Concert_SellInEqualsZero_ShouldSetQualityToZero()
        {
            IList<Item> Items = new List<Item>{
                new Item {Name = ItemName, SellIn = 1, Quality = 30 }
            };
            var app = new GildedRose(Items);

            app.UpdateQuality();
            Assert.AreEqual(33, Items[0].Quality);
            Assert.AreEqual(0, Items[0].SellIn);


            app.UpdateQuality();
            Assert.AreEqual(0, Items[0].Quality);
            Assert.AreEqual(-1, Items[0].SellIn);

            app.UpdateQuality();
            Assert.AreEqual(0, Items[0].Quality);
            Assert.AreEqual(-2, Items[0].SellIn);
        }

        [Test]
        public void Concert_QualityReachesFiftySellInAboveTen_ShouldNotSetQualityAboveFifty()
        {
            IList<Item> Items = new List<Item>{
                new Item {Name = ItemName, SellIn = 15, Quality = 49 }
            };
            var app = new GildedRose(Items);

            app.UpdateQuality();
            Assert.AreEqual(50, Items[0].Quality);


            app.UpdateQuality();
            Assert.AreEqual(50, Items[0].Quality);
        }

        [Test]
        public void Concert_QualityReachesFiftySellInBelowEleven_ShouldNotSetQualityAboveFifty()
        {
            IList<Item> Items = new List<Item>{
                new Item {Name = ItemName, SellIn = 10, Quality = 49 },
                new Item {Name = ItemName, SellIn = 10, Quality = 48 },
                new Item {Name = ItemName, SellIn = 10, Quality = 47 }
            };
            var app = new GildedRose(Items);

            app.UpdateQuality();
            Assert.AreEqual(50, Items[0].Quality);
            Assert.AreEqual(50, Items[1].Quality);
            Assert.AreEqual(49, Items[2].Quality);


            app.UpdateQuality();
            Assert.AreEqual(50, Items[0].Quality);
            Assert.AreEqual(50, Items[1].Quality);
            Assert.AreEqual(50, Items[2].Quality);

        }

        [Test]
        public void Concert_QualityReachesFiftySellInBelowSix_ShouldNotSetQualityAboveFifty()
        {
            IList<Item> Items = new List<Item>{
                new Item {Name = ItemName, SellIn = 5, Quality = 49 },
                new Item {Name = ItemName, SellIn = 5, Quality = 48 },
                new Item {Name = ItemName, SellIn = 5, Quality = 47 },
                new Item {Name = ItemName, SellIn = 5, Quality = 46 }
            };
            var app = new GildedRose(Items);

            app.UpdateQuality();
            Assert.AreEqual(50, Items[0].Quality);
            Assert.AreEqual(50, Items[1].Quality);
            Assert.AreEqual(50, Items[2].Quality);
            Assert.AreEqual(49, Items[3].Quality);


            app.UpdateQuality();
            Assert.AreEqual(50, Items[0].Quality);
            Assert.AreEqual(50, Items[1].Quality);
            Assert.AreEqual(50, Items[2].Quality);
            Assert.AreEqual(50, Items[3].Quality);

        }

        [Test]
        public void Concert_WhenQualityIsNegative_ShouldThrowArgumentOutOfRange()
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
        public void Concert_WhenQualityIsMoreThanFifty_ShouldThrowArgumentOutOfRange()
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
    }
}