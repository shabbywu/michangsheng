using System.Collections.Generic;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LunDaoManager : MonoBehaviour
{
	public enum GameState
	{
		玩家回合 = 1,
		Npc回合,
		论道结束
	}

	public static LunDaoManager inst;

	public LunDaoAmrMag lunDaoAmrMag;

	public SelectLunTi selectLunTi;

	public PlayerController playerController;

	public NpcController npcController;

	public LunDaoCardMag lunDaoCardMag;

	public LunTiMag lunTiMag;

	public LunDaoPanel lunDaoPanel;

	public LunDaoSuccess lunDaoSuccessPanel;

	public Dictionary<int, string> lunDaoStateNameDictionary;

	public List<int> selectLunTiList;

	public int npcId;

	public GameState gameState;

	public Avatar player;

	public List<int> hasCompleteLunTi;

	public Transform AnimatorPanel;

	public List<Sprite> cardSprites;

	public List<AudioClip> musicEffectList;

	public Dictionary<int, List<int>> getWuDaoExp = new Dictionary<int, List<int>>();

	[SerializeField]
	private Text wuDaoZhi;

	public int getWuDaoZhi;

	public List<Sprite> cardSpriteList;

	public GameObject playerCardTemp;

	public GameObject LunDaoFailPanel;

	public bool isOver;

	private void Awake()
	{
		inst = this;
		npcId = Tools.instance.LunDaoNpcId;
		hasCompleteLunTi = new List<int>();
		lunDaoAmrMag = new LunDaoAmrMag();
		lunDaoStateNameDictionary = new Dictionary<int, string>();
		foreach (JSONObject item in jsonData.instance.LunDaoStateData.list)
		{
			lunDaoStateNameDictionary.Add(item["id"].I, item["ZhuangTaiInfo"].Str);
		}
	}

	private void Start()
	{
		player = Tools.instance.getPlayer();
		lunTiMag = new LunTiMag();
		lunDaoCardMag = new LunDaoCardMag();
		InitLunDao();
	}

	private void InitLunDao()
	{
		if (!Tools.instance.IsSuiJiLunTi && (Tools.instance.LunTiList == null || Tools.instance.LunTiList.Count < 1))
		{
			selectLunTi.Init();
		}
		else
		{
			StartGame();
		}
		playerController.Init();
		npcController.Init();
	}

	public void StartGame()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Expected O, but got Unknown
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Expected O, but got Unknown
		lunDaoAmrMag.PlayStartLunDao().PlayingAction((UnityAction)delegate
		{
			if (Tools.instance.LunTiList != null && Tools.instance.LunTiList.Count > 0)
			{
				selectLunTiList = new List<int>();
				Tools.instance.LunTiList.ForEach(delegate(int i)
				{
					selectLunTiList.Add(i);
				});
				Tools.instance.LunTiList = new List<int>();
			}
			else if (!Tools.instance.IsSuiJiLunTi)
			{
				selectLunTiList = new List<int>(selectLunTi.selectLunTiList);
			}
			else
			{
				selectLunTiList = GetSuiJiLuntTi(Tools.instance.LunTiNum);
				Tools.instance.IsSuiJiLunTi = false;
				Tools.instance.LunTiNum = 0;
			}
			Object.Destroy((Object)(object)((Component)selectLunTi).gameObject);
			lunTiMag.CreateLunTi(selectLunTiList, npcId);
			lunDaoCardMag.CreatePaiKu(selectLunTiList, npcId);
			lunDaoPanel.Init();
		}).CompleteAction((UnityAction)delegate
		{
			lunDaoPanel.Show();
			gameState = GameState.Npc回合;
			npcController.NpcStartRound();
		})
			.Run();
	}

	public void EndRoundCallBack()
	{
		if (gameState == GameState.玩家回合)
		{
			playerController.PlayerStartRound();
		}
		else if (gameState == GameState.Npc回合)
		{
			npcController.NpcStartRound();
		}
		else if (gameState == GameState.论道结束)
		{
			GameOver();
		}
	}

	public void GameOver()
	{
		if (!isOver)
		{
			isOver = true;
			if (hasCompleteLunTi.Count > 0)
			{
				((MonoBehaviour)this).Invoke("LunDaoSuccess", 1f);
			}
			else
			{
				((MonoBehaviour)this).Invoke("LunDaoFail", 1f);
			}
		}
	}

	public void ChuPaiCallBack()
	{
		lunTiMag.CompleteLunTi();
		lunTiMag.LunDianHeCheng();
		if (gameState == GameState.玩家回合 && inst.lunTiMag.GetNullSlot() == -1)
		{
			playerController.tips.SetActive(true);
		}
		else
		{
			playerController.tips.SetActive(false);
		}
	}

	public void LunDaoSuccess()
	{
		lunDaoSuccessPanel.Init();
	}

	public void LunDaoFail()
	{
		if (NpcJieSuanManager.inst.lunDaoNpcList.Contains(npcId))
		{
			NpcJieSuanManager.inst.lunDaoNpcList.Remove(npcId);
		}
		NpcJieSuanManager.inst.npcNoteBook.NoteLunDaoFail(npcId, player.name);
		LunDaoFailPanel.SetActive(true);
	}

	public void Close()
	{
		Tools.instance.TargetLunTiNum = hasCompleteLunTi.Count;
		NpcJieSuanManager.inst.npcStatus.SetNpcStatus(npcId, 12);
		PanelMamager.CanOpenOrClose = true;
		Tools.instance.loadMapScenes(Tools.instance.FinalScene);
	}

	public void AddWuDaoZhi(int wudaoId, int addNum)
	{
		int i = jsonData.instance.AvatarJsonData[npcId.ToString()]["wuDaoJson"][wudaoId.ToString()]["level"].I;
		int wuDaoLevelByType = Tools.instance.getPlayer().wuDaoMag.getWuDaoLevelByType(wudaoId);
		if (wuDaoLevelByType < i)
		{
			addNum = addNum * LunDaoReduceData.DataDict[i - wuDaoLevelByType].ShuaiJianXiShu / 100;
		}
		getWuDaoZhi += addNum;
		wuDaoZhi.text = getWuDaoZhi.ToString();
	}

	public void AddWuDaoExp(int wuDaoId)
	{
		foreach (int item in lunTiMag.targetLunTiDictionary[wuDaoId])
		{
			if (!getWuDaoExp.ContainsKey(wuDaoId))
			{
				getWuDaoExp.Add(wuDaoId, new List<int> { item });
			}
			else
			{
				getWuDaoExp[wuDaoId].Add(item);
			}
		}
	}

	public List<int> GetSuiJiLuntTi(int num)
	{
		List<int> list = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
		List<int> list2 = new List<int>();
		int num2 = 0;
		for (int i = 0; i < num; i++)
		{
			num2 = lunDaoCardMag.getRandom(0, list.Count - 1);
			list2.Add(list[num2]);
			list.RemoveAt(num2);
		}
		return list2;
	}
}
