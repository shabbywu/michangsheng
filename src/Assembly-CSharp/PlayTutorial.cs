using System.Collections.Generic;
using Fungus;
using GUIPackage;
using JSONClass;
using KBEngine;
using UnityEngine;

public static class PlayTutorial
{
	private static List<int> ChuShiShenTong = new List<int> { 1, 101, 201, 301, 401, 501, 519 };

	private static List<int> ChuShiGongFa = new List<int> { 501, 536 };

	public static Avatar Player => Tools.instance.getPlayer();

	private static void Init()
	{
		if (!Player.PlayTutorialData.HasField("T61_1"))
		{
			Player.PlayTutorialData.SetField("T61_1", val: false);
			Player.PlayTutorialData.SetField("T61_2", val: false);
			Player.PlayTutorialData.SetField("T61_3", val: false);
			Player.PlayTutorialData.SetField("T61_4", val: false);
			Player.PlayTutorialData.SetField("T61_5", val: false);
			Player.PlayTutorialData.SetField("T61_6", val: false);
			Player.PlayTutorialData.SetField("T61_7", val: false);
			Player.PlayTutorialData.SetField("T62_1", val: false);
			Player.PlayTutorialData.SetField("T62_2", val: false);
			Player.PlayTutorialData.SetField("T62_3", val: false);
			Player.PlayTutorialData.SetField("T62_4", val: false);
			Player.PlayTutorialData.SetField("T64_1", val: false);
			Player.PlayTutorialData.SetField("T64_2", val: false);
		}
	}

	public static void CheckSkillTask()
	{
		CheckXiuLianZhiLu1();
	}

	public static void CheckGongFaTask()
	{
		CheckXiuLianZhiLu2();
		CheckXiuLianZhiLu3();
		CheckXiuLianZhiLu4();
		CheckXiuLianZhiLu5();
		CheckXiuLianZhiLu6();
		CheckXiuLianZhiLu7();
	}

	public static bool IsFinished(string taskid)
	{
		return Player.PlayTutorialData[taskid].b;
	}

	public static void SetFinish(string taskid, bool finish = true)
	{
		Debug.Log((object)$"设置任务{taskid}的状态为{finish}");
		Player.PlayTutorialData.SetField(taskid, finish);
	}

	public static void Test()
	{
		Player.taskMag.addTask(67);
		FinishTaskIndex(67, 1);
	}

	public static void FinishTaskIndex(int taskID, int index)
	{
		SetTaskIndexFinish.Do(taskID, index);
		Player.taskMag.setTaskIndex(taskID, index + 1);
	}

	private static void UpdateTaskIndex(string taskIdPre, int taskId, int taskCount)
	{
		int num = 0;
		for (int i = 1; i <= taskCount && IsFinished($"{taskIdPre}{i}"); i++)
		{
			num = i;
		}
		if (num == taskCount)
		{
			SetTaskIndexFinish.Do(taskId, num);
			SetTaskCompelet.Do(taskId);
			return;
		}
		int taskNowIndex = Player.taskMag.GetTaskNowIndex(taskId);
		if (num > 0 && taskNowIndex <= num)
		{
			for (int j = taskNowIndex; j <= num; j++)
			{
				FinishTaskIndex(taskId, j);
			}
		}
	}

	public static bool IsCanCheck(int taskID, int index)
	{
		Init();
		if (!Player.taskMag.isHasTask(taskID))
		{
			return false;
		}
		if (Player.taskMag.GetTaskNowIndex(taskID) != index)
		{
			return false;
		}
		return true;
	}

	private static void UpdateXiuLianZhiLuTaskIndex()
	{
		UpdateTaskIndex("T61_", 61, 7);
	}

	public static void CheckXiuLianZhiLu1()
	{
		Init();
		if (!Player.taskMag.isHasTask(61) || IsFinished("T61_1") || Player.hasSkillList.Count <= 7)
		{
			return;
		}
		foreach (SkillItem equipSkill in Player.equipSkillList)
		{
			if (!ChuShiShenTong.Contains(equipSkill.itemId))
			{
				SetFinish("T61_1");
				UpdateXiuLianZhiLuTaskIndex();
				break;
			}
		}
	}

