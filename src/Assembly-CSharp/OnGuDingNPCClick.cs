using Fungus;
using UnityEngine;

[EventHandlerInfo("NPCJiaoHu", "固定NPC被点击", "固定NPC被点击")]
[AddComponentMenu("")]
public class OnGuDingNPCClick : EventHandler
{
	public virtual void Update()
	{
		if ((Object)(object)UINPCJiaoHu.Inst != (Object)null && UINPCJiaoHu.Inst.IsGuDingNPCClicked)
		{
			UINPCJiaoHu.Inst.IsGuDingNPCClicked = false;
			ExecuteBlock();
		}
	}
}
