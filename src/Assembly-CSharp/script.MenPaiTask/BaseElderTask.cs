using System.Collections.Generic;
using Bag;
using UnityEngine;
using script.MenPaiTask.ZhangLao.UI;

namespace script.MenPaiTask;

public class BaseElderTask : UIBase
{
	public List<BaseSlot> Slots;

	public int Count = 5;

	public ElderTask ElderTask;

	protected virtual void Init()
	{
		Slots = new List<BaseSlot>();
		for (int i = 1; i <= Count; i++)
		{
			Slots.Add(Get<BaseSlot>($"任务物品列表/{i}"));
			Slots[i - 1].SetNull();
			((Component)Slots[i - 1]).gameObject.SetActive(false);
		}
		InitItemList(ElderTask.needItemList);
	}

	public static T Create<T>(ElderTask data, GameObject gameObject) where T : BaseElderTask, new()
	{
		T obj = new T
		{
			_go = gameObject
		};
		obj._go.SetActive(true);
		obj.ElderTask = data;
		obj.Init();
		return obj;
	}

	public static T Create<T>(ElderTask data, Transform transform) where T : BaseElderTask, new()
	{
		T obj = new T
		{
			_go = ((Component)transform).gameObject
		};
		obj._go.SetActive(true);
		obj.ElderTask = data;
		obj.Init();
		return obj;
	}

	public virtual void DestroySelf()
	{
		ElderTaskUIMag.Inst.ElderTaskUI.Ctr.TaskList.Remove(this);
		Object.Destroy((Object)(object)_go);
	}

	private void InitItemList(List<BaseItem> list)
	{
		for (int i = 0; i < list.Count; i++)
		{
			Slots[i].SetSlotData(list[i].Clone());
			((Component)Slots[i]).gameObject.SetActive(true);
		}
	}
}
