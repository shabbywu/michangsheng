using System.Collections.Generic;
using UnityEngine;

namespace Fungus;

public abstract class TweenUI : Command
{
	[Tooltip("List of objects to be affected by the tween")]
	[SerializeField]
	protected List<GameObject> targetObjects = new List<GameObject>();

	[Tooltip("Type of tween easing to apply")]
	[SerializeField]
	protected LeanTweenType tweenType = LeanTweenType.easeOutQuad;

	[Tooltip("Wait until this command completes before continuing execution")]
	[SerializeField]
	protected BooleanData waitUntilFinished = new BooleanData(v: true);

	[Tooltip("Time for the tween to complete")]
	[SerializeField]
	protected FloatData duration = new FloatData(1f);

	protected virtual void ApplyTween()
	{
		for (int i = 0; i < targetObjects.Count; i++)
		{
			GameObject val = targetObjects[i];
			if (!((Object)(object)val == (Object)null))
			{
				ApplyTween(val);
			}
		}
		if ((bool)waitUntilFinished)
		{
			LeanTween.value(((Component)this).gameObject, 0f, 1f, duration).setOnComplete(OnComplete);
		}
	}

	protected abstract void ApplyTween(GameObject go);

	protected virtual void OnComplete()
	{
		Continue();
	}

	protected virtual string GetSummaryValue()
	{
		return "";
	}

	public override void OnEnter()
	{
		if (targetObjects.Count == 0)
		{
			Continue();
			return;
		}
		ApplyTween();
		if (!waitUntilFinished)
		{
			Continue();
		}
	}

	public override void OnCommandAdded(Block parentBlock)
	{
		if (targetObjects.Count == 0)
		{
			targetObjects.Add(null);
		}
	}

	public override string GetSummary()
	{
		if (targetObjects.Count == 0)
		{
			return "Error: No targetObjects selected";
		}
		if (targetObjects.Count == 1)
		{
			if ((Object)(object)targetObjects[0] == (Object)null)
			{
				return "Error: No targetObjects selected";
			}
			return ((Object)targetObjects[0]).name + " = " + GetSummaryValue();
		}
		string text = "";
		for (int i = 0; i < targetObjects.Count; i++)
		{
			GameObject val = targetObjects[i];
			if (!((Object)(object)val == (Object)null))
			{
				text = ((!(text == "")) ? (text + ", " + ((Object)val).name) : (text + ((Object)val).name));
			}
		}
		return text + " = " + GetSummaryValue();
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)180, (byte)250, (byte)250, byte.MaxValue));
	}

	public override bool IsReorderableArray(string propertyName)
	{
		if (propertyName == "targetObjects")
		{
			return true;
		}
		return false;
	}

	public override bool HasReference(Variable variable)
	{
		if (!((Object)(object)waitUntilFinished.booleanRef == (Object)(object)variable) && !((Object)(object)duration.floatRef == (Object)(object)variable))
		{
			return base.HasReference(variable);
		}
		return true;
	}
}
