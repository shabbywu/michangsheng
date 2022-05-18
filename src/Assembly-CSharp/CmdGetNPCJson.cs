using System;
using Fungus;
using UnityEngine;

// Token: 0x02000355 RID: 853
[CommandInfo("YSNPCJiaoHu", "获取NPCJson数据", "获取NPCJson数据，Int值会赋值到TmpValue，文本会赋值到TmpStrValue，布尔值会赋值到TmpBoolValue", 0)]
[AddComponentMenu("")]
public class CmdGetNPCJson : Command
{
	// Token: 0x060018D0 RID: 6352 RVA: 0x000DDF88 File Offset: 0x000DC188
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
			if (jsonData.instance.AvatarJsonData.HasField(num.ToString()))
			{
				JSONObject jsonobject = jsonData.instance.AvatarJsonData[num.ToString()][this.valueName];
				switch (this.valueType)
				{
				case SetValueType.Int:
					flowchart.SetIntegerVariable("TmpValue", jsonobject.I);
					break;
				case SetValueType.Bool:
					flowchart.SetBooleanVariable("TmpBoolValue", jsonobject.b);
					break;
				case SetValueType.String:
					flowchart.SetStringVariable("TmpStrValue", jsonobject.Str);
					break;
				}
			}
			else
			{
				Debug.LogError(string.Format("获取NPCJson异常，没有NPCID:{0}的数据", num));
			}
		}
		catch (Exception ex)
		{
			Debug.LogError(ex);
			Debug.LogError(string.Format("获取NPCJson异常，NPCID:{0}，要获取的变量:{1}({2})，JSON数据:{3}", new object[]
			{
				num,
				this.valueName,
				this.valueType,
				jsonData.instance.AvatarJsonData[num.ToString()].ToString()
			}));
		}
		this.Continue();
	}

	// Token: 0x040013C2 RID: 5058
	[SerializeField]
	protected string npcid;

	// Token: 0x040013C3 RID: 5059
	[SerializeField]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected IntegerVariable VarNPCID;

	// Token: 0x040013C4 RID: 5060
	[SerializeField]
	protected string valueName;

	// Token: 0x040013C5 RID: 5061
	[SerializeField]
	protected SetValueType valueType;
}
