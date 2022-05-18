using System;
using Fungus;
using UnityEngine;

// Token: 0x02000377 RID: 887
[EventHandlerInfo("NPCJiaoHu", "截杀按钮被点击", "截杀按钮被点击")]
[AddComponentMenu("")]
public class OnJieShaClick : Fungus.EventHandler
{
	// Token: 0x0600191C RID: 6428 RVA: 0x00015870 File Offset: 0x00013A70
	public virtual void Update()
	{
		if (UINPCJiaoHu.Inst != null && UINPCJiaoHu.Inst.IsJieShaClicked)
		{
			UINPCJiaoHu.Inst.IsJieShaClicked = false;
			this.ExecuteBlock();
		}
	}
}
