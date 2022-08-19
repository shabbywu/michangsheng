using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Tab
{
	// Token: 0x020006F7 RID: 1783
	public class TabFangAnPanel : UIBase
	{
		// Token: 0x0600394C RID: 14668 RVA: 0x00187CBC File Offset: 0x00185EBC
		public TabFangAnPanel(GameObject gameObject)
		{
			this._go = gameObject;
			this.XuanZePanel = base.Get("选择方案", true);
			this.CurFangAnPanel = base.Get("当前方案", true);
			this.Name = base.Get<Text>("当前方案/Text");
			base.Get<FpBtn>("当前方案").mouseUpEvent.AddListener(new UnityAction(this.ClickEvent));
			this.Init();
		}

		// Token: 0x0600394D RID: 14669 RVA: 0x00187D40 File Offset: 0x00185F40
		private void Init()
		{
			for (int i = 0; i < this.XuanZePanel.transform.childCount; i++)
			{
				this._fangAns.Add(new TabFangAn(this.XuanZePanel.transform.GetChild(i).gameObject, i + 1));
			}
		}

		// Token: 0x0600394E RID: 14670 RVA: 0x00187D91 File Offset: 0x00185F91
		public void Show()
		{
			this._go.SetActive(true);
			this.CurFangAnPanel.SetActive(true);
			this.UpdateCurFanAn();
		}

		// Token: 0x0600394F RID: 14671 RVA: 0x00187DB4 File Offset: 0x00185FB4
		public void UpdateCurFanAn()
		{
			switch (SingletonMono<TabUIMag>.Instance.TabBag.GetCurBagType())
			{
			case BagType.功法:
				this.Name.SetText("方案" + (Tools.instance.getPlayer().nowConfigEquipStaticSkill + 1).ToCNNumber());
				break;
			case BagType.技能:
				this.Name.SetText("方案" + (Tools.instance.getPlayer().nowConfigEquipSkill + 1).ToCNNumber());
				break;
			case BagType.背包:
				this.Name.SetText("方案" + Tools.instance.getPlayer().StreamData.FangAnData.CurEquipIndex.ToCNNumber());
				break;
			}
			this.XuanZePanel.SetActive(false);
			this.CurFangAnPanel.SetActive(true);
		}

		// Token: 0x06003950 RID: 14672 RVA: 0x00187E8F File Offset: 0x0018608F
		private void ClickEvent()
		{
			this.XuanZePanel.SetActive(true);
		}

		// Token: 0x06003951 RID: 14673 RVA: 0x00187E9D File Offset: 0x0018609D
		public void Close()
		{
			this._go.SetActive(false);
			this.XuanZePanel.SetActive(false);
			this.CurFangAnPanel.SetActive(false);
		}

		// Token: 0x0400315D RID: 12637
		public Text Name;

		// Token: 0x0400315E RID: 12638
		private GameObject XuanZePanel;

		// Token: 0x0400315F RID: 12639
		private GameObject CurFangAnPanel;

		// Token: 0x04003160 RID: 12640
		private List<TabFangAn> _fangAns = new List<TabFangAn>();
	}
}
