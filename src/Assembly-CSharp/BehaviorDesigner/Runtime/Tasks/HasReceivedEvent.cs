using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x0200149D RID: 5277
	[TaskDescription("Returns success as soon as the event specified by eventName has been received.")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=123")]
	[TaskIcon("{SkinColor}HasReceivedEventIcon.png")]
	public class HasReceivedEvent : Conditional
	{
		// Token: 0x06007EB2 RID: 32434 RVA: 0x002C9220 File Offset: 0x002C7420
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

		// Token: 0x06007EB3 RID: 32435 RVA: 0x00055C6B File Offset: 0x00053E6B
		public override TaskStatus OnUpdate()
		{
			if (!this.eventReceived)
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x06007EB4 RID: 32436 RVA: 0x002C92C8 File Offset: 0x002C74C8
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

		// Token: 0x06007EB5 RID: 32437 RVA: 0x00055C78 File Offset: 0x00053E78
		private void ReceivedEvent()
		{
			this.eventReceived = true;
		}

		// Token: 0x06007EB6 RID: 32438 RVA: 0x00055C81 File Offset: 0x00053E81
		private void ReceivedEvent(object arg1)
		{
			this.ReceivedEvent();
			if (this.storedValue1 != null && !this.storedValue1.IsNone)
			{
				this.storedValue1.SetValue(arg1);
			}
		}

		// Token: 0x06007EB7 RID: 32439 RVA: 0x002C9378 File Offset: 0x002C7578
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

		// Token: 0x06007EB8 RID: 32440 RVA: 0x002C93D0 File Offset: 0x002C75D0
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

		// Token: 0x06007EB9 RID: 32441 RVA: 0x002C9448 File Offset: 0x002C7648
		public override void OnBehaviorComplete()
		{
			base.Owner.UnregisterEvent(this.eventName.Value, new Action(this.ReceivedEvent));
			base.Owner.UnregisterEvent<object>(this.eventName.Value, new Action<object>(this.ReceivedEvent));
			base.Owner.UnregisterEvent<object, object>(this.eventName.Value, new Action<object, object>(this.ReceivedEvent));
			base.Owner.UnregisterEvent<object, object, object>(this.eventName.Value, new Action<object, object, object>(this.ReceivedEvent));
			this.eventReceived = false;
			this.registered = false;
		}

		// Token: 0x06007EBA RID: 32442 RVA: 0x00055CAA File Offset: 0x00053EAA
		public override void OnReset()
		{
			this.eventName = "";
		}

		// Token: 0x04006BC3 RID: 27587
		[Tooltip("The name of the event to receive")]
		public SharedString eventName = "";

		// Token: 0x04006BC4 RID: 27588
		[Tooltip("Optionally store the first sent argument")]
		[SharedRequired]
		public SharedVariable storedValue1;

		// Token: 0x04006BC5 RID: 27589
		[Tooltip("Optionally store the second sent argument")]
		[SharedRequired]
		public SharedVariable storedValue2;

		// Token: 0x04006BC6 RID: 27590
		[Tooltip("Optionally store the third sent argument")]
		[SharedRequired]
		public SharedVariable storedValue3;

		// Token: 0x04006BC7 RID: 27591
		private bool eventReceived;

		// Token: 0x04006BC8 RID: 27592
		private bool registered;
	}
}
