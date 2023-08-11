using System;

public class UINPCEventData : IComparable
{
	[NonSerialized]
	public DateTime EventTime;

	public string EventTimeStr;

	public string EventDesc;

	public int CompareTo(object obj)
	{
		if (EventTime < ((UINPCEventData)obj).EventTime)
		{
			return 1;
		}
		if (EventTime == ((UINPCEventData)obj).EventTime)
		{
			return 0;
		}
		return -1;
	}
}
