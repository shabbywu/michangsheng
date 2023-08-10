using System.Collections.Generic;
using KBEngine;
using TuPo;
using UnityEngine;

public class JieYin
{
	public int JinDanHP;

	public int JinDanHP_Max;

	public int YiZhi;

	public int YiZhi_Max;

	public int JinMai;

	public int JinMai_Max;

	public int HuaYing;

	public int HuaYing_Max;

	public bool IsFail;

	public bool IsDnaSui;

	public Entity entity;

	public bool IsStop;

	public JieYin(Entity avater)
	{
		entity = avater;
	}

	private int getYiZhi_Max()
	{
		Avatar avatar = (Avatar)entity;
		List<int> list = new List<int> { 0, 2, 5, 10, 15, 20, 25 };
		List<int> list2 = new List<int> { 0, 2, 4, 10, 16, 25 };
		int index = avatar.GetXinJingLevel() - 1;
		int num = 80 + list[index];
		int wuDaoLevelByType = avatar.wuDaoMag.getWuDaoLevelByType(6);
		return num + list2[wuDaoLevelByType] + avatar._JieYingYiZHi;
	}

	private int getJinMai_Max()
	{
		Avatar avatar = (Avatar)entity;
		List<int> list = new List<int> { 0, 2, 4, 10, 16, 25 };
		int wuDaoLevelByType = avatar.wuDaoMag.getWuDaoLevelByType(7);
		int num = 80 + list[wuDaoLevelByType];
		int wuDaoLevelByType2 = avatar.wuDaoMag.getWuDaoLevelByType(9);
		return num + list[wuDaoLevelByType2] + avatar._JieYingJinMai;
	}

	private void ResetMax()
	{
		IsFail = false;
		IsDnaSui = false;
		JinDanHP_Max = 100;
		HuaYing_Max = 100;
		YiZhi_Max = getYiZhi_Max();
		JinMai_Max = getJinMai_Max();
	}

	private void RestNowValue()
	{
		JinDanHP = JinDanHP_Max;
		HuaYing = 0;
		YiZhi = YiZhi_Max;
		JinMai = JinMai_Max;
	}

	private void BuffSeid204SetNum(ref int num, int type)
	{
		if (num <= 0)
		{
			SetNumBySeid(204, ref num, type, "value1", "value2");
		}
	}

	public int GetNowValue(int type)
	{
		int result = 0;
		switch (type)
		{
		case 1:
			result = JinMai;
			break;
		case 2:
			result = YiZhi;
			break;
		}
		return result;
	}

	private void BuffSeid205SetNum(ref int num)
	{
		if (num >= 0)
		{
			SetNumBySeid(205, ref num, 0, "", "value1");
		}
	}

	private void BuffSeid209SetNum(ref int num)
	{
		Avatar obj = (Avatar)entity;
		int num2 = 209;
		foreach (List<int> item in obj.buffmag.getBuffBySeid(num2))
		{
			int num3 = item[2];
			int num4 = item[1];
			JSONObject jSONObject = jsonData.instance.BuffSeidJsonData[num2][string.Concat(num3)];
			num -= jSONObject["value1"].I * num4;
		}
	}

	private void BuffSeid210SetNum(ref int num)
	{
		if (num <= 0)
		{
			SetNumBySeid(210, ref num, 0, "", "value1");
		}
	}

	private void BuffSeid211SetNum(ref int num)
	{
		if (num <= 0)
		{
			SetNumBySeid(211, ref num, 0, "", "value1");
		}
	}

	private void SetNumBySeid(int Seid, ref int num, int type, string value1, string value2)
	{
		foreach (List<int> item in ((Avatar)entity).buffmag.getBuffBySeid(Seid))
		{
			int num2 = item[2];
			JSONObject jSONObject = jsonData.instance.BuffSeidJsonData[Seid][string.Concat(num2)];
			if (type == 0 || jSONObject[value1].I == type)
			{
				num += (int)((float)num * (jSONObject[value2].n / 100f));
			}
		}
	}

	public void AddJinMai(int num)
	{
		if (!IsStop)
		{
			BuffSeid204SetNum(ref num, 1);
			JinMai = Mathf.Clamp(JinMai + num, 0, JinMai_Max);
			if (num < 0 && ((Avatar)entity).buffmag.HasBuffSeid(207))
			{
				AddYiZhi(num);
			}
			if (JinMai <= 0)
			{
				JieYinFail();
			}
			((Component)RoundManager.instance).gameObject.GetComponent<JieYingManager>().showDamage(num, 2);
		}
	}

	public void AddYiZhi(int num)
	{
		if (IsStop)
		{
			return;
		}
		BuffSeid204SetNum(ref num, 2);
		YiZhi = Mathf.Clamp(YiZhi + num, 0, YiZhi_Max);
		if (YiZhi <= 0)
		{
			if (((Avatar)entity).buffmag.HasBuff(5424))
			{
				JieYingYiZhiFail();
			}
			else
			{
				JieYingDie();
			}
		}
		((Component)RoundManager.instance).gameObject.GetComponent<JieYingManager>().showDamage(num, 1);
	}

