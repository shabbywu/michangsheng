using System.Collections.Generic;
using CaiJi;
using Fungus;
using GUIPackage;
using JSONClass;
using KBEngine;
using UltimateSurvival;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;
using YSGame.TuJian;

public class MapInstComport : BaseMapCompont
{
	public enum NodeType
	{
		Null,
		Disable,
		Fan,
		Zheng
	}

	[Tooltip("是否显示图标的名字和图片")]
	public bool ShowStatic = true;

	[Tooltip("固定副本的名称，只有当IsStatic选项选择才有效果")]
	public string NameStr = "";

	[Tooltip("固定场景的图片")]
	public Sprite BuildSpriteImage;

	[Header("以下配置为固定设置，不需要配置")]
	public SpriteRenderer NodeSprite;

	public SpriteRenderer wenHao;

	public SpriteRenderer BuildSprite;

	public Text nametext;

	public List<Sprite> sprites;

	public Animation lineTop;

	public Animation lineDown;

	public Animation lineLeft;

	public Animation lineRight;

	private float m_LastTimeToggledInventory;

	public Value<NodeType> State = new Value<NodeType>(NodeType.Null);

	public Attempt<NodeType> SetState = new Attempt<NodeType>();

	private bool initCaiJi;

	protected override void Awake()
	{
		base.Awake();
	}

	protected override void Start()
	{
		int key = int.Parse(Tools.getScreenName().Replace("F", ""));
		AllMapCastTimeJsonData = jsonData.instance.FuBenJsonData[key][3];
		MapRandomJsonData = jsonData.instance.FuBenJsonData[key][0];
		SetState.SetTryer(TryChange_State);
		State.AddChangeListener(chengeState);
		State.Set(NodeType.Disable);
		base.Start();
	}

	public override void Update()
	{
		if ((Object)(object)WASDMove.Inst != (Object)null && WASDMove.Inst.IsMoved)
		{
			TrySetState();
			base.Update();
		}
	}

	private void LateUpdate()
	{
		if (!initCaiJi)
		{
			initCaiJi = true;
			CheckFubenCaiJi();
		}
	}

	public override void addOption(int talkID)
	{
		int key = int.Parse(Tools.getScreenName().Replace("F", ""));
		AddOption obj = new AddOption();
		obj.AllMapShiJianOptionJsonData = jsonData.instance.FuBenJsonData[key][1];
		obj.AllMapOptionJsonData = jsonData.instance.FuBenJsonData[key][2];
		obj.addOption(talkID);
	}

	public override void BaseSetFlag()
	{
	}

