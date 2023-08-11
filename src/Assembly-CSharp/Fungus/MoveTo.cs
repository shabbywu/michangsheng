using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus;

[CommandInfo("iTween", "Move To", "Moves a game object to a specified position over time. The position can be defined by a transform in another object (using To Transform) or by setting an absolute position (using To Position, if To Transform is set to None).", 0)]
[AddComponentMenu("")]
[ExecuteInEditMode]
public class MoveTo : iTweenCommand
{
	[Tooltip("Target transform that the GameObject will move to")]
	[SerializeField]
	protected TransformData _toTransform;

	[Tooltip("Target world position that the GameObject will move to, if no From Transform is set")]
	[SerializeField]
	protected Vector3Data _toPosition;

	[Tooltip("Whether to animate in world space or relative to the parent. False by default.")]
	[SerializeField]
	protected bool isLocal;

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
			hashtable.Add("position", _toPosition.Value);
		}
		else
		{
			hashtable.Add("position", _toTransform.Value);
		}
		hashtable.Add("time", _duration.Value);
		hashtable.Add("easetype", easeType);
		hashtable.Add("looptype", loopType);
		hashtable.Add("isLocal", isLocal);
		hashtable.Add("oncomplete", "OniTweenComplete");
		hashtable.Add("oncompletetarget", ((Component)this).gameObject);
		hashtable.Add("oncompleteparams", this);
		iTween.MoveTo(_targetObject.Value, hashtable);
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
