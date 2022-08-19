using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x02000FD4 RID: 4052
	[TaskDescription("Sets the field to the value specified. Returns success if the field was set.")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=149")]
	[TaskCategory("Reflection")]
	[TaskIcon("{SkinColor}ReflectionIcon.png")]
	public class SetFieldValue : Action
	{
		// Token: 0x0600703D RID: 28733 RVA: 0x002A9200 File Offset: 0x002A7400
		public override TaskStatus OnUpdate()
		{
			if (this.fieldValue == null)
			{
				Debug.LogWarning("Unable to get field - field value is null");
				return 1;
			}
			Type typeWithinAssembly = TaskUtility.GetTypeWithinAssembly(this.componentName.Value);
			if (typeWithinAssembly == null)
			{
				Debug.LogWarning("Unable to set field - type is null");
				return 1;
			}
			Component component = base.GetDefaultGameObject(this.targetGameObject.Value).GetComponent(typeWithinAssembly);
			if (component == null)
			{
				Debug.LogWarning("Unable to set the field with component " + this.componentName.Value);
				return 1;
			}
			component.GetType().GetField(this.fieldName.Value).SetValue(component, this.fieldValue.GetValue());
			return 2;
		}

		// Token: 0x0600703E RID: 28734 RVA: 0x002A92AC File Offset: 0x002A74AC
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.componentName = null;
			this.fieldName = null;
			this.fieldValue = null;
		}

		// Token: 0x04005C88 RID: 23688
		[Tooltip("The GameObject to set the field on")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005C89 RID: 23689
		[Tooltip("The component to set the field on")]
		public SharedString componentName;

		// Token: 0x04005C8A RID: 23690
		[Tooltip("The name of the field")]
		public SharedString fieldName;

		// Token: 0x04005C8B RID: 23691
		[Tooltip("The value to set")]
		public SharedVariable fieldValue;
	}
}
