using System;
using System.Collections.Generic;
using Fungus;
using GUIPackage;
using JSONClass;
using KBEngine;
using UnityEngine;

// Token: 0x020003AE RID: 942
public static class PlayTutorial
{
	// Token: 0x1700025D RID: 605
	// (get) Token: 0x06001E88 RID: 7816 RVA: 0x0006EC50 File Offset: 0x0006CE50
	public static Avatar Player
	{
		get
		{
			return Tools.instance.getPlayer();
		}
	}

	// Token: 0x06001E89 RID: 7817 RVA: 0x000D6C14 File Offset: 0x000D4E14
	private static void Init()
	{
		if (!PlayTutorial.Player.PlayTutorialData.HasField("T61_1"))
		{
			PlayTutorial.Player.PlayTutorialData.SetField("T61_1", false);
			PlayTutorial.Player.PlayTutorialData.SetField("T61_2", false);
			PlayTutorial.Player.PlayTutorialData.SetField("T61_3", false);
			PlayTutorial.Player.PlayTutorialData.SetField("T61_4", false);
			PlayTutorial.Player.PlayTutorialData.SetField("T61_5", false);
			PlayTutorial.Player.PlayTutorialData.SetField("T61_6", false);
			PlayTutorial.Player.PlayTutorialData.SetField("T61_7", false);
			PlayTutorial.Player.PlayTutorialData.SetField("T62_1", false);
			PlayTutorial.Player.PlayTutorialData.SetField("T62_2", false);
			PlayTutorial.Player.PlayTutorialData.SetField("T62_3", false);
			PlayTutorial.Player.PlayTutorialData.SetField("T62_4", false);
			PlayTutorial.Player.PlayTutorialData.SetField("T64_1", false);
			PlayTutorial.Player.PlayTutorialData.SetField("T64_2", false);
		}
	}

	// Token: 0x06001E8A RID: 7818 RVA: 0x000D6D4B File Offset: 0x000D4F4B
	public static void CheckSkillTask()
	{
		PlayTutorial.CheckXiuLianZhiLu1();
	}

	// Token: 0x06001E8B RID: 7819 RVA: 0x000D6D52 File Offset: 0x000D4F52
	public static void CheckGongFaTask()
	{
		PlayTutorial.CheckXiuLianZhiLu2();
		PlayTutorial.CheckXiuLianZhiLu3();
		PlayTutorial.CheckXiuLianZhiLu4();
		PlayTutorial.CheckXiuLianZhiLu5();
		PlayTutorial.CheckXiuLianZhiLu6();
		PlayTutorial.CheckXiuLianZhiLu7();
	}

	// Token: 0x06001E8C RID: 7820 RVA: 0x000D6D72 File Offset: 0x000D4F72
	public static bool IsFinished(string taskid)
	{
		return PlayTutorial.Player.PlayTutorialData[taskid].b;
	}

	// Token: 0x06001E8D RID: 7821 RVA: 0x000D6D89 File Offset: 0x000D4F89
	public static void SetFinish(string taskid, bool finish = true)
	{
		Debug.Log(string.Format("设置任务{0}的状态为{1}", taskid, finish));
		PlayTutorial.Player.PlayTutorialData.SetField(taskid, finish);
	}

	// Token: 0x06001E8E RID: 7822 RVA: 0x000D6DB2 File Offset: 0x000D4FB2
	public static void Test()
	{
		PlayTutorial.Player.taskMag.addTask(67);
		PlayTutorial.FinishTaskIndex(67, 1);
	}

	// Token: 0x06001E8F RID: 7823 RVA: 0x000D6DCD File Offset: 0x000D4FCD
	public static void FinishTaskIndex(int taskID, int index)
	{
		SetTaskIndexFinish.Do(taskID, index);
		PlayTutorial.Player.taskMag.setTaskIndex(taskID, index + 1);
	}

	// Token: 0x06001E90 RID: 7824 RVA: 0x000D6DEC File Offset: 0x000D4FEC
	private static void UpdateTaskIndex(string taskIdPre, int taskId, int taskCount)
	{
		int num = 0;
		int num2 = 1;
		while (num2 <= taskCount && PlayTutorial.IsFinished(string.Format("{0}{1}", taskIdPre, num2)))
		{
			num = num2;
			num2++;
		}
		if (num == taskCount)
		{
			SetTaskIndexFinish.Do(taskId, num);
			SetTaskCompelet.Do(taskId);
			return;
		}
		int taskNowIndex = PlayTutorial.Player.taskMag.GetTaskNowIndex(taskId);
		if (num > 0 && taskNowIndex <= num)
		{
			for (int i = taskNowIndex; i <= num; i++)
			{
				PlayTutorial.FinishTaskIndex(taskId, i);
			}
		}
	}

