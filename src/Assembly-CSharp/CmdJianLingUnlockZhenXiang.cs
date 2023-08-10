using Fungus;
using UnityEngine;

[CommandInfo("剑灵", "解锁真相", "解锁真相", 0)]
[AddComponentMenu("")]
public class CmdJianLingUnlockZhenXiang : Command
{
	[SerializeField]
	protected string ID;

	public override void OnEnter()
	{
		PlayerEx.Player.jianLingManager.UnlockZhenXiang(ID);
		Continue();
	}
}
