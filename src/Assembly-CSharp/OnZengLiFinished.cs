using Fungus;
using UnityEngine;

[EventHandlerInfo("NPCJiaoHu", "当完成一次赠礼", "当完成一次赠礼")]
[AddComponentMenu("")]
public class OnZengLiFinished : EventHandler
{
	public virtual void Update()
	{
		if ((Object)(object)UINPCJiaoHu.Inst != (Object)null && UINPCJiaoHu.Inst.IsZengLiFinished)
		{
			UINPCJiaoHu.Inst.IsZengLiFinished = false;
			((Component)this).GetComponent<Flowchart>()?.SetIntegerVariable("TmpValue", UINPCJiaoHu.Inst.ZengLiArg.JieGuo);
			((Component)this).GetComponent<Flowchart>()?.SetIntegerVariable("itemlevel", UINPCJiaoHu.Inst.ZengLiArg.Item.quality);
			((Component)this).GetComponent<Flowchart>()?.SetIntegerVariable("itemtype", UINPCJiaoHu.Inst.ZengLiArg.Item.itemtype);
			ExecuteBlock();
		}
	}
}
