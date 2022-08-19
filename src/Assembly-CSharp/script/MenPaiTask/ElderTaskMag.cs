using System;
using System.Collections.Generic;
using Bag;
using JSONClass;
using KBEngine;
using script.MenPaiTask.ZhangLao.ElderTaskSys;
using script.MenPaiTask.ZhangLao.UI.Base;
using UnityEngine;

namespace script.MenPaiTask
{
	// Token: 0x02000A08 RID: 2568
	[Serializable]
	public class ElderTaskMag
	{
		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x06004716 RID: 18198 RVA: 0x0006EC50 File Offset: 0x0006CE50
		private Avatar player
		{
			get
			{
				return Tools.instance.getPlayer();
			}
		}

		// Token: 0x06004717 RID: 18199 RVA: 0x001E21E4 File Offset: 0x001E03E4
		public void Init()
		{
			if (this.waitAcceptTaskList == null)
			{
				this.waitAcceptTaskList = new List<ElderTask>();
			}
			if (this.executingTaskList == null)
			{
				this.executingTaskList = new List<ElderTask>();
			}
			if (this.completeTaskList == null)
			{
				this.completeTaskList = new List<ElderTask>();
			}
			if (this.canAccpetNpcIdList == null)
			{
				this.canAccpetNpcIdList = new List<int>();
			}
			if (this.executingTaskNpcIdList == null)
			{
				this.executingTaskNpcIdList = new List<int>();
			}
			if (this.AllotTask == null)
			{
				this.AllotTask = new AllotTask();
			}
			if (this.UpdateTaskProcess == null)
			{
				this.UpdateTaskProcess = new UpdateTaskProcess();
			}
			if (this.itemList == null || this.itemList.Count < 1)
			{
				this.itemList = new List<int>();
				Dictionary<int, ElderTaskItemType> dataDict = ElderTaskItemType.DataDict;
				foreach (_ItemJsonData itemJsonData in _ItemJsonData.DataList)
				{
					if (itemJsonData.id >= jsonData.QingJiaoItemIDSegment)
					{
						break;
					}
					if (dataDict.ContainsKey(itemJsonData.type) && dataDict[itemJsonData.type].quality.Contains(itemJsonData.quality))
					{
						this.itemList.Add(itemJsonData.id);
					}
				}
				foreach (ElderTaskDisableItem elderTaskDisableItem in ElderTaskDisableItem.DataList)
				{
					this.itemList.Remove(elderTaskDisableItem.id);
				}
			}
		}

		// Token: 0x06004718 RID: 18200 RVA: 0x001E2378 File Offset: 0x001E0578
		public List<int> GetCanNeedItemList()
		{
			return this.itemList;
		}

		// Token: 0x06004719 RID: 18201 RVA: 0x001E2380 File Offset: 0x001E0580
		public bool CheckCanAllotTask(int money, int reputation)
		{
			return (int)this.player.money >= money && PlayerEx.GetShengWang((int)this.player.menPai) >= reputation;
		}

		// Token: 0x0600471A RID: 18202 RVA: 0x001E23AC File Offset: 0x001E05AC
		public bool PlayerAllotTask(List<ElderTaskSlot> slotList)
		{
			List<BaseItem> list = new List<BaseItem>();
			foreach (ElderTaskSlot elderTaskSlot in slotList)
			{
				if (!elderTaskSlot.IsNull())
				{
					list.Add(elderTaskSlot.Item.Clone());
				}
			}
			if (list.Count == 0)
			{
				UIPopTip.Inst.Pop("至少需要一个物品", PopTipIconType.叹号);
				return false;
			}
			if (this.player.menPai <= 0)
			{
				UIPopTip.Inst.Pop("无权发布任务", PopTipIconType.叹号);
				return false;
			}
			ElderTask elderTask = new ElderTask();
			int num = 0;
			int num2 = 0;
			foreach (BaseItem baseItem in list)
			{
				elderTask.AddNeedItem(baseItem);
				num += this.GetNeedMoney(baseItem);
				num2++;
			}
			elderTask.Money = num;
			if (this.CheckCanAllotTask(num, num2))
			{
				this.AddWaitAcceptTask(elderTask);
				this.player.AddMoney(-num);
				PlayerEx.AddShengWang((int)this.player.menPai, -num2, false);
				return true;
			}
			UIPopTip.Inst.Pop("灵石或声望不足", PopTipIconType.叹号);
			return false;
		}

		// Token: 0x0600471B RID: 18203 RVA: 0x001E24F4 File Offset: 0x001E06F4
		public int GetNeedMoney(BaseItem baseItem)
		{
			return baseItem.GetPrice() * baseItem.Count * ElderTaskItemType.DataDict[baseItem.Type].Xishu / 100;
		}

		// Token: 0x0600471C RID: 18204 RVA: 0x001E251C File Offset: 0x001E071C
		public void PlayerGetTaskItem(ElderTask task)
		{
			foreach (BaseItem baseItem in task.needItemList)
			{
				this.player.addItem(baseItem.Id, baseItem.Count, baseItem.Seid, true);
			}
			this.RemoveCompleteTask(task);
		}

		// Token: 0x0600471D RID: 18205 RVA: 0x001E2590 File Offset: 0x001E0790
		public void PlayerCancelTask(ElderTask task)
		{
			if (!this.waitAcceptTaskList.Contains(task))
			{
				UIPopTip.Inst.Pop("取消失败，该任务状态异常，请反馈", PopTipIconType.叹号);
				return;
			}
			this.player.AddMoney(task.Money);
			PlayerEx.AddShengWang((int)this.player.menPai, task.needItemList.Count, false);
			this.waitAcceptTaskList.Remove(task);
		}

