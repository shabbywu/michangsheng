using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus;

[CommandInfo("iTween", "Punch Scale", "Applies a jolt of force to a GameObject's scale and wobbles it back to its initial scale.", 0)]
[AddComponentMenu("")]
[ExecuteInEditMode]
public class PunchScale : iTweenCommand
{
	[Tooltip("A scale offset in space the GameObject will animate to")]
	[SerializeField]
	protected Vector3Data _amount;

	[HideInInspector]
	[FormerlySerializedAs("amount")]
	public Vector3 amountOLD;

	public override void DoTween()
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		Hashtable hashtable = new Hashtable();
		hashtable.Add("name", _tweenName.Value);
		hashtable.Add("amount", _amount.Value);
		hashtable.Add("time", _duration.Value);
		hashtable.Add("easetype", easeType);
		hashtable.Add("looptype", loopType);
		hashtable.Add("oncomplete", "OniTweenComplete");
		hashtable.Add("oncompletetarget", ((Component)this).gameObject);
		hashtable.Add("oncompleteparams", this);
		iTween.PunchScale(_targetObject.Value, hashtable);
	}

	public override bool HasReference(Variable variable)
	{
		return (Object)(object)variable == (Object)(object)_amount.vector3Ref;
	}

	protected override void OnEnable()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		base.OnEnable();
		if (amountOLD != default(Vector3))
		{
			_amount.Value = amountOLD;
			amountOLD = default(Vector3);
		}
	}
}
