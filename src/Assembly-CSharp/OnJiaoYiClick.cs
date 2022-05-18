using System;
using Fungus;
using UnityEngine;

// Token: 0x02000376 RID: 886
[EventHandlerInfo("NPCJiaoHu", "交易按钮被点击", "交易按钮被点击")]
[AddComponentMenu("")]
public class OnJiaoYiClick : Fungus.EventHandler
{
	// Token: 0x0600191A RID: 6426 RVA: 0x00015843 File Offset: 0x00013A43
	public virtual void Update()
	{
		if (UINPCJiaoHu.Inst != null && UINPCJiaoHu.Inst.IsJiaoYiClicked)
		{
			UINPCJiaoHu.Inst.IsJiaoYiClicked = false;
			this.ExecuteBlock();
		}
	}
}
