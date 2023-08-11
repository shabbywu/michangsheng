using Fungus;
using UnityEngine;

[CommandInfo("YSNPCJiaoHu", "打开NPCPop", "打开当前NPC的Pop", 0)]
[AddComponentMenu("")]
public class CmdOpenNPCPop : Command
{
	public override void OnEnter()
	{
		UINPCData nowJiaoHuNPC = UINPCJiaoHu.Inst.NowJiaoHuNPC;
		for (int i = 0; i < ((Transform)UINPCJiaoHu.Inst.NPCList.SVTransform).childCount; i++)
		{
			UINPCSVItem component = ((Component)((Transform)UINPCJiaoHu.Inst.NPCList.SVTransform).GetChild(i)).GetComponent<UINPCSVItem>();
			if (component.NPCData.ID == nowJiaoHuNPC.ID)
			{
				component.OnClick();
				break;
			}
		}
		Continue();
	}
}
