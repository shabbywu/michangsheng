using System;
using System.Collections.Generic;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000311 RID: 785
public class LunDaoManager : MonoBehaviour
{
	// Token: 0x06001B4A RID: 6986 RVA: 0x000C28DC File Offset: 0x000C0ADC
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

	// Token: 0x06001B4B RID: 6987 RVA: 0x000C2994 File Offset: 0x000C0B94
	private void Start()
	{
		this.player = Tools.instance.getPlayer();
		this.lunTiMag = new LunTiMag();
		this.lunDaoCardMag = new LunDaoCardMag();
		this.InitLunDao();
	}

	// Token: 0x06001B4C RID: 6988 RVA: 0x000C29C4 File Offset: 0x000C0BC4
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

	// Token: 0x06001B4D RID: 6989 RVA: 0x000C2A24 File Offset: 0x000C0C24
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

	// Token: 0x06001B4E RID: 6990 RVA: 0x000C2A58 File Offset: 0x000C0C58
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

	// Token: 0x06001B4F RID: 6991 RVA: 0x000C2A94 File Offset: 0x000C0C94
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

	// Token: 0x06001B50 RID: 6992 RVA: 0x000C2AE0 File Offset: 0x000C0CE0
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

	// Token: 0x06001B51 RID: 6993 RVA: 0x000C2B41 File Offset: 0x000C0D41
	public void LunDaoSuccess()
	{
		this.lunDaoSuccessPanel.Init();
	}

	// Token: 0x06001B52 RID: 6994 RVA: 0x000C2B50 File Offset: 0x000C0D50
	public void LunDaoFail()
	{
		if (NpcJieSuanManager.inst.lunDaoNpcList.Contains(this.npcId))
		{
			NpcJieSuanManager.inst.lunDaoNpcList.Remove(this.npcId);
		}
		NpcJieSuanManager.inst.npcNoteBook.NoteLunDaoFail(this.npcId, this.player.name);
		this.LunDaoFailPanel.SetActive(true);
	}

	// Token: 0x06001B53 RID: 6995 RVA: 0x000C2BB8 File Offset: 0x000C0DB8
	public void Close()
	{
		Tools.instance.TargetLunTiNum = this.hasCompleteLunTi.Count;
		NpcJieSuanManager.inst.npcStatus.SetNpcStatus(this.npcId, 12);
		PanelMamager.CanOpenOrClose = true;
		Tools.instance.loadMapScenes(Tools.instance.FinalScene, true);
	}

	// Token: 0x06001B54 RID: 6996 RVA: 0x000C2C0C File Offset: 0x000C0E0C
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

	// Token: 0x06001B55 RID: 6997 RVA: 0x000C2CB0 File Offset: 0x000C0EB0
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

	// Token: 0x06001B56 RID: 6998 RVA: 0x000C2D44 File Offset: 0x000C0F44
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

	// Token: 0x040015C2 RID: 5570
	public static LunDaoManager inst;

	// Token: 0x040015C3 RID: 5571
	public LunDaoAmrMag lunDaoAmrMag;

	// Token: 0x040015C4 RID: 5572
	public SelectLunTi selectLunTi;

	// Token: 0x040015C5 RID: 5573
	public PlayerController playerController;

	// Token: 0x040015C6 RID: 5574
	public NpcController npcController;

	// Token: 0x040015C7 RID: 5575
	public LunDaoCardMag lunDaoCardMag;

	// Token: 0x040015C8 RID: 5576
	public LunTiMag lunTiMag;

	// Token: 0x040015C9 RID: 5577
	public LunDaoPanel lunDaoPanel;

	// Token: 0x040015CA RID: 5578
	public LunDaoSuccess lunDaoSuccessPanel;

	// Token: 0x040015CB RID: 5579
	public Dictionary<int, string> lunDaoStateNameDictionary;

	// Token: 0x040015CC RID: 5580
	public List<int> selectLunTiList;

	// Token: 0x040015CD RID: 5581
	public int npcId;

	// Token: 0x040015CE RID: 5582
	public LunDaoManager.GameState gameState;

	// Token: 0x040015CF RID: 5583
	public Avatar player;

	// Token: 0x040015D0 RID: 5584
	public List<int> hasCompleteLunTi;

	// Token: 0x040015D1 RID: 5585
	public Transform AnimatorPanel;

	// Token: 0x040015D2 RID: 5586
	public List<Sprite> cardSprites;

	// Token: 0x040015D3 RID: 5587
	public List<AudioClip> musicEffectList;

	// Token: 0x040015D4 RID: 5588
	public Dictionary<int, List<int>> getWuDaoExp = new Dictionary<int, List<int>>();

	// Token: 0x040015D5 RID: 5589
	[SerializeField]
	private Text wuDaoZhi;

	// Token: 0x040015D6 RID: 5590
	public int getWuDaoZhi;

	// Token: 0x040015D7 RID: 5591
	public List<Sprite> cardSpriteList;

	// Token: 0x040015D8 RID: 5592
	public GameObject playerCardTemp;

	// Token: 0x040015D9 RID: 5593
	public GameObject LunDaoFailPanel;

	// Token: 0x040015DA RID: 5594
	public bool isOver;

	// Token: 0x02001337 RID: 4919
	public enum GameState
	{
		// Token: 0x040067D9 RID: 26585
		玩家回合 = 1,
		// Token: 0x040067DA RID: 26586
		Npc回合,
		// Token: 0x040067DB RID: 26587
		论道结束
	}
}
