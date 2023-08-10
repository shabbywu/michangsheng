using System;
using System.Collections.Generic;
using Bag;
using JSONClass;
using KBEngine;
using UnityEngine;
using script.MenPaiTask.ZhangLao.ElderTaskSys;
using script.MenPaiTask.ZhangLao.UI.Base;

namespace script.MenPaiTask;

[Serializable]
public class ElderTaskMag
{
	private List<ElderTask> waitAcceptTaskList = new List<ElderTask>();

	private List<ElderTask> executingTaskList = new List<ElderTask>();

	private List<ElderTask> completeTaskList = new List<ElderTask>();

	[NonSerialized]
	private List<int> canAccpetNpcIdList = new List<int>();

	private List<int> executingTaskNpcIdList = new List<int>();

	[NonSerialized]
	public AllotTask AllotTask;

	[NonSerialized]
	public UpdateTaskProcess UpdateTaskProcess;

	[NonSerialized]
	private List<int> itemList = new List<int>();

	private Avatar player => Tools.instance.getPlayer();

	public void Init()
	{
		if (waitAcceptTaskList == null)
		{
			waitAcceptTaskList = new List<ElderTask>();
		}
		if (executingTaskList == null)
		{
			executingTaskList = new List<ElderTask>();
		}
		if (completeTaskList == null)
		{
			completeTaskList = new List<ElderTask>();
		}
		if (canAccpetNpcIdList == null)
		{
			canAccpetNpcIdList = new List<int>();
		}
		if (executingTaskNpcIdList == null)
		{
			executingTaskNpcIdList = new List<int>();
		}
		if (AllotTask == null)
		{
			AllotTask = new AllotTask();
		}
		if (UpdateTaskProcess == null)
		{
			UpdateTaskProcess = new UpdateTaskProcess();
		}
		if (itemList != null && itemList.Count >= 1)
		{
			return;
		}
		itemList = new List<int>();
		Dictionary<int, ElderTaskItemType> dataDict = ElderTaskItemType.DataDict;
		foreach (_ItemJsonData data in _ItemJsonData.DataList)
		{
			if (data.id >= jsonData.QingJiaoItemIDSegment)
			{
				break;
			}
			if (dataDict.ContainsKey(data.type) && dataDict[data.type].quality.Contains(data.quality))
			{
				itemList.Add(data.id);
			}
		}
		foreach (ElderTaskDisableItem data2 in ElderTaskDisableItem.DataList)
		{
			itemList.Remove(data2.id);
		}
	}

	public List<int> GetCanNeedItemList()
	{
		return itemList;
	}

	public bool CheckCanAllotTask(int money, int reputation)
	{
		if ((int)player.money >= money)
		{
			return PlayerEx.GetShengWang(player.menPai) >= reputation;
		}
		return false;
	}

	public bool PlayerAllotTask(List<ElderTaskSlot> slotList)
	{
		List<BaseItem> list = new List<BaseItem>();
		foreach (ElderTaskSlot slot in slotList)
		{
			if (!slot.IsNull())
			{
				list.Add(slot.Item.Clone());
			}
		}
		if (list.Count == 0)
		{
			UIPopTip.Inst.Pop("至少需要一个物品");
			return false;
		}
		if (player.menPai <= 0)
		{
			UIPopTip.Inst.Pop("无权发布任务");
			return false;
		}
		ElderTask elderTask = new ElderTask();
		int num = 0;
		int num2 = 0;
		foreach (BaseItem item in list)
		{
			elderTask.AddNeedItem(item);
			num += GetNeedMoney(item);
			num2++;
		}
		elderTask.Money = num;
		if (CheckCanAllotTask(num, num2))
		{
			AddWaitAcceptTask(elderTask);
			player.AddMoney(-num);
			PlayerEx.AddShengWang(player.menPai, -num2);
			return true;
		}
		UIPopTip.Inst.Pop("灵石或声望不足");
		return false;
	}

	public int GetNeedMoney(BaseItem baseItem)
	{
		return baseItem.GetPrice() * baseItem.Count * ElderTaskItemType.DataDict[baseItem.Type].Xishu / 100;
	}

	public void PlayerGetTaskItem(ElderTask task)
	{
		foreach (BaseItem needItem in task.needItemList)
		{
			player.addItem(needItem.Id, needItem.Count, needItem.Seid, ShowText: true);
		}
		RemoveCompleteTask(task);
	}

	public void PlayerCancelTask(ElderTask task)
	{
		if (!waitAcceptTaskList.Contains(task))
		{
			UIPopTip.Inst.Pop("取消失败，该任务状态异常，请反馈");
			return;
		}
		player.AddMoney(task.Money);
		PlayerEx.AddShengWang(player.menPai, task.needItemList.Count);
		waitAcceptTaskList.Remove(task);
	}

	public void CompleteTask(ElderTask task)
	{
		AddCompleteTask(task);
		RemoveExecutingTask(task);
	}

	public List<ElderTask> GetWaitAcceptTaskList()
	{
		return waitAcceptTaskList;
	}

	public List<ElderTask> GetExecutingTaskList()
	{
		return executingTaskList;
	}

	public List<ElderTask> GetCompleteTaskList()
	{
		return completeTaskList;
	}

	public void AddWaitAcceptTask(ElderTask task)
	{
		waitAcceptTaskList.Add(task);
	}

	public void AddExecutingTask(ElderTask task)
	{
		executingTaskList.Add(task);
	}

	private void AddCompleteTask(ElderTask task)
	{
		completeTaskList.Add(task);
	}

	public void RemoveWaitAcceptTask(ElderTask task)
	{
		waitAcceptTaskList.Remove(task);
	}

	public void RemoveExecutingTask(ElderTask task)
	{
		executingTaskList.Remove(task);
	}

	public void RemoveCompleteTask(ElderTask task)
	{
		completeTaskList.Remove(task);
	}

	public bool CheckCanAccpetElderTask(int npcId)
	{
		if (executingTaskNpcIdList.Contains(npcId))
		{
			return false;
		}
		JSONObject npcData = NpcJieSuanManager.inst.GetNpcData(npcId);
		if (npcData == null)
		{
			return false;
		}
		if (player.menPai != npcData["MenPai"].I)
		{
			return false;
		}
		if (NpcJieSuanManager.inst.GetNpcBigLevel(npcId) > 2)
		{
			return false;
		}
		return true;
	}

	public List<int> GetCanAccpetNpcIdList()
	{
		return canAccpetNpcIdList;
	}

	public void ClearCanAccpetNpcIdList()
	{
		canAccpetNpcIdList.Clear();
	}

	public void AddCanAccpetNpcIdList(int npcId)
	{
		if (CheckCanAccpetElderTask(npcId))
		{
			canAccpetNpcIdList.Add(npcId);
		}
	}

	public void RemoveCanAccpetNpcIdList(int npcId)
	{
		canAccpetNpcIdList.Remove(npcId);
	}

	public void AddExecutingTaskNpcId(int npcId)
	{
		if (executingTaskNpcIdList.Contains(npcId))
		{
			Debug.LogError((object)$"npcId:{npcId}正在执行任务");
		}
		else
		{
			executingTaskNpcIdList.Add(npcId);
		}
	}

	public List<int> GetExecutingTaskNpcIdList()
	{
		return executingTaskNpcIdList;
	}

	public void RemoveExecutingTaskNpcId(int npcId)
	{
		executingTaskNpcIdList.Remove(npcId);
	}
}
