using System;

namespace script.EventMsg
{
	// Token: 0x02000A47 RID: 2631
	[Serializable]
	public class EventData : IComparable<EventData>
	{
		// Token: 0x0600482E RID: 18478 RVA: 0x000027FC File Offset: 0x000009FC
		public EventData()
		{
		}

		// Token: 0x0600482F RID: 18479 RVA: 0x001E76F9 File Offset: 0x001E58F9
		public EventData(int outYear, int startYear, int type, int typeId, int id)
		{
			this.OutYear = outYear;
			this.StartYear = startYear;
			this.Type = type;
			this.TypeId = typeId;
			this.Id = id;
		}

		// Token: 0x06004830 RID: 18480 RVA: 0x001E7726 File Offset: 0x001E5926
		public EventData(int outYear, int startYear, int type, int typeId, int id, string npcName)
		{
			this.OutYear = outYear;
			this.StartYear = startYear;
			this.Type = type;
			this.TypeId = typeId;
			this.Id = id;
			this.npcName = npcName;
		}

		// Token: 0x06004831 RID: 18481 RVA: 0x001E775B File Offset: 0x001E595B
		public int CompareTo(EventData other)
		{
			return this.StartYear + 1 - other.StartYear;
		}

		// Token: 0x040048D5 RID: 18645
		public int OutYear;

		// Token: 0x040048D6 RID: 18646
		public int StartYear;

		// Token: 0x040048D7 RID: 18647
		public int Type;

		// Token: 0x040048D8 RID: 18648
		public int TypeId;

		// Token: 0x040048D9 RID: 18649
		public int Id;

		// Token: 0x040048DA RID: 18650
		public string npcName;
	}
}
