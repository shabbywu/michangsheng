using System;
using System.Reflection;
using KBEngine;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSNew/Get", "GetAvatarData", "通过制定字符串获取玩家数据", 0)]
[AddComponentMenu("")]
public class GetAvatarData : Command
{
	[Tooltip("变量的名称")]
	[SerializeField]
	protected string StaticValueID = "";

	[Tooltip("获取到的值存放位置")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable TempValue;

	public void setHasVariable(string name, int num, Flowchart flowchart)
	{
		if (flowchart.HasVariable(name))
		{
			flowchart.SetIntegerVariable(name, num);
		}
	}

	public override void OnEnter()
	{
		Avatar player = Tools.instance.getPlayer();
		FieldInfo[] fields = player.GetType().GetFields();
		for (int i = 0; i < fields.Length; i++)
		{
			switch (fields[i].FieldType.Name)
			{
			case "Int32":
			case "Int64":
			case "Int16":
			case "UInt32":
			case "UInt16":
			case "UInt64":
			case "Int":
			case "uInt":
				if (StaticValueID == fields[i].Name)
				{
					TempValue.Value = Convert.ToInt32(fields[i].GetValue(player));
				}
				break;
			}
		}
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
