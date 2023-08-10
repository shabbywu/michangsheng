using System;
using Fungus;
using UnityEngine;

[CommandInfo("YSNPCJiaoHu", "获取NPC形象Json数据", "获取NPC形象Json数据，Int值会赋值到TmpValue，文本会赋值到TmpStrValue，布尔值会赋值到TmpBoolValue", 0)]
[AddComponentMenu("")]
public class CmdGetNPCFaceJson : Command
{
	[SerializeField]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	protected IntegerVariable VarNPCID;

	[SerializeField]
	protected string valueName;

	[SerializeField]
	protected SetValueType valueType;

	public override void OnEnter()
	{
		Flowchart flowchart = GetFlowchart();
		int value = VarNPCID.Value;
		try
		{
			if (jsonData.instance.AvatarRandomJsonData.HasField(value.ToString()))
			{
				JSONObject jSONObject = jsonData.instance.AvatarRandomJsonData[value.ToString()][valueName];
				switch (valueType)
				{
				case SetValueType.Int:
					flowchart.SetIntegerVariable("TmpValue", jSONObject.I);
					break;
				case SetValueType.String:
					flowchart.SetStringVariable("TmpStrValue", jSONObject.Str);
					break;
				case SetValueType.Bool:
					flowchart.SetBooleanVariable("TmpBoolValue", jSONObject.b);
					break;
				}
			}
			else
			{
				Debug.LogError((object)$"获取AvatarRandomJsonData异常，没有NPCID:{value}的数据");
			}
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
			Debug.LogError((object)$"获取AvatarRandomJsonData异常，NPCID:{value}，要获取的变量:{valueName}({valueType})，JSON数据:{jsonData.instance.AvatarRandomJsonData[value.ToString()].ToString()}");
		}
		Continue();
	}
}
