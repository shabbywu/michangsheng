using System;
using Fungus;
using UnityEngine;

[CommandInfo("YSDongFu", "让玩家设置道侣对自己的称呼", "让玩家设置道侣对自己的称呼", 0)]
[AddComponentMenu("")]
public class CmdSetDaoLvChengHu : Command
{
	[Tooltip("NPCID")]
	[SerializeField]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	protected IntegerVariable NPCID;

	public override void OnEnter()
	{
		OpenInputBox();
		Continue();
	}

	public void OpenInputBox()
	{
		UInputBox.Show("设定称呼", delegate(string s)
		{
			if (string.IsNullOrWhiteSpace(s))
			{
				OpenInputBox();
			}
			else if (s.Length > 6)
			{
				UIPopTip.Inst.Pop("称呼太长了");
				OpenInputBox();
			}
			else
			{
				PlayerEx.SetDaoLvChengHu(NPCID.Value, s);
			}
		});
	}
}
