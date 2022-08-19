using System;
using Fungus;
using UnityEngine;

// Token: 0x02000221 RID: 545
[CommandInfo("YSDongFu", "让玩家设置道侣对自己的称呼", "让玩家设置道侣对自己的称呼", 0)]
[AddComponentMenu("")]
public class CmdSetDaoLvChengHu : Command
{
	// Token: 0x060015B3 RID: 5555 RVA: 0x000913D9 File Offset: 0x0008F5D9
	public override void OnEnter()
	{
		this.OpenInputBox();
		this.Continue();
	}

	// Token: 0x060015B4 RID: 5556 RVA: 0x000913E7 File Offset: 0x0008F5E7
	public void OpenInputBox()
	{
		UInputBox.Show("设定称呼", delegate(string s)
		{
			if (string.IsNullOrWhiteSpace(s))
			{
				this.OpenInputBox();
				return;
			}
			if (s.Length > 6)
			{
				UIPopTip.Inst.Pop("称呼太长了", PopTipIconType.叹号);
				this.OpenInputBox();
				return;
			}
			PlayerEx.SetDaoLvChengHu(this.NPCID.Value, s);
		});
	}

	// Token: 0x0400103B RID: 4155
	[Tooltip("NPCID")]
	[SerializeField]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected IntegerVariable NPCID;
}
