using System;
using Fungus;
using UnityEngine;

// Token: 0x02000375 RID: 885
[EventHandlerInfo("NPCJiaoHu", "固定NPC被点击", "固定NPC被点击")]
[AddComponentMenu("")]
public class OnGuDingNPCClick : Fungus.EventHandler
{
	// Token: 0x06001918 RID: 6424 RVA: 0x0001580E File Offset: 0x00013A0E
	public virtual void Update()
	{
		if (UINPCJiaoHu.Inst != null && UINPCJiaoHu.Inst.IsGuDingNPCClicked)
		{
			UINPCJiaoHu.Inst.IsGuDingNPCClicked = false;
			this.ExecuteBlock();
		}
	}
}
