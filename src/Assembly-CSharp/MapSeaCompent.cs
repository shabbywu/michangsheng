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

// Token: 0x02000198 RID: 408
public class MapSeaCompent : MapInstComport
{
	// Token: 0x17000215 RID: 533
	// (get) Token: 0x0600115A RID: 4442 RVA: 0x000688F0 File Offset: 0x00066AF0
	public int X
	{
		get
		{
			return FuBenMap.getIndexX(this.NodeIndex, EndlessSeaMag.MapWide);
		}
	}

	// Token: 0x17000216 RID: 534
	// (get) Token: 0x0600115B RID: 4443 RVA: 0x00068902 File Offset: 0x00066B02
	public int Y
	{
		get
		{
			return FuBenMap.getIndexY(this.NodeIndex, EndlessSeaMag.MapWide);
		}
	}

	// Token: 0x0600115C RID: 4444 RVA: 0x00068914 File Offset: 0x00066B14
	protected override void Awake()
	{
		this.NodeIndex = int.Parse(base.name);
		this.AllMapCastTimeJsonData = jsonData.instance.AllMapCastTimeJsonData;
		this.MapRandomJsonData = jsonData.instance.MapRandomJsonData;
		this.AutoSetPositon();
	}

	// Token: 0x0600115D RID: 4445 RVA: 0x0006894D File Offset: 0x00066B4D
	protected override void Start()
	{
		this.ResetTime = (float)(jsonData.GetRandom() % 20) + 0.2f * (float)(jsonData.GetRandom() % 6);
		this.StartSeting();
		this.WhetherHasIsLand = this.HasIsLand();
		this.WhetherHasJiZhi = this.HasBoss();
	}

	// Token: 0x0600115E RID: 4446 RVA: 0x0006898B File Offset: 0x00066B8B
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

	// Token: 0x0600115F RID: 4447 RVA: 0x000689B4 File Offset: 0x00066BB4
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

	// Token: 0x06001160 RID: 4448 RVA: 0x00068AEA File Offset: 0x00066CEA
	public void SetParent(MapSeaCompent parent, int g)
	{
		this.SeaParent = parent;
		this.G = g;
		this.F = this.G + this.H;
	}

	// Token: 0x06001161 RID: 4449 RVA: 0x00068B0D File Offset: 0x00066D0D
	public bool NodeHasIsLand()
	{
		return this.WhetherHasIsLand || this.IsStatic;
	}

	// Token: 0x06001162 RID: 4450 RVA: 0x00068B20 File Offset: 0x00066D20
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
				if (!this.IsInitYun)
				{
					if (this.YunObject != null)
					{
						int id = this.GerLandId();
						if (this.IsNeedYun(id))
						{
							this.YunObject.SetActive(true);
						}
						else
						{
							this.YunObject.SetActive(false);
						}
					}
					this.IsInitYun = true;
				}
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

	// Token: 0x06001163 RID: 4451 RVA: 0x00068CD0 File Offset: 0x00066ED0
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

	// Token: 0x06001164 RID: 4452 RVA: 0x00068D19 File Offset: 0x00066F19
	public int GerLandId()
	{
		return PlayerEx.Player.seaNodeMag.GetInSeaID(this.NodeIndex, EndlessSeaMag.MapWide);
	}

	// Token: 0x06001165 RID: 4453 RVA: 0x00068D38 File Offset: 0x00066F38
	private bool IsNeedYun(int id)
	{
		return !PlayerEx.Player.RandomFuBenList.ContainsKey(id.ToString()) || (bool)PlayerEx.Player.RandomFuBenList[id.ToString()]["ShouldReset"] || PlayerEx.Player.RandomFuBenList[id.ToString()]["type"] == null;
	}

	// Token: 0x06001166 RID: 4454 RVA: 0x00068DB0 File Offset: 0x00066FB0
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

	// Token: 0x06001167 RID: 4455 RVA: 0x00068DF8 File Offset: 0x00066FF8
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

	// Token: 0x06001168 RID: 4456 RVA: 0x00068E40 File Offset: 0x00067040
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

	// Token: 0x06001169 RID: 4457 RVA: 0x00068F04 File Offset: 0x00067104
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

	// Token: 0x0600116A RID: 4458 RVA: 0x0006926C File Offset: 0x0006746C
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

	// Token: 0x0600116B RID: 4459 RVA: 0x000692EC File Offset: 0x000674EC
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

	// Token: 0x0600116C RID: 4460 RVA: 0x00069424 File Offset: 0x00067624
	public void AutoMoveToThis()
	{
		List<int> list = new List<int>();
		this.GetIndexList(list);
		if (list.Count > 0)
		{
			((MapSeaCompent)AllMapManage.instance.mapIndex[list[0]]).SatrtMove();
		}
	}

	// Token: 0x0600116D RID: 4461 RVA: 0x00069468 File Offset: 0x00067668
	public void GetIndexList(List<int> NodeIndexList)
	{
		int avatarNowMapIndex = this.getAvatarNowMapIndex();
		int nodeIndex = this.NodeIndex;
		this.dieDaiAddIndex(NodeIndexList, avatarNowMapIndex);
	}

	// Token: 0x0600116E RID: 4462 RVA: 0x00024C5F File Offset: 0x00022E5F
	public bool CheckCanGetIn(int index)
	{
		return true;
	}

