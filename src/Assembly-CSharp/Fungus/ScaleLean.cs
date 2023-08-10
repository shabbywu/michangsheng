using UnityEngine;

namespace Fungus;

[CommandInfo("LeanTween", "Scale", "Changes a game object's scale to a specified value over time.", 0)]
[AddComponentMenu("")]
[ExecuteInEditMode]
public class ScaleLean : BaseLeanTweenCommand
{
	[Tooltip("Target transform that the GameObject will scale to")]
	[SerializeField]
	protected TransformData _toTransform;

	[Tooltip("Target scale that the GameObject will scale to, if no To Transform is set")]
	[SerializeField]
	protected Vector3Data _toScale = new Vector3Data(Vector3.one);

	public override LTDescr ExecuteTween()
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		Vector3 val = (((Object)(object)_toTransform.Value == (Object)null) ? _toScale.Value : _toTransform.Value.localScale);
		if (base.IsInAddativeMode)
		{
			val += _targetObject.Value.transform.localScale;
		}
		if (base.IsInFromMode)
		{
			Vector3 localScale = _targetObject.Value.transform.localScale;
			_targetObject.Value.transform.localScale = val;
			val = localScale;
		}
		return LeanTween.scale(_targetObject.Value, val, _duration);
	}

	public override bool HasReference(Variable variable)
	{
		if (!((Object)(object)variable == (Object)(object)_toTransform.transformRef) && !((Object)(object)_toScale.vector3Ref == (Object)(object)variable))
		{
			return base.HasReference(variable);
		}
		return true;
	}
}
