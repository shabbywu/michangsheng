using System;
using Fungus;
using UnityEngine;

// Token: 0x02000372 RID: 882
[CommandInfo("YSNPCJiaoHu", "威胁当前索取的NPC", "威胁当前索取的NPC，必须在索取后使用", 0)]
[AddComponentMenu("")]
public class CmdWeiXieNPC : Command
{
	// Token: 0x06001914 RID: 6420 RVA: 0x000DF230 File Offset: 0x000DD430
	public override void OnEnter()
	{
		switch (this.WeiXieType)
		{
		case 0:
			NPCEx.FirstWeiXie();
			break;
		case 1:
			NPCEx.ShenShiWeiXie();
			break;
		case 2:
			NPCEx.GuShiWeiXie(this.FriendIndex);
			break;
		}
		this.Continue();
	}

	// Token: 0x040013FC RID: 5116
	[SerializeField]
	[Tooltip("威胁类型 0初次威胁 1神识迫压 2怙势凌人")]
	protected int WeiXieType;

	// Token: 0x040013FD RID: 5117
	[SerializeField]
	[Tooltip("怙势凌人时助阵好友索引 012")]
	protected int FriendIndex;
}
