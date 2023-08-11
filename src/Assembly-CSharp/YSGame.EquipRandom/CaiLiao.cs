using System.Collections.Generic;

namespace YSGame.EquipRandom;

public class CaiLiao
{
	private static Dictionary<int, int> CaiLiaoLingLiDict = new Dictionary<int, int>
	{
		{ 1, 1 },
		{ 2, 3 },
		{ 3, 9 },
		{ 4, 18 },
		{ 5, 30 },
		{ 6, 48 }
	};

	public JSONObject Data;

	public string Name;

	public int Quality;

	public int Type;

	public int ShuXingType;

	public int LingLi;

	public int WuWeiType;

	public int QinHe;

	public int CaoKong;

	public int LingXing;

	public int JianGu;

	public int RenXing;

	public int TotalWuWei;

	public int ShuXingID(int EquipType)
	{
		return RandomEquip.FindShuXingIDByEquipTypeAndShuXingType(ShuXingType, EquipType);
	}

	public int AttackType(int EquipType)
	{
		return jsonData.instance.LianQiShuXinLeiBie[ShuXingType.ToString()]["AttackType"].I;
	}

	public CaiLiao(JSONObject item)
	{
		Data = item;
		Name = item["name"].str.ToCN();
		Quality = item["quality"].I;
		Type = item["type"].I;
		ShuXingType = item["ShuXingType"].I;
		LingLi = CaiLiaoLingLiDict[Quality];
		WuWeiType = item["WuWeiType"].I;
		JSONObject lianQiWuWeiBiao = jsonData.instance.LianQiWuWeiBiao;
		QinHe = lianQiWuWeiBiao[WuWeiType.ToString()]["value1"].I;
		CaoKong = lianQiWuWeiBiao[WuWeiType.ToString()]["value2"].I;
		LingXing = lianQiWuWeiBiao[WuWeiType.ToString()]["value3"].I;
		JianGu = lianQiWuWeiBiao[WuWeiType.ToString()]["value4"].I;
		RenXing = lianQiWuWeiBiao[WuWeiType.ToString()]["value5"].I;
		TotalWuWei = QinHe + CaoKong + LingXing + JianGu + RenXing;
	}

	public override string ToString()
	{
		return $"{Quality}品{Name} 类型{Type} 属性类型{ShuXingType} 灵力{LingLi} 五维类型{WuWeiType} 亲和{QinHe} 操控{CaoKong} 灵性{LingXing} 坚固{JianGu} 韧性{RenXing} 总五维{TotalWuWei}";
	}
}
