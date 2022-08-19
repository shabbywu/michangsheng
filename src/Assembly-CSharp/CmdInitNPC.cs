using System;
using Fungus;
using UnityEngine;

// Token: 0x02000246 RID: 582
[CommandInfo("YSNPCJiaoHu", "初始化NPC", "初始化NPC", 0)]
[AddComponentMenu("")]
public class CmdInitNPC : Command
{
	// Token: 0x06001638 RID: 5688 RVA: 0x000966D0 File Offset: 0x000948D0
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

	// Token: 0x06001639 RID: 5689 RVA: 0x000968F8 File Offset: 0x00094AF8
	private void Set(string name, int value)
	{
		this.flowchart.SetIntegerVariable(name, value);
	}

	// Token: 0x0600163A RID: 5690 RVA: 0x00096907 File Offset: 0x00094B07
	private void SetBool(string name, bool value)
	{
		this.flowchart.SetBooleanVariable(name, value);
	}

	// Token: 0x0600163B RID: 5691 RVA: 0x00096916 File Offset: 0x00094B16
	private void SetString(string name, string value)
	{
		this.flowchart.SetStringVariable(name, value);
	}

	// Token: 0x04001086 RID: 4230
	private Flowchart flowchart;
}
