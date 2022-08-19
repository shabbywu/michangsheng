using System;
using Fungus;
using UnityEngine;

// Token: 0x020001FE RID: 510
[CommandInfo("YSDongFu", "进入洞府", "进入洞府", 0)]
[AddComponentMenu("")]
public class CmdLoadDongFu : Command
{
	// Token: 0x060014AF RID: 5295 RVA: 0x00084C31 File Offset: 0x00082E31
	public override void OnEnter()
	{
		DongFuManager.LoadDongFuScene(this.dongFuID);
		this.Continue();
	}

	// Token: 0x04000F71 RID: 3953
	[Tooltip("洞府ID")]
	[SerializeField]
	protected int dongFuID;
}
