using System;
using System.Collections.Generic;
using KBEngine;
using TuPo;
using UnityEngine;

// Token: 0x020003BA RID: 954
public class JieYin
{
	// Token: 0x06001F04 RID: 7940 RVA: 0x000D93B9 File Offset: 0x000D75B9
	public JieYin(Entity avater)
	{
		this.entity = avater;
	}

	// Token: 0x06001F05 RID: 7941 RVA: 0x000D93C8 File Offset: 0x000D75C8
	private int getYiZhi_Max()
	{
		Avatar avatar = (Avatar)this.entity;
		int num = 80;
		List<int> list = new List<int>
		{
			0,
			2,
			5,
			10,
			15,
			20,
			25
		};
		List<int> list2 = new List<int>
		{
			0,
			2,
			4,
			10,
			16,
			25
		};
		int index = avatar.GetXinJingLevel() - 1;
		int num2 = num + list[index];
		int wuDaoLevelByType = avatar.wuDaoMag.getWuDaoLevelByType(6);
		return num2 + list2[wuDaoLevelByType] + avatar._JieYingYiZHi;
	}

	// Token: 0x06001F06 RID: 7942 RVA: 0x000D9480 File Offset: 0x000D7680
	private int getJinMai_Max()
	{
		Avatar avatar = (Avatar)this.entity;
		int num = 80;
		List<int> list = new List<int>
		{
			0,
			2,
			4,
			10,
			16,
			25
		};
		int wuDaoLevelByType = avatar.wuDaoMag.getWuDaoLevelByType(7);
		int num2 = num + list[wuDaoLevelByType];
		int wuDaoLevelByType2 = avatar.wuDaoMag.getWuDaoLevelByType(9);
		return num2 + list[wuDaoLevelByType2] + avatar._JieYingJinMai;
	}

	// Token: 0x06001F07 RID: 7943 RVA: 0x000D9500 File Offset: 0x000D7700
	private void ResetMax()
	{
		this.IsFail = false;
		this.IsDnaSui = false;
		this.JinDanHP_Max = 100;
		this.HuaYing_Max = 100;
		this.YiZhi_Max = this.getYiZhi_Max();
		this.JinMai_Max = this.getJinMai_Max();
	}

	// Token: 0x06001F08 RID: 7944 RVA: 0x000D9538 File Offset: 0x000D7738
	private void RestNowValue()
	{
		this.JinDanHP = this.JinDanHP_Max;
		this.HuaYing = 0;
		this.YiZhi = this.YiZhi_Max;
		this.JinMai = this.JinMai_Max;
	}

	// Token: 0x06001F09 RID: 7945 RVA: 0x000D9565 File Offset: 0x000D7765
	private void BuffSeid204SetNum(ref int num, int type)
	{
		if (num > 0)
		{
			return;
		}
		this.SetNumBySeid(204, ref num, type, "value1", "value2");
	}

	// Token: 0x06001F0A RID: 7946 RVA: 0x000D9584 File Offset: 0x000D7784
	public int GetNowValue(int type)
	{
		int result = 0;
		if (type != 1)
		{
			if (type == 2)
			{
				result = this.YiZhi;
			}
		}
		else
		{
			result = this.JinMai;
		}
		return result;
	}

	// Token: 0x06001F0B RID: 7947 RVA: 0x000D95AE File Offset: 0x000D77AE
	private void BuffSeid205SetNum(ref int num)
	{
		if (num < 0)
		{
			return;
		}
		this.SetNumBySeid(205, ref num, 0, "", "value1");
	}

	// Token: 0x06001F0C RID: 7948 RVA: 0x000D95D0 File Offset: 0x000D77D0
	private void BuffSeid209SetNum(ref int num)
	{
		Avatar avatar = (Avatar)this.entity;
		int num2 = 209;
		foreach (List<int> list in avatar.buffmag.getBuffBySeid(num2))
		{
			int num3 = list[2];
			int num4 = list[1];
			JSONObject jsonobject = jsonData.instance.BuffSeidJsonData[num2][string.Concat(num3)];
			num -= jsonobject["value1"].I * num4;
		}
	}

	// Token: 0x06001F0D RID: 7949 RVA: 0x000D9678 File Offset: 0x000D7878
	private void BuffSeid210SetNum(ref int num)
	{
		if (num > 0)
		{
			return;
		}
		this.SetNumBySeid(210, ref num, 0, "", "value1");
	}

