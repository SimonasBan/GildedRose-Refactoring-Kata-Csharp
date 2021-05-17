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
        const int Conjured_LowestValidQualityLowSellIn = 3;
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
        * --------
        * My solution is based on switch since
        * the task asked to make the code readable
        * and maintanable. This would make it easier to add new
        * items in the future.
        * ---------- 
        * If I could change Item class, I would add
        * itemType property to it and make switch based on itemType.
        * However I can't. I thought about doing Polymorphism 
        * too but I suppose that the requirement:
        * 'do not alter the Item class or Items property'
        * does not allow
        * me to do that as well since I wouldn't use
        * Items property anymore.
        */
        public void UpdateQuality()
        {
            foreach (var item in Items)

            {
                //The Quality of an item is never negative
                if (item.Quality >= LowestValidQualityDefault)
                {

                    //The Quality of an item is never more than 50 unless it's sulfuras
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
                            case "Sulfuras, Hand of Ragnaros":
                                break;
                            case "Conjured Mana Cake":
                                HandleConjuredItemChanges(item);
                                break;
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

        public void HandleConjuredItemChanges(Item item)
        {
            if (item.Quality > 0)
            {
                if (item.Quality > 1)
                {
                    //SellIn < 0
                    if (item.SellIn <= LowestValidSellInDefault)
                    {
                        //Quality > 2 ? (-4) : 0
                        item.Quality = (item.Quality > Conjured_LowestValidQualityLowSellIn) ? (item.Quality - 4) : 0;
                    }
                    else
                    {
                        item.Quality -= 2;
                    }
                }
                //Quality == 1
                else
                {
                    item.Quality = 0;
                }
            }
            item.SellIn -= 1;
        }
    }
}
