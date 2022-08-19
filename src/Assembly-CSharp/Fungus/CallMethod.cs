using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000DBB RID: 3515
	[CommandInfo("Scripting", "Call Method", "Calls a named method on a GameObject using the GameObject.SendMessage() system.", 0)]
	[AddComponentMenu("")]
	public class CallMethod : Command
	{
		// Token: 0x06006406 RID: 25606 RVA: 0x0027D419 File Offset: 0x0027B619
		protected virtual void CallTheMethod()
		{
			this.targetObject.SendMessage(this.methodName, 1);
		}

		// Token: 0x06006407 RID: 25607 RVA: 0x0027D430 File Offset: 0x0027B630
		public override void OnEnter()
		{
			if (this.targetObject == null || this.methodName.Length == 0)
			{
				this.Continue();
				return;
			}
			if (Mathf.Approximately(this.delay, 0f))
			{
				this.CallTheMethod();
			}
			else
			{
				base.Invoke("CallTheMethod", this.delay);
			}
			this.Continue();
		}

		// Token: 0x06006408 RID: 25608 RVA: 0x0027D490 File Offset: 0x0027B690
		public override string GetSummary()
		{
			if (this.targetObject == null)
			{
				return "Error: No target GameObject specified";
			}
			if (this.methodName.Length == 0)
			{
				return "Error: No named method specified";
			}
			return this.targetObject.name + " : " + this.methodName;
		}

		// Token: 0x06006409 RID: 25609 RVA: 0x0027D3DB File Offset: 0x0027B5DB
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x04005613 RID: 22035
		[Tooltip("Target monobehavior which contains the method we want to call")]
		[SerializeField]
		protected GameObject targetObject;

		// Token: 0x04005614 RID: 22036
		[Tooltip("Name of the method to call")]
		[SerializeField]
		protected string methodName = "";

		// Token: 0x04005615 RID: 22037
		[Tooltip("Delay (in seconds) before the method will be called")]
		[SerializeField]
		protected float delay;
	}
}
