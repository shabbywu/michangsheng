using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus;

[CommandInfo("iTween", "Rotate Add", "Rotates a game object by the specified angles over time.", 0)]
[AddComponentMenu("")]
[ExecuteInEditMode]
public class RotateAdd : iTweenCommand
{
	[Tooltip("A rotation offset in space the GameObject will animate to")]
	[SerializeField]
	protected Vector3Data _offset;

	[Tooltip("Apply the transformation in either the world coordinate or local cordinate system")]
	[SerializeField]
	protected Space space = (Space)1;

	[HideInInspector]
	[FormerlySerializedAs("offset")]
	public Vector3 offsetOLD;

	public override void DoTween()
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		Hashtable hashtable = new Hashtable();
		hashtable.Add("name", _tweenName.Value);
		hashtable.Add("amount", _offset.Value);
		hashtable.Add("space", space);
		hashtable.Add("time", _duration.Value);
		hashtable.Add("easetype", easeType);
		hashtable.Add("looptype", loopType);
		hashtable.Add("oncomplete", "OniTweenComplete");
		hashtable.Add("oncompletetarget", ((Component)this).gameObject);
		hashtable.Add("oncompleteparams", this);
		iTween.RotateAdd(_targetObject.Value, hashtable);
	}

	public override bool HasReference(Variable variable)
	{
		if (!((Object)(object)_offset.vector3Ref == (Object)(object)variable))
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
		if (offsetOLD != default(Vector3))
		{
			_offset.Value = offsetOLD;
			offsetOLD = default(Vector3);
		}
	}
}
