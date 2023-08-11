using Fungus;
using UnityEngine;

[CommandInfo("剑灵", "检查是否解锁线索", "检查是否解锁了线索(赋值到TmpBool)", 0)]
[AddComponentMenu("")]
public class CmdJianLingIsUnlockXianSuo : Command
{
	[SerializeField]
	protected string ID;

	public override void OnEnter()
	{
		bool value = PlayerEx.Player.jianLingManager.IsXianSuoUnlocked(ID);
		GetFlowchart().SetBooleanVariable("TmpBool", value);
		Continue();
	}
}
