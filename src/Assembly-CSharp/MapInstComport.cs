using System;
using System.Collections.Generic;
using CaiJi;
using Fungus;
using GUIPackage;
using JSONClass;
using KBEngine;
using UltimateSurvival;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using YSGame.TuJian;

// Token: 0x0200018F RID: 399
public class MapInstComport : BaseMapCompont
{
	// Token: 0x0600110F RID: 4367 RVA: 0x000667BC File Offset: 0x000649BC
	protected override void Awake()
	{
		base.Awake();
	}

	// Token: 0x06001110 RID: 4368 RVA: 0x000667C4 File Offset: 0x000649C4
	protected override void Start()
	{
		int key = int.Parse(Tools.getScreenName().Replace("F", ""));
		this.AllMapCastTimeJsonData = jsonData.instance.FuBenJsonData[key][3];
		this.MapRandomJsonData = jsonData.instance.FuBenJsonData[key][0];
		this.SetState.SetTryer(new Attempt<MapInstComport.NodeType>.GenericTryerDelegate(this.TryChange_State));
		this.State.AddChangeListener(new Action(this.chengeState));
		this.State.Set(MapInstComport.NodeType.Disable);
		base.Start();
	}

	// Token: 0x06001111 RID: 4369 RVA: 0x00066863 File Offset: 0x00064A63
	public override void Update()
	{
		if (WASDMove.Inst != null && WASDMove.Inst.IsMoved)
		{
			this.TrySetState();
			base.Update();
		}
	}

	// Token: 0x06001112 RID: 4370 RVA: 0x0006688A File Offset: 0x00064A8A
	private void LateUpdate()
	{
		if (!this.initCaiJi)
		{
			this.initCaiJi = true;
			this.CheckFubenCaiJi();
		}
	}

	// Token: 0x06001113 RID: 4371 RVA: 0x000668A4 File Offset: 0x00064AA4
	public override void addOption(int talkID)
	{
		int key = int.Parse(Tools.getScreenName().Replace("F", ""));
		new AddOption
		{
			AllMapShiJianOptionJsonData = jsonData.instance.FuBenJsonData[key][1],
			AllMapOptionJsonData = jsonData.instance.FuBenJsonData[key][2]
		}.addOption(talkID);
	}

	// Token: 0x06001114 RID: 4372 RVA: 0x00004095 File Offset: 0x00002295
	public override void BaseSetFlag()
	{
	}

	// Token: 0x06001115 RID: 4373 RVA: 0x00066910 File Offset: 0x00064B10
	public override void AvatarMoveToThis()
	{
		if (AllMapManage.instance != null)
		{
			iTween.MoveTo(AllMapManage.instance.MapPlayerController.gameObject, iTween.Hash(new object[]
			{
				"x",
				base.transform.position.x,
				"y",
				base.transform.position.y - 0.4f,
				"z",
				base.transform.position.z,
				"time",
				0.3f,
				"islocal",
				false,
				"EaseType",
				"linear"
			}));
			MapPlayerController.Inst.SetSpeed(1);
			base.Invoke("SetSpeed0", 0.3f);
			WASDMove.waitTime = 0.3f;
			WASDMove.needWait = true;
			this.setAvatarNowMapIndex();
		}
	}

	// Token: 0x06001116 RID: 4374 RVA: 0x00066A1D File Offset: 0x00064C1D
	private void SetSpeed0()
	{
		MapPlayerController.Inst.SetSpeed(0);
	}

	// Token: 0x06001117 RID: 4375 RVA: 0x00066A2A File Offset: 0x00064C2A
	protected void chengeState()
	{
		if (this.State.Is(MapInstComport.NodeType.Disable))
		{
			this.showDisable();
		}
		if (this.State.Is(MapInstComport.NodeType.Fan))
		{
			this.showFan();
		}
		if (this.State.Is(MapInstComport.NodeType.Zheng))
		{
			this.showZheng();
		}
	}

