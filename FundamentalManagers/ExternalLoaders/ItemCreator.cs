using System;
using System.Collections.Generic;
using MTM101BaldAPI;
using MTM101BaldAPI.ObjectCreation;
using bbpfer.CustomContent.CustomItems;
using bbpfer.Extessions;
using bbpfer.Enums;

namespace bbpfer.FundamentalManagers.ExternalLoaders
{
    public partial class ItemCreator
    {
        public static void Load()
        {
            ItemObject commonTeleporter = new ItemBuilder(BasePlugin.instance.Info)
            .SetNameAndDescription("Itm_CommonTeleporter", "Desc_CommonTeleporter")
            .SetEnum(CustomItems.CommonTeleporter.ToString())
            .SetShopPrice(150)
            .SetItemComponent<ITM_CommonTeleporter>()
            .SetSprites("CommonTeleporterIcon")
            .SetGeneratorCost(35)
            .Build()
            .InitializeItem()
            .CreateItem(new string[] { "F1", "F2", "F3", "F4", "END" }, new int[] { 75, 100, 80, 95, 120 })
            .AddIntoShop(new string[] { "F1", "F2", "F3", "F4", "END" }, new int[] { 75, 95, 110, 100, 130 });

            ItemObject coffeWithSugar = new ItemBuilder(BasePlugin.instance.Info)
            .SetNameAndDescription("Itm_CoffeWithSugar", "Desc_CoffeWithSugar")
            .SetEnum(CustomItems.CoffeAndSugar.ToString())
            .SetShopPrice(600)
            .SetItemComponent<ITM_CoffeWithSugar>()
            .SetSprites("CoffeWithSugar")
            .SetGeneratorCost(55)
            .Build()
            .InitializeItem()
            .CreateItem(new string[] { "F1", "F2", "F3", "F4", "END" }, new int[] { 40, 32, 44, 46, 65 })
            .AddIntoShop(new string[] { "F1", "F2", "F3", "F4", "END" }, new int[] { 50, 20, 55, 46, 70 });

            ItemObject hammer = new ItemBuilder(BasePlugin.instance.Info)
            .SetNameAndDescription("Itm__Hammer", "Desc__Hammer")
            .SetEnum(CustomItems.GenericHammer.ToString())
            .SetShopPrice(420)
            .SetItemComponent<ITM_Hammer>()
            .SetSprites("Hammer")
            .SetGeneratorCost(45)
            .Build()
            .CreateItem(new string[] { "F1", "F2", "F3", "END" }, new int[] { 32, 54, 40, 75 })
            .AddIntoShop(new string[] { "F1", "F2", "F3", "END" }, new int[] { 40, 50, 30, 88 });

            ItemObject iceCream3 = new ItemBuilder(BasePlugin.instance.Info)
            .SetNameAndDescription("Itm_IceCream3", "Desc_IceCream")
            .SetEnum(CustomItems.IceCream.ToString())
            .SetShopPrice(400)
            .SetItemComponent<ITM_IceCream>()
            .SetSprites("IceCream")
            .SetGeneratorCost(22)
            .SetMeta(MTM101BaldAPI.Registers.ItemFlags.MultipleUse, new string[] { "maxItemUse" })
            .Build()
            .InitializeItem()
            .CreateItem(new string[] { "F1", "F2", "F3", "F4", "END" }, new int[] { 20, 42, 35, 40, 65 })
            .AddIntoShop(new string[] { "F1", "F2", "F3", "F4", "END" }, new int[] { 40, 60, 50, 20, 80 });

            ItemObject iceCream2 = new ItemBuilder(BasePlugin.instance.Info)
            .SetNameAndDescription("Itm_IceCream2", "Desc_IceCream")
            .SetEnum(EnumExtensions.GetFromExtendedName<Items>(CustomItems.IceCream.ToString()))
            .SetItemComponent<ITM_IceCream>()
            .SetSprites("IceCream")
            .SetMeta(MTM101BaldAPI.Registers.ItemFlags.MultipleUse, new string[] { })
            .Build()
            .InitializeItem();

            ItemObject iceCream1 = new ItemBuilder(BasePlugin.instance.Info)
            .SetNameAndDescription("Itm_IceCream1", "Desc_IceCream")
            .SetEnum(EnumExtensions.GetFromExtendedName<Items>(CustomItems.IceCream.ToString()))
            .SetItemComponent<ITM_IceCream>()
            .SetSprites("IceCream")
            .SetMeta(MTM101BaldAPI.Registers.ItemFlags.MultipleUse, new string[] { })
            .Build()
            .InitializeItem();

            iceCream3.item.GetComponent<ITM_IceCream>().nextItem = iceCream2;
            iceCream2.item.GetComponent<ITM_IceCream>().nextItem = iceCream1;

            ItemObject soda = new ItemBuilder(BasePlugin.instance.Info)
            .SetNameAndDescription("Itm_Soda", "Desc_Soda")
            .SetEnum(CustomItems.Soda.ToString())
            .SetShopPrice(420)
            .SetItemComponent<ITM_Soda>()
            .SetSprites("Soda")
            .SetGeneratorCost(45)
            .Build()
             .InitializeItem()
            .CreateItem(new string[] { "F1", "F2", "F3", "F4", "END" }, new int[] { 60, 35, 70, 35, 70 })
            .AddIntoShop(new string[] { "F1", "F2", "F3", "F4", "END" }, new int[] { 100, 56, 85, 54, 80 });

            ItemObject cookie = new ItemBuilder(BasePlugin.instance.Info)
            .SetNameAndDescription("Itm_Cookie", "Desc_Cookie")
            .SetEnum(CustomItems.Cookie.ToString())
            .SetShopPrice(120)
            .SetItemComponent<ITM_Cookie>()
            .SetSprites("Cookie")
            .SetGeneratorCost(27)
            .SetMeta(MTM101BaldAPI.Registers.ItemFlags.MultipleUse, new string[] { "maxItemUse" })
            .Build()
            .InitializeItem()
            .CreateItem(new string[] { "F1", "F2", "F3", "F4", "END" }, new int[] { 110, 90, 77, 64, 115 })
            .AddIntoShop(new string[] { "F1", "F2", "F3", "F4", "END" }, new int[] { 45, 60, 70, 91, 50 });

            ItemObject cookie2 = new ItemBuilder(BasePlugin.instance.Info)
            .SetNameAndDescription("Itm_Cookie2", "Desc_Cookie")
            .SetEnum(EnumExtensions.GetFromExtendedName<Items>(CustomItems.Cookie.ToString()))
            .SetItemComponent<ITM_Cookie>()
            .SetSprites("Cookie1")
            .SetGeneratorCost(27)
            .SetMeta(MTM101BaldAPI.Registers.ItemFlags.MultipleUse, new string[] { })
            .Build()
            .InitializeItem();

            cookie.item.GetComponent<ITM_Cookie>().nextItem = cookie2;

            ItemObject gps = new ItemBuilder(BasePlugin.instance.Info)
            .SetNameAndDescription("Itm_GPS", "Desc_GPS")
            .SetEnum(CustomItems.GPS.ToString())
            .SetShopPrice(280)
            .SetItemComponent<ITM_GPS>()
            .SetSprites("Gps")
            .SetGeneratorCost(40)
            .Build()
            .InitializeItem()
            .CreateItem(new string[] { "F2", "F3", "F4", "END" }, new int[] { 87, 110, 85, 52 })
            .AddIntoShop(new string[] { "F2", "F3", "F4", "END" }, new int[] { 100, 80, 45, 95 });

            ItemObject whistle = new ItemBuilder(BasePlugin.instance.Info)
            .SetNameAndDescription("Itm_SweepWhistle", "Desc_SweepWhistle")
            .SetEnum(CustomItems.SweepWhistle.ToString())
            .SetShopPrice(300)
            .SetItemComponent<ITM_SweepWhistle>()
            .SetSprites("SweepWhistleIcon")
            .SetGeneratorCost(27)
            .Build()
            .InitializeItem()
            .CreateItem(new string[] { "F1", "F2", "F3", "F4", "END" }, new int[] { 40, 95, 80, 60, 98 })
            .AddIntoShop(new string[] { "F1", "F2", "F3", "F4", "END" }, new int[] { 80, 70, 100, 5, 85  });

            ItemObject banHammer = new ItemBuilder(BasePlugin.instance.Info)
            .SetNameAndDescription("Itm_BanHammer", "Desc_BanHammer")
            .SetEnum(CustomItems.BanHammer.ToString())
            .SetShopPrice(700)
            .SetItemComponent<ITM_BanHammer>()
            .SetSprites("BanHammer")
            .SetGeneratorCost(27)
            .Build()
            .InitializeItem()
            .CreateItem(new string[] { "F1", "F2", "F3", "F4", "END" }, new int[] { 20, 72, 28, 55, 78 })
            .AddIntoShop(new string[] { "F1", "F2", "F3", "F4", "END" }, new int[] { 50, 79, 30, 37, 54 });
            

            ItemObject plasticPercussionHammer = new ItemBuilder(BasePlugin.instance.Info)
            .SetNameAndDescription("Itm_PlasticPercussionHammer", "Desc_PlasticPercussionHammer")
            .SetEnum(CustomItems.PlasticPercussionHammer.ToString())
            .SetShopPrice(450)
            .SetItemComponent<ITM_PercussionHammer>()
            .SetSprites("PercusionHammer")
            .SetGeneratorCost(32)
            .Build()
            .InitializeItem()
            .CreateItem(new string[] { "F2", "F3", "F4", "END" }, new int[] { 85, 100, 92, 105 })
            .AddIntoShop(new string[] { "F2", "F3", "F4", "END" }, new int[] { 100, 85, 155, 92 });
            plasticPercussionHammer.item.GetComponent<ITM_PercussionHammer>().squishTime = 13;

            ItemObject percussionHammer = new ItemBuilder(BasePlugin.instance.Info)
            .SetNameAndDescription("Itm_PercussionHammer", "Desc_PercussionHammer")
            .SetEnum(CustomItems.PercussionHammer.ToString())
            .SetShopPrice(500)
            .SetItemComponent<ITM_PercussionHammer>()
            .SetSprites("PercusionHammerV2")
            .SetGeneratorCost(62)
            .Build()
            .InitializeItem()
            .CreateItem(new string[] { "F2", "F3", "F4", "END" }, new int[] { 45, 50, 52, 55 })
            .AddIntoShop(new string[] { "F2", "F3", "F4", "END" }, new int[] { 60, 80, 75, 45 });
            percussionHammer.item.GetComponent<ITM_PercussionHammer>().squishTime = 30;

            ItemObject pretzel = new ItemBuilder(BasePlugin.instance.Info)
            .SetNameAndDescription("Itm_Pretzel", "Desc_Pretzel")
            .SetEnum(CustomItems.Pretzel.ToString())
            .SetShopPrice(350)
            .SetItemComponent<ITM_Pretzel>()
            .SetSprites("Pretzel")
            .SetGeneratorCost(25)
            .Build()
            .InitializeItem()
            .CreateItem(new string[] { "F2", "F3", "F4", "END" }, new int[] { 55, 80, 85, 110 })
            .AddIntoShop(new string[] { "F2", "F3", "F4", "END" }, new int[] { 70, 67, 15, 100 });

            ItemObject genericSoda = new ItemBuilder(BasePlugin.instance.Info)
            .SetNameAndDescription("Itm_GenericSoda", "Desc_GenericSoda")
            .SetEnum(CustomItems.GenericSoda.ToString())
            .SetShopPrice(315)
            .SetItemComponent<ITM_GenericSoda>()
            .SetSprites("GenericSoda")
            .SetGeneratorCost(30)
            .Build()
            .InitializeItem()
            .CreateItem(new string[] { "F1", "F2", "F3", "F4", "END" }, new int[] { 50, 30, 75, 100, 95 })
            .AddIntoShop(new string[] { "F1", "F2", "F3", "F4", "END" }, new int[] { 90, 70, 30, 10, 60 });

            ItemObject glue = new ItemBuilder(BasePlugin.instance.Info)
            .SetNameAndDescription("Itm_Glue", "Desc_Glue")
            .SetEnum(CustomItems.Glue.ToString())
            .SetShopPrice(415)
            .SetItemComponent<ITM_Glue>()
            .SetSprites("Glue")
            .SetGeneratorCost(42)
            .Build()
            .InitializeItem()
            .CreateItem(new string[] { "F1", "F2", "F3", "F4", "END" }, new int[] { 15, 80, 45, 54, 70 })
            .AddIntoShop(new string[] { "F1", "F2", "F3", "F4", "END" }, new int[] { 25, 55, 72, 48, 68 });

            ItemObject gum = new ItemBuilder(BasePlugin.instance.Info)
            .SetNameAndDescription("Itm_Gum", "Desc_Gum")
            .SetEnum(CustomItems.Gum.ToString())
            .SetShopPrice(325)
            .SetItemComponent<ITM_Gum>()
            .SetSprites("Gum")
            .SetGeneratorCost(25)
            .Build()
            .InitializeItem()
            .CreateItem(new string[] { "F1", "F2", "F3", "F4", "END" }, new int[] { 45, 20, 30, 40, 60 })
            .AddIntoShop(new string[] { "F1", "F2", "F3", "F4", "END" }, new int[] { 28, 60, 84, 10, 20 });

            ItemObject dietGum = new ItemBuilder(BasePlugin.instance.Info)
            .SetNameAndDescription("Itm_DietGum", "Desc_DietGum")
            .SetEnum(CustomItems.DietGum.ToString())
            .SetShopPrice(205)
            .SetItemComponent<ITM_Gum>()
            .SetSprites("DietGum")
            .SetGeneratorCost(25)
            .Build()
            .InitializeItem()
            .CreateItem(new string[] { "F1", "F2", "F3", "F4", "END" }, new int[] { 85, 42, 38, 55, 78 })
            .AddIntoShop(new string[] { "F1", "F2", "F3", "F4", "END" }, new int[] { 48, 82, 108, 32, 45 });
            dietGum.item.GetComponent<ITM_Gum>().diet = true;

            ItemObject traumatized = new ItemBuilder(BasePlugin.instance.Info)
            .SetNameAndDescription("Itm_Traumatized", "Desc_Traumatized")
            .SetEnum(CustomItems.Traumatized.ToString())
            .SetShopPrice(545)
            .SetItemComponent<ITM_Traumatized>()
            .SetSprites("Traumatized")
            .SetGeneratorCost(25)
            .Build();
            //AddCustomData<CD_Traumatized>();
            //.CreateItem(new string[] { "F1", "F2", "F3", "F4", "END" }, new int[] { 999950, 20, 30, 40, 60 })
            //.AddIntoShop(new string[] { "F1", "F2", "F3", "F4", "END" }, new int[] { 28, 999960, 84, 10, 20 });
            
            
            ItemObject present = new ItemBuilder(BasePlugin.instance.Info)
            .SetNameAndDescription("Itm_Present", "Desc_Present")
            .SetEnum(CustomItems.Present.ToString())
            .SetShopPrice(100)
            .SetItemComponent<ITM_Present>()
            .SetSprites("PresentIcon")
            .SetAsInstantUse()
            .Build()
            .AddIntoShop(new string[] { "F1", "F2", "F3", "F4", "END" }, new int[] { 45, 82, 70, 52, 100 });

            ItemObject AdvertenceBook = new ItemBuilder(BasePlugin.instance.Info)
            .SetNameAndDescription("Itm_AdvertenceBook", "Desc_AdvertenceBook")
            .SetEnum(CustomItems.AdvertenceBook.ToString())
            .SetShopPrice(300)
            .SetItemComponent<ITM_AdvertenceBook>()
            .SetSprites("AdvertenceBook")
            .SetGeneratorCost(50)
            .Build()
            .InitializeItem()
            .CreateItem(new string[] { "F2", "F3", "F4", "END" }, new int[] { 75, 55, 85, 45, 65 })
            .AddIntoShop(new string[] { "F2", "F3", "F4", "END" }, new int[] { 35, 45, 65, 85, 95 });

            ItemObject ConnectedAdvertenceBook = new ItemBuilder(BasePlugin.instance.Info)
            .SetNameAndDescription("Itm_ConnectedAdvertenceBook", "Desc_ConnectedAdvertenceBook")
            .SetEnum(CustomItems.ConnectedAddvertenceBook.ToString())
            .SetShopPrice(415)
            .SetItemComponent<ITM_AdvertenceBook>()
            .SetSprites("AdvertenceBookWithWhistle")
            .SetGeneratorCost(75)
            .Build()
            .InitializeItem()
            .CreateItem(new string[] { "F2", "F3", "F4", "END" }, new int[] { 35, 42, 48, 30, 48 })
            .AddIntoShop(new string[] { "F2", "F3", "F4", "END" }, new int[] { 35, 40, 60, 80, 88 });
            ConnectedAdvertenceBook.item.GetComponent<ITM_AdvertenceBook>().connected = true;

            ItemObject IceCreamMask = new ItemBuilder(BasePlugin.instance.Info)
            .SetNameAndDescription("Itm_IceCreamMask", "Desc_IceCreamMask")
            .SetEnum(CustomItems.IceCreamMask.ToString())
            .SetShopPrice(380)
            .SetItemComponent<ITM_IceCreamMask>()
            .SetSprites("IceCreamMask")
            .SetGeneratorCost(41)
            .Build()
            .InitializeItem()
            .CreateItem(new string[] { "F2", "F3", "F4", "END" }, new int[] { 80, 60, 50, 75 })
            .AddIntoShop(new string[] { "F2", "F3", "F4", "END" }, new int[] { 40, 60, 85, 85 });

            ItemObject Tea = new ItemBuilder(BasePlugin.instance.Info)
            .SetNameAndDescription("Itm_Tea", "Desc_Tea")
            .SetEnum(CustomItems.Tea.ToString())
            .SetShopPrice(420)
            .SetItemComponent<ITM_Tea>()
            .SetSprites("Tea")
            .SetGeneratorCost(70)
            .Build()
            .InitializeItem()
            .CreateItem(new string[] { "F2", "F3", "F4", "END" }, new int[] { 45, 40, 38, 58 })
            .AddIntoShop(new string[] { "F2", "F3", "F4", "END" }, new int[] { 66, 77, 55, 88 });

            ItemObject dietTea = new ItemBuilder(BasePlugin.instance.Info)
            .SetNameAndDescription("Itm_DietTea", "Desc_DietTea")
            .SetEnum(CustomItems.DietTea.ToString())
            .SetShopPrice(220)
            .SetItemComponent<ITM_Tea>()
            .SetSprites("DietTea")
            .SetGeneratorCost(70)
            .Build()
            .InitializeItem()
            .CreateItem(new string[] { "F1", "F2", "F3", "F4", "END" }, new int[] { 45, 48, 80, 55, 95 })
            .AddIntoShop(new string[] { "F1", "F2", "F3", "F4", "END" }, new int[] { 54, 67, 90, 62, 100 });
            dietTea.item.GetComponent<ITM_Tea>().diet = true;

            ItemObject cheese = new ItemBuilder(BasePlugin.instance.Info)
            .SetNameAndDescription("Itm_Cheese", "Desc_Cheese")
            .SetEnum(CustomItems.Cheese.ToString())
            .SetShopPrice(300)
            .SetItemComponent<ITM_Cheese>()
            .SetSprites("Cheese")
            .SetGeneratorCost(40)
            .Build()
            .InitializeItem()
            .CreateItem(new string[] { "F1", "F2", "F3", "F4", "END" }, new int[] { 60, 70, 45, 80, 90 })
            .AddIntoShop(new string[] { "F1", "F2", "F3", "F4", "END" }, new int[] { 40, 30, 20, 50, 70 });

            ItemObject horn = new ItemBuilder(BasePlugin.instance.Info)
            .SetNameAndDescription("Itm_Horn", "Desc_Horn")
            .SetEnum(CustomItems.Horn.ToString())
            .SetShopPrice(210)
            .SetItemComponent<ITM_Horn>()
            .SetSprites("Horn")
            .SetGeneratorCost(25)
            .Build()
            .InitializeItem()
            .CreateItem(new string[] { "F1", "F2", "F3", "F4", "END" }, new int[] { 80, 62, 100, 95, 114 })
            .AddIntoShop(new string[] { "F1", "F2", "F3", "F4", "END" }, new int[] { 30, 30, 55, 74, 100 });

            ItemObject cleanChalkEaser = new ItemBuilder(BasePlugin.instance.Info)
            .SetNameAndDescription("Itm_CleanChalkEaser", "Desc_CleanChalkEaser")
            .SetEnum(CustomItems.AlternativeChalkEaser.ToString())
            .SetShopPrice(420)
            .SetItemComponent<ITM_CleanChalkEaser>()
            .SetSprites("AlternativeChalkEaser")
            .SetGeneratorCost(35)
            .Build();
            //.CreateItem(new string[] { "F1", "F2", "F3", "F4", "END" }, new int[] { 15, 30, 27, 32, 47, 5 })
            //.AddIntoShop(new string[] { "F1", "F2", "F3", "F4", "END" }, new int[] { 30, 27, 42, 30, 17, 40 });

            ItemObject entityTeleporter = new ItemBuilder(BasePlugin.instance.Info)
            .SetNameAndDescription("Itm_EntityTel.", "Desc_EntityTel.")
            .SetEnum(CustomItems.EntityTeleporter.ToString())
            .SetShopPrice(340)
            .SetItemComponent<ITM_EntityTeleporter>()
            .SetSprites("NPCTeleport")
            .SetGeneratorCost(42)
            .Build()
            .CreateItem(new string[] { "F1", "F2", "F3", "F4", "END" }, new int[] { 1, 35, 47, 62, 64 })
            .AddIntoShop(new string[] { "F1", "F2", "F3", "F4", "END" }, new int[] { 1, 45, 42, 39, 82 });

            ItemObject eletricSoda = new ItemBuilder(BasePlugin.instance.Info)
            .SetNameAndDescription("Itm_EletricSoda", "Desc_EletricSoda")
            .SetEnum(CustomItems.EletricSoda.ToString())
            .SetShopPrice(450)
            .SetItemComponent<ITM_EletricSoda>()
            .SetSprites("EletricSoda")
            .SetGeneratorCost(52)
            .Build();
            //.AddCustomData<CD_EletricSoda>();
            //.CreateItem(new string[] { "F1", "F2", "F3", "F4", "END" }, new int[] { 99991, 30, 42, 50, 54 })
            //.AddIntoShop(new string[] { "F1", "F2", "F3", "F4", "END" }, new int[] { 1, 999940, 25, 30, 48 });

            ItemObject potion = new ItemBuilder(BasePlugin.instance.Info)
            .SetNameAndDescription("Itm_Potion", "Desc_Potion")
            .SetEnum(CustomItems.Potion.ToString())
            .SetShopPrice(540)
            .SetItemComponent<ITM_Potion>()
            .SetSprites("MysteryPotion")
            .SetGeneratorCost(52)
            .Build()
            .InitializeItem()
            .CreateItem(new string[] { "F1", "F2", "F3", "F4", "END" }, new int[] { 52, 52, 38, 45, 51 })
            .AddIntoShop(new string[] { "F1", "F2", "F3", "F4", "END" }, new int[] { 48, 51, 28, 54, 72 });

            ItemObject bag = new ItemBuilder(BasePlugin.instance.Info)
            .SetNameAndDescription("Itm_Bag", "Desc_Bag")
            .SetEnum(CustomItems.Bag.ToString())
            .SetShopPrice(560)
            .SetItemComponent<ITM_Bag>()
            .SetSprites("Bag")
            .SetGeneratorCost(80)
            .Build()
            .CreateItem(new string[] { "F1", "F2", "F3", "F4", "END" }, new int[] { 20, 25, 12, 5, 40 })
            .AddIntoShop(new string[] { "F1", "F2", "F3", "F4", "END" }, new int[] { 60, 50, 24, 10, 80 });

            bag.item.GetComponent<ITM_Bag>().open = true;

            ItemObject bagOpen = new ItemBuilder(BasePlugin.instance.Info)
            .SetNameAndDescription("Itm_BagOpen", "Desc_Nothing")
            .SetEnum(CustomItems.OpenBag.ToString())
            .SetShopPrice(560)
            .SetItemComponent<ITM_Bag>()
            .SetSprites("BagOpen")
            .SetGeneratorCost(80)
            .Build();

            /*
            ItemObject compass = new ItemBuilder(BasePlugin.instance.Info)
            .SetNameAndDescription("Itm_Compass", "Desc_Compass")
            .SetEnum(CustomItems.Compass.ToString())
            .SetShopPrice(399)
            .SetItemComponent<ITM_Compass>()
            .SetSprites("Compass")
            .SetGeneratorCost(25)
            .Build()
            .AddCustomData<CD_Compass>()
            .CreateItem(new string[] { "F1", "F2", "F3", "F4", "END" }, new int[] { 999930, 25, 12, 5, 40 })
            .AddIntoShop(new string[] { "F1", "F2", "F3", "F4", "END" }, new int[] { 60, 999950, 24, 10, 80 });
            */

            ItemObject shovel = new ItemBuilder(BasePlugin.instance.Info)
            .SetNameAndDescription("Itm_Shovel", "Desc_Shovel")
            .SetEnum(CustomItems.Shovel.ToString())
            .SetShopPrice(405)
            .SetItemComponent<ITM_Shovel>()
            .SetSprites("Shovel")
            .SetGeneratorCost(45)
            .Build()
            .InitializeItem()
            .CreateItem(new string[] { "F1", "F2", "F3", "F4", "END" }, new int[] { 50, 35, 50, 44, 70 })
            .AddIntoShop(new string[] { "F1", "F2", "F3", "F4", "END" }, new int[] { 30, 28, 30, 33, 48 });

            /*
            ItemObject bomb = new ItemBuilder(BasePlugin.instance.Info)
            .SetNameAndDescription("Itm_Bomb", "Desc_Bomb")
            .SetEnum(CustomItems.Bomb.ToString())
            .SetShopPrice(300)
            .SetItemComponent<ITM_Bomb>()
            .SetSprites("Bomb")
            .SetGeneratorCost(25)
            .Build()
            //.AddCustomData<CD_Shovel>()
            .CreateItem(new string[] { "F1", "F2", "F3", "F4", "END" }, new int[] { 999950, 35, 50, 44, 70 })
            .AddIntoShop(new string[] { "F1", "F2", "F3", "F4", "END" }, new int[] { 30, 9999928, 30, 33, 48 });
            */

            ItemObject hallPass = new ItemBuilder(BasePlugin.instance.Info)
            .SetNameAndDescription("Itm_HallPass", "Desc_HallPass")
            .SetEnum(CustomItems.HallPass.ToString())
            .SetShopPrice(430)
            .SetItemComponent<Item>()
            .SetSprites("HallPass")
            .SetGeneratorCost(25)
            .Build()
            .CreateItem(new string[] { "F2", "F3", "F4", "END" })
            .AddIntoShop(new string[] { "F1", "F2", "F3", "F4", "END" }, new int[] { 40, 50, 60, 70, 14 });

            ItemObject bullyPresent = new ItemBuilder(BasePlugin.instance.Info)
            .SetNameAndDescription("Itm_BullyPresent", "Desc_BullyPresent")
            .SetEnum(CustomItems.BullyPresent.ToString())
            .SetShopPrice(300)
            .SetItemComponent<Item>()
            .SetSprites("BullyPresent")
            .SetGeneratorCost(45)
            .Build()
            .CreateItem(new string[] { "F1", "F2", "F3", "F4", "END" }, new int[] { 50, 70, 62, 55, 80 })
            .AddIntoShop(new string[] { "F1", "F2", "F3", "F4", "END" }, new int[] { 30, 40, 42, 35, 40 });

            ItemObject soup = new ItemBuilder(BasePlugin.instance.Info)
            .SetNameAndDescription("Itm_Soup", "Desc_Soup")
            .SetEnum(CustomItems.SoupInCan.ToString())
            .SetShopPrice(70)
            .SetItemComponent<ITM_SoupCan>()
            .SetSprites("soup")
            .SetGeneratorCost(30)
            .Build()
            .InitializeItem()
            .CreateItem(new string[] { "F1", "F2", "F3", "F4", "END" }, new int[] { 90, 100, 70, 80, 111 })
            .AddIntoShop(new string[] { "F1", "F2", "F3", "F4", "END" }, new int[] { 70, 65, 100, 70, 102 });

            ItemObject whiteZesty = new ItemBuilder(BasePlugin.instance.Info)
            .SetNameAndDescription("Itm_ZestyFlavoredWhite", "Desc_ZestyFlavoredWhite")
            .SetEnum(CustomItems.WhiteZesty.ToString())
            .SetShopPrice(250)
            .SetItemComponent<ITM_ZestyWhiteFlavored>()
            .SetSprites("WhiteZestyBarIcon")
            .SetGeneratorCost(45)
            .Build()
            .InitializeItem()
            .CreateItem(new string[] { "F1", "F2", "F3", "F4", "END" }, new int[] { 45, 70, 55, 60, 38 })
            .AddIntoShop(new string[] { "F1", "F2", "F3", "F4", "END" }, new int[] { 50, 75, 60, 65, 97 });

            ItemObject ductTape = new ItemBuilder(BasePlugin.instance.Info)
            .SetNameAndDescription("Itm_DuctTape", "Desc_DuctTape")
            .SetEnum(CustomItems.DuctTape.ToString())
            .SetShopPrice(500)
            .SetItemComponent<ITM_DuctTape>()
            .SetSprites("Ducttape")
            .SetGeneratorCost(35)
            .Build()
            .InitializeItem()
            .CreateItem(new string[] { "F2", "F3", "F4", "END" }, new int[] { 30, 22, 40, 38, 50 })
            .AddIntoShop(new string[] { "F2", "F3", "F4", "END" }, new int[] { 20, 12, 30, 24, 25 });

            ItemObject walkman = new ItemBuilder(BasePlugin.instance.Info)
            .SetNameAndDescription("Itm_Walkman", "Desc_Walkman")
            .SetEnum(CustomItems.Walkman.ToString())
            .SetShopPrice(380)
            .SetItemComponent<ITM_Walkman>()
            .SetSprites("PortableTape")
            .SetGeneratorCost(55)
            .Build()
            .InitializeItem()
            .CreateItem(new string[] { "F1", "F2", "F3", "F4", "END" }, new int[] { 53, 48, 40, 50, 64 })
            .AddIntoShop(new string[] { "F1", "F2", "F3", "F4", "END" }, new int[] { 70, 75, 88, 98, 100 });
        }
    }
}
