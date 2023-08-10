using System;
using UnityEngine;

namespace Fungus;

[CommandInfo("Math", "Trig", "Command to execute and store the result of basic trigonometry", 0)]
[AddComponentMenu("")]
public class Trig : BaseUnaryMathCommand
{
	public enum Function
	{
		Rad2Deg,
		Deg2Rad,
		ACos,
		ASin,
		ATan,
		Cos,
		Sin,
		Tan
	}

	[Tooltip("Trigonometric function to run.")]
	[SerializeField]
	protected Function function = Function.Sin;

	public override void OnEnter()
	{
		switch (function)
		{
		case Function.Rad2Deg:
			outValue.Value = inValue.Value * 57.29578f;
			break;
		case Function.Deg2Rad:
			outValue.Value = inValue.Value * ((float)Math.PI / 180f);
			break;
		case Function.ACos:
			outValue.Value = Mathf.Acos(inValue.Value);
			break;
		case Function.ASin:
			outValue.Value = Mathf.Asin(inValue.Value);
			break;
		case Function.ATan:
			outValue.Value = Mathf.Atan(inValue.Value);
			break;
		case Function.Cos:
			outValue.Value = Mathf.Cos(inValue.Value);
			break;
		case Function.Sin:
			outValue.Value = Mathf.Sin(inValue.Value);
			break;
		case Function.Tan:
			outValue.Value = Mathf.Tan(inValue.Value);
			break;
		}
		Continue();
	}

	public override string GetSummary()
	{
		return function.ToString() + " " + base.GetSummary();
	}
}
