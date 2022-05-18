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

// Token: 0x0200027B RID: 635
public class MapInstComport : BaseMapCompont
{
	// Token: 0x0600138C RID: 5004 RVA: 0x000124B9 File Offset: 0x000106B9
	protected override void Awake()
	{
		base.Awake();
	}

	// Token: 0x0600138D RID: 5005 RVA: 0x000B4F54 File Offset: 0x000B3154
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

	// Token: 0x0600138E RID: 5006 RVA: 0x000124C1 File Offset: 0x000106C1
	public override void Update()
	{
		if (WASDMove.Inst != null && WASDMove.Inst.IsMoved)
		{
			this.TrySetState();
			base.Update();
		}
	}

	// Token: 0x0600138F RID: 5007 RVA: 0x000124E8 File Offset: 0x000106E8
	private void LateUpdate()
	{
		if (!this.initCaiJi)
		{
			this.initCaiJi = true;
			this.CheckFubenCaiJi();
		}
	}

	// Token: 0x06001390 RID: 5008 RVA: 0x000B4FF4 File Offset: 0x000B31F4
	public override void addOption(int talkID)
	{
		int key = int.Parse(Tools.getScreenName().Replace("F", ""));
		new AddOption
		{
			AllMapShiJianOptionJsonData = jsonData.instance.FuBenJsonData[key][1],
			AllMapOptionJsonData = jsonData.instance.FuBenJsonData[key][2]
		}.addOption(talkID);
	}

	// Token: 0x06001391 RID: 5009 RVA: 0x000042DD File Offset: 0x000024DD
	public override void BaseSetFlag()
	{
	}

	// Token: 0x06001392 RID: 5010 RVA: 0x000B5060 File Offset: 0x000B3260
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

	// Token: 0x06001393 RID: 5011 RVA: 0x000124FF File Offset: 0x000106FF
	private void SetSpeed0()
	{
		MapPlayerController.Inst.SetSpeed(0);
	}

	// Token: 0x06001394 RID: 5012 RVA: 0x0001250C File Offset: 0x0001070C
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

	// Token: 0x06001395 RID: 5013 RVA: 0x000B5170 File Offset: 0x000B3370
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

	// Token: 0x06001396 RID: 5014 RVA: 0x000B5394 File Offset: 0x000B3594
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

	// Token: 0x06001397 RID: 5015 RVA: 0x000B54DC File Offset: 0x000B36DC
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

	// Token: 0x06001398 RID: 5016 RVA: 0x0001254A File Offset: 0x0001074A
	private void showDisable()
	{
		this.NodeSprite.sprite = this.sprites[0];
		this.setImageState(0);
	}

	// Token: 0x06001399 RID: 5017 RVA: 0x0001256A File Offset: 0x0001076A
	private void showFan()
	{
		this.NodeSprite.sprite = this.sprites[1];
		this.setImageState(1);
	}

	// Token: 0x0600139A RID: 5018 RVA: 0x0001258A File Offset: 0x0001078A
	protected virtual int GetGrideNum()
	{
		return base.transform.parent.GetComponent<FubenGrid>().num;
	}

	// Token: 0x0600139B RID: 5019 RVA: 0x000B563C File Offset: 0x000B383C
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

	// Token: 0x0600139C RID: 5020 RVA: 0x000125A1 File Offset: 0x000107A1
	private void checkShowLine(int index, GameObject obj)
	{
		if (this.nextIndex.Contains(index))
		{
			obj.SetActive(true);
		}
	}

	// Token: 0x0600139D RID: 5021 RVA: 0x000125B8 File Offset: 0x000107B8
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

	// Token: 0x0600139E RID: 5022 RVA: 0x000125D5 File Offset: 0x000107D5
	private MapInstComport.NodeType Filter(MapInstComport.NodeType lastValue, MapInstComport.NodeType newValue)
	{
		return newValue;
	}

	// Token: 0x0600139F RID: 5023 RVA: 0x000B56FC File Offset: 0x000B38FC
	public override void setAvatarNowMapIndex()
	{
		Tools.instance.fubenLastIndex = this.ComAvatar.fubenContorl[Tools.getScreenName()].NowIndex;
		this.ComAvatar.fubenContorl[Tools.getScreenName()].NowIndex = this.NodeIndex;
	}

