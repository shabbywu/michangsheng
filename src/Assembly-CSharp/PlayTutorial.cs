using System;
using System.Collections.Generic;
using Fungus;
using GUIPackage;
using JSONClass;
using KBEngine;
using UnityEngine;

// Token: 0x02000539 RID: 1337
public static class PlayTutorial
{
	// Token: 0x170002AF RID: 687
	// (get) Token: 0x0600220B RID: 8715 RVA: 0x000132CA File Offset: 0x000114CA
	public static Avatar Player
	{
		get
		{
			return Tools.instance.getPlayer();
		}
	}

	// Token: 0x0600220C RID: 8716 RVA: 0x0011A2C8 File Offset: 0x001184C8
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

	// Token: 0x0600220D RID: 8717 RVA: 0x0001BEDA File Offset: 0x0001A0DA
	public static void CheckSkillTask()
	{
		PlayTutorial.CheckXiuLianZhiLu1();
	}

	// Token: 0x0600220E RID: 8718 RVA: 0x0001BEE1 File Offset: 0x0001A0E1
	public static void CheckGongFaTask()
	{
		PlayTutorial.CheckXiuLianZhiLu2();
		PlayTutorial.CheckXiuLianZhiLu3();
		PlayTutorial.CheckXiuLianZhiLu4();
		PlayTutorial.CheckXiuLianZhiLu5();
		PlayTutorial.CheckXiuLianZhiLu6();
		PlayTutorial.CheckXiuLianZhiLu7();
	}

	// Token: 0x0600220F RID: 8719 RVA: 0x0001BF01 File Offset: 0x0001A101
	public static bool IsFinished(string taskid)
	{
		return PlayTutorial.Player.PlayTutorialData[taskid].b;
	}

	// Token: 0x06002210 RID: 8720 RVA: 0x0001BF18 File Offset: 0x0001A118
	public static void SetFinish(string taskid, bool finish = true)
	{
		Debug.Log(string.Format("设置任务{0}的状态为{1}", taskid, finish));
		PlayTutorial.Player.PlayTutorialData.SetField(taskid, finish);
	}

	// Token: 0x06002211 RID: 8721 RVA: 0x0001BF41 File Offset: 0x0001A141
	public static void Test()
	{
		PlayTutorial.Player.taskMag.addTask(67);
		PlayTutorial.FinishTaskIndex(67, 1);
	}

	// Token: 0x06002212 RID: 8722 RVA: 0x0001BF5C File Offset: 0x0001A15C
	public static void FinishTaskIndex(int taskID, int index)
	{
		SetTaskIndexFinish.Do(taskID, index);
		PlayTutorial.Player.taskMag.setTaskIndex(taskID, index + 1);
	}

	// Token: 0x06002213 RID: 8723 RVA: 0x0011A400 File Offset: 0x00118600
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

	// Token: 0x06002214 RID: 8724 RVA: 0x0001BF78 File Offset: 0x0001A178
	public static bool IsCanCheck(int taskID, int index)
	{
		PlayTutorial.Init();
		return PlayTutorial.Player.taskMag.isHasTask(taskID) && PlayTutorial.Player.taskMag.GetTaskNowIndex(taskID) == index;
	}

	// Token: 0x06002215 RID: 8725 RVA: 0x0001BFA9 File Offset: 0x0001A1A9
	private static void UpdateXiuLianZhiLuTaskIndex()
	{
		PlayTutorial.UpdateTaskIndex("T61_", 61, 7);
	}

	// Token: 0x06002216 RID: 8726 RVA: 0x0011A474 File Offset: 0x00118674
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

	// Token: 0x06002217 RID: 8727 RVA: 0x0011A520 File Offset: 0x00118720
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

	// Token: 0x06002218 RID: 8728 RVA: 0x0011A62C File Offset: 0x0011882C
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

	// Token: 0x06002219 RID: 8729 RVA: 0x0011A6E4 File Offset: 0x001188E4
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

	// Token: 0x0600221A RID: 8730 RVA: 0x0011A7C8 File Offset: 0x001189C8
	private static bool CheckZhuXiuGongFaSpeed(int targetSpeed)
	{
		int staticID = PlayTutorial.Player.getStaticID();
		return staticID != 0 && jsonData.instance.StaticSkillJsonData[staticID.ToString()]["Skill_Speed"].I >= targetSpeed;
	}

	// Token: 0x0600221B RID: 8731 RVA: 0x0011A810 File Offset: 0x00118A10
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

	// Token: 0x0600221C RID: 8732 RVA: 0x0011A860 File Offset: 0x00118A60
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

	// Token: 0x0600221D RID: 8733 RVA: 0x0011A91C File Offset: 0x00118B1C
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

	// Token: 0x0600221E RID: 8734 RVA: 0x0001BFB8 File Offset: 0x0001A1B8
	private static void UpdateGanWuTianDiTaskIndex()
	{
		PlayTutorial.UpdateTaskIndex("T62_", 62, 4);
	}

	// Token: 0x0600221F RID: 8735 RVA: 0x0001BFC7 File Offset: 0x0001A1C7
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

	// Token: 0x06002220 RID: 8736 RVA: 0x0011A96C File Offset: 0x00118B6C
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

	// Token: 0x06002221 RID: 8737 RVA: 0x0011A9DC File Offset: 0x00118BDC
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

	// Token: 0x06002222 RID: 8738 RVA: 0x0011AA2C File Offset: 0x00118C2C
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

	// Token: 0x06002223 RID: 8739 RVA: 0x0011AA88 File Offset: 0x00118C88
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

	// Token: 0x06002224 RID: 8740 RVA: 0x0001BFFF File Offset: 0x0001A1FF
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

	// Token: 0x06002225 RID: 8741 RVA: 0x0011AAD8 File Offset: 0x00118CD8
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

	// Token: 0x06002226 RID: 8742 RVA: 0x0011AB68 File Offset: 0x00118D68
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

	// Token: 0x06002227 RID: 8743 RVA: 0x0001C020 File Offset: 0x0001A220
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

	// Token: 0x06002228 RID: 8744 RVA: 0x0011AC04 File Offset: 0x00118E04
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

	// Token: 0x06002229 RID: 8745 RVA: 0x0001C044 File Offset: 0x0001A244
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

	// Token: 0x0600222A RID: 8746 RVA: 0x0001C068 File Offset: 0x0001A268
	public static void CheckCaoYao2()
	{
		if (!PlayTutorial.IsCanCheck(67, 2))
		{
			return;
		}
		SetTaskIndexFinish.Do(67, 2);
		SetTaskCompelet.Do(67);
	}

	// Token: 0x04001D92 RID: 7570
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

	// Token: 0x04001D93 RID: 7571
	private static List<int> ChuShiGongFa = new List<int>
	{
		501,
		536
	};
}
