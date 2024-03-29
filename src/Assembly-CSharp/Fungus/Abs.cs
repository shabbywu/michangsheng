using UnityEngine;

namespace Fungus;

[CommandInfo("Math", "Abs", "Command to execute and store the result of a Abs", 0)]
[AddComponentMenu("")]
public class Abs : BaseUnaryMathCommand
{
	public override void OnEnter()
	{
		outValue.Value = Mathf.Abs(inValue.Value);
		Continue();
	}
}
