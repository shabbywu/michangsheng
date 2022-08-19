using System;
using System.Collections.Generic;
using GUIPackage;
using KBEngine;
using TuPo;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200047C RID: 1148
public class JieDanManager : MonoBehaviour
{
	// Token: 0x060023D4 RID: 9172 RVA: 0x000F4DD8 File Offset: 0x000F2FD8
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

	// Token: 0x060023D5 RID: 9173 RVA: 0x000F4E38 File Offset: 0x000F3038
	private void OnDestroy()
	{
		JieDanManager.instence = null;
	}

	// Token: 0x060023D6 RID: 9174 RVA: 0x000F4E40 File Offset: 0x000F3040
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

	// Token: 0x060023D7 RID: 9175 RVA: 0x000D6520 File Offset: 0x000D4720
	public void quitJieDan()
	{
		Tools.instance.loadMapScenes(Tools.instance.FinalScene, true);
	}

	// Token: 0x060023D8 RID: 9176 RVA: 0x000F4ED0 File Offset: 0x000F30D0
	public void JieDanSuccess()
	{
		GlobalValue.SetTalk(1, 2, "JieDanManager.JieDanSuccess");
		this.addJieDanSkill();
		Avatar player = Tools.instance.getPlayer();
		ResManager.inst.LoadPrefab("BigTuPoResult").Inst(null).GetComponent<BigTuPoResultIMag>().ShowSuccess(2);
		JieDanSkill.resetJieDanSeid(player);
	}

	// Token: 0x060023D9 RID: 9177 RVA: 0x000F4F20 File Offset: 0x000F3120
	public void addJieDanSkill()
	{
		Avatar player = Tools.instance.getPlayer();
		int jinDanID = this.getJinDanID();
		if (jinDanID != -1)
		{
			player.addJieDanSkillList(jinDanID);
		}
	}

	// Token: 0x060023DA RID: 9178 RVA: 0x000F4F4C File Offset: 0x000F314C
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

	// Token: 0x060023DB RID: 9179 RVA: 0x000F4F94 File Offset: 0x000F3194
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
						result = jsonobject["id"].I;
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
						result = jsonobject["id"].I;
						break;
					}
				}
				if ((int)jsonobject["JinDanType"][0].n == 5)
				{
					result = jsonobject["id"].I;
					break;
				}
			}
		}
		return result;
	}

	// Token: 0x060023DC RID: 9180 RVA: 0x000F51B0 File Offset: 0x000F33B0
	public void JieDanFaile()
	{
		GlobalValue.SetTalk(1, 3, "JieDanManager.JieDanFaile");
		Tools.instance.getPlayer().exp -= 10000UL;
		ResManager.inst.LoadPrefab("BigTuPoResult").Inst(null).GetComponent<BigTuPoResultIMag>().ShowFail(2, 0);
	}

	// Token: 0x060023DD RID: 9181 RVA: 0x000F5206 File Offset: 0x000F3406
	private void Update()
	{
		this.checkPlayerBuff();
	}

	// Token: 0x060023DE RID: 9182 RVA: 0x000F5210 File Offset: 0x000F3410
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

	// Token: 0x04001CA3 RID: 7331
	public Text ShengYuHuiHe;

	// Token: 0x04001CA4 RID: 7332
	public static JieDanManager instence;

	// Token: 0x04001CA5 RID: 7333
	private bool CanJieSuan = true;

	// Token: 0x04001CA6 RID: 7334
	[NonSerialized]
	public List<int> jieDanBuff = new List<int>();
}
