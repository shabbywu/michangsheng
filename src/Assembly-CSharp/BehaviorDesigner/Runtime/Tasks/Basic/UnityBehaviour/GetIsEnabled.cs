using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityBehaviour
{
	// Token: 0x02001621 RID: 5665
	[TaskCategory("Basic/Behaviour")]
	[TaskDescription("Stores the enabled state of the object. Returns Success.")]
	public class GetIsEnabled : Action
	{
		// Token: 0x06008419 RID: 33817 RVA: 0x002CF3E0 File Offset: 0x002CD5E0
		public override TaskStatus OnUpdate()
		{
			if (this.specifiedObject == null && !(this.specifiedObject.Value is Behaviour))
			{
				Debug.LogWarning("SpecifiedObject is null or not a subclass of UnityEngine.Behaviour");
				return 1;
			}
			this.storeValue.Value = (this.specifiedObject.Value as Behaviour).enabled;
			return 2;
		}

		// Token: 0x0600841A RID: 33818 RVA: 0x0005B2CE File Offset: 0x000594CE
		public override void OnReset()
		{
			if (this.specifiedObject != null)
			{
				this.specifiedObject.Value = null;
			}
			this.storeValue = false;
		}

		// Token: 0x040070C8 RID: 28872
		[Tooltip("The Object to use")]
		public SharedObject specifiedObject;

		// Token: 0x040070C9 RID: 28873
		[Tooltip("The enabled/disabled state")]
		[RequiredField]
		public SharedBool storeValue;
	}
}
