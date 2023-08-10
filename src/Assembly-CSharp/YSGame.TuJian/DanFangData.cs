namespace YSGame.TuJian;

public class DanFangData
{
	public int ItemID;

	public int YaoYinID;

	public int ZhuYao1ID;

	public int ZhuYao2ID;

	public int FuYao1ID;

	public int FuYao2ID;

	public int YaoYinCount;

	public int ZhuYao1Count;

	public int ZhuYao2Count;

	public int FuYao1Count;

	public int FuYao2Count;

	public int CastTime;

	public int YaoCaiTypeCount;

	public void CalcYaoCaiTypeCount()
	{
		if (YaoYinCount > 0)
		{
			YaoCaiTypeCount++;
		}
		if (ZhuYao1Count > 0)
		{
			YaoCaiTypeCount++;
		}
		if (ZhuYao2Count > 0)
		{
			YaoCaiTypeCount++;
		}
		if (FuYao1Count > 0)
		{
			YaoCaiTypeCount++;
		}
		if (FuYao2Count > 0)
		{
			YaoCaiTypeCount++;
		}
	}
}
