using System;
using GUIPackage;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Tab
{
	// Token: 0x02000A2F RID: 2607
	public class TabFangAn : UIBase
	{
		// Token: 0x06004380 RID: 17280 RVA: 0x001CD560 File Offset: 0x001CB760
		public TabFangAn(GameObject gameObject, int index)
		{
			this._go = gameObject;
			this.Name = base.Get<Text>("Text");
			this.Index = index;
			this._go.GetComponent<FpBtn>().mouseUpEvent.AddListener(new UnityAction(this.ClickEvent));
		}

		// Token: 0x06004381 RID: 17281 RVA: 0x001CD5B4 File Offset: 0x001CB7B4
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

		// Token: 0x04003B7F RID: 15231
		public Text Name;

		// Token: 0x04003B80 RID: 15232
		private FpBtn _fpBtn;

		// Token: 0x04003B81 RID: 15233
		public int Index;
	}
}
