using UnityEngine;

namespace Fungus;

[CommandInfo("Math", "Sign", "Command to execute and store the result of a Sign", 0)]
[AddComponentMenu("")]
public class Sign : BaseUnaryMathCommand
{
	public override void OnEnter()
	{
		outValue.Value = Mathf.Sign(inValue.Value);
		Continue();
	}
}
