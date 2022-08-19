using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityBehaviour
{
	// Token: 0x02001163 RID: 4451
	[TaskCategory("Basic/Behaviour")]
	[TaskDescription("Returns Success if the object is enabled, otherwise Failure.")]
	public class IsEnabled : Conditional
	{
		// Token: 0x06007622 RID: 30242 RVA: 0x002B5DF4 File Offset: 0x002B3FF4
		public override TaskStatus OnUpdate()
		{
			if (this.specifiedObject == null && !(this.specifiedObject.Value is Behaviour))
			{
				Debug.LogWarning("SpecifiedObject is null or not a subclass of UnityEngine.Behaviour");
				return 1;
			}
			if (!(this.specifiedObject.Value as Behaviour).enabled)
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x06007623 RID: 30243 RVA: 0x002B5E41 File Offset: 0x002B4041
		public override void OnReset()
		{
			if (this.specifiedObject != null)
			{
				this.specifiedObject.Value = null;
			}
		}

		// Token: 0x040061A7 RID: 24999
		[Tooltip("The Object to use")]
		public SharedObject specifiedObject;
	}
}
