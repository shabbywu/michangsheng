using System;
using UnityEngine;
using UnityEngine.Events;

namespace Fungus
{
	// Token: 0x02001217 RID: 4631
	[CommandInfo("Scripting", "Invoke Event", "Calls a list of component methods via the Unity Event System (as used in the Unity UI). This command is more efficient than the Invoke Method command but can only pass a single parameter and doesn't support return values.", 0)]
	[AddComponentMenu("")]
	public class InvokeEvent : Command
	{
		// Token: 0x06007131 RID: 28977 RVA: 0x002A4780 File Offset: 0x002A2980
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

		// Token: 0x06007132 RID: 28978 RVA: 0x0004CEA6 File Offset: 0x0004B0A6
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

		// Token: 0x06007133 RID: 28979 RVA: 0x002A4818 File Offset: 0x002A2A18
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

		// Token: 0x06007134 RID: 28980 RVA: 0x0004C5E0 File Offset: 0x0004A7E0
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x06007135 RID: 28981 RVA: 0x002A48FC File Offset: 0x002A2AFC
		public override bool HasReference(Variable variable)
		{
			return this.booleanParameter.booleanRef == variable || this.integerParameter.integerRef == variable || this.floatParameter.floatRef == variable || this.stringParameter.stringRef == variable || base.HasReference(variable);
		}

		// Token: 0x04006385 RID: 25477
		[Tooltip("A description of what this command does. Appears in the command summary.")]
		[SerializeField]
		protected string description = "";

		// Token: 0x04006386 RID: 25478
		[Tooltip("Delay (in seconds) before the methods will be called")]
		[SerializeField]
		protected float delay;

		// Token: 0x04006387 RID: 25479
		[Tooltip("Selects type of method parameter to pass")]
		[SerializeField]
		protected InvokeType invokeType;

		// Token: 0x04006388 RID: 25480
		[Tooltip("List of methods to call. Supports methods with no parameters or exactly one string, int, float or object parameter.")]
		[SerializeField]
		protected UnityEvent staticEvent = new UnityEvent();

		// Token: 0x04006389 RID: 25481
		[Tooltip("Boolean parameter to pass to the invoked methods.")]
		[SerializeField]
		protected BooleanData booleanParameter;

		// Token: 0x0400638A RID: 25482
		[Tooltip("List of methods to call. Supports methods with one boolean parameter.")]
		[SerializeField]
		protected InvokeEvent.BooleanEvent booleanEvent = new InvokeEvent.BooleanEvent();

		// Token: 0x0400638B RID: 25483
		[Tooltip("Integer parameter to pass to the invoked methods.")]
		[SerializeField]
		protected IntegerData integerParameter;

		// Token: 0x0400638C RID: 25484
		[Tooltip("List of methods to call. Supports methods with one integer parameter.")]
		[SerializeField]
		protected InvokeEvent.IntegerEvent integerEvent = new InvokeEvent.IntegerEvent();

		// Token: 0x0400638D RID: 25485
		[Tooltip("Float parameter to pass to the invoked methods.")]
		[SerializeField]
		protected FloatData floatParameter;

		// Token: 0x0400638E RID: 25486
		[Tooltip("List of methods to call. Supports methods with one float parameter.")]
		[SerializeField]
		protected InvokeEvent.FloatEvent floatEvent = new InvokeEvent.FloatEvent();

		// Token: 0x0400638F RID: 25487
		[Tooltip("String parameter to pass to the invoked methods.")]
		[SerializeField]
		protected StringDataMulti stringParameter;

		// Token: 0x04006390 RID: 25488
		[Tooltip("List of methods to call. Supports methods with one string parameter.")]
		[SerializeField]
		protected InvokeEvent.StringEvent stringEvent = new InvokeEvent.StringEvent();

		// Token: 0x02001218 RID: 4632
		[Serializable]
		public class BooleanEvent : UnityEvent<bool>
		{
		}

		// Token: 0x02001219 RID: 4633
		[Serializable]
		public class IntegerEvent : UnityEvent<int>
		{
		}

		// Token: 0x0200121A RID: 4634
		[Serializable]
		public class FloatEvent : UnityEvent<float>
		{
		}

		// Token: 0x0200121B RID: 4635
		[Serializable]
		public class StringEvent : UnityEvent<string>
		{
		}
	}
}
