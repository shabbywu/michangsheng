using System;
using System.Reflection;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x0200148A RID: 5258
	[TaskDescription("Gets the value from the property specified. Returns success if the property was retrieved.")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=148")]
	[TaskCategory("Reflection")]
	[TaskIcon("{SkinColor}ReflectionIcon.png")]
	public class GetPropertyValue : Action
	{
		// Token: 0x06007E31 RID: 32305 RVA: 0x002C8418 File Offset: 0x002C6618
		public override TaskStatus OnUpdate()
		{
			if (this.propertyValue == null)
			{
				Debug.LogWarning("Unable to get property - property value is null");
				return 1;
			}
			Type typeWithinAssembly = TaskUtility.GetTypeWithinAssembly(this.componentName.Value);
			if (typeWithinAssembly == null)
			{
				Debug.LogWarning("Unable to get property - type is null");
				return 1;
			}
			Component component = base.GetDefaultGameObject(this.targetGameObject.Value).GetComponent(typeWithinAssembly);
			if (component == null)
			{
				Debug.LogWarning("Unable to get the property with component " + this.componentName.Value);
				return 1;
			}
			PropertyInfo property = component.GetType().GetProperty(this.propertyName.Value);
			this.propertyValue.SetValue(property.GetValue(component, null));
			return 2;
		}

		// Token: 0x06007E32 RID: 32306 RVA: 0x00055525 File Offset: 0x00053725
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.componentName = null;
			this.propertyName = null;
			this.propertyValue = null;
		}

		// Token: 0x04006B74 RID: 27508
		[Tooltip("The GameObject to get the property of")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006B75 RID: 27509
		[Tooltip("The component to get the property of")]
		public SharedString componentName;

		// Token: 0x04006B76 RID: 27510
		[Tooltip("The name of the property")]
		public SharedString propertyName;

		// Token: 0x04006B77 RID: 27511
		[Tooltip("The value of the property")]
		[RequiredField]
		public SharedVariable propertyValue;
	}
}
