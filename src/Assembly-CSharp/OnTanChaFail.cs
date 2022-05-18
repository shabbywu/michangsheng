using System;
using Fungus;
using UnityEngine;

// Token: 0x0200037F RID: 895
[EventHandlerInfo("NPCJiaoHu", "当探查失败", "当探查失败或探查成功但被发现时")]
[AddComponentMenu("")]
public class OnTanChaFail : Fungus.EventHandler
{
	// Token: 0x0600192C RID: 6444 RVA: 0x000159C9 File Offset: 0x00013BC9
	public virtual void Update()
	{
		if (UINPCJiaoHu.Inst != null && UINPCJiaoHu.Inst.IsTanChaShiBaiOrFaXian)
		{
			UINPCJiaoHu.Inst.IsTanChaShiBaiOrFaXian = false;
			this.ExecuteBlock();
		}
	}
}
