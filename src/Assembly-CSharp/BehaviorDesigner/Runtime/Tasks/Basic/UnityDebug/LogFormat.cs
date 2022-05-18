using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityDebug
{
	// Token: 0x02001600 RID: 5632
	[TaskDescription("LogFormat is analgous to Debug.LogFormat().\nIt takes format string, substitutes arguments supplied a '{0-4}' and returns success.\nAny fields or arguments not supplied are ignored.It can be used for debugging.")]
	[TaskIcon("{SkinColor}LogIcon.png")]
	public class LogFormat : Action
	{
		// Token: 0x06008394 RID: 33684 RVA: 0x002CEAC8 File Offset: 0x002CCCC8
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

		// Token: 0x06008395 RID: 33685 RVA: 0x002CEB10 File Offset: 0x002CCD10
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

		// Token: 0x06008396 RID: 33686 RVA: 0x0005A945 File Offset: 0x00058B45
		private bool isValid(SharedVariable sv)
		{
			return sv != null && !sv.IsNone;
		}

		// Token: 0x06008397 RID: 33687 RVA: 0x0005A955 File Offset: 0x00058B55
		public override void OnReset()
		{
			this.textFormat = string.Empty;
			this.logError = false;
			this.arg0 = null;
			this.arg1 = null;
			this.arg2 = null;
			this.arg3 = null;
		}

		// Token: 0x04007046 RID: 28742
		[Tooltip("Text format with {0}, {1}, etc")]
		public SharedString textFormat;

		// Token: 0x04007047 RID: 28743
		[Tooltip("Is this text an error?")]
		public SharedBool logError;

		// Token: 0x04007048 RID: 28744
		public SharedVariable arg0;

		// Token: 0x04007049 RID: 28745
		public SharedVariable arg1;

		// Token: 0x0400704A RID: 28746
		public SharedVariable arg2;

		// Token: 0x0400704B RID: 28747
		public SharedVariable arg3;
	}
}
