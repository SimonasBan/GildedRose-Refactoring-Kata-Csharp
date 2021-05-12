using System.Collections.Generic;

namespace csharp
{
    public class GildedRose
    {
        IList<Item> Items;
        public GildedRose(IList<Item> Items)
        {
            this.Items = Items;
        }

        /*This method can be changed*/
        public void UpdateQuality()
        {
            foreach (var item in Items)
            {
                /*WHOLE IF - takes care of Quality
                 * -------                 * 
                 * Items: basic, Sulfurus*/
                if (item.Name != "Aged Brie" && item.Name != "Backstage passes to a TAFKAL80ETC concert")
                {
                    if (item.Quality > 0)
                    {   
                        //If basic item
                        if (item.Name != "Sulfuras, Hand of Ragnaros")
                        {
                            item.Quality = item.Quality - 1;
                        }
                    }
                    /*else{}
                     Cover edge case of item with quality<0*/
                }
                /*Items: Brie, Concert*/
                else
                {
                    if (item.Quality < 50)
                    {
                        //Always increases Brie and concert at least by 1
                        item.Quality = item.Quality + 1;

                        if (item.Name == "Backstage passes to a TAFKAL80ETC concert")
                        {

                            //< 11 is +2 (but 1 is already plussed by default)
                            if (item.SellIn < 11)
                            {
                                //This is already checked I thik
                                if (item.Quality < 50)
                                {
                                    item.Quality = item.Quality + 1;
                                }
                            }

                            /*< 6 is +3 (but 1 is already plussed
                             * by default and another 1 because it was < 11)
                             * --------
                             * But this should be in an else of <11
                             */
                            if (item.SellIn < 6)
                            {
                                //This is already checked I thik
                                if (item.Quality < 50)
                                {
                                    item.Quality = item.Quality + 1;
                                }
                            }
                        }
                    }
                    /*else{}
                     Cover edge case of item with quality>50*/
                }

                //Every item get older except Sulfuras.
                if (item.Name != "Sulfuras, Hand of Ragnaros")
                {
                    item.SellIn = item.SellIn - 1;
                }


                /*WHOLE IF - takes care of age
                 * (decreases quality on edge cases caused by age)
                 Maybe could be put as an else*/
                if (item.SellIn < 0)
                {
                    //Basic, concert, sulfurues items
                    if (item.Name != "Aged Brie")
                    {
                        //Basic, sulfurus items
                        if (item.Name != "Backstage passes to a TAFKAL80ETC concert")
                        {
                            //This should be put earlier and not repeated
                            if (item.Quality > 0)
                            {   
                                /*Basic items.
                                Check it earlier together with concert?*/
                                if (item.Name != "Sulfuras, Hand of Ragnaros")
                                {
                                    /*Basic items were already decreased
                                     * but with age <0 their Quality decreases by 2
                                    */
                                    item.Quality = item.Quality - 1;
                                }
                            }
                        }
                        //Concert items. Quality is zero when age is <0
                        else
                        {
                            //Change to = 0
                            item.Quality = item.Quality - item.Quality;
                        }
                    }
                    //Brie items. Get better no matter the age
                    else
                    {
                        if (item.Quality < 50)
                        {
                            item.Quality = item.Quality + 1;
                        }
                    }
                }
            }
        }
    }
}
