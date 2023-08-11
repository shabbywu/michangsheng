using System;
using KBEngine;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSNew/Get", "GetMenPaiHaoGanDu", "获取门派好感度存放到TempValue中", 0)]
[AddComponentMenu("")]
public class GetMenPaiHaoGanDu : Command
{
	[Tooltip("要获取的门派的ID")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable MenPaiID;

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
		Flowchart flowchart = GetFlowchart();
		int value = (player.MenPaiHaoGanDu.HasField(string.Concat(MenPaiID)) ? ((int)player.MenPaiHaoGanDu[string.Concat(MenPaiID)].n) : 0);
		flowchart.SetIntegerVariable("TempValue", value);
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
