namespace KBEngine;

public class Property
{
	public enum EntityDataFlags
	{
		ED_FLAG_UNKOWN = 0,
		ED_FLAG_CELL_PUBLIC = 1,
		ED_FLAG_CELL_PRIVATE = 2,
		ED_FLAG_ALL_CLIENTS = 4,
		ED_FLAG_CELL_PUBLIC_AND_OWN = 8,
		ED_FLAG_OWN_CLIENT = 0x10,
		ED_FLAG_BASE_AND_CLIENT = 0x20,
		ED_FLAG_BASE = 0x40,
		ED_FLAG_OTHER_CLIENTS = 0x80
	}

	public string name = "";

	public ushort properUtype;

	public uint properFlags;

	public short aliasID = -1;

	public object defaultVal;

	public bool isBase()
	{
		if (properFlags != 32)
		{
			return properFlags == 64;
		}
		return true;
	}

	public bool isOwnerOnly()
	{
		if (properFlags != 8)
		{
			return properFlags == 16;
		}
		return true;
	}

	public bool isOtherOnly()
	{
		if (properFlags != 128)
		{
			return properFlags == 128;
		}
		return true;
	}
}
