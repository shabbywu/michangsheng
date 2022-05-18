using System;
using System.Collections.Generic;

// Token: 0x02000383 RID: 899
public class UINPCWuDaoData : IComparable
{
	// Token: 0x06001941 RID: 6465 RVA: 0x00015A05 File Offset: 0x00013C05
	public int CompareTo(object obj)
	{
		if (this.Exp < ((UINPCWuDaoData)obj).Exp)
		{
			return 1;
		}
		if (this.Exp == ((UINPCWuDaoData)obj).Exp)
		{
			return 0;
		}
		return -1;
	}

	// Token: 0x04001450 RID: 5200
	public int ID;

	// Token: 0x04001451 RID: 5201
	public int Level;

	// Token: 0x04001452 RID: 5202
	public int Exp;

	// Token: 0x04001453 RID: 5203
	public List<int> SkillIDList = new List<int>();
}
