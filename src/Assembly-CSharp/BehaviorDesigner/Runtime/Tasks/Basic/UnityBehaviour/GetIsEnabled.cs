using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityBehaviour
{
	// Token: 0x02001162 RID: 4450
	[TaskCategory("Basic/Behaviour")]
	[TaskDescription("Stores the enabled state of the object. Returns Success.")]
	public class GetIsEnabled : Action
	{
		// Token: 0x0600761F RID: 30239 RVA: 0x002B5D7C File Offset: 0x002B3F7C
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

		// Token: 0x06007620 RID: 30240 RVA: 0x002B5DD0 File Offset: 0x002B3FD0
		public override void OnReset()
		{
			if (this.specifiedObject != null)
			{
				this.specifiedObject.Value = null;
			}
			this.storeValue = false;
		}

		// Token: 0x040061A5 RID: 24997
		[Tooltip("The Object to use")]
		public SharedObject specifiedObject;

		// Token: 0x040061A6 RID: 24998
		[Tooltip("The enabled/disabled state")]
		[RequiredField]
		public SharedBool storeValue;
	}
}
