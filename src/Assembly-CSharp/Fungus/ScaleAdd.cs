using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus;

[CommandInfo("iTween", "Scale Add", "Changes a game object's scale by a specified offset over time.", 0)]
[AddComponentMenu("")]
[ExecuteInEditMode]
public class ScaleAdd : iTweenCommand
{
	[Tooltip("A scale offset in space the GameObject will animate to")]
	[SerializeField]
	protected Vector3Data _offset;

	[HideInInspector]
	[FormerlySerializedAs("offset")]
	public Vector3 offsetOLD;

	public override void DoTween()
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		Hashtable hashtable = new Hashtable();
		hashtable.Add("name", _tweenName.Value);
		hashtable.Add("amount", _offset.Value);
		hashtable.Add("time", _duration.Value);
		hashtable.Add("easetype", easeType);
		hashtable.Add("looptype", loopType);
		hashtable.Add("oncomplete", "OniTweenComplete");
		hashtable.Add("oncompletetarget", ((Component)this).gameObject);
		hashtable.Add("oncompleteparams", this);
		iTween.ScaleAdd(_targetObject.Value, hashtable);
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
