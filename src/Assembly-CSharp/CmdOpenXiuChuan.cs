using Fungus;
using UnityEngine;

[CommandInfo("YSTool", "打开修船界面", "打开修船界面", 0)]
[AddComponentMenu("")]
public class CmdOpenXiuChuan : Command
{
	public override void OnEnter()
	{
		UIXiuChuanPanel.OpenDefaultXiuChuan();
		Continue();
	}
}
