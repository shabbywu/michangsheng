using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using CaiYao;
using Fungus;
using KBEngine;
using UltimateSurvival;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using YSGame;

public class BaseMapCompont : MonoBehaviour
{
	[NonSerialized]
	public int NodeIndex;

	public List<int> nextIndex = new List<int>();

	public Vector2 MapPositon;

	[Tooltip("是否是固定场景，在副本中这个选项决定是否显示节点名称")]
	public bool IsStatic;

	public JSONObject MapRandomJsonData;

	public JSONObject AllMapCastTimeJsonData;

	protected Avatar ComAvatar;

	protected GameObject enterScenes;

	protected Transform PlayerPosition;

	protected virtual void Awake()
	{
		NodeIndex = int.Parse(((Object)this).name);
		AllMapCastTimeJsonData = jsonData.instance.AllMapCastTimeJsonData;
		MapRandomJsonData = jsonData.instance.MapRandomJsonData;
	}

	protected virtual void Start()
	{
		StartSeting();
	}

	public virtual void Update()
	{
		SetStatic();
	}

	public virtual void StartSeting()
	{
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		Transform val = ((Component)this).transform.Find("flowchat/enter");
		if ((Object)(object)val != (Object)null)
		{
			enterScenes = ((Component)val).gameObject;
		}
		PlayerPosition = ((Component)this).transform.Find("PlayerPosition");
		ComAvatar = (Avatar)KBEngineApp.app.player();
		AllMapManage.instance.mapIndex[NodeIndex] = this;
		if (NodeIndex == getAvatarNowMapIndex())
		{
			Vector3 position = ((Component)this).transform.position;
			if ((Object)(object)PlayerPosition != (Object)null)
			{
				position = PlayerPosition.position;
			}
			if (Tools.getScreenName().StartsWith("F"))
			{
				position.y -= 0.4f;
			}
			((Component)AllMapManage.instance.MapPlayerController).transform.position = position;
		}
		BaseSetFlag();
	}

	public virtual void BaseSetFlag()
	{
		setFlag();
	}

	public int getRandomNumSum()
	{
		int num = 0;
		foreach (JSONObject item in MapRandomJsonData.list)
		{
			if (item["EventLv"].list.Find((JSONObject aa) => (int)aa.n == Tools.instance.getPlayer().level) != null)
			{
				num += (int)item["percent"].n;
			}
		}
		return num;
	}

	public int getRandomNum()
	{
		byte[] array = new byte[8];
		new RNGCryptoServiceProvider().GetBytes(array);
		return Math.Abs(BitConverter.ToInt32(array, 0));
	}

	public int getEventID()
	{
		int randomNumSum = getRandomNumSum();
		if (randomNumSum == 0)
		{
			Debug.LogError((object)$"地图{SceneEx.NowSceneName}的{NodeIndex}获取事件ID失败，MapRandomJsonData中没有EventLv为{PlayerEx.Player.getLevelType()}的数据，请反馈策划\njson详细:\n{MapRandomJsonData}");
			return 0;
		}
		int num = getRandomNum() % randomNumSum;
		int result = 0;
		foreach (JSONObject item in MapRandomJsonData.list)
		{
			if (item["EventLv"].list.Find((JSONObject aa) => (int)aa.n == Tools.instance.getPlayer().level) != null)
			{
				if ((int)item["percent"].n >= num)
				{
					result = item["id"].I;
					break;
				}
				num -= (int)item["percent"].n;
			}
		}
		return result;
	}

	public void Talk()
	{
	}

	public void StartGame()
	{
	}

	public virtual bool YuJianFeiXing()
	{
		return false;
	}

	public bool CanClick()
	{
		if (AllMapManage.instance.isPlayMove)
		{
			return false;
		}
		if (YuJianFeiXing())
		{
			return true;
		}
		if (AllMapManage.instance.mapIndex[getAvatarNowMapIndex()].nextIndex.Contains(NodeIndex) && getAvatarNowMapIndex() != NodeIndex)
		{
			return true;
		}
		return false;
	}

