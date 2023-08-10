namespace KBEngine;

public class ITEM_INFO
{
	public ulong UUID;

	public string uuid = "";

	public int itemId;

	public uint itemCount;

	public int itemIndex;

	public JSONObject Seid = new JSONObject(JSONObject.Type.OBJECT);

	public override string ToString()
	{
		return string.Concat(string.Concat(string.Concat(string.Concat(string.Concat("" + $"UUID:{UUID}\n", "uuid:", uuid, "\n"), $"itemId:{itemId}\n"), $"itemCount:{itemCount}\n"), $"itemIndex:{itemIndex}\n"), $"Seid:{Seid}\n");
	}
}
