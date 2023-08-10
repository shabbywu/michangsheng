using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus;

[CommandInfo("iTween", "Look From", "Instantly rotates a GameObject to look at the supplied Vector3 then returns it to it's starting rotation over time.", 0)]
[AddComponentMenu("")]
[ExecuteInEditMode]
public class LookFrom : iTweenCommand
{
	[Tooltip("Target transform that the GameObject will look at")]
	[SerializeField]
	protected TransformData _fromTransform;

	[Tooltip("Target world position that the GameObject will look at, if no From Transform is set")]
	[SerializeField]
	protected Vector3Data _fromPosition;

	[Tooltip("Restricts rotation to the supplied axis only")]
	[SerializeField]
	protected iTweenAxis axis;

	[HideInInspector]
	[FormerlySerializedAs("fromTransform")]
	public Transform fromTransformOLD;

	[HideInInspector]
	[FormerlySerializedAs("fromPosition")]
	public Vector3 fromPositionOLD;

	public override void DoTween()
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		Hashtable hashtable = new Hashtable();
		hashtable.Add("name", _tweenName.Value);
		if ((Object)(object)_fromTransform.Value == (Object)null)
		{
			hashtable.Add("looktarget", _fromPosition.Value);
		}
		else
		{
			hashtable.Add("looktarget", _fromTransform.Value);
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
		iTween.LookFrom(_targetObject.Value, hashtable);
	}

	public override bool HasReference(Variable variable)
	{
		if (!((Object)(object)_fromTransform.transformRef == (Object)(object)variable) && !((Object)(object)_fromPosition.vector3Ref == (Object)(object)variable))
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
		if ((Object)(object)fromTransformOLD != (Object)null)
		{
			_fromTransform.Value = fromTransformOLD;
			fromTransformOLD = null;
		}
		if (fromPositionOLD != default(Vector3))
		{
			_fromPosition.Value = fromPositionOLD;
			fromPositionOLD = default(Vector3);
		}
	}
}
