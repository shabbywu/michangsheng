using System;
using Fungus;
using UnityEngine;

// Token: 0x02000259 RID: 601
[CommandInfo("YSNPCJiaoHu", "威胁当前索取的NPC", "威胁当前索取的NPC，必须在索取后使用", 0)]
[AddComponentMenu("")]
public class CmdWeiXieNPC : Command
{
	// Token: 0x06001660 RID: 5728 RVA: 0x00096F08 File Offset: 0x00095108
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

	// Token: 0x040010AA RID: 4266
	[SerializeField]
	[Tooltip("威胁类型 0初次威胁 1神识迫压 2怙势凌人")]
	protected int WeiXieType;

	// Token: 0x040010AB RID: 4267
	[SerializeField]
	[Tooltip("怙势凌人时助阵好友索引 012")]
	protected int FriendIndex;
}
