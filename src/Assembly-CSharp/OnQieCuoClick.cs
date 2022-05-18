using System;
using Fungus;
using UnityEngine;

// Token: 0x0200037A RID: 890
[EventHandlerInfo("NPCJiaoHu", "切磋按钮被点击", "切磋按钮被点击")]
[AddComponentMenu("")]
public class OnQieCuoClick : Fungus.EventHandler
{
	// Token: 0x06001922 RID: 6434 RVA: 0x000158F7 File Offset: 0x00013AF7
	public virtual void Update()
	{
		if (UINPCJiaoHu.Inst != null && UINPCJiaoHu.Inst.IsQieCuoClicked)
		{
			UINPCJiaoHu.Inst.IsQieCuoClicked = false;
			this.ExecuteBlock();
		}
	}
}
