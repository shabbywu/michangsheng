using System;
using Fungus;
using UnityEngine;

// Token: 0x02000356 RID: 854
[CommandInfo("YSNPCJiaoHu", "获取NPC名字", "获取NPC名字，文本会赋值到TmpStrValue", 0)]
[AddComponentMenu("")]
public class CmdGetNPCName : Command
{
	// Token: 0x060018D2 RID: 6354 RVA: 0x000DE0DC File Offset: 0x000DC2DC
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

	// Token: 0x040013C6 RID: 5062
	[SerializeField]
	protected string npcid;

	// Token: 0x040013C7 RID: 5063
	[SerializeField]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected IntegerVariable VarNPCID;
}
