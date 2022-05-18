using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x0200148B RID: 5259
	[TaskDescription("Invokes the specified method with the specified parameters. Can optionally store the return value. Returns success if the method was invoked.")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=145")]
	[TaskCategory("Reflection")]
	[TaskIcon("{SkinColor}ReflectionIcon.png")]
	public class InvokeMethod : Action
	{
		// Token: 0x06007E34 RID: 32308 RVA: 0x002C84C8 File Offset: 0x002C66C8
		public override TaskStatus OnUpdate()
		{
			Type typeWithinAssembly = TaskUtility.GetTypeWithinAssembly(this.componentName.Value);
			if (typeWithinAssembly == null)
			{
				Debug.LogWarning("Unable to invoke - type is null");
				return 1;
			}
			Component component = base.GetDefaultGameObject(this.targetGameObject.Value).GetComponent(typeWithinAssembly);
			if (component == null)
			{
				Debug.LogWarning("Unable to invoke method with component " + this.componentName.Value);
				return 1;
			}
			List<object> list = new List<object>();
			List<Type> list2 = new List<Type>();
			int num = 0;
			SharedVariable sharedVariable;
			while (num < 4 && (sharedVariable = (base.GetType().GetField("parameter" + (num + 1)).GetValue(this) as SharedVariable)) != null)
			{
				list.Add(sharedVariable.GetValue());
				list2.Add(sharedVariable.GetType().GetProperty("Value").PropertyType);
				num++;
			}
			MethodInfo method = component.GetType().GetMethod(this.methodName.Value, list2.ToArray());
			if (method == null)
			{
				Debug.LogWarning("Unable to invoke method " + this.methodName.Value + " on component " + this.componentName.Value);
				return 1;
			}
			object value = method.Invoke(component, list.ToArray());
			if (this.storeResult != null)
			{
				this.storeResult.SetValue(value);
			}
			return 2;
		}

		// Token: 0x06007E35 RID: 32309 RVA: 0x00055543 File Offset: 0x00053743
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.componentName = null;
			this.methodName = null;
			this.parameter1 = null;
			this.parameter2 = null;
			this.parameter3 = null;
			this.parameter4 = null;
			this.storeResult = null;
		}

		// Token: 0x04006B78 RID: 27512
		[Tooltip("The GameObject to invoke the method on")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006B79 RID: 27513
		[Tooltip("The component to invoke the method on")]
		public SharedString componentName;

		// Token: 0x04006B7A RID: 27514
		[Tooltip("The name of the method")]
		public SharedString methodName;

		// Token: 0x04006B7B RID: 27515
		[Tooltip("The first parameter of the method")]
		public SharedVariable parameter1;

		// Token: 0x04006B7C RID: 27516
		[Tooltip("The second parameter of the method")]
		public SharedVariable parameter2;

		// Token: 0x04006B7D RID: 27517
		[Tooltip("The third parameter of the method")]
		public SharedVariable parameter3;

		// Token: 0x04006B7E RID: 27518
		[Tooltip("The fourth parameter of the method")]
		public SharedVariable parameter4;

		// Token: 0x04006B7F RID: 27519
		[Tooltip("Store the result of the invoke call")]
		public SharedVariable storeResult;
	}
}