	// Token: 0x06001F0E RID: 7950 RVA: 0x000D9697 File Offset: 0x000D7897
	private void BuffSeid211SetNum(ref int num)
	{
		if (num > 0)
		{
			return;
		}
		this.SetNumBySeid(211, ref num, 0, "", "value1");
	}

	// Token: 0x06001F0F RID: 7951 RVA: 0x000D96B8 File Offset: 0x000D78B8
	private void SetNumBySeid(int Seid, ref int num, int type, string value1, string value2)
	{
		foreach (List<int> list in ((Avatar)this.entity).buffmag.getBuffBySeid(Seid))
		{
			int num2 = list[2];
			JSONObject jsonobject = jsonData.instance.BuffSeidJsonData[Seid][string.Concat(num2)];
			if (type == 0 || jsonobject[value1].I == type)
			{
				num += (int)((float)num * (jsonobject[value2].n / 100f));
			}
		}
	}

	// Token: 0x06001F10 RID: 7952 RVA: 0x000D976C File Offset: 0x000D796C
	public void AddJinMai(int num)
	{
		if (this.IsStop)
		{
			return;
		}
		this.BuffSeid204SetNum(ref num, 1);
		this.JinMai = Mathf.Clamp(this.JinMai + num, 0, this.JinMai_Max);
		if (num < 0 && ((Avatar)this.entity).buffmag.HasBuffSeid(207))
		{
			this.AddYiZhi(num);
		}
		if (this.JinMai <= 0)
		{
			this.JieYinFail();
		}
		RoundManager.instance.gameObject.GetComponent<JieYingManager>().showDamage(num, 2);
	}

	// Token: 0x06001F11 RID: 7953 RVA: 0x000D97F4 File Offset: 0x000D79F4
	public void AddYiZhi(int num)
	{
		if (this.IsStop)
		{
			return;
		}
		this.BuffSeid204SetNum(ref num, 2);
		this.YiZhi = Mathf.Clamp(this.YiZhi + num, 0, this.YiZhi_Max);
		if (this.YiZhi <= 0)
		{
			if (((Avatar)this.entity).buffmag.HasBuff(5424))
			{
				this.JieYingYiZhiFail();
			}
			else
			{
				this.JieYingDie();
			}
		}
		RoundManager.instance.gameObject.GetComponent<JieYingManager>().showDamage(num, 1);
	}

	// Token: 0x06001F12 RID: 7954 RVA: 0x000D9876 File Offset: 0x000D7A76
	public void AddHuaYing(int num)
	{
		this.BuffSeid205SetNum(ref num);
		this.HuaYing = Mathf.Clamp(this.HuaYing + num, 0, this.HuaYing_Max);
		if (this.HuaYing >= this.HuaYing_Max)
		{
			this.JieYinSuccess();
		}
	}

	// Token: 0x06001F13 RID: 7955 RVA: 0x000D98B0 File Offset: 0x000D7AB0
	public void JieYinSuccess()
	{
		if (this.IsFail)
		{
			return;
		}
		if (this.IsStop)
		{
			return;
		}
		this.IsStop = true;
		Avatar player = PlayerEx.Player;
		int buffSum = player.buffmag.GetBuffSum(5428);
		if (buffSum > 0)
		{
			player._HP_Max += buffSum * 200;
			player.HP = player.HP_Max;
		}
		GlobalValue.SetTalk(1, 2, "JieYin.JieYinSuccess");
		ResManager.inst.LoadPrefab("BigTuPoResult").Inst(null).GetComponent<BigTuPoResultIMag>().ShowSuccess(3);
	}

	// Token: 0x06001F14 RID: 7956 RVA: 0x000D9940 File Offset: 0x000D7B40
	public void JieYinFail()
	{
		this.IsFail = true;
		if (this.IsStop)
		{
			return;
		}
		this.IsStop = true;
		Avatar player = Tools.instance.getPlayer();
		GlobalValue.SetTalk(1, 3, "JieYin.JieYinFail");
		player._JieYingJinMai += 5;
		if (player.exp > 1500000UL)
		{
			player.exp -= 1500000UL;
		}
		else
		{
			player.exp = 0UL;
		}
		ResManager.inst.LoadPrefab("BigTuPoResult").Inst(null).GetComponent<BigTuPoResultIMag>().ShowFail(3, 1);
	}

