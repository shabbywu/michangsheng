using System;
using Fungus;
using UnityEngine;
using YSGame.TuJian;

// Token: 0x0200036D RID: 877
[CommandInfo("YSTools", "触发图鉴超链接", "触发图鉴超链接", 0)]
[AddComponentMenu("")]
public class CmdTuJianHyperlink : Command
{
	// Token: 0x06001909 RID: 6409 RVA: 0x00015783 File Offset: 0x00013983
	public override void OnEnter()
	{
		TuJianManager.Inst.OnHyperlink(this.Hyperlink);
		this.Continue();
	}

	// Token: 0x040013F4 RID: 5108
	[Tooltip("超链接")]
	[SerializeField]
	protected string Hyperlink;
}
