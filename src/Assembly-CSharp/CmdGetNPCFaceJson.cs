using System;
using Fungus;
using UnityEngine;

// Token: 0x02000237 RID: 567
[CommandInfo("YSNPCJiaoHu", "获取NPC形象Json数据", "获取NPC形象Json数据，Int值会赋值到TmpValue，文本会赋值到TmpStrValue，布尔值会赋值到TmpBoolValue", 0)]
[AddComponentMenu("")]
public class CmdGetNPCFaceJson : Command
{
	// Token: 0x06001614 RID: 5652 RVA: 0x00095788 File Offset: 0x00093988
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

	// Token: 0x04001066 RID: 4198
	[SerializeField]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected IntegerVariable VarNPCID;

	// Token: 0x04001067 RID: 4199
	[SerializeField]
	protected string valueName;

	// Token: 0x04001068 RID: 4200
	[SerializeField]
	protected SetValueType valueType;
}
