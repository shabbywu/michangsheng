using System;
using Fungus;
using UnityEngine;

// Token: 0x0200037C RID: 892
[EventHandlerInfo("NPCJiaoHu", "当请教失败(情分不够)", "当请教失败(情分不够)")]
[AddComponentMenu("")]
public class OnQingJiaoShiBaiQF : Fungus.EventHandler
{
	// Token: 0x06001926 RID: 6438 RVA: 0x0001595B File Offset: 0x00013B5B
	public virtual void Update()
	{
		if (UINPCJiaoHu.Inst != null && UINPCJiaoHu.Inst.IsQingJiaoShiBaiQF)
		{
			UINPCJiaoHu.Inst.IsQingJiaoShiBaiQF = false;
			Debug.Log("请教失败(情分不够)");
			this.ExecuteBlock();
		}
	}
}
