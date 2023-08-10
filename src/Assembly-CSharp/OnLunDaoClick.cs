using Fungus;
using UnityEngine;

[EventHandlerInfo("NPCJiaoHu", "论道按钮被点击", "论道按钮被点击")]
[AddComponentMenu("")]
public class OnLunDaoClick : EventHandler
{
	public virtual void Update()
	{
		if ((Object)(object)UINPCJiaoHu.Inst != (Object)null && UINPCJiaoHu.Inst.IsLunDaoClicked)
		{
			UINPCJiaoHu.Inst.IsLunDaoClicked = false;
			ExecuteBlock();
		}
	}
}
