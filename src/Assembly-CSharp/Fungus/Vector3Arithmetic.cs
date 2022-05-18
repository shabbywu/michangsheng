using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020012AC RID: 4780
	[CommandInfo("Vector3", "Arithmetic", "Vector3 add, sub, mul, div arithmetic", 0)]
	[AddComponentMenu("")]
	public class Vector3Arithmetic : Command
	{
		// Token: 0x060073BB RID: 29627 RVA: 0x002AC53C File Offset: 0x002AA73C
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

		// Token: 0x060073BC RID: 29628 RVA: 0x002AC65C File Offset: 0x002AA85C
		public override string GetSummary()
		{
			if (this.output.vector3Ref == null)
			{
				return "Error: no output set";
			}
			return this.operation.ToString() + ": stored in " + this.output.vector3Ref.Key;
		}

		// Token: 0x060073BD RID: 29629 RVA: 0x0004C5E0 File Offset: 0x0004A7E0
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x060073BE RID: 29630 RVA: 0x0004EF9F File Offset: 0x0004D19F
		public override bool HasReference(Variable variable)
		{
			return this.lhs.vector3Ref == variable || this.rhs.vector3Ref == variable || this.output.vector3Ref == variable;
		}

		// Token: 0x0400659D RID: 26013
		[SerializeField]
		protected Vector3Data lhs;

		// Token: 0x0400659E RID: 26014
		[SerializeField]
		protected Vector3Data rhs;

		// Token: 0x0400659F RID: 26015
		[SerializeField]
		protected Vector3Data output;

		// Token: 0x040065A0 RID: 26016
		[SerializeField]
		protected Vector3Arithmetic.Operation operation;

		// Token: 0x020012AD RID: 4781
		public enum Operation
		{
			// Token: 0x040065A2 RID: 26018
			Add,
			// Token: 0x040065A3 RID: 26019
			Sub,
			// Token: 0x040065A4 RID: 26020
			Mul,
			// Token: 0x040065A5 RID: 26021
			Div
		}
	}
}
