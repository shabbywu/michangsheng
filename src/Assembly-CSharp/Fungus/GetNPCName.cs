using System;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSNew/Get", "GetNPCName", "获取NPC姓和名", 0)]
[AddComponentMenu("")]
public class GetNPCName : Command
{
	[Tooltip("NPC编号")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable NPCID;

	[Tooltip("姓名存放的位置")]
	[VariableProperty(new Type[] { typeof(StringVariable) })]
	[SerializeField]
	protected StringVariable Name;

	public void setHasVariable(string name, int num, Flowchart flowchart)
	{
		if (flowchart.HasVariable(name))
		{
			flowchart.SetIntegerVariable(name, num);
		}
	}

	public override void OnEnter()
	{
		Tools.instance.getPlayer();
		JSONObject wuJiangBangDing = Tools.instance.getWuJiangBangDing(NPCID.Value);
		string value = ((wuJiangBangDing == null) ? Tools.instance.Code64ToString(jsonData.instance.AvatarRandomJsonData[string.Concat(NPCID.Value)]["Name"].str) : Tools.Code64(wuJiangBangDing["Name"].str));
		Name.Value = value;
		Continue();
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)184, (byte)210, (byte)235, byte.MaxValue));
	}

	public override void OnReset()
	{
	}
}
