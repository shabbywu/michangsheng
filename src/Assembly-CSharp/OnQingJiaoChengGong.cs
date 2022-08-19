using System;
using Fungus;
using UnityEngine;

// Token: 0x02000262 RID: 610
[EventHandlerInfo("NPCJiaoHu", "当请教成功", "当请教成功")]
[AddComponentMenu("")]
public class OnQingJiaoChengGong : Fungus.EventHandler
{
	// Token: 0x06001670 RID: 5744 RVA: 0x000974D1 File Offset: 0x000956D1
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
