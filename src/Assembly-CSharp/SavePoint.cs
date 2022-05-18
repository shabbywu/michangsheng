using System;
using Fungus;
using UnityEngine;

// Token: 0x02000253 RID: 595
[CommandInfo("YSTools", "存储点", "存储点", 0)]
[AddComponentMenu("")]
public class SavePoint : Command
{
	// Token: 0x06001214 RID: 4628 RVA: 0x00011424 File Offset: 0x0000F624
	public override void OnEnter()
	{
		this.Continue();
	}
}
