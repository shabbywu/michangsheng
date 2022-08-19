using System;
using script.MenPaiTask.ZhangLao.UI.Ctr;
using script.NewLianDan.Base;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace script.MenPaiTask.ZhangLao.UI.UI
{
	// Token: 0x02000A0E RID: 2574
	public class CreateElderTaskUI : BasePanel
	{
		// Token: 0x06004746 RID: 18246 RVA: 0x001E2C90 File Offset: 0x001E0E90
		public CreateElderTaskUI(GameObject gameObject)
		{
			this._go = gameObject;
			this.Ctr = new CreateElderTaskCtr();
		}

		// Token: 0x06004747 RID: 18247 RVA: 0x001E2CAC File Offset: 0x001E0EAC
		private void Init()
		{
			this.ItemPrefab = base.Get("提交物品列表/1", true);
			this.ItemParent = base.Get("提交物品列表", true).transform;
			this.灵石 = base.Get<Text>("Cost/灵石/Value");
			this.声望 = base.Get<Text>("Cost/声望/Value");
			this.任务内容 = base.Get<Text>("任务内容");
			base.Get<FpBtn>("发布按钮").mouseUpEvent.AddListener(new UnityAction(this.Ctr.PublishTask));
			base.Get<FpBtn>("返回按钮").mouseUpEvent.AddListener(delegate()
			{
				this.Ctr.ClearItemList();
				ElderTaskUIMag.Inst.OpenElderTaskUI();
			});
			this.Ctr.CreateItemList();
		}

		// Token: 0x06004748 RID: 18248 RVA: 0x001E2D68 File Offset: 0x001E0F68
		public void UpdateUI()
		{
			this.灵石.SetText(this.Ctr.NeedMoney);
			this.声望.SetText(this.Ctr.NeedReputation);
			this.任务内容.SetText(this.Ctr.CreateTaskDesc());
		}

		// Token: 0x06004749 RID: 18249 RVA: 0x001E2DC1 File Offset: 0x001E0FC1
		public override void Hide()
		{
			base.Hide();
		}

		// Token: 0x0600474A RID: 18250 RVA: 0x001E2DC9 File Offset: 0x001E0FC9
		public override void Show()
		{
			if (!this.isInit)
			{
				this.Init();
				this.isInit = true;
			}
			base.Show();
		}

		// Token: 0x04004868 RID: 18536
		private bool isInit;

		// Token: 0x04004869 RID: 18537
		public CreateElderTaskCtr Ctr;

		// Token: 0x0400486A RID: 18538
		public GameObject ItemPrefab;

		// Token: 0x0400486B RID: 18539
		public Transform ItemParent;

		// Token: 0x0400486C RID: 18540
		private Text 灵石;

		// Token: 0x0400486D RID: 18541
		private Text 声望;

		// Token: 0x0400486E RID: 18542
		private Text 任务内容;
	}
}
