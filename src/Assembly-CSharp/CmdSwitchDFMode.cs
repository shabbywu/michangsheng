using System;
using Fungus;
using UnityEngine;

// Token: 0x02000316 RID: 790
[CommandInfo("YSDongFu", "切换洞府模式", "切换洞府模式", 0)]
[AddComponentMenu("")]
public class CmdSwitchDFMode : Command
{
	// Token: 0x06001761 RID: 5985 RVA: 0x00014A59 File Offset: 0x00012C59
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
