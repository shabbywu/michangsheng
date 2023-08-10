using Fungus;
using UnityEngine;

[CommandInfo("YSSea", "检查是否已经领过海域探索奖励", "检查是否已经领过海域探索奖励(赋值到TmpBool)", 0)]
[AddComponentMenu("")]
public class CmdCheckHasHaiDaoJiangLi : Command
{
	[Tooltip("海域ID")]
	[SerializeField]
	protected int seaID;

	public override void OnEnter()
	{
		GetFlowchart().SetBooleanVariable("TmpBool", PlayerEx.Player.HideHaiYuTanSuo.HasItem(seaID));
		Continue();
	}
}
