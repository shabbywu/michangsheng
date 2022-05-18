using System;
using System.Reflection;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x02001489 RID: 5257
	[TaskDescription("Gets the value from the field specified. Returns success if the field was retrieved.")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=147")]
	[TaskCategory("Reflection")]
	[TaskIcon("{SkinColor}ReflectionIcon.png")]
	public class GetFieldValue : Action
	{
		// Token: 0x06007E2E RID: 32302 RVA: 0x002C8368 File Offset: 0x002C6568
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

		// Token: 0x06007E2F RID: 32303 RVA: 0x00055507 File Offset: 0x00053707
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.componentName = null;
			this.fieldName = null;
			this.fieldValue = null;
		}

		// Token: 0x04006B70 RID: 27504
		[Tooltip("The GameObject to get the field on")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006B71 RID: 27505
		[Tooltip("The component to get the field on")]
		public SharedString componentName;

		// Token: 0x04006B72 RID: 27506
		[Tooltip("The name of the field")]
		public SharedString fieldName;

		// Token: 0x04006B73 RID: 27507
		[Tooltip("The value of the field")]
		[RequiredField]
		public SharedVariable fieldValue;
	}
}
