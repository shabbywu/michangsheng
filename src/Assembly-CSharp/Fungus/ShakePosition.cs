using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus;

[CommandInfo("iTween", "Shake Position", "Randomly shakes a GameObject's position by a diminishing amount over time.", 0)]
[AddComponentMenu("")]
[ExecuteInEditMode]
public class ShakePosition : iTweenCommand
{
	[Tooltip("A translation offset in space the GameObject will animate to")]
	[SerializeField]
	protected Vector3Data _amount;

	[Tooltip("Whether to animate in world space or relative to the parent. False by default.")]
	[SerializeField]
	protected bool isLocal;

	[Tooltip("Restricts rotation to the supplied axis only")]
	[SerializeField]
	protected iTweenAxis axis;

	[HideInInspector]
	[FormerlySerializedAs("amount")]
	public Vector3 amountOLD;

	public override void DoTween()
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		Hashtable hashtable = new Hashtable();
		hashtable.Add("name", _tweenName.Value);
		hashtable.Add("amount", _amount.Value);
		switch (axis)
		{
		case iTweenAxis.X:
			hashtable.Add("axis", "x");
			break;
		case iTweenAxis.Y:
			hashtable.Add("axis", "y");
			break;
		case iTweenAxis.Z:
			hashtable.Add("axis", "z");
			break;
		}
		hashtable.Add("time", _duration.Value);
		hashtable.Add("easetype", easeType);
		hashtable.Add("looptype", loopType);
		hashtable.Add("isLocal", isLocal);
		hashtable.Add("oncomplete", "OniTweenComplete");
		hashtable.Add("oncompletetarget", ((Component)this).gameObject);
		hashtable.Add("oncompleteparams", this);
		iTween.ShakePosition(_targetObject.Value, hashtable);
	}

	public override bool HasReference(Variable variable)
	{
		if (!((Object)(object)_amount.vector3Ref == (Object)(object)variable))
		{
			return base.HasReference(variable);
		}
		return true;
	}

	protected override void OnEnable()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		base.OnEnable();
		if (amountOLD != default(Vector3))
		{
			_amount.Value = amountOLD;
			amountOLD = default(Vector3);
		}
	}
}
