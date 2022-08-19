using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x02000FF0 RID: 4080
	[TaskDescription("Compares the property value to the value specified. Returns success if the values are the same.")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=152")]
	[TaskCategory("Reflection")]
	[TaskIcon("{SkinColor}ReflectionIcon.png")]
	public class ComparePropertyValue : Conditional
	{
		// Token: 0x060070F1 RID: 28913 RVA: 0x002AAE6C File Offset: 0x002A906C
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

		// Token: 0x060070F2 RID: 28914 RVA: 0x002AAF36 File Offset: 0x002A9136
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.componentName = null;
			this.propertyName = null;
			this.compareValue = null;
		}

		// Token: 0x04005CF1 RID: 23793
		[Tooltip("The GameObject to compare the property of")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005CF2 RID: 23794
		[Tooltip("The component to compare the property of")]
		public SharedString componentName;

		// Token: 0x04005CF3 RID: 23795
		[Tooltip("The name of the property")]
		public SharedString propertyName;

		// Token: 0x04005CF4 RID: 23796
		[Tooltip("The value to compare to")]
		public SharedVariable compareValue;
	}
}
