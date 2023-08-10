using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MoonSharp.Interpreter.Compatibility;
using MoonSharp.Interpreter.Interop.BasicDescriptors;
using MoonSharp.Interpreter.Interop.Converters;

namespace MoonSharp.Interpreter.Interop;

public abstract class FunctionMemberDescriptorBase : IOverloadableMemberDescriptor, IMemberDescriptor
{
	public bool IsStatic { get; private set; }

	public string Name { get; private set; }

	public string SortDiscriminant { get; private set; }

	public ParameterDescriptor[] Parameters { get; private set; }

	public Type ExtensionMethodType { get; private set; }

	public Type VarArgsArrayType { get; private set; }

	public Type VarArgsElementType { get; private set; }

	public MemberDescriptorAccess MemberAccess => MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanExecute;

	protected void Initialize(string funcName, bool isStatic, ParameterDescriptor[] parameters, bool isExtensionMethod)
	{
		Name = funcName;
		IsStatic = isStatic;
		Parameters = parameters;
		if (isExtensionMethod)
		{
			ExtensionMethodType = Parameters[0].Type;
		}
		if (Parameters.Length != 0 && Parameters[Parameters.Length - 1].IsVarArgs)
		{
			VarArgsArrayType = Parameters[Parameters.Length - 1].Type;
			VarArgsElementType = Parameters[Parameters.Length - 1].Type.GetElementType();
		}
		SortDiscriminant = string.Join(":", Parameters.Select((ParameterDescriptor pi) => pi.Type.FullName).ToArray());
	}

	public Func<ScriptExecutionContext, CallbackArguments, DynValue> GetCallback(Script script, object obj = null)
	{
		return (ScriptExecutionContext c, CallbackArguments a) => Execute(script, obj, c, a);
	}

	public CallbackFunction GetCallbackFunction(Script script, object obj = null)
	{
		return new CallbackFunction(GetCallback(script, obj), Name);
	}

	public DynValue GetCallbackAsDynValue(Script script, object obj = null)
	{
		return DynValue.NewCallback(GetCallbackFunction(script, obj));
	}

	public static DynValue CreateCallbackDynValue(Script script, MethodInfo mi, object obj = null)
	{
		return new MethodMemberDescriptor(mi).GetCallbackAsDynValue(script, obj);
	}

	protected virtual object[] BuildArgumentList(Script script, object obj, ScriptExecutionContext context, CallbackArguments args, out List<int> outParams)
	{
		ParameterDescriptor[] parameters = Parameters;
		object[] array = new object[parameters.Length];
		int num = (args.IsMethodCall ? 1 : 0);
		outParams = null;
		for (int i = 0; i < array.Length; i++)
		{
			if (parameters[i].Type.IsByRef)
			{
				if (outParams == null)
				{
					outParams = new List<int>();
				}
				outParams.Add(i);
			}
			if (ExtensionMethodType != null && obj != null && i == 0)
			{
				array[i] = obj;
			}
			else if (parameters[i].Type == typeof(Script))
			{
				array[i] = script;
			}
			else if (parameters[i].Type == typeof(ScriptExecutionContext))
			{
				array[i] = context;
			}
			else if (parameters[i].Type == typeof(CallbackArguments))
			{
				array[i] = args.SkipMethodCall();
			}
			else if (parameters[i].IsOut)
			{
				array[i] = null;
			}
			else if (i == parameters.Length - 1 && VarArgsArrayType != null)
			{
				List<DynValue> list = new List<DynValue>();
				while (true)
				{
					DynValue dynValue = args.RawGet(num, translateVoids: false);
					num++;
					if (dynValue == null)
					{
						break;
					}
					list.Add(dynValue);
				}
				if (list.Count == 1)
				{
					DynValue dynValue2 = list[0];
					if (dynValue2.Type == DataType.UserData && dynValue2.UserData.Object != null && Framework.Do.IsAssignableFrom(VarArgsArrayType, dynValue2.UserData.Object.GetType()))
					{
						array[i] = dynValue2.UserData.Object;
						continue;
					}
				}
				Array array2 = Array.CreateInstance(VarArgsElementType, list.Count);
				for (int j = 0; j < list.Count; j++)
				{
					array2.SetValue(ScriptToClrConversions.DynValueToObjectOfType(list[j], VarArgsElementType, null, isOptional: false), j);
				}
				array[i] = array2;
			}
			else
			{
				DynValue value = args.RawGet(num, translateVoids: false) ?? DynValue.Void;
				array[i] = ScriptToClrConversions.DynValueToObjectOfType(value, parameters[i].Type, parameters[i].DefaultValue, parameters[i].HasDefaultValue);
				num++;
			}
		}
		return array;
	}

	protected static DynValue BuildReturnValue(Script script, List<int> outParams, object[] pars, object retv)
	{
		if (outParams == null)
		{
			return ClrToScriptConversions.ObjectToDynValue(script, retv);
		}
		DynValue[] array = new DynValue[outParams.Count + 1];
		if (retv is DynValue && ((DynValue)retv).IsVoid())
		{
			array[0] = DynValue.Nil;
		}
		else
		{
			array[0] = ClrToScriptConversions.ObjectToDynValue(script, retv);
		}
		for (int i = 0; i < outParams.Count; i++)
		{
			array[i + 1] = ClrToScriptConversions.ObjectToDynValue(script, pars[outParams[i]]);
		}
		return DynValue.NewTuple(array);
	}

	public abstract DynValue Execute(Script script, object obj, ScriptExecutionContext context, CallbackArguments args);

	public virtual DynValue GetValue(Script script, object obj)
	{
		this.CheckAccess(MemberDescriptorAccess.CanRead, obj);
		return GetCallbackAsDynValue(script, obj);
	}

	public virtual void SetValue(Script script, object obj, DynValue v)
	{
		this.CheckAccess(MemberDescriptorAccess.CanWrite, obj);
	}
}
