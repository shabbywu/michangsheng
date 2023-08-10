using System;
using System.Collections.Generic;

namespace JSONClass;

public class NPCLeiXingDate : IJSONClass
{
	public static Dictionary<int, NPCLeiXingDate> DataDict = new Dictionary<int, NPCLeiXingDate>();

	public static List<NPCLeiXingDate> DataList = new List<NPCLeiXingDate>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int Type;

	public int LiuPai;

	public int MengPai;

	public int Level;

	public int yuanying;

	public int HuaShenLingYu;

	public int wudaoType;

	public int canjiaPaiMai;

	public int AvatarType;

	public int XinQuType;

	public int AttackType;

	public int DefenseType;

	public string FirstName;

	public List<int> skills = new List<int>();

	public List<int> staticSkills = new List<int>();

	public List<int> LingGen = new List<int>();

	public List<int> NPCTag = new List<int>();

	public List<int> paimaifenzu = new List<int>();

	public List<int> equipWeapon = new List<int>();

	public List<int> equipClothing = new List<int>();

	public List<int> equipRing = new List<int>();

	public List<int> JinDanType = new List<int>();

	public List<int> ShiLi = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.NPCLeiXingDate.list)
		{
			try
			{
				NPCLeiXingDate nPCLeiXingDate = new NPCLeiXingDate();
				nPCLeiXingDate.id = item["id"].I;
				nPCLeiXingDate.Type = item["Type"].I;
				nPCLeiXingDate.LiuPai = item["LiuPai"].I;
				nPCLeiXingDate.MengPai = item["MengPai"].I;
				nPCLeiXingDate.Level = item["Level"].I;
				nPCLeiXingDate.yuanying = item["yuanying"].I;
				nPCLeiXingDate.HuaShenLingYu = item["HuaShenLingYu"].I;
				nPCLeiXingDate.wudaoType = item["wudaoType"].I;
				nPCLeiXingDate.canjiaPaiMai = item["canjiaPaiMai"].I;
				nPCLeiXingDate.AvatarType = item["AvatarType"].I;
				nPCLeiXingDate.XinQuType = item["XinQuType"].I;
				nPCLeiXingDate.AttackType = item["AttackType"].I;
				nPCLeiXingDate.DefenseType = item["DefenseType"].I;
				nPCLeiXingDate.FirstName = item["FirstName"].Str;
				nPCLeiXingDate.skills = item["skills"].ToList();
				nPCLeiXingDate.staticSkills = item["staticSkills"].ToList();
				nPCLeiXingDate.LingGen = item["LingGen"].ToList();
				nPCLeiXingDate.NPCTag = item["NPCTag"].ToList();
				nPCLeiXingDate.paimaifenzu = item["paimaifenzu"].ToList();
				nPCLeiXingDate.equipWeapon = item["equipWeapon"].ToList();
				nPCLeiXingDate.equipClothing = item["equipClothing"].ToList();
				nPCLeiXingDate.equipRing = item["equipRing"].ToList();
				nPCLeiXingDate.JinDanType = item["JinDanType"].ToList();
				nPCLeiXingDate.ShiLi = item["ShiLi"].ToList();
				if (DataDict.ContainsKey(nPCLeiXingDate.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典NPCLeiXingDate.DataDict添加数据时出现重复的键，Key:{nPCLeiXingDate.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(nPCLeiXingDate.id, nPCLeiXingDate);
				DataList.Add(nPCLeiXingDate);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典NPCLeiXingDate.DataDict添加数据时出现异常，已跳过，请检查配表");
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
