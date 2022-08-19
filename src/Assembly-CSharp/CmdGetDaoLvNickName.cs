using System;
using Fungus;
using UnityEngine;

// Token: 0x02000231 RID: 561
[CommandInfo("YSPlayer", "获取道侣对自己的昵称", "获取道侣对自己的昵称赋值到NickName变量", 0)]
[AddComponentMenu("")]
public class CmdGetDaoLvNickName : Command
{
	// Token: 0x06001600 RID: 5632 RVA: 0x000950E0 File Offset: 0x000932E0
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

	// Token: 0x04001059 RID: 4185
	[Tooltip("NPC的ID")]
	[SerializeField]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected IntegerVariable NPCID;

	// Token: 0x0400105A RID: 4186
	[Tooltip("结果")]
	[SerializeField]
	[VariableProperty(new Type[]
	{
		typeof(StringVariable)
	})]
	protected StringVariable NickName;
}
