using System;
using Fungus;
using UnityEngine;

// Token: 0x02000381 RID: 897
[EventHandlerInfo("NPCJiaoHu", "当完成一次赠礼", "当完成一次赠礼")]
[AddComponentMenu("")]
public class OnZengLiFinished : Fungus.EventHandler
{
	// Token: 0x06001930 RID: 6448 RVA: 0x000DF980 File Offset: 0x000DDB80
	public virtual void Update()
	{
		if (UINPCJiaoHu.Inst != null && UINPCJiaoHu.Inst.IsZengLiFinished)
		{
			UINPCJiaoHu.Inst.IsZengLiFinished = false;
			Flowchart component = base.GetComponent<Flowchart>();
			if (component != null)
			{
				component.SetIntegerVariable("TmpValue", UINPCJiaoHu.Inst.ZengLiArg.JieGuo);
			}
			Flowchart component2 = base.GetComponent<Flowchart>();
			if (component2 != null)
			{
				component2.SetIntegerVariable("itemlevel", UINPCJiaoHu.Inst.ZengLiArg.Item.quality);
			}
			Flowchart component3 = base.GetComponent<Flowchart>();
			if (component3 != null)
			{
				component3.SetIntegerVariable("itemtype", UINPCJiaoHu.Inst.ZengLiArg.Item.itemtype);
			}
			this.ExecuteBlock();
		}
	}
}
