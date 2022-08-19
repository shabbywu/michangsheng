using System;
using System.Collections.Generic;
using Fungus;
using UnityEngine;

// Token: 0x020001DE RID: 478
[CommandInfo("YSPlayer", "记录生平", "记录生平", 0)]
[AddComponentMenu("")]
public class CmdRecordShengPing : Command
{
	// Token: 0x0600142F RID: 5167 RVA: 0x00082834 File Offset: 0x00080A34
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

	// Token: 0x04000EF9 RID: 3833
	[Tooltip("生平的ID值")]
	[SerializeField]
	protected string ID = "";

	// Token: 0x04000EFA RID: 3834
	[Tooltip("参数列表")]
	[SerializeField]
	protected List<ShengPingArg> Args = new List<ShengPingArg>();
}
