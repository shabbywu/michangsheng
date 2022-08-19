using System;
using Fungus;
using UnityEngine;

// Token: 0x0200025F RID: 607
[EventHandlerInfo("NPCJiaoHu", "聊天按钮被点击", "聊天按钮被点击")]
[AddComponentMenu("")]
public class OnLiaoTianClick : Fungus.EventHandler
{
	// Token: 0x0600166A RID: 5738 RVA: 0x0009744A File Offset: 0x0009564A
	public virtual void Update()
	{
		if (UINPCJiaoHu.Inst != null && UINPCJiaoHu.Inst.IsLiaoTianClicked)
		{
			UINPCJiaoHu.Inst.IsLiaoTianClicked = false;
			this.ExecuteBlock();
		}
	}
}
