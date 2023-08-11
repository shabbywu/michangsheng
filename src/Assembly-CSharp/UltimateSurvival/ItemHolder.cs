using System;
using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival;

[Serializable]
public class ItemHolder
{
	public Message<ItemHolder> Updated = new Message<ItemHolder>();

	public ulong uuid;

	public int index;

	public int itemID;

	public bool HasItem => CurrentItem != null;

	public SavableItem CurrentItem { get; private set; }

	public static implicit operator bool(ItemHolder holder)
	{
		return holder != null;
	}

	public bool TryAddItem(ItemData itemData, int amount, out int added, List<ItemProperty.Value> customPropertyValues = null, ulong _uuid = 0uL, int _index = 0)
	{
		added = 0;
		if (HasItem && itemData.Id != CurrentItem.Id)
		{
			return false;
		}
		if (!HasItem)
		{
			CurrentItem = new SavableItem(itemData, 1, customPropertyValues);
			CurrentItem.CurrentInStack = 0;
			CurrentItem.PropertyChanged.AddListener(On_PropertyChanged);
			CurrentItem.StackChanged.AddListener(On_StackChanged);
			uuid = _uuid;
			index = _index;
			itemID = itemData.Id;
		}
		int currentInStack = CurrentItem.CurrentInStack;
		int num = amount + currentInStack - itemData.StackSize;
		int num2 = currentInStack;
		num2 = ((num > 0) ? itemData.StackSize : (num2 + amount));
		CurrentItem.CurrentInStack = num2;
		added = num2 - currentInStack;
		Updated.Send(this);
		return added > 0;
	}

	public void SetItem(SavableItem item)
	{
		if ((bool)CurrentItem)
		{
			CurrentItem.PropertyChanged.RemoveListener(On_PropertyChanged);
			CurrentItem.StackChanged.RemoveListener(On_StackChanged);
		}
		CurrentItem = item;
		if ((bool)CurrentItem)
		{
			CurrentItem.PropertyChanged.AddListener(On_PropertyChanged);
			CurrentItem.StackChanged.AddListener(On_StackChanged);
		}
		Updated.Send(this);
	}

	public void RemoveFromStack(int amount, out int removed)
	{
		removed = 0;
		if (HasItem)
		{
			if (amount >= CurrentItem.CurrentInStack)
			{
				removed = CurrentItem.CurrentInStack;
				SetItem(null);
				return;
			}
			int currentInStack = CurrentItem.CurrentInStack;
			CurrentItem.CurrentInStack = Mathf.Max(CurrentItem.CurrentInStack - amount, 0);
			removed = currentInStack - CurrentItem.CurrentInStack;
			Updated.Send(this);
		}
	}

	public void RemoveFromStack(int amount)
	{
		if (HasItem)
		{
			_ = CurrentItem.CurrentInStack;
			CurrentItem.CurrentInStack = Mathf.Max(CurrentItem.CurrentInStack - amount, 0);
			Updated.Send(this);
		}
	}

	private void On_PropertyChanged(ItemProperty.Value propertyValue)
	{
		Updated.Send(this);
	}

	private void On_StackChanged()
	{
		Updated.Send(this);
	}
}
