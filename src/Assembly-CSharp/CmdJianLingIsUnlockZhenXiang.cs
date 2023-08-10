using Fungus;
using UnityEngine;

[CommandInfo("剑灵", "检查是否解锁真相", "检查是否解锁了真相(赋值到TmpBool)", 0)]
[AddComponentMenu("")]
public class CmdJianLingIsUnlockZhenXiang : Command
{
	[SerializeField]
	protected string ID;

	public override void OnEnter()
	{
		bool value = PlayerEx.Player.jianLingManager.IsZhenXiangUnlocked(ID);
		GetFlowchart().SetBooleanVariable("TmpBool", value);
		Continue();
	}
}
