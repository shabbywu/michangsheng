using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001214 RID: 4628
	[CommandInfo("Input", "GetKey", "Store Input.GetKey in a variable. Supports an optional Negative key input. A negative value will be overridden by a positive one, they do not add.", 0)]
	[AddComponentMenu("")]
	public class GetKey : Command
	{
		// Token: 0x06007129 RID: 28969 RVA: 0x002A4578 File Offset: 0x002A2778
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

		// Token: 0x0600712A RID: 28970 RVA: 0x002A4608 File Offset: 0x002A2808
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

		// Token: 0x0600712B RID: 28971 RVA: 0x002A4660 File Offset: 0x002A2860
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

		// Token: 0x0600712C RID: 28972 RVA: 0x002A46B8 File Offset: 0x002A28B8
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

		// Token: 0x0600712D RID: 28973 RVA: 0x002A4724 File Offset: 0x002A2924
		public override string GetSummary()
		{
			if (this.outValue == null)
			{
				return "Error: no outvalue set";
			}
			return ((this.keyCode != null) ? this.keyCode.ToString() : this.keyCodeName) + " in " + this.outValue.Key;
		}

		// Token: 0x0600712E RID: 28974 RVA: 0x0004C5E0 File Offset: 0x0004A7E0
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x0600712F RID: 28975 RVA: 0x0004CE3E File Offset: 0x0004B03E
		public override bool HasReference(Variable variable)
		{
			return this.keyCodeName.stringRef == variable || this.outValue == variable || this.keyCodeNameNegative.stringRef == variable;
		}

		// Token: 0x04006375 RID: 25461
		[SerializeField]
		protected KeyCode keyCode;

		// Token: 0x04006376 RID: 25462
		[Tooltip("Optional, secondary or negative keycode. For booleans will also set to true, for int and float will set to -1.")]
		[SerializeField]
		protected KeyCode keyCodeNegative;

		// Token: 0x04006377 RID: 25463
		[SerializeField]
		[Tooltip("Only used if KeyCode is KeyCode.None, expects a name of the key to use.")]
		protected StringData keyCodeName = new StringData(string.Empty);

		// Token: 0x04006378 RID: 25464
		[SerializeField]
		[Tooltip("Optional, secondary or negative keycode. For booleans will also set to true, for int and float will set to -1.Only used if KeyCode is KeyCode.None, expects a name of the key to use.")]
		protected StringData keyCodeNameNegative = new StringData(string.Empty);

		// Token: 0x04006379 RID: 25465
		[Tooltip("Do we want an Input.GetKeyDown, GetKeyUp or GetKey")]
		[SerializeField]
		protected GetKey.InputKeyQueryType keyQueryType = GetKey.InputKeyQueryType.State;

		// Token: 0x0400637A RID: 25466
		[Tooltip("Will store true or false or 0 or 1 depending on type. Sets true or -1 for negative key values.")]
		[SerializeField]
		[VariableProperty(new Type[]
		{
			typeof(FloatVariable),
			typeof(BooleanVariable),
			typeof(IntegerVariable)
		})]
		protected Variable outValue;

		// Token: 0x02001215 RID: 4629
		public enum InputKeyQueryType
		{
			// Token: 0x0400637C RID: 25468
			Down,
			// Token: 0x0400637D RID: 25469
			Up,
			// Token: 0x0400637E RID: 25470
			State
		}
	}
}
