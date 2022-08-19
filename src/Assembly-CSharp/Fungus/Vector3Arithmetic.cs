using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000E59 RID: 3673
	[CommandInfo("Vector3", "Arithmetic", "Vector3 add, sub, mul, div arithmetic", 0)]
	[AddComponentMenu("")]
	public class Vector3Arithmetic : Command
	{
		// Token: 0x0600672D RID: 26413 RVA: 0x002899D4 File Offset: 0x00287BD4
		public override void OnEnter()
		{
			switch (this.operation)
			{
			case Vector3Arithmetic.Operation.Add:
				this.output.Value = this.lhs.Value + this.rhs.Value;
				break;
			case Vector3Arithmetic.Operation.Sub:
				this.output.Value = this.lhs.Value - this.rhs.Value;
				break;
			case Vector3Arithmetic.Operation.Mul:
			{
				Vector3 value = this.lhs.Value;
				value.Scale(this.rhs.Value);
				this.output.Value = value;
				break;
			}
			case Vector3Arithmetic.Operation.Div:
			{
				Vector3 value = this.lhs.Value;
				value.Scale(new Vector3(1f / this.rhs.Value.x, 1f / this.rhs.Value.y, 1f / this.rhs.Value.z));
				this.output.Value = value;
				break;
			}
			}
			this.Continue();
		}

		// Token: 0x0600672E RID: 26414 RVA: 0x00289AF4 File Offset: 0x00287CF4
		public override string GetSummary()
		{
			if (this.output.vector3Ref == null)
			{
				return "Error: no output set";
			}
			return this.operation.ToString() + ": stored in " + this.output.vector3Ref.Key;
		}

		// Token: 0x0600672F RID: 26415 RVA: 0x0027D3DB File Offset: 0x0027B5DB
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x06006730 RID: 26416 RVA: 0x00289B45 File Offset: 0x00287D45
		public override bool HasReference(Variable variable)
		{
			return this.lhs.vector3Ref == variable || this.rhs.vector3Ref == variable || this.output.vector3Ref == variable;
		}

		// Token: 0x04005846 RID: 22598
		[SerializeField]
		protected Vector3Data lhs;

		// Token: 0x04005847 RID: 22599
		[SerializeField]
		protected Vector3Data rhs;

		// Token: 0x04005848 RID: 22600
		[SerializeField]
		protected Vector3Data output;

		// Token: 0x04005849 RID: 22601
		[SerializeField]
		protected Vector3Arithmetic.Operation operation;

		// Token: 0x020016C7 RID: 5831
		public enum Operation
		{
			// Token: 0x040073B3 RID: 29619
			Add,
			// Token: 0x040073B4 RID: 29620
			Sub,
			// Token: 0x040073B5 RID: 29621
			Mul,
			// Token: 0x040073B6 RID: 29622
			Div
		}
	}
}