	// Token: 0x06001E91 RID: 7825 RVA: 0x000D6E5F File Offset: 0x000D505F
	public static bool IsCanCheck(int taskID, int index)
	{
		PlayTutorial.Init();
		return PlayTutorial.Player.taskMag.isHasTask(taskID) && PlayTutorial.Player.taskMag.GetTaskNowIndex(taskID) == index;
	}

	// Token: 0x06001E92 RID: 7826 RVA: 0x000D6E90 File Offset: 0x000D5090
	private static void UpdateXiuLianZhiLuTaskIndex()
	{
		PlayTutorial.UpdateTaskIndex("T61_", 61, 7);
	}

	// Token: 0x06001E93 RID: 7827 RVA: 0x000D6EA0 File Offset: 0x000D50A0
	public static void CheckXiuLianZhiLu1()
	{
		PlayTutorial.Init();
		if (!PlayTutorial.Player.taskMag.isHasTask(61))
		{
			return;
		}
		if (PlayTutorial.IsFinished("T61_1"))
		{
			return;
		}
		if (PlayTutorial.Player.hasSkillList.Count > 7)
		{
			foreach (SkillItem skillItem in PlayTutorial.Player.equipSkillList)
			{
				if (!PlayTutorial.ChuShiShenTong.Contains(skillItem.itemId))
				{
					PlayTutorial.SetFinish("T61_1", true);
					PlayTutorial.UpdateXiuLianZhiLuTaskIndex();
					break;
				}
			}
		}
	}

	// Token: 0x06001E94 RID: 7828 RVA: 0x000D6F4C File Offset: 0x000D514C
	public static void CheckXiuLianZhiLu2()
	{
		PlayTutorial.Init();
		if (!PlayTutorial.Player.taskMag.isHasTask(61))
		{
			return;
		}
		if (PlayTutorial.IsFinished("T61_2"))
		{
			return;
		}
		if (PlayTutorial.Player.hasStaticSkillList.Count > 1)
		{
			SkillStaticDatebase component = jsonData.instance.gameObject.GetComponent<SkillStaticDatebase>();
			foreach (SkillItem skillItem in PlayTutorial.Player.equipStaticSkillList)
			{
				if (!PlayTutorial.ChuShiGongFa.Contains(skillItem.itemId))
				{
					int skill_ID = component.Dict[skillItem.itemId][skillItem.level].skill_ID;
					if (jsonData.instance.StaticSkillJsonData[skill_ID.ToString()]["AttackType"].I != 6)
					{
						PlayTutorial.SetFinish("T61_2", true);
						PlayTutorial.UpdateXiuLianZhiLuTaskIndex();
						break;
					}
				}
			}
		}
	}

	// Token: 0x06001E95 RID: 7829 RVA: 0x000D7058 File Offset: 0x000D5258
	public static void CheckXiuLianZhiLu3()
	{
		PlayTutorial.Init();
		if (!PlayTutorial.Player.taskMag.isHasTask(61))
		{
			return;
		}
		if (PlayTutorial.IsFinished("T61_3"))
		{
			return;
		}
		foreach (SkillItem skillItem in PlayTutorial.Player.hasStaticSkillList)
		{
			Debug.Log(string.Format("{0} {1}", skillItem.itemId, skillItem.level));
			if (skillItem.level > 1)
			{
				PlayTutorial.SetFinish("T61_3", true);
				PlayTutorial.UpdateXiuLianZhiLuTaskIndex();
				break;
			}
		}
	}

	// Token: 0x06001E96 RID: 7830 RVA: 0x000D7110 File Offset: 0x000D5310
	public static void CheckXiuLianZhiLu4()
	{
		PlayTutorial.Init();
		if (!PlayTutorial.Player.taskMag.isHasTask(61))
		{
			return;
		}
		if (PlayTutorial.IsFinished("T61_4"))
		{
			return;
		}
		SkillStaticDatebase component = jsonData.instance.gameObject.GetComponent<SkillStaticDatebase>();
		foreach (SkillItem skillItem in PlayTutorial.Player.equipStaticSkillList)
		{
			int skill_ID = component.Dict[skillItem.itemId][skillItem.level].skill_ID;
			if (jsonData.instance.StaticSkillJsonData[skill_ID.ToString()]["AttackType"].I == 6)
			{
				PlayTutorial.SetFinish("T61_4", true);
				PlayTutorial.UpdateXiuLianZhiLuTaskIndex();
				break;
			}
		}
	}