	// Token: 0x06001118 RID: 4376 RVA: 0x00066A68 File Offset: 0x00064C68
	public void setImageState(int state)
	{
		switch (state)
		{
		case 0:
			this.wenHao.gameObject.SetActive(false);
			this.BuildSprite.gameObject.SetActive(false);
			this.nametext.transform.parent.parent.gameObject.SetActive(false);
			this.lineDown.transform.parent.gameObject.SetActive(false);
			this.lineTop.transform.parent.gameObject.SetActive(false);
			this.lineLeft.transform.parent.gameObject.SetActive(false);
			this.lineRight.transform.parent.gameObject.SetActive(false);
			return;
		case 1:
			this.wenHao.gameObject.SetActive(true);
			this.BuildSprite.gameObject.SetActive(false);
			this.lineDown.transform.parent.gameObject.SetActive(false);
			this.lineTop.transform.parent.gameObject.SetActive(false);
			this.lineLeft.transform.parent.gameObject.SetActive(false);
			this.lineRight.transform.parent.gameObject.SetActive(false);
			this.nametext.transform.parent.parent.gameObject.SetActive(false);
			return;
		case 2:
			this.wenHao.gameObject.SetActive(false);
			this.BuildSprite.gameObject.SetActive(false);
			this.nametext.transform.parent.parent.gameObject.SetActive(false);
			if (this.IsStatic && this.ShowStatic)
			{
				this.nametext.transform.parent.parent.gameObject.SetActive(true);
				this.BuildSprite.gameObject.SetActive(true);
				this.BuildSprite.sprite = this.BuildSpriteImage;
				this.nametext.text = this.NameStr;
			}
			return;
		default:
			return;
		}
	}

