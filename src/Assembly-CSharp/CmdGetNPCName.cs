using System;
using Fungus;
using UnityEngine;

[CommandInfo("YSNPCJiaoHu", "获取NPC名字", "获取NPC名字，文本会赋值到TmpStrValue", 0)]
[AddComponentMenu("")]
public class CmdGetNPCName : Command
{
	[SerializeField]
	protected string npcid;

	[SerializeField]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	protected IntegerVariable VarNPCID;

	public override void OnEnter()
	{
		Flowchart flowchart = GetFlowchart();
		int num = flowchart.GetIntegerVariable(npcid);
		if ((Object)(object)VarNPCID != (Object)null)
		{
			num = VarNPCID.Value;
		}
		try
		{
			if (jsonData.instance.AvatarRandomJsonData.HasField(num.ToString()))
			{
				JSONObject jSONObject = jsonData.instance.AvatarRandomJsonData[num.ToString()];
				flowchart.SetStringVariable("TmpStrValue", jSONObject["Name"].Str);
			}
			else
			{
				Debug.LogError((object)$"获取NPC AvatarRandomJsonData异常，没有NPCID:{num}的数据");
			}
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
			Debug.LogError((object)$"获取NPC名字异常，NPCID:{num}");
		}
		Continue();
	}
}