	// Token: 0x06001E97 RID: 7831 RVA: 0x000D71F4 File Offset: 0x000D53F4
	private static bool CheckZhuXiuGongFaSpeed(int targetSpeed)
	{
		int staticID = PlayTutorial.Player.getStaticID();
		return staticID != 0 && jsonData.instance.StaticSkillJsonData[staticID.ToString()]["Skill_Speed"].I >= targetSpeed;
	}

	// Token: 0x06001E98 RID: 7832 RVA: 0x000D723C File Offset: 0x000D543C
	public static void CheckXiuLianZhiLu5()
	{
		PlayTutorial.Init();
		if (!PlayTutorial.Player.taskMag.isHasTask(61))
		{
			return;
		}
		if (PlayTutorial.IsFinished("T61_5"))
		{
			return;
		}
		if (PlayTutorial.CheckZhuXiuGongFaSpeed(300))
		{
			PlayTutorial.SetFinish("T61_5", true);
			PlayTutorial.UpdateXiuLianZhiLuTaskIndex();
		}
	}

	// Token: 0x06001E99 RID: 7833 RVA: 0x000D728C File Offset: 0x000D548C
	public static void CheckXiuLianZhiLu6()
	{
		PlayTutorial.Init();
		if (!PlayTutorial.Player.taskMag.isHasTask(61))
		{
			return;
		}
		if (PlayTutorial.IsFinished("T61_6"))
		{
			return;
		}
		SkillStaticDatebase component = jsonData.instance.gameObject.GetComponent<SkillStaticDatebase>();
		foreach (SkillItem skillItem in PlayTutorial.Player.equipStaticSkillList)
		{
			if (component.Dict[skillItem.itemId][skillItem.level].SkillQuality == 2)
			{
				PlayTutorial.SetFinish("T61_6", true);
				PlayTutorial.UpdateXiuLianZhiLuTaskIndex();
				break;
			}
		}
	}

	// Token: 0x06001E9A RID: 7834 RVA: 0x000D7348 File Offset: 0x000D5548
	public static void CheckXiuLianZhiLu7()
	{
		PlayTutorial.Init();
		if (!PlayTutorial.Player.taskMag.isHasTask(61))
		{
			return;
		}
		if (PlayTutorial.IsFinished("T61_7"))
		{
			return;
		}
		if (PlayTutorial.CheckZhuXiuGongFaSpeed(1000))
		{
			PlayTutorial.SetFinish("T61_7", true);
			PlayTutorial.UpdateXiuLianZhiLuTaskIndex();
		}
	}

	// Token: 0x06001E9B RID: 7835 RVA: 0x000D7397 File Offset: 0x000D5597
	private static void UpdateGanWuTianDiTaskIndex()
	{
		PlayTutorial.UpdateTaskIndex("T62_", 62, 4);
	}

	// Token: 0x06001E9C RID: 7836 RVA: 0x000D73A6 File Offset: 0x000D55A6
	public static void CheckGanWuTianDi1()
	{
		PlayTutorial.Init();
		if (!PlayTutorial.Player.taskMag.isHasTask(62))
		{
			return;
		}
		if (PlayTutorial.IsFinished("T62_1"))
		{
			return;
		}
		PlayTutorial.SetFinish("T62_1", true);
		PlayTutorial.UpdateGanWuTianDiTaskIndex();
	}

