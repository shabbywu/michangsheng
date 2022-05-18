using System;
using Fungus;
using UnityEngine;

// Token: 0x02000379 RID: 889
[EventHandlerInfo("NPCJiaoHu", "论道按钮被点击", "论道按钮被点击")]
[AddComponentMenu("")]
public class OnLunDaoClick : Fungus.EventHandler
{
	// Token: 0x06001920 RID: 6432 RVA: 0x000158CA File Offset: 0x00013ACA
	public virtual void Update()
	{
		if (UINPCJiaoHu.Inst != null && UINPCJiaoHu.Inst.IsLunDaoClicked)
		{
			UINPCJiaoHu.Inst.IsLunDaoClicked = false;
			this.ExecuteBlock();
		}
	}
}
