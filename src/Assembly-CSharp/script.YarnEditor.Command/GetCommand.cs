using Yarn.Unity;
using script.YarnEditor.Manager;

namespace script.YarnEditor.Command;

public class GetCommand
{
	[YarnFunction("GetValue")]
	public static string GetValue(string key)
	{
		return StoryManager.Inst.GetGoalValue(key);
	}

	[YarnFunction("GetRandomNpcId")]
	public static int GetRandomNpcId(int level, int liuPai, int sex)
	{
		return FactoryManager.inst.npcFactory.CreateNpcByLiuPaiAndLevel(liuPai, level, sex);
	}
}