	public static void CheckXiuLianZhiLu2()
	{
		Init();
		if (!Player.taskMag.isHasTask(61) || IsFinished("T61_2") || Player.hasStaticSkillList.Count <= 1)
		{
			return;
		}
		SkillStaticDatebase component = ((Component)jsonData.instance).gameObject.GetComponent<SkillStaticDatebase>();
		foreach (SkillItem equipStaticSkill in Player.equipStaticSkillList)
		{
			if (!ChuShiGongFa.Contains(equipStaticSkill.itemId))
			{
				int skill_ID = component.Dict[equipStaticSkill.itemId][equipStaticSkill.level].skill_ID;
				if (jsonData.instance.StaticSkillJsonData[skill_ID.ToString()]["AttackType"].I != 6)
				{
					SetFinish("T61_2");
					UpdateXiuLianZhiLuTaskIndex();
					break;
				}
			}
		}
	}

	public static void CheckXiuLianZhiLu3()
	{
		Init();
		if (!Player.taskMag.isHasTask(61) || IsFinished("T61_3"))
		{
			return;
		}
		foreach (SkillItem hasStaticSkill in Player.hasStaticSkillList)
		{
			Debug.Log((object)$"{hasStaticSkill.itemId} {hasStaticSkill.level}");
			if (hasStaticSkill.level > 1)
			{
				SetFinish("T61_3");
				UpdateXiuLianZhiLuTaskIndex();
				break;
			}
		}
	}

	public static void CheckXiuLianZhiLu4()
	{
		Init();
		if (!Player.taskMag.isHasTask(61) || IsFinished("T61_4"))
		{
			return;
		}
		SkillStaticDatebase component = ((Component)jsonData.instance).gameObject.GetComponent<SkillStaticDatebase>();
		foreach (SkillItem equipStaticSkill in Player.equipStaticSkillList)
		{
			int skill_ID = component.Dict[equipStaticSkill.itemId][equipStaticSkill.level].skill_ID;
			if (jsonData.instance.StaticSkillJsonData[skill_ID.ToString()]["AttackType"].I == 6)
			{
				SetFinish("T61_4");
				UpdateXiuLianZhiLuTaskIndex();
				break;
			}
		}
	}

	private static bool CheckZhuXiuGongFaSpeed(int targetSpeed)
	{
		int staticID = Player.getStaticID();
		if (staticID == 0)
		{
			return false;
		}
		if (jsonData.instance.StaticSkillJsonData[staticID.ToString()]["Skill_Speed"].I >= targetSpeed)
		{
			return true;
		}
		return false;
	}

	public static void CheckXiuLianZhiLu5()
	{
		Init();
		if (Player.taskMag.isHasTask(61) && !IsFinished("T61_5") && CheckZhuXiuGongFaSpeed(300))
		{
			SetFinish("T61_5");
			UpdateXiuLianZhiLuTaskIndex();
		}
	}

	public static void CheckXiuLianZhiLu6()
	{
		Init();
		if (!Player.taskMag.isHasTask(61) || IsFinished("T61_6"))
		{
			return;
		}
		SkillStaticDatebase component = ((Component)jsonData.instance).gameObject.GetComponent<SkillStaticDatebase>();
		foreach (SkillItem equipStaticSkill in Player.equipStaticSkillList)
		{
			if (component.Dict[equipStaticSkill.itemId][equipStaticSkill.level].SkillQuality == 2)
			{
				SetFinish("T61_6");
				UpdateXiuLianZhiLuTaskIndex();
				break;
			}
		}
	}

	public static void CheckXiuLianZhiLu7()
	{
		Init();
		if (Player.taskMag.isHasTask(61) && !IsFinished("T61_7") && CheckZhuXiuGongFaSpeed(1000))
		{
			SetFinish("T61_7");
			UpdateXiuLianZhiLuTaskIndex();
		}
	}

