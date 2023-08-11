using Fungus;
using UnityEngine;

[EventHandlerInfo("NPCJiaoHu", "当完成一次威胁", "威胁结果:\n0(概率成功)\n1(成功1)\n2(成功2)\n3(成功3)\n4(成功4)\n5(初次威胁失败)\n6(概率失败1)\n7(概率失败2)\n11(失败1)\n12(失败2)\n13(失败3)\n14(失败4)\n15(失败5)\n16(失败6)\n其他数据:\n好友1ID TmpInt1\n好友2ID TmpInt2\n好友3ID TmpInt3\n好友数量 FriendCount\n是否是首次威胁 TmpBoolValue")]
[AddComponentMenu("")]
public class OnWeiXieFinished : EventHandler
{
	public virtual void Update()
	{
		if ((Object)(object)UINPCJiaoHu.Inst != (Object)null && UINPCJiaoHu.Inst.IsWeiXieFinished)
		{
			UINPCJiaoHu.Inst.IsWeiXieFinished = false;
			UINPCJiaoHu.Inst.SuoQu.RefreshUI();
			((Component)this).GetComponent<Flowchart>()?.SetIntegerVariable("TmpValue", UINPCJiaoHu.Inst.ZengLiArg.JieGuo);
			((Component)this).GetComponent<Flowchart>()?.SetIntegerVariable("itemlevel", UINPCJiaoHu.Inst.ZengLiArg.Item.quality);
			((Component)this).GetComponent<Flowchart>()?.SetIntegerVariable("itemtype", UINPCJiaoHu.Inst.ZengLiArg.Item.itemtype);
			((Component)this).GetComponent<Flowchart>()?.SetBooleanVariable("TmpBoolValue", NPCEx.IsFirstWeiXie);
			((Component)this).GetComponent<Flowchart>()?.SetIntegerVariable("FriendCount", NPCEx.WeiXieFriendCount);
			for (int i = 0; i < NPCEx.WeiXieFriendCount; i++)
			{
				((Component)this).GetComponent<Flowchart>()?.SetIntegerVariable($"TmpInt{i + 1}", NPCEx.WeiXieFriends[i].ID);
			}
			if (UINPCJiaoHu.Inst.WeiXieArg != null)
			{
				((Component)this).GetComponent<Flowchart>()?.SetIntegerVariable("FriendID", UINPCJiaoHu.Inst.WeiXieArg.FriendID);
			}
			ExecuteBlock();
		}
	}
}
