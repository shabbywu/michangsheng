using System;
using System.Collections.Generic;

namespace JSONClass;

public class LianQiHeCheng : IJSONClass
{
	public static Dictionary<int, LianQiHeCheng> DataDict = new Dictionary<int, LianQiHeCheng>();

	public static List<LianQiHeCheng> DataList = new List<LianQiHeCheng>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int ShuXingType;

	public int zhonglei;

	public int cast;

	public int seid;

	public int HP;

	public int intvalue1;

	public int intvalue2;

	public int intvalue3;

	public int Itemseid;

	public int itemfanbei;

	public string ZhuShi1;

	public string ZhuShi2;

	public string ZhuShi3;

	public string xiangxidesc;

	public string descfirst;

	public string desc;

	public List<int> Affix = new List<int>();

	public List<int> fanbei = new List<int>();

	public List<int> listvalue1 = new List<int>();

	public List<int> listvalue2 = new List<int>();

	public List<int> listvalue3 = new List<int>();

	public List<int> Itemintvalue1 = new List<int>();

	public List<int> Itemintvalue2 = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.LianQiHeCheng.list)
		{
			try
			{
				LianQiHeCheng lianQiHeCheng = new LianQiHeCheng();
				lianQiHeCheng.id = item["id"].I;
				lianQiHeCheng.ShuXingType = item["ShuXingType"].I;
				lianQiHeCheng.zhonglei = item["zhonglei"].I;
				lianQiHeCheng.cast = item["cast"].I;
				lianQiHeCheng.seid = item["seid"].I;
				lianQiHeCheng.HP = item["HP"].I;
				lianQiHeCheng.intvalue1 = item["intvalue1"].I;
				lianQiHeCheng.intvalue2 = item["intvalue2"].I;
				lianQiHeCheng.intvalue3 = item["intvalue3"].I;
				lianQiHeCheng.Itemseid = item["Itemseid"].I;
				lianQiHeCheng.itemfanbei = item["itemfanbei"].I;
				lianQiHeCheng.ZhuShi1 = item["ZhuShi1"].Str;
				lianQiHeCheng.ZhuShi2 = item["ZhuShi2"].Str;
				lianQiHeCheng.ZhuShi3 = item["ZhuShi3"].Str;
				lianQiHeCheng.xiangxidesc = item["xiangxidesc"].Str;
				lianQiHeCheng.descfirst = item["descfirst"].Str;
				lianQiHeCheng.desc = item["desc"].Str;
				lianQiHeCheng.Affix = item["Affix"].ToList();
				lianQiHeCheng.fanbei = item["fanbei"].ToList();
				lianQiHeCheng.listvalue1 = item["listvalue1"].ToList();
				lianQiHeCheng.listvalue2 = item["listvalue2"].ToList();
				lianQiHeCheng.listvalue3 = item["listvalue3"].ToList();
				lianQiHeCheng.Itemintvalue1 = item["Itemintvalue1"].ToList();
				lianQiHeCheng.Itemintvalue2 = item["Itemintvalue2"].ToList();
				if (DataDict.ContainsKey(lianQiHeCheng.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典LianQiHeCheng.DataDict添加数据时出现重复的键，Key:{lianQiHeCheng.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(lianQiHeCheng.id, lianQiHeCheng);
				DataList.Add(lianQiHeCheng);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典LianQiHeCheng.DataDict添加数据时出现异常，已跳过，请检查配表");
				PreloadManager.LogException($"异常信息:\n{arg}");
				PreloadManager.LogException($"数据序列化:\n{item}");
			}
		}
		if (OnInitFinishAction != null)
		{
			OnInitFinishAction();
		}
	}

	private static void OnInitFinish()
	{
	}
}
