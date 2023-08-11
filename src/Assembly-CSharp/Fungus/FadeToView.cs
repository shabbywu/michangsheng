using UnityEngine;

namespace Fungus;

[CommandInfo("Camera", "Fade To View", "Fades the camera out and in again at a position specified by a View object.", 0)]
[AddComponentMenu("")]
public class FadeToView : Command
{
	[Tooltip("Time for fade effect to complete")]
	[SerializeField]
	protected float duration = 1f;

	[Tooltip("Fade from fully visible to opaque at start of fade")]
	[SerializeField]
	protected bool fadeOut = true;

	[Tooltip("View to transition to when Fade is complete")]
	[SerializeField]
	protected View targetView;

	[Tooltip("Wait until the fade has finished before executing next command")]
	[SerializeField]
	protected bool waitUntilFinished = true;

	[Tooltip("Color to render fullscreen fade texture with when screen is obscured.")]
	[SerializeField]
	protected Color fadeColor = Color.black;

	[Tooltip("Optional texture to use when rendering the fullscreen fade effect.")]
	[SerializeField]
	protected Texture2D fadeTexture;

	[Tooltip("Camera to use for the fade. Will use main camera if set to none.")]
	[SerializeField]
	protected Camera targetCamera;

	public virtual View TargetView => targetView;

	protected virtual void Start()
	{
		AcquireCamera();
	}

	protected virtual void AcquireCamera()
	{
		if (!((Object)(object)targetCamera != (Object)null))
		{
			targetCamera = Camera.main;
			if ((Object)(object)targetCamera == (Object)null)
			{
				targetCamera = Object.FindObjectOfType<Camera>();
			}
		}
	}

	public override void OnEnter()
	{
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		AcquireCamera();
		if ((Object)(object)targetCamera == (Object)null || (Object)(object)targetView == (Object)null)
		{
			Continue();
			return;
		}
		CameraManager cameraManager = FungusManager.Instance.CameraManager;
		if (Object.op_Implicit((Object)(object)fadeTexture))
		{
			cameraManager.ScreenFadeTexture = fadeTexture;
		}
		else
		{
			cameraManager.ScreenFadeTexture = CameraManager.CreateColorTexture(fadeColor, 32, 32);
		}
		cameraManager.FadeToView(targetCamera, targetView, duration, fadeOut, delegate
		{
			if (waitUntilFinished)
			{
				Continue();
			}
		});
		if (!waitUntilFinished)
		{
			Continue();
		}
	}

	public override void OnStopExecuting()
	{
		FungusManager.Instance.CameraManager.Stop();
	}

	public override string GetSummary()
	{
		if ((Object)(object)targetView == (Object)null)
		{
			return "Error: No view selected";
		}
		return ((Object)targetView).name;
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)216, (byte)228, (byte)170, byte.MaxValue));
	}
}
