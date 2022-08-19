using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x02000FEF RID: 4079
	[TaskDescription("Compares the field value to the value specified. Returns success if the values are the same.")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=151")]
	[TaskCategory("Reflection")]
	[TaskIcon("{SkinColor}ReflectionIcon.png")]
	public class CompareFieldValue : Conditional
	{
		// Token: 0x060070EE RID: 28910 RVA: 0x002AAD84 File Offset: 0x002A8F84
		public override TaskStatus OnUpdate()
		{
			if (this.compareValue == null)
			{
				Debug.LogWarning("Unable to compare field - compare value is null");
				return 1;
			}
			Type typeWithinAssembly = TaskUtility.GetTypeWithinAssembly(this.componentName.Value);
			if (typeWithinAssembly == null)
			{
				Debug.LogWarning("Unable to compare field - type is null");
				return 1;
			}
			Component component = base.GetDefaultGameObject(this.targetGameObject.Value).GetComponent(typeWithinAssembly);
			if (component == null)
			{
				Debug.LogWarning("Unable to compare the field with component " + this.componentName.Value);
				return 1;
			}
			object value = component.GetType().GetField(this.fieldName.Value).GetValue(component);
			if (value == null && this.compareValue.GetValue() == null)
			{
				return 2;
			}
			if (!value.Equals(this.compareValue.GetValue()))
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x060070EF RID: 28911 RVA: 0x002AAE4D File Offset: 0x002A904D
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.componentName = null;
			this.fieldName = null;
			this.compareValue = null;
		}

		// Token: 0x04005CED RID: 23789
		[Tooltip("The GameObject to compare the field on")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005CEE RID: 23790
		[Tooltip("The component to compare the field on")]
		public SharedString componentName;

		// Token: 0x04005CEF RID: 23791
		[Tooltip("The name of the field")]
		public SharedString fieldName;

		// Token: 0x04005CF0 RID: 23792
		[Tooltip("The value to compare to")]
		public SharedVariable compareValue;
	}
}
