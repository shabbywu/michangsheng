using System;

// Token: 0x0200026D RID: 621
public class UINPCEventData : IComparable
{
	// Token: 0x06001692 RID: 5778 RVA: 0x0009A0F4 File Offset: 0x000982F4
	public int CompareTo(object obj)
	{
		if (this.EventTime < ((UINPCEventData)obj).EventTime)
		{
			return 1;
		}
		if (this.EventTime == ((UINPCEventData)obj).EventTime)
		{
			return 0;
		}
		return -1;
	}

	// Token: 0x04001109 RID: 4361
	[NonSerialized]
	public DateTime EventTime;

	// Token: 0x0400110A RID: 4362
	public string EventTimeStr;

	// Token: 0x0400110B RID: 4363
	public string EventDesc;
}
