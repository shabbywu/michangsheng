using System;
using System.Reflection;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x02000FD1 RID: 4049
	[TaskDescription("Gets the value from the field specified. Returns success if the field was retrieved.")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=147")]
	[TaskCategory("Reflection")]
	[TaskIcon("{SkinColor}ReflectionIcon.png")]
	public class GetFieldValue : Action
	{
		// Token: 0x06007034 RID: 28724 RVA: 0x002A8EC8 File Offset: 0x002A70C8
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
				Debug.LogWarning("Unable to get field - type is null");
				return 1;
			}
			Component component = base.GetDefaultGameObject(this.targetGameObject.Value).GetComponent(typeWithinAssembly);
			if (component == null)
			{
				Debug.LogWarning("Unable to get the field with component " + this.componentName.Value);
				return 1;
			}
			FieldInfo field = component.GetType().GetField(this.fieldName.Value);
			this.fieldValue.SetValue(field.GetValue(component));
			return 2;
		}

		// Token: 0x06007035 RID: 28725 RVA: 0x002A8F76 File Offset: 0x002A7176
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.componentName = null;
			this.fieldName = null;
			this.fieldValue = null;
		}

		// Token: 0x04005C78 RID: 23672
		[Tooltip("The GameObject to get the field on")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005C79 RID: 23673
		[Tooltip("The component to get the field on")]
		public SharedString componentName;

		// Token: 0x04005C7A RID: 23674
		[Tooltip("The name of the field")]
		public SharedString fieldName;

		// Token: 0x04005C7B RID: 23675
		[Tooltip("The value of the field")]
		[RequiredField]
		public SharedVariable fieldValue;
	}
}
