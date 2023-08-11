using System;

namespace MoonSharp.Interpreter.Interop.StandardDescriptors;

internal class EventFacade : IUserDataType
{
	private Func<object, ScriptExecutionContext, CallbackArguments, DynValue> m_AddCallback;

	private Func<object, ScriptExecutionContext, CallbackArguments, DynValue> m_RemoveCallback;

	private object m_Object;

	public EventFacade(EventMemberDescriptor parent, object obj)
	{
		m_Object = obj;
		m_AddCallback = parent.AddCallback;
		m_RemoveCallback = parent.RemoveCallback;
	}

	public EventFacade(Func<object, ScriptExecutionContext, CallbackArguments, DynValue> addCallback, Func<object, ScriptExecutionContext, CallbackArguments, DynValue> removeCallback, object obj)
	{
		m_Object = obj;
		m_AddCallback = addCallback;
		m_RemoveCallback = removeCallback;
	}

	public DynValue Index(Script script, DynValue index, bool isDirectIndexing)
	{
		if (index.Type == DataType.String)
		{
			if (index.String == "add")
			{
				return DynValue.NewCallback((ScriptExecutionContext c, CallbackArguments a) => m_AddCallback(m_Object, c, a));
			}
			if (index.String == "remove")
			{
				return DynValue.NewCallback((ScriptExecutionContext c, CallbackArguments a) => m_RemoveCallback(m_Object, c, a));
			}
		}
		throw new ScriptRuntimeException("Events only support add and remove methods");
	}

	public bool SetIndex(Script script, DynValue index, DynValue value, bool isDirectIndexing)
	{
		throw new ScriptRuntimeException("Events do not have settable fields");
	}

	public DynValue MetaIndex(Script script, string metaname)
	{
		return null;
	}
}
