namespace Fungus;

public static class FungusPrioritySignals
{
	public delegate void FungusPriorityStartHandler();

	public delegate void FungusPriorityEndHandler();

	public delegate void FungusPriorityChangeHandler(int previousActiveDepth, int newActiveDepth);

	private static int activeDepth;

	public static int CurrentPriorityDepth => activeDepth;

	public static event FungusPriorityStartHandler OnFungusPriorityStart;

	public static event FungusPriorityEndHandler OnFungusPriorityEnd;

	public static event FungusPriorityChangeHandler OnFungusPriorityChange;

	public static void DoIncreasePriorityDepth()
	{
		if (activeDepth == 0 && FungusPrioritySignals.OnFungusPriorityStart != null)
		{
			FungusPrioritySignals.OnFungusPriorityStart();
		}
		if (FungusPrioritySignals.OnFungusPriorityChange != null)
		{
			FungusPrioritySignals.OnFungusPriorityChange(activeDepth, activeDepth + 1);
		}
		activeDepth++;
	}

	public static void DoDecreasePriorityDepth()
	{
		if (FungusPrioritySignals.OnFungusPriorityChange != null)
		{
			FungusPrioritySignals.OnFungusPriorityChange(activeDepth, activeDepth - 1);
		}
		if (activeDepth == 1 && FungusPrioritySignals.OnFungusPriorityEnd != null)
		{
			FungusPrioritySignals.OnFungusPriorityEnd();
		}
		activeDepth--;
	}

	public static void DoResetPriority()
	{
		if (activeDepth != 0)
		{
			if (FungusPrioritySignals.OnFungusPriorityChange != null)
			{
				FungusPrioritySignals.OnFungusPriorityChange(activeDepth, 0);
			}
			if (FungusPrioritySignals.OnFungusPriorityEnd != null)
			{
				FungusPrioritySignals.OnFungusPriorityEnd();
			}
			activeDepth = 0;
		}
	}
}