	private static void UpdateGanWuTianDiTaskIndex()
	{
		UpdateTaskIndex("T62_", 62, 4);
	}

	public static void CheckGanWuTianDi1()
	{
		Init();
		if (Player.taskMag.isHasTask(62) && !IsFinished("T62_1"))
		{
			SetFinish("T62_1");
			UpdateGanWuTianDiTaskIndex();
		}
	}

	public static bool CheckWuDaoExp(int targetExp)
	{
		foreach (JSONObject item in Player.WuDaoJson.list)
		{
			if (item["ex"].I >= targetExp)
			{
				return true;
			}
		}
		return false;
	}

	public static void CheckGanWuTianDi2()
	{
		Init();
		if (Player.taskMag.isHasTask(62) && !IsFinished("T62_2") && CheckWuDaoExp(1000))
		{
			SetFinish("T62_2");
			UpdateGanWuTianDiTaskIndex();
		}
	}

	public static void CheckGanWuTianDi3()
	{
		Init();
		if (Player.taskMag.isHasTask(62) && !IsFinished("T62_3") && Player.wuDaoMag.GetAllWuDaoSkills().Count > 0)
		{
			SetFinish("T62_3");
			UpdateGanWuTianDiTaskIndex();
		}
	}

	public static void CheckGanWuTianDi4()
	{
		Init();
		if (Player.taskMag.isHasTask(62) && !IsFinished("T62_4") && CheckWuDaoExp(8000))
		{
			SetFinish("T62_4");
			UpdateGanWuTianDiTaskIndex();
		}
	}

	public static void CheckChuTaXianTu2(int taskID)
	{
		if (IsCanCheck(64, 2) && ZhuChengRenWu.DataDict.ContainsKey(taskID))
		{
			SetTaskCompelet.Do(64);
		}
	}

	public static void CheckLianDan2(int itemID, List<LianDanResultManager.DanyaoItem> yaocaiList)
	{
		if (!IsCanCheck(65, 2) || itemID != 5003)
		{
			return;
		}
		string text = "";
		foreach (LianDanResultManager.DanyaoItem yaocai in yaocaiList)
		{
			text += $"{yaocai}|";
		}
		Player.PlayTutorialData.SetField("65_2_yaocai", text);
		FinishTaskIndex(65, 2);
	}

	public static void CheckLianDan3(int itemID, List<LianDanResultManager.DanyaoItem> yaocaiList)
	{
		if (!IsCanCheck(65, 3) || itemID != 5003)
		{
			return;
		}
		string text = "";
		foreach (LianDanResultManager.DanyaoItem yaocai in yaocaiList)
		{
			text += $"{yaocai}|";
		}
		if (Player.PlayTutorialData["65_2_yaocai"].str != text)
		{
			FinishTaskIndex(65, 3);
		}
	}

	public static void CheckLianDan4(int itemID)
	{
		if (IsCanCheck(65, 4) && itemID == 5101)
		{
			SetTaskIndexFinish.Do(65, 4);
			SetTaskCompelet.Do(65);
		}
	}

	public static void CheckLianQi2()
	{
		if (IsCanCheck(66, 2) && Player.WuDaoJson.HasField("22") && Player.WuDaoJson["22"]["ex"].I >= 1000)
		{
			FinishTaskIndex(66, 2);
		}
	}

	public static void CheckLianQi3(int quality, int shangxia)
	{
		if (IsCanCheck(66, 3) && quality == 1 && shangxia == 3)
		{
			SetTaskIndexFinish.Do(66, 3);
			SetTaskCompelet.Do(66);
		}
	}

	public static void CheckCaoYao2()
	{
		if (IsCanCheck(67, 2))
		{
			SetTaskIndexFinish.Do(67, 2);
			SetTaskCompelet.Do(67);
		}
	}
}
