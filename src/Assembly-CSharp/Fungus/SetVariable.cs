using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001296 RID: 4758
	[CommandInfo("Variable", "Set Variable", "Sets a Boolean, Integer, Float or String variable to a new value using a simple arithmetic operation. The value can be a constant or reference another variable of the same type.", 0)]
	[AddComponentMenu("")]
	public class SetVariable : Command
	{
		// Token: 0x06007357 RID: 29527 RVA: 0x002AA0EC File Offset: 0x002A82EC
		protected virtual void DoSetOperation()
		{
			if (this.variable == null)
			{
				return;
			}
			Type type = this.variable.GetType();
			if (type == typeof(BooleanVariable))
			{
				(this.variable as BooleanVariable).Apply(this.setOperator, this.booleanData.Value);
				return;
			}
			if (type == typeof(IntegerVariable))
			{
				(this.variable as IntegerVariable).Apply(this.setOperator, this.integerData.Value);
				return;
			}
			if (type == typeof(FloatVariable))
			{
				(this.variable as FloatVariable).Apply(this.setOperator, this.floatData.Value);
				return;
			}
			if (type == typeof(StringVariable))
			{
				VariableBase<string> variableBase = this.variable as StringVariable;
				Flowchart flowchart = this.GetFlowchart();
				variableBase.Apply(this.setOperator, flowchart.SubstituteVariables(this.stringData.Value));
				return;
			}
			if (type == typeof(AnimatorVariable))
			{
				(this.variable as AnimatorVariable).Apply(this.setOperator, this.animatorData.Value);
				return;
			}
			if (type == typeof(AudioSourceVariable))
			{
				(this.variable as AudioSourceVariable).Apply(this.setOperator, this.audioSourceData.Value);
				return;
			}
			if (type == typeof(ColorVariable))
			{
				(this.variable as ColorVariable).Apply(this.setOperator, this.colorData.Value);
				return;
			}
			if (type == typeof(GameObjectVariable))
			{
				(this.variable as GameObjectVariable).Apply(this.setOperator, this.gameObjectData.Value);
				return;
			}
			if (type == typeof(MaterialVariable))
			{
				(this.variable as MaterialVariable).Apply(this.setOperator, this.materialData.Value);
				return;
			}
			if (type == typeof(ObjectVariable))
			{
				(this.variable as ObjectVariable).Apply(this.setOperator, this.objectData.Value);
				return;
			}
			if (type == typeof(Rigidbody2DVariable))
			{
				(this.variable as Rigidbody2DVariable).Apply(this.setOperator, this.rigidbody2DData.Value);
				return;
			}
			if (type == typeof(SpriteVariable))
			{
				(this.variable as SpriteVariable).Apply(this.setOperator, this.spriteData.Value);
				return;
			}
			if (type == typeof(TextureVariable))
			{
				(this.variable as TextureVariable).Apply(this.setOperator, this.textureData.Value);
				return;
			}
			if (type == typeof(TransformVariable))
			{
				(this.variable as TransformVariable).Apply(this.setOperator, this.transformData.Value);
				return;
			}
			if (type == typeof(Vector2Variable))
			{
				(this.variable as Vector2Variable).Apply(this.setOperator, this.vector2Data.Value);
				return;
			}
			if (type == typeof(Vector3Variable))
			{
				(this.variable as Vector3Variable).Apply(this.setOperator, this.vector3Data.Value);
			}
		}

		// Token: 0x17000A85 RID: 2693
		// (get) Token: 0x06007358 RID: 29528 RVA: 0x0004EB58 File Offset: 0x0004CD58
		public virtual SetOperator _SetOperator
		{
			get
			{
				return this.setOperator;
			}
		}

		// Token: 0x06007359 RID: 29529 RVA: 0x0004EB60 File Offset: 0x0004CD60
		public override void OnEnter()
		{
			this.DoSetOperation();
			this.Continue();
		}

		// Token: 0x0600735A RID: 29530 RVA: 0x002AA460 File Offset: 0x002A8660
		public override string GetSummary()
		{
			if (this.variable == null)
			{
				return "Error: Variable not selected";
			}
			string text = this.variable.Key;
			switch (this.setOperator)
			{
			default:
				text += " = ";
				break;
			case SetOperator.Negate:
				text += " =! ";
				break;
			case SetOperator.Add:
				text += " += ";
				break;
			case SetOperator.Subtract:
				text += " -= ";
				break;
			case SetOperator.Multiply:
				text += " *= ";
				break;
			case SetOperator.Divide:
				text += " /= ";
				break;
			case SetOperator.Remainder:
				text += " %= ";
				break;
			}
			Type type = this.variable.GetType();
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

		// Token: 0x0600735B RID: 29531 RVA: 0x002AA7A8 File Offset: 0x002A89A8
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

		// Token: 0x0600735C RID: 29532 RVA: 0x0004C5A3 File Offset: 0x0004A7A3
		public override Color GetButtonColor()
		{
			return new Color32(253, 253, 150, byte.MaxValue);
		}

		// Token: 0x04006535 RID: 25909
		[Tooltip("The variable whos value will be set")]
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

		// Token: 0x04006536 RID: 25910
		[Tooltip("The type of math operation to be performed")]
		[SerializeField]
		protected SetOperator setOperator;

		// Token: 0x04006537 RID: 25911
		[Tooltip("Boolean value to set with")]
		[SerializeField]
		protected BooleanData booleanData;

		// Token: 0x04006538 RID: 25912
		[Tooltip("Integer value to set with")]
		[SerializeField]
		protected IntegerData integerData;

		// Token: 0x04006539 RID: 25913
		[Tooltip("Float value to set with")]
		[SerializeField]
		protected FloatData floatData;

		// Token: 0x0400653A RID: 25914
		[Tooltip("String value to set with")]
		[SerializeField]
		protected StringDataMulti stringData;

		// Token: 0x0400653B RID: 25915
		[Tooltip("Animator value to set with")]
		[SerializeField]
		protected AnimatorData animatorData;

		// Token: 0x0400653C RID: 25916
		[Tooltip("AudioSource value to set with")]
		[SerializeField]
		protected AudioSourceData audioSourceData;

		// Token: 0x0400653D RID: 25917
		[Tooltip("Color value to set with")]
		[SerializeField]
		protected ColorData colorData;

		// Token: 0x0400653E RID: 25918
		[Tooltip("GameObject value to set with")]
		[SerializeField]
		protected GameObjectData gameObjectData;

		// Token: 0x0400653F RID: 25919
		[Tooltip("Material value to set with")]
		[SerializeField]
		protected MaterialData materialData;

		// Token: 0x04006540 RID: 25920
		[Tooltip("Object value to set with")]
		[SerializeField]
		protected ObjectData objectData;

		// Token: 0x04006541 RID: 25921
		[Tooltip("Rigidbody2D value to set with")]
		[SerializeField]
		protected Rigidbody2DData rigidbody2DData;

		// Token: 0x04006542 RID: 25922
		[Tooltip("Sprite value to set with")]
		[SerializeField]
		protected SpriteData spriteData;

		// Token: 0x04006543 RID: 25923
		[Tooltip("Texture value to set with")]
		[SerializeField]
		protected TextureData textureData;

		// Token: 0x04006544 RID: 25924
		[Tooltip("Transform value to set with")]
		[SerializeField]
		protected TransformData transformData;

		// Token: 0x04006545 RID: 25925
		[Tooltip("Vector2 value to set with")]
		[SerializeField]
		protected Vector2Data vector2Data;

		// Token: 0x04006546 RID: 25926
		[Tooltip("Vector3 value to set with")]
		[SerializeField]
		protected Vector3Data vector3Data;

		// Token: 0x04006547 RID: 25927
		public static readonly Dictionary<Type, SetOperator[]> operatorsByVariableType = new Dictionary<Type, SetOperator[]>
		{
			{
				typeof(BooleanVariable),
				BooleanVariable.setOperators
			},
			{
				typeof(IntegerVariable),
				IntegerVariable.setOperators
			},
			{
				typeof(FloatVariable),
				FloatVariable.setOperators
			},
			{
				typeof(StringVariable),
				StringVariable.setOperators
			},
			{
				typeof(AnimatorVariable),
				AnimatorVariable.setOperators
			},
			{
				typeof(AudioSourceVariable),
				AudioSourceVariable.setOperators
			},
			{
				typeof(ColorVariable),
				ColorVariable.setOperators
			},
			{
				typeof(GameObjectVariable),
				GameObjectVariable.setOperators
			},
			{
				typeof(MaterialVariable),
				MaterialVariable.setOperators
			},
			{
				typeof(ObjectVariable),
				ObjectVariable.setOperators
			},
			{
				typeof(Rigidbody2DVariable),
				Rigidbody2DVariable.setOperators
			},
			{
				typeof(SpriteVariable),
				SpriteVariable.setOperators
			},
			{
				typeof(TextureVariable),
				TextureVariable.setOperators
			},
			{
				typeof(TransformVariable),
				TransformVariable.setOperators
			},
			{
				typeof(Vector2Variable),
				Vector2Variable.setOperators
			},
			{
				typeof(Vector3Variable),
				Vector3Variable.setOperators
			}
		};
	}
}
