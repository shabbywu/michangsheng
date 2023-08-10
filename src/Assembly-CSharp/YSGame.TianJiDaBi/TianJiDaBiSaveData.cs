using System;
using System.Collections.Generic;

namespace YSGame.TianJiDaBi;

[Serializable]
public class TianJiDaBiSaveData
{
	public int LastMatchIndex;

	public int LastMatchYear;

	public Match LastMatch;

	public Match NowMatch;

	public List<Match> HistotyMatchList = new List<Match>();
}
