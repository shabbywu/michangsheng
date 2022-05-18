using System;
using Fungus;
using UnityEngine;

// Token: 0x0200037B RID: 891
[EventHandlerInfo("NPCJiaoHu", "当请教成功", "当请教成功")]
[AddComponentMenu("")]
public class OnQingJiaoChengGong : Fungus.EventHandler
{
	// Token: 0x06001924 RID: 6436 RVA: 0x00015924 File Offset: 0x00013B24
	public virtual void Update()
	{
		if (UINPCJiaoHu.Inst != null && UINPCJiaoHu.Inst.IsQingJiaoChengGong)
		{
			UINPCJiaoHu.Inst.IsQingJiaoChengGong = false;
			Debug.Log("请教成功");
			this.ExecuteBlock();
		}
	}
}
