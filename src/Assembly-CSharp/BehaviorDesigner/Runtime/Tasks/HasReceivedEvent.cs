using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x02000FE5 RID: 4069
	[TaskDescription("Returns success as soon as the event specified by eventName has been received.")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=123")]
	[TaskIcon("{SkinColor}HasReceivedEventIcon.png")]
	public class HasReceivedEvent : Conditional
	{
		// Token: 0x060070B8 RID: 28856 RVA: 0x002AA4F0 File Offset: 0x002A86F0
		public override void OnStart()
		{
			if (!this.registered)
			{
				base.Owner.RegisterEvent(this.eventName.Value, new Action(this.ReceivedEvent));
				base.Owner.RegisterEvent<object>(this.eventName.Value, new Action<object>(this.ReceivedEvent));
				base.Owner.RegisterEvent<object, object>(this.eventName.Value, new Action<object, object>(this.ReceivedEvent));
				base.Owner.RegisterEvent<object, object, object>(this.eventName.Value, new Action<object, object, object>(this.ReceivedEvent));
				this.registered = true;
			}
		}

		// Token: 0x060070B9 RID: 28857 RVA: 0x002AA597 File Offset: 0x002A8797
		public override TaskStatus OnUpdate()
		{
			if (!this.eventReceived)
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x060070BA RID: 28858 RVA: 0x002AA5A4 File Offset: 0x002A87A4
		public override void OnEnd()
		{
			if (this.eventReceived)
			{
				base.Owner.UnregisterEvent(this.eventName.Value, new Action(this.ReceivedEvent));
				base.Owner.UnregisterEvent<object>(this.eventName.Value, new Action<object>(this.ReceivedEvent));
				base.Owner.UnregisterEvent<object, object>(this.eventName.Value, new Action<object, object>(this.ReceivedEvent));
				base.Owner.UnregisterEvent<object, object, object>(this.eventName.Value, new Action<object, object, object>(this.ReceivedEvent));
				this.registered = false;
			}
			this.eventReceived = false;
		}

		// Token: 0x060070BB RID: 28859 RVA: 0x002AA652 File Offset: 0x002A8852
		private void ReceivedEvent()
		{
			this.eventReceived = true;
		}

		// Token: 0x060070BC RID: 28860 RVA: 0x002AA65B File Offset: 0x002A885B
		private void ReceivedEvent(object arg1)
		{
			this.ReceivedEvent();
			if (this.storedValue1 != null && !this.storedValue1.IsNone)
			{
				this.storedValue1.SetValue(arg1);
			}
		}

		// Token: 0x060070BD RID: 28861 RVA: 0x002AA684 File Offset: 0x002A8884
		private void ReceivedEvent(object arg1, object arg2)
		{
			this.ReceivedEvent();
			if (this.storedValue1 != null && !this.storedValue1.IsNone)
			{
				this.storedValue1.SetValue(arg1);
			}
			if (this.storedValue2 != null && !this.storedValue2.IsNone)
			{
				this.storedValue2.SetValue(arg2);
			}
		}

		// Token: 0x060070BE RID: 28862 RVA: 0x002AA6DC File Offset: 0x002A88DC
		private void ReceivedEvent(object arg1, object arg2, object arg3)
		{
			this.ReceivedEvent();
			if (this.storedValue1 != null && !this.storedValue1.IsNone)
			{
				this.storedValue1.SetValue(arg1);
			}
			if (this.storedValue2 != null && !this.storedValue2.IsNone)
			{
				this.storedValue2.SetValue(arg2);
			}
			if (this.storedValue3 != null && !this.storedValue3.IsNone)
			{
				this.storedValue3.SetValue(arg3);
			}
		}

		// Token: 0x060070BF RID: 28863 RVA: 0x002AA754 File Offset: 0x002A8954
		public override void OnBehaviorComplete()
		{
			base.Owner.UnregisterEvent(this.eventName.Value, new Action(this.ReceivedEvent));
			base.Owner.UnregisterEvent<object>(this.eventName.Value, new Action<object>(this.ReceivedEvent));
			base.Owner.UnregisterEvent<object, object>(this.eventName.Value, new Action<object, object>(this.ReceivedEvent));
			base.Owner.UnregisterEvent<object, object, object>(this.eventName.Value, new Action<object, object, object>(this.ReceivedEvent));
			this.eventReceived = false;
			this.registered = false;
		}

		// Token: 0x060070C0 RID: 28864 RVA: 0x002AA7F7 File Offset: 0x002A89F7
		public override void OnReset()
		{
			this.eventName = "";
		}

		// Token: 0x04005CCB RID: 23755
		[Tooltip("The name of the event to receive")]
		public SharedString eventName = "";

		// Token: 0x04005CCC RID: 23756
		[Tooltip("Optionally store the first sent argument")]
		[SharedRequired]
		public SharedVariable storedValue1;

		// Token: 0x04005CCD RID: 23757
		[Tooltip("Optionally store the second sent argument")]
		[SharedRequired]
		public SharedVariable storedValue2;

		// Token: 0x04005CCE RID: 23758
		[Tooltip("Optionally store the third sent argument")]
		[SharedRequired]
		public SharedVariable storedValue3;

		// Token: 0x04005CCF RID: 23759
		private bool eventReceived;

		// Token: 0x04005CD0 RID: 23760
		private bool registered;
	}
}
