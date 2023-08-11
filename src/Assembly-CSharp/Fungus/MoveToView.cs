using UnityEngine;

namespace Fungus;

[CommandInfo("Camera", "Move To View", "Moves the camera to a location specified by a View object.", 0)]
[AddComponentMenu("")]
public class MoveToView : Command
{
	[Tooltip("Time for move effect to complete")]
	[SerializeField]
	protected float duration = 1f;

	[Tooltip("View to transition to when move is complete")]
	[SerializeField]
	protected View targetView;

	[Tooltip("Wait until the fade has finished before executing next command")]
	[SerializeField]
	protected bool waitUntilFinished = true;

	[Tooltip("Camera to use for the pan. Will use main camera if set to none.")]
	[SerializeField]
	protected Camera targetCamera;

	public virtual View TargetView => targetView;

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

	public virtual void Start()
	{
		AcquireCamera();
	}

	public override void OnEnter()
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		AcquireCamera();
		if ((Object)(object)targetCamera == (Object)null || (Object)(object)targetView == (Object)null)
		{
			Continue();
			return;
		}
		FungusManager.Instance.CameraManager.PanToPosition(targetPosition: ((Component)targetView).transform.position, targetRotation: ((Component)targetView).transform.rotation, targetSize: targetView.ViewSize, camera: targetCamera, duration: duration, arriveAction: delegate
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
