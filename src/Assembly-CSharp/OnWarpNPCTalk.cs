using Fungus;
using UnityEngine;

[EventHandlerInfo("NPCJiaoHu", "当跳转到NPCTalk", "当跳转到NPCTalk")]
[AddComponentMenu("")]
public class OnWarpNPCTalk : EventHandler
{
	public virtual void Update()
	{
		if ((Object)(object)UINPCJiaoHu.Inst != (Object)null && UINPCJiaoHu.Inst.IsNeedWarpToNPCTalk)
		{
			UINPCJiaoHu.Inst.IsNeedWarpToNPCTalk = false;
			ExecuteBlock();
		}
	}
}
