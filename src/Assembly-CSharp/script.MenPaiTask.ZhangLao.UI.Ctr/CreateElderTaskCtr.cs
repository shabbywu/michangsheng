using System.Collections.Generic;
using Bag;
using KBEngine;
using UnityEngine;
using script.MenPaiTask.ZhangLao.UI.Base;
using script.MenPaiTask.ZhangLao.UI.UI;

namespace script.MenPaiTask.ZhangLao.UI.Ctr;

public class CreateElderTaskCtr
{
	public List<ElderTaskSlot> SlotList;

	public int ItemCount = 5;

	public int ItemIndex = 1;

	public int X = 176;

	public int NeedMoney;

	public int NeedReputation;

	public int MaxNum = 10;

	public CreateElderTaskUI UI => ElderTaskUIMag.Inst.CreateElderTaskUI;

	private Avatar player => Tools.instance.getPlayer();

	public void CreateItemList()
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		SlotList = new List<ElderTaskSlot>();
		int num = (int)UI.ItemPrefab.transform.localPosition.x;
		int num2 = (int)UI.ItemPrefab.transform.localPosition.y;
		for (int i = ItemIndex; i <= ItemCount; i++)
		{
			GameObject obj = UI.ItemPrefab.Inst(UI.ItemParent);
			obj.transform.localPosition = Vector2.op_Implicit(new Vector2((float)num, (float)num2));
			ElderTaskSlot component = obj.GetComponent<ElderTaskSlot>();
			component.SetNull();
			SlotList.Add(component);
			obj.SetActive(true);
			num += X;
		}
	}

	public void PutItem(ElderTaskSlot dragSlot)
	{
		ElderTaskSlot toSlot = ElderTaskUIMag.Inst.Bag.ToSlot;
		if (!dragSlot.Item.IsEqual(toSlot.Item))
		{
			USelectNum.Show(dragSlot.Item.GetName() + " x{num}", 1, MaxNum, delegate(int num)
			{
				BaseItem baseItem = dragSlot.Item.Clone();
				baseItem.Count = num;
				toSlot.SetSlotData(baseItem);
				ElderTaskUIMag.Inst.Bag.Hide();
				UpdateData();
				UI.UpdateUI();
			});
		}
		else if (toSlot.Item.Count >= MaxNum)
		{
			UIPopTip.Inst.Pop("每个格子最多只能放" + MaxNum + "个");
		}
		else
		{
			USelectNum.Show(dragSlot.Item.GetName() + " x{num}", 1, MaxNum - toSlot.Item.Count, delegate(int num)
			{
				toSlot.Item.Count += num;
				toSlot.UpdateUI();
				ElderTaskUIMag.Inst.Bag.Hide();
				UpdateData();
				UI.UpdateUI();
			});
		}
	}

	public void BackItem(ElderTaskSlot dragSlot)
	{
		if (dragSlot.IsNull() || dragSlot.Item.Count <= 0)
		{
			UIPopTip.Inst.Pop("物品数目为空");
			return;
		}
		if (dragSlot.Item.Count == 1)
		{
			dragSlot.SetNull();
			UI.UpdateUI();
			return;
		}
		USelectNum.Show(dragSlot.Item.GetName() + " x{num}", 1, dragSlot.Item.Count, delegate(int num)
		{
			if (num <= dragSlot.Item.Count)
			{
				dragSlot.Item.Count -= num;
				if (dragSlot.Item.Count == 0)
				{
					dragSlot.SetNull();
				}
				else
				{
					dragSlot.UpdateUI();
				}
				UpdateData();
				UI.UpdateUI();
			}
			else
			{
				UIPopTip.Inst.Pop("超过当前物品数目");
			}
		});
	}

	private void UpdateData()
	{
		NeedMoney = 0;
		NeedReputation = 0;
		foreach (ElderTaskSlot slot in SlotList)
		{
			if (!slot.IsNull())
			{
				NeedMoney += player.ElderTaskMag.GetNeedMoney(slot.Item);
				NeedReputation++;
			}
		}
	}

	public void PublishTask()
	{
		if (player.ElderTaskMag.PlayerAllotTask(SlotList))
		{
			ElderTaskUIMag.Inst.ElderTaskUI.Ctr.CreateTaskList();
			ClearItemList();
			ElderTaskUIMag.Inst.OpenElderTaskUI();
			UIPopTip.Inst.Pop("发布任务成功");
		}
	}

	private bool IsNull()
	{
		foreach (ElderTaskSlot slot in SlotList)
		{
			if (!slot.IsNull())
			{
				return false;
			}
		}
		return true;
	}

	public string CreateTaskDesc()
	{
		if (IsNull())
		{
			return "";
		}
		string text = $"本门长老愿以{NeedMoney}灵石求购";
		foreach (ElderTaskSlot slot in SlotList)
		{
			if (!slot.IsNull())
			{
				text += $"{slot.Item.GetName()} x{slot.Item.Count},";
			}
		}
		text = text.Remove(text.Length - 1, 1);
		return text + "。";
	}

	public void ClearItemList()
	{
		foreach (ElderTaskSlot slot in SlotList)
		{
			slot.SetNull();
		}
		UpdateData();
		UI.UpdateUI();
	}
}
