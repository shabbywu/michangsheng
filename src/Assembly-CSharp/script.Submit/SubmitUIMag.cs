using System;
using System.Collections.Generic;
using Bag;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace script.Submit;

public class SubmitUIMag : MonoBehaviour, IESCClose
{
	public static SubmitUIMag Inst;

	public SubmitBag Bag;

	public UnityAction SubmitAction;

	public List<SubmitSlot> SlotList;

	public Func<BaseItem, bool> CanPut;

	public Text ChengHao;

	public Text Name;

	public Text Title;

	private void Awake()
	{
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		ChengHao.text = PlayerEx.GetMenPaiChengHao();
		Name.text = Tools.instance.getPlayer().name;
		Inst = this;
		((Component)Inst).transform.SetParent(((Component)NewUICanvas.Inst).gameObject.transform);
		((Component)Inst).transform.localScale = Vector3.one;
		((Component)Inst).transform.localPosition = Vector3.zero;
		((Component)Inst).transform.SetAsLastSibling();
		ESCCloseManager.Inst.RegisterClose(this);
	}

	public void OpenLianQi(Func<BaseItem, bool> canPut, UnityAction submit, string title, int num)
	{
		SubmitAction = submit;
		CanPut = canPut;
		Title.SetText(title);
		Bag.ItemType = global::Bag.ItemType.法宝;
		Bag.EquipType = EquipType.装备;
		Bag.Open();
		InitSlotList(num);
	}

	private void InitSlotList(int num)
	{
		Transform val = ((Component)this).transform.Find($"提交/物品列表{num}");
		((Component)val).gameObject.SetActive(true);
		SlotList = new List<SubmitSlot>();
		for (int i = 0; i < val.childCount; i++)
		{
			SlotList.Add(((Component)val.GetChild(i)).GetComponent<SubmitSlot>());
		}
		foreach (SubmitSlot slot in SlotList)
		{
			slot.InitUI();
			slot.SetNull();
		}
	}

	public void PutItem(SubmitSlot dragSlot, SubmitSlot toSlot = null)
	{
		if (!dragSlot.Item.CanSale)
		{
			UIPopTip.Inst.Pop("此物品无法交易");
		}
		if ((Object)(object)toSlot == (Object)null)
		{
			toSlot = GetNullSlot();
			if ((Object)(object)toSlot == (Object)null)
			{
				UIPopTip.Inst.Pop("已达提交上限");
				return;
			}
		}
		if (!toSlot.IsNull())
		{
			Bag.AddTempItem(toSlot.Item, 1);
		}
		toSlot.SetSlotData(dragSlot.Item.Clone());
		toSlot.Item.Count = 1;
		toSlot.UpdateUI();
		Bag.RemoveTempItem(dragSlot.Item.Uid, 1);
		Bag.UpdateItem(flag: true);
	}

	public void BackItem(SubmitSlot dragSlot, SubmitSlot toSlot = null)
	{
		if ((Object)(object)toSlot == (Object)null)
		{
			SlotBase nullBagSlot = Bag.GetNullBagSlot(dragSlot.Item.Uid);
			if ((Object)(object)nullBagSlot != (Object)null)
			{
				toSlot = (SubmitSlot)nullBagSlot;
			}
		}
		if ((Object)(object)toSlot == (Object)null || toSlot.IsNull())
		{
			if ((Object)(object)toSlot != (Object)null)
			{
				toSlot.SetSlotData(dragSlot.Item);
			}
			Bag.AddTempItem(dragSlot.Item, 1);
			dragSlot.SetNull();
		}
		else
		{
			BaseItem slotData = dragSlot.Item.Clone();
			dragSlot.SetSlotData(toSlot.Item);
			toSlot.SetSlotData(slotData);
			Bag.AddTempItem(dragSlot.Item, 1);
			Bag.RemoveTempItem(toSlot.Item.Uid, 1);
		}
		Bag.UpdateItem(flag: true);
	}

	public SubmitSlot GetNullSlot()
	{
		foreach (SubmitSlot slot in SlotList)
		{
			if (slot.IsNull())
			{
				return slot;
			}
		}
		return null;
	}

	public void Close()
	{
		Object.Destroy((Object)(object)((Component)this).gameObject);
	}

	public void Submit()
	{
		if (!CheckPutAll())
		{
			UIPopTip.Inst.Pop("数量不足");
			return;
		}
		foreach (SubmitSlot slot in SlotList)
		{
			Tools.instance.RemoveItem(slot.Item.Uid);
		}
		UnityAction submitAction = SubmitAction;
		if (submitAction != null)
		{
			submitAction.Invoke();
		}
		Object.Destroy((Object)(object)((Component)this).gameObject);
	}

	public bool CheckPutAll()
	{
		foreach (SubmitSlot slot in SlotList)
		{
			if (slot.IsNull())
			{
				return false;
			}
		}
		return true;
	}

	private void OnDestroy()
	{
		Inst = null;
		ESCCloseManager.Inst.UnRegisterClose(this);
	}

	public bool TryEscClose()
	{
		Close();
		return true;
	}
}
