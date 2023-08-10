using System;

namespace script.EventMsg;

[Serializable]
public class EventData : IComparable<EventData>
{
	public int OutYear;

	public int StartYear;

	public int Type;

	public int TypeId;

	public int Id;

	public string npcName;

	public EventData()
	{
	}

	public EventData(int outYear, int startYear, int type, int typeId, int id)
	{
		OutYear = outYear;
		StartYear = startYear;
		Type = type;
		TypeId = typeId;
		Id = id;
	}

	public EventData(int outYear, int startYear, int type, int typeId, int id, string npcName)
	{
		OutYear = outYear;
		StartYear = startYear;
		Type = type;
		TypeId = typeId;
		Id = id;
		this.npcName = npcName;
	}

	public int CompareTo(EventData other)
	{
		return StartYear + 1 - other.StartYear;
	}
}
