using System;
using System.Collections.Generic;
using GUIPackage;
using KBEngine;
using TuPo;
using UnityEngine;
using UnityEngine.UI;

public class JieDanManager : MonoBehaviour
{
	public Text ShengYuHuiHe;

	public static JieDanManager instence;

	private bool CanJieSuan = true;

	[NonSerialized]
	public List<int> jieDanBuff = new List<int>();

	private void Start()
	{
		instence = this;
		jieDanBuff = new List<int> { 4022, 4023, 4024, 4025, 4026, 4027 };
	}

	private void OnDestroy()
	{
		instence = null;
	}

	public void JieSuan()
	{
		Avatar player = Tools.instance.getPlayer();
		bool flag = false;
		foreach (int item in jieDanBuff)
		{
			if (player.buffmag.HasBuff(item))
			{
				flag = true;
			}
		}
		Tools.instance.getPlayer().OtherAvatar.state = 1;
		if (flag)
		{
			JieDanSuccess();
		}
		else
		{
			JieDanFaile();
		}
	}

	public void quitJieDan()
	{
		Tools.instance.loadMapScenes(Tools.instance.FinalScene);
	}

	public void JieDanSuccess()
	{
		GlobalValue.SetTalk(1, 2, "JieDanManager.JieDanSuccess");
		addJieDanSkill();
		Avatar player = Tools.instance.getPlayer();
		ResManager.inst.LoadPrefab("BigTuPoResult").Inst().GetComponent<BigTuPoResultIMag>()
			.ShowSuccess(2);
		JieDanSkill.resetJieDanSeid(player);
	}

	public void addJieDanSkill()
	{
		Avatar player = Tools.instance.getPlayer();
		int jinDanID = getJinDanID();
		if (jinDanID != -1)
		{
			player.addJieDanSkillList(jinDanID);
		}
	}

	public int getJinDanPingZhi()
	{
		Avatar avatar = Tools.instance.getPlayer();
		int buffCengShu = 0;
		jieDanBuff.ForEach(delegate(int aa)
		{
			avatar.buffmag.getBuffByID(aa).ForEach(delegate(List<int> cc)
			{
				buffCengShu += cc[1];
			});
		});
		return buffCengShu;
	}

	public int getJinDanID()
	{
		Avatar avatar = Tools.instance.getPlayer();
		int buffCengShu = 0;
		Dictionary<int, int> lingqichi = new Dictionary<int, int>
		{
			{ 0, 0 },
			{ 1, 0 },
			{ 2, 0 },
			{ 3, 0 },
			{ 4, 0 },
			{ 5, 0 }
		};
		jieDanBuff.ForEach(delegate(int aa)
		{
			avatar.buffmag.getBuffByID(aa).ForEach(delegate(List<int> cc)
			{
				buffCengShu += cc[1];
				lingqichi[aa - 4022] += cc[1];
			});
		});
		int result = -1;
		foreach (JSONObject item in jsonData.instance.JieDanBiao.list)
		{
			if ((int)item["JinDanQuality"].n != buffCengShu)
			{
				continue;
			}
			if (item["JinDanType"].Count == 1)
			{
				if (lingqichi[(int)item["JinDanType"][0].n] > buffCengShu / 2)
				{
					result = item["id"].I;
					break;
				}
			}
			else if (item["JinDanType"].Count == 2)
			{
				bool flag = true;
				foreach (JSONObject item2 in item["JinDanType"].list)
				{
					if (lingqichi[(int)item2.n] <= buffCengShu / 3)
					{
						flag = false;
					}
				}
				if (flag)
				{
					result = item["id"].I;
					break;
				}
			}
			if ((int)item["JinDanType"][0].n == 5)
			{
				result = item["id"].I;
				break;
			}
		}
		return result;
	}

	public void JieDanFaile()
	{
		GlobalValue.SetTalk(1, 3, "JieDanManager.JieDanFaile");
		Tools.instance.getPlayer().exp -= 10000uL;
		ResManager.inst.LoadPrefab("BigTuPoResult").Inst().GetComponent<BigTuPoResultIMag>()
			.ShowFail(2);
	}

	private void Update()
	{
		checkPlayerBuff();
	}

	public void checkPlayerBuff()
	{
		Avatar player = Tools.instance.getPlayer();
		int jinDanPingZhi = getJinDanPingZhi();
		bool flag = player.buffmag.HasBuff(4010);
		if (CanJieSuan && (!flag || jinDanPingZhi >= 9))
		{
			CanJieSuan = false;
			JieSuan();
		}
		if (flag)
		{
			ShengYuHuiHe.text = "剩余回合 " + player.buffmag.getBuffByID(4010)[0][1].ToCNNumber();
		}
	}
}
