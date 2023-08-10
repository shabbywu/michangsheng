using System.Collections.Generic;
using MoonSharp.Interpreter.Execution;

namespace MoonSharp.Interpreter;

public class Closure : RefIdObject, IScriptPrivateResource
{
	public enum UpvaluesType
	{
		None,
		Environment,
		Closure
	}

	private static ClosureContext emptyClosure = new ClosureContext();

	public int EntryPointByteCodeLocation { get; private set; }

	public Script OwnerScript { get; private set; }

	internal ClosureContext ClosureContext { get; private set; }

	internal Closure(Script script, int idx, SymbolRef[] symbols, IEnumerable<DynValue> resolvedLocals)
	{
		OwnerScript = script;
		EntryPointByteCodeLocation = idx;
		if (symbols.Length != 0)
		{
			ClosureContext = new ClosureContext(symbols, resolvedLocals);
		}
		else
		{
			ClosureContext = emptyClosure;
		}
	}

	public DynValue Call()
	{
		return OwnerScript.Call(this);
	}

	public DynValue Call(params object[] args)
	{
		return OwnerScript.Call(this, args);
	}

	public DynValue Call(params DynValue[] args)
	{
		return OwnerScript.Call(this, args);
	}

	public ScriptFunctionDelegate GetDelegate()
	{
		return (object[] args) => Call(args).ToObject();
	}

	public ScriptFunctionDelegate<T> GetDelegate<T>()
	{
		return (object[] args) => Call(args).ToObject<T>();
	}

	public int GetUpvaluesCount()
	{
		return ClosureContext.Count;
	}

	public string GetUpvalueName(int idx)
	{
		return ClosureContext.Symbols[idx];
	}

	public DynValue GetUpvalue(int idx)
	{
		return ClosureContext[idx];
	}

	public UpvaluesType GetUpvaluesType()
	{
		switch (GetUpvaluesCount())
		{
		case 0:
			return UpvaluesType.None;
		case 1:
			if (GetUpvalueName(0) == "_ENV")
			{
				return UpvaluesType.Environment;
			}
			break;
		}
		return UpvaluesType.Closure;
	}
}
