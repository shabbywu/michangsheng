using System.Collections.Generic;

namespace script.NpcAction;

public class NpcDataGroup
{
	public Dictionary<int, NpcData> NpcDict = new Dictionary<int, NpcData>();

	public bool IsFree = true;

	public string Error;

	public bool IsNeedSavePlace;

	public void Start(bool isNeedSavePlace = false)
	{
		IsNeedSavePlace = isNeedSavePlace;
	}

	public void Clear()
	{
		NpcDict = new Dictionary<int, NpcData>();
		IsFree = true;
		IsNeedSavePlace = false;
	}
}
