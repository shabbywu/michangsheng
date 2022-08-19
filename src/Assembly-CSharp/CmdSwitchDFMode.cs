using System;
using Fungus;
using UnityEngine;

// Token: 0x02000201 RID: 513
[CommandInfo("YSDongFu", "切换洞府模式", "切换洞府模式", 0)]
[AddComponentMenu("")]
public class CmdSwitchDFMode : Command
{
	// Token: 0x060014B7 RID: 5303 RVA: 0x00084CF2 File Offset: 0x00082EF2
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
		this.Continue();
	}
}
