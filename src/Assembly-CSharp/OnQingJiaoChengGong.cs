using Fungus;
using UnityEngine;

[EventHandlerInfo("NPCJiaoHu", "当请教成功", "当请教成功")]
[AddComponentMenu("")]
public class OnQingJiaoChengGong : EventHandler
{
	public virtual void Update()
	{
		if ((Object)(object)UINPCJiaoHu.Inst != (Object)null && UINPCJiaoHu.Inst.IsQingJiaoChengGong)
		{
			UINPCJiaoHu.Inst.IsQingJiaoChengGong = false;
			Debug.Log((object)"请教成功");
			ExecuteBlock();
		}
	}
}
