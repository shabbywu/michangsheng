using System;
using Fungus;
using UnityEngine;

// Token: 0x02000310 RID: 784
[CommandInfo("YSDongFu", "给与玩家洞府", "给与玩家洞府", 0)]
[AddComponentMenu("")]
public class CmdCreateDongFu : Command
{
	// Token: 0x06001750 RID: 5968 RVA: 0x0001492F File Offset: 0x00012B2F
	public override void OnEnter()
	{
		DongFuManager.CreateDongFu(this.DongFuID, this.level);
		this.Continue();
	}

	// Token: 0x040012B3 RID: 4787
	[Tooltip("洞府ID")]
	[SerializeField]
	protected int DongFuID;

	// Token: 0x040012B4 RID: 4788
	[Tooltip("灵眼等级 限定123")]
	[SerializeField]
	protected int level;
}
