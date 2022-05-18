using System;
using System.Collections;
using System.Collections.Generic;
using Bag;
using Fungus;
using JSONClass;
using KBEngine;
using Newtonsoft.Json.Linq;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200028B RID: 651
public class MapSeaCompent : MapInstComport
{
	// Token: 0x17000257 RID: 599
	// (get) Token: 0x060013E8 RID: 5096 RVA: 0x000128DE File Offset: 0x00010ADE
	public int X
	{
		get
		{
			return FuBenMap.getIndexX(this.NodeIndex, EndlessSeaMag.MapWide);
		}
	}

	// Token: 0x17000258 RID: 600
	// (get) Token: 0x060013E9 RID: 5097 RVA: 0x000128F0 File Offset: 0x00010AF0
	public int Y
	{
		get
		{
			return FuBenMap.getIndexY(this.NodeIndex, EndlessSeaMag.MapWide);
		}
	}

	// Token: 0x060013EA RID: 5098 RVA: 0x00012902 File Offset: 0x00010B02
	protected override void Awake()
	{
		this.NodeIndex = int.Parse(base.name);
		this.AllMapCastTimeJsonData = jsonData.instance.AllMapCastTimeJsonData;
		this.MapRandomJsonData = jsonData.instance.MapRandomJsonData;
		this.AutoSetPositon();
	}

	// Token: 0x060013EB RID: 5099 RVA: 0x0001293B File Offset: 0x00010B3B
	protected override void Start()
	{
		this.ResetTime = (float)(jsonData.GetRandom() % 20) + 0.2f * (float)(jsonData.GetRandom() % 6);
		this.StartSeting();
		this.WhetherHasIsLand = this.HasIsLand();
		this.WhetherHasJiZhi = this.HasBoss();
	}

	// Token: 0x060013EC RID: 5100 RVA: 0x00012979 File Offset: 0x00010B79
	public override void Update()
	{
		if (WASDMove.Inst != null)
		{
			if (WASDMove.Inst.IsMoved)
			{
				this.Refresh();
				return;
			}
		}
		else
		{
			this.Refresh();
		}
	}

	// Token: 0x060013ED RID: 5101 RVA: 0x000B6E30 File Offset: 0x000B5030
	public void AddLangHua()
	{
		if (Vector3.Distance(AllMapManage.instance.MapPlayerController.transform.position, base.transform.position) <= 18f && this.LaseSetLangHuangTime > this.ResetTime)
		{
			GameObject gameObject = GameObjectPool.Get(EndlessSeaMag.Inst.LangHua);
			float num = 0.34f * (float)(jsonData.GetRandom() % 11);
			float num2 = 0.34f * (float)(jsonData.GetRandom() % 11);
			gameObject.transform.position = new Vector3(base.transform.position.x + num, base.transform.position.y + num2, base.transform.position.z);
			this.LaseSetLangHuangTime = 0f;
			int num3 = jsonData.GetRandom() % 12 + 1;
			gameObject.GetComponentInChildren<SkeletonAnimation>().AnimationName = ((num3 < 10) ? "L0" : "L") + num3 + "_1";
			this.ResetTime = (float)(jsonData.GetRandom() % 20) + 0.2f * (float)(jsonData.GetRandom() % 6) + 5f;
		}
		this.LaseSetLangHuangTime += 0.2f;
	}

	// Token: 0x060013EE RID: 5102 RVA: 0x000129A1 File Offset: 0x00010BA1
	public void SetParent(MapSeaCompent parent, int g)
	{
		this.SeaParent = parent;
		this.G = g;
		this.F = this.G + this.H;
	}

	// Token: 0x060013EF RID: 5103 RVA: 0x000129C4 File Offset: 0x00010BC4
	public bool NodeHasIsLand()
	{
		return this.WhetherHasIsLand || this.IsStatic;
	}

