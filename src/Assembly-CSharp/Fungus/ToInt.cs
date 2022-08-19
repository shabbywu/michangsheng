using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000E00 RID: 3584
	[CommandInfo("Math", "ToInt", "Command to execute and store the result of a float to int conversion", 0)]
	[AddComponentMenu("")]
	public class ToInt : Command
	{
		// Token: 0x06006547 RID: 25927 RVA: 0x00282804 File Offset: 0x00280A04
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

		// Token: 0x06006548 RID: 25928 RVA: 0x00282888 File Offset: 0x00280A88
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

		// Token: 0x06006549 RID: 25929 RVA: 0x0027D3DB File Offset: 0x0027B5DB
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x0600654A RID: 25930 RVA: 0x0028293A File Offset: 0x00280B3A
		public override bool HasReference(Variable variable)
		{
			return variable == this.inValue.floatRef || variable == this.outValue.integerRef;
		}

		// Token: 0x0400570B RID: 22283
		[Tooltip("To integer mode; round, floor or ceil.")]
		[SerializeField]
		protected ToInt.Mode function;

		// Token: 0x0400570C RID: 22284
		[Tooltip("Value to be passed in to the function.")]
		[SerializeField]
		protected FloatData inValue;

		// Token: 0x0400570D RID: 22285
		[Tooltip("Where the result of the function is stored.")]
		[SerializeField]
		protected IntegerData outValue;

		// Token: 0x020016BE RID: 5822
		public enum Mode
		{
			// Token: 0x04007381 RID: 29569
			RoundToInt,
			// Token: 0x04007382 RID: 29570
			FloorToInt,
			// Token: 0x04007383 RID: 29571
			CeilToInt
		}
	}
}
