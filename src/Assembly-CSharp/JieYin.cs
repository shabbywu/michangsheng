using System;
using System.Collections.Generic;
using KBEngine;
using TuPo;
using UnityEngine;

// Token: 0x02000548 RID: 1352
public class JieYin
{
	// Token: 0x06002282 RID: 8834 RVA: 0x0001C3FD File Offset: 0x0001A5FD
	public JieYin(Entity avater)
	{
		this.entity = avater;
	}

	// Token: 0x06002283 RID: 8835 RVA: 0x0011C390 File Offset: 0x0011A590
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

	// Token: 0x06002284 RID: 8836 RVA: 0x0011C448 File Offset: 0x0011A648
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

	// Token: 0x06002285 RID: 8837 RVA: 0x0001C40C File Offset: 0x0001A60C
	private void ResetMax()
	{
		this.IsFail = false;
		this.IsDnaSui = false;
		this.JinDanHP_Max = 100;
		this.HuaYing_Max = 100;
		this.YiZhi_Max = this.getYiZhi_Max();
		this.JinMai_Max = this.getJinMai_Max();
	}

	// Token: 0x06002286 RID: 8838 RVA: 0x0001C444 File Offset: 0x0001A644
	private void RestNowValue()
	{
		this.JinDanHP = this.JinDanHP_Max;
		this.HuaYing = 0;
		this.YiZhi = this.YiZhi_Max;
		this.JinMai = this.JinMai_Max;
	}

	// Token: 0x06002287 RID: 8839 RVA: 0x0001C471 File Offset: 0x0001A671
	private void BuffSeid204SetNum(ref int num, int type)
	{
		if (num > 0)
		{
			return;
		}
		this.SetNumBySeid(204, ref num, type, "value1", "value2");
	}

	// Token: 0x06002288 RID: 8840 RVA: 0x0011C4C8 File Offset: 0x0011A6C8
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

	// Token: 0x06002289 RID: 8841 RVA: 0x0001C490 File Offset: 0x0001A690
	private void BuffSeid205SetNum(ref int num)
	{
		if (num < 0)
		{
			return;
		}
		this.SetNumBySeid(205, ref num, 0, "", "value1");
	}

	// Token: 0x0600228A RID: 8842 RVA: 0x0011C4F4 File Offset: 0x0011A6F4
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

	// Token: 0x0600228B RID: 8843 RVA: 0x0001C4AF File Offset: 0x0001A6AF
	private void BuffSeid210SetNum(ref int num)
	{
		if (num > 0)
		{
			return;
		}
		this.SetNumBySeid(210, ref num, 0, "", "value1");
	}

	// Token: 0x0600228C RID: 8844 RVA: 0x0001C4CE File Offset: 0x0001A6CE
	private void BuffSeid211SetNum(ref int num)
	{
		if (num > 0)
		{
			return;
		}
		this.SetNumBySeid(211, ref num, 0, "", "value1");
	}

	// Token: 0x0600228D RID: 8845 RVA: 0x0011C59C File Offset: 0x0011A79C
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

	// Token: 0x0600228E RID: 8846 RVA: 0x0011C650 File Offset: 0x0011A850
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

	// Token: 0x0600228F RID: 8847 RVA: 0x0011C6D8 File Offset: 0x0011A8D8
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

	// Token: 0x06002290 RID: 8848 RVA: 0x0001C4ED File Offset: 0x0001A6ED
	public void AddHuaYing(int num)
	{
		this.BuffSeid205SetNum(ref num);
		this.HuaYing = Mathf.Clamp(this.HuaYing + num, 0, this.HuaYing_Max);
		if (this.HuaYing >= this.HuaYing_Max)
		{
			this.JieYinSuccess();
		}
	}

	// Token: 0x06002291 RID: 8849 RVA: 0x0011C75C File Offset: 0x0011A95C
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

	// Token: 0x06002292 RID: 8850 RVA: 0x0011C7EC File Offset: 0x0011A9EC
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

	// Token: 0x06002293 RID: 8851 RVA: 0x0011C884 File Offset: 0x0011AA84
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

	// Token: 0x06002294 RID: 8852 RVA: 0x0001C525 File Offset: 0x0001A725
	public void JieYingDie()
	{
		if (this.IsStop)
		{
			return;
		}
		UIDeath.Inst.Show(DeathType.心魔入体);
	}

	// Token: 0x06002295 RID: 8853 RVA: 0x0011C91C File Offset: 0x0011AB1C
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

	// Token: 0x06002296 RID: 8854 RVA: 0x0011C998 File Offset: 0x0011AB98
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

	// Token: 0x06002297 RID: 8855 RVA: 0x0001C53B File Offset: 0x0001A73B
	public void Init()
	{
		this.ResetMax();
		this.RestNowValue();
		this.IsStop = false;
	}

	// Token: 0x04001DCA RID: 7626
	public int JinDanHP;

	// Token: 0x04001DCB RID: 7627
	public int JinDanHP_Max;

	// Token: 0x04001DCC RID: 7628
	public int YiZhi;

	// Token: 0x04001DCD RID: 7629
	public int YiZhi_Max;

	// Token: 0x04001DCE RID: 7630
	public int JinMai;

	// Token: 0x04001DCF RID: 7631
	public int JinMai_Max;

	// Token: 0x04001DD0 RID: 7632
	public int HuaYing;

	// Token: 0x04001DD1 RID: 7633
	public int HuaYing_Max;

	// Token: 0x04001DD2 RID: 7634
	public bool IsFail;

	// Token: 0x04001DD3 RID: 7635
	public bool IsDnaSui;

	// Token: 0x04001DD4 RID: 7636
	public Entity entity;

	// Token: 0x04001DD5 RID: 7637
	public bool IsStop;
}
