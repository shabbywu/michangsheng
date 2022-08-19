using System;
using System.Reflection;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x02000FD2 RID: 4050
	[TaskDescription("Gets the value from the property specified. Returns success if the property was retrieved.")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=148")]
	[TaskCategory("Reflection")]
	[TaskIcon("{SkinColor}ReflectionIcon.png")]
	public class GetPropertyValue : Action
	{
		// Token: 0x06007037 RID: 28727 RVA: 0x002A8F94 File Offset: 0x002A7194
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

		// Token: 0x06007038 RID: 28728 RVA: 0x002A9043 File Offset: 0x002A7243
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.componentName = null;
			this.propertyName = null;
			this.propertyValue = null;
		}

		// Token: 0x04005C7C RID: 23676
		[Tooltip("The GameObject to get the property of")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005C7D RID: 23677
		[Tooltip("The component to get the property of")]
		public SharedString componentName;

		// Token: 0x04005C7E RID: 23678
		[Tooltip("The name of the property")]
		public SharedString propertyName;

		// Token: 0x04005C7F RID: 23679
		[Tooltip("The value of the property")]
		[RequiredField]
		public SharedVariable propertyValue;
	}
}
