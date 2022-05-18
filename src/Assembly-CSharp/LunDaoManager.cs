using System;
using System.Collections.Generic;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000472 RID: 1138
public class LunDaoManager : MonoBehaviour
{
	// Token: 0x06001E7B RID: 7803 RVA: 0x00107F0C File Offset: 0x0010610C
	private void Awake()
	{
		LunDaoManager.inst = this;
		this.npcId = Tools.instance.LunDaoNpcId;
		this.hasCompleteLunTi = new List<int>();
		this.lunDaoAmrMag = new LunDaoAmrMag();
		this.lunDaoStateNameDictionary = new Dictionary<int, string>();
		foreach (JSONObject jsonobject in jsonData.instance.LunDaoStateData.list)
		{
			this.lunDaoStateNameDictionary.Add(jsonobject["id"].I, jsonobject["ZhuangTaiInfo"].Str);
		}
	}

	// Token: 0x06001E7C RID: 7804 RVA: 0x00019486 File Offset: 0x00017686
	private void Start()
	{
		this.player = Tools.instance.getPlayer();
		this.lunTiMag = new LunTiMag();
		this.lunDaoCardMag = new LunDaoCardMag();
		this.InitLunDao();
	}

	// Token: 0x06001E7D RID: 7805 RVA: 0x00107FC4 File Offset: 0x001061C4
	private void InitLunDao()
	{
		if (!Tools.instance.IsSuiJiLunTi && (Tools.instance.LunTiList == null || Tools.instance.LunTiList.Count < 1))
		{
			this.selectLunTi.Init();
		}
		else
		{
			this.StartGame();
		}
		this.playerController.Init();
		this.npcController.Init();
	}

	// Token: 0x06001E7E RID: 7806 RVA: 0x000194B4 File Offset: 0x000176B4
	public void StartGame()
	{
		this.lunDaoAmrMag.PlayStartLunDao().PlayingAction(delegate
		{
			if (Tools.instance.LunTiList != null && Tools.instance.LunTiList.Count > 0)
			{
				this.selectLunTiList = new List<int>();
				Tools.instance.LunTiList.ForEach(delegate(int i)
				{
					this.selectLunTiList.Add(i);
				});
				Tools.instance.LunTiList = new List<int>();
			}
			else if (!Tools.instance.IsSuiJiLunTi)
			{
				this.selectLunTiList = new List<int>(this.selectLunTi.selectLunTiList);
			}
			else
			{
				this.selectLunTiList = this.GetSuiJiLuntTi(Tools.instance.LunTiNum);
				Tools.instance.IsSuiJiLunTi = false;
				Tools.instance.LunTiNum = 0;
			}
			Object.Destroy(this.selectLunTi.gameObject);
			this.lunTiMag.CreateLunTi(this.selectLunTiList, this.npcId);
			this.lunDaoCardMag.CreatePaiKu(this.selectLunTiList, this.npcId);
			this.lunDaoPanel.Init();
		}).CompleteAction(delegate
		{
			this.lunDaoPanel.Show();
			this.gameState = LunDaoManager.GameState.Npc回合;
			this.npcController.NpcStartRound();
		}).Run();
	}

	// Token: 0x06001E7F RID: 7807 RVA: 0x000194E8 File Offset: 0x000176E8
	public void EndRoundCallBack()
	{
		if (this.gameState == LunDaoManager.GameState.玩家回合)
		{
			this.playerController.PlayerStartRound();
			return;
		}
		if (this.gameState == LunDaoManager.GameState.Npc回合)
		{
			this.npcController.NpcStartRound();
			return;
		}
		if (this.gameState == LunDaoManager.GameState.论道结束)
		{
			this.GameOver();
		}
	}

	// Token: 0x06001E80 RID: 7808 RVA: 0x00108024 File Offset: 0x00106224
	public void GameOver()
	{
		if (this.isOver)
		{
			return;
		}
		this.isOver = true;
		if (this.hasCompleteLunTi.Count > 0)
		{
			base.Invoke("LunDaoSuccess", 1f);
			return;
		}
		base.Invoke("LunDaoFail", 1f);
	}

	// Token: 0x06001E81 RID: 7809 RVA: 0x00108070 File Offset: 0x00106270
	public void ChuPaiCallBack()
	{
		this.lunTiMag.CompleteLunTi();
		this.lunTiMag.LunDianHeCheng();
		if (this.gameState == LunDaoManager.GameState.玩家回合 && LunDaoManager.inst.lunTiMag.GetNullSlot() == -1)
		{
			this.playerController.tips.SetActive(true);
			return;
		}
		this.playerController.tips.SetActive(false);
	}

	// Token: 0x06001E82 RID: 7810 RVA: 0x00019523 File Offset: 0x00017723
	public void LunDaoSuccess()
	{
		this.lunDaoSuccessPanel.Init();
	}

	// Token: 0x06001E83 RID: 7811 RVA: 0x001080D4 File Offset: 0x001062D4
	public void LunDaoFail()
	{
		if (NpcJieSuanManager.inst.lunDaoNpcList.Contains(this.npcId))
		{
			NpcJieSuanManager.inst.lunDaoNpcList.Remove(this.npcId);
		}
		NpcJieSuanManager.inst.npcNoteBook.NoteLunDaoFail(this.npcId, this.player.name);
		this.LunDaoFailPanel.SetActive(true);
	}

