using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020011F1 RID: 4593
	[CommandInfo("Scripting", "Call Method", "Calls a named method on a GameObject using the GameObject.SendMessage() system.", 0)]
	[AddComponentMenu("")]
	public class CallMethod : Command
	{
		// Token: 0x06007073 RID: 28787 RVA: 0x0004C641 File Offset: 0x0004A841
		protected virtual void CallTheMethod()
		{
			this.targetObject.SendMessage(this.methodName, 1);
		}

		// Token: 0x06007074 RID: 28788 RVA: 0x002A2628 File Offset: 0x002A0828
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

		// Token: 0x06007075 RID: 28789 RVA: 0x002A2688 File Offset: 0x002A0888
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

		// Token: 0x06007076 RID: 28790 RVA: 0x0004C5E0 File Offset: 0x0004A7E0
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x04006308 RID: 25352
		[Tooltip("Target monobehavior which contains the method we want to call")]
		[SerializeField]
		protected GameObject targetObject;

		// Token: 0x04006309 RID: 25353
		[Tooltip("Name of the method to call")]
		[SerializeField]
		protected string methodName = "";

		// Token: 0x0400630A RID: 25354
		[Tooltip("Delay (in seconds) before the method will be called")]
		[SerializeField]
		protected float delay;
	}
}
