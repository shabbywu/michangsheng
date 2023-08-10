using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus;

[CommandInfo("Variable", "Set Variable", "Sets a Boolean, Integer, Float or String variable to a new value using a simple arithmetic operation. The value can be a constant or reference another variable of the same type.", 0)]
[AddComponentMenu("")]
public class SetVariable : Command
{
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

	[Tooltip("The type of math operation to be performed")]
	[SerializeField]
	protected SetOperator setOperator;

	[Tooltip("Boolean value to set with")]
	[SerializeField]
	protected BooleanData booleanData;

	[Tooltip("Integer value to set with")]
	[SerializeField]
	protected IntegerData integerData;

	[Tooltip("Float value to set with")]
	[SerializeField]
	protected FloatData floatData;

	[Tooltip("String value to set with")]
	[SerializeField]
	protected StringDataMulti stringData;

	[Tooltip("Animator value to set with")]
	[SerializeField]
	protected AnimatorData animatorData;

	[Tooltip("AudioSource value to set with")]
	[SerializeField]
	protected AudioSourceData audioSourceData;

	[Tooltip("Color value to set with")]
	[SerializeField]
	protected ColorData colorData;

	[Tooltip("GameObject value to set with")]
	[SerializeField]
	protected GameObjectData gameObjectData;

	[Tooltip("Material value to set with")]
	[SerializeField]
	protected MaterialData materialData;

	[Tooltip("Object value to set with")]
	[SerializeField]
	protected ObjectData objectData;

	[Tooltip("Rigidbody2D value to set with")]
	[SerializeField]
	protected Rigidbody2DData rigidbody2DData;

	[Tooltip("Sprite value to set with")]
	[SerializeField]
	protected SpriteData spriteData;

	[Tooltip("Texture value to set with")]
	[SerializeField]
	protected TextureData textureData;

	[Tooltip("Transform value to set with")]
	[SerializeField]
	protected TransformData transformData;

	[Tooltip("Vector2 value to set with")]
	[SerializeField]
	protected Vector2Data vector2Data;

	[Tooltip("Vector3 value to set with")]
	[SerializeField]
	protected Vector3Data vector3Data;

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

	public virtual SetOperator _SetOperator => setOperator;

	protected virtual void DoSetOperation()
	{
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_0329: Unknown result type (might be due to invalid IL or missing references)
		//IL_035d: Unknown result type (might be due to invalid IL or missing references)
		if (!((Object)(object)variable == (Object)null))
		{
			Type type = ((object)variable).GetType();
			if (type == typeof(BooleanVariable))
			{
				(variable as BooleanVariable).Apply(setOperator, booleanData.Value);
			}
			else if (type == typeof(IntegerVariable))
			{
				(variable as IntegerVariable).Apply(setOperator, integerData.Value);
			}
			else if (type == typeof(FloatVariable))
			{
				(variable as FloatVariable).Apply(setOperator, floatData.Value);
			}
			else if (type == typeof(StringVariable))
			{
				(variable as StringVariable).Apply(value: GetFlowchart().SubstituteVariables(stringData.Value), setOperator: setOperator);
			}
			else if (type == typeof(AnimatorVariable))
			{
				(variable as AnimatorVariable).Apply(setOperator, animatorData.Value);
			}
			else if (type == typeof(AudioSourceVariable))
			{
				(variable as AudioSourceVariable).Apply(setOperator, audioSourceData.Value);
			}
			else if (type == typeof(ColorVariable))
			{
				(variable as ColorVariable).Apply(setOperator, colorData.Value);
			}
			else if (type == typeof(GameObjectVariable))
			{
				(variable as GameObjectVariable).Apply(setOperator, gameObjectData.Value);
			}
			else if (type == typeof(MaterialVariable))
			{
				(variable as MaterialVariable).Apply(setOperator, materialData.Value);
			}
			else if (type == typeof(ObjectVariable))
			{
				(variable as ObjectVariable).Apply(setOperator, objectData.Value);
			}
			else if (type == typeof(Rigidbody2DVariable))
			{
				(variable as Rigidbody2DVariable).Apply(setOperator, rigidbody2DData.Value);
			}
			else if (type == typeof(SpriteVariable))
			{
				(variable as SpriteVariable).Apply(setOperator, spriteData.Value);
			}
			else if (type == typeof(TextureVariable))
			{
				(variable as TextureVariable).Apply(setOperator, textureData.Value);
			}
			else if (type == typeof(TransformVariable))
			{
				(variable as TransformVariable).Apply(setOperator, transformData.Value);
			}
			else if (type == typeof(Vector2Variable))
			{
				(variable as Vector2Variable).Apply(setOperator, vector2Data.Value);
			}
			else if (type == typeof(Vector3Variable))
			{
				(variable as Vector3Variable).Apply(setOperator, vector3Data.Value);
			}
		}
	}

	public override void OnEnter()
	{
		DoSetOperation();
		Continue();
	}

	public override string GetSummary()
	{
		if ((Object)(object)variable == (Object)null)
		{
			return "Error: Variable not selected";
		}
		string key = variable.Key;
		key = setOperator switch
		{
			SetOperator.Negate => key + " =! ", 
			SetOperator.Add => key + " += ", 
			SetOperator.Subtract => key + " -= ", 
			SetOperator.Multiply => key + " *= ", 
			SetOperator.Divide => key + " /= ", 
			SetOperator.Remainder => key + " %= ", 
			_ => key + " = ", 
		};
		Type type = ((object)variable).GetType();
		if (type == typeof(BooleanVariable))
		{
			key += booleanData.GetDescription();
		}
		else if (type == typeof(IntegerVariable))
		{
			key += integerData.GetDescription();
		}
		else if (type == typeof(FloatVariable))
		{
			key += floatData.GetDescription();
		}
		else if (type == typeof(StringVariable))
		{
			key += stringData.GetDescription();
		}
		else if (type == typeof(AnimatorVariable))
		{
			key += animatorData.GetDescription();
		}
		else if (type == typeof(AudioSourceVariable))
		{
			key += audioSourceData.GetDescription();
		}
		else if (type == typeof(ColorVariable))
		{
			key += colorData.GetDescription();
		}
		else if (type == typeof(GameObjectVariable))
		{
			key += gameObjectData.GetDescription();
		}
		else if (type == typeof(MaterialVariable))
		{
			key += materialData.GetDescription();
		}
		else if (type == typeof(ObjectVariable))
		{
			key += objectData.GetDescription();
		}
		else if (type == typeof(Rigidbody2DVariable))
		{
			key += rigidbody2DData.GetDescription();
		}
		else if (type == typeof(SpriteVariable))
		{
			key += spriteData.GetDescription();
		}
		else if (type == typeof(TextureVariable))
		{
			key += textureData.GetDescription();
		}
		else if (type == typeof(TransformVariable))
		{
			key += transformData.GetDescription();
		}
		else if (type == typeof(Vector2Variable))
		{
			key += vector2Data.GetDescription();
		}
		else if (type == typeof(Vector3Variable))
		{
			key += vector3Data.GetDescription();
		}
		return key;
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
