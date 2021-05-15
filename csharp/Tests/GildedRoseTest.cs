using NUnit.Framework;
using System.Collections.Generic;

namespace csharp
{
    [TestFixture]
    public class GildedRoseTest
    {
        /*This test was made to work with
         the original code*/
        [Test]
        public void foo()
        {
            IList<Item> Items = new List<Item>{
                new Item {Name = "Basic item", SellIn = 3, Quality = 6 },
                new Item {Name = "Aged Brie", SellIn = 2, Quality = 0},
                new Item {Name = "Aged Brie", SellIn = 1, Quality = 46},
                new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 5, Quality = 80},
                new Item
                {
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = 12,
                    Quality = 20
                },
                new Item
                {
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = 6,
                    Quality = 43
                },
                new Item
                {
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = 2,
                    Quality = 30
                }
            };

            var app = new GildedRose(Items);

            app.UpdateQuality();
            //---basic---
            Assert.AreEqual(2, Items[0].SellIn);
            Assert.AreEqual(5, Items[0].Quality);
            //---brieLowQ---
            Assert.AreEqual(1, Items[1].Quality);
            //---brieHighQ---
            Assert.AreEqual(47, Items[2].Quality);
            ////---Sulfuras---
            //Assert.AreEqual(80, Items[3].Quality);
            //Assert.AreEqual(5, Items[3].SellIn);
            ////---BackstageHighSell---
            //Assert.AreEqual(21, Items[4].Quality);
            ////---BackstageMidSell---
            //Assert.AreEqual(45, Items[5].Quality);
            ////---BackstageLowSell---
            //Assert.AreEqual(33, Items[6].Quality);



            app.UpdateQuality();
            //---basic---
            Assert.AreEqual(1, Items[0].SellIn);
            Assert.AreEqual(4, Items[0].Quality);
            //---brieLowQ---
            Assert.AreEqual(2, Items[1].Quality);
            //---brieHighQ---
            Assert.AreEqual(49, Items[2].Quality);
            ////---Sulfuras---
            //Assert.AreEqual(80, Items[3].Quality);
            //Assert.AreEqual(5, Items[3].SellIn);
            ////---BackstageHighSell---
            //Assert.AreEqual(22, Items[4].Quality);
            ////---BackstageMidSell---
            //Assert.AreEqual(48, Items[5].Quality);
            ////---BackstageLowSell---
            //Assert.AreEqual(36, Items[6].Quality);





            app.UpdateQuality();
            //---basic---
            Assert.AreEqual(0, Items[0].SellIn);
            Assert.AreEqual(3, Items[0].Quality);
            //---brieLowQ---
            Assert.AreEqual(4, Items[1].Quality);
            //---brieHighQ---
            Assert.AreEqual(50, Items[2].Quality);
            ////---BackstageHighSell---
            //Assert.AreEqual(24, Items[4].Quality);
            ////---BackstageMidSell---
            //Assert.AreEqual(50, Items[5].Quality);
            ////---BackstageLowSell---
            //Assert.AreEqual(0, Items[6].Quality);




            app.UpdateQuality();
            //---basic---
            Assert.AreEqual(-1, Items[0].SellIn);
            Assert.AreEqual(1, Items[0].Quality);
            //---brieLowQ---
            Assert.AreEqual(6, Items[1].Quality);
            ////---BackstageHighSell---
            //Assert.AreEqual(26, Items[4].Quality);




            app.UpdateQuality();
            //---basic---
            Assert.AreEqual(-2, Items[0].SellIn);
            Assert.AreEqual(0, Items[0].Quality);




            app.UpdateQuality();
            //---basic---
            Assert.AreEqual(-3, Items[0].SellIn);
            Assert.AreEqual(0, Items[0].Quality);

        }
    }
}
