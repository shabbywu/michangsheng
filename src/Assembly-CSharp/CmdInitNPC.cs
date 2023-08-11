using Fungus;
using UnityEngine;

[CommandInfo("YSNPCJiaoHu", "初始化NPC", "初始化NPC", 0)]
[AddComponentMenu("")]
public class CmdInitNPC : Command
{
	private Flowchart flowchart;

	public override void OnEnter()
	{
		flowchart = GetFlowchart();
		UINPCData nowJiaoHuNPC = UINPCJiaoHu.Inst.NowJiaoHuNPC;
		Set("npcid", nowJiaoHuNPC.ID);
		SetBool("IsThreeSceneNPC", nowJiaoHuNPC.IsThreeSceneNPC);
		SetString("Name", nowJiaoHuNPC.Name);
		Set("Sex", nowJiaoHuNPC.Sex);
		SetString("Title", nowJiaoHuNPC.Title);
		Set("Age", nowJiaoHuNPC.Age);
		Set("HP", nowJiaoHuNPC.HP);
		Set("QingFen", nowJiaoHuNPC.QingFen);
		Set("Exp", nowJiaoHuNPC.Exp);
		Set("Level", nowJiaoHuNPC.Level);
		SetString("LevelStr", nowJiaoHuNPC.LevelStr);
		Set("ZhuangTai", nowJiaoHuNPC.ZhuangTai);
		SetString("ZhuangTaiStr", nowJiaoHuNPC.ZhuangTaiStr);
		Set("ShouYuan", nowJiaoHuNPC.ShouYuan);
		Set("ZiZhi", nowJiaoHuNPC.ZiZhi);
		Set("WuXing", nowJiaoHuNPC.WuXing);
		Set("DunSu", nowJiaoHuNPC.DunSu);
		Set("ShenShi", nowJiaoHuNPC.ShenShi);
		Set("Favor", nowJiaoHuNPC.Favor);
		Set("FavorLevel", nowJiaoHuNPC.FavorLevel);
		Set("XingGe", nowJiaoHuNPC.XingGe);
		Set("ActionID", nowJiaoHuNPC.ActionID);
		Set("BigLevel", nowJiaoHuNPC.BigLevel);
		SetBool("IsKnowPlayer", nowJiaoHuNPC.IsKnowPlayer);
		SetBool("IsNingZhouNPC", nowJiaoHuNPC.IsNingZhouNPC);
		SetBool("IsNeedHelp", nowJiaoHuNPC.IsNeedHelp);
		SetBool("IsZhongYaoNPC", nowJiaoHuNPC.IsZhongYaoNPC);
		Set("ShiLi", nowJiaoHuNPC.MenPai);
		SetString("UUID", nowJiaoHuNPC.UUID);
		Set("NPCType", nowJiaoHuNPC.NPCType);
		Continue();
	}

	private void Set(string name, int value)
	{
		flowchart.SetIntegerVariable(name, value);
	}

	private void SetBool(string name, bool value)
	{
		flowchart.SetBooleanVariable(name, value);
	}

	private void SetString(string name, string value)
	{
		flowchart.SetStringVariable(name, value);
	}
}
