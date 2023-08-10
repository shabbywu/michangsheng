using Fungus;
using UnityEngine;

[CommandInfo("YSDongFu", "开始双修", "开始双修", 0)]
[AddComponentMenu("")]
public class CmdStartShuangXiu : Command
{
	public override void OnEnter()
	{
		UINPCJiaoHu.Inst.ShowNPCShuangXiuSelect();
		Continue();
	}
}
