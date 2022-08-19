using System;
using Fungus;
using UnityEngine;

// Token: 0x02000264 RID: 612
[EventHandlerInfo("NPCJiaoHu", "当请教失败(声望不够)", "当请教失败(声望不够)")]
[AddComponentMenu("")]
public class OnQingJiaoShiBaiSW : Fungus.EventHandler
{
	// Token: 0x06001674 RID: 5748 RVA: 0x0009753F File Offset: 0x0009573F
	public virtual void Update()
	{
		if (UINPCJiaoHu.Inst != null && UINPCJiaoHu.Inst.IsQingJiaoShiBaiSW)
		{
			UINPCJiaoHu.Inst.IsQingJiaoShiBaiSW = false;
			Debug.Log("请教失败(声望不够)");
			this.ExecuteBlock();
		}
	}
}
