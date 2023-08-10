using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus;

[CommandInfo("Flow", "Wait", "Waits for period of time before executing the next command in the block.", 0)]
[AddComponentMenu("")]
[ExecuteInEditMode]
public class Wait : Command
{
	[Tooltip("Duration to wait for")]
	[SerializeField]
	protected FloatData _duration = new FloatData(1f);

	[HideInInspector]
	[FormerlySerializedAs("duration")]
	public float durationOLD;

	protected virtual void OnWaitComplete()
	{
		Tools.canClickFlag = true;
		Continue();
	}

	public override void OnEnter()
	{
		Tools.canClickFlag = false;
		((MonoBehaviour)this).Invoke("OnWaitComplete", _duration.Value);
	}

	public override string GetSummary()
	{
		return _duration.Value + " seconds";
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)235, (byte)191, (byte)217, byte.MaxValue));
	}

	public override bool HasReference(Variable variable)
	{
		if (!((Object)(object)_duration.floatRef == (Object)(object)variable))
		{
			return base.HasReference(variable);
		}
		return true;
	}

	protected virtual void OnEnable()
	{
		if (durationOLD != 0f)
		{
			_duration.Value = durationOLD;
			durationOLD = 0f;
		}
	}
}
