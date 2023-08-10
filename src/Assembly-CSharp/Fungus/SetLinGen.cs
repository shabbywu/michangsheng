using System;
using KBEngine;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSNew/Set", "SetLinGen", "设置后续对话", 0)]
[AddComponentMenu("")]
public class SetLinGen : Command
{
	[Tooltip("灵根类型")]
	[SerializeField]
	protected int TaskID;

	[Tooltip("灵根值")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable TaskIndex;

	public override void OnEnter()
	{
		Avatar player = Tools.instance.getPlayer();
		player.taskMag.setTaskIndex(TaskID, TaskIndex.Value);
		player.LingGeng[TaskID] = TaskIndex.Value;
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
