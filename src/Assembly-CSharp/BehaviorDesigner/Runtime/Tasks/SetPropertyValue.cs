using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x0200148D RID: 5261
	[TaskDescription("Sets the property to the value specified. Returns success if the property was set.")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=150")]
	[TaskCategory("Reflection")]
	[TaskIcon("{SkinColor}ReflectionIcon.png")]
	public class SetPropertyValue : Action
	{
		// Token: 0x06007E3A RID: 32314 RVA: 0x002C86D4 File Offset: 0x002C68D4
		public override TaskStatus OnUpdate()
		{
			if (this.propertyValue == null)
			{
				Debug.LogWarning("Unable to get field - field value is null");
				return 1;
			}
			Type typeWithinAssembly = TaskUtility.GetTypeWithinAssembly(this.componentName.Value);
			if (typeWithinAssembly == null)
			{
				Debug.LogWarning("Unable to set property - type is null");
				return 1;
			}
			Component component = base.GetDefaultGameObject(this.targetGameObject.Value).GetComponent(typeWithinAssembly);
			if (component == null)
			{
				Debug.LogWarning("Unable to set the property with component " + this.componentName.Value);
				return 1;
			}
			component.GetType().GetProperty(this.propertyName.Value).SetValue(component, this.propertyValue.GetValue(), null);
			return 2;
		}

		// Token: 0x06007E3B RID: 32315 RVA: 0x0005559B File Offset: 0x0005379B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.componentName = null;
			this.propertyName = null;
			this.propertyValue = null;
		}

		// Token: 0x04006B84 RID: 27524
		[Tooltip("The GameObject to set the property on")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006B85 RID: 27525
		[Tooltip("The component to set the property on")]
		public SharedString componentName;

		// Token: 0x04006B86 RID: 27526
		[Tooltip("The name of the property")]
		public SharedString propertyName;

		// Token: 0x04006B87 RID: 27527
		[Tooltip("The value to set")]
		public SharedVariable propertyValue;
	}
}
