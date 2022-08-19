using System;
using Fungus;
using UnityEngine;

// Token: 0x02000378 RID: 888
[CommandInfo("YSSea", "检查是否已经领过海域探索奖励", "检查是否已经领过海域探索奖励(赋值到TmpBool)", 0)]
[AddComponentMenu("")]
public class CmdCheckHasHaiDaoJiangLi : Command
{
	// Token: 0x06001D9C RID: 7580 RVA: 0x000D1469 File Offset: 0x000CF669
	public override void OnEnter()
	{
		this.GetFlowchart().SetBooleanVariable("TmpBool", PlayerEx.Player.HideHaiYuTanSuo.HasItem(this.seaID));
		this.Continue();
	}

	// Token: 0x0400182B RID: 6187
	[Tooltip("海域ID")]
	[SerializeField]
	protected int seaID;
}
