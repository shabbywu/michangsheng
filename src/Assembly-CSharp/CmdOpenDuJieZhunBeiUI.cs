using System;
using Fungus;
using UnityEngine;

// Token: 0x020002A8 RID: 680
[CommandInfo("渡劫", "打开渡劫准备UI", "打开渡劫准备UI", 0)]
[AddComponentMenu("")]
public class CmdOpenDuJieZhunBeiUI : Command
{
	// Token: 0x06001825 RID: 6181 RVA: 0x000A8ACC File Offset: 0x000A6CCC
	public override void OnEnter()
	{
		UIDuJieZhunBei.OpenPanel(false);
		this.Continue();
	}
}
