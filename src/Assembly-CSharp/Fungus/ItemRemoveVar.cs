using System;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSTools", "ItemRemoveVar", "移除物品", 0)]
[AddComponentMenu("")]
public class ItemRemoveVar : Command
{
	[Tooltip("需要移除的物品ID")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable ItemID;

	[Tooltip("需要移除的物品数量")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable ItemRemoveNum;

	public void setHasVariable(string name, int num, Flowchart flowchart)
	{
		if (flowchart.HasVariable(name))
		{
			flowchart.SetIntegerVariable(name, num);
		}
	}

	public override void OnEnter()
	{
		if (ItemID.Value == 1 || ItemID.Value == 117 || ItemID.Value == 218 || ItemID.Value == 304)
		{
			Tools.instance.RemoveTieJian(ItemID.Value);
		}
		else
		{
			Tools.instance.RemoveItem(ItemID.Value, ItemRemoveNum.Value);
		}
		Continue();
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)184, (byte)210, (byte)235, byte.MaxValue));
	}
}
