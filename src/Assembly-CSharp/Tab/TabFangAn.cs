using System;
using GUIPackage;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Tab
{
	// Token: 0x020006EE RID: 1774
	public class TabFangAn : UIBase
	{
		// Token: 0x06003915 RID: 14613 RVA: 0x00185AC4 File Offset: 0x00183CC4
		public TabFangAn(GameObject gameObject, int index)
		{
			this._go = gameObject;
			this.Name = base.Get<Text>("Text");
			this.Index = index;
			this._go.GetComponent<FpBtn>().mouseUpEvent.AddListener(new UnityAction(this.ClickEvent));
		}

		// Token: 0x06003916 RID: 14614 RVA: 0x00185B18 File Offset: 0x00183D18
		public void ClickEvent()
		{
			Avatar player = Tools.instance.getPlayer();
			switch (SingletonMono<TabUIMag>.Instance.TabBag.GetCurBagType())
			{
			case BagType.功法:
				player.nowConfigEquipStaticSkill = this.Index - 1;
				player.equipStaticSkillList = player.configEquipStaticSkill[player.nowConfigEquipStaticSkill];
				SingletonMono<TabUIMag>.Instance.GongFaPanel.LoadSkillData();
				StaticSkill.resetSeid(player);
				break;
			case BagType.技能:
				player.nowConfigEquipSkill = this.Index - 1;
				player.equipSkillList = player.configEquipSkill[player.nowConfigEquipSkill];
				SingletonMono<TabUIMag>.Instance.ShenTongPanel.LoadSkillData();
				break;
			case BagType.背包:
				player.StreamData.FangAnData.SwitchFangAn(this.Index);
				SingletonMono<TabUIMag>.Instance.WuPingPanel.LoadEquipData();
				SingletonMono<TabUIMag>.Instance.TabBag.UpdateItem();
				break;
			}
			SingletonMono<TabUIMag>.Instance.TabFangAnPanel.UpdateCurFanAn();
		}

		// Token: 0x0400311A RID: 12570
		public Text Name;

		// Token: 0x0400311B RID: 12571
		private FpBtn _fpBtn;

		// Token: 0x0400311C RID: 12572
		public int Index;
	}
}