	// Token: 0x0600116F RID: 4463 RVA: 0x0006948C File Offset: 0x0006768C
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

	// Token: 0x06001170 RID: 4464 RVA: 0x00069584 File Offset: 0x00067784
	public void RestNodeLuXianIndex()
	{
		if (this.MoveFlagUI != null)
		{
			this.MoveFlagUI.transform.Find("Image/Text").GetComponent<Text>().text = string.Concat(EndlessSeaMag.Inst.LuXian.IndexOf(this.NodeIndex));
		}
	}

	// Token: 0x06001171 RID: 4465 RVA: 0x00004095 File Offset: 0x00002295
	public void NodeOnClick()
	{
	}

	// Token: 0x06001172 RID: 4466 RVA: 0x000695DD File Offset: 0x000677DD
	private void OnMouseUpAsButton()
	{
		if (Tools.instance.canClick(false, true))
		{
			this.NodeOnClick();
		}
	}

	// Token: 0x06001173 RID: 4467 RVA: 0x000695F3 File Offset: 0x000677F3
	public override void EventRandom()
	{
		if (WASDMove.Inst != null)
		{
			WASDMove.Inst.IsMoved = true;
		}
		EndlessSeaMag.Inst.AddLuXianDian(this.NodeIndex);
		EndlessSeaMag.Inst.StartMove();
	}

	// Token: 0x06001174 RID: 4468 RVA: 0x00069628 File Offset: 0x00067828
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

	// Token: 0x06001175 RID: 4469 RVA: 0x000696C8 File Offset: 0x000678C8
	public override void BaseAddTime()
	{
		this.ComAvatar.AddTime(this.GetAddTimeNum(), 0, 0);
	}

	// Token: 0x06001176 RID: 4470 RVA: 0x000696E0 File Offset: 0x000678E0
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

	// Token: 0x06001177 RID: 4471 RVA: 0x000697FC File Offset: 0x000679FC
	private IEnumerator StopMove()
	{
		yield return new WaitForSeconds(this.waitTime);
		MapPlayerController.Inst.SetSpeed(0);
		yield break;
	}

	// Token: 0x06001178 RID: 4472 RVA: 0x00004095 File Offset: 0x00002295
	public void MonsterMoveIn(Avatar target)
	{
	}

	// Token: 0x06001179 RID: 4473 RVA: 0x0006980C File Offset: 0x00067A0C
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

	// Token: 0x0600117A RID: 4474 RVA: 0x0006985C File Offset: 0x00067A5C
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

	// Token: 0x0600117B RID: 4475 RVA: 0x000699F6 File Offset: 0x00067BF6
	public void setMonstarNowMapIndex(SeaAvatarObjBase target)
	{
		target.NowMapIndex = this.NodeIndex;
		Tools.instance.getPlayer().seaNodeMag.SetSeaMonstarIndex(target.UUID, target.SeaId, this.NodeIndex);
	}

	// Token: 0x0600117C RID: 4476 RVA: 0x00069A2C File Offset: 0x00067C2C
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

	// Token: 0x0600117D RID: 4477 RVA: 0x00069AE7 File Offset: 0x00067CE7
	public IEnumerator ReduceHP(int realHp)
	{
		yield return new WaitForSeconds(this.waitTime);
		this.ComAvatar.AllMapAddHP(realHp, DeathType.身死道消);
		yield break;
	}

	// Token: 0x04000C7C RID: 3196
	public float waitTime = 1f;

	// Token: 0x04000C7D RID: 3197
	public GameObject MoveFlagUI;

	// Token: 0x04000C7E RID: 3198
	public int G;

	// Token: 0x04000C7F RID: 3199
	public int H;

	// Token: 0x04000C80 RID: 3200
	public int F;

	// Token: 0x04000C81 RID: 3201
	public bool IsInitYun;

	// Token: 0x04000C82 RID: 3202
	public GameObject YunObject;

	// Token: 0x04000C83 RID: 3203
	public MapSeaCompent SeaParent;

	// Token: 0x04000C84 RID: 3204
	public Text Ftext;

	// Token: 0x04000C85 RID: 3205
	public Text Htext;

	// Token: 0x04000C86 RID: 3206
	public Text Gtext;

	// Token: 0x04000C87 RID: 3207
	public GameObject IsLand;

	// Token: 0x04000C88 RID: 3208
	public GameObject InIsLandBtn;

	// Token: 0x04000C89 RID: 3209
	public GameObject BiGuanBtnObj;

	// Token: 0x04000C8A RID: 3210
	[Header("是否显示按钮")]
	public bool HideButton;

	// Token: 0x04000C8B RID: 3211
	private bool WhetherHasIsLand;

	// Token: 0x04000C8C RID: 3212
	[HideInInspector]
	public bool WhetherHasJiZhi;

	// Token: 0x04000C8D RID: 3213
	private float LaseSetLangHuangTime;

	// Token: 0x04000C8E RID: 3214
	private float ResetTime = 5f;

	// Token: 0x04000C8F RID: 3215
	[HideInInspector]
	public int jiZhiType;

	// Token: 0x04000C90 RID: 3216
	[HideInInspector]
	public int jiZhiTalkID;

	// Token: 0x04000C91 RID: 3217
	[HideInInspector]
	public int jiZhiChuFaID;
}
