using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus;

[CommandInfo("iTween", "Scale From", "Changes a game object's scale to the specified value and back to its original scale over time.", 0)]
[AddComponentMenu("")]
[ExecuteInEditMode]
public class ScaleFrom : iTweenCommand
{
	[Tooltip("Target transform that the GameObject will scale from")]
	[SerializeField]
	protected TransformData _fromTransform;

	[Tooltip("Target scale that the GameObject will scale from, if no From Transform is set")]
	[SerializeField]
	protected Vector3Data _fromScale;

	[HideInInspector]
	[FormerlySerializedAs("fromTransform")]
	public Transform fromTransformOLD;

	[HideInInspector]
	[FormerlySerializedAs("fromScale")]
	public Vector3 fromScaleOLD;

	public override void DoTween()
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		Hashtable hashtable = new Hashtable();
		hashtable.Add("name", _tweenName.Value);
		if ((Object)(object)_fromTransform.Value == (Object)null)
		{
			hashtable.Add("scale", _fromScale.Value);
		}
		else
		{
			hashtable.Add("scale", _fromTransform.Value);
		}
		hashtable.Add("time", _duration.Value);
		hashtable.Add("easetype", easeType);
		hashtable.Add("looptype", loopType);
		hashtable.Add("oncomplete", "OniTweenComplete");
		hashtable.Add("oncompletetarget", ((Component)this).gameObject);
		hashtable.Add("oncompleteparams", this);
		iTween.ScaleFrom(_targetObject.Value, hashtable);
	}

	public override bool HasReference(Variable variable)
	{
		if (!((Object)(object)_fromTransform.transformRef == (Object)(object)variable) && !((Object)(object)_fromScale.vector3Ref == (Object)(object)variable))
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
		if (fromScaleOLD != default(Vector3))
		{
			_fromScale.Value = fromScaleOLD;
			fromScaleOLD = default(Vector3);
		}
	}
}
