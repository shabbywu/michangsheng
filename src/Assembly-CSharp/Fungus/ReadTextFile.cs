using System;
using UnityEngine;

namespace Fungus;

[CommandInfo("Variable", "Read Text File", "Reads in a text file and stores the contents in a string variable", 0)]
public class ReadTextFile : Command
{
	[Tooltip("Text file to read into the string variable")]
	[SerializeField]
	protected TextAsset textFile;

	[Tooltip("String variable to store the tex file contents in")]
	[VariableProperty(new Type[] { typeof(StringVariable) })]
	[SerializeField]
	protected StringVariable stringVariable;

	public override void OnEnter()
	{
		if ((Object)(object)textFile == (Object)null || (Object)(object)stringVariable == (Object)null)
		{
			Continue();
			return;
		}
		stringVariable.Value = textFile.text;
		Continue();
	}

	public override string GetSummary()
	{
		if ((Object)(object)stringVariable == (Object)null)
		{
			return "Error: Variable not selected";
		}
		if ((Object)(object)textFile == (Object)null)
		{
			return "Error: Text file not selected";
		}
		return stringVariable.Key;
	}

	public override bool HasReference(Variable variable)
	{
		return (Object)(object)variable == (Object)(object)stringVariable;
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)253, (byte)253, (byte)150, byte.MaxValue));
	}
}
