using System;
using Fungus;
using UnityEngine;

// Token: 0x02000261 RID: 609
[EventHandlerInfo("NPCJiaoHu", "切磋按钮被点击", "切磋按钮被点击")]
[AddComponentMenu("")]
public class OnQieCuoClick : Fungus.EventHandler
{
	// Token: 0x0600166E RID: 5742 RVA: 0x000974A4 File Offset: 0x000956A4
	public virtual void Update()
	{
		if (UINPCJiaoHu.Inst != null && UINPCJiaoHu.Inst.IsQieCuoClicked)
		{
			UINPCJiaoHu.Inst.IsQieCuoClicked = false;
			this.ExecuteBlock();
		}
	}
}
