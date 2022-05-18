using System;
using Fungus;
using UnityEngine;

// Token: 0x02000380 RID: 896
[EventHandlerInfo("NPCJiaoHu", "当完成一次威胁", "威胁结果:\n0(概率成功)\n1(成功1)\n2(成功2)\n3(成功3)\n4(成功4)\n5(初次威胁失败)\n6(概率失败1)\n7(概率失败2)\n11(失败1)\n12(失败2)\n13(失败3)\n14(失败4)\n15(失败5)\n16(失败6)\n其他数据:\n好友1ID TmpInt1\n好友2ID TmpInt2\n好友3ID TmpInt3\n好友数量 FriendCount\n是否是首次威胁 TmpBoolValue")]
[AddComponentMenu("")]
public class OnWeiXieFinished : Fungus.EventHandler
{
	// Token: 0x0600192E RID: 6446 RVA: 0x000DF810 File Offset: 0x000DDA10
	public virtual void Update()
	{
		if (UINPCJiaoHu.Inst != null && UINPCJiaoHu.Inst.IsWeiXieFinished)
		{
			UINPCJiaoHu.Inst.IsWeiXieFinished = false;
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
				component4.SetBooleanVariable("TmpBoolValue", NPCEx.IsFirstWeiXie);
			}
			Flowchart component5 = base.GetComponent<Flowchart>();
			if (component5 != null)
			{
				component5.SetIntegerVariable("FriendCount", NPCEx.WeiXieFriendCount);
			}
			for (int i = 0; i < NPCEx.WeiXieFriendCount; i++)
			{
				Flowchart component6 = base.GetComponent<Flowchart>();
				if (component6 != null)
				{
					component6.SetIntegerVariable(string.Format("TmpInt{0}", i + 1), NPCEx.WeiXieFriends[i].ID);
				}
			}
			if (UINPCJiaoHu.Inst.WeiXieArg != null)
			{
				Flowchart component7 = base.GetComponent<Flowchart>();
				if (component7 != null)
				{
					component7.SetIntegerVariable("FriendID", UINPCJiaoHu.Inst.WeiXieArg.FriendID);
				}
			}
			this.ExecuteBlock();
		}
	}
}
