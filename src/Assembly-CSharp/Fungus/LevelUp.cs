using KBEngine;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSTools", "LevelUp", "直接提升一个等级，并把经验值设为0", 0)]
[AddComponentMenu("")]
public class LevelUp : Command
{
	[Tooltip("说明")]
	[SerializeField]
	protected string init = "直接提升一个等级，不用配置什么东西";

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
		player.exp = 0uL;
		player.levelUp();
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
