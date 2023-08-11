namespace script.ItemSource.Interface;

public abstract class ABItemSourceMag
{
	public ABItemSourceUpdate Update;

	public ABItemSourceIO IO;

	private static ABItemSourceMag _inst;

	public static ABItemSourceMag Inst
	{
		get
		{
			if (_inst == null)
			{
				_inst = new ItemSourceMag();
			}
			return _inst;
		}
	}

	public static void SetNull()
	{
		_inst = null;
	}
}
