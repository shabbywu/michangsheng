using System;
using Fungus;
using UnityEngine;

// Token: 0x02000378 RID: 888
[EventHandlerInfo("NPCJiaoHu", "聊天按钮被点击", "聊天按钮被点击")]
[AddComponentMenu("")]
public class OnLiaoTianClick : Fungus.EventHandler
{
	// Token: 0x0600191E RID: 6430 RVA: 0x0001589D File Offset: 0x00013A9D
	public virtual void Update()
	{
		if (UINPCJiaoHu.Inst != null && UINPCJiaoHu.Inst.IsLiaoTianClicked)
		{
			UINPCJiaoHu.Inst.IsLiaoTianClicked = false;
			this.ExecuteBlock();
		}
	}
}
