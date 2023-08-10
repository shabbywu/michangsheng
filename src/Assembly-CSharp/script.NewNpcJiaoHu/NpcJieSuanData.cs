using System;
using System.Collections.Generic;

namespace script.NewNpcJiaoHu;

[Serializable]
public class NpcJieSuanData
{
	public List<int> allBigMapNpcList = new List<int>();

	public List<int> JieShaNpcList = new List<int>();

	public List<EmailData> lateEmailList = new List<EmailData>();

	public Dictionary<int, EmailData> lateEmailDict = new Dictionary<int, EmailData>();

	public List<int> lunDaoNpcList = new List<int>();

	public List<List<int>> afterDeathList = new List<List<int>>();

	public void SaveData()
	{
		allBigMapNpcList = NpcJieSuanManager.inst.allBigMapNpcList;
		lunDaoNpcList = NpcJieSuanManager.inst.lunDaoNpcList;
		JieShaNpcList = NpcJieSuanManager.inst.JieShaNpcList;
		lateEmailList = NpcJieSuanManager.inst.lateEmailList;
		lateEmailDict = NpcJieSuanManager.inst.lateEmailDict;
		afterDeathList = NpcJieSuanManager.inst.afterDeathList;
	}

	public void Init()
	{
		NpcJieSuanManager.inst.allBigMapNpcList = allBigMapNpcList;
		NpcJieSuanManager.inst.lunDaoNpcList = lunDaoNpcList;
		NpcJieSuanManager.inst.JieShaNpcList = JieShaNpcList;
		NpcJieSuanManager.inst.lateEmailList = lateEmailList;
		NpcJieSuanManager.inst.lateEmailDict = lateEmailDict;
		NpcJieSuanManager.inst.afterDeathList = afterDeathList;
	}
}
