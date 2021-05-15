using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace csharp
{
    [TestFixture]
    public class AgedBrieTest
    {
        [Test]
        public void AgedBrie_NormalCaseBehaviour_ShouldIncreaseQualityByOne()
        {
            IList<Item> Items = new List<Item>{
                new Item {Name = "Aged Brie", SellIn = 15, Quality = 30 },
            };
            var app = new GildedRose(Items);

            app.UpdateQuality();
            Assert.AreEqual(14, Items[0].SellIn);
            Assert.AreEqual(31, Items[0].Quality);

            app.UpdateQuality();
            Assert.AreEqual(13, Items[0].SellIn);
            Assert.AreEqual(32, Items[0].Quality);
        }

        [Test]
        public void AgedBrie_HighQualityLowSellIn_QualityShouldNotGetMoreThanFifty()
        {
            IList<Item> Items = new List<Item>{
                new Item {Name = "Aged Brie", SellIn = -2, Quality = 49 },
                new Item {Name = "Aged Brie", SellIn = -2, Quality = 48 },
                new Item {Name = "Aged Brie", SellIn = -2, Quality = 47 }
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
        public void AgedBrie_HighQuality_QualityShouldNotGetMoreThanFifty()
        {
            IList<Item> Items = new List<Item>{
                new Item {Name = "Aged Brie", SellIn = 5, Quality = 49 },
            };
            var app = new GildedRose(Items);

            app.UpdateQuality();
            Assert.AreEqual(50, Items[0].Quality);

            app.UpdateQuality();
            Assert.AreEqual(50, Items[0].Quality);
        }

        [Test]
        public void AgedBrie_LowSellIn_ShouldIncreaseQualityByTwo ()
        {
            IList<Item> Items = new List<Item>{
                new Item {Name = "Aged Brie", SellIn = 2, Quality = 10 },
            };
            var app = new GildedRose(Items);

            app.UpdateQuality();            
            Assert.AreEqual(11, Items[0].Quality);
            Assert.AreEqual(1, Items[0].SellIn);

            app.UpdateQuality();
            Assert.AreEqual(12, Items[0].Quality);
            Assert.AreEqual(0, Items[0].SellIn);

            app.UpdateQuality();
            Assert.AreEqual(14, Items[0].Quality);
            Assert.AreEqual(-1, Items[0].SellIn);
        }

        [Test]
        public void AgedBrie_WhenQualityIsNegative_ShouldThrowArgumentOutOfRange()
        {
            IList<Item> Items = new List<Item>{
                new Item {Name = "Aged Brie", SellIn = 15, Quality = -1 },
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
        public void AgedBrie_WhenQualityIsMoreThanFifty_ShouldThrowArgumentOutOfRange()
        {
            IList<Item> Items = new List<Item>{
                new Item {Name = "Aged Brie", SellIn = 15, Quality = 51 },
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