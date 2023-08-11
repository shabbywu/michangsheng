using System;
using System.Collections.Generic;

namespace JSONClass;

public class AvatarRandomJsonData : IJSONClass
{
	public static Dictionary<int, AvatarRandomJsonData> DataDict = new Dictionary<int, AvatarRandomJsonData>();

	public static List<AvatarRandomJsonData> DataList = new List<AvatarRandomJsonData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int Sex;

	public int feature;

	public int yanying;

	public int Shawl_hair;

	public int back_gown;

	public int r_arm;

	public int gown;

	public int l_arm;

	public int l_big_arm;

	public int lower_body;

	public int r_big_arm;

	public int blush;

	public int tattoo;

	public int shoes;

	public int upper_body;

	public int yanqiu;

	public int hairColorG;

	public int hairColorB;

	public int mouthColor;

	public int tattooColor;

	public int blushColor;

	public int HaoGanDu;

	public int head;

	public int eyes;

	public int mouth;

	public int nose;

	public int eyebrow;

	public int hair;

	public int a_hair;

	public int b_hair;

	public int characteristic;

	public int a_suit;

	public int hairColorR;

	public int yanzhuColor;

	public int tezhengColor;

	public int eyebrowColor;

	public string BirthdayTime;

	public string Name;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.AvatarRandomJsonData.list)
		{
			try
			{
				AvatarRandomJsonData avatarRandomJsonData = new AvatarRandomJsonData();
				avatarRandomJsonData.Sex = item["Sex"].I;
				avatarRandomJsonData.feature = item["feature"].I;
				avatarRandomJsonData.yanying = item["yanying"].I;
				avatarRandomJsonData.Shawl_hair = item["Shawl_hair"].I;
				avatarRandomJsonData.back_gown = item["back_gown"].I;
				avatarRandomJsonData.r_arm = item["r_arm"].I;
				avatarRandomJsonData.gown = item["gown"].I;
				avatarRandomJsonData.l_arm = item["l_arm"].I;
				avatarRandomJsonData.l_big_arm = item["l_big_arm"].I;
				avatarRandomJsonData.lower_body = item["lower_body"].I;
				avatarRandomJsonData.r_big_arm = item["r_big_arm"].I;
				avatarRandomJsonData.blush = item["blush"].I;
				avatarRandomJsonData.tattoo = item["tattoo"].I;
				avatarRandomJsonData.shoes = item["shoes"].I;
				avatarRandomJsonData.upper_body = item["upper_body"].I;
				avatarRandomJsonData.yanqiu = item["yanqiu"].I;
				avatarRandomJsonData.hairColorG = item["hairColorG"].I;
				avatarRandomJsonData.hairColorB = item["hairColorB"].I;
				avatarRandomJsonData.mouthColor = item["mouthColor"].I;
				avatarRandomJsonData.tattooColor = item["tattooColor"].I;
				avatarRandomJsonData.blushColor = item["blushColor"].I;
				avatarRandomJsonData.HaoGanDu = item["HaoGanDu"].I;
				avatarRandomJsonData.head = item["head"].I;
				avatarRandomJsonData.eyes = item["eyes"].I;
				avatarRandomJsonData.mouth = item["mouth"].I;
				avatarRandomJsonData.nose = item["nose"].I;
				avatarRandomJsonData.eyebrow = item["eyebrow"].I;
				avatarRandomJsonData.hair = item["hair"].I;
				avatarRandomJsonData.a_hair = item["a_hair"].I;
				avatarRandomJsonData.b_hair = item["b_hair"].I;
				avatarRandomJsonData.characteristic = item["characteristic"].I;
				avatarRandomJsonData.a_suit = item["a_suit"].I;
				avatarRandomJsonData.hairColorR = item["hairColorR"].I;
				avatarRandomJsonData.yanzhuColor = item["yanzhuColor"].I;
				avatarRandomJsonData.tezhengColor = item["tezhengColor"].I;
				avatarRandomJsonData.eyebrowColor = item["eyebrowColor"].I;
				avatarRandomJsonData.BirthdayTime = item["BirthdayTime"].Str;
				avatarRandomJsonData.Name = item["Name"].Str;
				if (DataDict.ContainsKey(avatarRandomJsonData.Sex))
				{
					PreloadManager.LogException($"!!!错误!!!向字典AvatarRandomJsonData.DataDict添加数据时出现重复的键，Key:{avatarRandomJsonData.Sex}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(avatarRandomJsonData.Sex, avatarRandomJsonData);
				DataList.Add(avatarRandomJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典AvatarRandomJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
