using bbpfer.Enums;
using MTM101BaldAPI;
using bbpfer.CustomContent.Structures.Builders;
using UnityEngine;
using bbpfer.Extessions;
using PixelInternalAPI.Extensions;
using MTM101BaldAPI.Registers;

namespace bbpfer.FundamentalManagers.ExternalLoaders
{
    public partial class StructuresCreator
    {
        public static void Load()
        {
            ClockBuilder clock = new GameObject("ClockBuilder").AddComponent<ClockBuilder>();
            clock.obstacle = Obstacle.Null;
            clock.gameObject.ConvertToPrefab(true);
            clock.AddStructure(new string[] { "F1", "F2", "F3", "F4", "END" }, null);

            GameObject gameObject = new GameObject("SwingDoorEletricBuilder");
            var eletricDoor = gameObject.AddComponent<EletricSwingDoorBuilder>();
            eletricDoor.obstacle = EnumExtensions.ExtendEnum<Obstacle>(Structures.EletricDoor.ToString());
            eletricDoor.gameObject.ConvertToPrefab(true);
            //eletricDoor.AddStructure(new string[] { "F1" }, null);

            CreateVendingMachine("sodaMachine", "SodaMachine", new int[] { 1, 1 }, new string[] { "F1", "F2", "F3", "F4", "END" }, new int[] { 70, 60, 50, 40, 60 }, CustomItems.Soda, 2);
            CreateVendingMachine("cookieMachine", "CookieMachine", new int[] { 1, 4 }, new string[] { "F1", "F2", "F3", "F4", "END" }, new int[] { 100, 50, 80, 20, 95 }, CustomItems.Cookie);
            CreateVendingMachine("genericSodaMachine", "GenericSodaMachine", new int[] { 1, 3 }, new string[] { "F1", "F3", "F4", "END" }, new int[] { 80, 60, 74, 45 }, CustomItems.GenericSoda); 
            CreateVendingMachine("iceCreamMachine", "IceCreamMachine", new int[] { 1, 2 }, new string[] { "F2", "F3", "F4", "END" }, new int[] { 75, 40, 60, 70 }, CustomItems.IceCream);
            CreateVendingMachine("teaMachine", "TeaMachine", new int[] { 1, 3 }, new string[] { "F2", "F3", "F4", "END" }, new int[] { 120, 100, 80, 110 }, CustomItems.Tea, 1, new WeightedItemObject[] {
                new WeightedItemObject { selection = ItemMetaStorage.Instance.FindByEnum(EnumExtensions.GetFromExtendedName<Items>(CustomItems.Tea.ToString())).value, weight = 50 },
                new WeightedItemObject { selection = ItemMetaStorage.Instance.FindByEnum(EnumExtensions.GetFromExtendedName<Items>(CustomItems.DietTea.ToString())).value, weight = 100 }
            });
        }

        public static void CreateVendingMachine(string name, string textureName, int[] amount, string[] floors, int[] weight, CustomItems item = CustomItems.Null, int uses = 1, WeightedItemObject[] randomItem = null)
        {
            SodaMachine machine = ObjectCreationExtensions.CreateSodaMachineInstance(AssetsCreator.CreateTexture(textureName, "VendingMachines"), AssetsCreator.CreateTexture(textureName + "_Out", "VendingMachines"), true);
            machine.SetRequiredItems(Items.Quarter);

            if (randomItem == null)
                machine.SetPotentialItems(new WeightedItemObject[] { new WeightedItemObject { selection = ItemMetaStorage.Instance.FindByEnum(EnumExtensions.GetFromExtendedName<Items>(item.ToString())).value, weight = 120 } });
            else
                machine.SetPotentialItems(randomItem);
            machine.SetUses(uses);
            AssetsCreator.assetMan.Add<SodaMachine>(name, machine);

            var vendingMachineBuilder = new GameObject($"{name}MachineBuilder{"Custom"}").AddComponent<GenericHallBuilder>();

            ObjectBuilderMetaStorage.Instance.Add(new ObjectBuilderMeta(BasePlugin.instance.Info, vendingMachineBuilder));

            vendingMachineBuilder.SetObjectPlacer(
                ObjectCreationExtensions.SetANewObjectPlacer(
                    machine.gameObject,
                    CellCoverage.North | CellCoverage.Down, TileShape.Closed, TileShape.Single, TileShape.Straight, TileShape.Corner, TileShape.End)
                    .SetMinAndMaxObjects(amount[0], amount[1])
                    .SetTilePreferences(true, false, true)
                );

            vendingMachineBuilder.gameObject.ConvertToPrefab(true);

            int weights = 0;
            foreach (string data in floors)
            {
                FundamentalManager.GetFloorByName(data).specialObjectBuilders.Add(new WeightedObjectBuilder { selection = vendingMachineBuilder, weight = weight[weights] });
                weights++;
            }
        }
    }
}
