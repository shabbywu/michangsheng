namespace Fungus;

public static class MenuSignals
{
	public delegate void MenuStartHandler(MenuDialog menu);

	public delegate void MenuEndHandler(MenuDialog menu);

	public static event MenuStartHandler OnMenuStart;

	public static event MenuEndHandler OnMenuEnd;

	public static void DoMenuStart(MenuDialog menu)
	{
		if (MenuSignals.OnMenuStart != null)
		{
			MenuSignals.OnMenuStart(menu);
		}
	}

	public static void DoMenuEnd(MenuDialog menu)
	{
		if (MenuSignals.OnMenuEnd != null)
		{
			MenuSignals.OnMenuEnd(menu);
		}
	}
}
