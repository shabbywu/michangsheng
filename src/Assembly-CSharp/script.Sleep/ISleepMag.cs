namespace script.Sleep;

public abstract class ISleepMag
{
	private static ISleepMag _inst;

	public static ISleepMag Inst
	{
		get
		{
			if (_inst == null)
			{
				_inst = new SleepMag();
			}
			return _inst;
		}
	}

	public abstract void Sleep();
}