		// Token: 0x0600471E RID: 18206 RVA: 0x001E25F6 File Offset: 0x001E07F6
		public void CompleteTask(ElderTask task)
		{
			this.AddCompleteTask(task);
			this.RemoveExecutingTask(task);
		}

		// Token: 0x0600471F RID: 18207 RVA: 0x001E2606 File Offset: 0x001E0806
		public List<ElderTask> GetWaitAcceptTaskList()
		{
			return this.waitAcceptTaskList;
		}

		// Token: 0x06004720 RID: 18208 RVA: 0x001E260E File Offset: 0x001E080E
		public List<ElderTask> GetExecutingTaskList()
		{
			return this.executingTaskList;
		}

		// Token: 0x06004721 RID: 18209 RVA: 0x001E2616 File Offset: 0x001E0816
		public List<ElderTask> GetCompleteTaskList()
		{
			return this.completeTaskList;
		}

		// Token: 0x06004722 RID: 18210 RVA: 0x001E261E File Offset: 0x001E081E
		public void AddWaitAcceptTask(ElderTask task)
		{
			this.waitAcceptTaskList.Add(task);
		}

		// Token: 0x06004723 RID: 18211 RVA: 0x001E262C File Offset: 0x001E082C
		public void AddExecutingTask(ElderTask task)
		{
			this.executingTaskList.Add(task);
		}

		// Token: 0x06004724 RID: 18212 RVA: 0x001E263A File Offset: 0x001E083A
		private void AddCompleteTask(ElderTask task)
		{
			this.completeTaskList.Add(task);
		}

		// Token: 0x06004725 RID: 18213 RVA: 0x001E2648 File Offset: 0x001E0848
		public void RemoveWaitAcceptTask(ElderTask task)
		{
			this.waitAcceptTaskList.Remove(task);
		}

		// Token: 0x06004726 RID: 18214 RVA: 0x001E2657 File Offset: 0x001E0857
		public void RemoveExecutingTask(ElderTask task)
		{
			this.executingTaskList.Remove(task);
		}

		// Token: 0x06004727 RID: 18215 RVA: 0x001E2666 File Offset: 0x001E0866
		public void RemoveCompleteTask(ElderTask task)
		{
			this.completeTaskList.Remove(task);
		}

		// Token: 0x06004728 RID: 18216 RVA: 0x001E2678 File Offset: 0x001E0878
		public bool CheckCanAccpetElderTask(int npcId)
		{
			if (this.executingTaskNpcIdList.Contains(npcId))
			{
				return false;
			}
			JSONObject npcData = NpcJieSuanManager.inst.GetNpcData(npcId);
			return npcData != null && (int)this.player.menPai == npcData["MenPai"].I && NpcJieSuanManager.inst.GetNpcBigLevel(npcId) <= 2;
		}

		// Token: 0x06004729 RID: 18217 RVA: 0x001E26D6 File Offset: 0x001E08D6
		public List<int> GetCanAccpetNpcIdList()
		{
			return this.canAccpetNpcIdList;
		}

		// Token: 0x0600472A RID: 18218 RVA: 0x001E26DE File Offset: 0x001E08DE
		public void ClearCanAccpetNpcIdList()
		{
			this.canAccpetNpcIdList.Clear();
		}

		// Token: 0x0600472B RID: 18219 RVA: 0x001E26EB File Offset: 0x001E08EB
		public void AddCanAccpetNpcIdList(int npcId)
		{
			if (this.CheckCanAccpetElderTask(npcId))
			{
				this.canAccpetNpcIdList.Add(npcId);
			}
		}

		// Token: 0x0600472C RID: 18220 RVA: 0x001E2702 File Offset: 0x001E0902
		public void RemoveCanAccpetNpcIdList(int npcId)
		{
			this.canAccpetNpcIdList.Remove(npcId);
		}

		// Token: 0x0600472D RID: 18221 RVA: 0x001E2711 File Offset: 0x001E0911
		public void AddExecutingTaskNpcId(int npcId)
		{
			if (this.executingTaskNpcIdList.Contains(npcId))
			{
				Debug.LogError(string.Format("npcId:{0}正在执行任务", npcId));
				return;
			}
			this.executingTaskNpcIdList.Add(npcId);
		}

		// Token: 0x0600472E RID: 18222 RVA: 0x001E2743 File Offset: 0x001E0943
		public List<int> GetExecutingTaskNpcIdList()
		{
			return this.executingTaskNpcIdList;
		}

		// Token: 0x0600472F RID: 18223 RVA: 0x001E274B File Offset: 0x001E094B
		public void RemoveExecutingTaskNpcId(int npcId)
		{
			this.executingTaskNpcIdList.Remove(npcId);
		}

		// Token: 0x04004859 RID: 18521
		private List<ElderTask> waitAcceptTaskList = new List<ElderTask>();

		// Token: 0x0400485A RID: 18522
		private List<ElderTask> executingTaskList = new List<ElderTask>();

		// Token: 0x0400485B RID: 18523
		private List<ElderTask> completeTaskList = new List<ElderTask>();

		// Token: 0x0400485C RID: 18524
		[NonSerialized]
		private List<int> canAccpetNpcIdList = new List<int>();

		// Token: 0x0400485D RID: 18525
		private List<int> executingTaskNpcIdList = new List<int>();

		// Token: 0x0400485E RID: 18526
		[NonSerialized]
		public AllotTask AllotTask;

		// Token: 0x0400485F RID: 18527
		[NonSerialized]
		public UpdateTaskProcess UpdateTaskProcess;

		// Token: 0x04004860 RID: 18528
		[NonSerialized]
		private List<int> itemList = new List<int>();
	}
}
