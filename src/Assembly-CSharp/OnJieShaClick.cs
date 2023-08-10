using Fungus;
using UnityEngine;

[EventHandlerInfo("NPCJiaoHu", "截杀按钮被点击", "截杀按钮被点击")]
[AddComponentMenu("")]
public class OnJieShaClick : EventHandler
{
	public virtual void Update()
	{
		if ((Object)(object)UINPCJiaoHu.Inst != (Object)null && UINPCJiaoHu.Inst.IsJieShaClicked)
		{
			UINPCJiaoHu.Inst.IsJieShaClicked = false;
			ExecuteBlock();
		}
	}
}