	// Token: 0x060013F0 RID: 5104 RVA: 0x000B6F68 File Offset: 0x000B5168
	public void Refresh()
	{
		Avatar player = Tools.instance.getPlayer();
		if (Tools.ContensInt((JArray)player.EndlessSeaAvatarSeeIsland["Island"], this.NodeIndex))
		{
			int avatarNowMapIndex = this.getAvatarNowMapIndex();
			if (this.NodeHasIsLand())
			{
				int num = int.Parse(SceneEx.NowSceneName.Replace("Sea", ""));
				if (player.HideHaiYuTanSuo.HasItem(num) && SeaHaiYuTanSuo.DataDict[num].ZuoBiao == this.NodeIndex)
				{
					this.IsLand.gameObject.SetActive(false);
					this.InIsLandBtn.gameObject.SetActive(false);
					return;
				}
				this.IsLand.gameObject.SetActive(true);
				if (!this.HideButton)
				{
					if (avatarNowMapIndex != this.NodeIndex)
					{
						this.InIsLandBtn.gameObject.SetActive(false);
						return;
					}
					this.InIsLandBtn.gameObject.SetActive(true);
					if (this.BiGuanBtnObj != null)
					{
						if (player.GetEquipLingZhouData() == null)
						{
							this.BiGuanBtnObj.SetActive(false);
							return;
						}
						this.BiGuanBtnObj.SetActive(true);
						return;
					}
				}
			}
			else
			{
				if (this.WhetherHasJiZhi)
				{
					this.IsLand.gameObject.SetActive(true);
					return;
				}
				this.IsLand.gameObject.SetActive(false);
				return;
			}
		}
		else
		{
			this.IsLand.gameObject.SetActive(false);
		}
	}

	// Token: 0x060013F1 RID: 5105 RVA: 0x000B70D0 File Offset: 0x000B52D0
	public void DengDaoBtn()
	{
		if (!Tools.instance.canClick(false, true))
		{
			return;
		}
		Avatar player = Tools.instance.getPlayer();
		int inSeaID = player.seaNodeMag.GetInSeaID(this.NodeIndex, EndlessSeaMag.MapWide);
		player.randomFuBenMag.GetInRandomFuBen(inSeaID, -1);
	}

	// Token: 0x060013F2 RID: 5106 RVA: 0x000B711C File Offset: 0x000B531C
	public void StaticBtn()
	{
		if (!Tools.instance.canClick(false, true))
		{
			return;
		}
		Transform transform = base.transform.Find("StaticFlowchat");
		if (transform != null)
		{
			transform.GetComponent<Flowchart>().ExecuteBlock("startClick");
		}
	}

	// Token: 0x060013F3 RID: 5107 RVA: 0x000B7164 File Offset: 0x000B5364
	public void BiGuanBtn()
	{
		if (!Tools.instance.canClick(false, true))
		{
			return;
		}
		Transform transform = base.transform.Find("flowchat");
		if (transform != null)
		{
			transform.GetComponent<Flowchart>().ExecuteBlock("startBiGuan");
		}
	}

