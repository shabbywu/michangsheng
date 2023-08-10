using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus;

[CommandInfo("iTween", "Rotate From", "Rotates a game object from the specified angles back to its starting orientation over time.", 0)]
[AddComponentMenu("")]
[ExecuteInEditMode]
public class RotateFrom : iTweenCommand
{
	[Tooltip("Target transform that the GameObject will rotate from")]
	[SerializeField]
	protected TransformData _fromTransform;

	[Tooltip("Target rotation that the GameObject will rotate from, if no From Transform is set")]
	[SerializeField]
	protected Vector3Data _fromRotation;

	[Tooltip("Whether to animate in world space or relative to the parent. False by default.")]
	[SerializeField]
	protected bool isLocal;

	[HideInInspector]
	[FormerlySerializedAs("fromTransform")]
	public Transform fromTransformOLD;

	[HideInInspector]
	[FormerlySerializedAs("fromRotation")]
	public Vector3 fromRotationOLD;

	public override void DoTween()
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		Hashtable hashtable = new Hashtable();
		hashtable.Add("name", _tweenName.Value);
		if ((Object)(object)_fromTransform.Value == (Object)null)
		{
			hashtable.Add("rotation", _fromRotation.Value);
		}
		else
		{
			hashtable.Add("rotation", _fromTransform.Value);
		}
		hashtable.Add("time", _duration.Value);
		hashtable.Add("easetype", easeType);
		hashtable.Add("looptype", loopType);
		hashtable.Add("isLocal", isLocal);
		hashtable.Add("oncomplete", "OniTweenComplete");
		hashtable.Add("oncompletetarget", ((Component)this).gameObject);
		hashtable.Add("oncompleteparams", this);
		iTween.RotateFrom(_targetObject.Value, hashtable);
	}

	public override bool HasReference(Variable variable)
	{
		if (!((Object)(object)_fromTransform.transformRef == (Object)(object)variable) && !((Object)(object)_fromRotation.vector3Ref == (Object)(object)variable))
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
		if (fromRotationOLD != default(Vector3))
		{
			_fromRotation.Value = fromRotationOLD;
			fromRotationOLD = default(Vector3);
		}
	}
}
