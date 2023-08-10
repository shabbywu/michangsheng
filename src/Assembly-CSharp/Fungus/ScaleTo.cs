using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus;

[CommandInfo("iTween", "Scale To", "Changes a game object's scale to a specified value over time.", 0)]
[AddComponentMenu("")]
[ExecuteInEditMode]
public class ScaleTo : iTweenCommand
{
	[Tooltip("Target transform that the GameObject will scale to")]
	[SerializeField]
	protected TransformData _toTransform;

	[Tooltip("Target scale that the GameObject will scale to, if no To Transform is set")]
	[SerializeField]
	protected Vector3Data _toScale = new Vector3Data(Vector3.one);

	[HideInInspector]
	[FormerlySerializedAs("toTransform")]
	public Transform toTransformOLD;

	[HideInInspector]
	[FormerlySerializedAs("toScale")]
	public Vector3 toScaleOLD;

	public override void DoTween()
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		Hashtable hashtable = new Hashtable();
		hashtable.Add("name", _tweenName.Value);
		if ((Object)(object)_toTransform.Value == (Object)null)
		{
			hashtable.Add("scale", _toScale.Value);
		}
		else
		{
			hashtable.Add("scale", _toTransform.Value);
		}
		hashtable.Add("time", _duration.Value);
		hashtable.Add("easetype", easeType);
		hashtable.Add("looptype", loopType);
		hashtable.Add("oncomplete", "OniTweenComplete");
		hashtable.Add("oncompletetarget", ((Component)this).gameObject);
		hashtable.Add("oncompleteparams", this);
		iTween.ScaleTo(_targetObject.Value, hashtable);
	}

	public override bool HasReference(Variable variable)
	{
		if (!((Object)(object)_toTransform.transformRef == (Object)(object)variable) && !((Object)(object)_toScale.vector3Ref == (Object)(object)variable))
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
		if (toScaleOLD != default(Vector3))
		{
			_toScale.Value = toScaleOLD;
			toScaleOLD = default(Vector3);
		}
	}
}
