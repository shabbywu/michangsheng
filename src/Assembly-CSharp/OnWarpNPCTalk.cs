using System;
using Fungus;
using UnityEngine;

// Token: 0x02000267 RID: 615
[EventHandlerInfo("NPCJiaoHu", "当跳转到NPCTalk", "当跳转到NPCTalk")]
[AddComponentMenu("")]
public class OnWarpNPCTalk : Fungus.EventHandler
{
	// Token: 0x0600167A RID: 5754 RVA: 0x000976CE File Offset: 0x000958CE
	public virtual void Update()
	{
		if (UINPCJiaoHu.Inst != null && UINPCJiaoHu.Inst.IsNeedWarpToNPCTalk)
		{
			UINPCJiaoHu.Inst.IsNeedWarpToNPCTalk = false;
			this.ExecuteBlock();
		}
	}
}
