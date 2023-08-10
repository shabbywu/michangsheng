using UnityEngine;

namespace Fungus;

[CommandInfo("Audio", "Stop Ambiance", "Stops the currently playing game ambiance.", 0)]
[AddComponentMenu("")]
public class StopAmbiance : Command
{
	public override void OnEnter()
	{
		FungusManager.Instance.MusicManager.StopAmbiance();
		Continue();
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)242, (byte)209, (byte)176, byte.MaxValue));
	}
}
