using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus;

[CommandInfo("iTween", "Rotate To", "Rotates a game object to the specified angles over time.", 0)]
[AddComponentMenu("")]
[ExecuteInEditMode]
public class RotateTo : iTweenCommand
{
	[Tooltip("Target transform that the GameObject will rotate to")]
	[SerializeField]
	protected TransformData _toTransform;

	[Tooltip("Target rotation that the GameObject will rotate to, if no To Transform is set")]
	[SerializeField]
	protected Vector3Data _toRotation;

	[Tooltip("Whether to animate in world space or relative to the parent. False by default.")]
	[SerializeField]
	protected bool isLocal;

	[HideInInspector]
	[FormerlySerializedAs("toTransform")]
	public Transform toTransformOLD;

	[HideInInspector]
	[FormerlySerializedAs("toRotation")]
	public Vector3 toRotationOLD;

	public override void DoTween()
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		Hashtable hashtable = new Hashtable();
		hashtable.Add("name", _tweenName.Value);
		if ((Object)(object)_toTransform.Value == (Object)null)
		{
			hashtable.Add("rotation", _toRotation.Value);
		}
		else
		{
			hashtable.Add("rotation", _toTransform.Value);
		}
		hashtable.Add("time", _duration.Value);
		hashtable.Add("easetype", easeType);
		hashtable.Add("looptype", loopType);
		hashtable.Add("isLocal", isLocal);
		hashtable.Add("oncomplete", "OniTweenComplete");
		hashtable.Add("oncompletetarget", ((Component)this).gameObject);
		hashtable.Add("oncompleteparams", this);
		iTween.RotateTo(_targetObject.Value, hashtable);
	}

	public override bool HasReference(Variable variable)
	{
		if (!((Object)(object)_toTransform.transformRef == (Object)(object)variable) && !((Object)(object)_toRotation.vector3Ref == (Object)(object)variable))
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
		if (toRotationOLD != default(Vector3))
		{
			_toRotation.Value = toRotationOLD;
			toRotationOLD = default(Vector3);
		}
	}
}
