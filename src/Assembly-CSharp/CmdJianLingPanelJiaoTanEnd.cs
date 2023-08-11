using Fungus;
using UnityEngine;

[CommandInfo("剑灵", "剑灵主界面交谈结束", "剑灵主界面交谈结束", 0)]
[AddComponentMenu("")]
public class CmdJianLingPanelJiaoTanEnd : Command
{
	public override void OnEnter()
	{
		if ((Object)(object)UIJianLingPanel.Inst != (Object)null)
		{
			UIJianLingPanel.Inst.OnJiaoTanEnd();
			Continue();
		}
	}
}