	// Token: 0x06001F15 RID: 7957 RVA: 0x000D99D8 File Offset: 0x000D7BD8
	public void JieYingYiZhiFail()
	{
		this.IsFail = true;
		if (this.IsStop)
		{
			return;
		}
		this.IsStop = true;
		Avatar player = Tools.instance.getPlayer();
		GlobalValue.SetTalk(1, 3, "JieYin.JieYingYiZhiFail");
		player._JieYingYiZHi += 5;
		if (player.exp > 1500000UL)
		{
			player.exp -= 1500000UL;
		}
		else
		{
			player.exp = 0UL;
		}
		ResManager.inst.LoadPrefab("BigTuPoResult").Inst(null).GetComponent<BigTuPoResultIMag>().ShowFail(3, 2);
	}

	// Token: 0x06001F16 RID: 7958 RVA: 0x000D9A6D File Offset: 0x000D7C6D
	public void JieYingDie()
	{
		if (this.IsStop)
		{
			return;
		}
		UIDeath.Inst.Show(DeathType.心魔入体);
	}

	// Token: 0x06001F17 RID: 7959 RVA: 0x000D9A84 File Offset: 0x000D7C84
	public void AddJinDanHP(int num)
	{
		this.BuffSeid209SetNum(ref num);
		this.BuffSeid210SetNum(ref num);
		this.BuffSeid211SetNum(ref num);
		this.JinDanHP = Mathf.Clamp(this.JinDanHP + num, 0, this.JinDanHP_Max);
		if (num < 0)
		{
			RoundManager.instance.gameObject.GetComponent<JieYingManager>().JinDanAttacked();
		}
		if (this.JinDanHP <= 0)
		{
			this.StartHuaYing();
		}
		RoundManager.instance.gameObject.GetComponent<JieYingManager>().showDamage(num, 0);
	}

	// Token: 0x06001F18 RID: 7960 RVA: 0x000D9B00 File Offset: 0x000D7D00
	private void StartHuaYing()
	{
		if (this.IsDnaSui)
		{
			return;
		}
		this.IsDnaSui = true;
		Avatar player = (Avatar)this.entity;
		RoundManager.instance.gameObject.GetComponent<JieYingManager>().HuaYingCallBack();
		player.FightClearSkill(0, 6);
		player.FightAddSkill(11077, 0, 6);
		player.FightAddSkill(11082, 0, 6);
		foreach (SkillItem skillItem in player.hasSkillList)
		{
			int skillKeyByID = Tools.instance.getSkillKeyByID(skillItem.itemId, player);
			if (skillKeyByID != -1)
			{
				if (jsonData.instance.skillJsonData[skillKeyByID.ToString()]["AttackType"].list.Find((JSONObject aaa) => (int)aaa.n == 15) != null)
				{
					player.FightAddSkill(skillKeyByID, 0, 6);
				}
			}
		}
		List<List<int>> aa = new List<List<int>>();
		List<int> RemoveBuffLIst = new List<int>
		{
			3099,
			3100
		};
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

	// Token: 0x06001F19 RID: 7961 RVA: 0x000D9CEC File Offset: 0x000D7EEC
	public void Init()
	{
		this.ResetMax();
		this.RestNowValue();
		this.IsStop = false;
	}

	// Token: 0x04001951 RID: 6481
	public int JinDanHP;

	// Token: 0x04001952 RID: 6482
	public int JinDanHP_Max;

	// Token: 0x04001953 RID: 6483
	public int YiZhi;

	// Token: 0x04001954 RID: 6484
	public int YiZhi_Max;

	// Token: 0x04001955 RID: 6485
	public int JinMai;

	// Token: 0x04001956 RID: 6486
	public int JinMai_Max;

	// Token: 0x04001957 RID: 6487
	public int HuaYing;

	// Token: 0x04001958 RID: 6488
	public int HuaYing_Max;

	// Token: 0x04001959 RID: 6489
	public bool IsFail;

	// Token: 0x0400195A RID: 6490
	public bool IsDnaSui;

	// Token: 0x0400195B RID: 6491
	public Entity entity;

	// Token: 0x0400195C RID: 6492
	public bool IsStop;
}
