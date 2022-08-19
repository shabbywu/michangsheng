using System;
using Fungus;
using UnityEngine;

// Token: 0x02000265 RID: 613
[EventHandlerInfo("NPCJiaoHu", "当完成一次索取", "索取结果TmpValue\n0索取成功(正常)\n1索取成功(垃圾)(SuoQuLaJi)\n2索取失败(失败1)(SuoQuShiBai1)\n3索取失败(失败2)(SuoQuShiBai2)\n好友1ID TmpStr1\n好友2ID TmpStr2\n好友3ID TmpStr3\n好友数量 FriendCount")]
[AddComponentMenu("")]
public class OnSuoQuFinished : Fungus.EventHandler
{
	// Token: 0x06001676 RID: 5750 RVA: 0x00097578 File Offset: 0x00095778
	public virtual void Update()
	{
		if (UINPCJiaoHu.Inst != null && UINPCJiaoHu.Inst.IsSuoQuFinished)
		{
			UINPCJiaoHu.Inst.IsSuoQuFinished = false;
			UINPCJiaoHu.Inst.SuoQu.RefreshUI();
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
			Flowchart component4 = base.GetComponent<Flowchart>();
			if (component4 != null)
			{
				component4.SetIntegerVariable("FriendCount", NPCEx.WeiXieFriendCount);
			}
			for (int i = 0; i < NPCEx.WeiXieFriendCount; i++)
			{
				Flowchart component5 = base.GetComponent<Flowchart>();
				if (component5 != null)
				{
					component5.SetStringVariable(string.Format("TmpStr{0}", i + 1), NPCEx.WeiXieFriends[i].ID.ToString());
				}
			}
			this.ExecuteBlock();
		}
	}
}
