using System;
using Fungus;
using UnityEngine;

// Token: 0x02000379 RID: 889
[CommandInfo("YSSea", "隐藏海域探索奖励", "隐藏海域探索奖励", 0)]
[AddComponentMenu("")]
public class CmdSetHideIsland : Command
{
	// Token: 0x06001D9E RID: 7582 RVA: 0x000D1498 File Offset: 0x000CF698
	public override void OnEnter()
	{
		if (!PlayerEx.Player.HideHaiYuTanSuo.HasItem(this.seaID))
		{
			PlayerEx.Player.HideHaiYuTanSuo.Add(this.seaID);
		}
		MessageMag.Instance.Send(MessageName.MSG_Sea_TanSuoDu_Refresh, new MessageData(false));
		UISeaTanSuoPanel.Inst.RefreshUI();
		this.Continue();
	}

	// Token: 0x0400182C RID: 6188
	[Tooltip("海域ID")]
	[SerializeField]
	protected int seaID;
}
