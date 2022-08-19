using System;
using Fungus;
using UnityEngine;

// Token: 0x02000260 RID: 608
[EventHandlerInfo("NPCJiaoHu", "论道按钮被点击", "论道按钮被点击")]
[AddComponentMenu("")]
public class OnLunDaoClick : Fungus.EventHandler
{
	// Token: 0x0600166C RID: 5740 RVA: 0x00097477 File Offset: 0x00095677
	public virtual void Update()
	{
		if (UINPCJiaoHu.Inst != null && UINPCJiaoHu.Inst.IsLunDaoClicked)
		{
			UINPCJiaoHu.Inst.IsLunDaoClicked = false;
			this.ExecuteBlock();
		}
	}
}
