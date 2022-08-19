using System;
using System.Collections.Generic;
using Bag;
using script.MenPaiTask.ZhangLao.UI;
using UnityEngine;

namespace script.MenPaiTask
{
	// Token: 0x02000A09 RID: 2569
	public class BaseElderTask : UIBase
	{
		// Token: 0x06004731 RID: 18225 RVA: 0x001E27B4 File Offset: 0x001E09B4
		protected virtual void Init()
		{
			this.Slots = new List<BaseSlot>();
			for (int i = 1; i <= this.Count; i++)
			{
				this.Slots.Add(base.Get<BaseSlot>(string.Format("任务物品列表/{0}", i)));
				this.Slots[i - 1].SetNull();
				this.Slots[i - 1].gameObject.SetActive(false);
			}
			this.InitItemList(this.ElderTask.needItemList);
		}

		// Token: 0x06004732 RID: 18226 RVA: 0x001E283B File Offset: 0x001E0A3B
		public static T Create<T>(ElderTask data, GameObject gameObject) where T : BaseElderTask, new()
		{
			T t = Activator.CreateInstance<T>();
			t._go = gameObject;
			t._go.SetActive(true);
			t.ElderTask = data;
			t.Init();
			return t;
		}

		// Token: 0x06004733 RID: 18227 RVA: 0x001E2876 File Offset: 0x001E0A76
		public static T Create<T>(ElderTask data, Transform transform) where T : BaseElderTask, new()
		{
			T t = Activator.CreateInstance<T>();
			t._go = transform.gameObject;
			t._go.SetActive(true);
			t.ElderTask = data;
			t.Init();
			return t;
		}

		// Token: 0x06004734 RID: 18228 RVA: 0x001E28B6 File Offset: 0x001E0AB6
		public virtual void DestroySelf()
		{
			ElderTaskUIMag.Inst.ElderTaskUI.Ctr.TaskList.Remove(this);
			Object.Destroy(this._go);
		}

		// Token: 0x06004735 RID: 18229 RVA: 0x001E28E0 File Offset: 0x001E0AE0
		private void InitItemList(List<BaseItem> list)
		{
			for (int i = 0; i < list.Count; i++)
			{
				this.Slots[i].SetSlotData(list[i].Clone());
				this.Slots[i].gameObject.SetActive(true);
			}
		}

		// Token: 0x04004861 RID: 18529
		public List<BaseSlot> Slots;

		// Token: 0x04004862 RID: 18530
		public int Count = 5;

		// Token: 0x04004863 RID: 18531
		public ElderTask ElderTask;
	}
}
