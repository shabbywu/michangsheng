using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x02000FCF RID: 4047
	[TaskDescription("Log is a simple task which will output the specified text and return success. It can be used for debugging.")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=16")]
	[TaskIcon("{SkinColor}LogIcon.png")]
	public class Log : Action
	{
		// Token: 0x0600702E RID: 28718 RVA: 0x002A8E2D File Offset: 0x002A702D
		public override TaskStatus OnUpdate()
		{
			if (this.logError.Value)
			{
				Debug.LogError(this.text);
			}
			else
			{
				Debug.Log(this.text);
			}
			return 2;
		}

		// Token: 0x0600702F RID: 28719 RVA: 0x002A8E55 File Offset: 0x002A7055
		public override void OnReset()
		{
			this.text = "";
			this.logError = false;
		}

		// Token: 0x04005C74 RID: 23668
		[Tooltip("Text to output to the log")]
		public SharedString text;

		// Token: 0x04005C75 RID: 23669
		[Tooltip("Is this text an error?")]
		public SharedBool logError;
	}
}
