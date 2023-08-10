using System.Collections.Generic;
using Fungus;
using UnityEngine;

[CommandInfo("YSPlayer", "记录生平", "记录生平", 0)]
[AddComponentMenu("")]
public class CmdRecordShengPing : Command
{
	[Tooltip("生平的ID值")]
	[SerializeField]
	protected string ID = "";

	[Tooltip("参数列表")]
	[SerializeField]
	protected List<ShengPingArg> Args = new List<ShengPingArg>();

	public override void OnEnter()
	{
		if (Args != null && Args.Count > 0)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (ShengPingArg arg in Args)
			{
				if (!string.IsNullOrWhiteSpace(arg.ArgName) && (Object)(object)arg.Var != (Object)null)
				{
					dictionary.Add(arg.ArgName, arg.Var.Value);
				}
				else
				{
					Debug.LogError((object)("调用记录生平指令出错，有为空的参数。" + GetCommandSourceDesc()));
				}
			}
			PlayerEx.RecordShengPing(ID, dictionary);
		}
		else
		{
			PlayerEx.RecordShengPing(ID);
		}
		Continue();
	}
}
