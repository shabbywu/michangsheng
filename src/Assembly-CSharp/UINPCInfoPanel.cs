using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000271 RID: 625
public class UINPCInfoPanel : MonoBehaviour, IESCClose
{
	// Token: 0x060016A0 RID: 5792 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x060016A1 RID: 5793 RVA: 0x0009A3CB File Offset: 0x000985CB
	private void Update()
	{
		this.AutoHide();
	}

	// Token: 0x060016A2 RID: 5794 RVA: 0x0009A3D4 File Offset: 0x000985D4
	public bool CanShow()
	{
		return !UINPCJiaoHu.AllShouldHide && (!(PanelMamager.inst != null) || !(PanelMamager.inst.UISceneGameObject == null)) && (!(PanelMamager.inst != null) || PanelMamager.inst.nowPanel == PanelMamager.PanelType.空);
	}

	// Token: 0x060016A3 RID: 5795 RVA: 0x0009A428 File Offset: 0x00098628
	private void AutoHide()
	{
		if (!this.CanShow())
		{
			UINPCJiaoHu.Inst.HideNPCInfoPanel();
		}
	}

	// Token: 0x060016A4 RID: 5796 RVA: 0x0009A43C File Offset: 0x0009863C
	public void RefreshUI(UINPCData data = null)
	{
		if (data == null)
		{
			this.npc = UINPCJiaoHu.Inst.NowJiaoHuNPC;
		}
		else
		{
			this.npc = data;
		}
		this.Face.SetNPCFace(this.npc.ID);
		this.NPCName.text = this.npc.Name;
		if (this.npc.IsFight)
		{
			this.SetFightInfo();
		}
		else
		{
			this.SetNPCInfo();
		}
		this.TabGroup.SetFirstTab();
	}

	// Token: 0x060016A5 RID: 5797 RVA: 0x0009A4B8 File Offset: 0x000986B8
	public void SetNPCInfo()
	{
		this.ShuXing.SetActive(true);
		this.FightShuXing.SetActive(false);
		this.Age.text = this.npc.Age.ToString();
		this.HP.text = this.npc.HP.ToString();
		if (this.npc.Favor >= 200)
		{
			this.QingFen.text = string.Format("{0} (满)", this.npc.Favor);
		}
		else
		{
			this.QingFen.text = string.Format("{0} ({1}%)", this.npc.Favor, (int)(this.npc.FavorPer * 100f));
		}
		this.XiuWei.text = this.npc.LevelStr;
		this.ZhuangTai.text = this.npc.ZhuangTaiStr;
		this.ShouYuan.text = this.npc.ShouYuan.ToString();
		this.ZiZhi.text = this.npc.ZiZhi.ToString();
		this.WuXing.text = this.npc.WuXing.ToString();
		this.DunSu.text = this.npc.DunSu.ToString();
		this.ShenShi.text = this.npc.ShenShi.ToString();
	}

	// Token: 0x060016A6 RID: 5798 RVA: 0x0009A63C File Offset: 0x0009883C
	public void SetFightInfo()
	{
		this.ShuXing.SetActive(false);
		this.FightShuXing.SetActive(true);
		this.FightHP.text = this.npc.HP.ToString();
		this.FightXiuWei.text = this.npc.LevelStr;
		this.FightDunSu.text = this.npc.DunSu.ToString();
		this.FightShenShi.text = this.npc.ShenShi.ToString();
	}

	// Token: 0x060016A7 RID: 5799 RVA: 0x0009A6C8 File Offset: 0x000988C8
	public bool TryEscClose()
	{
		UINPCJiaoHu.Inst.HideNPCInfoPanel();
		return true;
	}

	// Token: 0x04001117 RID: 4375
	[HideInInspector]
	public UINPCData npc;

	// Token: 0x04001118 RID: 4376
	public PlayerSetRandomFace Face;

	// Token: 0x04001119 RID: 4377
	public TabGroup TabGroup;

	// Token: 0x0400111A RID: 4378
	public GameObject ShuXing;

	// Token: 0x0400111B RID: 4379
	public Text NPCName;

	// Token: 0x0400111C RID: 4380
	public Text Age;

	// Token: 0x0400111D RID: 4381
	public Text HP;

	// Token: 0x0400111E RID: 4382
	public Text QingFen;

	// Token: 0x0400111F RID: 4383
	public Text XiuWei;

	// Token: 0x04001120 RID: 4384
	public Text ZhuangTai;

	// Token: 0x04001121 RID: 4385
	public Text ShouYuan;

	// Token: 0x04001122 RID: 4386
	public Text ZiZhi;

	// Token: 0x04001123 RID: 4387
	public Text WuXing;

	// Token: 0x04001124 RID: 4388
	public Text DunSu;

	// Token: 0x04001125 RID: 4389
	public Text ShenShi;

	// Token: 0x04001126 RID: 4390
	public GameObject FightShuXing;

	// Token: 0x04001127 RID: 4391
	public Text FightHP;

	// Token: 0x04001128 RID: 4392
	public Text FightXiuWei;

	// Token: 0x04001129 RID: 4393
	public Text FightDunSu;

	// Token: 0x0400112A RID: 4394
	public Text FightShenShi;
}
