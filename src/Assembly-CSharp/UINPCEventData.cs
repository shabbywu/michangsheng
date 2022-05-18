using System;

// Token: 0x02000385 RID: 901
public class UINPCEventData : IComparable
{
	// Token: 0x06001944 RID: 6468 RVA: 0x00015A58 File Offset: 0x00013C58
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

	// Token: 0x04001459 RID: 5209
	[NonSerialized]
	public DateTime EventTime;

	// Token: 0x0400145A RID: 5210
	public string EventTimeStr;

	// Token: 0x0400145B RID: 5211
	public string EventDesc;
}
