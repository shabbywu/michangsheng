using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityBehaviour
{
	// Token: 0x02001623 RID: 5667
	[TaskCategory("Basic/Behaviour")]
	[TaskDescription("Enables/Disables the object. Returns Success.")]
	public class SetIsEnabled : Action
	{
		// Token: 0x0600841F RID: 33823 RVA: 0x002CF484 File Offset: 0x002CD684
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

		// Token: 0x06008420 RID: 33824 RVA: 0x0005B306 File Offset: 0x00059506
		public override void OnReset()
		{
			if (this.specifiedObject != null)
			{
				this.specifiedObject.Value = null;
			}
			this.enabled = false;
		}

		// Token: 0x040070CB RID: 28875
		[Tooltip("The Object to use")]
		public SharedObject specifiedObject;

		// Token: 0x040070CC RID: 28876
		[Tooltip("The enabled/disabled state")]
		public SharedBool enabled;
	}
}
