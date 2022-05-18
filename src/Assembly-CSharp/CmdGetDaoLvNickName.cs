using System;
using Fungus;
using UnityEngine;

// Token: 0x0200034D RID: 845
[CommandInfo("YSPlayer", "获取道侣对自己的昵称", "获取道侣对自己的昵称赋值到NickName变量", 0)]
[AddComponentMenu("")]
public class CmdGetDaoLvNickName : Command
{
	// Token: 0x060018B8 RID: 6328 RVA: 0x000DD768 File Offset: 0x000DB968
	public override void OnEnter()
	{
		int npcid = NPCEx.NPCIDToNew(this.NPCID.Value);
		if (PlayerEx.IsDaoLv(npcid))
		{
			this.NickName.Value = PlayerEx.GetDaoLvNickName(npcid);
		}
		else
		{
			Debug.LogError(string.Format("获取道侣昵称出错，{0}不是玩家的道侣", this.NPCID));
		}
		this.Continue();
	}

	// Token: 0x040013B1 RID: 5041
	[Tooltip("NPC的ID")]
	[SerializeField]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected IntegerVariable NPCID;

	// Token: 0x040013B2 RID: 5042
	[Tooltip("结果")]
	[SerializeField]
	[VariableProperty(new Type[]
	{
		typeof(StringVariable)
	})]
	protected StringVariable NickName;
}
