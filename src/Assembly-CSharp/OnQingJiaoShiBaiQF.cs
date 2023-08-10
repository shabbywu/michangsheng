using Fungus;
using UnityEngine;

[EventHandlerInfo("NPCJiaoHu", "当请教失败(情分不够)", "当请教失败(情分不够)")]
[AddComponentMenu("")]
public class OnQingJiaoShiBaiQF : EventHandler
{
	public virtual void Update()
	{
		if ((Object)(object)UINPCJiaoHu.Inst != (Object)null && UINPCJiaoHu.Inst.IsQingJiaoShiBaiQF)
		{
			UINPCJiaoHu.Inst.IsQingJiaoShiBaiQF = false;
			Debug.Log((object)"请教失败(情分不够)");
			ExecuteBlock();
		}
	}
}
