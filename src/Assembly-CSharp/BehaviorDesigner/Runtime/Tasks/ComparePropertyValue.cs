using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020014A8 RID: 5288
	[TaskDescription("Compares the property value to the value specified. Returns success if the values are the same.")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=152")]
	[TaskCategory("Reflection")]
	[TaskIcon("{SkinColor}ReflectionIcon.png")]
	public class ComparePropertyValue : Conditional
	{
		// Token: 0x06007EEB RID: 32491 RVA: 0x002C9858 File Offset: 0x002C7A58
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
				Debug.LogWarning("Unable to compare property - type is null");
				return 1;
			}
			Component component = base.GetDefaultGameObject(this.targetGameObject.Value).GetComponent(typeWithinAssembly);
			if (component == null)
			{
				Debug.LogWarning("Unable to compare the property with component " + this.componentName.Value);
				return 1;
			}
			object value = component.GetType().GetProperty(this.propertyName.Value).GetValue(component, null);
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

		// Token: 0x06007EEC RID: 32492 RVA: 0x00055FAB File Offset: 0x000541AB
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.componentName = null;
			this.propertyName = null;
			this.compareValue = null;
		}

		// Token: 0x04006BE9 RID: 27625
		[Tooltip("The GameObject to compare the property of")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006BEA RID: 27626
		[Tooltip("The component to compare the property of")]
		public SharedString componentName;

		// Token: 0x04006BEB RID: 27627
		[Tooltip("The name of the property")]
		public SharedString propertyName;

		// Token: 0x04006BEC RID: 27628
		[Tooltip("The value to compare to")]
		public SharedVariable compareValue;
	}
}
