using System.Collections;
using UnityEngine;

namespace Fungus;

[CommandInfo("Camera", "Shake Camera", "Applies a camera shake effect to the main camera.", 0)]
[AddComponentMenu("")]
public class ShakeCamera : Command
{
	[Tooltip("Time for camera shake effect to complete")]
	[SerializeField]
	protected float duration = 0.5f;

	[Tooltip("Magnitude of shake effect in x & y axes")]
	[SerializeField]
	protected Vector2 amount = new Vector2(1f, 1f);

	[Tooltip("Wait until the shake effect has finished before executing next command")]
	[SerializeField]
	protected bool waitUntilFinished;

	protected virtual void OniTweenComplete(object param)
	{
		Command command = param as Command;
		if ((Object)(object)command != (Object)null && ((object)command).Equals((object?)this) && waitUntilFinished)
		{
			Continue();
		}
	}

	public override void OnEnter()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		Vector3 val = default(Vector3);
		val = Vector2.op_Implicit(amount);
		Hashtable hashtable = new Hashtable();
		hashtable.Add("amount", val);
		hashtable.Add("time", duration);
		hashtable.Add("oncomplete", "OniTweenComplete");
		hashtable.Add("oncompletetarget", ((Component)this).gameObject);
		hashtable.Add("oncompleteparams", this);
		iTween.ShakePosition(((Component)Camera.main).gameObject, hashtable);
		if (!waitUntilFinished)
		{
			Continue();
		}
	}

	public override string GetSummary()
	{
		return "For " + duration + " seconds.";
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)216, (byte)228, (byte)170, byte.MaxValue));
	}
}
