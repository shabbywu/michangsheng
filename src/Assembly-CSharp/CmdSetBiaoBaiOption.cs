using Fungus;
using UnityEngine;

[CommandInfo("YSDongFu", "设置玩家表白的选择", "设置玩家表白的选择", 0)]
[AddComponentMenu("")]
public class CmdSetBiaoBaiOption : Command
{
	[Tooltip("题干类型 1正邪2性格3标签")]
	[SerializeField]
	protected int type;

	[Tooltip("选择 123")]
	[SerializeField]
	protected int choose;

	public override void OnEnter()
	{
		BiaoBaiManager.SetPlayerOptionResult(type, choose);
		Continue();
	}
}
