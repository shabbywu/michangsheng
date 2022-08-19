using System;
using Fungus;
using UnityEngine;

// Token: 0x02000263 RID: 611
[EventHandlerInfo("NPCJiaoHu", "当请教失败(情分不够)", "当请教失败(情分不够)")]
[AddComponentMenu("")]
public class OnQingJiaoShiBaiQF : Fungus.EventHandler
{
	// Token: 0x06001672 RID: 5746 RVA: 0x00097508 File Offset: 0x00095708
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
