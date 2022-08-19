using System;
using Fungus;
using UnityEngine;
using YSGame.TuJian;

// Token: 0x02000253 RID: 595
[CommandInfo("YSTools", "触发图鉴超链接", "触发图鉴超链接", 0)]
[AddComponentMenu("")]
public class CmdTuJianHyperlink : Command
{
	// Token: 0x06001653 RID: 5715 RVA: 0x00096DD1 File Offset: 0x00094FD1
	public override void OnEnter()
	{
		TuJianManager.Inst.OnHyperlink(this.Hyperlink);
		this.Continue();
	}

	// Token: 0x040010A2 RID: 4258
	[Tooltip("超链接")]
	[SerializeField]
	protected string Hyperlink;
}
