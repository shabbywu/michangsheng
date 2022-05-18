using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityString
{
	// Token: 0x020014FE RID: 5374
	[TaskCategory("Basic/String")]
	[TaskDescription("Stores a string with the specified format.")]
	public class Format : Action
	{
		// Token: 0x0600801E RID: 32798 RVA: 0x0005717D File Offset: 0x0005537D
		public override void OnAwake()
		{
			this.variableValues = new object[this.variables.Length];
		}

		// Token: 0x0600801F RID: 32799 RVA: 0x002CAD14 File Offset: 0x002C8F14
		public override TaskStatus OnUpdate()
		{
			for (int i = 0; i < this.variableValues.Length; i++)
			{
				this.variableValues[i] = this.variables[i].Value.value.GetValue();
			}
			try
			{
				this.storeResult.Value = string.Format(this.format.Value, this.variableValues);
			}
			catch (Exception ex)
			{
				Debug.LogError(ex.Message);
				return 1;
			}
			return 2;
		}

		// Token: 0x06008020 RID: 32800 RVA: 0x00057192 File Offset: 0x00055392
		public override void OnReset()
		{
			this.format = "";
			this.variables = null;
			this.storeResult = null;
		}

		// Token: 0x04006CFB RID: 27899
		[Tooltip("The format of the string")]
		public SharedString format;

		// Token: 0x04006CFC RID: 27900
		[Tooltip("Any variables to appear in the string")]
		public SharedGenericVariable[] variables;

		// Token: 0x04006CFD RID: 27901
		[Tooltip("The result of the format")]
		[RequiredField]
		public SharedString storeResult;

		// Token: 0x04006CFE RID: 27902
		private object[] variableValues;
	}
}
