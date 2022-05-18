using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020012AB RID: 4779
	public abstract class VariableCondition : Condition, INoCommand
	{
		// Token: 0x060073B3 RID: 29619 RVA: 0x002ABA70 File Offset: 0x002A9C70
		protected override bool EvaluateCondition()
		{
			if (this.variable == null)
			{
				return false;
			}
			bool result = false;
			Type type = this.variable.GetType();
			if (type == typeof(BooleanVariable))
			{
				result = (this.variable as BooleanVariable).Evaluate(this.compareOperator, this.booleanData.Value);
			}
			else if (type == typeof(IntegerVariable))
			{
				result = (this.variable as IntegerVariable).Evaluate(this.compareOperator, this.integerData.Value);
			}
			else if (type == typeof(FloatVariable))
			{
				result = (this.variable as FloatVariable).Evaluate(this.compareOperator, this.floatData.Value);
			}
			else if (type == typeof(StringVariable))
			{
				result = (this.variable as StringVariable).Evaluate(this.compareOperator, this.stringData.Value);
			}
			else if (type == typeof(AnimatorVariable))
			{
				result = (this.variable as AnimatorVariable).Evaluate(this.compareOperator, this.animatorData.Value);
			}
			else if (type == typeof(AudioSourceVariable))
			{
				result = (this.variable as AudioSourceVariable).Evaluate(this.compareOperator, this.audioSourceData.Value);
			}
			else if (type == typeof(ColorVariable))
			{
				result = (this.variable as ColorVariable).Evaluate(this.compareOperator, this.colorData.Value);
			}
			else if (type == typeof(GameObjectVariable))
			{
				result = (this.variable as GameObjectVariable).Evaluate(this.compareOperator, this.gameObjectData.Value);
			}
			else if (type == typeof(MaterialVariable))
			{
				result = (this.variable as MaterialVariable).Evaluate(this.compareOperator, this.materialData.Value);
			}
			else if (type == typeof(ObjectVariable))
			{
				result = (this.variable as ObjectVariable).Evaluate(this.compareOperator, this.objectData.Value);
			}
			else if (type == typeof(Rigidbody2DVariable))
			{
				result = (this.variable as Rigidbody2DVariable).Evaluate(this.compareOperator, this.rigidbody2DData.Value);
			}
			else if (type == typeof(SpriteVariable))
			{
				result = (this.variable as SpriteVariable).Evaluate(this.compareOperator, this.spriteData.Value);
			}
			else if (type == typeof(TextureVariable))
			{
				result = (this.variable as TextureVariable).Evaluate(this.compareOperator, this.textureData.Value);
			}
			else if (type == typeof(TransformVariable))
			{
				result = (this.variable as TransformVariable).Evaluate(this.compareOperator, this.transformData.Value);
			}
			else if (type == typeof(Vector2Variable))
			{
				result = (this.variable as Vector2Variable).Evaluate(this.compareOperator, this.vector2Data.Value);
			}
			else if (type == typeof(Vector3Variable))
			{
				result = (this.variable as Vector3Variable).Evaluate(this.compareOperator, this.vector3Data.Value);
			}
			return result;
		}

		// Token: 0x060073B4 RID: 29620 RVA: 0x0004EF81 File Offset: 0x0004D181
		protected override bool HasNeededProperties()
		{
			return this.variable != null;
		}

		// Token: 0x17000A86 RID: 2694
		// (get) Token: 0x060073B5 RID: 29621 RVA: 0x0004EF8F File Offset: 0x0004D18F
		public virtual CompareOperator _CompareOperator
		{
			get
			{
				return this.compareOperator;
			}
		}

		// Token: 0x060073B6 RID: 29622 RVA: 0x002ABE24 File Offset: 0x002AA024
		public override string GetSummary()
		{
			if (this.variable == null)
			{
				return "Error: No variable selected";
			}
			Type type = this.variable.GetType();
			string text = this.variable.Key + " ";
			text = text + Condition.GetOperatorDescription(this.compareOperator) + " ";
			if (type == typeof(BooleanVariable))
			{
				text += this.booleanData.GetDescription();
			}
			else if (type == typeof(IntegerVariable))
			{
				text += this.integerData.GetDescription();
			}
			else if (type == typeof(FloatVariable))
			{
				text += this.floatData.GetDescription();
			}
			else if (type == typeof(StringVariable))
			{
				text += this.stringData.GetDescription();
			}
			else if (type == typeof(AnimatorVariable))
			{
				text += this.animatorData.GetDescription();
			}
			else if (type == typeof(AudioSourceVariable))
			{
				text += this.audioSourceData.GetDescription();
			}
			else if (type == typeof(ColorVariable))
			{
				text += this.colorData.GetDescription();
			}
			else if (type == typeof(GameObjectVariable))
			{
				text += this.gameObjectData.GetDescription();
			}
			else if (type == typeof(MaterialVariable))
			{
				text += this.materialData.GetDescription();
			}
			else if (type == typeof(ObjectVariable))
			{
				text += this.objectData.GetDescription();
			}
			else if (type == typeof(Rigidbody2DVariable))
			{
				text += this.rigidbody2DData.GetDescription();
			}
			else if (type == typeof(SpriteVariable))
			{
				text += this.spriteData.GetDescription();
			}
			else if (type == typeof(TextureVariable))
			{
				text += this.textureData.GetDescription();
			}
			else if (type == typeof(TransformVariable))
			{
				text += this.transformData.GetDescription();
			}
			else if (type == typeof(Vector2Variable))
			{
				text += this.vector2Data.GetDescription();
			}
			else if (type == typeof(Vector3Variable))
			{
				text += this.vector3Data.GetDescription();
			}
			return text;
		}

		// Token: 0x060073B7 RID: 29623 RVA: 0x002AC104 File Offset: 0x002AA304
		public override bool HasReference(Variable variable)
		{
			bool flag = variable == this.variable || base.HasReference(variable);
			Type type = variable.GetType();
			if (type == typeof(BooleanVariable))
			{
				flag |= (this.booleanData.booleanRef == variable);
			}
			else if (type == typeof(IntegerVariable))
			{
				flag |= (this.integerData.integerRef == variable);
			}
			else if (type == typeof(FloatVariable))
			{
				flag |= (this.floatData.floatRef == variable);
			}
			else if (type == typeof(StringVariable))
			{
				flag |= (this.stringData.stringRef == variable);
			}
			else if (type == typeof(AnimatorVariable))
			{
				flag |= (this.animatorData.animatorRef == variable);
			}
			else if (type == typeof(AudioSourceVariable))
			{
				flag |= (this.audioSourceData.audioSourceRef == variable);
			}
			else if (type == typeof(ColorVariable))
			{
				flag |= (this.colorData.colorRef == variable);
			}
			else if (type == typeof(GameObjectVariable))
			{
				flag |= (this.gameObjectData.gameObjectRef == variable);
			}
			else if (type == typeof(MaterialVariable))
			{
				flag |= (this.materialData.materialRef == variable);
			}
			else if (type == typeof(ObjectVariable))
			{
				flag |= (this.objectData.objectRef == variable);
			}
			else if (type == typeof(Rigidbody2DVariable))
			{
				flag |= (this.rigidbody2DData.rigidbody2DRef == variable);
			}
			else if (type == typeof(SpriteVariable))
			{
				flag |= (this.spriteData.spriteRef == variable);
			}
			else if (type == typeof(TextureVariable))
			{
				flag |= (this.textureData.textureRef == variable);
			}
			else if (type == typeof(TransformVariable))
			{
				flag |= (this.transformData.transformRef == variable);
			}
			else if (type == typeof(Vector2Variable))
			{
				flag |= (this.vector2Data.vector2Ref == variable);
			}
			else if (type == typeof(Vector3Variable))
			{
				flag |= (this.vector3Data.vector3Ref == variable);
			}
			return flag;
		}

		// Token: 0x060073B8 RID: 29624 RVA: 0x0004C5A3 File Offset: 0x0004A7A3
		public override Color GetButtonColor()
		{
			return new Color32(253, 253, 150, byte.MaxValue);
		}

		// Token: 0x0400658A RID: 25994
		[Tooltip("The type of comparison to be performed")]
		[SerializeField]
		protected CompareOperator compareOperator;

		// Token: 0x0400658B RID: 25995
		[Tooltip("Variable to use in expression")]
		[VariableProperty(new Type[]
		{
			typeof(BooleanVariable),
			typeof(IntegerVariable),
			typeof(FloatVariable),
			typeof(StringVariable),
			typeof(AnimatorVariable),
			typeof(AudioSourceVariable),
			typeof(ColorVariable),
			typeof(GameObjectVariable),
			typeof(MaterialVariable),
			typeof(ObjectVariable),
			typeof(Rigidbody2DVariable),
			typeof(SpriteVariable),
			typeof(TextureVariable),
			typeof(TransformVariable),
			typeof(Vector2Variable),
			typeof(Vector3Variable)
		})]
		[SerializeField]
		protected Variable variable;

		// Token: 0x0400658C RID: 25996
		[Tooltip("Boolean value to compare against")]
		[SerializeField]
		protected BooleanData booleanData;

		// Token: 0x0400658D RID: 25997
		[Tooltip("Integer value to compare against")]
		[SerializeField]
		protected IntegerData integerData;

		// Token: 0x0400658E RID: 25998
		[Tooltip("Float value to compare against")]
		[SerializeField]
		protected FloatData floatData;

		// Token: 0x0400658F RID: 25999
		[Tooltip("String value to compare against")]
		[SerializeField]
		protected StringDataMulti stringData;

		// Token: 0x04006590 RID: 26000
		[Tooltip("Animator value to compare against")]
		[SerializeField]
		protected AnimatorData animatorData;

		// Token: 0x04006591 RID: 26001
		[Tooltip("AudioSource value to compare against")]
		[SerializeField]
		protected AudioSourceData audioSourceData;

		// Token: 0x04006592 RID: 26002
		[Tooltip("Color value to compare against")]
		[SerializeField]
		protected ColorData colorData;

		// Token: 0x04006593 RID: 26003
		[Tooltip("GameObject value to compare against")]
		[SerializeField]
		protected GameObjectData gameObjectData;

		// Token: 0x04006594 RID: 26004
		[Tooltip("Material value to compare against")]
		[SerializeField]
		protected MaterialData materialData;

		// Token: 0x04006595 RID: 26005
		[Tooltip("Object value to compare against")]
		[SerializeField]
		protected ObjectData objectData;

		// Token: 0x04006596 RID: 26006
		[Tooltip("Rigidbody2D value to compare against")]
		[SerializeField]
		protected Rigidbody2DData rigidbody2DData;

		// Token: 0x04006597 RID: 26007
		[Tooltip("Sprite value to compare against")]
		[SerializeField]
		protected SpriteData spriteData;

		// Token: 0x04006598 RID: 26008
		[Tooltip("Texture value to compare against")]
		[SerializeField]
		protected TextureData textureData;

		// Token: 0x04006599 RID: 26009
		[Tooltip("Transform value to compare against")]
		[SerializeField]
		protected TransformData transformData;

		// Token: 0x0400659A RID: 26010
		[Tooltip("Vector2 value to compare against")]
		[SerializeField]
		protected Vector2Data vector2Data;

		// Token: 0x0400659B RID: 26011
		[Tooltip("Vector3 value to compare against")]
		[SerializeField]
		protected Vector3Data vector3Data;

		// Token: 0x0400659C RID: 26012
		public static readonly Dictionary<Type, CompareOperator[]> operatorsByVariableType = new Dictionary<Type, CompareOperator[]>
		{
			{
				typeof(BooleanVariable),
				BooleanVariable.compareOperators
			},
			{
				typeof(IntegerVariable),
				IntegerVariable.compareOperators
			},
			{
				typeof(FloatVariable),
				FloatVariable.compareOperators
			},
			{
				typeof(StringVariable),
				StringVariable.compareOperators
			},
			{
				typeof(AnimatorVariable),
				AnimatorVariable.compareOperators
			},
			{
				typeof(AudioSourceVariable),
				AudioSourceVariable.compareOperators
			},
			{
				typeof(ColorVariable),
				ColorVariable.compareOperators
			},
			{
				typeof(GameObjectVariable),
				GameObjectVariable.compareOperators
			},
			{
				typeof(MaterialVariable),
				MaterialVariable.compareOperators
			},
			{
				typeof(ObjectVariable),
				ObjectVariable.compareOperators
			},
			{
				typeof(Rigidbody2DVariable),
				Rigidbody2DVariable.compareOperators
			},
			{
				typeof(SpriteVariable),
				SpriteVariable.compareOperators
			},
			{
				typeof(TextureVariable),
				TextureVariable.compareOperators
			},
			{
				typeof(TransformVariable),
				TransformVariable.compareOperators
			},
			{
				typeof(Vector2Variable),
				Vector2Variable.compareOperators
			},
			{
				typeof(Vector3Variable),
				Vector3Variable.compareOperators
			}
		};
	}
}
