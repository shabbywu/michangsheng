using System.Collections;
using System.Collections.Generic;
using Bag;
using Fungus;
using JSONClass;
using KBEngine;
using Newtonsoft.Json.Linq;
using Spine.Unity;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MapSeaCompent : MapInstComport
{
	public float waitTime = 1f;

	public GameObject MoveFlagUI;

	public int G;

	public int H;

	public int F;

	public bool IsInitYun;

	public GameObject YunObject;

	public MapSeaCompent SeaParent;

	public Text Ftext;

	public Text Htext;

	public Text Gtext;

	public GameObject IsLand;

	public GameObject InIsLandBtn;

	public GameObject BiGuanBtnObj;

	[Header("是否显示按钮")]
	public bool HideButton;

	private bool WhetherHasIsLand;

	[HideInInspector]
	public bool WhetherHasJiZhi;

	private float LaseSetLangHuangTime;

	private float ResetTime = 5f;

	[HideInInspector]
	public int jiZhiType;

	[HideInInspector]
	public int jiZhiTalkID;

	[HideInInspector]
	public int jiZhiChuFaID;

	public int X => FuBenMap.getIndexX(NodeIndex, EndlessSeaMag.MapWide);

	public int Y => FuBenMap.getIndexY(NodeIndex, EndlessSeaMag.MapWide);

	protected override void Awake()
	{
		NodeIndex = int.Parse(((Object)this).name);
		AllMapCastTimeJsonData = jsonData.instance.AllMapCastTimeJsonData;
		MapRandomJsonData = jsonData.instance.MapRandomJsonData;
		AutoSetPositon();
	}

	protected override void Start()
	{
		ResetTime = (float)(jsonData.GetRandom() % 20) + 0.2f * (float)(jsonData.GetRandom() % 6);
		StartSeting();
		WhetherHasIsLand = HasIsLand();
		WhetherHasJiZhi = HasBoss();
	}

	public override void Update()
	{
		if ((Object)(object)WASDMove.Inst != (Object)null)
		{
			if (WASDMove.Inst.IsMoved)
			{
				Refresh();
			}
		}
		else
		{
			Refresh();
		}
	}

	public void AddLangHua()
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		if (Vector3.Distance(((Component)AllMapManage.instance.MapPlayerController).transform.position, ((Component)this).transform.position) <= 18f && LaseSetLangHuangTime > ResetTime)
		{
			GameObject obj = GameObjectPool.Get(EndlessSeaMag.Inst.LangHua);
			float num = 0.34f * (float)(jsonData.GetRandom() % 11);
			float num2 = 0.34f * (float)(jsonData.GetRandom() % 11);
			obj.transform.position = new Vector3(((Component)this).transform.position.x + num, ((Component)this).transform.position.y + num2, ((Component)this).transform.position.z);
			LaseSetLangHuangTime = 0f;
			int num3 = jsonData.GetRandom() % 12 + 1;
			obj.GetComponentInChildren<SkeletonAnimation>().AnimationName = ((num3 < 10) ? "L0" : "L") + num3 + "_1";
			ResetTime = (float)(jsonData.GetRandom() % 20) + 0.2f * (float)(jsonData.GetRandom() % 6) + 5f;
		}
		LaseSetLangHuangTime += 0.2f;
	}

	public void SetParent(MapSeaCompent parent, int g)
	{
		SeaParent = parent;
		G = g;
		F = G + H;
	}

	public bool NodeHasIsLand()
	{
		if (!WhetherHasIsLand)
		{
			return IsStatic;
		}
		return true;
	}

	public void Refresh()
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Expected O, but got Unknown
		Avatar player = Tools.instance.getPlayer();
		if (Tools.ContensInt((JArray)player.EndlessSeaAvatarSeeIsland["Island"], NodeIndex))
		{
			int avatarNowMapIndex = getAvatarNowMapIndex();
			if (NodeHasIsLand())
			{
				int num = int.Parse(SceneEx.NowSceneName.Replace("Sea", ""));
				if (player.HideHaiYuTanSuo.HasItem(num) && SeaHaiYuTanSuo.DataDict[num].ZuoBiao == NodeIndex)
				{
					IsLand.gameObject.SetActive(false);
					InIsLandBtn.gameObject.SetActive(false);
					return;
				}
				IsLand.gameObject.SetActive(true);
				if (!IsInitYun)
				{
					if ((Object)(object)YunObject != (Object)null)
					{
						int id = GerLandId();
						if (IsNeedYun(id))
						{
							YunObject.SetActive(true);
						}
						else
						{
							YunObject.SetActive(false);
						}
					}
					IsInitYun = true;
				}
				if (HideButton)
				{
					return;
				}
				if (avatarNowMapIndex == NodeIndex)
				{
					InIsLandBtn.gameObject.SetActive(true);
					if ((Object)(object)BiGuanBtnObj != (Object)null)
					{
						if (player.GetEquipLingZhouData() == null)
						{
							BiGuanBtnObj.SetActive(false);
						}
						else
						{
							BiGuanBtnObj.SetActive(true);
						}
					}
				}
				else
				{
					InIsLandBtn.gameObject.SetActive(false);
				}
			}
			else if (WhetherHasJiZhi)
			{
				IsLand.gameObject.SetActive(true);
			}
			else
			{
				IsLand.gameObject.SetActive(false);
			}
		}
		else
		{
			IsLand.gameObject.SetActive(false);
		}
	}

	public void DengDaoBtn()
	{
		if (Tools.instance.canClick())
		{
			Avatar player = Tools.instance.getPlayer();
			int inSeaID = player.seaNodeMag.GetInSeaID(NodeIndex, EndlessSeaMag.MapWide);
			player.randomFuBenMag.GetInRandomFuBen(inSeaID);
		}
	}

	public int GerLandId()
	{
		return PlayerEx.Player.seaNodeMag.GetInSeaID(NodeIndex, EndlessSeaMag.MapWide);
	}

	private bool IsNeedYun(int id)
	{
		if (!PlayerEx.Player.RandomFuBenList.ContainsKey(id.ToString()))
		{
			return true;
		}
		if (!(bool)PlayerEx.Player.RandomFuBenList[id.ToString()][(object)"ShouldReset"])
		{
			if (PlayerEx.Player.RandomFuBenList[id.ToString()][(object)"type"] == null)
			{
				return true;
			}
			return false;
		}
		return true;
	}

	public void StaticBtn()
	{
		if (Tools.instance.canClick())
		{
			Transform val = ((Component)this).transform.Find("StaticFlowchat");
			if ((Object)(object)val != (Object)null)
			{
				((Component)val).GetComponent<Flowchart>().ExecuteBlock("startClick");
			}
		}
	}

	public void BiGuanBtn()
	{
		if (Tools.instance.canClick())
		{
			Transform val = ((Component)this).transform.Find("flowchat");
			if ((Object)(object)val != (Object)null)
			{
				((Component)val).GetComponent<Flowchart>().ExecuteBlock("startBiGuan");
			}
		}
	}

	public bool HasIsLand()
	{
		Avatar player = Tools.instance.getPlayer();
		int inSeaID = player.seaNodeMag.GetInSeaID(NodeIndex, EndlessSeaMag.MapWide);
		int num = (int)player.EndlessSea["AllIaLand"][(object)(inSeaID - 1)];
		foreach (FubenGrid.StaticNodeInfo staticNode in EndlessSeaMag.Inst.seaGrid.StaticNodeList)
		{
			if (staticNode.index == inSeaID)
			{
				return false;
			}
		}
		if (FuBenMap.getIndex(X % 7, Y % 7, 7) == num)
		{
			return true;
		}
		return false;
	}

	public bool HasBoss()
	{
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		Avatar player = PlayerEx.Player;
		foreach (SeaHaiYuJiZhiShuaXin data in SeaHaiYuJiZhiShuaXin.DataList)
		{
			if (!player.EndlessSeaBoss.HasField(data.id.ToString()))
			{
				continue;
			}
			JSONObject jSONObject = player.EndlessSeaBoss[data.id.ToString()];
			if (jSONObject["Pos"].I != NodeIndex || jSONObject["Close"].b)
			{
				continue;
			}
			int i = jSONObject["JiZhiID"].I;
			SeaJiZhiID seaJiZhiID = SeaJiZhiID.DataDict[i];
			jiZhiType = seaJiZhiID.Type;
			if (jiZhiType == 0)
			{
				jiZhiTalkID = seaJiZhiID.TalkID;
				jiZhiChuFaID = jSONObject["AvatarID"].I;
			}
			else if (jiZhiType == 1)
			{
				jiZhiTalkID = seaJiZhiID.TalkID;
				jiZhiChuFaID = seaJiZhiID.FuBenType;
			}
			if (ResRefHolder.Inst.SeaJiZhiRes.Count > seaJiZhiID.XingXiang - 1)
			{
				SkeletonDataAsset skeletonDataAsset = ResRefHolder.Inst.SeaJiZhiRes[seaJiZhiID.XingXiang - 1];
				if ((Object)(object)IsLand != (Object)null)
				{
					SkeletonAnimation componentInChildren = IsLand.GetComponentInChildren<SkeletonAnimation>(true);
					if ((Object)(object)componentInChildren != (Object)null)
					{
						((SkeletonRenderer)componentInChildren).skeletonDataAsset = skeletonDataAsset;
						SeaJiZhiXingXiang seaJiZhiXingXiang = SeaJiZhiXingXiang.DataDict[seaJiZhiID.XingXiang];
						((SkeletonRenderer)componentInChildren).initialSkinName = seaJiZhiXingXiang.Skin;
						((SkeletonRenderer)componentInChildren).Initialize(true);
						componentInChildren.AnimationName = seaJiZhiXingXiang.Anim;
						if (seaJiZhiXingXiang.OffsetX != 0 || seaJiZhiXingXiang.OffsetY != 0)
						{
							float num = ((Component)componentInChildren).transform.localPosition.x;
							float num2 = ((Component)componentInChildren).transform.localPosition.y;
							if (seaJiZhiXingXiang.OffsetX != 0)
							{
								num = (float)seaJiZhiXingXiang.OffsetX / 100f;
							}
							if (seaJiZhiXingXiang.OffsetY != 0)
							{
								num2 = (float)seaJiZhiXingXiang.OffsetY / 100f;
							}
							((Component)componentInChildren).transform.localPosition = new Vector3(num, num2, ((Component)componentInChildren).transform.localPosition.z);
						}
						if (seaJiZhiXingXiang.ScaleX != 0 || seaJiZhiXingXiang.ScaleY != 0)
						{
							float num3 = ((Component)componentInChildren).transform.localScale.x;
							float num4 = ((Component)componentInChildren).transform.localScale.y;
							if (seaJiZhiXingXiang.ScaleX != 0)
							{
								num3 = (float)seaJiZhiXingXiang.ScaleX / 100f;
							}
							if (seaJiZhiXingXiang.ScaleY != 0)
							{
								num4 = (float)seaJiZhiXingXiang.ScaleY / 100f;
							}
							((Component)componentInChildren).transform.localScale = new Vector3(num3, num4, ((Component)componentInChildren).transform.localScale.z);
						}
					}
					else
					{
						Debug.LogError((object)$"海格子{NodeIndex}没有用于设置形象的spine骨骼，请检查");
					}
				}
				else
				{
					Debug.LogError((object)$"海格子{NodeIndex}没有岛屿子物体，请检查");
				}
			}
			else
			{
				Debug.LogError((object)$"海域机制形象{seaJiZhiID.XingXiang}超界，请在ResRefHolder配置形象");
			}
			return true;
		}
		return false;
	}

	public void AutoSetPositon()
	{
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		FubenGrid component = ((Component)((Component)this).transform.parent).GetComponent<FubenGrid>();
		int num = component.creatNum / component.num;
		int num2 = component.num;
		int num3 = 1;
		bool flag = false;
		for (int i = 0; i < num; i++)
		{
			for (int j = 0; j < num2; j++)
			{
				if (num3 == NodeIndex)
				{
					MapPositon = new Vector2((float)j, (float)i);
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

	public override void AvatarMoveToThis()
	{
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)AllMapManage.instance != (Object)null)
		{
			int avatarNowMapIndex = getAvatarNowMapIndex();
			int nodeIndex = NodeIndex;
			SeaAvatarObjBase.Directon directon = GetDirecton(avatarNowMapIndex, nodeIndex);
			EndlessSeaMag.Inst.PlayerDirecton = directon;
			AllMapManage.instance.MapPlayerController.SeaShow.SetDir(directon);
			AllMapManage.instance.MapPlayerController.SetSpeed(1);
			iTween.MoveTo(((Component)AllMapManage.instance.MapPlayerController).gameObject, iTween.Hash(new object[12]
			{
				"x",
				((Component)this).transform.position.x,
				"y",
				((Component)this).transform.position.y,
				"z",
				((Component)this).transform.position.z,
				"time",
				waitTime,
				"islocal",
				false,
				"EaseType",
				"linear"
			}));
			WASDMove.waitTime = waitTime;
			WASDMove.needWait = true;
			setAvatarNowMapIndex();
		}
	}

	public void AutoMoveToThis()
	{
		List<int> list = new List<int>();
		GetIndexList(list);
		if (list.Count > 0)
		{
			((MapSeaCompent)AllMapManage.instance.mapIndex[list[0]]).SatrtMove();
		}
	}

	public void GetIndexList(List<int> NodeIndexList)
	{
		int avatarNowMapIndex = getAvatarNowMapIndex();
		_ = NodeIndex;
		dieDaiAddIndex(NodeIndexList, avatarNowMapIndex);
	}

	public bool CheckCanGetIn(int index)
	{
		return true;
	}

	public void dieDaiAddIndex(List<int> NodeIndexList, int index)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		MapSeaCompent obj = (MapSeaCompent)AllMapManage.instance.mapIndex[index];
		Vector2 mapPositon = obj.MapPositon;
		Vector2 mapPositon2 = MapPositon;
		List<int> list = obj.nextIndex;
		int num = 0;
		int num2 = -1;
		float num3 = 1E+13f;
		Avatar player = Tools.instance.getPlayer();
		foreach (int item in list)
		{
			_ = (MapSeaCompent)AllMapManage.instance.mapIndex[item];
			float num4 = Vector2.Distance(mapPositon, mapPositon2);
			int indexFengBaoLv = player.seaNodeMag.GetIndexFengBaoLv(NodeIndex, EndlessSeaMag.MapWide);
			num4 += (float)indexFengBaoLv;
			if (num4 < num3 && CheckCanGetIn(item))
			{
				num3 = num4;
				num2 = num;
			}
			num++;
		}
		if (num2 != -1 && num2 != NodeIndex)
		{
			dieDaiAddIndex(NodeIndexList, num2);
		}
	}

	public void RestNodeLuXianIndex()
	{
		if ((Object)(object)MoveFlagUI != (Object)null)
		{
			((Component)MoveFlagUI.transform.Find("Image/Text")).GetComponent<Text>().text = string.Concat(EndlessSeaMag.Inst.LuXian.IndexOf(NodeIndex));
		}
	}

	public void NodeOnClick()
	{
	}

	private void OnMouseUpAsButton()
	{
		if (Tools.instance.canClick())
		{
			NodeOnClick();
		}
	}

	public override void EventRandom()
	{
		if ((Object)(object)WASDMove.Inst != (Object)null)
		{
			WASDMove.Inst.IsMoved = true;
		}
		EndlessSeaMag.Inst.AddLuXianDian(NodeIndex);
		EndlessSeaMag.Inst.StartMove();
	}

	public int GetAddTimeNum()
	{
		Avatar player = Tools.instance.getPlayer();
		_ItemJsonData equipLingZhouData = player.GetEquipLingZhouData();
		int num = 30;
		int result = num;
		if (equipLingZhouData != null)
		{
			JToken val = jsonData.instance.LingZhouPinJie[equipLingZhouData.quality.ToString()];
			result = num - (int)val[(object)"speed"];
		}
		else if (player.YuJianFeiXing())
		{
			JSONObject jSONObject = jsonData.instance.SeaCastTimeJsonData.list.Find((JSONObject aa) => (int)aa["dunSu"].n >= ComAvatar.dunSu);
			if (jSONObject != null)
			{
				result = (int)jSONObject["XiaoHao"].n;
			}
		}
		else
		{
			result = 60;
		}
		return result;
	}

	public override void BaseAddTime()
	{
		ComAvatar.AddTime(GetAddTimeNum());
	}

	public bool SatrtMove()
	{
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Expected O, but got Unknown
		//IL_0094: Expected O, but got Unknown
		if (!CanClick())
		{
			return false;
		}
		fuBenSetClick();
		movaAvatar();
		SeaToOherScene seaToOther = ((Component)this).GetComponent<SeaToOherScene>();
		if ((Object)(object)seaToOther != (Object)null)
		{
			JSONObject jSONObject = jsonData.instance.SceneNameJsonData[seaToOther.ToSceneName];
			USelectBox.Show("是否进入" + jSONObject["EventName"].Str + "？", (UnityAction)delegate
			{
				LoadFuBen.loadfuben(seaToOther.ToSceneName, NodeIndex);
			}, (UnityAction)delegate
			{
				if ((Object)(object)AllMapManage.instance != (Object)null && AllMapManage.instance.mapIndex.ContainsKey(Tools.instance.fubenLastIndex))
				{
					AllMapManage.instance.mapIndex[Tools.instance.fubenLastIndex].AvatarMoveToThis();
					EndlessSeaMag.Inst.RemoveAllLuXian();
					((MonoBehaviour)this).StartCoroutine(StopMove());
				}
			});
			EndlessSeaMag.Inst.StopAllContens();
			return true;
		}
		CastSeaCastHP();
		foreach (SeaAvatarObjBase monstar in EndlessSeaMag.Inst.MonstarList)
		{
			if ((Object)(object)monstar != (Object)null)
			{
				monstar.Think();
			}
		}
		EndlessSeaMag.Inst.autoResetFengBao();
		EndlessSeaMag.Inst.SetCanSeeMonstar();
		return true;
	}

	private IEnumerator StopMove()
	{
		yield return (object)new WaitForSeconds(waitTime);
		MapPlayerController.Inst.SetSpeed(0);
	}

	public void MonsterMoveIn(Avatar target)
	{
	}

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

	public void MonstarMoveToThis(SeaAvatarObjBase target)
	{
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)AllMapManage.instance != (Object)null)
		{
			int nowMapIndex = target.NowMapIndex;
			int nodeIndex = NodeIndex;
			SeaAvatarObjBase.Directon directon = GetDirecton(nowMapIndex, nodeIndex);
			Animator component = ((Component)target).GetComponent<Animator>();
			component.SetInteger("direction", (int)directon);
			component.SetInteger("speed", 1);
			if (directon == SeaAvatarObjBase.Directon.Right)
			{
				((Component)target).transform.localScale = new Vector3(0f - Mathf.Abs(((Component)target).transform.localScale.x), ((Component)target).transform.localScale.y, ((Component)target).transform.localScale.z);
			}
			else
			{
				((Component)target).transform.localScale = new Vector3(Mathf.Abs(((Component)target).transform.localScale.x), ((Component)target).transform.localScale.y, ((Component)target).transform.localScale.z);
			}
			iTween.MoveTo(((Component)target).gameObject, iTween.Hash(new object[12]
			{
				"x",
				((Component)this).transform.position.x,
				"y",
				((Component)this).transform.position.y,
				"z",
				((Component)this).transform.position.z,
				"time",
				waitTime,
				"islocal",
				false,
				"EaseType",
				"linear"
			}));
			setMonstarNowMapIndex(target);
		}
	}

	public void setMonstarNowMapIndex(SeaAvatarObjBase target)
	{
		target.NowMapIndex = NodeIndex;
		Tools.instance.getPlayer().seaNodeMag.SetSeaMonstarIndex(target.UUID, target.SeaId, NodeIndex);
	}

	public void CastSeaCastHP()
	{
		Avatar player = Tools.instance.getPlayer();
		int indexFengBaoLv = player.seaNodeMag.GetIndexFengBaoLv(NodeIndex, EndlessSeaMag.MapWide);
		if (indexFengBaoLv > 0)
		{
			BaseItem lingZhou = player.GetLingZhou();
			JToken val = jsonData.instance.EndlessSeaLinQiSafeLvData[indexFengBaoLv.ToString()];
			if (lingZhou != null)
			{
				int num = Mathf.Clamp((int)val[(object)"chuandemage"] - (int)player.GetNowLingZhouShuXinJson()[(object)"Defense"], 0, 99999);
				ComAvatar.ReduceLingZhouNaiJiu(lingZhou, num);
			}
			else
			{
				int num2 = (int)val[(object)"damage"];
				((MonoBehaviour)this).StartCoroutine(ReduceHP(-num2));
			}
		}
	}

	public IEnumerator ReduceHP(int realHp)
	{
		yield return (object)new WaitForSeconds(waitTime);
		ComAvatar.AllMapAddHP(realHp);
	}
}
