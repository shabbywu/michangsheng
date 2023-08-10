using UnityEngine;

namespace Fungus;

[CommandInfo("Math", "Inverse", "Multiplicative Inverse of a float (1/f)", 0)]
[AddComponentMenu("")]
public class Inv : BaseUnaryMathCommand
{
	public override void OnEnter()
	{
		float value = inValue.Value;
		outValue.Value = ((value != 0f) ? (1f / inValue.Value) : 0f);
		Continue();
	}
}
