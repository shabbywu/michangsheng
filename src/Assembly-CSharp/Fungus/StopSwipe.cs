using UnityEngine;

namespace Fungus;

[CommandInfo("Camera", "Stop Swipe", "Deactivates swipe panning mode.", 0)]
[AddComponentMenu("")]
public class StopSwipe : Command
{
	public override void OnEnter()
	{
		FungusManager.Instance.CameraManager.StopSwipePan();
		Continue();
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)216, (byte)228, (byte)170, byte.MaxValue));
	}
}
