using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityDebug
{
	// Token: 0x02001141 RID: 4417
	[TaskDescription("LogFormat is analgous to Debug.LogFormat().\nIt takes format string, substitutes arguments supplied a '{0-4}' and returns success.\nAny fields or arguments not supplied are ignored.It can be used for debugging.")]
	[TaskIcon("{SkinColor}LogIcon.png")]
	public class LogFormat : Action
	{
		// Token: 0x0600759A RID: 30106 RVA: 0x002B4AD0 File Offset: 0x002B2CD0
		public override TaskStatus OnUpdate()
		{
			object[] array = this.buildParamsArray();
			if (this.logError.Value)
			{
				Debug.LogErrorFormat(this.textFormat.Value, array);
			}
			else
			{
				Debug.LogFormat(this.textFormat.Value, array);
			}
			return 2;
		}

		// Token: 0x0600759B RID: 30107 RVA: 0x002B4B18 File Offset: 0x002B2D18
		private object[] buildParamsArray()
		{
			object[] array;
			if (this.isValid(this.arg3))
			{
				array = new object[]
				{
					null,
					null,
					null,
					this.arg3.GetValue()
				};
				array[2] = this.arg2.GetValue();
				array[1] = this.arg1.GetValue();
				array[0] = this.arg0.GetValue();
			}
			else if (this.isValid(this.arg2))
			{
				array = new object[]
				{
					null,
					null,
					this.arg2.GetValue()
				};
				array[1] = this.arg1.GetValue();
				array[0] = this.arg0.GetValue();
			}
			else if (this.isValid(this.arg1))
			{
				array = new object[]
				{
					null,
					this.arg1.GetValue()
				};
				array[0] = this.arg0.GetValue();
			}
			else
			{
				if (!this.isValid(this.arg0))
				{
					return null;
				}
				array = new object[]
				{
					this.arg0.GetValue()
				};
			}
			return array;
		}

		// Token: 0x0600759C RID: 30108 RVA: 0x002B4C13 File Offset: 0x002B2E13
		private bool isValid(SharedVariable sv)
		{
			return sv != null && !sv.IsNone;
		}

		// Token: 0x0600759D RID: 30109 RVA: 0x002B4C23 File Offset: 0x002B2E23
		public override void OnReset()
		{
			this.textFormat = string.Empty;
			this.logError = false;
			this.arg0 = null;
			this.arg1 = null;
			this.arg2 = null;
			this.arg3 = null;
		}

		// Token: 0x04006123 RID: 24867
		[Tooltip("Text format with {0}, {1}, etc")]
		public SharedString textFormat;

		// Token: 0x04006124 RID: 24868
		[Tooltip("Is this text an error?")]
		public SharedBool logError;

		// Token: 0x04006125 RID: 24869
		public SharedVariable arg0;

		// Token: 0x04006126 RID: 24870
		public SharedVariable arg1;

		// Token: 0x04006127 RID: 24871
		public SharedVariable arg2;

		// Token: 0x04006128 RID: 24872
		public SharedVariable arg3;
	}
}
