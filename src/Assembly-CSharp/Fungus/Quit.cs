using UnityEngine;

namespace Fungus;

[CommandInfo("Flow", "Quit", "Quits the application. Does not work in Editor or Webplayer builds. Shouldn't generally be used on iOS.", 0)]
[AddComponentMenu("")]
public class Quit : Command
{
	public override void OnEnter()
	{
		Application.Quit();
		Continue();
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)235, (byte)191, (byte)217, byte.MaxValue));
	}
}
