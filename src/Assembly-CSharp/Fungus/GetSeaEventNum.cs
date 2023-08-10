using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSNew/Get", "GetSeaEventNum", "获取周围海域事件数量", 0)]
[AddComponentMenu("")]
public class GetSeaEventNum : Command
{
	[Tooltip("半径")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable radius;

	[Tooltip("事件类型")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable EventType;

	[Tooltip("返回的数量值")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable EventNum;

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
		List<SeaAvatarObjBase> aroundEventList = EndlessSeaMag.Inst.GetAroundEventList(radius.Value, EventType.Value);
		EventNum.Value = aroundEventList.Count;
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
