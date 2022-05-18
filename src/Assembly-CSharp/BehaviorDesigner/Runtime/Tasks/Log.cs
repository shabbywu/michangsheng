using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x02001487 RID: 5255
	[TaskDescription("Log is a simple task which will output the specified text and return success. It can be used for debugging.")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=16")]
	[TaskIcon("{SkinColor}LogIcon.png")]
	public class Log : Action
	{
		// Token: 0x06007E28 RID: 32296 RVA: 0x000554AC File Offset: 0x000536AC
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

		// Token: 0x06007E29 RID: 32297 RVA: 0x000554D4 File Offset: 0x000536D4
		public override void OnReset()
		{
			this.text = "";
			this.logError = false;
		}

		// Token: 0x04006B6C RID: 27500
		[Tooltip("Text to output to the log")]
		public SharedString text;

		// Token: 0x04006B6D RID: 27501
		[Tooltip("Is this text an error?")]
		public SharedBool logError;
	}
}
