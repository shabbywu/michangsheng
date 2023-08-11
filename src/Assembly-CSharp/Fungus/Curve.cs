using UnityEngine;

namespace Fungus;

[CommandInfo("Math", "Curve", "Pass a value through an AnimationCurve", 0)]
[AddComponentMenu("")]
public class Curve : BaseUnaryMathCommand
{
	[SerializeField]
	protected AnimationCurve curve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

	public override void OnEnter()
	{
		outValue.Value = curve.Evaluate(inValue.Value);
		Continue();
	}
}
