using System;
using MoonSharp.Interpreter.Compatibility;
using MoonSharp.Interpreter.Interop;

namespace MoonSharp.Interpreter;

internal class AutoDescribingUserDataDescriptor : IUserDataDescriptor
{
	private string m_FriendlyName;

	private Type m_Type;

	public string Name => m_FriendlyName;

	public Type Type => m_Type;

	public AutoDescribingUserDataDescriptor(Type type, string friendlyName)
	{
		m_FriendlyName = friendlyName;
		m_Type = type;
	}

	public DynValue Index(Script script, object obj, DynValue index, bool isDirectIndexing)
	{
		if (obj is IUserDataType userDataType)
		{
			return userDataType.Index(script, index, isDirectIndexing);
		}
		return null;
	}

	public bool SetIndex(Script script, object obj, DynValue index, DynValue value, bool isDirectIndexing)
	{
		if (obj is IUserDataType userDataType)
		{
			return userDataType.SetIndex(script, index, value, isDirectIndexing);
		}
		return false;
	}

	public string AsString(object obj)
	{
		return obj?.ToString();
	}

	public DynValue MetaIndex(Script script, object obj, string metaname)
	{
		if (obj is IUserDataType userDataType)
		{
			return userDataType.MetaIndex(script, metaname);
		}
		return null;
	}

	public bool IsTypeCompatible(Type type, object obj)
	{
		return Framework.Do.IsInstanceOfType(type, obj);
	}
}
