using System;
using Fungus;
using UnityEngine;

// Token: 0x02000266 RID: 614
[EventHandlerInfo("NPCJiaoHu", "当探查失败", "当探查失败或探查成功但被发现时")]
[AddComponentMenu("")]
public class OnTanChaFail : Fungus.EventHandler
{
	// Token: 0x06001678 RID: 5752 RVA: 0x000976A1 File Offset: 0x000958A1
	public virtual void Update()
	{
		if (UINPCJiaoHu.Inst != null && UINPCJiaoHu.Inst.IsTanChaShiBaiOrFaXian)
		{
			UINPCJiaoHu.Inst.IsTanChaShiBaiOrFaXian = false;
			this.ExecuteBlock();
		}
	}
}
