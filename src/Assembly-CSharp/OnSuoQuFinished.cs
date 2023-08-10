using Fungus;
using UnityEngine;

[EventHandlerInfo("NPCJiaoHu", "当完成一次索取", "索取结果TmpValue\n0索取成功(正常)\n1索取成功(垃圾)(SuoQuLaJi)\n2索取失败(失败1)(SuoQuShiBai1)\n3索取失败(失败2)(SuoQuShiBai2)\n好友1ID TmpStr1\n好友2ID TmpStr2\n好友3ID TmpStr3\n好友数量 FriendCount")]
[AddComponentMenu("")]
public class OnSuoQuFinished : EventHandler
{
	public virtual void Update()
	{
		if ((Object)(object)UINPCJiaoHu.Inst != (Object)null && UINPCJiaoHu.Inst.IsSuoQuFinished)
		{
			UINPCJiaoHu.Inst.IsSuoQuFinished = false;
			UINPCJiaoHu.Inst.SuoQu.RefreshUI();
			((Component)this).GetComponent<Flowchart>()?.SetIntegerVariable("TmpValue", UINPCJiaoHu.Inst.ZengLiArg.JieGuo);
			((Component)this).GetComponent<Flowchart>()?.SetIntegerVariable("itemlevel", UINPCJiaoHu.Inst.ZengLiArg.Item.quality);
			((Component)this).GetComponent<Flowchart>()?.SetIntegerVariable("itemtype", UINPCJiaoHu.Inst.ZengLiArg.Item.itemtype);
			((Component)this).GetComponent<Flowchart>()?.SetIntegerVariable("FriendCount", NPCEx.WeiXieFriendCount);
			for (int i = 0; i < NPCEx.WeiXieFriendCount; i++)
			{
				((Component)this).GetComponent<Flowchart>()?.SetStringVariable($"TmpStr{i + 1}", NPCEx.WeiXieFriends[i].ID.ToString());
			}
			ExecuteBlock();
		}
	}
}
