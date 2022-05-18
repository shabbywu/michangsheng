using System;
using System.Collections.Generic;
using Fungus;
using UnityEngine;

// Token: 0x020002F1 RID: 753
[CommandInfo("YSPlayer", "记录生平", "记录生平", 0)]
[AddComponentMenu("")]
public class CmdRecordShengPing : Command
{
	// Token: 0x060016D4 RID: 5844 RVA: 0x000CB9CC File Offset: 0x000C9BCC
	public override void OnEnter()
	{
		if (this.Args != null && this.Args.Count > 0)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (ShengPingArg shengPingArg in this.Args)
			{
				if (!string.IsNullOrWhiteSpace(shengPingArg.ArgName) && shengPingArg.Var != null)
				{
					dictionary.Add(shengPingArg.ArgName, shengPingArg.Var.Value);
				}
				else
				{
					Debug.LogError("调用记录生平指令出错，有为空的参数。" + base.GetCommandSourceDesc());
				}
			}
			PlayerEx.RecordShengPing(this.ID, dictionary);
		}
		else
		{
			PlayerEx.RecordShengPing(this.ID, null);
		}
		this.Continue();
	}

	// Token: 0x04001237 RID: 4663
	[Tooltip("生平的ID值")]
	[SerializeField]
	protected string ID = "";

	// Token: 0x04001238 RID: 4664
	[Tooltip("参数列表")]
	[SerializeField]
	protected List<ShengPingArg> Args = new List<ShengPingArg>();
}
