using Fungus;
using UnityEngine;

[CommandInfo("YSPlayer", "开始突破化神", "开始突破化神", 0)]
[AddComponentMenu("")]
public class CmdStartHuaShen : Command
{
	public override void OnEnter()
	{
		UIHuaShenRuDaoSelect.Inst.Show();
		Continue();
	}
}
