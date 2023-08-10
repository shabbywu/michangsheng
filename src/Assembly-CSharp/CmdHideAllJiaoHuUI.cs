using Fungus;
using UnityEngine;

[CommandInfo("YSNPCJiaoHu", "设置交互UI的隐藏", "设置交互UI的隐藏，为true时隐藏，为false时显示", 0)]
[AddComponentMenu("")]
public class CmdHideAllJiaoHuUI : Command
{
	[SerializeField]
	[Tooltip("为true时隐藏，为false时显示")]
	protected bool isHide;

	public override void OnEnter()
	{
		UINPCJiaoHu.AllShouldHide = isHide;
		Continue();
	}
}