	// Token: 0x06001119 RID: 4377 RVA: 0x00066C8C File Offset: 0x00064E8C
	public override void fuBenSetClick()
	{
		Flowchart component = base.transform.Find("flowchat").GetComponent<Flowchart>();
		string screenName = Tools.getScreenName();
		jsonData.instance.gameObject.GetComponent<ItemDatebase>();
		bool flag = false;
		UnityAction clickAction = null;
		using (List<JSONObject>.Enumerator enumerator = jsonData.instance.CaiYaoDiaoLuo.list.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				JSONObject temp = enumerator.Current;
				if (screenName == temp["FuBen"].str && this.NodeIndex == (int)temp["MapIndex"].n)
				{
					flag = true;
					TuJianManager.Inst.UnlockMap(screenName);
					JSONObject temp2 = temp;
					clickAction = delegate()
					{
						ResManager.inst.LoadPrefab("CaiJiPanel").Inst(null);
						CaiJiUIMag.inst.OpenCaiJi(temp["id"].I);
					};
				}
			}
		}
		if (screenName == "F26" && LingHeCaiJi.DataDict.ContainsKey(this.NodeIndex))
		{
			clickAction = delegate()
			{
				LingHeCaiJiManager.TryOpenCaiJi(this.NodeIndex);
			};
			flag = true;
		}
		if (SceneBtnMag.inst != null)
		{
			SceneBtnMag.inst.SetFubenCaiJi(flag, clickAction);
		}
		if (!flag)
		{
			component.ExecuteBlock("AvatarIn");
		}
	}

	// Token: 0x0600111A RID: 4378 RVA: 0x00066DD4 File Offset: 0x00064FD4
	public void CheckFubenCaiJi()
	{
		if (PlayerEx.Player.fubenContorl[Tools.getScreenName()].NowIndex == this.NodeIndex)
		{
			base.transform.Find("flowchat").GetComponent<Flowchart>();
			string screenName = Tools.getScreenName();
			jsonData.instance.gameObject.GetComponent<ItemDatebase>();
			bool flag = false;
			UnityAction fubenCaiJiAction = null;
			using (List<JSONObject>.Enumerator enumerator = jsonData.instance.CaiYaoDiaoLuo.list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					JSONObject temp = enumerator.Current;
					if (screenName == temp["FuBen"].str && this.NodeIndex == (int)temp["MapIndex"].n)
					{
						flag = true;
						TuJianManager.Inst.UnlockMap(screenName);
						JSONObject temp2 = temp;
						fubenCaiJiAction = delegate()
						{
							ResManager.inst.LoadPrefab("CaiJiPanel").Inst(null);
							CaiJiUIMag.inst.OpenCaiJi(temp["id"].I);
						};
					}
				}
			}
			if (flag)
			{
				SceneBtnMag.FubenCaiJiAction = fubenCaiJiAction;
				SceneBtnMag.hasCaiJi = true;
			}
			if (screenName == "F26" && LingHeCaiJi.DataDict.ContainsKey(this.NodeIndex))
			{
				fubenCaiJiAction = delegate()
				{
					LingHeCaiJiManager.TryOpenCaiJi(this.NodeIndex);
				};
				SceneBtnMag.FubenCaiJiAction = fubenCaiJiAction;
				SceneBtnMag.hasCaiJi = true;
			}
		}
	}

	// Token: 0x0600111B RID: 4379 RVA: 0x00066F34 File Offset: 0x00065134
	private void showDisable()
	{
		this.NodeSprite.sprite = this.sprites[0];
		this.setImageState(0);
	}

	// Token: 0x0600111C RID: 4380 RVA: 0x00066F54 File Offset: 0x00065154
	private void showFan()
	{
		this.NodeSprite.sprite = this.sprites[1];
		this.setImageState(1);
	}

	// Token: 0x0600111D RID: 4381 RVA: 0x00066F74 File Offset: 0x00065174
	protected virtual int GetGrideNum()
	{
		return base.transform.parent.GetComponent<FubenGrid>().num;
	}

	// Token: 0x0600111E RID: 4382 RVA: 0x00066F8C File Offset: 0x0006518C
	private void showZheng()
	{
		int grideNum = this.GetGrideNum();
		this.NodeSprite.sprite = this.sprites[2];
		this.checkShowLine(this.NodeIndex - 1, this.lineLeft.transform.parent.gameObject);
		this.checkShowLine(this.NodeIndex + 1, this.lineRight.transform.parent.gameObject);
		this.checkShowLine(this.NodeIndex - grideNum, this.lineDown.transform.parent.gameObject);
		this.checkShowLine(this.NodeIndex + grideNum, this.lineTop.transform.parent.gameObject);
		this.setImageState(2);
	}

	// Token: 0x0600111F RID: 4383 RVA: 0x0006704A File Offset: 0x0006524A
	private void checkShowLine(int index, GameObject obj)
	{
		if (this.nextIndex.Contains(index))
		{
			obj.SetActive(true);
		}
	}

	// Token: 0x06001120 RID: 4384 RVA: 0x00067061 File Offset: 0x00065261
	protected bool TryChange_State(MapInstComport.NodeType state)
	{
		int num = 1;
		if (num != 0)
		{
			this.State.Set(state);
			EventSystem.current.SetSelectedGameObject(null);
		}
		return num != 0;
	}

	// Token: 0x06001121 RID: 4385 RVA: 0x0006707E File Offset: 0x0006527E
	private MapInstComport.NodeType Filter(MapInstComport.NodeType lastValue, MapInstComport.NodeType newValue)
	{
		return newValue;
	}

	// Token: 0x06001122 RID: 4386 RVA: 0x00067084 File Offset: 0x00065284
	public override void setAvatarNowMapIndex()
	{
		Tools.instance.fubenLastIndex = this.ComAvatar.fubenContorl[Tools.getScreenName()].NowIndex;
		this.ComAvatar.fubenContorl[Tools.getScreenName()].NowIndex = this.NodeIndex;
	}

	// Token: 0x06001123 RID: 4387 RVA: 0x000670D5 File Offset: 0x000652D5
	public override int getAvatarNowMapIndex()
	{
		return PlayerEx.Player.fubenContorl[Tools.getScreenName()].NowIndex;
	}

	// Token: 0x06001124 RID: 4388 RVA: 0x00004095 File Offset: 0x00002295
	public override void SetStatic()
	{
	}

	// Token: 0x06001125 RID: 4389 RVA: 0x000670F0 File Offset: 0x000652F0
	private void TrySetState()
	{
		if (this.ComAvatar.FuBen[Tools.getScreenName()].HasField("RoadNode") && this.ComAvatar.FuBen[Tools.getScreenName()]["RoadNode"].HasField(this.NodeIndex.ToString()))
		{
			List<JSONObject> list = this.ComAvatar.FuBen[Tools.getScreenName()]["RoadNode"][this.NodeIndex.ToString()].list;
			List<int> nextIndex = AllMapManage.instance.mapIndex[this.NodeIndex].nextIndex;
			foreach (JSONObject jsonobject in list)
			{
				if (!nextIndex.Contains(jsonobject.I))
				{
					AllMapManage.instance.mapIndex[this.NodeIndex].nextIndex.Add(jsonobject.I);
				}
			}
		}
		List<int> exploredNode = this.ComAvatar.fubenContorl[Tools.getScreenName()].ExploredNode;
		if (exploredNode.Contains(this.NodeIndex))
		{
			this.SetState.Try(MapInstComport.NodeType.Zheng);
		}
		foreach (int key in exploredNode)
		{
			if (AllMapManage.instance.mapIndex[key].nextIndex.Contains(this.NodeIndex) && this.State.Is(MapInstComport.NodeType.Disable))
			{
				this.SetState.Try(MapInstComport.NodeType.Fan);
			}
		}
	}

	// Token: 0x06001126 RID: 4390 RVA: 0x000672BC File Offset: 0x000654BC
	public override void EventRandom()
	{
		if (!base.CanClick())
		{
			return;
		}
		if (WASDMove.Inst != null)
		{
			WASDMove.Inst.IsMoved = true;
		}
		this.fuBenSetClick();
		this.movaAvatar();
		if (UINPCLeftList.Inst != null)
		{
			Debug.Log("由地图通知刷新NPC列表");
			NpcJieSuanManager.inst.isUpDateNpcList = true;
		}
		if (this.IsStatic)
		{
			return;
		}
		Avatar avatar = (Avatar)KBEngineApp.app.player();
		int EventID = base.getEventID();
		int i = this.MapRandomJsonData[string.Concat(EventID)]["EventData"].I;
		int i2 = this.MapRandomJsonData[string.Concat(EventID)]["MosterID"].I;
		int i3 = this.MapRandomJsonData[string.Concat(EventID)]["once"].I;
		switch (this.MapRandomJsonData[string.Concat(EventID)]["EventList"].I)
		{
		case 0:
			Object.Instantiate<GameObject>(Resources.Load<GameObject>("talkPrefab/TalkPrefab/talk" + i));
			return;
		case 1:
			if (avatar.SuiJiShiJian.HasField(Tools.getScreenName()) && avatar.SuiJiShiJian[Tools.getScreenName()].list.Find((JSONObject aa) => (int)aa.n == EventID) != null)
			{
				Object.Instantiate<GameObject>(Resources.Load<GameObject>("talkPrefab/TalkPrefab/talk" + 5001));
				return;
			}
			if (i3 != 0)
			{
				if (!avatar.SuiJiShiJian.HasField(Tools.getScreenName()))
				{
					avatar.SuiJiShiJian.AddField(Tools.getScreenName(), new JSONObject(JSONObject.Type.ARRAY));
				}
				avatar.SuiJiShiJian[Tools.getScreenName()].Add(EventID);
			}
			this.addOption(i);
			return;
		case 2:
			Tools.instance.MonstarID = i2;
			Object.Instantiate<GameObject>(Resources.Load<GameObject>("talkPrefab/FightPrefab/Fight" + i));
			return;
		default:
			return;
		}
	}

	// Token: 0x04000C37 RID: 3127
	[Tooltip("是否显示图标的名字和图片")]
	public bool ShowStatic = true;

	// Token: 0x04000C38 RID: 3128
	[Tooltip("固定副本的名称，只有当IsStatic选项选择才有效果")]
	public string NameStr = "";

	// Token: 0x04000C39 RID: 3129
	[Tooltip("固定场景的图片")]
	public Sprite BuildSpriteImage;

	// Token: 0x04000C3A RID: 3130
	[Header("以下配置为固定设置，不需要配置")]
	public SpriteRenderer NodeSprite;

	// Token: 0x04000C3B RID: 3131
	public SpriteRenderer wenHao;

	// Token: 0x04000C3C RID: 3132
	public SpriteRenderer BuildSprite;

	// Token: 0x04000C3D RID: 3133
	public Text nametext;

	// Token: 0x04000C3E RID: 3134
	public List<Sprite> sprites;

	// Token: 0x04000C3F RID: 3135
	public Animation lineTop;

	// Token: 0x04000C40 RID: 3136
	public Animation lineDown;

	// Token: 0x04000C41 RID: 3137
	public Animation lineLeft;

	// Token: 0x04000C42 RID: 3138
	public Animation lineRight;

	// Token: 0x04000C43 RID: 3139
	private float m_LastTimeToggledInventory;

	// Token: 0x04000C44 RID: 3140
	public Value<MapInstComport.NodeType> State = new Value<MapInstComport.NodeType>(MapInstComport.NodeType.Null);

	// Token: 0x04000C45 RID: 3141
	public Attempt<MapInstComport.NodeType> SetState = new Attempt<MapInstComport.NodeType>();

	// Token: 0x04000C46 RID: 3142
	private bool initCaiJi;

	// Token: 0x020012AC RID: 4780
	public enum NodeType
	{
		// Token: 0x04006652 RID: 26194
		Null,
		// Token: 0x04006653 RID: 26195
		Disable,
		// Token: 0x04006654 RID: 26196
		Fan,
		// Token: 0x04006655 RID: 26197
		Zheng
	}
}
