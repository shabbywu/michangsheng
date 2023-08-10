using Fungus;
using UnityEngine;

[CommandInfo("YSDongFu", "切换洞府模式", "切换洞府模式", 0)]
[AddComponentMenu("")]
public class CmdSwitchDFMode : Command
{
	public override void OnEnter()
	{
		if (DongFuScene.Inst.InteractiveMode == DFInteractiveMode.Function)
		{
			DongFuScene.Inst.InteractiveMode = DFInteractiveMode.Decorate;
		}
		else
		{
			DongFuScene.Inst.InteractiveMode = DFInteractiveMode.Function;
		}
		Continue();
	}
}
