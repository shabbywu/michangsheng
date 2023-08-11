using System;
using System.Collections.Generic;

public class UINPCWuDaoData : IComparable
{
	public int ID;

	public int Level;

	public int Exp;

	public List<int> SkillIDList = new List<int>();

	public int CompareTo(object obj)
	{
		if (Exp < ((UINPCWuDaoData)obj).Exp)
		{
			return 1;
		}
		if (Exp == ((UINPCWuDaoData)obj).Exp)
		{
			return 0;
		}
		return -1;
	}
}
