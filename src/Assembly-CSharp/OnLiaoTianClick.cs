using Fungus;
using UnityEngine;

[EventHandlerInfo("NPCJiaoHu", "聊天按钮被点击", "聊天按钮被点击")]
[AddComponentMenu("")]
public class OnLiaoTianClick : EventHandler
{
	public virtual void Update()
	{
		if ((Object)(object)UINPCJiaoHu.Inst != (Object)null && UINPCJiaoHu.Inst.IsLiaoTianClicked)
		{
			UINPCJiaoHu.Inst.IsLiaoTianClicked = false;
			ExecuteBlock();
		}
	}
}
