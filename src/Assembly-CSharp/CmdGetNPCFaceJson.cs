using System;
using Fungus;
using UnityEngine;

// Token: 0x02000353 RID: 851
[CommandInfo("YSNPCJiaoHu", "获取NPC形象Json数据", "获取NPC形象Json数据，Int值会赋值到TmpValue，文本会赋值到TmpStrValue，布尔值会赋值到TmpBoolValue", 0)]
[AddComponentMenu("")]
public class CmdGetNPCFaceJson : Command
{
	// Token: 0x060018CC RID: 6348 RVA: 0x000DDDA0 File Offset: 0x000DBFA0
	public override void OnEnter()
	{
		Flowchart flowchart = this.GetFlowchart();
		int value = this.VarNPCID.Value;
		try
		{
			if (jsonData.instance.AvatarRandomJsonData.HasField(value.ToString()))
			{
				JSONObject jsonobject = jsonData.instance.AvatarRandomJsonData[value.ToString()][this.valueName];
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
				Debug.LogError(string.Format("获取AvatarRandomJsonData异常，没有NPCID:{0}的数据", value));
			}
		}
		catch (Exception ex)
		{
			Debug.LogError(ex);
			Debug.LogError(string.Format("获取AvatarRandomJsonData异常，NPCID:{0}，要获取的变量:{1}({2})，JSON数据:{3}", new object[]
			{
				value,
				this.valueName,
				this.valueType,
				jsonData.instance.AvatarRandomJsonData[value.ToString()].ToString()
			}));
		}
		this.Continue();
	}

	// Token: 0x040013BE RID: 5054
	[SerializeField]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected IntegerVariable VarNPCID;

	// Token: 0x040013BF RID: 5055
	[SerializeField]
	protected string valueName;

	// Token: 0x040013C0 RID: 5056
	[SerializeField]
	protected SetValueType valueType;
}
