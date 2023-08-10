using Fungus;
using UnityEngine;

[EventHandlerInfo("NPCJiaoHu", "交易按钮被点击", "交易按钮被点击")]
[AddComponentMenu("")]
public class OnJiaoYiClick : EventHandler
{
	public virtual void Update()
	{
		if ((Object)(object)UINPCJiaoHu.Inst != (Object)null && UINPCJiaoHu.Inst.IsJiaoYiClicked)
		{
			UINPCJiaoHu.Inst.IsJiaoYiClicked = false;
			ExecuteBlock();
		}
	}
}