	// Token: 0x060013F4 RID: 5108 RVA: 0x000B71AC File Offset: 0x000B53AC
	public bool HasIsLand()
	{
		Avatar player = Tools.instance.getPlayer();
		int inSeaID = player.seaNodeMag.GetInSeaID(this.NodeIndex, EndlessSeaMag.MapWide);
		int num = (int)player.EndlessSea["AllIaLand"][inSeaID - 1];
		using (List<FubenGrid.StaticNodeInfo>.Enumerator enumerator = EndlessSeaMag.Inst.seaGrid.StaticNodeList.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.index == inSeaID)
				{
					return false;
				}
			}
		}
		return FuBenMap.getIndex(this.X % 7, this.Y % 7, 7) == num;
	}

	// Token: 0x060013F5 RID: 5109 RVA: 0x000B7270 File Offset: 0x000B5470
	public bool HasBoss()
	{
		Avatar player = PlayerEx.Player;
		foreach (SeaHaiYuJiZhiShuaXin seaHaiYuJiZhiShuaXin in SeaHaiYuJiZhiShuaXin.DataList)
		{
			if (player.EndlessSeaBoss.HasField(seaHaiYuJiZhiShuaXin.id.ToString()))
			{
				JSONObject jsonobject = player.EndlessSeaBoss[seaHaiYuJiZhiShuaXin.id.ToString()];
				if (jsonobject["Pos"].I == this.NodeIndex && !jsonobject["Close"].b)
				{
					int i = jsonobject["JiZhiID"].I;
					SeaJiZhiID seaJiZhiID = SeaJiZhiID.DataDict[i];
					this.jiZhiType = seaJiZhiID.Type;
					if (this.jiZhiType == 0)
					{
						this.jiZhiTalkID = seaJiZhiID.TalkID;
						this.jiZhiChuFaID = jsonobject["AvatarID"].I;
					}
					else if (this.jiZhiType == 1)
					{
						this.jiZhiTalkID = seaJiZhiID.TalkID;
						this.jiZhiChuFaID = seaJiZhiID.FuBenType;
					}
					if (ResRefHolder.Inst.SeaJiZhiRes.Count > seaJiZhiID.XingXiang - 1)
					{
						SkeletonDataAsset skeletonDataAsset = ResRefHolder.Inst.SeaJiZhiRes[seaJiZhiID.XingXiang - 1];
						if (this.IsLand != null)
						{
							SkeletonAnimation componentInChildren = this.IsLand.GetComponentInChildren<SkeletonAnimation>(true);
							if (componentInChildren != null)
							{
								componentInChildren.skeletonDataAsset = skeletonDataAsset;
								SeaJiZhiXingXiang seaJiZhiXingXiang = SeaJiZhiXingXiang.DataDict[seaJiZhiID.XingXiang];
								componentInChildren.initialSkinName = seaJiZhiXingXiang.Skin;
								componentInChildren.Initialize(true);
								componentInChildren.AnimationName = seaJiZhiXingXiang.Anim;
								if (seaJiZhiXingXiang.OffsetX != 0 || seaJiZhiXingXiang.OffsetY != 0)
								{
									float num = componentInChildren.transform.localPosition.x;
									float num2 = componentInChildren.transform.localPosition.y;
									if (seaJiZhiXingXiang.OffsetX != 0)
									{
										num = (float)seaJiZhiXingXiang.OffsetX / 100f;
									}
									if (seaJiZhiXingXiang.OffsetY != 0)
									{
										num2 = (float)seaJiZhiXingXiang.OffsetY / 100f;
									}
									componentInChildren.transform.localPosition = new Vector3(num, num2, componentInChildren.transform.localPosition.z);
								}
								if (seaJiZhiXingXiang.ScaleX != 0 || seaJiZhiXingXiang.ScaleY != 0)
								{
									float num3 = componentInChildren.transform.localScale.x;
									float num4 = componentInChildren.transform.localScale.y;
									if (seaJiZhiXingXiang.ScaleX != 0)
									{
										num3 = (float)seaJiZhiXingXiang.ScaleX / 100f;
									}
									if (seaJiZhiXingXiang.ScaleY != 0)
									{
										num4 = (float)seaJiZhiXingXiang.ScaleY / 100f;
									}
									componentInChildren.transform.localScale = new Vector3(num3, num4, componentInChildren.transform.localScale.z);
								}
							}
							else
							{
								Debug.LogError(string.Format("海格子{0}没有用于设置形象的spine骨骼，请检查", this.NodeIndex));
							}
						}
						else
						{
							Debug.LogError(string.Format("海格子{0}没有岛屿子物体，请检查", this.NodeIndex));
						}
					}
					else
					{
						Debug.LogError(string.Format("海域机制形象{0}超界，请在ResRefHolder配置形象", seaJiZhiID.XingXiang));
					}
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x060013F6 RID: 5110 RVA: 0x000B75D8 File Offset: 0x000B57D8
	public void AutoSetPositon()
	{
		FubenGrid component = base.transform.parent.GetComponent<FubenGrid>();
		int num = component.creatNum / component.num;
		int num2 = component.num;
		int num3 = 1;
		bool flag = false;
		for (int i = 0; i < num; i++)
		{
			for (int j = 0; j < num2; j++)
			{
				if (num3 == this.NodeIndex)
				{
					this.MapPositon = new Vector2((float)j, (float)i);
					flag = true;
					break;
				}
				num3++;
			}
			if (flag)
			{
				break;
			}
		}
	}

	// Token: 0x060013F7 RID: 5111 RVA: 0x000B7658 File Offset: 0x000B5858
	public override void AvatarMoveToThis()
	{
		if (AllMapManage.instance != null)
		{
			int avatarNowMapIndex = this.getAvatarNowMapIndex();
			int nodeIndex = this.NodeIndex;
			SeaAvatarObjBase.Directon directon = this.GetDirecton(avatarNowMapIndex, nodeIndex);
			EndlessSeaMag.Inst.PlayerDirecton = directon;
			AllMapManage.instance.MapPlayerController.SeaShow.SetDir(directon);
			AllMapManage.instance.MapPlayerController.SetSpeed(1);
			iTween.MoveTo(AllMapManage.instance.MapPlayerController.gameObject, iTween.Hash(new object[]
			{
				"x",
				base.transform.position.x,
				"y",
				base.transform.position.y,
				"z",
				base.transform.position.z,
				"time",
				this.waitTime,
				"islocal",
				false,
				"EaseType",
				"linear"
			}));
			WASDMove.waitTime = this.waitTime;
			WASDMove.needWait = true;
			this.setAvatarNowMapIndex();
		}
	}

	// Token: 0x060013F8 RID: 5112 RVA: 0x000B7790 File Offset: 0x000B5990
	public void AutoMoveToThis()
	{
		List<int> list = new List<int>();
		this.GetIndexList(list);
		if (list.Count > 0)
		{
			((MapSeaCompent)AllMapManage.instance.mapIndex[list[0]]).SatrtMove();
		}
	}

	// Token: 0x060013F9 RID: 5113 RVA: 0x000B77D4 File Offset: 0x000B59D4
	public void GetIndexList(List<int> NodeIndexList)
	{
		int avatarNowMapIndex = this.getAvatarNowMapIndex();
		int nodeIndex = this.NodeIndex;
		this.dieDaiAddIndex(NodeIndexList, avatarNowMapIndex);
	}

	// Token: 0x060013FA RID: 5114 RVA: 0x0000A093 File Offset: 0x00008293
	public bool CheckCanGetIn(int index)
	{
		return true;
	}

	// Token: 0x060013FB RID: 5115 RVA: 0x000B77F8 File Offset: 0x000B59F8
	public void dieDaiAddIndex(List<int> NodeIndexList, int index)
	{
		MapSeaCompent mapSeaCompent = (MapSeaCompent)AllMapManage.instance.mapIndex[index];
		Vector2 mapPositon = mapSeaCompent.MapPositon;
		Vector2 mapPositon2 = this.MapPositon;
		List<int> nextIndex = mapSeaCompent.nextIndex;
		int num = 0;
		int num2 = -1;
		float num3 = 1E+13f;
		Avatar player = Tools.instance.getPlayer();
		foreach (int num4 in nextIndex)
		{
			MapSeaCompent mapSeaCompent2 = (MapSeaCompent)AllMapManage.instance.mapIndex[num4];
			float num5 = Vector2.Distance(mapPositon, mapPositon2);
			int indexFengBaoLv = player.seaNodeMag.GetIndexFengBaoLv(this.NodeIndex, EndlessSeaMag.MapWide);
			num5 += (float)indexFengBaoLv;
			if (num5 < num3 && this.CheckCanGetIn(num4))
			{
				num3 = num5;
				num2 = num;
			}
			num++;
		}
		if (num2 != -1 && num2 != this.NodeIndex)
		{
			this.dieDaiAddIndex(NodeIndexList, num2);
		}
	}

	// Token: 0x060013FC RID: 5116 RVA: 0x000B78F0 File Offset: 0x000B5AF0
	public void RestNodeLuXianIndex()
	{
		if (this.MoveFlagUI != null)
		{
			this.MoveFlagUI.transform.Find("Image/Text").GetComponent<Text>().text = string.Concat(EndlessSeaMag.Inst.LuXian.IndexOf(this.NodeIndex));
		}
	}

	// Token: 0x060013FD RID: 5117 RVA: 0x000042DD File Offset: 0x000024DD
	public void NodeOnClick()
	{
	}

	// Token: 0x060013FE RID: 5118 RVA: 0x000129D6 File Offset: 0x00010BD6
	private void OnMouseUpAsButton()
	{
		if (Tools.instance.canClick(false, true))
		{
			this.NodeOnClick();
		}
	}

	// Token: 0x060013FF RID: 5119 RVA: 0x000129EC File Offset: 0x00010BEC
	public override void EventRandom()
	{
		if (WASDMove.Inst != null)
		{
			WASDMove.Inst.IsMoved = true;
		}
		EndlessSeaMag.Inst.AddLuXianDian(this.NodeIndex);
		EndlessSeaMag.Inst.StartMove();
	}

	// Token: 0x06001400 RID: 5120 RVA: 0x000B794C File Offset: 0x000B5B4C
	public int GetAddTimeNum()
	{
		Avatar player = Tools.instance.getPlayer();
		_ItemJsonData equipLingZhouData = player.GetEquipLingZhouData();
		int num = 30;
		int result = num;
		if (equipLingZhouData != null)
		{
			JToken jtoken = jsonData.instance.LingZhouPinJie[equipLingZhouData.quality.ToString()];
			result = num - (int)jtoken["speed"];
		}
		else if (player.YuJianFeiXing())
		{
			JSONObject jsonobject = jsonData.instance.SeaCastTimeJsonData.list.Find((JSONObject aa) => (int)aa["dunSu"].n >= this.ComAvatar.dunSu);
			if (jsonobject != null)
			{
				result = (int)jsonobject["XiaoHao"].n;
			}
		}
		else
		{
			result = 60;
		}
		return result;
	}

	// Token: 0x06001401 RID: 5121 RVA: 0x00012A20 File Offset: 0x00010C20
	public override void BaseAddTime()
	{
		this.ComAvatar.AddTime(this.GetAddTimeNum(), 0, 0);
	}

	// Token: 0x06001402 RID: 5122 RVA: 0x000B79EC File Offset: 0x000B5BEC
	public bool SatrtMove()
	{
		if (!base.CanClick())
		{
			return false;
		}
		this.fuBenSetClick();
		this.movaAvatar();
		SeaToOherScene seaToOther = base.GetComponent<SeaToOherScene>();
		if (seaToOther != null)
		{
			JSONObject jsonobject = jsonData.instance.SceneNameJsonData[seaToOther.ToSceneName];
			USelectBox.Show("是否进入" + jsonobject["EventName"].Str + "？", delegate
			{
				LoadFuBen.loadfuben(seaToOther.ToSceneName, this.NodeIndex);
			}, delegate
			{
				if (AllMapManage.instance != null && AllMapManage.instance.mapIndex.ContainsKey(Tools.instance.fubenLastIndex))
				{
					AllMapManage.instance.mapIndex[Tools.instance.fubenLastIndex].AvatarMoveToThis();
					EndlessSeaMag.Inst.RemoveAllLuXian();
					this.StartCoroutine(this.StopMove());
				}
			});
			EndlessSeaMag.Inst.StopAllContens();
			return true;
		}
		this.CastSeaCastHP();
		foreach (SeaAvatarObjBase seaAvatarObjBase in EndlessSeaMag.Inst.MonstarList)
		{
			if (seaAvatarObjBase != null)
			{
				seaAvatarObjBase.Think();
			}
		}
		EndlessSeaMag.Inst.autoResetFengBao();
		EndlessSeaMag.Inst.SetCanSeeMonstar();
		return true;
	}

	// Token: 0x06001403 RID: 5123 RVA: 0x00012A35 File Offset: 0x00010C35
	private IEnumerator StopMove()
	{
		yield return new WaitForSeconds(this.waitTime);
		MapPlayerController.Inst.SetSpeed(0);
		yield break;
	}

	// Token: 0x06001404 RID: 5124 RVA: 0x000042DD File Offset: 0x000024DD
	public void MonsterMoveIn(Avatar target)
	{
	}

	// Token: 0x06001405 RID: 5125 RVA: 0x000B7B08 File Offset: 0x000B5D08
	public SeaAvatarObjBase.Directon GetDirecton(int Lastindex, int nowIndex)
	{
		int indexX = FuBenMap.getIndexX(Lastindex, EndlessSeaMag.MapWide);
		int indexY = FuBenMap.getIndexY(Lastindex, EndlessSeaMag.MapWide);
		int indexX2 = FuBenMap.getIndexX(nowIndex, EndlessSeaMag.MapWide);
		int indexY2 = FuBenMap.getIndexY(nowIndex, EndlessSeaMag.MapWide);
		if (indexY > indexY2)
		{
			return SeaAvatarObjBase.Directon.UP;
		}
		if (indexY < indexY2)
		{
			return SeaAvatarObjBase.Directon.Down;
		}
		if (indexX < indexX2)
		{
			return SeaAvatarObjBase.Directon.Right;
		}
		return SeaAvatarObjBase.Directon.Left;
	}

	// Token: 0x06001406 RID: 5126 RVA: 0x000B7B58 File Offset: 0x000B5D58
	public void MonstarMoveToThis(SeaAvatarObjBase target)
	{
		if (AllMapManage.instance != null)
		{
			int nowMapIndex = target.NowMapIndex;
			int nodeIndex = this.NodeIndex;
			SeaAvatarObjBase.Directon directon = this.GetDirecton(nowMapIndex, nodeIndex);
			Animator component = target.GetComponent<Animator>();
			component.SetInteger("direction", (int)directon);
			component.SetInteger("speed", 1);
			if (directon == SeaAvatarObjBase.Directon.Right)
			{
				target.transform.localScale = new Vector3(-Mathf.Abs(target.transform.localScale.x), target.transform.localScale.y, target.transform.localScale.z);
			}
			else
			{
				target.transform.localScale = new Vector3(Mathf.Abs(target.transform.localScale.x), target.transform.localScale.y, target.transform.localScale.z);
			}
			iTween.MoveTo(target.gameObject, iTween.Hash(new object[]
			{
				"x",
				base.transform.position.x,
				"y",
				base.transform.position.y,
				"z",
				base.transform.position.z,
				"time",
				this.waitTime,
				"islocal",
				false,
				"EaseType",
				"linear"
			}));
			this.setMonstarNowMapIndex(target);
		}
	}

	// Token: 0x06001407 RID: 5127 RVA: 0x00012A44 File Offset: 0x00010C44
	public void setMonstarNowMapIndex(SeaAvatarObjBase target)
	{
		target.NowMapIndex = this.NodeIndex;
		Tools.instance.getPlayer().seaNodeMag.SetSeaMonstarIndex(target.UUID, target.SeaId, this.NodeIndex);
	}

	// Token: 0x06001408 RID: 5128 RVA: 0x000B7CF4 File Offset: 0x000B5EF4
	public void CastSeaCastHP()
	{
		Avatar player = Tools.instance.getPlayer();
		int indexFengBaoLv = player.seaNodeMag.GetIndexFengBaoLv(this.NodeIndex, EndlessSeaMag.MapWide);
		if (indexFengBaoLv > 0)
		{
			BaseItem lingZhou = player.GetLingZhou();
			JToken jtoken = jsonData.instance.EndlessSeaLinQiSafeLvData[indexFengBaoLv.ToString()];
			if (lingZhou != null)
			{
				int num = Mathf.Clamp((int)jtoken["chuandemage"] - (int)player.GetNowLingZhouShuXinJson()["Defense"], 0, 99999);
				this.ComAvatar.ReduceLingZhouNaiJiu(lingZhou, num);
				return;
			}
			int num2 = (int)jtoken["damage"];
			base.StartCoroutine(this.ReduceHP(-num2));
		}
	}

	// Token: 0x06001409 RID: 5129 RVA: 0x00012A78 File Offset: 0x00010C78
	public IEnumerator ReduceHP(int realHp)
	{
		yield return new WaitForSeconds(this.waitTime);
		this.ComAvatar.AllMapAddHP(realHp, DeathType.身死道消);
		yield break;
	}

	// Token: 0x04000F83 RID: 3971
	public float waitTime = 1f;

	// Token: 0x04000F84 RID: 3972
	public GameObject MoveFlagUI;

	// Token: 0x04000F85 RID: 3973
	public int G;

	// Token: 0x04000F86 RID: 3974
	public int H;

	// Token: 0x04000F87 RID: 3975
	public int F;

	// Token: 0x04000F88 RID: 3976
	public MapSeaCompent SeaParent;

	// Token: 0x04000F89 RID: 3977
	public Text Ftext;

	// Token: 0x04000F8A RID: 3978
	public Text Htext;

	// Token: 0x04000F8B RID: 3979
	public Text Gtext;

	// Token: 0x04000F8C RID: 3980
	public GameObject IsLand;

	// Token: 0x04000F8D RID: 3981
	public GameObject InIsLandBtn;

	// Token: 0x04000F8E RID: 3982
	public GameObject BiGuanBtnObj;

	// Token: 0x04000F8F RID: 3983
	[Header("是否显示按钮")]
	public bool HideButton;

	// Token: 0x04000F90 RID: 3984
	private bool WhetherHasIsLand;

	// Token: 0x04000F91 RID: 3985
	[HideInInspector]
	public bool WhetherHasJiZhi;

	// Token: 0x04000F92 RID: 3986
	private float LaseSetLangHuangTime;

	// Token: 0x04000F93 RID: 3987
	private float ResetTime = 5f;

	// Token: 0x04000F94 RID: 3988
	[HideInInspector]
	public int jiZhiType;

	// Token: 0x04000F95 RID: 3989
	[HideInInspector]
	public int jiZhiTalkID;

	// Token: 0x04000F96 RID: 3990
	[HideInInspector]
	public int jiZhiChuFaID;
}
