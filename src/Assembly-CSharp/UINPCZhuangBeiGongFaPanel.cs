using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000282 RID: 642
public class UINPCZhuangBeiGongFaPanel : TabPanelBase
{
	// Token: 0x0600173B RID: 5947 RVA: 0x0009E958 File Offset: 0x0009CB58
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

	// Token: 0x0600173C RID: 5948 RVA: 0x0009E9A8 File Offset: 0x0009CBA8
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

	// Token: 0x0600173D RID: 5949 RVA: 0x0009EA3C File Offset: 0x0009CC3C
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

	// Token: 0x0600173E RID: 5950 RVA: 0x0009EBEC File Offset: 0x0009CDEC
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

	// Token: 0x0600173F RID: 5951 RVA: 0x0009EC8C File Offset: 0x0009CE8C
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

	// Token: 0x040011F2 RID: 4594
	private UINPCData npc;

	// Token: 0x040011F3 RID: 4595
	public GameObject Equip3Root;

	// Token: 0x040011F4 RID: 4596
	public GameObject Equip4Root;

	// Token: 0x040011F5 RID: 4597
	public List<UIIconShow> Equip3IconList = new List<UIIconShow>();

	// Token: 0x040011F6 RID: 4598
	public List<UIIconShow> Equip4IconList = new List<UIIconShow>();

	// Token: 0x040011F7 RID: 4599
	public List<UIIconShow> StaticSkillIconList = new List<UIIconShow>();

	// Token: 0x040011F8 RID: 4600
	public List<UIIconShow> SkillIconList = new List<UIIconShow>();

	// Token: 0x040011F9 RID: 4601
	public static bool SetNullOnce;
}
