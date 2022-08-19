using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x02000FD5 RID: 4053
	[TaskDescription("Sets the property to the value specified. Returns success if the property was set.")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=150")]
	[TaskCategory("Reflection")]
	[TaskIcon("{SkinColor}ReflectionIcon.png")]
	public class SetPropertyValue : Action
	{
		// Token: 0x06007040 RID: 28736 RVA: 0x002A92CC File Offset: 0x002A74CC
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

		// Token: 0x06007041 RID: 28737 RVA: 0x002A9379 File Offset: 0x002A7579
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.componentName = null;
			this.propertyName = null;
			this.propertyValue = null;
		}

		// Token: 0x04005C8C RID: 23692
		[Tooltip("The GameObject to set the property on")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005C8D RID: 23693
		[Tooltip("The component to set the property on")]
		public SharedString componentName;

		// Token: 0x04005C8E RID: 23694
		[Tooltip("The name of the property")]
		public SharedString propertyName;

		// Token: 0x04005C8F RID: 23695
		[Tooltip("The value to set")]
		public SharedVariable propertyValue;
	}
}
