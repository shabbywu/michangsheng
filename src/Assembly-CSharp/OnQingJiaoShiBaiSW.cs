using System;
using Fungus;
using UnityEngine;

// Token: 0x0200037D RID: 893
[EventHandlerInfo("NPCJiaoHu", "当请教失败(声望不够)", "当请教失败(声望不够)")]
[AddComponentMenu("")]
public class OnQingJiaoShiBaiSW : Fungus.EventHandler
{
	// Token: 0x06001928 RID: 6440 RVA: 0x00015992 File Offset: 0x00013B92
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