	public override void AvatarMoveToThis()
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)AllMapManage.instance != (Object)null)
		{
			iTween.MoveTo(((Component)AllMapManage.instance.MapPlayerController).gameObject, iTween.Hash(new object[12]
			{
				"x",
				((Component)this).transform.position.x,
				"y",
				((Component)this).transform.position.y - 0.4f,
				"z",
				((Component)this).transform.position.z,
				"time",
				0.3f,
				"islocal",
				false,
				"EaseType",
				"linear"
			}));
			MapPlayerController.Inst.SetSpeed(1);
			((MonoBehaviour)this).Invoke("SetSpeed0", 0.3f);
			WASDMove.waitTime = 0.3f;
			WASDMove.needWait = true;
			setAvatarNowMapIndex();
		}
	}

	private void SetSpeed0()
	{
		MapPlayerController.Inst.SetSpeed(0);
	}

	protected void chengeState()
	{
		if (State.Is(NodeType.Disable))
		{
			showDisable();
		}
		if (State.Is(NodeType.Fan))
		{
			showFan();
		}
		if (State.Is(NodeType.Zheng))
		{
			showZheng();
		}
	}

	public void setImageState(int state)
	{
		switch (state)
		{
		case 0:
			((Component)wenHao).gameObject.SetActive(false);
			((Component)BuildSprite).gameObject.SetActive(false);
			((Component)((Component)nametext).transform.parent.parent).gameObject.SetActive(false);
			((Component)((Component)lineDown).transform.parent).gameObject.SetActive(false);
			((Component)((Component)lineTop).transform.parent).gameObject.SetActive(false);
			((Component)((Component)lineLeft).transform.parent).gameObject.SetActive(false);
			((Component)((Component)lineRight).transform.parent).gameObject.SetActive(false);
			break;
		case 1:
			((Component)wenHao).gameObject.SetActive(true);
			((Component)BuildSprite).gameObject.SetActive(false);
			((Component)((Component)lineDown).transform.parent).gameObject.SetActive(false);
			((Component)((Component)lineTop).transform.parent).gameObject.SetActive(false);
			((Component)((Component)lineLeft).transform.parent).gameObject.SetActive(false);
			((Component)((Component)lineRight).transform.parent).gameObject.SetActive(false);
			((Component)((Component)nametext).transform.parent.parent).gameObject.SetActive(false);
			break;
		case 2:
			((Component)wenHao).gameObject.SetActive(false);
			((Component)BuildSprite).gameObject.SetActive(false);
			((Component)((Component)nametext).transform.parent.parent).gameObject.SetActive(false);
			if (IsStatic && ShowStatic)
			{
				((Component)((Component)nametext).transform.parent.parent).gameObject.SetActive(true);
				((Component)BuildSprite).gameObject.SetActive(true);
				BuildSprite.sprite = BuildSpriteImage;
				nametext.text = NameStr;
			}
			break;
		}
	}

	public override void fuBenSetClick()
	{
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Expected O, but got Unknown
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Expected O, but got Unknown
		Flowchart component = ((Component)((Component)this).transform.Find("flowchat")).GetComponent<Flowchart>();
		string screenName = Tools.getScreenName();
		((Component)jsonData.instance).gameObject.GetComponent<ItemDatebase>();
		bool flag = false;
		UnityAction clickAction = null;
		foreach (JSONObject temp in jsonData.instance.CaiYaoDiaoLuo.list)
		{
			if (screenName == temp["FuBen"].str && NodeIndex == (int)temp["MapIndex"].n)
			{
				flag = true;
				TuJianManager.Inst.UnlockMap(screenName);
				_ = temp;
				clickAction = (UnityAction)delegate
				{
					ResManager.inst.LoadPrefab("CaiJiPanel").Inst();
					CaiJiUIMag.inst.OpenCaiJi(temp["id"].I);
				};
			}
		}
		if (screenName == "F26" && LingHeCaiJi.DataDict.ContainsKey(NodeIndex))
		{
			clickAction = (UnityAction)delegate
			{
				LingHeCaiJiManager.TryOpenCaiJi(NodeIndex);
			};
			flag = true;
		}
		if ((Object)(object)SceneBtnMag.inst != (Object)null)
		{
			SceneBtnMag.inst.SetFubenCaiJi(flag, clickAction);
		}
		if (!flag)
		{
			component.ExecuteBlock("AvatarIn");
		}
	}

	public void CheckFubenCaiJi()
	{
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Expected O, but got Unknown
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Expected O, but got Unknown
		if (PlayerEx.Player.fubenContorl[Tools.getScreenName()].NowIndex != NodeIndex)
		{
			return;
		}
		((Component)((Component)this).transform.Find("flowchat")).GetComponent<Flowchart>();
		string screenName = Tools.getScreenName();
		((Component)jsonData.instance).gameObject.GetComponent<ItemDatebase>();
		bool flag = false;
		UnityAction fubenCaiJiAction = null;
		foreach (JSONObject temp in jsonData.instance.CaiYaoDiaoLuo.list)
		{
			if (screenName == temp["FuBen"].str && NodeIndex == (int)temp["MapIndex"].n)
			{
				flag = true;
				TuJianManager.Inst.UnlockMap(screenName);
				_ = temp;
				fubenCaiJiAction = (UnityAction)delegate
				{
					ResManager.inst.LoadPrefab("CaiJiPanel").Inst();
					CaiJiUIMag.inst.OpenCaiJi(temp["id"].I);
				};
			}
		}
		if (flag)
		{
			SceneBtnMag.FubenCaiJiAction = fubenCaiJiAction;
			SceneBtnMag.hasCaiJi = true;
		}
		if (screenName == "F26" && LingHeCaiJi.DataDict.ContainsKey(NodeIndex))
		{
			fubenCaiJiAction = (UnityAction)delegate
			{
				LingHeCaiJiManager.TryOpenCaiJi(NodeIndex);
			};
			SceneBtnMag.FubenCaiJiAction = fubenCaiJiAction;
			SceneBtnMag.hasCaiJi = true;
		}
	}

	private void showDisable()
	{
		NodeSprite.sprite = sprites[0];
		setImageState(0);
	}

	private void showFan()
	{
		NodeSprite.sprite = sprites[1];
		setImageState(1);
	}

	protected virtual int GetGrideNum()
	{
		return ((Component)((Component)this).transform.parent).GetComponent<FubenGrid>().num;
	}

	private void showZheng()
	{
		int grideNum = GetGrideNum();
		NodeSprite.sprite = sprites[2];
		checkShowLine(NodeIndex - 1, ((Component)((Component)lineLeft).transform.parent).gameObject);
		checkShowLine(NodeIndex + 1, ((Component)((Component)lineRight).transform.parent).gameObject);
		checkShowLine(NodeIndex - grideNum, ((Component)((Component)lineDown).transform.parent).gameObject);
		checkShowLine(NodeIndex + grideNum, ((Component)((Component)lineTop).transform.parent).gameObject);
		setImageState(2);
	}

	private void checkShowLine(int index, GameObject obj)
	{
		if (nextIndex.Contains(index))
		{
			obj.SetActive(true);
		}
	}

	protected bool TryChange_State(NodeType state)
	{
		if (true)
		{
			State.Set(state);
			EventSystem.current.SetSelectedGameObject((GameObject)null);
		}
		return true;
	}

	private NodeType Filter(NodeType lastValue, NodeType newValue)
	{
		return newValue;
	}

	public override void setAvatarNowMapIndex()
	{
		Tools.instance.fubenLastIndex = ComAvatar.fubenContorl[Tools.getScreenName()].NowIndex;
		ComAvatar.fubenContorl[Tools.getScreenName()].NowIndex = NodeIndex;
	}

	public override int getAvatarNowMapIndex()
	{
		return PlayerEx.Player.fubenContorl[Tools.getScreenName()].NowIndex;
	}

	public override void SetStatic()
	{
	}

	private void TrySetState()
	{
		if (ComAvatar.FuBen[Tools.getScreenName()].HasField("RoadNode") && ComAvatar.FuBen[Tools.getScreenName()]["RoadNode"].HasField(NodeIndex.ToString()))
		{
			List<JSONObject> list = ComAvatar.FuBen[Tools.getScreenName()]["RoadNode"][NodeIndex.ToString()].list;
			List<int> list2 = AllMapManage.instance.mapIndex[NodeIndex].nextIndex;
			foreach (JSONObject item in list)
			{
				if (!list2.Contains(item.I))
				{
					AllMapManage.instance.mapIndex[NodeIndex].nextIndex.Add(item.I);
				}
			}
		}
		List<int> exploredNode = ComAvatar.fubenContorl[Tools.getScreenName()].ExploredNode;
		if (exploredNode.Contains(NodeIndex))
		{
			SetState.Try(NodeType.Zheng);
		}
		foreach (int item2 in exploredNode)
		{
			if (AllMapManage.instance.mapIndex[item2].nextIndex.Contains(NodeIndex) && State.Is(NodeType.Disable))
			{
				SetState.Try(NodeType.Fan);
			}
		}
	}

	public override void EventRandom()
	{
		if (!CanClick())
		{
			return;
		}
		if ((Object)(object)WASDMove.Inst != (Object)null)
		{
			WASDMove.Inst.IsMoved = true;
		}
		fuBenSetClick();
		movaAvatar();
		if ((Object)(object)UINPCLeftList.Inst != (Object)null)
		{
			Debug.Log((object)"由地图通知刷新NPC列表");
			NpcJieSuanManager.inst.isUpDateNpcList = true;
		}
		if (IsStatic)
		{
			return;
		}
		Avatar avatar = (Avatar)KBEngineApp.app.player();
		int EventID = getEventID();
		int i = MapRandomJsonData[string.Concat(EventID)]["EventData"].I;
		int i2 = MapRandomJsonData[string.Concat(EventID)]["MosterID"].I;
		int i3 = MapRandomJsonData[string.Concat(EventID)]["once"].I;
		switch (MapRandomJsonData[string.Concat(EventID)]["EventList"].I)
		{
		case 0:
			Object.Instantiate<GameObject>(Resources.Load<GameObject>("talkPrefab/TalkPrefab/talk" + i));
			break;
		case 1:
			if (avatar.SuiJiShiJian.HasField(Tools.getScreenName()) && avatar.SuiJiShiJian[Tools.getScreenName()].list.Find((JSONObject aa) => (int)aa.n == EventID) != null)
			{
				Object.Instantiate<GameObject>(Resources.Load<GameObject>("talkPrefab/TalkPrefab/talk" + 5001));
				break;
			}
			if (i3 != 0)
			{
				if (!avatar.SuiJiShiJian.HasField(Tools.getScreenName()))
				{
					avatar.SuiJiShiJian.AddField(Tools.getScreenName(), new JSONObject(JSONObject.Type.ARRAY));
				}
				avatar.SuiJiShiJian[Tools.getScreenName()].Add(EventID);
			}
			addOption(i);
			break;
		case 2:
			Tools.instance.MonstarID = i2;
			Object.Instantiate<GameObject>(Resources.Load<GameObject>("talkPrefab/FightPrefab/Fight" + i));
			break;
		}
	}
}
