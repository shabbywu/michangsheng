using System;
using System.Collections;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

namespace Fungus;

[CommandInfo("Scripting", "Invoke Method", "Invokes a method of a component via reflection. Supports passing multiple parameters and storing returned values in a Fungus variable.", 0)]
public class InvokeMethod : Command
{
	[Tooltip("A description of what this command does. Appears in the command summary.")]
	[SerializeField]
	protected string description = "";

	[Tooltip("GameObject containing the component method to be invoked")]
	[SerializeField]
	protected GameObject targetObject;

	[HideInInspector]
	[Tooltip("Name of assembly containing the target component")]
	[SerializeField]
	protected string targetComponentAssemblyName;

	[HideInInspector]
	[Tooltip("Full name of the target component")]
	[SerializeField]
	protected string targetComponentFullname;

	[HideInInspector]
	[Tooltip("Display name of the target component")]
	[SerializeField]
	protected string targetComponentText;

	[HideInInspector]
	[Tooltip("Name of target method to invoke on the target component")]
	[SerializeField]
	protected string targetMethod;

	[HideInInspector]
	[Tooltip("Display name of target method to invoke on the target component")]
	[SerializeField]
	protected string targetMethodText;

	[HideInInspector]
	[Tooltip("List of parameters to pass to the invoked method")]
	[SerializeField]
	protected InvokeMethodParameter[] methodParameters;

	[HideInInspector]
	[Tooltip("If true, store the return value in a flowchart variable of the same type.")]
	[SerializeField]
	protected bool saveReturnValue;

	[HideInInspector]
	[Tooltip("Name of Fungus variable to store the return value in")]
	[SerializeField]
	protected string returnValueVariableKey;

	[HideInInspector]
	[Tooltip("The type of the return value")]
	[SerializeField]
	protected string returnValueType;

	[HideInInspector]
	[Tooltip("If true, list all inherited methods for the component")]
	[SerializeField]
	protected bool showInherited;

	[HideInInspector]
	[Tooltip("The coroutine call behavior for methods that return IEnumerator")]
	[SerializeField]
	protected CallMode callMode;

	protected Type componentType;

	protected Component objComponent;

	protected Type[] parameterTypes;

	protected MethodInfo objMethod;

	public virtual GameObject TargetObject => targetObject;

	protected virtual void Awake()
	{
		if (componentType == null)
		{
			componentType = ReflectionHelper.GetType(targetComponentAssemblyName);
		}
		if ((Object)(object)objComponent == (Object)null)
		{
			objComponent = targetObject.GetComponent(componentType);
		}
		if (parameterTypes == null)
		{
			parameterTypes = GetParameterTypes();
		}
		if (objMethod == null)
		{
			objMethod = UnityEventBase.GetValidMethodInfo((object)objComponent, targetMethod, parameterTypes);
		}
	}

	protected virtual IEnumerator ExecuteCoroutine()
	{
		yield return ((MonoBehaviour)this).StartCoroutine((IEnumerator)objMethod.Invoke(objComponent, GetParameterValues()));
		if (callMode == CallMode.WaitUntilFinished)
		{
			Continue();
		}
	}

	protected virtual Type[] GetParameterTypes()
	{
		Type[] array = new Type[methodParameters.Length];
		for (int i = 0; i < methodParameters.Length; i++)
		{
			Type type = ReflectionHelper.GetType(methodParameters[i].objValue.typeAssemblyname);
			array[i] = type;
		}
		return array;
	}

