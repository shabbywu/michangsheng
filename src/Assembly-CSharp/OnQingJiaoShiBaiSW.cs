using Fungus;
using UnityEngine;

[EventHandlerInfo("NPCJiaoHu", "当请教失败(声望不够)", "当请教失败(声望不够)")]
[AddComponentMenu("")]
public class OnQingJiaoShiBaiSW : EventHandler
{
	public virtual void Update()
	{
		if ((Object)(object)UINPCJiaoHu.Inst != (Object)null && UINPCJiaoHu.Inst.IsQingJiaoShiBaiSW)
		{
			UINPCJiaoHu.Inst.IsQingJiaoShiBaiSW = false;
			Debug.Log((object)"请教失败(声望不够)");
			ExecuteBlock();
		}
	}
}
