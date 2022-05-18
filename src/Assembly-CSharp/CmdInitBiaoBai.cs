using System;
using Fungus;
using UnityEngine;

// Token: 0x0200033B RID: 827
[CommandInfo("YSDongFu", "初始化表白", "初始化表白", 0)]
[AddComponentMenu("")]
public class CmdInitBiaoBai : Command
{
	// Token: 0x06001867 RID: 6247 RVA: 0x00015324 File Offset: 0x00013524
	public override void OnEnter()
	{
		BiaoBaiManager.InitBiaoBai();
		this.Continue();
	}
}
