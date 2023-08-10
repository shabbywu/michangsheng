using UnityEngine;

namespace Fungus;

[ExecuteInEditMode]
public abstract class BaseLeanTweenCommand : Command
{
	public enum ToFrom
	{
		To,
		From
	}

	public enum AbsAdd
	{
		Absolute,
		Additive
	}

	[Tooltip("Target game object to apply the Tween to")]
	[SerializeField]
	protected GameObjectData _targetObject;

	[Tooltip("The time in seconds the animation will take to complete")]
	[SerializeField]
	protected FloatData _duration = new FloatData(1f);

	[Tooltip("Does the tween act from current TO destination or is it reversed and act FROM destination to its current")]
	[SerializeField]
	protected ToFrom _toFrom;

	[Tooltip("Does the tween use the value as a target or as a delta to be added to current.")]
	[SerializeField]
	protected AbsAdd _absAdd;

	[Tooltip("The shape of the easing curve applied to the animation")]
	[SerializeField]
	protected LeanTweenType easeType = LeanTweenType.easeInOutQuad;

	[Tooltip("The type of loop to apply once the animation has completed")]
	[SerializeField]
	protected LeanTweenType loopType = LeanTweenType.once;

	[Tooltip("Number of times to repeat the tween, -1 is infinite.")]
	[SerializeField]
	protected int repeats;

	[Tooltip("Stop any previously LeanTweens on this object before adding this one. Warning; expensive.")]
	[SerializeField]
	protected bool stopPreviousTweens;

	[Tooltip("Wait until the tween has finished before executing the next command")]
	[SerializeField]
	protected bool waitUntilFinished = true;

	[HideInInspector]
	protected LTDescr ourTween;

	public bool IsInFromMode => _toFrom == ToFrom.From;

	public bool IsInAddativeMode => _absAdd == AbsAdd.Additive;

	protected virtual void OnTweenComplete()
	{
		Continue();
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
			LeanTween.cancel(_targetObject.Value);
		}
		ourTween = ExecuteTween();
		ourTween.setEase(easeType).setRepeat(repeats).setLoopType(loopType);
		if (waitUntilFinished)
		{
			if (ourTween != null)
			{
				ourTween.setOnComplete(OnTweenComplete);
			}
		}
		else
		{
			Continue();
		}
	}

	public abstract LTDescr ExecuteTween();

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
		if (!((Object)(object)variable == (Object)(object)_targetObject.gameObjectRef))
		{
			return (Object)(object)variable == (Object)(object)_duration.floatRef;
		}
		return true;
	}
}
