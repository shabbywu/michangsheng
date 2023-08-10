using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus;

public abstract class VariableCondition : Condition, INoCommand
{
	[Tooltip("The type of comparison to be performed")]
	[SerializeField]
	protected CompareOperator compareOperator;

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

	[Tooltip("Boolean value to compare against")]
	[SerializeField]
	protected BooleanData booleanData;

	[Tooltip("Integer value to compare against")]
	[SerializeField]
	protected IntegerData integerData;

	[Tooltip("Float value to compare against")]
	[SerializeField]
	protected FloatData floatData;

	[Tooltip("String value to compare against")]
	[SerializeField]
	protected StringDataMulti stringData;

	[Tooltip("Animator value to compare against")]
	[SerializeField]
	protected AnimatorData animatorData;

	[Tooltip("AudioSource value to compare against")]
	[SerializeField]
	protected AudioSourceData audioSourceData;

	[Tooltip("Color value to compare against")]
	[SerializeField]
	protected ColorData colorData;

	[Tooltip("GameObject value to compare against")]
	[SerializeField]
	protected GameObjectData gameObjectData;

	[Tooltip("Material value to compare against")]
	[SerializeField]
	protected MaterialData materialData;

	[Tooltip("Object value to compare against")]
	[SerializeField]
	protected ObjectData objectData;

	[Tooltip("Rigidbody2D value to compare against")]
	[SerializeField]
	protected Rigidbody2DData rigidbody2DData;

	[Tooltip("Sprite value to compare against")]
	[SerializeField]
	protected SpriteData spriteData;

	[Tooltip("Texture value to compare against")]
	[SerializeField]
	protected TextureData textureData;

	[Tooltip("Transform value to compare against")]
	[SerializeField]
	protected TransformData transformData;

	[Tooltip("Vector2 value to compare against")]
	[SerializeField]
	protected Vector2Data vector2Data;

	[Tooltip("Vector3 value to compare against")]
	[SerializeField]
	protected Vector3Data vector3Data;

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

	public virtual CompareOperator _CompareOperator => compareOperator;

	protected override bool EvaluateCondition()
	{
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0362: Unknown result type (might be due to invalid IL or missing references)
		//IL_0398: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)variable == (Object)null)
		{
			return false;
		}
		bool result = false;
		Type type = ((object)variable).GetType();
		if (type == typeof(BooleanVariable))
		{
			result = (variable as BooleanVariable).Evaluate(compareOperator, booleanData.Value);
		}
		else if (type == typeof(IntegerVariable))
		{
			result = (variable as IntegerVariable).Evaluate(compareOperator, integerData.Value);
		}
		else if (type == typeof(FloatVariable))
		{
			result = (variable as FloatVariable).Evaluate(compareOperator, floatData.Value);
		}
		else if (type == typeof(StringVariable))
		{
			result = (variable as StringVariable).Evaluate(compareOperator, stringData.Value);
		}
		else if (type == typeof(AnimatorVariable))
		{
			result = (variable as AnimatorVariable).Evaluate(compareOperator, animatorData.Value);
		}
		else if (type == typeof(AudioSourceVariable))
		{
			result = (variable as AudioSourceVariable).Evaluate(compareOperator, audioSourceData.Value);
		}
		else if (type == typeof(ColorVariable))
		{
			result = (variable as ColorVariable).Evaluate(compareOperator, colorData.Value);
		}
		else if (type == typeof(GameObjectVariable))
		{
			result = (variable as GameObjectVariable).Evaluate(compareOperator, gameObjectData.Value);
		}
		else if (type == typeof(MaterialVariable))
		{
			result = (variable as MaterialVariable).Evaluate(compareOperator, materialData.Value);
		}
		else if (type == typeof(ObjectVariable))
		{
			result = (variable as ObjectVariable).Evaluate(compareOperator, objectData.Value);
		}
		else if (type == typeof(Rigidbody2DVariable))
		{
			result = (variable as Rigidbody2DVariable).Evaluate(compareOperator, rigidbody2DData.Value);
		}
		else if (type == typeof(SpriteVariable))
		{
			result = (variable as SpriteVariable).Evaluate(compareOperator, spriteData.Value);
		}
		else if (type == typeof(TextureVariable))
		{
			result = (variable as TextureVariable).Evaluate(compareOperator, textureData.Value);
		}
		else if (type == typeof(TransformVariable))
		{
			result = (variable as TransformVariable).Evaluate(compareOperator, transformData.Value);
		}
		else if (type == typeof(Vector2Variable))
		{
			result = (variable as Vector2Variable).Evaluate(compareOperator, vector2Data.Value);
		}
		else if (type == typeof(Vector3Variable))
		{
			result = (variable as Vector3Variable).Evaluate(compareOperator, vector3Data.Value);
		}
		return result;
	}