	public void showDebugLine()
	{
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		foreach (int item in nextIndex)
		{
			Transform val = ((Component)this).transform.parent.Find(string.Concat(item));
			if ((Object)(object)val != (Object)null)
			{
				Vector3 position = ((Component)val).transform.position;
				Vector3 val2 = ((Component)this).transform.position - position;
				Vector3 normalized = ((Vector3)(ref val2)).normalized;
				Debug.DrawLine(((Component)this).transform.position, position + normalized * 0.5f, Color.red, 0.01f);
			}
			else
			{
				Debug.LogError((object)(((Object)this).name + "节点的链接节点（" + item + "）找不到，请进行修改"));
			}
		}
	}

	public virtual void SetStatic()
	{
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		if (IsStatic && (Object)(object)PlayerPosition != (Object)null)
		{
			if (getAvatarNowMapIndex() == NodeIndex)
			{
				enterScenes.transform.localPosition = ((Component)PlayerPosition).transform.localPosition + new Vector3(0f, -1.2f, -2f);
				enterScenes.SetActive(true);
			}
			else if (enterScenes.activeSelf)
			{
				enterScenes.transform.localPosition = ((Component)PlayerPosition).transform.localPosition + new Vector3(0f, -1.2f, -2f);
				enterScenes.SetActive(false);
			}
		}
	}

	public void setFlag()
	{
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		if (ComAvatar.taskMag._TaskData.HasField("ShowTask") && (int)ComAvatar.taskMag._TaskData["ShowTask"].n != 0)
		{
			int taskID = (int)ComAvatar.taskMag._TaskData["ShowTask"].n;
			int index = (int)ComAvatar.taskMag._TaskData["Task"][taskID.ToString()]["NowIndex"].n;
			JSONObject taskInfo = jsonData.instance.getTaskInfo(taskID, index);
			JSONObject jSONObject = jsonData.instance.TaskJsonData[taskID.ToString()];
			if ((taskInfo != null && (int)taskInfo["mapIndex"].n == NodeIndex) || (int)jSONObject["mapIndex"].n == NodeIndex)
			{
				Transform val = ((Component)this).transform.Find("PlayerPosition");
				AllMapManage.instance.TaskFlag.transform.position = (((Object)(object)val != (Object)null) ? ((Component)val).transform.position : ((Component)this).transform.position);
			}
		}
	}

	public virtual void movaAvatar()
	{
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		if (!CanClick())
		{
			((Component)((Component)this).transform.Find("flowchat")).GetComponent<Flowchart>().StopAllBlocks();
			return;
		}
		if ((Object)(object)UINPCLeftList.Inst != (Object)null && !UINPCLeftList.Inst.nowLeft)
		{
			Scene activeScene = SceneManager.GetActiveScene();
			if (!((Scene)(ref activeScene)).name.StartsWith("Sea") && !UINPCJiaoHu.Inst.NowIsJiaoHu)
			{
				UINPCLeftList.Inst.ToLeft();
			}
		}
		AvatarMoveToThis();
		BaseAddTime();
	}

	public virtual void BaseAddTime()
	{
		JSONObject jSONObject = AllMapCastTimeJsonData.list.Find((JSONObject aa) => (int)aa["dunSu"].n >= ComAvatar.dunSu);
		if (jSONObject != null)
		{
			ComAvatar.AddTime((int)jSONObject["XiaoHao"].n);
		}
	}

