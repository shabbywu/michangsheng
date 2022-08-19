using System;
using Fungus;
using UnityEngine;

// Token: 0x0200021F RID: 543
[CommandInfo("YSDongFu", "初始化表白", "初始化表白", 0)]
[AddComponentMenu("")]
public class CmdInitBiaoBai : Command
{
	// Token: 0x060015AF RID: 5551 RVA: 0x000913B3 File Offset: 0x0008F5B3
	public override void OnEnter()
	{
		BiaoBaiManager.InitBiaoBai();
		this.Continue();
	}
}
