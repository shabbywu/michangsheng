using System;
using Fungus;
using UnityEngine;

// Token: 0x0200025C RID: 604
[EventHandlerInfo("NPCJiaoHu", "固定NPC被点击", "固定NPC被点击")]
[AddComponentMenu("")]
public class OnGuDingNPCClick : Fungus.EventHandler
{
	// Token: 0x06001664 RID: 5732 RVA: 0x000973BB File Offset: 0x000955BB
	public virtual void Update()
	{
		if (UINPCJiaoHu.Inst != null && UINPCJiaoHu.Inst.IsGuDingNPCClicked)
		{
			UINPCJiaoHu.Inst.IsGuDingNPCClicked = false;
			this.ExecuteBlock();
		}
	}
}
