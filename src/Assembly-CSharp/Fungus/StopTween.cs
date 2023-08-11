using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus;

[CommandInfo("iTween", "Stop Tween", "Stops an active iTween by name.", 0)]
[AddComponentMenu("")]
[ExecuteInEditMode]
public class StopTween : Command
{
	[Tooltip("Stop and destroy any Tweens in current scene with the supplied name")]
	[SerializeField]
	protected StringData _tweenName;

	[HideInInspector]
	[FormerlySerializedAs("tweenName")]
	public string tweenNameOLD = "";

	public override void OnEnter()
	{
		iTween.StopByName(_tweenName.Value);
		Continue();
	}

	public override bool HasReference(Variable variable)
	{
		if (!((Object)(object)_tweenName.stringRef == (Object)(object)variable))
		{
			return base.HasReference(variable);
		}
		return true;
	}

	protected virtual void OnEnable()
	{
		if (tweenNameOLD != "")
		{
			_tweenName.Value = tweenNameOLD;
			tweenNameOLD = "";
		}
	}
}
