using System.Text;

namespace Fungus;

public interface IStringSubstituter
{
	StringBuilder _StringBuilder { get; }

	string SubstituteStrings(string input);

	bool SubstituteStrings(StringBuilder input);
}
