using System;
using Fungus;
using UnityEngine;

// Token: 0x02000313 RID: 787
[CommandInfo("YSDongFu", "进入洞府", "进入洞府", 0)]
[AddComponentMenu("")]
public class CmdLoadDongFu : Command
{
	// Token: 0x06001759 RID: 5977 RVA: 0x000149E3 File Offset: 0x00012BE3
	public override void OnEnter()
	{
		DongFuManager.LoadDongFuScene(this.dongFuID);
		this.Continue();
	}

	// Token: 0x040012B7 RID: 4791
	[Tooltip("洞府ID")]
	[SerializeField]
	protected int dongFuID;
}