	protected override bool HasNeededProperties()
	{
		return (Object)(object)variable != (Object)null;
	}

	public override string GetSummary()
	{
		if ((Object)(object)variable == (Object)null)
		{
			return "Error: No variable selected";
		}
		Type type = ((object)variable).GetType();
		string text = variable.Key + " ";
		text = text + Condition.GetOperatorDescription(compareOperator) + " ";
		if (type == typeof(BooleanVariable))
		{
			text += booleanData.GetDescription();
		}
		else if (type == typeof(IntegerVariable))
		{
			text += integerData.GetDescription();
		}
		else if (type == typeof(FloatVariable))
		{
			text += floatData.GetDescription();
		}
		else if (type == typeof(StringVariable))
		{
			text += stringData.GetDescription();
		}
		else if (type == typeof(AnimatorVariable))
		{
			text += animatorData.GetDescription();
		}
		else if (type == typeof(AudioSourceVariable))
		{
			text += audioSourceData.GetDescription();
		}
		else if (type == typeof(ColorVariable))
		{
			text += colorData.GetDescription();
		}
		else if (type == typeof(GameObjectVariable))
		{
			text += gameObjectData.GetDescription();
		}
		else if (type == typeof(MaterialVariable))
		{
			text += materialData.GetDescription();
		}
		else if (type == typeof(ObjectVariable))
		{
			text += objectData.GetDescription();
		}
		else if (type == typeof(Rigidbody2DVariable))
		{
			text += rigidbody2DData.GetDescription();
		}
		else if (type == typeof(SpriteVariable))
		{
			text += spriteData.GetDescription();
		}
		else if (type == typeof(TextureVariable))
		{
			text += textureData.GetDescription();
		}
		else if (type == typeof(TransformVariable))
		{
			text += transformData.GetDescription();
		}
		else if (type == typeof(Vector2Variable))
		{
			text += vector2Data.GetDescription();
		}
		else if (type == typeof(Vector3Variable))
		{
			text += vector3Data.GetDescription();
		}
		return text;
	}

	public override bool HasReference(Variable variable)
	{
		bool flag = (Object)(object)variable == (Object)(object)this.variable || base.HasReference(variable);
		Type type = ((object)variable).GetType();
		if (type == typeof(BooleanVariable))
		{
			flag |= (Object)(object)booleanData.booleanRef == (Object)(object)variable;
		}
		else if (type == typeof(IntegerVariable))
		{
			flag |= (Object)(object)integerData.integerRef == (Object)(object)variable;
		}
		else if (type == typeof(FloatVariable))
		{
			flag |= (Object)(object)floatData.floatRef == (Object)(object)variable;
		}
		else if (type == typeof(StringVariable))
		{
			flag |= (Object)(object)stringData.stringRef == (Object)(object)variable;
		}
		else if (type == typeof(AnimatorVariable))
		{
			flag |= (Object)(object)animatorData.animatorRef == (Object)(object)variable;
		}
		else if (type == typeof(AudioSourceVariable))
		{
			flag |= (Object)(object)audioSourceData.audioSourceRef == (Object)(object)variable;
		}
		else if (type == typeof(ColorVariable))
		{
			flag |= (Object)(object)colorData.colorRef == (Object)(object)variable;
		}
		else if (type == typeof(GameObjectVariable))
		{
			flag |= (Object)(object)gameObjectData.gameObjectRef == (Object)(object)variable;
		}
		else if (type == typeof(MaterialVariable))
		{
			flag |= (Object)(object)materialData.materialRef == (Object)(object)variable;
		}
		else if (type == typeof(ObjectVariable))
		{
			flag |= (Object)(object)objectData.objectRef == (Object)(object)variable;
		}
		else if (type == typeof(Rigidbody2DVariable))
		{
			flag |= (Object)(object)rigidbody2DData.rigidbody2DRef == (Object)(object)variable;
		}
		else if (type == typeof(SpriteVariable))
		{
			flag |= (Object)(object)spriteData.spriteRef == (Object)(object)variable;
		}
		else if (type == typeof(TextureVariable))
		{
			flag |= (Object)(object)textureData.textureRef == (Object)(object)variable;
		}
		else if (type == typeof(TransformVariable))
		{
			flag |= (Object)(object)transformData.transformRef == (Object)(object)variable;
		}
		else if (type == typeof(Vector2Variable))
		{
			flag |= (Object)(object)vector2Data.vector2Ref == (Object)(object)variable;
		}
		else if (type == typeof(Vector3Variable))
		{
			flag |= (Object)(object)vector3Data.vector3Ref == (Object)(object)variable;
		}
		return flag;
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)253, (byte)253, (byte)150, byte.MaxValue));
	}
}
