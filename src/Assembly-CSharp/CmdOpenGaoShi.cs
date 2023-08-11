using Fungus;
using UnityEngine;

[CommandInfo("YSPlayer", "打开告示", "打开告示", 0)]
[AddComponentMenu("")]
public class CmdOpenGaoShi : Command
{
	public override void OnEnter()
	{
		UIGaoShi.Inst.Show();
		Continue();
	}
}
