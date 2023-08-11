using System;
using System.Collections.Generic;

namespace JSONClass;

public class AvatarJsonData : IJSONClass
{
	public static Dictionary<int, AvatarJsonData> DataDict = new Dictionary<int, AvatarJsonData>();

	public static List<AvatarJsonData> DataList = new List<AvatarJsonData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int face;

	public int fightFace;

	public int SexType;

	public int AvatarType;

	public int Level;

	public int HP;

	public int dunSu;

	public int ziZhi;

	public int wuXin;

	public int shengShi;

	public int shaQi;

	public int shouYuan;

	public int age;

	public int equipWeapon;

	public int equipClothing;

	public int equipRing;

	public int yuanying;

	public int HuaShenLingYu;

	public int MoneyType;

	public int IsRefresh;

	public int dropType;

	public int canjiaPaiMai;

	public int wudaoType;

	public int XinQuType;

	public int gudingjiage;

	public int sellPercent;

	public string Title;

	public string FirstName;

	public string Name;

	public string menPai;

	public List<int> LingGen = new List<int>();

	public List<int> skills = new List<int>();

	public List<int> staticSkills = new List<int>();

	public List<int> paimaifenzu = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.AvatarJsonData.list)
		{
			try
			{
				AvatarJsonData avatarJsonData = new AvatarJsonData();
				avatarJsonData.id = item["id"].I;
				avatarJsonData.face = item["face"].I;
				avatarJsonData.fightFace = item["fightFace"].I;
				avatarJsonData.SexType = item["SexType"].I;
				avatarJsonData.AvatarType = item["AvatarType"].I;
				avatarJsonData.Level = item["Level"].I;
				avatarJsonData.HP = item["HP"].I;
				avatarJsonData.dunSu = item["dunSu"].I;
				avatarJsonData.ziZhi = item["ziZhi"].I;
				avatarJsonData.wuXin = item["wuXin"].I;
				avatarJsonData.shengShi = item["shengShi"].I;
				avatarJsonData.shaQi = item["shaQi"].I;
				avatarJsonData.shouYuan = item["shouYuan"].I;
				avatarJsonData.age = item["age"].I;
				avatarJsonData.equipWeapon = item["equipWeapon"].I;
				avatarJsonData.equipClothing = item["equipClothing"].I;
				avatarJsonData.equipRing = item["equipRing"].I;
				avatarJsonData.yuanying = item["yuanying"].I;
				avatarJsonData.HuaShenLingYu = item["HuaShenLingYu"].I;
				avatarJsonData.MoneyType = item["MoneyType"].I;
				avatarJsonData.IsRefresh = item["IsRefresh"].I;
				avatarJsonData.dropType = item["dropType"].I;
				avatarJsonData.canjiaPaiMai = item["canjiaPaiMai"].I;
				avatarJsonData.wudaoType = item["wudaoType"].I;
				avatarJsonData.XinQuType = item["XinQuType"].I;
				avatarJsonData.gudingjiage = item["gudingjiage"].I;
				avatarJsonData.sellPercent = item["sellPercent"].I;
				avatarJsonData.Title = item["Title"].Str;
				avatarJsonData.FirstName = item["FirstName"].Str;
				avatarJsonData.Name = item["Name"].Str;
				avatarJsonData.menPai = item["menPai"].Str;
				avatarJsonData.LingGen = item["LingGen"].ToList();
				avatarJsonData.skills = item["skills"].ToList();
				avatarJsonData.staticSkills = item["staticSkills"].ToList();
				avatarJsonData.paimaifenzu = item["paimaifenzu"].ToList();
				if (DataDict.ContainsKey(avatarJsonData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典AvatarJsonData.DataDict添加数据时出现重复的键，Key:{avatarJsonData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(avatarJsonData.id, avatarJsonData);
				DataList.Add(avatarJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典AvatarJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
