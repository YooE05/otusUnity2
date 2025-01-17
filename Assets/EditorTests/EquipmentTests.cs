using Homework.Inventory;
using NUnit.Framework;
using UnityEngine;

public sealed class EquipmentTests
{
    private Inventory _inventory;
    private InventoryItemConfig _glove;
    private InventoryItemConfig _sword;
    private InventoryItemConfig _helmet;
    private InventoryItemConfig _hood;

    [SetUp]
    public void Setup()
    {
        _inventory = new Inventory();

        _glove = EquipTestHelper.CreateItemConfig("glove",
            EquipablePlayerParts.RightHand | EquipablePlayerParts.LeftHand);

        _sword = EquipTestHelper.CreateItemConfig("sword",
            EquipablePlayerParts.RightHand | EquipablePlayerParts.LeftHand);

        _helmet = EquipTestHelper.CreateItemConfig("helmet", EquipablePlayerParts.Head);
        _hood = EquipTestHelper.CreateItemConfig("hood", EquipablePlayerParts.Head);
    }

    [Test]
    public void WhenEquipItem_AndSlotIsOccupiedByAnotherItem_ThenSlotBecomeOccupiedByNewItem()
    {
        //Arrange
        _inventory.TryAddItem(_helmet.GetClone());
        _inventory.TryEquipItem(_helmet.GetClone());

        _inventory.TryAddItem(_hood.GetClone());

        //Act
        _inventory.TryEquipItem(_hood.GetClone());

        //Assert
        Assert.True(_inventory.GetBodyPartItem(EquipablePlayerParts.Head).Name == _hood.GetClone().Name);
    }

    [Test]
    public void WhenEquipItem_AndSlotIsEmpty_ThenItemIsEquippedOnCorrectSlot()
    {
        //Arrange
        _inventory.TryAddItem(_glove.GetClone());

        //Act
        _inventory.TryEquipItem(_glove.GetClone());

        //Assert
        var result = EquipTestHelper.ItemEquippedInNeedCount(_inventory, _glove, 1);
        Assert.True(result);
    }

    [Test]
    public void WhenEquipOneItemFewTimes_AndCountOfItemsLowerThanSlotsCounts_ThenEquippedItemCountStayTheSame()
    {
        //Arrange
        _inventory.TryAddItem(_glove.GetClone());
        _inventory.TryEquipItem(_glove.GetClone());

        //Act
        _inventory.TryEquipItem(_glove.GetClone());

        //Assert
        var result = EquipTestHelper.ItemEquippedInNeedCount(_inventory, _glove, 1);
        Assert.True(result);
    }

    [Test]
    public void WhenEquipOneItemFewTimes_AndCountOfItemsBiggerOrEqualSlotsCounts_ThenEquippedItemCountIncrease()
    {
        //Arrange
        _inventory.TryAddItem(_glove.GetClone());
        _inventory.TryAddItem(_glove.GetClone());
        _inventory.TryAddItem(_glove.GetClone());

        _inventory.TryEquipItem(_glove.GetClone());

        //Act
        _inventory.TryEquipItem(_glove.GetClone());

        //Assert
        var result = EquipTestHelper.ItemEquippedInNeedCount(_inventory, _glove, 2);
        Assert.True(result);
    }

    [Test]
    public void
        WhenEquipItem_AndAllSlotsOccupiedAndHasSlotsOccupiedByItemWithAnotherType_ThenSlotWithAnotherItemOccupiedByNewItem()
    {
        //Arrange
        _inventory.TryAddItem(_glove.GetClone());
        _inventory.TryAddItem(_glove.GetClone());
        _inventory.TryAddItem(_sword.GetClone());

        _inventory.TryEquipItem(_glove.GetClone());
        _inventory.TryEquipItem(_sword.GetClone());

        //Act
        _inventory.TryEquipItem(_glove.GetClone());

        //Assert
        var result = EquipTestHelper.ItemEquippedInNeedCount(_inventory, _glove, 2);
        Assert.True(result);
    }

    [Test]
    public void WhenRemoveItem_AndItemCountMoreThanEquippedItemCount_ThenEquippedCountTheSame()
    {
        //Arrange
        _inventory.TryAddItem(_glove.GetClone());
        _inventory.TryAddItem(_glove.GetClone());
        _inventory.TryAddItem(_glove.GetClone());
        _inventory.TryAddItem(_glove.GetClone());

        _inventory.TryEquipItem(_glove.GetClone());
        _inventory.TryEquipItem(_glove.GetClone());

        //Act
        _inventory.TryRemoveItem(_glove.GetClone());

        //Assert
        var result = EquipTestHelper.ItemEquippedInNeedCount(_inventory, _glove, 2);
        Assert.True(result);
    }

    [Test]
    public void WhenRemoveItem_AndItemCountEqualThanEquippedItemCount_ThenItemUnequippedAndRemoved()
    {
        //Arrange
        _inventory.TryAddItem(_glove.GetClone());
        _inventory.TryAddItem(_glove.GetClone());

        _inventory.TryEquipItem(_glove.GetClone());
        _inventory.TryEquipItem(_glove.GetClone());

        //Act
        _inventory.TryRemoveItem(_glove.GetClone());

        //Assert
        var isNedCountEquipped = EquipTestHelper.ItemEquippedInNeedCount(_inventory, _glove, 1);
        var result = isNedCountEquipped && EquipTestHelper.HasNeededCountOfItems(_inventory, _glove, 1);
        Assert.True(result);
    }


    [Test]
    public void WhenUnequipItem_AndThisItemEquipped_ThenItemSlotBecomeEmptyAnd()
    {
        //Arrange
        _inventory.TryAddItem(_glove.GetClone());
        _inventory.TryEquipItem(_glove.GetClone());

        //Act
        _inventory.TryUnequipItem(_glove.GetClone());

        //Assert
        var result = EquipTestHelper.ItemEquippedInNeedCount(_inventory, _glove, 0);
        Assert.True(result);
    }
}

public static class EquipTestHelper
{
    public static InventoryItemConfig CreateItemConfig(string name, EquipablePlayerParts itemEquipableParts)
    {
        var equipComponent = new EquipComponent {EquipableParts = itemEquipableParts};
        IItemComponent[] itemComponents = {equipComponent};

        var itemConfig = ScriptableObject.CreateInstance<InventoryItemConfig>();
        itemConfig.Prototype = new InventoryItem(name, itemComponents);

        return itemConfig;
    }

    public static bool HasNeededCountOfItems(Inventory inventory, InventoryItemConfig itemConfig, int neededCount)
    {
        var result = inventory.FindItem(itemConfig.GetClone()).Count == neededCount;
        return result;
    }

    public static bool ItemEquippedInNeedCount(Inventory inventory, InventoryItemConfig itemConfig, int count)
    {
        var equipablePartsList =
            InventoryUseCases.GetEquipablePartsList(itemConfig.Prototype.GetComponent<EquipComponent>());

        var needAmountOfEquippedItem =
            equipablePartsList.FindAll(part =>
            {
                if (inventory.GetBodyPartItem(part) != null)
                {
                    return inventory.GetBodyPartItem(part).Name == itemConfig.Prototype.Name;
                }

                return false;
            }).Count == count;

        var result = needAmountOfEquippedItem &&
                     inventory.FindItem(itemConfig.GetClone()).GetComponent<EquipComponent>().EquippedCount == count;

        return result;
    }
}