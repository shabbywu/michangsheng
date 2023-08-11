using System;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSNew/Get", "GetHaoGanDuVar", "获取好感度保存到一个变量中", 0)]
[AddComponentMenu("")]
public class GetHaoGanDuVar : Command
{
	[Tooltip("需要获取好感度的武将ID")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable AvatarID;

	[Tooltip("存放值的变量")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable Value;

	public void setHasVariable(string name, int num, Flowchart flowchart)
	{
		if (flowchart.HasVariable(name))
		{
			flowchart.SetIntegerVariable(name, num);
		}
	}

	public override void OnEnter()
	{
		int num = NPCEx.NPCIDToNew(AvatarID.Value);
		int value = (int)jsonData.instance.AvatarRandomJsonData[num.ToString()]["HaoGanDu"].n;
		Value.Value = value;
		Continue();
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)184, (byte)210, (byte)235, byte.MaxValue));
	}
}
