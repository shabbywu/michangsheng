using UnityEngine;

namespace Fungus;

[CommandInfo("LeanTween", "Rotate", "Rotates a game object to the specified angles over time.", 0)]
[AddComponentMenu("")]
[ExecuteInEditMode]
public class RotateLean : BaseLeanTweenCommand
{
	public enum RotateMode
	{
		PureRotate,
		LookAt2D,
		LookAt3D
	}

	[Tooltip("Target transform that the GameObject will rotate to")]
	[SerializeField]
	protected TransformData _toTransform;

	[Tooltip("Target rotation that the GameObject will rotate to, if no To Transform is set")]
	[SerializeField]
	protected Vector3Data _toRotation;

	[Tooltip("Whether to animate in world space or relative to the parent. False by default.")]
	[SerializeField]
	protected bool isLocal;

	[Tooltip("Whether to use the provided Transform or Vector as a target to look at rather than a euler to match.")]
	[SerializeField]
	protected RotateMode rotateMode;

	public override LTDescr ExecuteTween()
	{
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		Quaternion val;
		Vector3 val2;
		if (!((Object)(object)_toTransform.Value == (Object)null))
		{
			val = _toTransform.Value.rotation;
			val2 = ((Quaternion)(ref val)).eulerAngles;
		}
		else
		{
			val2 = _toRotation.Value;
		}
		Vector3 val3 = val2;
		if (rotateMode == RotateMode.LookAt3D)
		{
			Vector3 val4 = (((Object)(object)_toTransform.Value == (Object)null) ? _toRotation.Value : _toTransform.Value.position) - _targetObject.Value.transform.position;
			val = Quaternion.LookRotation(((Vector3)(ref val4)).normalized);
			val3 = ((Quaternion)(ref val)).eulerAngles;
		}
		else if (rotateMode == RotateMode.LookAt2D)
		{
			Vector3 val5 = (((Object)(object)_toTransform.Value == (Object)null) ? _toRotation.Value : _toTransform.Value.position) - _targetObject.Value.transform.position;
			val5.z = 0f;
			val = Quaternion.FromToRotation(_targetObject.Value.transform.up, ((Vector3)(ref val5)).normalized);
			val3 = ((Quaternion)(ref val)).eulerAngles;
		}
		if (base.IsInAddativeMode)
		{
			Vector3 val6 = val3;
			val = _targetObject.Value.transform.rotation;
			val3 = val6 + ((Quaternion)(ref val)).eulerAngles;
		}
		if (base.IsInFromMode)
		{
			val = _targetObject.Value.transform.rotation;
			Vector3 eulerAngles = ((Quaternion)(ref val)).eulerAngles;
			_targetObject.Value.transform.rotation = Quaternion.Euler(val3);
			val3 = eulerAngles;
		}
		if (isLocal)
		{
			return LeanTween.rotateLocal(_targetObject.Value, val3, _duration);
		}
		return LeanTween.rotate(_targetObject.Value, val3, _duration);
	}

	public override bool HasReference(Variable variable)
	{
		if (!((Object)(object)variable == (Object)(object)_toTransform.transformRef) && !((Object)(object)_toRotation.vector3Ref == (Object)(object)variable))
		{
			return base.HasReference(variable);
		}
		return true;
	}
}
