using UnityEngine;

namespace Fungus;

[CommandInfo("Math", "Exp", "Command to execute and store the result of a Exp", 0)]
[AddComponentMenu("")]
public class Exp : BaseUnaryMathCommand
{
	public override void OnEnter()
	{
		outValue.Value = Mathf.Exp(inValue.Value);
		Continue();
	}
}
