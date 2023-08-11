using UnityEngine;

namespace Fungus;

[CommandInfo("Flow", "Stop", "Stop executing the Block that contains this command.", 0)]
[AddComponentMenu("")]
public class Stop : Command
{
	public override void OnEnter()
	{
		StopParentBlock();
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)235, (byte)191, (byte)217, byte.MaxValue));
	}
}
