using System;
using UnityEngine;
using UnityEngine.Events;

namespace Fungus
{
	// Token: 0x02000DDB RID: 3547
	[CommandInfo("Scripting", "Invoke Event", "Calls a list of component methods via the Unity Event System (as used in the Unity UI). This command is more efficient than the Invoke Method command but can only pass a single parameter and doesn't support return values.", 0)]
	[AddComponentMenu("")]
	public class InvokeEvent : Command
	{
		// Token: 0x060064AF RID: 25775 RVA: 0x0027FBF0 File Offset: 0x0027DDF0
		protected virtual void DoInvoke()
		{
			switch (this.invokeType)
			{
			default:
				this.staticEvent.Invoke();
				return;
			case InvokeType.DynamicBoolean:
				this.booleanEvent.Invoke(this.booleanParameter.Value);
				return;
			case InvokeType.DynamicInteger:
				this.integerEvent.Invoke(this.integerParameter.Value);
				return;
			case InvokeType.DynamicFloat:
				this.floatEvent.Invoke(this.floatParameter.Value);
				return;
			case InvokeType.DynamicString:
				this.stringEvent.Invoke(this.stringParameter.Value);
				return;
			}
		}

		// Token: 0x060064B0 RID: 25776 RVA: 0x0027FC85 File Offset: 0x0027DE85
		public override void OnEnter()
		{
			if (Mathf.Approximately(this.delay, 0f))
			{
				this.DoInvoke();
			}
			else
			{
				base.Invoke("DoInvoke", this.delay);
			}
			this.Continue();
		}

		// Token: 0x060064B1 RID: 25777 RVA: 0x0027FCB8 File Offset: 0x0027DEB8
		public override string GetSummary()
		{
			if (!string.IsNullOrEmpty(this.description))
			{
				return this.description;
			}
			string text = this.invokeType.ToString() + " ";
			switch (this.invokeType)
			{
			default:
				text += this.staticEvent.GetPersistentEventCount();
				break;
			case InvokeType.DynamicBoolean:
				text += this.booleanEvent.GetPersistentEventCount();
				break;
			case InvokeType.DynamicInteger:
				text += this.integerEvent.GetPersistentEventCount();
				break;
			case InvokeType.DynamicFloat:
				text += this.floatEvent.GetPersistentEventCount();
				break;
			case InvokeType.DynamicString:
				text += this.stringEvent.GetPersistentEventCount();
				break;
			}
			return text + " methods";
		}

		// Token: 0x060064B2 RID: 25778 RVA: 0x0027D3DB File Offset: 0x0027B5DB
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x060064B3 RID: 25779 RVA: 0x0027FD9C File Offset: 0x0027DF9C
		public override bool HasReference(Variable variable)
		{
			return this.booleanParameter.booleanRef == variable || this.integerParameter.integerRef == variable || this.floatParameter.floatRef == variable || this.stringParameter.stringRef == variable || base.HasReference(variable);
		}

		// Token: 0x04005681 RID: 22145
		[Tooltip("A description of what this command does. Appears in the command summary.")]
		[SerializeField]
		protected string description = "";

		// Token: 0x04005682 RID: 22146
		[Tooltip("Delay (in seconds) before the methods will be called")]
		[SerializeField]
		protected float delay;

		// Token: 0x04005683 RID: 22147
		[Tooltip("Selects type of method parameter to pass")]
		[SerializeField]
		protected InvokeType invokeType;

		// Token: 0x04005684 RID: 22148
		[Tooltip("List of methods to call. Supports methods with no parameters or exactly one string, int, float or object parameter.")]
		[SerializeField]
		protected UnityEvent staticEvent = new UnityEvent();

		// Token: 0x04005685 RID: 22149
		[Tooltip("Boolean parameter to pass to the invoked methods.")]
		[SerializeField]
		protected BooleanData booleanParameter;

		// Token: 0x04005686 RID: 22150
		[Tooltip("List of methods to call. Supports methods with one boolean parameter.")]
		[SerializeField]
		protected InvokeEvent.BooleanEvent booleanEvent = new InvokeEvent.BooleanEvent();

		// Token: 0x04005687 RID: 22151
		[Tooltip("Integer parameter to pass to the invoked methods.")]
		[SerializeField]
		protected IntegerData integerParameter;

		// Token: 0x04005688 RID: 22152
		[Tooltip("List of methods to call. Supports methods with one integer parameter.")]
		[SerializeField]
		protected InvokeEvent.IntegerEvent integerEvent = new InvokeEvent.IntegerEvent();

		// Token: 0x04005689 RID: 22153
		[Tooltip("Float parameter to pass to the invoked methods.")]
		[SerializeField]
		protected FloatData floatParameter;

		// Token: 0x0400568A RID: 22154
		[Tooltip("List of methods to call. Supports methods with one float parameter.")]
		[SerializeField]
		protected InvokeEvent.FloatEvent floatEvent = new InvokeEvent.FloatEvent();

		// Token: 0x0400568B RID: 22155
		[Tooltip("String parameter to pass to the invoked methods.")]
		[SerializeField]
		protected StringDataMulti stringParameter;

		// Token: 0x0400568C RID: 22156
		[Tooltip("List of methods to call. Supports methods with one string parameter.")]
		[SerializeField]
		protected InvokeEvent.StringEvent stringEvent = new InvokeEvent.StringEvent();

		// Token: 0x020016B0 RID: 5808
		[Serializable]
		public class BooleanEvent : UnityEvent<bool>
		{
		}

		// Token: 0x020016B1 RID: 5809
		[Serializable]
		public class IntegerEvent : UnityEvent<int>
		{
		}

		// Token: 0x020016B2 RID: 5810
		[Serializable]
		public class FloatEvent : UnityEvent<float>
		{
		}

		// Token: 0x020016B3 RID: 5811
		[Serializable]
		public class StringEvent : UnityEvent<string>
		{
		}
	}
}
