using System.Collections.Generic;
using System.Linq;

namespace MoonSharp.Interpreter.Execution;

internal class ClosureContext : List<DynValue>
{
	public string[] Symbols { get; private set; }

	internal ClosureContext(SymbolRef[] symbols, IEnumerable<DynValue> values)
	{
		Symbols = symbols.Select((SymbolRef s) => s.i_Name).ToArray();
		AddRange(values);
	}

	internal ClosureContext()
	{
		Symbols = new string[0];
	}
}