	public void AddHuaYing(int num)
	{
		BuffSeid205SetNum(ref num);
		HuaYing = Mathf.Clamp(HuaYing + num, 0, HuaYing_Max);
		if (HuaYing >= HuaYing_Max)
		{
			JieYinSuccess();
		}
	}

	public void JieYinSuccess()
	{
		if (!IsFail && !IsStop)
		{
			IsStop = true;
			Avatar player = PlayerEx.Player;
			int buffSum = player.buffmag.GetBuffSum(5428);
			if (buffSum > 0)
			{
				player._HP_Max += buffSum * 200;
				player.HP = player.HP_Max;
			}
			GlobalValue.SetTalk(1, 2, "JieYin.JieYinSuccess");
			ResManager.inst.LoadPrefab("BigTuPoResult").Inst().GetComponent<BigTuPoResultIMag>()
				.ShowSuccess(3);
		}
	}

	public void JieYinFail()
	{
		IsFail = true;
		if (!IsStop)
		{
			IsStop = true;
			Avatar player = Tools.instance.getPlayer();
			GlobalValue.SetTalk(1, 3, "JieYin.JieYinFail");
			player._JieYingJinMai += 5;
			if (player.exp > 1500000)
			{
				player.exp -= 1500000uL;
			}
			else
			{
				player.exp = 0uL;
			}
			ResManager.inst.LoadPrefab("BigTuPoResult").Inst().GetComponent<BigTuPoResultIMag>()
				.ShowFail(3, 1);
		}
	}

	public void JieYingYiZhiFail()
	{
		IsFail = true;
		if (!IsStop)
		{
			IsStop = true;
			Avatar player = Tools.instance.getPlayer();
			GlobalValue.SetTalk(1, 3, "JieYin.JieYingYiZhiFail");
			player._JieYingYiZHi += 5;
			if (player.exp > 1500000)
			{
				player.exp -= 1500000uL;
			}
			else
			{
				player.exp = 0uL;
			}
			ResManager.inst.LoadPrefab("BigTuPoResult").Inst().GetComponent<BigTuPoResultIMag>()
				.ShowFail(3, 2);
		}
	}

	public void JieYingDie()
	{
		if (!IsStop)
		{
			UIDeath.Inst.Show(DeathType.心魔入体);
		}
	}

	public void AddJinDanHP(int num)
	{
		BuffSeid209SetNum(ref num);
		BuffSeid210SetNum(ref num);
		BuffSeid211SetNum(ref num);
		JinDanHP = Mathf.Clamp(JinDanHP + num, 0, JinDanHP_Max);
		if (num < 0)
		{
			((Component)RoundManager.instance).gameObject.GetComponent<JieYingManager>().JinDanAttacked();
		}
		if (JinDanHP <= 0)
		{
			StartHuaYing();
		}
		((Component)RoundManager.instance).gameObject.GetComponent<JieYingManager>().showDamage(num);
	}

	private void StartHuaYing()
	{
		if (IsDnaSui)
		{
			return;
		}
		IsDnaSui = true;
		Avatar player = (Avatar)entity;
		((Component)RoundManager.instance).gameObject.GetComponent<JieYingManager>().HuaYingCallBack();
		player.FightClearSkill(0, 6);
		player.FightAddSkill(11077, 0, 6);
		player.FightAddSkill(11082, 0, 6);
		foreach (SkillItem hasSkill in player.hasSkillList)
		{
			int skillKeyByID = Tools.instance.getSkillKeyByID(hasSkill.itemId, player);
			if (skillKeyByID != -1 && jsonData.instance.skillJsonData[skillKeyByID.ToString()]["AttackType"].list.Find((JSONObject aaa) => (int)aaa.n == 15) != null)
			{
				player.FightAddSkill(skillKeyByID, 0, 6);
			}
		}
		List<List<int>> aa = new List<List<int>>();
		List<int> RemoveBuffLIst = new List<int> { 3099, 3100 };
		player.bufflist.ForEach(delegate(List<int> _aa)
		{
			int i = jsonData.instance.BuffJsonData[_aa[2].ToString()]["buffid"].I;
			if (RemoveBuffLIst.Contains(i))
			{
				aa.Add(_aa);
			}
		});
		aa.ForEach(delegate(List<int> _aa)
		{
			player.spell.removeBuff(_aa);
		});
		player.spell.addDBuff(3101);
		player.spell.addDBuff(3102);
		player.spell.addDBuff(3111);
		player.spell.addDBuff(921, 5);
	}

	public void Init()
	{
		ResetMax();
		RestNowValue();
		IsStop = false;
	}
}
