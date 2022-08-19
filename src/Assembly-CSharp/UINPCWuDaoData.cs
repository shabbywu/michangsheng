using System;
using System.Collections.Generic;

// Token: 0x0200026B RID: 619
public class UINPCWuDaoData : IComparable
{
	// Token: 0x0600168F RID: 5775 RVA: 0x0009A0A1 File Offset: 0x000982A1
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

	// Token: 0x04001100 RID: 4352
	public int ID;

	// Token: 0x04001101 RID: 4353
	public int Level;

	// Token: 0x04001102 RID: 4354
	public int Exp;

	// Token: 0x04001103 RID: 4355
	public List<int> SkillIDList = new List<int>();
}