	public virtual void AvatarMoveToThis()
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)AllMapManage.instance != (Object)null)
		{
			((Component)AllMapManage.instance.MapPlayerController).transform.position = ((Component)this).transform.position;
			setAvatarNowMapIndex();
		}
	}

	public virtual void setAvatarNowMapIndex()
	{
		Tools.instance.fubenLastIndex = ComAvatar.NowMapIndex;
		ComAvatar.NowMapIndex = NodeIndex;
	}

	public virtual int getAvatarNowMapIndex()
	{
		return ComAvatar.NowMapIndex;
	}

	public virtual void addOption(int talkID)
	{
		new AddOption().addOption(talkID);
	}

	public virtual void fuBenSetClick()
	{
	}

	public virtual void showLuDian()
	{
	}

	public virtual void CloseLuDian()
	{
	}

	public virtual void ResteAllMapNode()
	{
	}

	public virtual void EventRandom()
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Expected O, but got Unknown
		if (!CanClick())
		{
			return;
		}
		fuBenSetClick();
		movaAvatar();
		if (IsStatic)
		{
			return;
		}
		Queue<UnityAction> queue = new Queue<UnityAction>();
		UnityAction item = (UnityAction)delegate
		{
			Avatar avatar = (Avatar)KBEngineApp.app.player();
			int num = avatar.nomelTaskMag.AutoAllMapPlaceHasNTask(new List<int> { NodeIndex });
			if (num != -1)
			{
				JSONObject jSONObject = avatar.nomelTaskMag.IsNTaskZiXiangInLuJin(num, new List<int> { NodeIndex });
				JSONObject nowChildIDSuiJiJson = avatar.nomelTaskMag.GetNowChildIDSuiJiJson(num);
				if (jSONObject["type"].I == 5)
				{
					avatar.randomFuBenMag.GetInRandomFuBen(NodeIndex);
				}
				else
				{
					GlobalValue.Set(401, nowChildIDSuiJiJson["Value"].I, ((object)this).GetType().Name + ".EventRandom");
					GlobalValue.Set(402, num, ((object)this).GetType().Name + ".EventRandom");
					Object.Instantiate<GameObject>(Resources.Load<GameObject>("talkPrefab/TalkPrefab/talk" + jSONObject["talkID"].str));
				}
				YSFuncList.Ints.Continue();
			}
			else
			{
				int i = avatar.AllMapRandomNode[NodeIndex.ToString()]["EventId"].I;
				int num2 = (int)avatar.AllMapRandomNode[NodeIndex.ToString()]["Type"].n;
				if (num2 == 2 || num2 == 5 || avatar.AllMapRandomNode[NodeIndex.ToString()]["EventId"].I == 0)
				{
					if ((Object)(object)FungusManager.Instance.jieShaBlock == (Object)null)
					{
						GameObject val = Object.Instantiate<GameObject>(Resources.Load<GameObject>("talkPrefab/TalkPrefab/talk4010"));
						FungusManager.Instance.jieShaBlock = val.GetComponentInChildren<Flowchart>();
					}
					else if (GlobalValue.Get(171, ((object)this).GetType().Name + ".EventRandom") == 1)
					{
						GlobalValue.Set(171, 0, ((object)this).GetType().Name + ".EventRandom");
					}
					else
					{
						FungusManager.Instance.jieShaBlock.Reset(resetCommands: false, resetVariables: true);
						FungusManager.Instance.jieShaBlock.ExecuteBlock("Splash");
					}
					ResteAllMapNode();
					Tools.instance.getPlayer().AllMapSetNode();
					YSFuncList.Ints.Continue();
				}
				else
				{
					int i2 = MapRandomJsonData[string.Concat(i)]["EventData"].I;
					int i3 = MapRandomJsonData[string.Concat(i)]["MosterID"].I;
					if (MapRandomJsonData[string.Concat(i)]["once"].I != 0)
					{
						if (!avatar.SuiJiShiJian.HasField(Tools.getScreenName()))
						{
							avatar.SuiJiShiJian.AddField(Tools.getScreenName(), new JSONObject(JSONObject.Type.ARRAY));
						}
						avatar.SuiJiShiJian[Tools.getScreenName()].Add(i);
					}
					switch ((int)MapRandomJsonData[string.Concat(i)]["EventList"].n)
					{
					case 0:
						Object.Instantiate<GameObject>(Resources.Load<GameObject>("talkPrefab/TalkPrefab/talk" + i2));
						break;
					case 1:
						addOption(i2);
						break;
					case 2:
						Tools.instance.MonstarID = i3;
						Object.Instantiate<GameObject>(Resources.Load<GameObject>("talkPrefab/FightPrefab/Fight" + i2));
						break;
					case 3:
						OpenDadituCaiJi();
						break;
					}
					ResteAllMapNode();
					Tools.instance.getPlayer().AllMapSetNode();
					YSFuncList.Ints.Continue();
				}
			}
		};
		queue.Enqueue(item);
		YSFuncList.Ints.AddFunc(queue);
	}

	public void OpenDadituCaiJi()
	{
		ResManager.inst.LoadPrefab("CaiYaoEvent").Inst().GetComponent<CaiYaoUIMag>()
			.ShowNomal();
	}

	public void closeOption(GameObject OptionObject)
	{
		UltimateSurvival.MonoSingleton<UI_Backgroud>.Instance.Value = false;
		OptionObject.gameObject.SetActive(false);
		Tools.canClickFlag = true;
	}
}
