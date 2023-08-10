using UnityEngine;

namespace Fungus;

[CommandInfo("LeanTween", "Move", "Moves a game object to a specified position over time. The position can be defined by a transform in another object (using To Transform) or by setting an absolute position (using To Position, if To Transform is set to None).", 0)]
[AddComponentMenu("")]
[ExecuteInEditMode]
public class MoveLean : BaseLeanTweenCommand
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

	public override LTDescr ExecuteTween()
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		Vector3 val = (((Object)(object)_toTransform.Value == (Object)null) ? _toPosition.Value : _toTransform.Value.position);
		if (base.IsInAddativeMode)
		{
			val += _targetObject.Value.transform.position;
		}
		if (base.IsInFromMode)
		{
			Vector3 position = _targetObject.Value.transform.position;
			_targetObject.Value.transform.position = val;
			val = position;
		}
		if (isLocal)
		{
			return LeanTween.moveLocal(_targetObject.Value, val, _duration);
		}
		return LeanTween.move(_targetObject.Value, val, _duration);
	}

	public override bool HasReference(Variable variable)
	{
		if (!((Object)(object)_toTransform.transformRef == (Object)(object)variable) && !((Object)(object)_toPosition.vector3Ref == (Object)(object)variable))
		{
			return base.HasReference(variable);
		}
		return true;
	}
}
