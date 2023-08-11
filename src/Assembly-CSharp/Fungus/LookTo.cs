using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus;

[CommandInfo("iTween", "Look To", "Rotates a GameObject to look at a supplied Transform or Vector3 over time.", 0)]
[AddComponentMenu("")]
[ExecuteInEditMode]
public class LookTo : iTweenCommand
{
	[Tooltip("Target transform that the GameObject will look at")]
	[SerializeField]
	protected TransformData _toTransform;

	[Tooltip("Target world position that the GameObject will look at, if no From Transform is set")]
	[SerializeField]
	protected Vector3Data _toPosition;

	[Tooltip("Restricts rotation to the supplied axis only")]
	[SerializeField]
	protected iTweenAxis axis;

	[HideInInspector]
	[FormerlySerializedAs("toTransform")]
	public Transform toTransformOLD;

	[HideInInspector]
	[FormerlySerializedAs("toPosition")]
	public Vector3 toPositionOLD;

	public override void DoTween()
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		Hashtable hashtable = new Hashtable();
		hashtable.Add("name", _tweenName.Value);
		if ((Object)(object)_toTransform.Value == (Object)null)
		{
			hashtable.Add("looktarget", _toPosition.Value);
		}
		else
		{
			hashtable.Add("looktarget", _toTransform.Value);
		}
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
		hashtable.Add("oncomplete", "OniTweenComplete");
		hashtable.Add("oncompletetarget", ((Component)this).gameObject);
		hashtable.Add("oncompleteparams", this);
		iTween.LookTo(_targetObject.Value, hashtable);
	}

	public override bool HasReference(Variable variable)
	{
		if (!((Object)(object)_toTransform.transformRef == (Object)(object)variable) && !((Object)(object)_toPosition.vector3Ref == (Object)(object)variable))
		{
			return base.HasReference(variable);
		}
		return true;
	}

	protected override void OnEnable()
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		base.OnEnable();
		if ((Object)(object)toTransformOLD != (Object)null)
		{
			_toTransform.Value = toTransformOLD;
			toTransformOLD = null;
		}
		if (toPositionOLD != default(Vector3))
		{
			_toPosition.Value = toPositionOLD;
			toPositionOLD = default(Vector3);
		}
	}
}
