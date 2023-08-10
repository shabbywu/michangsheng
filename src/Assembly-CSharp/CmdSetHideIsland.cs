using Fungus;
using UnityEngine;

[CommandInfo("YSSea", "隐藏海域探索奖励", "隐藏海域探索奖励", 0)]
[AddComponentMenu("")]
public class CmdSetHideIsland : Command
{
	[Tooltip("海域ID")]
	[SerializeField]
	protected int seaID;

	public override void OnEnter()
	{
		if (!PlayerEx.Player.HideHaiYuTanSuo.HasItem(seaID))
		{
			PlayerEx.Player.HideHaiYuTanSuo.Add(seaID);
		}
		MessageMag.Instance.Send(MessageName.MSG_Sea_TanSuoDu_Refresh, new MessageData(value: false));
		UISeaTanSuoPanel.Inst.RefreshUI();
		Continue();
	}
}
