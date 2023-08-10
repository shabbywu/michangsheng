namespace KBEngine;

public class card
{
	public string uuid;

	public int cardType;

	public card()
	{
	}

	public card(int type)
	{
		uuid = Tools.getUUID();
		cardType = type;
	}
}
