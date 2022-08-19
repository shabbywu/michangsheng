using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityString
{
	// Token: 0x02001044 RID: 4164
	[TaskCategory("Basic/String")]
	[TaskDescription("Stores a string with the specified format.")]
	public class Format : Action
	{
		// Token: 0x06007224 RID: 29220 RVA: 0x002AD502 File Offset: 0x002AB702
		public override void OnAwake()
		{
			this.variableValues = new object[this.variables.Length];
		}

		// Token: 0x06007225 RID: 29221 RVA: 0x002AD518 File Offset: 0x002AB718
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

		// Token: 0x06007226 RID: 29222 RVA: 0x002AD59C File Offset: 0x002AB79C
		public override void OnReset()
		{
			this.format = "";
			this.variables = null;
			this.storeResult = null;
		}

		// Token: 0x04005DFB RID: 24059
		[Tooltip("The format of the string")]
		public SharedString format;

		// Token: 0x04005DFC RID: 24060
		[Tooltip("Any variables to appear in the string")]
		public SharedGenericVariable[] variables;

		// Token: 0x04005DFD RID: 24061
		[Tooltip("The result of the format")]
		[RequiredField]
		public SharedString storeResult;

		// Token: 0x04005DFE RID: 24062
		private object[] variableValues;
	}
}
