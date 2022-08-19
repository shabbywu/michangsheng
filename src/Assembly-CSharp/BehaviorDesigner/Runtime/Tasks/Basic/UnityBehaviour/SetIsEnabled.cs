using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityBehaviour
{
	// Token: 0x02001164 RID: 4452
	[TaskCategory("Basic/Behaviour")]
	[TaskDescription("Enables/Disables the object. Returns Success.")]
	public class SetIsEnabled : Action
	{
		// Token: 0x06007625 RID: 30245 RVA: 0x002B5E58 File Offset: 0x002B4058
		public override TaskStatus OnUpdate()
		{
			if (this.specifiedObject == null && !(this.specifiedObject.Value is Behaviour))
			{
				Debug.LogWarning("SpecifiedObject is null or not a subclass of UnityEngine.Behaviour");
				return 1;
			}
			(this.specifiedObject.Value as Behaviour).enabled = this.enabled.Value;
			return 2;
		}

		// Token: 0x06007626 RID: 30246 RVA: 0x002B5EAC File Offset: 0x002B40AC
		public override void OnReset()
		{
			if (this.specifiedObject != null)
			{
				this.specifiedObject.Value = null;
			}
			this.enabled = false;
		}

		// Token: 0x040061A8 RID: 25000
		[Tooltip("The Object to use")]
		public SharedObject specifiedObject;

		// Token: 0x040061A9 RID: 25001
		[Tooltip("The enabled/disabled state")]
		public SharedBool enabled;
	}
}
