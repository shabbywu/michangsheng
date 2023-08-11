using Fungus;
using UnityEngine;

[CommandInfo("YSNPCJiaoHu", "威胁当前索取的NPC", "威胁当前索取的NPC，必须在索取后使用", 0)]
[AddComponentMenu("")]
public class CmdWeiXieNPC : Command
{
	[SerializeField]
	[Tooltip("威胁类型 0初次威胁 1神识迫压 2怙势凌人")]
	protected int WeiXieType;

	[SerializeField]
	[Tooltip("怙势凌人时助阵好友索引 012")]
	protected int FriendIndex;

	public override void OnEnter()
	{
		switch (WeiXieType)
		{
		case 0:
			NPCEx.FirstWeiXie();
			break;
		case 1:
			NPCEx.ShenShiWeiXie();
			break;
		case 2:
			NPCEx.GuShiWeiXie(FriendIndex);
			break;
		}
		Continue();
	}
}
