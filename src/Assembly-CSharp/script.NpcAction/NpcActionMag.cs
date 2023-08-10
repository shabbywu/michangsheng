using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace script.NpcAction;

public class NpcActionMag : MonoBehaviour
{
	public static NpcActionMag Inst;

	public bool IsNoJieSuan;

	public int GroupNum;

	public int CompleteGroupNum;

	public List<NpcDataGroup> GroupList;

	public GroupPool Pool;

	public int NeedTimes;

	public bool IsFree;

	private void Awake()
	{
		Pool = new GroupPool();
		Inst = this;
		Object.DontDestroyOnLoad((Object)(object)((Component)Inst).gameObject);
	}

	public void GroupAction(int times, bool isCanChanger = true)
	{
		GroupNum = 0;
		CompleteGroupNum = 0;
		GroupList = new List<NpcDataGroup>();
		List<NpcData> list = new List<NpcData>();
		if (IsNoJieSuan)
		{
			IsNoJieSuan = false;
			return;
		}
		foreach (string key in jsonData.instance.AvatarJsonData.keys)
		{
			int num = int.Parse(key);
			if (num >= 20000)
			{
				NpcData npcData = new NpcData(num);
				if (npcData.IsInit)
				{
					list.Add(npcData);
				}
			}
		}
		if (list.Count == 0)
		{
			return;
		}
		int num2 = 1;
		if (list.Count >= Loom.maxThreads)
		{
			num2 = list.Count / Loom.maxThreads;
		}
		int num3 = 1;
		int num4 = 0;
		GroupList.Add(Pool.GetGroup());
		foreach (NpcData item in list)
		{
			if (num3 > num2 && GroupList.Count <= Loom.maxThreads)
			{
				num4++;
				num3 = 1;
				GroupList.Add(Pool.GetGroup());
			}
			GroupList[num4].NpcDict.Add(item.NpcId, item);
			num3++;
		}
		GroupNum = GroupList.Count;
		if (GroupNum > 0)
		{
			IsFree = false;
		}
		NeedTimes = times;
		BeforeAction();
	}

	private void BeforeAction()
	{
		Loom.RunAsync(delegate
		{
			try
			{
				NpcJieSuanManager.inst.PaiMaiAction();
				NpcJieSuanManager.inst.LunDaoAction();
				NpcJieSuanManager.inst.npcTeShu.NextJieSha();
				NpcJieSuanManager.inst.npcMap.RestartMap();
			}
			catch (Exception ex)
			{
				Debug.LogError((object)"BeforeAction出错");
				Debug.LogError((object)ex);
				throw;
			}
			Loom.QueueOnMainThread(delegate
			{
				StartAction();
			}, null);
		});
	}

	private void AfterAction()
	{
		Loom.RunAsync(delegate
		{
			Loom.QueueOnMainThread(delegate
			{
				CompleteGroupNum = 0;
				NeedTimes--;
				NpcJieSuanManager.inst.JieSuanTimes++;
				NpcJieSuanManager.inst.JieSuanTime = DateTime.Parse(NpcJieSuanManager.inst.JieSuanTime).AddMonths(1).ToString(CultureInfo.CurrentCulture);
				if (NeedTimes == 0)
				{
					ActionCompleteCallBack();
				}
				else
				{
					BeforeAction();
				}
			}, null);
		});
	}

	private void StartAction()
	{
		foreach (NpcDataGroup group in GroupList)
		{
			group.Start();
		}
	}

	public void ActionCompleteCallBack()
	{
		Loom.RunAsync(delegate
		{
			try
			{
				foreach (NpcDataGroup group in GroupList)
				{
					foreach (int key in group.NpcDict.Keys)
					{
						group.NpcDict[key].BackWriter();
					}
					Pool.BackGroup(group);
				}
			}
			catch (Exception ex)
			{
				Debug.LogError((object)"完成小组行动回调出错");
				Debug.LogError((object)ex);
			}
			finally
			{
				IsFree = true;
			}
		});
	}

	public void SendMessage()
	{
		foreach (NpcDataGroup group in GroupList)
		{
			foreach (int key in group.NpcDict.Keys)
			{
				_ = key;
			}
		}
	}

	public void GroupCompleteCallBack()
	{
		CompleteGroupNum++;
		if (CompleteGroupNum == GroupNum)
		{
			AfterAction();
		}
	}
}
