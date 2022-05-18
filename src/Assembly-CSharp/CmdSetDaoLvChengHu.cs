using System;
using Fungus;
using UnityEngine;

// Token: 0x0200033D RID: 829
[CommandInfo("YSDongFu", "让玩家设置道侣对自己的称呼", "让玩家设置道侣对自己的称呼", 0)]
[AddComponentMenu("")]
public class CmdSetDaoLvChengHu : Command
{
	// Token: 0x0600186B RID: 6251 RVA: 0x0001534A File Offset: 0x0001354A
	public override void OnEnter()
	{
		this.OpenInputBox();
		this.Continue();
	}

	// Token: 0x0600186C RID: 6252 RVA: 0x00015358 File Offset: 0x00013558
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

	// Token: 0x04001393 RID: 5011
	[Tooltip("NPCID")]
	[SerializeField]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected IntegerVariable NPCID;
}
