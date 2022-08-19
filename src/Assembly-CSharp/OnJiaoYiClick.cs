using System;
using Fungus;
using UnityEngine;

// Token: 0x0200025D RID: 605
[EventHandlerInfo("NPCJiaoHu", "交易按钮被点击", "交易按钮被点击")]
[AddComponentMenu("")]
public class OnJiaoYiClick : Fungus.EventHandler
{
	// Token: 0x06001666 RID: 5734 RVA: 0x000973F0 File Offset: 0x000955F0
	public virtual void Update()
	{
		if (UINPCJiaoHu.Inst != null && UINPCJiaoHu.Inst.IsJiaoYiClicked)
		{
			UINPCJiaoHu.Inst.IsJiaoYiClicked = false;
			this.ExecuteBlock();
		}
	}
}
