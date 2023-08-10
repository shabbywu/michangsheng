using UnityEngine;

namespace Fungus;

[CommandInfo("Camera", "Fade Screen", "Draws a fullscreen texture over the scene to give a fade effect. Setting Target Alpha to 1 will obscure the screen, alpha 0 will reveal the screen. If no Fade Texture is provided then a default flat color texture is used.", 0)]
[AddComponentMenu("")]
public class FadeScreen : Command
{
	[Tooltip("Time for fade effect to complete")]
	[SerializeField]
	protected float duration = 1f;

	[Tooltip("Current target alpha transparency value. The fade gradually adjusts the alpha to approach this target value.")]
	[SerializeField]
	protected float targetAlpha = 1f;

	[Tooltip("Wait until the fade has finished before executing next command")]
	[SerializeField]
	protected bool waitUntilFinished = true;

	[Tooltip("Color to render fullscreen fade texture with when screen is obscured.")]
	[SerializeField]
	protected Color fadeColor = Color.black;

	[Tooltip("Optional texture to use when rendering the fullscreen fade effect.")]
	[SerializeField]
	protected Texture2D fadeTexture;

	public override void OnEnter()
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		CameraManager cameraManager = FungusManager.Instance.CameraManager;
		if (Object.op_Implicit((Object)(object)fadeTexture))
		{
			cameraManager.ScreenFadeTexture = fadeTexture;
		}
		else
		{
			cameraManager.ScreenFadeTexture = CameraManager.CreateColorTexture(fadeColor, 32, 32);
		}
		Tools.canClickFlag = false;
		cameraManager.Fade(targetAlpha, duration, delegate
		{
			if (waitUntilFinished)
			{
				Tools.canClickFlag = true;
				Continue();
			}
		});
		if (!waitUntilFinished)
		{
			Continue();
		}
	}

	public override string GetSummary()
	{
		return "Fade to " + targetAlpha + " over " + duration + " seconds";
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)216, (byte)228, (byte)170, byte.MaxValue));
	}
}
