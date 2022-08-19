using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000DD9 RID: 3545
	[CommandInfo("Input", "GetKey", "Store Input.GetKey in a variable. Supports an optional Negative key input. A negative value will be overridden by a positive one, they do not add.", 0)]
	[AddComponentMenu("")]
	public class GetKey : Command
	{
		// Token: 0x060064A7 RID: 25767 RVA: 0x0027F980 File Offset: 0x0027DB80
		public override void OnEnter()
		{
			this.FillOutValue(0);
			if (this.keyCodeNegative != null)
			{
				this.DoKeyCode(this.keyCodeNegative, -1);
			}
			else if (!string.IsNullOrEmpty(this.keyCodeNameNegative))
			{
				this.DoKeyName(this.keyCodeNameNegative, -1);
			}
			if (this.keyCode != null)
			{
				this.DoKeyCode(this.keyCode, 1);
			}
			else if (!string.IsNullOrEmpty(this.keyCodeName))
			{
				this.DoKeyName(this.keyCodeName, 1);
			}
			this.Continue();
		}

		// Token: 0x060064A8 RID: 25768 RVA: 0x0027FA10 File Offset: 0x0027DC10
		private void DoKeyCode(KeyCode key, int trueVal)
		{
			switch (this.keyQueryType)
			{
			case GetKey.InputKeyQueryType.Down:
				if (Input.GetKeyDown(key))
				{
					this.FillOutValue(trueVal);
					return;
				}
				break;
			case GetKey.InputKeyQueryType.Up:
				if (Input.GetKeyUp(key))
				{
					this.FillOutValue(trueVal);
					return;
				}
				break;
			case GetKey.InputKeyQueryType.State:
				if (Input.GetKey(key))
				{
					this.FillOutValue(trueVal);
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x060064A9 RID: 25769 RVA: 0x0027FA68 File Offset: 0x0027DC68
		private void DoKeyName(string key, int trueVal)
		{
			switch (this.keyQueryType)
			{
			case GetKey.InputKeyQueryType.Down:
				if (Input.GetKeyDown(key))
				{
					this.FillOutValue(trueVal);
					return;
				}
				break;
			case GetKey.InputKeyQueryType.Up:
				if (Input.GetKeyUp(key))
				{
					this.FillOutValue(trueVal);
					return;
				}
				break;
			case GetKey.InputKeyQueryType.State:
				if (Input.GetKey(key))
				{
					this.FillOutValue(trueVal);
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x060064AA RID: 25770 RVA: 0x0027FAC0 File Offset: 0x0027DCC0
		private void FillOutValue(int v)
		{
			FloatVariable floatVariable = this.outValue as FloatVariable;
			if (floatVariable != null)
			{
				floatVariable.Value = (float)v;
				return;
			}
			BooleanVariable booleanVariable = this.outValue as BooleanVariable;
			if (booleanVariable != null)
			{
				booleanVariable.Value = (v != 0);
				return;
			}
			IntegerVariable integerVariable = this.outValue as IntegerVariable;
			if (integerVariable != null)
			{
				integerVariable.Value = v;
				return;
			}
		}

		// Token: 0x060064AB RID: 25771 RVA: 0x0027FB2C File Offset: 0x0027DD2C
		public override string GetSummary()
		{
			if (this.outValue == null)
			{
				return "Error: no outvalue set";
			}
			return ((this.keyCode != null) ? this.keyCode.ToString() : this.keyCodeName) + " in " + this.outValue.Key;
		}

		// Token: 0x060064AC RID: 25772 RVA: 0x0027D3DB File Offset: 0x0027B5DB
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x060064AD RID: 25773 RVA: 0x0027FB88 File Offset: 0x0027DD88
		public override bool HasReference(Variable variable)
		{
			return this.keyCodeName.stringRef == variable || this.outValue == variable || this.keyCodeNameNegative.stringRef == variable;
		}

		// Token: 0x04005675 RID: 22133
		[SerializeField]
		protected KeyCode keyCode;

		// Token: 0x04005676 RID: 22134
		[Tooltip("Optional, secondary or negative keycode. For booleans will also set to true, for int and float will set to -1.")]
		[SerializeField]
		protected KeyCode keyCodeNegative;

		// Token: 0x04005677 RID: 22135
		[SerializeField]
		[Tooltip("Only used if KeyCode is KeyCode.None, expects a name of the key to use.")]
		protected StringData keyCodeName = new StringData(string.Empty);

		// Token: 0x04005678 RID: 22136
		[SerializeField]
		[Tooltip("Optional, secondary or negative keycode. For booleans will also set to true, for int and float will set to -1.Only used if KeyCode is KeyCode.None, expects a name of the key to use.")]
		protected StringData keyCodeNameNegative = new StringData(string.Empty);

		// Token: 0x04005679 RID: 22137
		[Tooltip("Do we want an Input.GetKeyDown, GetKeyUp or GetKey")]
		[SerializeField]
		protected GetKey.InputKeyQueryType keyQueryType = GetKey.InputKeyQueryType.State;

		// Token: 0x0400567A RID: 22138
		[Tooltip("Will store true or false or 0 or 1 depending on type. Sets true or -1 for negative key values.")]
		[SerializeField]
		[VariableProperty(new Type[]
		{
			typeof(FloatVariable),
			typeof(BooleanVariable),
			typeof(IntegerVariable)
		})]
		protected Variable outValue;

		// Token: 0x020016AF RID: 5807
		public enum InputKeyQueryType
		{
			// Token: 0x0400735C RID: 29532
			Down,
			// Token: 0x0400735D RID: 29533
			Up,
			// Token: 0x0400735E RID: 29534
			State
		}
	}
}