	// Token: 0x06001E84 RID: 7812 RVA: 0x0010813C File Offset: 0x0010633C
	public void Close()
	{
		Tools.instance.TargetLunTiNum = this.hasCompleteLunTi.Count;
		NpcJieSuanManager.inst.npcStatus.SetNpcStatus(this.npcId, 12);
		PanelMamager.CanOpenOrClose = true;
		Tools.instance.loadMapScenes(Tools.instance.FinalScene, true);
	}

	// Token: 0x06001E85 RID: 7813 RVA: 0x00108190 File Offset: 0x00106390
	public void AddWuDaoZhi(int wudaoId, int addNum)
	{
		int i = jsonData.instance.AvatarJsonData[this.npcId.ToString()]["wuDaoJson"][wudaoId.ToString()]["level"].I;
		int wuDaoLevelByType = Tools.instance.getPlayer().wuDaoMag.getWuDaoLevelByType(wudaoId);
		if (wuDaoLevelByType < i)
		{
			addNum = addNum * LunDaoReduceData.DataDict[i - wuDaoLevelByType].ShuaiJianXiShu / 100;
		}
		this.getWuDaoZhi += addNum;
		this.wuDaoZhi.text = this.getWuDaoZhi.ToString();
	}

	// Token: 0x06001E86 RID: 7814 RVA: 0x00108234 File Offset: 0x00106434
	public void AddWuDaoExp(int wuDaoId)
	{
		foreach (int item in this.lunTiMag.targetLunTiDictionary[wuDaoId])
		{
			if (!this.getWuDaoExp.ContainsKey(wuDaoId))
			{
				this.getWuDaoExp.Add(wuDaoId, new List<int>
				{
					item
				});
			}
			else
			{
				this.getWuDaoExp[wuDaoId].Add(item);
			}
		}
	}

	// Token: 0x06001E87 RID: 7815 RVA: 0x001082C8 File Offset: 0x001064C8
	public List<int> GetSuiJiLuntTi(int num)
	{
		List<int> list = new List<int>
		{
			1,
			2,
			3,
			4,
			5,
			6,
			7,
			8,
			9,
			10
		};
		List<int> list2 = new List<int>();
		for (int i = 0; i < num; i++)
		{
			int random = this.lunDaoCardMag.getRandom(0, list.Count - 1);
			list2.Add(list[random]);
			list.RemoveAt(random);
		}
		return list2;
	}

	// Token: 0x040019D8 RID: 6616
	public static LunDaoManager inst;

	// Token: 0x040019D9 RID: 6617
	public LunDaoAmrMag lunDaoAmrMag;

	// Token: 0x040019DA RID: 6618
	public SelectLunTi selectLunTi;

	// Token: 0x040019DB RID: 6619
	public PlayerController playerController;

	// Token: 0x040019DC RID: 6620
	public NpcController npcController;

	// Token: 0x040019DD RID: 6621
	public LunDaoCardMag lunDaoCardMag;

	// Token: 0x040019DE RID: 6622
	public LunTiMag lunTiMag;

	// Token: 0x040019DF RID: 6623
	public LunDaoPanel lunDaoPanel;

	// Token: 0x040019E0 RID: 6624
	public LunDaoSuccess lunDaoSuccessPanel;

	// Token: 0x040019E1 RID: 6625
	public Dictionary<int, string> lunDaoStateNameDictionary;

	// Token: 0x040019E2 RID: 6626
	public List<int> selectLunTiList;

	// Token: 0x040019E3 RID: 6627
	public int npcId;

	// Token: 0x040019E4 RID: 6628
	public LunDaoManager.GameState gameState;

	// Token: 0x040019E5 RID: 6629
	public Avatar player;

	// Token: 0x040019E6 RID: 6630
	public List<int> hasCompleteLunTi;

	// Token: 0x040019E7 RID: 6631
	public Transform AnimatorPanel;

	// Token: 0x040019E8 RID: 6632
	public List<Sprite> cardSprites;

	// Token: 0x040019E9 RID: 6633
	public List<AudioClip> musicEffectList;

	// Token: 0x040019EA RID: 6634
	public Dictionary<int, List<int>> getWuDaoExp = new Dictionary<int, List<int>>();

	// Token: 0x040019EB RID: 6635
	[SerializeField]
	private Text wuDaoZhi;

	// Token: 0x040019EC RID: 6636
	public int getWuDaoZhi;

	// Token: 0x040019ED RID: 6637
	public List<Sprite> cardSpriteList;

	// Token: 0x040019EE RID: 6638
	public GameObject playerCardTemp;

	// Token: 0x040019EF RID: 6639
	public GameObject LunDaoFailPanel;

	// Token: 0x040019F0 RID: 6640
	public bool isOver;

	// Token: 0x02000473 RID: 1139
	public enum GameState
	{
		// Token: 0x040019F2 RID: 6642
		玩家回合 = 1,
		// Token: 0x040019F3 RID: 6643
		Npc回合,
		// Token: 0x040019F4 RID: 6644
		论道结束
	}
}
