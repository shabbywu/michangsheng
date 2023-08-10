using System;
using UnityEngine;

namespace Fungus;

[CommandInfo("YS", "ItemCheckVar", "检测是否拥有某个物品", 0)]
[AddComponentMenu("")]
public class ItemCheckVar : Command
{
	public enum CompareNum
	{
		GreaterThan,
		LessThan,
		equalTo
	}

	[Tooltip("需要检测物品ID")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable ItemID;

	[Tooltip("比较类型，大于 小于 等于")]
	[SerializeField]
	protected CompareNum CompareType;

	[Tooltip("数量")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable ItemNum;

	[Tooltip("将检测到的值赋给一个变量")]
	[VariableProperty(new Type[] { typeof(BooleanVariable) })]
	[SerializeField]
	protected BooleanVariable TempBool;

	public void setHasVariable(string name, int num, Flowchart flowchart)
	{
		if (flowchart.HasVariable(name))
		{
			flowchart.SetIntegerVariable(name, num);
		}
	}

	public override void OnEnter()
	{
		int itemNum = Tools.instance.getPlayer().getItemNum(ItemID.Value);
		bool value = false;
		if (CompareType == CompareNum.GreaterThan)
		{
			if (itemNum > ItemNum.Value)
			{
				value = true;
			}
		}
		else if (CompareType == CompareNum.LessThan)
		{
			if (itemNum < ItemNum.Value)
			{
				value = true;
			}
		}
		else if (CompareType == CompareNum.equalTo && itemNum == ItemNum.Value)
		{
			value = true;
		}
		TempBool.Value = value;
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
