using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020014A7 RID: 5287
	[TaskDescription("Compares the field value to the value specified. Returns success if the values are the same.")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=151")]
	[TaskCategory("Reflection")]
	[TaskIcon("{SkinColor}ReflectionIcon.png")]
	public class CompareFieldValue : Conditional
	{
		// Token: 0x06007EE8 RID: 32488 RVA: 0x002C978C File Offset: 0x002C798C
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

		// Token: 0x06007EE9 RID: 32489 RVA: 0x00055F8D File Offset: 0x0005418D
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.componentName = null;
			this.fieldName = null;
			this.compareValue = null;
		}

		// Token: 0x04006BE5 RID: 27621
		[Tooltip("The GameObject to compare the field on")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006BE6 RID: 27622
		[Tooltip("The component to compare the field on")]
		public SharedString componentName;

		// Token: 0x04006BE7 RID: 27623
		[Tooltip("The name of the field")]
		public SharedString fieldName;

		// Token: 0x04006BE8 RID: 27624
		[Tooltip("The value to compare to")]
		public SharedVariable compareValue;
	}
}
