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
        public void foo()
        {
            IList<Item> Items = new List<Item>{
                new Item {Name = "Basic item", SellIn = 3, Quality = 6 },
            };

            var app = new GildedRose(Items);

            app.UpdateQuality();
            //---basic---
            Assert.AreEqual(2, Items[0].SellIn);
            Assert.AreEqual(5, Items[0].Quality);


            app.UpdateQuality();
            //---basic---
            Assert.AreEqual(1, Items[0].SellIn);
            Assert.AreEqual(4, Items[0].Quality);






            app.UpdateQuality();
            //---basic---
            Assert.AreEqual(0, Items[0].SellIn);
            Assert.AreEqual(3, Items[0].Quality);





            app.UpdateQuality();
            //---basic---
            Assert.AreEqual(-1, Items[0].SellIn);
            Assert.AreEqual(1, Items[0].Quality);





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