	// Token: 0x06001E9D RID: 7837 RVA: 0x000D73E0 File Offset: 0x000D55E0
	public static bool CheckWuDaoExp(int targetExp)
	{
		using (List<JSONObject>.Enumerator enumerator = PlayTutorial.Player.WuDaoJson.list.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current["ex"].I >= targetExp)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06001E9E RID: 7838 RVA: 0x000D7450 File Offset: 0x000D5650
	public static void CheckGanWuTianDi2()
	{
		PlayTutorial.Init();
		if (!PlayTutorial.Player.taskMag.isHasTask(62))
		{
			return;
		}
		if (PlayTutorial.IsFinished("T62_2"))
		{
			return;
		}
		if (PlayTutorial.CheckWuDaoExp(1000))
		{
			PlayTutorial.SetFinish("T62_2", true);
			PlayTutorial.UpdateGanWuTianDiTaskIndex();
		}
	}

	// Token: 0x06001E9F RID: 7839 RVA: 0x000D74A0 File Offset: 0x000D56A0
	public static void CheckGanWuTianDi3()
	{
		PlayTutorial.Init();
		if (!PlayTutorial.Player.taskMag.isHasTask(62))
		{
			return;
		}
		if (PlayTutorial.IsFinished("T62_3"))
		{
			return;
		}
		if (PlayTutorial.Player.wuDaoMag.GetAllWuDaoSkills().Count > 0)
		{
			PlayTutorial.SetFinish("T62_3", true);
			PlayTutorial.UpdateGanWuTianDiTaskIndex();
		}
	}

	// Token: 0x06001EA0 RID: 7840 RVA: 0x000D74FC File Offset: 0x000D56FC
	public static void CheckGanWuTianDi4()
	{
		PlayTutorial.Init();
		if (!PlayTutorial.Player.taskMag.isHasTask(62))
		{
			return;
		}
		if (PlayTutorial.IsFinished("T62_4"))
		{
			return;
		}
		if (PlayTutorial.CheckWuDaoExp(8000))
		{
			PlayTutorial.SetFinish("T62_4", true);
			PlayTutorial.UpdateGanWuTianDiTaskIndex();
		}
	}

	// Token: 0x06001EA1 RID: 7841 RVA: 0x000D754B File Offset: 0x000D574B
	public static void CheckChuTaXianTu2(int taskID)
	{
		if (!PlayTutorial.IsCanCheck(64, 2))
		{
			return;
		}
		if (ZhuChengRenWu.DataDict.ContainsKey(taskID))
		{
			SetTaskCompelet.Do(64);
		}
	}

	// Token: 0x06001EA2 RID: 7842 RVA: 0x000D756C File Offset: 0x000D576C
	public static void CheckLianDan2(int itemID, List<LianDanResultManager.DanyaoItem> yaocaiList)
	{
		if (!PlayTutorial.IsCanCheck(65, 2))
		{
			return;
		}
		if (itemID == 5003)
		{
			string text = "";
			foreach (LianDanResultManager.DanyaoItem arg in yaocaiList)
			{
				text += string.Format("{0}|", arg);
			}
			PlayTutorial.Player.PlayTutorialData.SetField("65_2_yaocai", text);
			PlayTutorial.FinishTaskIndex(65, 2);
		}
	}

	// Token: 0x06001EA3 RID: 7843 RVA: 0x000D75FC File Offset: 0x000D57FC
	public static void CheckLianDan3(int itemID, List<LianDanResultManager.DanyaoItem> yaocaiList)
	{
		if (!PlayTutorial.IsCanCheck(65, 3))
		{
			return;
		}
		if (itemID == 5003)
		{
			string text = "";
			foreach (LianDanResultManager.DanyaoItem arg in yaocaiList)
			{
				text += string.Format("{0}|", arg);
			}
			if (PlayTutorial.Player.PlayTutorialData["65_2_yaocai"].str != text)
			{
				PlayTutorial.FinishTaskIndex(65, 3);
			}
		}
	}

	// Token: 0x06001EA4 RID: 7844 RVA: 0x000D7698 File Offset: 0x000D5898
	public static void CheckLianDan4(int itemID)
	{
		if (!PlayTutorial.IsCanCheck(65, 4))
		{
			return;
		}
		if (itemID == 5101)
		{
			SetTaskIndexFinish.Do(65, 4);
			SetTaskCompelet.Do(65);
		}
	}

	// Token: 0x06001EA5 RID: 7845 RVA: 0x000D76BC File Offset: 0x000D58BC
	public static void CheckLianQi2()
	{
		if (!PlayTutorial.IsCanCheck(66, 2))
		{
			return;
		}
		if (PlayTutorial.Player.WuDaoJson.HasField("22") && PlayTutorial.Player.WuDaoJson["22"]["ex"].I >= 1000)
		{
			PlayTutorial.FinishTaskIndex(66, 2);
		}
	}

	// Token: 0x06001EA6 RID: 7846 RVA: 0x000D771C File Offset: 0x000D591C
	public static void CheckLianQi3(int quality, int shangxia)
	{
		if (!PlayTutorial.IsCanCheck(66, 3))
		{
			return;
		}
		if (quality == 1 && shangxia == 3)
		{
			SetTaskIndexFinish.Do(66, 3);
			SetTaskCompelet.Do(66);
		}
	}

	// Token: 0x06001EA7 RID: 7847 RVA: 0x000D7740 File Offset: 0x000D5940
	public static void CheckCaoYao2()
	{
		if (!PlayTutorial.IsCanCheck(67, 2))
		{
			return;
		}
		SetTaskIndexFinish.Do(67, 2);
		SetTaskCompelet.Do(67);
	}

	// Token: 0x0400191E RID: 6430
	private static List<int> ChuShiShenTong = new List<int>
	{
		1,
		101,
		201,
		301,
		401,
		501,
		519
	};

	// Token: 0x0400191F RID: 6431
	private static List<int> ChuShiGongFa = new List<int>
	{
		501,
		536
	};
}
