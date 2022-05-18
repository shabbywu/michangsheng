using System;
using Fungus;
using UnityEngine;

// Token: 0x02000362 RID: 866
[CommandInfo("YSNPCJiaoHu", "初始化NPC", "初始化NPC", 0)]
[AddComponentMenu("")]
public class CmdInitNPC : Command
{
	// Token: 0x060018F0 RID: 6384 RVA: 0x000DEC00 File Offset: 0x000DCE00
	public override void OnEnter()
	{
		this.flowchart = this.GetFlowchart();
		UINPCData nowJiaoHuNPC = UINPCJiaoHu.Inst.NowJiaoHuNPC;
		this.Set("npcid", nowJiaoHuNPC.ID);
		this.SetBool("IsThreeSceneNPC", nowJiaoHuNPC.IsThreeSceneNPC);
		this.SetString("Name", nowJiaoHuNPC.Name);
		this.Set("Sex", nowJiaoHuNPC.Sex);
		this.SetString("Title", nowJiaoHuNPC.Title);
		this.Set("Age", nowJiaoHuNPC.Age);
		this.Set("HP", nowJiaoHuNPC.HP);
		this.Set("QingFen", nowJiaoHuNPC.QingFen);
		this.Set("Exp", nowJiaoHuNPC.Exp);
		this.Set("Level", nowJiaoHuNPC.Level);
		this.SetString("LevelStr", nowJiaoHuNPC.LevelStr);
		this.Set("ZhuangTai", nowJiaoHuNPC.ZhuangTai);
		this.SetString("ZhuangTaiStr", nowJiaoHuNPC.ZhuangTaiStr);
		this.Set("ShouYuan", nowJiaoHuNPC.ShouYuan);
		this.Set("ZiZhi", nowJiaoHuNPC.ZiZhi);
		this.Set("WuXing", nowJiaoHuNPC.WuXing);
		this.Set("DunSu", nowJiaoHuNPC.DunSu);
		this.Set("ShenShi", nowJiaoHuNPC.ShenShi);
		this.Set("Favor", nowJiaoHuNPC.Favor);
		this.Set("FavorLevel", nowJiaoHuNPC.FavorLevel);
		this.Set("XingGe", nowJiaoHuNPC.XingGe);
		this.Set("ActionID", nowJiaoHuNPC.ActionID);
		this.Set("BigLevel", nowJiaoHuNPC.BigLevel);
		this.SetBool("IsKnowPlayer", nowJiaoHuNPC.IsKnowPlayer);
		this.SetBool("IsNingZhouNPC", nowJiaoHuNPC.IsNingZhouNPC);
		this.SetBool("IsNeedHelp", nowJiaoHuNPC.IsNeedHelp);
		this.SetBool("IsZhongYaoNPC", nowJiaoHuNPC.IsZhongYaoNPC);
		this.Set("ShiLi", nowJiaoHuNPC.MenPai);
		this.SetString("UUID", nowJiaoHuNPC.UUID);
		this.Set("NPCType", nowJiaoHuNPC.NPCType);
		this.Continue();
	}

	// Token: 0x060018F1 RID: 6385 RVA: 0x000156A0 File Offset: 0x000138A0
	private void Set(string name, int value)
	{
		this.flowchart.SetIntegerVariable(name, value);
	}

	// Token: 0x060018F2 RID: 6386 RVA: 0x000156AF File Offset: 0x000138AF
	private void SetBool(string name, bool value)
	{
		this.flowchart.SetBooleanVariable(name, value);
	}

	// Token: 0x060018F3 RID: 6387 RVA: 0x000156BE File Offset: 0x000138BE
	private void SetString(string name, string value)
	{
		this.flowchart.SetStringVariable(name, value);
	}

	// Token: 0x040013DE RID: 5086
	private Flowchart flowchart;
}
