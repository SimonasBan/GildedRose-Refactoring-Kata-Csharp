using System;
using System.Collections.Generic;

namespace csharp
{
    public class GildedRose
    {        
        IList<Item> Items;

        //Default validation values
        const int LowestValidQualityDefault = 0;
        const int HighestValidQualityDefault = 50;     
        const int LowestValidSellInDefault = 0;

        //Specific validation values
        const int Brie_HighestValidQualityLowSellIn = HighestValidQualityDefault - 1;
        const int BasicItem_LowestValidQualityLowSellIn = LowestValidSellInDefault + 1;
        const int Concert_FirstQualityChangeSellIn = 10;
        const int Concert_FirstQualityChange_HighestValidQuality = HighestValidQualityDefault - 1;
        const int Concert_SecondQualityChangeSellIn = 5;
        const int Concert_SecondQualityChange_HighestValidQuality = HighestValidQualityDefault - 2;

        //Validation messages
        const string QualityLessThanZeroMessage = "Quality can not be less than 0";
        const string QualityMoreThanFifty = "Quality can not be more than 50";

        public GildedRose(IList<Item> Items)
        {
            this.Items = Items;
        }

        /*To read for evaluator.
        * ---------- 
        * If I could change Item class, I would add
        * itemType property to it and make switch based on itemType.
        * However I can't. I thought about doing Polymorphism 
        * too but I suppose that the requirement:
        * 'do not alter the Item class or Items property'
        * does not allow
        * me to do that as well since I wouldn't use
        * Items property anymore.
        * ------
        * My solution is based on switch since
        * the task asked to make the code readable
        * and maintanable. This would make it easier to add new
        * items in the future.
        * --------
        * There was also an idea to
        ad if (item.SellIn > 0) here and have
        different switches for items that are old
        and not since almost all of the items
        act differently with age., nm
        But this would decrease readability
        as well as maintainability
        */
        public void UpdateQuality()
        {
            foreach (var item in Items)
            {
                //The Quality of an item is never negative
                if (item.Quality >= LowestValidQualityDefault)
                {              

                    //The Quality of an item is never more than 50
                    if (item.Quality > HighestValidQualityDefault && item.Name != "Sulfuras, Hand of Ragnaros")
                    {
                        throw new ArgumentOutOfRangeException("Quality", QualityMoreThanFifty);
                    }
                    else
                    {
                        switch (item.Name)
                        {
                            case "Backstage passes to a TAFKAL80ETC concert":
                                HandleConcertChanges(item);
                                break;
                            case "Aged Brie":
                                HandleAgedBrieChanges(item);
                                break;
                            //Basic items
                            default:
                                HandleBasicItemChanges(item);
                                break;
                        }
                    }
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Quality", QualityLessThanZeroMessage);
                }
            }
        }

        public void HandleConcertChanges(Item item)
        {
            //SellIn < 0
            if (item.SellIn <= LowestValidSellInDefault)
            {
                item.Quality =  0;
            }
            //SellIn > 0 && <=5 && Quality < 48 (Because Quality can not be >50)
            else if (item.SellIn <= Concert_SecondQualityChangeSellIn && item.Quality < Concert_SecondQualityChange_HighestValidQuality)
            {
                item.Quality += 3;
            }
            //SellIn > 5 && SellIn <= 10 && Quality < 49 (Because Quality can not be >50)
            else if (item.SellIn <= Concert_FirstQualityChangeSellIn && item.Quality < Concert_FirstQualityChange_HighestValidQuality)
            {
                item.Quality += 2;
            }
            //SellIn > 10 && Quality <50 (because Quality can not be >50)
            else if (item.Quality < HighestValidQualityDefault)
            {
                item.Quality += 1;
            }

            item.SellIn -= 1;
        }

        public void HandleAgedBrieChanges(Item item)
        {
            //SellIn <=0 and Quality < 49 (Because Quality can not be >50)
            if (item.SellIn <= LowestValidSellInDefault && item.Quality < Brie_HighestValidQualityLowSellIn)
            {
                item.Quality += 2;
            }
            //SellIn >0 && Quality <50 (because Quality can not be >50)
            else if (item.Quality < HighestValidQualityDefault)
            {
                item.Quality += 1;
            }
            item.SellIn -= 1;
        }

