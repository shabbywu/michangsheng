using System;
using Fungus;
using UnityEngine;

// Token: 0x020001FB RID: 507
[CommandInfo("YSDongFu", "给与玩家洞府", "给与玩家洞府", 0)]
[AddComponentMenu("")]
public class CmdCreateDongFu : Command
{
	// Token: 0x060014A6 RID: 5286 RVA: 0x00084B1C File Offset: 0x00082D1C
	public override void OnEnter()
	{
		DongFuManager.CreateDongFu(this.DongFuID, this.level);
		this.Continue();
	}

	// Token: 0x04000F6D RID: 3949
	[Tooltip("洞府ID")]
	[SerializeField]
	protected int DongFuID;

	// Token: 0x04000F6E RID: 3950
	[Tooltip("灵眼等级 限定123")]
	[SerializeField]
	protected int level;
}
