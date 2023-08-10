using UnityEngine;

namespace Fungus;

[CommandInfo("Camera", "Fullscreen", "Sets the application to fullscreen, windowed or toggles the current state.", 0)]
[AddComponentMenu("")]
public class Fullscreen : Command
{
	[SerializeField]
	protected FullscreenMode fullscreenMode;

	public override void OnEnter()
	{
		switch (fullscreenMode)
		{
		case FullscreenMode.Toggle:
			Screen.fullScreen = !Screen.fullScreen;
			break;
		case FullscreenMode.Fullscreen:
			Screen.fullScreen = true;
			break;
		case FullscreenMode.Windowed:
			Screen.fullScreen = false;
			break;
		}
		Continue();
	}

	public override string GetSummary()
	{
		return fullscreenMode.ToString();
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)216, (byte)228, (byte)170, byte.MaxValue));
	}
}
