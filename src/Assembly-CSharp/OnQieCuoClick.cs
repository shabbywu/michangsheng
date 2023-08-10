using Fungus;
using UnityEngine;

[EventHandlerInfo("NPCJiaoHu", "切磋按钮被点击", "切磋按钮被点击")]
[AddComponentMenu("")]
public class OnQieCuoClick : EventHandler
{
	public virtual void Update()
	{
		if ((Object)(object)UINPCJiaoHu.Inst != (Object)null && UINPCJiaoHu.Inst.IsQieCuoClicked)
		{
			UINPCJiaoHu.Inst.IsQieCuoClicked = false;
			ExecuteBlock();
		}
	}
}
