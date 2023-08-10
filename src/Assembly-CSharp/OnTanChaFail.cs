using Fungus;
using UnityEngine;

[EventHandlerInfo("NPCJiaoHu", "当探查失败", "当探查失败或探查成功但被发现时")]
[AddComponentMenu("")]
public class OnTanChaFail : EventHandler
{
	public virtual void Update()
	{
		if ((Object)(object)UINPCJiaoHu.Inst != (Object)null && UINPCJiaoHu.Inst.IsTanChaShiBaiOrFaXian)
		{
			UINPCJiaoHu.Inst.IsTanChaShiBaiOrFaXian = false;
			ExecuteBlock();
		}
	}
}
