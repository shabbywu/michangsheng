using System;
using MoonSharp.Interpreter.Interop.BasicDescriptors;
using MoonSharp.Interpreter.Interop.Converters;

namespace MoonSharp.Interpreter.Interop;

public class ObjectCallbackMemberDescriptor : FunctionMemberDescriptorBase
{
	private Func<object, ScriptExecutionContext, CallbackArguments, object> m_CallbackFunc;

	public ObjectCallbackMemberDescriptor(string funcName)
		: this(funcName, (object o, ScriptExecutionContext c, CallbackArguments a) => DynValue.Void, new ParameterDescriptor[0])
	{
	}

	public ObjectCallbackMemberDescriptor(string funcName, Func<object, ScriptExecutionContext, CallbackArguments, object> callBack)
		: this(funcName, callBack, new ParameterDescriptor[0])
	{
	}

	public ObjectCallbackMemberDescriptor(string funcName, Func<object, ScriptExecutionContext, CallbackArguments, object> callBack, ParameterDescriptor[] parameters)
	{
		m_CallbackFunc = callBack;
		Initialize(funcName, isStatic: false, parameters, isExtensionMethod: false);
	}

	public override DynValue Execute(Script script, object obj, ScriptExecutionContext context, CallbackArguments args)
	{
		if (m_CallbackFunc != null)
		{
			object obj2 = m_CallbackFunc(obj, context, args);
			return ClrToScriptConversions.ObjectToDynValue(script, obj2);
		}
		return DynValue.Void;
	}
}
