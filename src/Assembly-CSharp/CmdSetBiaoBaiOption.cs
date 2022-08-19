using System;
using Fungus;
using UnityEngine;

// Token: 0x02000220 RID: 544
[CommandInfo("YSDongFu", "设置玩家表白的选择", "设置玩家表白的选择", 0)]
[AddComponentMenu("")]
public class CmdSetBiaoBaiOption : Command
{
	// Token: 0x060015B1 RID: 5553 RVA: 0x000913C0 File Offset: 0x0008F5C0
	public override void OnEnter()
	{
		BiaoBaiManager.SetPlayerOptionResult(this.type, this.choose);
		this.Continue();
	}

	// Token: 0x04001039 RID: 4153
	[Tooltip("题干类型 1正邪2性格3标签")]
	[SerializeField]
	protected int type;

	// Token: 0x0400103A RID: 4154
	[Tooltip("选择 123")]
	[SerializeField]
	protected int choose;
}
