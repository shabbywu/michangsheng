using System;
using UnityEngine;

namespace Fungus;

[CommandInfo("YS", "ChoiceNum", "玩家选择一个数量，并将玩家选择的数量返回到一个变量中", 0)]
[AddComponentMenu("")]
public class ChoiceNum : Command
{
	[Tooltip("弹框描述")]
	[SerializeField]
	protected string desc = "选择数量";

	[Tooltip("玩家最终选择的个数")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable FinalSelectNum;

	public override void OnEnter()
	{
		Tools.instance.getPlayer();
		selectNum.instence.setChoice(new EventDelegate(delegate
		{
			int value = int.Parse(((Component)selectNum.instence).gameObject.GetComponent<UI_chaifen>().inputNum.value);
			FinalSelectNum.Value = value;
			Continue();
		}), new EventDelegate(delegate
		{
			FinalSelectNum.Value = 0;
			Continue();
		}), desc);
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
