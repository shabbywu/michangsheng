using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityBehaviour
{
	// Token: 0x02001622 RID: 5666
	[TaskCategory("Basic/Behaviour")]
	[TaskDescription("Returns Success if the object is enabled, otherwise Failure.")]
	public class IsEnabled : Conditional
	{
		// Token: 0x0600841C RID: 33820 RVA: 0x002CF434 File Offset: 0x002CD634
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

		// Token: 0x0600841D RID: 33821 RVA: 0x0005B2F0 File Offset: 0x000594F0
		public override void OnReset()
		{
			if (this.specifiedObject != null)
			{
				this.specifiedObject.Value = null;
			}
		}

		// Token: 0x040070CA RID: 28874
		[Tooltip("The Object to use")]
		public SharedObject specifiedObject;
	}
}
