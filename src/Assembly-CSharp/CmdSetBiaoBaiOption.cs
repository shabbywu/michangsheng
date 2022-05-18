using System;
using Fungus;
using UnityEngine;

// Token: 0x0200033C RID: 828
[CommandInfo("YSDongFu", "设置玩家表白的选择", "设置玩家表白的选择", 0)]
[AddComponentMenu("")]
public class CmdSetBiaoBaiOption : Command
{
	// Token: 0x06001869 RID: 6249 RVA: 0x00015331 File Offset: 0x00013531
	public override void OnEnter()
	{
		BiaoBaiManager.SetPlayerOptionResult(this.type, this.choose);
		this.Continue();
	}

	// Token: 0x04001391 RID: 5009
	[Tooltip("题干类型 1正邪2性格3标签")]
	[SerializeField]
	protected int type;

	// Token: 0x04001392 RID: 5010
	[Tooltip("选择 123")]
	[SerializeField]
	protected int choose;
}
