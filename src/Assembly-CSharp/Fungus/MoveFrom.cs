using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus;

[CommandInfo("iTween", "Move From", "Moves a game object from a specified position back to its starting position over time. The position can be defined by a transform in another object (using To Transform) or by setting an absolute position (using To Position, if To Transform is set to None).", 0)]
[AddComponentMenu("")]
[ExecuteInEditMode]
public class MoveFrom : iTweenCommand
{
	[Tooltip("Target transform that the GameObject will move from")]
	[SerializeField]
	protected TransformData _fromTransform;

	[Tooltip("Target world position that the GameObject will move from, if no From Transform is set")]
	[SerializeField]
	protected Vector3Data _fromPosition;

	[Tooltip("Whether to animate in world space or relative to the parent. False by default.")]
	[SerializeField]
	protected bool isLocal;

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
			hashtable.Add("position", _fromPosition.Value);
		}
		else
		{
			hashtable.Add("position", _fromTransform.Value);
		}
		hashtable.Add("time", _duration.Value);
		hashtable.Add("easetype", easeType);
		hashtable.Add("looptype", loopType);
		hashtable.Add("isLocal", isLocal);
		hashtable.Add("oncomplete", "OniTweenComplete");
		hashtable.Add("oncompletetarget", ((Component)this).gameObject);
		hashtable.Add("oncompleteparams", this);
		iTween.MoveFrom(_targetObject.Value, hashtable);
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
