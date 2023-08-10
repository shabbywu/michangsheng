namespace KBEngine;

public class DATATYPE_BASE
{
	public virtual void bind()
	{
	}

	public virtual object createFromStream(MemoryStream stream)
	{
		return null;
	}

	public virtual void addToStream(Bundle stream, object v)
	{
	}

	public virtual object parseDefaultValStr(string v)
	{
		return null;
	}

	public virtual bool isSameType(object v)
	{
		return v == null;
	}
}