	protected virtual object[] GetParameterValues()
	{
		//IL_0393: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bb: Unknown result type (might be due to invalid IL or missing references)
		object[] array = new object[methodParameters.Length];
		Flowchart flowchart = GetFlowchart();
		for (int i = 0; i < methodParameters.Length; i++)
		{
			InvokeMethodParameter invokeMethodParameter = methodParameters[i];
			if (string.IsNullOrEmpty(invokeMethodParameter.variableKey))
			{
				array[i] = invokeMethodParameter.objValue.GetValue();
				continue;
			}
			object obj = null;
			switch (invokeMethodParameter.objValue.typeFullname)
			{
			case "System.Int32":
			{
				IntegerVariable variable8 = flowchart.GetVariable<IntegerVariable>(invokeMethodParameter.variableKey);
				if ((Object)(object)variable8 != (Object)null)
				{
					obj = variable8.Value;
				}
				break;
			}
			case "System.Boolean":
			{
				BooleanVariable variable12 = flowchart.GetVariable<BooleanVariable>(invokeMethodParameter.variableKey);
				if ((Object)(object)variable12 != (Object)null)
				{
					obj = variable12.Value;
				}
				break;
			}
			case "System.Single":
			{
				FloatVariable variable4 = flowchart.GetVariable<FloatVariable>(invokeMethodParameter.variableKey);
				if ((Object)(object)variable4 != (Object)null)
				{
					obj = variable4.Value;
				}
				break;
			}
			case "System.String":
			{
				StringVariable variable10 = flowchart.GetVariable<StringVariable>(invokeMethodParameter.variableKey);
				if ((Object)(object)variable10 != (Object)null)
				{
					obj = variable10.Value;
				}
				break;
			}
			case "UnityEngine.Color":
			{
				ColorVariable variable6 = flowchart.GetVariable<ColorVariable>(invokeMethodParameter.variableKey);
				if ((Object)(object)variable6 != (Object)null)
				{
					obj = variable6.Value;
				}
				break;
			}
			case "UnityEngine.GameObject":
			{
				GameObjectVariable variable2 = flowchart.GetVariable<GameObjectVariable>(invokeMethodParameter.variableKey);
				if ((Object)(object)variable2 != (Object)null)
				{
					obj = variable2.Value;
				}
				break;
			}
			case "UnityEngine.Material":
			{
				MaterialVariable variable11 = flowchart.GetVariable<MaterialVariable>(invokeMethodParameter.variableKey);
				if ((Object)(object)variable11 != (Object)null)
				{
					obj = variable11.Value;
				}
				break;
			}
			case "UnityEngine.Sprite":
			{
				SpriteVariable variable9 = flowchart.GetVariable<SpriteVariable>(invokeMethodParameter.variableKey);
				if ((Object)(object)variable9 != (Object)null)
				{
					obj = variable9.Value;
				}
				break;
			}
			case "UnityEngine.Texture":
			{
				TextureVariable variable7 = flowchart.GetVariable<TextureVariable>(invokeMethodParameter.variableKey);
				if ((Object)(object)variable7 != (Object)null)
				{
					obj = variable7.Value;
				}
				break;
			}
			case "UnityEngine.Vector2":
			{
				Vector2Variable variable5 = flowchart.GetVariable<Vector2Variable>(invokeMethodParameter.variableKey);
				if ((Object)(object)variable5 != (Object)null)
				{
					obj = variable5.Value;
				}
				break;
			}
			case "UnityEngine.Vector3":
			{
				Vector3Variable variable3 = flowchart.GetVariable<Vector3Variable>(invokeMethodParameter.variableKey);
				if ((Object)(object)variable3 != (Object)null)
				{
					obj = variable3.Value;
				}
				break;
			}
			default:
			{
				ObjectVariable variable = flowchart.GetVariable<ObjectVariable>(invokeMethodParameter.variableKey);
				if ((Object)(object)variable != (Object)null)
				{
					obj = variable.Value;
				}
				break;
			}
			}
			array[i] = obj;
		}
		return array;
	}

	protected virtual void SetVariable(string key, object value, string returnType)
	{
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Expected O, but got Unknown
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Expected O, but got Unknown
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Expected O, but got Unknown
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Expected O, but got Unknown
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Expected O, but got Unknown
		Flowchart flowchart = GetFlowchart();
		switch (returnType)
		{
		case "System.Int32":
			flowchart.GetVariable<IntegerVariable>(key).Value = (int)value;
			break;
		case "System.Boolean":
			flowchart.GetVariable<BooleanVariable>(key).Value = (bool)value;
			break;
		case "System.Single":
			flowchart.GetVariable<FloatVariable>(key).Value = (float)value;
			break;
		case "System.String":
			flowchart.GetVariable<StringVariable>(key).Value = (string)value;
			break;
		case "UnityEngine.Color":
			flowchart.GetVariable<ColorVariable>(key).Value = (Color)value;
			break;
		case "UnityEngine.GameObject":
			flowchart.GetVariable<GameObjectVariable>(key).Value = (GameObject)value;
			break;
		case "UnityEngine.Material":
			flowchart.GetVariable<MaterialVariable>(key).Value = (Material)value;
			break;
		case "UnityEngine.Sprite":
			flowchart.GetVariable<SpriteVariable>(key).Value = (Sprite)value;
			break;
		case "UnityEngine.Texture":
			flowchart.GetVariable<TextureVariable>(key).Value = (Texture)value;
			break;
		case "UnityEngine.Vector2":
			flowchart.GetVariable<Vector2Variable>(key).Value = (Vector2)value;
			break;
		case "UnityEngine.Vector3":
			flowchart.GetVariable<Vector3Variable>(key).Value = (Vector3)value;
			break;
		default:
			flowchart.GetVariable<ObjectVariable>(key).Value = (Object)value;
			break;
		}
	}

	public override void OnEnter()
	{
		try
		{
			if ((Object)(object)targetObject == (Object)null || string.IsNullOrEmpty(targetComponentAssemblyName) || string.IsNullOrEmpty(targetMethod))
			{
				Continue();
				return;
			}
			if (returnValueType != "System.Collections.IEnumerator")
			{
				object value = objMethod.Invoke(objComponent, GetParameterValues());
				if (saveReturnValue)
				{
					SetVariable(returnValueVariableKey, value, returnValueType);
				}
				Continue();
				return;
			}
			((MonoBehaviour)this).StartCoroutine(ExecuteCoroutine());
			if (callMode == CallMode.Continue)
			{
				Continue();
			}
			else if (callMode == CallMode.Stop)
			{
				StopParentBlock();
			}
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("Error: " + ex.Message));
		}
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)235, (byte)191, (byte)217, byte.MaxValue));
	}

	public override string GetSummary()
	{
		if ((Object)(object)targetObject == (Object)null)
		{
			return "Error: targetObject is not assigned";
		}
		if (!string.IsNullOrEmpty(description))
		{
			return description;
		}
		return ((Object)targetObject).name + "." + targetComponentText + "." + targetMethodText;
	}
}
