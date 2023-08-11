using Yarn.Unity;

namespace script.YarnEditor.Command;

public class AddCommand
{
	[YarnCommand("AddWuDaoDian")]
	public static void AddWuDaoDian(int num)
	{
		Tools.instance.getPlayer()._WuDaoDian += num;
	}
}
