using UnityEngine;

namespace Fungus;

[CommandInfo("YSNew/Get", "GetHaoGanDu", "获取好感度保存到TempValue中", 0)]
[AddComponentMenu("")]
public class GetHaoGanDu : Command
{
	[Tooltip("需要获取好感度的武将ID")]
	[SerializeField]
	protected int AvatarID = 1;

	public void setHasVariable(string name, int num, Flowchart flowchart)
	{
		if (flowchart.HasVariable(name))
		{
			flowchart.SetIntegerVariable(name, num);
		}
	}

	public override void OnEnter()
	{
		int favor = NPCEx.GetFavor(AvatarID);
		Flowchart flowchart = GetFlowchart();
		setHasVariable("TempValue", favor, flowchart);
		Continue();
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)184, (byte)210, (byte)235, byte.MaxValue));
	}
}
