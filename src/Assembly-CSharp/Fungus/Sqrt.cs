using UnityEngine;

namespace Fungus;

[CommandInfo("Math", "Sqrt", "Command to execute and store the result of a Sqrt", 0)]
[AddComponentMenu("")]
public class Sqrt : BaseUnaryMathCommand
{
	public override void OnEnter()
	{
		outValue.Value = Mathf.Sqrt(inValue.Value);
		Continue();
	}
}
