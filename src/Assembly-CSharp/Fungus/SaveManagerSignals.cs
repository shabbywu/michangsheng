namespace Fungus;

public static class SaveManagerSignals
{
	public delegate void SavePointLoadedHandler(string savePointKey);

	public delegate void SavePointAddedHandler(string savePointKey, string savePointDescription);

	public delegate void SaveResetHandler();

	public static event SavePointLoadedHandler OnSavePointLoaded;

	public static event SavePointAddedHandler OnSavePointAdded;

	public static event SaveResetHandler OnSaveReset;

	public static void DoSavePointLoaded(string savePointKey)
	{
		if (SaveManagerSignals.OnSavePointLoaded != null)
		{
			SaveManagerSignals.OnSavePointLoaded(savePointKey);
		}
	}

	public static void DoSavePointAdded(string savePointKey, string savePointDescription)
	{
		if (SaveManagerSignals.OnSavePointAdded != null)
		{
			SaveManagerSignals.OnSavePointAdded(savePointKey, savePointDescription);
		}
	}

	public static void DoSaveReset()
	{
		if (SaveManagerSignals.OnSaveReset != null)
		{
			SaveManagerSignals.OnSaveReset();
		}
	}
}
