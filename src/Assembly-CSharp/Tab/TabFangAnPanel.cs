using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Tab
{
	// Token: 0x02000A3F RID: 2623
	public class TabFangAnPanel : UIBase
	{
		// Token: 0x060043C5 RID: 17349 RVA: 0x001CF608 File Offset: 0x001CD808
		public TabFangAnPanel(GameObject gameObject)
		{
			this._go = gameObject;
			this.XuanZePanel = base.Get("选择方案", true);
			this.CurFangAnPanel = base.Get("当前方案", true);
			this.Name = base.Get<Text>("当前方案/Text");
			base.Get<FpBtn>("当前方案").mouseUpEvent.AddListener(new UnityAction(this.ClickEvent));
			this.Init();
		}

		// Token: 0x060043C6 RID: 17350 RVA: 0x001CF68C File Offset: 0x001CD88C
		private void Init()
		{
			for (int i = 0; i < this.XuanZePanel.transform.childCount; i++)
			{
				this._fangAns.Add(new TabFangAn(this.XuanZePanel.transform.GetChild(i).gameObject, i + 1));
			}
		}

		// Token: 0x060043C7 RID: 17351 RVA: 0x0003079F File Offset: 0x0002E99F
		public void Show()
		{
			this._go.SetActive(true);
			this.CurFangAnPanel.SetActive(true);
			this.UpdateCurFanAn();
		}

		// Token: 0x060043C8 RID: 17352 RVA: 0x001CF6E0 File Offset: 0x001CD8E0
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

		// Token: 0x060043C9 RID: 17353 RVA: 0x000307BF File Offset: 0x0002E9BF
		private void ClickEvent()
		{
			this.XuanZePanel.SetActive(true);
		}

		// Token: 0x060043CA RID: 17354 RVA: 0x000307CD File Offset: 0x0002E9CD
		public void Close()
		{
			this._go.SetActive(false);
			this.XuanZePanel.SetActive(false);
			this.CurFangAnPanel.SetActive(false);
		}

		// Token: 0x04003BD0 RID: 15312
		public Text Name;

		// Token: 0x04003BD1 RID: 15313
		private GameObject XuanZePanel;

		// Token: 0x04003BD2 RID: 15314
		private GameObject CurFangAnPanel;

		// Token: 0x04003BD3 RID: 15315
		private List<TabFangAn> _fangAns = new List<TabFangAn>();
	}
}
