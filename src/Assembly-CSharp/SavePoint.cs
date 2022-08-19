using System;
using Fungus;
using UnityEngine;

// Token: 0x02000176 RID: 374
[CommandInfo("YSTools", "存储点", "存储点", 0)]
[AddComponentMenu("")]
public class SavePoint : Command
{
	// Token: 0x06000FB4 RID: 4020 RVA: 0x0005E3AF File Offset: 0x0005C5AF
	public override void OnEnter()
	{
		this.Continue();
	}
}
