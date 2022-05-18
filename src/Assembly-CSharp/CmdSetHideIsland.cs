using System;
using Fungus;
using UnityEngine;

// Token: 0x020004F8 RID: 1272
[CommandInfo("YSSea", "隐藏海域探索奖励", "隐藏海域探索奖励", 0)]
[AddComponentMenu("")]
public class CmdSetHideIsland : Command
{
	// Token: 0x06002107 RID: 8455 RVA: 0x001153BC File Offset: 0x001135BC
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

	// Token: 0x04001C7C RID: 7292
	[Tooltip("海域ID")]
	[SerializeField]
	protected int seaID;
}
