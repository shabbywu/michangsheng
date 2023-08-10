using System;
using System.Collections.Generic;

namespace PaiMai;

[Serializable]
public class PaiMaiData
{
	public int Id;

	public List<int> ShopList = new List<int>();

	public bool IsJoined;

	public DateTime NextUpdateTime;

	public int No;
}
