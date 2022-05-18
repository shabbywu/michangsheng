using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200124A RID: 4682
	[CommandInfo("Math", "ToInt", "Command to execute and store the result of a float to int conversion", 0)]
	[AddComponentMenu("")]
	public class ToInt : Command
	{
		// Token: 0x060071D5 RID: 29141 RVA: 0x002A6CA4 File Offset: 0x002A4EA4
		public override void OnEnter()
		{
			switch (this.function)
			{
			case ToInt.Mode.RoundToInt:
				this.outValue.Value = Mathf.RoundToInt(this.inValue.Value);
				break;
			case ToInt.Mode.FloorToInt:
				this.outValue.Value = Mathf.FloorToInt(this.inValue.Value);
				break;
			case ToInt.Mode.CeilToInt:
				this.outValue.Value = Mathf.CeilToInt(this.inValue.Value);
				break;
			}
			this.Continue();
		}

		// Token: 0x060071D6 RID: 29142 RVA: 0x002A6D28 File Offset: 0x002A4F28
		public override string GetSummary()
		{
			return string.Concat(new string[]
			{
				this.function.ToString(),
				" in: ",
				(this.inValue.floatRef != null) ? this.inValue.floatRef.Key : this.inValue.Value.ToString(),
				", out: ",
				(this.outValue.integerRef != null) ? this.outValue.integerRef.Key : this.outValue.Value.ToString()
			});
		}

		// Token: 0x060071D7 RID: 29143 RVA: 0x0004C5E0 File Offset: 0x0004A7E0
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x060071D8 RID: 29144 RVA: 0x0004D678 File Offset: 0x0004B878
		public override bool HasReference(Variable variable)
		{
			return variable == this.inValue.floatRef || variable == this.outValue.integerRef;
		}

		// Token: 0x04006430 RID: 25648
		[Tooltip("To integer mode; round, floor or ceil.")]
		[SerializeField]
		protected ToInt.Mode function;

		// Token: 0x04006431 RID: 25649
		[Tooltip("Value to be passed in to the function.")]
		[SerializeField]
		protected FloatData inValue;

		// Token: 0x04006432 RID: 25650
		[Tooltip("Where the result of the function is stored.")]
		[SerializeField]
		protected IntegerData outValue;

		// Token: 0x0200124B RID: 4683
		public enum Mode
		{
			// Token: 0x04006434 RID: 25652
			RoundToInt,
			// Token: 0x04006435 RID: 25653
			FloorToInt,
			// Token: 0x04006436 RID: 25654
			CeilToInt
		}
	}
}