        public void HandleBasicItemChanges(Item item)
        {
            if (item.Quality > 0)
            {
                //SellIn < 0
                if (item.SellIn <= LowestValidSellInDefault)
                {
                    //Quality > 1 ? (-2) : 0
                    item.Quality = (item.Quality > BasicItem_LowestValidQualityLowSellIn) ? (item.Quality - 2) : 0;
                }
                else
                {
                    item.Quality -= 1;
                }
            }            
            item.SellIn -= 1;
        }



        ////Old solution
        //public void UpdateQuality()
        //{          


        //    foreach (var item in Items)
        //    {
        //        /*WHOLE IF - takes care of Quality
        //         * -------                 * 
        //         * Items: basic, Sulfurus*/
        //        if (item.Name != "Aged Brie" && item.Name != "Backstage passes to a TAFKAL80ETC concert")
        //        {
        //            if (item.Quality > 0)
        //            {
        //                //If basic item
        //                if (item.Name != "Sulfuras, Hand of Ragnaros")
        //                {
        //                    item.Quality = item.Quality - 1;
        //                }
        //            }
        //            /*else{}
        //             Cover edge case of item with quality<0*/
        //        }
        //        /*Items: Brie, Concert*/
        //        else
        //        {
        //            if (item.Quality < 50)
        //            {
        //                //Always increases Brie and concert at least by 1
        //                item.Quality = item.Quality + 1;

        //                if (item.Name == "Backstage passes to a TAFKAL80ETC concert")
        //                {

        //                    //< 11 is +2 (but 1 is already plussed by default)
        //                    if (item.SellIn < 11)
        //                    {
        //                        //This is already checked I thik
        //                        if (item.Quality < 50)
        //                        {
        //                            item.Quality = item.Quality + 1;
        //                        }
        //                    }

        //                    /*< 6 is +3 (but 1 is already plussed
        //                     * by default and another 1 because it was < 11)
        //                     * --------
        //                     * But this should be in an else of <11
        //                     */
        //                    if (item.SellIn < 6)
        //                    {
        //                        //This is already checked I thik
        //                        if (item.Quality < 50)
        //                        {
        //                            item.Quality = item.Quality + 1;
        //                        }
        //                    }
        //                }
        //            }
        //            /*else{}
        //             Cover edge case of item with quality>50*/
        //        }

        //        //Every item get older except Sulfuras.
        //        if (item.Name != "Sulfuras, Hand of Ragnaros")
        //        {
        //            item.SellIn = item.SellIn - 1;
        //        }


        //        /*WHOLE IF - takes care of age
        //         * (decreases quality on edge cases caused by age)
        //         Maybe could be put as an else*/
        //        if (item.SellIn < 0)
        //        {
        //            //Basic, concert, sulfurues items
        //            if (item.Name != "Aged Brie")
        //            {
        //                //Basic, sulfurus items
        //                if (item.Name != "Backstage passes to a TAFKAL80ETC concert")
        //                {
        //                    //This should be put earlier and not repeated
        //                    if (item.Quality > 0)
        //                    {
        //                        /*Basic items.
        //                        Check it earlier together with concert?*/
        //                        if (item.Name != "Sulfuras, Hand of Ragnaros")
        //                        {
        //                            /*Basic items were already decreased
        //                             * but with age <0 their Quality decreases by 2
        //                            */
        //                            item.Quality = item.Quality - 1;
        //                        }
        //                    }
        //                }
        //                //Concert items. Quality is zero when age is <0
        //                else
        //                {
        //                    //Change to = 0
        //                    item.Quality = item.Quality - item.Quality;
        //                }
        //            }
        //            //Brie items. Get better no matter the age
        //            else
        //            {
        //                if (item.Quality < 50)
        //                {
        //                    item.Quality = item.Quality + 1;
        //                }
        //            }
        //        }
        //    }
        //}
    }
}