	// Token: 0x060013A0 RID: 5024 RVA: 0x000125D8 File Offset: 0x000107D8
	public override int getAvatarNowMapIndex()
	{
		return PlayerEx.Player.fubenContorl[Tools.getScreenName()].NowIndex;
	}

	// Token: 0x060013A1 RID: 5025 RVA: 0x000042DD File Offset: 0x000024DD
	public override void SetStatic()
	{
	}

	// Token: 0x060013A2 RID: 5026 RVA: 0x000B5750 File Offset: 0x000B3950
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

	// Token: 0x060013A3 RID: 5027 RVA: 0x000B591C File Offset: 0x000B3B1C
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
		int num = (int)this.MapRandomJsonData[string.Concat(EventID)]["EventData"].n;
		int monstarID = (int)this.MapRandomJsonData[string.Concat(EventID)]["MosterID"].n;
		int num2 = (int)this.MapRandomJsonData[string.Concat(EventID)]["once"].n;
		switch ((int)this.MapRandomJsonData[string.Concat(EventID)]["EventList"].n)
		{
		case 0:
			Object.Instantiate<GameObject>(Resources.Load<GameObject>("talkPrefab/TalkPrefab/talk" + num));
			return;
		case 1:
			if (avatar.SuiJiShiJian.HasField(Tools.getScreenName()) && avatar.SuiJiShiJian[Tools.getScreenName()].list.Find((JSONObject aa) => (int)aa.n == EventID) != null)
			{
				Object.Instantiate<GameObject>(Resources.Load<GameObject>("talkPrefab/TalkPrefab/talk" + 5001));
				return;
			}
			if (num2 != 0)
			{
				if (!avatar.SuiJiShiJian.HasField(Tools.getScreenName()))
				{
					avatar.SuiJiShiJian.AddField(Tools.getScreenName(), new JSONObject(JSONObject.Type.ARRAY));
				}
				avatar.SuiJiShiJian[Tools.getScreenName()].Add(EventID);
			}
			this.addOption(num);
			return;
		case 2:
			Tools.instance.MonstarID = monstarID;
			Object.Instantiate<GameObject>(Resources.Load<GameObject>("talkPrefab/FightPrefab/Fight" + num));
			return;
		default:
			return;
		}
	}

	// Token: 0x04000F2F RID: 3887
	[Tooltip("是否显示图标的名字和图片")]
	public bool ShowStatic = true;

	// Token: 0x04000F30 RID: 3888
	[Tooltip("固定副本的名称，只有当IsStatic选项选择才有效果")]
	public string NameStr = "";

	// Token: 0x04000F31 RID: 3889
	[Tooltip("固定场景的图片")]
	public Sprite BuildSpriteImage;

	// Token: 0x04000F32 RID: 3890
	[Header("以下配置为固定设置，不需要配置")]
	public SpriteRenderer NodeSprite;

	// Token: 0x04000F33 RID: 3891
	public SpriteRenderer wenHao;

	// Token: 0x04000F34 RID: 3892
	public SpriteRenderer BuildSprite;

	// Token: 0x04000F35 RID: 3893
	public Text nametext;

	// Token: 0x04000F36 RID: 3894
	public List<Sprite> sprites;

	// Token: 0x04000F37 RID: 3895
	public Animation lineTop;

	// Token: 0x04000F38 RID: 3896
	public Animation lineDown;

	// Token: 0x04000F39 RID: 3897
	public Animation lineLeft;

	// Token: 0x04000F3A RID: 3898
	public Animation lineRight;

	// Token: 0x04000F3B RID: 3899
	private float m_LastTimeToggledInventory;

	// Token: 0x04000F3C RID: 3900
	public Value<MapInstComport.NodeType> State = new Value<MapInstComport.NodeType>(MapInstComport.NodeType.Null);

	// Token: 0x04000F3D RID: 3901
	public Attempt<MapInstComport.NodeType> SetState = new Attempt<MapInstComport.NodeType>();

	// Token: 0x04000F3E RID: 3902
	private bool initCaiJi;

	// Token: 0x0200027C RID: 636
	public enum NodeType
	{
		// Token: 0x04000F40 RID: 3904
		Null,
		// Token: 0x04000F41 RID: 3905
		Disable,
		// Token: 0x04000F42 RID: 3906
		Fan,
		// Token: 0x04000F43 RID: 3907
		Zheng
	}
}
