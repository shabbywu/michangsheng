using System;
using Fungus;
using UnityEngine;

// Token: 0x020004F7 RID: 1271
[CommandInfo("YSSea", "检查是否已经领过海域探索奖励", "检查是否已经领过海域探索奖励(赋值到TmpBool)", 0)]
[AddComponentMenu("")]
public class CmdCheckHasHaiDaoJiangLi : Command
{
	// Token: 0x06002105 RID: 8453 RVA: 0x0001B394 File Offset: 0x00019594
	public override void OnEnter()
	{
		this.GetFlowchart().SetBooleanVariable("TmpBool", PlayerEx.Player.HideHaiYuTanSuo.HasItem(this.seaID));
		this.Continue();
	}

	// Token: 0x04001C7B RID: 7291
	[Tooltip("海域ID")]
	[SerializeField]
	protected int seaID;
}
