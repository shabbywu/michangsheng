using Fungus;
using UnityEngine;

[CommandInfo("剑灵", "解锁线索", "解锁线索", 0)]
[AddComponentMenu("")]
public class CmdJianLingUnlockXianSuo : Command
{
	[SerializeField]
	protected string ID;

	public override void OnEnter()
	{
		PlayerEx.Player.jianLingManager.UnlockXianSuo(ID);
		Continue();
	}
}
