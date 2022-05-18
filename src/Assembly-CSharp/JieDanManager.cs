using System;
using System.Collections.Generic;
using GUIPackage;
using KBEngine;
using TuPo;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200063A RID: 1594
public class JieDanManager : MonoBehaviour
{
	// Token: 0x06002791 RID: 10129 RVA: 0x00134C7C File Offset: 0x00132E7C
	private void Start()
	{
		JieDanManager.instence = this;
		this.jieDanBuff = new List<int>
		{
			4022,
			4023,
			4024,
			4025,
			4026,
			4027
		};
	}

	// Token: 0x06002792 RID: 10130 RVA: 0x0001F4CA File Offset: 0x0001D6CA
	private void OnDestroy()
	{
		JieDanManager.instence = null;
	}

	// Token: 0x06002793 RID: 10131 RVA: 0x00134CDC File Offset: 0x00132EDC
	public void JieSuan()
	{
		Avatar player = Tools.instance.getPlayer();
		bool flag = false;
		foreach (int buffID in this.jieDanBuff)
		{
			if (player.buffmag.HasBuff(buffID))
			{
				flag = true;
			}
		}
		Tools.instance.getPlayer().OtherAvatar.state = 1;
		if (flag)
		{
			this.JieDanSuccess();
			return;
		}
		this.JieDanFaile();
	}

	// Token: 0x06002794 RID: 10132 RVA: 0x0001BE16 File Offset: 0x0001A016
	public void quitJieDan()
	{
		Tools.instance.loadMapScenes(Tools.instance.FinalScene, true);
	}

	// Token: 0x06002795 RID: 10133 RVA: 0x00134D6C File Offset: 0x00132F6C
	public void JieDanSuccess()
	{
		GlobalValue.SetTalk(1, 2, "JieDanManager.JieDanSuccess");
		this.addJieDanSkill();
		Avatar player = Tools.instance.getPlayer();
		ResManager.inst.LoadPrefab("BigTuPoResult").Inst(null).GetComponent<BigTuPoResultIMag>().ShowSuccess(2);
		JieDanSkill.resetJieDanSeid(player);
	}

	// Token: 0x06002796 RID: 10134 RVA: 0x00134DBC File Offset: 0x00132FBC
	public void addJieDanSkill()
	{
		Avatar player = Tools.instance.getPlayer();
		int jinDanID = this.getJinDanID();
		if (jinDanID != -1)
		{
			player.addJieDanSkillList(jinDanID);
		}
	}

	// Token: 0x06002797 RID: 10135 RVA: 0x00134DE8 File Offset: 0x00132FE8
	public int getJinDanPingZhi()
	{
		Avatar avatar = Tools.instance.getPlayer();
		int buffCengShu = 0;
		Action<List<int>> <>9__1;
		this.jieDanBuff.ForEach(delegate(int aa)
		{
			List<List<int>> buffByID = avatar.buffmag.getBuffByID(aa);
			Action<List<int>> action;
			if ((action = <>9__1) == null)
			{
				action = (<>9__1 = delegate(List<int> cc)
				{
					buffCengShu += cc[1];
				});
			}
			buffByID.ForEach(action);
		});
		return buffCengShu;
	}

	// Token: 0x06002798 RID: 10136 RVA: 0x00134E30 File Offset: 0x00133030
	public int getJinDanID()
	{
		Avatar avatar = Tools.instance.getPlayer();
		int buffCengShu = 0;
		Dictionary<int, int> lingqichi = new Dictionary<int, int>
		{
			{
				0,
				0
			},
			{
				1,
				0
			},
			{
				2,
				0
			},
			{
				3,
				0
			},
			{
				4,
				0
			},
			{
				5,
				0
			}
		};
		this.jieDanBuff.ForEach(delegate(int aa)
		{
			avatar.buffmag.getBuffByID(aa).ForEach(delegate(List<int> cc)
			{
				buffCengShu += cc[1];
				Dictionary<int, int> lingqichi = lingqichi;
				int key = aa - 4022;
				lingqichi[key] += cc[1];
			});
		});
		int result = -1;
		foreach (JSONObject jsonobject in jsonData.instance.JieDanBiao.list)
		{
			if ((int)jsonobject["JinDanQuality"].n == buffCengShu)
			{
				if (jsonobject["JinDanType"].Count == 1)
				{
					if (lingqichi[(int)jsonobject["JinDanType"][0].n] > buffCengShu / 2)
					{
						result = (int)jsonobject["id"].n;
						break;
					}
				}
				else if (jsonobject["JinDanType"].Count == 2)
				{
					bool flag = true;
					foreach (JSONObject jsonobject2 in jsonobject["JinDanType"].list)
					{
						if (lingqichi[(int)jsonobject2.n] <= buffCengShu / 3)
						{
							flag = false;
						}
					}
					if (flag)
					{
						result = (int)jsonobject["id"].n;
						break;
					}
				}
				if ((int)jsonobject["JinDanType"][0].n == 5)
				{
					result = (int)jsonobject["id"].n;
					break;
				}
			}
		}
		return result;
	}

	// Token: 0x06002799 RID: 10137 RVA: 0x00135050 File Offset: 0x00133250
	public void JieDanFaile()
	{
		GlobalValue.SetTalk(1, 3, "JieDanManager.JieDanFaile");
		Tools.instance.getPlayer().exp -= 10000UL;
		ResManager.inst.LoadPrefab("BigTuPoResult").Inst(null).GetComponent<BigTuPoResultIMag>().ShowFail(2, 0);
	}

	// Token: 0x0600279A RID: 10138 RVA: 0x0001F4D2 File Offset: 0x0001D6D2
	private void Update()
	{
		this.checkPlayerBuff();
	}

	// Token: 0x0600279B RID: 10139 RVA: 0x001350A8 File Offset: 0x001332A8
	public void checkPlayerBuff()
	{
		Avatar player = Tools.instance.getPlayer();
		int jinDanPingZhi = this.getJinDanPingZhi();
		bool flag = player.buffmag.HasBuff(4010);
		if (this.CanJieSuan && (!flag || jinDanPingZhi >= 9))
		{
			this.CanJieSuan = false;
			this.JieSuan();
		}
		if (flag)
		{
			this.ShengYuHuiHe.text = "剩余回合 " + player.buffmag.getBuffByID(4010)[0][1].ToCNNumber();
		}
	}

	// Token: 0x0400217E RID: 8574
	public Text ShengYuHuiHe;

	// Token: 0x0400217F RID: 8575
	public static JieDanManager instence;

	// Token: 0x04002180 RID: 8576
	private bool CanJieSuan = true;

	// Token: 0x04002181 RID: 8577
	[NonSerialized]
	public List<int> jieDanBuff = new List<int>();
}
