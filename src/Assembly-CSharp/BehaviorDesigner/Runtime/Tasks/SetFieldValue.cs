using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x0200148C RID: 5260
	[TaskDescription("Sets the field to the value specified. Returns success if the field was set.")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=149")]
	[TaskCategory("Reflection")]
	[TaskIcon("{SkinColor}ReflectionIcon.png")]
	public class SetFieldValue : Action
	{
		// Token: 0x06007E37 RID: 32311 RVA: 0x002C8628 File Offset: 0x002C6828
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

		// Token: 0x06007E38 RID: 32312 RVA: 0x0005557D File Offset: 0x0005377D
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.componentName = null;
			this.fieldName = null;
			this.fieldValue = null;
		}

		// Token: 0x04006B80 RID: 27520
		[Tooltip("The GameObject to set the field on")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006B81 RID: 27521
		[Tooltip("The component to set the field on")]
		public SharedString componentName;

		// Token: 0x04006B82 RID: 27522
		[Tooltip("The name of the field")]
		public SharedString fieldName;

		// Token: 0x04006B83 RID: 27523
		[Tooltip("The value to set")]
		public SharedVariable fieldValue;
	}
}
