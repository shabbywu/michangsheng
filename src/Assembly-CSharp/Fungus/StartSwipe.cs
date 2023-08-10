using UnityEngine;

namespace Fungus;

[CommandInfo("Camera", "Start Swipe", "Activates swipe panning mode where the player can pan the camera within the area between viewA & viewB.", 0)]
[AddComponentMenu("")]
public class StartSwipe : Command
{
	[Tooltip("Defines one extreme of the scrollable area that the player can pan around")]
	[SerializeField]
	protected View viewA;

	[Tooltip("Defines one extreme of the scrollable area that the player can pan around")]
	[SerializeField]
	protected View viewB;

	[Tooltip("Time to move the camera to a valid starting position between the two views")]
	[SerializeField]
	protected float duration = 0.5f;

	[Tooltip("Multiplier factor for speed of swipe pan")]
	[SerializeField]
	protected float speedMultiplier = 1f;

	[Tooltip("Camera to use for the pan. Will use main camera if set to none.")]
	[SerializeField]
	protected Camera targetCamera;

	public virtual void Start()
	{
		if ((Object)(object)targetCamera == (Object)null)
		{
			targetCamera = Camera.main;
		}
		if ((Object)(object)targetCamera == (Object)null)
		{
			targetCamera = Object.FindObjectOfType<Camera>();
		}
	}

	public override void OnEnter()
	{
		if ((Object)(object)targetCamera == (Object)null || (Object)(object)viewA == (Object)null || (Object)(object)viewB == (Object)null)
		{
			Continue();
			return;
		}
		FungusManager.Instance.CameraManager.StartSwipePan(targetCamera, viewA, viewB, duration, speedMultiplier, delegate
		{
			Continue();
		});
	}

	public override string GetSummary()
	{
		if ((Object)(object)viewA == (Object)null)
		{
			return "Error: No view selected for View A";
		}
		if ((Object)(object)viewB == (Object)null)
		{
			return "Error: No view selected for View B";
		}
		return ((Object)viewA).name + " to " + ((Object)viewB).name;
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)216, (byte)228, (byte)170, byte.MaxValue));
	}
}
