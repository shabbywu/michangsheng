using System;
using Fungus;
using UnityEngine;

// Token: 0x020003E3 RID: 995
[CommandInfo("渡劫", "打开渡劫准备UI", "打开渡劫准备UI", 0)]
[AddComponentMenu("")]
public class CmdOpenDuJieZhunBeiUI : Command
{
	// Token: 0x06001B1E RID: 6942 RVA: 0x00016F02 File Offset: 0x00015102
	public override void OnEnter()
	{
		UIDuJieZhunBei.OpenPanel(false);
		this.Continue();
	}
}
