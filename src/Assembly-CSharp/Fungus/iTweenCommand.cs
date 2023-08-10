using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus;

[ExecuteInEditMode]
public abstract class iTweenCommand : Command
{
	[Tooltip("Target game object to apply the Tween to")]
	[SerializeField]
	protected GameObjectData _targetObject;

	[Tooltip("An individual name useful for stopping iTweens by name")]
	[SerializeField]
	protected StringData _tweenName;

	[Tooltip("The time in seconds the animation will take to complete")]
	[SerializeField]
	protected FloatData _duration = new FloatData(1f);

	[Tooltip("The shape of the easing curve applied to the animation")]
	[SerializeField]
	protected iTween.EaseType easeType = iTween.EaseType.easeInOutQuad;

	[Tooltip("The type of loop to apply once the animation has completed")]
	[SerializeField]
	protected iTween.LoopType loopType;

	[Tooltip("Stop any previously added iTweens on this object before adding this iTween")]
	[SerializeField]
	protected bool stopPreviousTweens;

	[Tooltip("Wait until the tween has finished before executing the next command")]
	[SerializeField]
	protected bool waitUntilFinished = true;

	[HideInInspector]
	[FormerlySerializedAs("target")]
	[FormerlySerializedAs("targetObject")]
	public GameObject targetObjectOLD;

	[HideInInspector]
	[FormerlySerializedAs("tweenName")]
	public string tweenNameOLD = "";

	[HideInInspector]
	[FormerlySerializedAs("duration")]
	public float durationOLD;

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
		if ((Object)(object)_targetObject.Value == (Object)null)
		{
			Continue();
			return;
		}
		if (stopPreviousTweens)
		{
			iTween[] components = _targetObject.Value.GetComponents<iTween>();
			foreach (iTween obj in components)
			{
				obj.time = 0f;
				((Component)obj).SendMessage("Update");
			}
		}
		DoTween();
		if (!waitUntilFinished)
		{
			Continue();
		}
	}

	public virtual void DoTween()
	{
	}

	public override string GetSummary()
	{
		if ((Object)(object)_targetObject.Value == (Object)null)
		{
			return "Error: No target object selected";
		}
		return ((Object)_targetObject.Value).name + " over " + _duration.Value + " seconds";
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)233, (byte)163, (byte)180, byte.MaxValue));
	}

	public override bool HasReference(Variable variable)
	{
		if (!((Object)(object)_targetObject.gameObjectRef == (Object)(object)variable) && !((Object)(object)_tweenName.stringRef == (Object)(object)variable))
		{
			return base.HasReference(variable);
		}
		return true;
	}

	protected virtual void OnEnable()
	{
		if ((Object)(object)targetObjectOLD != (Object)null)
		{
			_targetObject.Value = targetObjectOLD;
			targetObjectOLD = null;
		}
		if (tweenNameOLD != "")
		{
			_tweenName.Value = tweenNameOLD;
			tweenNameOLD = "";
		}
		if (durationOLD != 0f)
		{
			_duration.Value = durationOLD;
			durationOLD = 0f;
		}
	}
}
