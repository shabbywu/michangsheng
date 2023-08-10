using UnityEngine;

namespace Fungus;

[CommandInfo("Math", "Negate", "Negate a float", 0)]
[AddComponentMenu("")]
public class Neg : BaseUnaryMathCommand
{
	public override void OnEnter()
	{
		outValue.Value = 0f - inValue.Value;
		Continue();
	}
}
