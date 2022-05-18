using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200124E RID: 4686
	[Serializable]
	public class MenuSetVar
	{
		// Token: 0x060071DD RID: 29149 RVA: 0x002A6F14 File Offset: 0x002A5114
		public void setValue()
		{
			Type type = this.variable.GetType();
			if (type == typeof(BooleanVariable))
			{
				VariableBase<bool> variableBase = this.variable as BooleanVariable;
				BooleanVariable booleanVariable = this.ToVariable as BooleanVariable;
				variableBase.Value = booleanVariable.Value;
				return;
			}
			if (type == typeof(IntegerVariable))
			{
				VariableBase<int> variableBase2 = this.variable as IntegerVariable;
				IntegerVariable integerVariable = this.ToVariable as IntegerVariable;
				variableBase2.Value = integerVariable.Value;
				return;
			}
			if (type == typeof(FloatVariable))
			{
				VariableBase<float> variableBase3 = this.variable as FloatVariable;
				FloatVariable floatVariable = this.ToVariable as FloatVariable;
				variableBase3.Value = floatVariable.Value;
			}
		}

		// Token: 0x04006441 RID: 25665
		[Tooltip("The variable whos value will be set")]
		[VariableProperty(new Type[]
		{
			typeof(BooleanVariable),
			typeof(IntegerVariable),
			typeof(FloatVariable)
		})]
		[SerializeField]
		protected Variable variable;

		// Token: 0x04006442 RID: 25666
		[Tooltip("设置的值")]
		[VariableProperty(new Type[]
		{
			typeof(BooleanVariable),
			typeof(IntegerVariable),
			typeof(FloatVariable)
		})]
		[SerializeField]
		protected Variable ToVariable;
	}
}
