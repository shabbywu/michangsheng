using System;
using Fungus;
using UnityEngine;

// Token: 0x0200025E RID: 606
[EventHandlerInfo("NPCJiaoHu", "截杀按钮被点击", "截杀按钮被点击")]
[AddComponentMenu("")]
public class OnJieShaClick : Fungus.EventHandler
{
	// Token: 0x06001668 RID: 5736 RVA: 0x0009741D File Offset: 0x0009561D
	public virtual void Update()
	{
		if (UINPCJiaoHu.Inst != null && UINPCJiaoHu.Inst.IsJieShaClicked)
		{
			UINPCJiaoHu.Inst.IsJieShaClicked = false;
			this.ExecuteBlock();
		}
	}
}
