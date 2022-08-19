using System;
using Fungus;
using UnityEngine;

// Token: 0x0200023A RID: 570
[CommandInfo("YSNPCJiaoHu", "获取NPC名字", "获取NPC名字，文本会赋值到TmpStrValue", 0)]
[AddComponentMenu("")]
public class CmdGetNPCName : Command
{
	// Token: 0x0600161A RID: 5658 RVA: 0x00095AC4 File Offset: 0x00093CC4
	public override void OnEnter()
	{
		Flowchart flowchart = this.GetFlowchart();
		int num = flowchart.GetIntegerVariable(this.npcid);
		if (this.VarNPCID != null)
		{
			num = this.VarNPCID.Value;
		}
		try
		{
			if (jsonData.instance.AvatarRandomJsonData.HasField(num.ToString()))
			{
				JSONObject jsonobject = jsonData.instance.AvatarRandomJsonData[num.ToString()];
				flowchart.SetStringVariable("TmpStrValue", jsonobject["Name"].Str);
			}
			else
			{
				Debug.LogError(string.Format("获取NPC AvatarRandomJsonData异常，没有NPCID:{0}的数据", num));
			}
		}
		catch (Exception ex)
		{
			Debug.LogError(ex);
			Debug.LogError(string.Format("获取NPC名字异常，NPCID:{0}", num));
		}
		this.Continue();
	}

	// Token: 0x0400106E RID: 4206
	[SerializeField]
	protected string npcid;

	// Token: 0x0400106F RID: 4207
	[SerializeField]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected IntegerVariable VarNPCID;
}
