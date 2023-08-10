using Yarn.Unity;
using script.YarnEditor.Manager;

namespace script.YarnEditor.Command;

public class SetCommand
{
	[YarnCommand("SetTempValue")]
	public static void SetTempValue(string variableName, string value)
	{
		((VariableStorageBehaviour)StoryManager.Inst.TempValueSystem).SetValue(variableName, value);
	}

	[YarnCommand("SetValue")]
	public static void SetValue(string variableName, string value)
	{
		StoryManager.Inst.SetGoalValue(variableName, value);
	}

	[YarnCommand("SetNextYarn")]
	public static void SetNextYarn(string talkName)
	{
		StoryManager.Inst.NextYarn = talkName;
	}

	[YarnCommand("SetPlayerDie")]
	public static void SetPlayerDie()
	{
		UIDeath.Inst.Show(DeathType.身死道消);
	}

	[YarnCommand("SetPlayerLingGen")]
	public static void SetPlayerLingGen(int id, int value)
	{
		Tools.instance.getPlayer().SetLingGen(id, value);
	}

	[YarnCommand("SetPlayerMenPai")]
	public static void SetPlayerMenPai(int id)
	{
		Tools.instance.getPlayer().SetMenPai(id);
	}
}
