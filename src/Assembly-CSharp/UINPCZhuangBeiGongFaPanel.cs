using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003A9 RID: 937
public class UINPCZhuangBeiGongFaPanel : TabPanelBase
{
	// Token: 0x06001A0B RID: 6667 RVA: 0x000E6174 File Offset: 0x000E4374
	public override void OnPanelShow()
	{
		base.OnPanelShow();
		if (UINPCZhuangBeiGongFaPanel.SetNullOnce)
		{
			UINPCZhuangBeiGongFaPanel.SetNullOnce = false;
			this.SetNull();
			return;
		}
		this.npc = UINPCJiaoHu.Inst.InfoPanel.npc;
		this.RefreshEquip();
		this.RefreshStaticSkill();
		this.RefreshSkill();
	}

	// Token: 0x06001A0C RID: 6668 RVA: 0x000E61C4 File Offset: 0x000E43C4
	public void SetNull()
	{
		this.Equip3Root.SetActive(true);
		this.Equip4Root.SetActive(false);
		this.Equip3IconList[0].SetNull();
		this.Equip3IconList[1].SetNull();
		this.Equip3IconList[2].SetNull();
		for (int i = 0; i < 6; i++)
		{
			this.StaticSkillIconList[i].SetNull();
		}
		for (int j = 0; j < 10; j++)
		{
			this.SkillIconList[j].SetNull();
		}
	}

	// Token: 0x06001A0D RID: 6669 RVA: 0x000E6258 File Offset: 0x000E4458
	private void RefreshEquip()
	{
		if (this.npc.IsDoubleWeapon)
		{
			this.Equip3Root.SetActive(false);
			this.Equip4Root.SetActive(true);
			this.Equip4IconList[0].SetItem(this.npc.Weapon1);
			this.Equip4IconList[1].SetItem(this.npc.Weapon2);
			if (this.npc.Clothing != null)
			{
				this.Equip4IconList[2].SetItem(this.npc.Clothing);
			}
			else
			{
				this.Equip4IconList[2].SetNull();
			}
			if (this.npc.Ring != null)
			{
				this.Equip4IconList[3].SetItem(this.npc.Ring);
				return;
			}
			this.Equip4IconList[3].SetNull();
			return;
		}
		else
		{
			this.Equip3Root.SetActive(true);
			this.Equip4Root.SetActive(false);
			if (this.npc.Weapon1 != null)
			{
				this.Equip3IconList[0].SetItem(this.npc.Weapon1);
			}
			else
			{
				this.Equip3IconList[0].SetNull();
			}
			if (this.npc.Clothing != null)
			{
				this.Equip3IconList[1].SetItem(this.npc.Clothing);
			}
			else
			{
				this.Equip3IconList[1].SetNull();
			}
			if (this.npc.Ring != null)
			{
				this.Equip3IconList[2].SetItem(this.npc.Ring);
				return;
			}
			this.Equip3IconList[2].SetNull();
			return;
		}
	}

	// Token: 0x06001A0E RID: 6670 RVA: 0x000E6408 File Offset: 0x000E4608
	private void RefreshStaticSkill()
	{
		for (int i = 0; i < 5; i++)
		{
			if (i >= this.npc.StaticSkills.Count)
			{
				this.StaticSkillIconList[i].SetNull();
			}
			else
			{
				this.StaticSkillIconList[i].SetStaticSkill(this.npc.StaticSkills[i], false);
			}
		}
		if (this.npc.YuanYingStaticSkill > 0)
		{
			this.StaticSkillIconList[5].SetStaticSkill(this.npc.YuanYingStaticSkill, false);
			return;
		}
		this.StaticSkillIconList[5].SetNull();
	}

	// Token: 0x06001A0F RID: 6671 RVA: 0x000E64A8 File Offset: 0x000E46A8
	private void RefreshSkill()
	{
		for (int i = 0; i < 10; i++)
		{
			if (i >= this.npc.Skills.Count)
			{
				this.SkillIconList[i].SetNull();
			}
			else
			{
				this.SkillIconList[i].SetSkill(this.npc.Skills[i], false, 1);
			}
		}
	}

	// Token: 0x04001565 RID: 5477
	private UINPCData npc;

	// Token: 0x04001566 RID: 5478
	public GameObject Equip3Root;

	// Token: 0x04001567 RID: 5479
	public GameObject Equip4Root;

	// Token: 0x04001568 RID: 5480
	public List<UIIconShow> Equip3IconList = new List<UIIconShow>();

	// Token: 0x04001569 RID: 5481
	public List<UIIconShow> Equip4IconList = new List<UIIconShow>();

	// Token: 0x0400156A RID: 5482
	public List<UIIconShow> StaticSkillIconList = new List<UIIconShow>();

	// Token: 0x0400156B RID: 5483
	public List<UIIconShow> SkillIconList = new List<UIIconShow>();

	// Token: 0x0400156C RID: 5484
	public static bool SetNullOnce;
}
