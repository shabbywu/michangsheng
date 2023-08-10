using System;
using System.Collections.Generic;

[Serializable]
public class CyPaiMaiInfo
{
	public int PaiMaiId;

	public DateTime StartTime;

	public DateTime EndTime;

	public List<int> ItemList;
}
