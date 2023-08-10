using System.Collections.Generic;
using System.Text;

namespace Fungus;

public class StringSubstituter : IStringSubstituter
{
	protected static List<ISubstitutionHandler> substitutionHandlers = new List<ISubstitutionHandler>();

	protected StringBuilder stringBuilder;

	protected int recursionDepth;

	public virtual StringBuilder _StringBuilder => stringBuilder;

	public static void RegisterHandler(ISubstitutionHandler handler)
	{
		if (!substitutionHandlers.Contains(handler))
		{
			substitutionHandlers.Add(handler);
		}
	}

	public static void UnregisterHandler(ISubstitutionHandler handler)
	{
		substitutionHandlers.Remove(handler);
	}

	public StringSubstituter(int recursionDepth = 5)
	{
		stringBuilder = new StringBuilder(1024);
		this.recursionDepth = recursionDepth;
	}

	public virtual string SubstituteStrings(string input)
	{
		stringBuilder.Length = 0;
		stringBuilder.Append(input);
		if (SubstituteStrings(stringBuilder))
		{
			return stringBuilder.ToString();
		}
		return input;
	}

	public virtual bool SubstituteStrings(StringBuilder input)
	{
		bool result = false;
		for (int i = 0; i < recursionDepth; i++)
		{
			bool flag = false;
			foreach (ISubstitutionHandler substitutionHandler in substitutionHandlers)
			{
				if (substitutionHandler.SubstituteStrings(input))
				{
					flag = true;
					result = true;
				}
			}
			if (!flag)
			{
				break;
			}
		}
		return result;
	}
}
