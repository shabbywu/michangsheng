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

// Token: 0x02000183 RID: 387
public class BaseMapCompont : MonoBehaviour
{
	// Token: 0x0600106F RID: 4207 RVA: 0x0006094E File Offset: 0x0005EB4E
	protected virtual void Awake()
	{
		this.NodeIndex = int.Parse(base.name);
		this.AllMapCastTimeJsonData = jsonData.instance.AllMapCastTimeJsonData;
		this.MapRandomJsonData = jsonData.instance.MapRandomJsonData;
	}

	// Token: 0x06001070 RID: 4208 RVA: 0x00060981 File Offset: 0x0005EB81
	protected virtual void Start()
	{
		this.StartSeting();
	}

	// Token: 0x06001071 RID: 4209 RVA: 0x00060989 File Offset: 0x0005EB89
	public virtual void Update()
	{
		this.SetStatic();
	}

	// Token: 0x06001072 RID: 4210 RVA: 0x00060994 File Offset: 0x0005EB94
	public virtual void StartSeting()
	{
		Transform transform = base.transform.Find("flowchat/enter");
		if (transform != null)
		{
			this.enterScenes = transform.gameObject;
		}
		this.PlayerPosition = base.transform.Find("PlayerPosition");
		this.ComAvatar = (Avatar)KBEngineApp.app.player();
		AllMapManage.instance.mapIndex[this.NodeIndex] = this;
		if (this.NodeIndex == this.getAvatarNowMapIndex())
		{
			Vector3 position = base.transform.position;
			if (this.PlayerPosition != null)
			{
				position = this.PlayerPosition.position;
			}
			if (Tools.getScreenName().StartsWith("F"))
			{
				position.y -= 0.4f;
			}
			AllMapManage.instance.MapPlayerController.transform.position = position;
		}
		this.BaseSetFlag();
	}

	// Token: 0x06001073 RID: 4211 RVA: 0x00060A78 File Offset: 0x0005EC78
	public virtual void BaseSetFlag()
	{
		this.setFlag();
	}

	// Token: 0x06001074 RID: 4212 RVA: 0x00060A80 File Offset: 0x0005EC80
	public int getRandomNumSum()
	{
		int num = 0;
		foreach (JSONObject jsonobject in this.MapRandomJsonData.list)
		{
			if (jsonobject["EventLv"].list.Find((JSONObject aa) => (int)aa.n == (int)Tools.instance.getPlayer().level) != null)
			{
				num += (int)jsonobject["percent"].n;
			}
		}
		return num;
	}

	// Token: 0x06001075 RID: 4213 RVA: 0x00060B20 File Offset: 0x0005ED20
	public int getRandomNum()
	{
		byte[] array = new byte[8];
		new RNGCryptoServiceProvider().GetBytes(array);
		return Math.Abs(BitConverter.ToInt32(array, 0));
	}

	// Token: 0x06001076 RID: 4214 RVA: 0x00060B4C File Offset: 0x0005ED4C
	public int getEventID()
	{
		int randomNumSum = this.getRandomNumSum();
		if (randomNumSum == 0)
		{
			Debug.LogError(string.Format("地图{0}的{1}获取事件ID失败，MapRandomJsonData中没有EventLv为{2}的数据，请反馈策划\njson详细:\n{3}", new object[]
			{
				SceneEx.NowSceneName,
				this.NodeIndex,
				PlayerEx.Player.getLevelType(),
				this.MapRandomJsonData
			}));
			return 0;
		}
		int num = this.getRandomNum() % randomNumSum;
		int result = 0;
		foreach (JSONObject jsonobject in this.MapRandomJsonData.list)
		{
			if (jsonobject["EventLv"].list.Find((JSONObject aa) => (int)aa.n == (int)Tools.instance.getPlayer().level) != null)
			{
				if ((int)jsonobject["percent"].n >= num)
				{
					result = jsonobject["id"].I;
					break;
				}
				num -= (int)jsonobject["percent"].n;
			}
		}
		return result;
	}

	// Token: 0x06001077 RID: 4215 RVA: 0x00004095 File Offset: 0x00002295
	public void Talk()
	{
	}

	// Token: 0x06001078 RID: 4216 RVA: 0x00004095 File Offset: 0x00002295
	public void StartGame()
	{
	}

	// Token: 0x06001079 RID: 4217 RVA: 0x0000280F File Offset: 0x00000A0F
	public virtual bool YuJianFeiXing()
	{
		return false;
	}

	// Token: 0x0600107A RID: 4218 RVA: 0x00060C74 File Offset: 0x0005EE74
	public bool CanClick()
	{
		return !AllMapManage.instance.isPlayMove && (this.YuJianFeiXing() || (AllMapManage.instance.mapIndex[this.getAvatarNowMapIndex()].nextIndex.Contains(this.NodeIndex) && this.getAvatarNowMapIndex() != this.NodeIndex));
	}

	// Token: 0x0600107B RID: 4219 RVA: 0x00060CD4 File Offset: 0x0005EED4
	public void showDebugLine()
	{
		foreach (int num in this.nextIndex)
		{
			Transform transform = base.transform.parent.Find(string.Concat(num));
			if (transform != null)
			{
				Vector3 position = transform.transform.position;
				Vector3 normalized = (base.transform.position - position).normalized;
				Debug.DrawLine(base.transform.position, position + normalized * 0.5f, Color.red, 0.01f);
			}
			else
			{
				Debug.LogError(string.Concat(new object[]
				{
					base.name,
					"节点的链接节点（",
					num,
					"）找不到，请进行修改"
				}));
			}
		}
	}

	// Token: 0x0600107C RID: 4220 RVA: 0x00060DD4 File Offset: 0x0005EFD4
	public virtual void SetStatic()
	{
		if (this.IsStatic && this.PlayerPosition != null)
		{
			if (this.getAvatarNowMapIndex() == this.NodeIndex)
			{
				this.enterScenes.transform.localPosition = this.PlayerPosition.transform.localPosition + new Vector3(0f, -1.2f, -2f);
				this.enterScenes.SetActive(true);
				return;
			}
			if (this.enterScenes.activeSelf)
			{
				this.enterScenes.transform.localPosition = this.PlayerPosition.transform.localPosition + new Vector3(0f, -1.2f, -2f);
				this.enterScenes.SetActive(false);
			}
		}
	}

	// Token: 0x0600107D RID: 4221 RVA: 0x00060EA4 File Offset: 0x0005F0A4
	public void setFlag()
	{
		if (this.ComAvatar.taskMag._TaskData.HasField("ShowTask") && (int)this.ComAvatar.taskMag._TaskData["ShowTask"].n != 0)
		{
			int taskID = (int)this.ComAvatar.taskMag._TaskData["ShowTask"].n;
			int index = (int)this.ComAvatar.taskMag._TaskData["Task"][taskID.ToString()]["NowIndex"].n;
			JSONObject taskInfo = jsonData.instance.getTaskInfo(taskID, index);
			JSONObject jsonobject = jsonData.instance.TaskJsonData[taskID.ToString()];
			if ((taskInfo != null && (int)taskInfo["mapIndex"].n == this.NodeIndex) || (int)jsonobject["mapIndex"].n == this.NodeIndex)
			{
				Transform transform = base.transform.Find("PlayerPosition");
				AllMapManage.instance.TaskFlag.transform.position = ((transform != null) ? transform.transform.position : base.transform.position);
			}
		}
	}

	// Token: 0x0600107E RID: 4222 RVA: 0x00060FF0 File Offset: 0x0005F1F0
	public virtual void movaAvatar()
	{
		if (!this.CanClick())
		{
			base.transform.Find("flowchat").GetComponent<Flowchart>().StopAllBlocks();
			return;
		}
		if (UINPCLeftList.Inst != null && !UINPCLeftList.Inst.nowLeft && !SceneManager.GetActiveScene().name.StartsWith("Sea") && !UINPCJiaoHu.Inst.NowIsJiaoHu)
		{
			UINPCLeftList.Inst.ToLeft();
		}
		this.AvatarMoveToThis();
		this.BaseAddTime();
	}

	// Token: 0x0600107F RID: 4223 RVA: 0x00061074 File Offset: 0x0005F274
	public virtual void BaseAddTime()
	{
		JSONObject jsonobject = this.AllMapCastTimeJsonData.list.Find((JSONObject aa) => (int)aa["dunSu"].n >= this.ComAvatar.dunSu);
		if (jsonobject != null)
		{
			this.ComAvatar.AddTime((int)jsonobject["XiaoHao"].n, 0, 0);
		}
	}

	// Token: 0x06001080 RID: 4224 RVA: 0x000610BF File Offset: 0x0005F2BF
	public virtual void AvatarMoveToThis()
	{
		if (AllMapManage.instance != null)
		{
			AllMapManage.instance.MapPlayerController.transform.position = base.transform.position;
			this.setAvatarNowMapIndex();
		}
	}

	// Token: 0x06001081 RID: 4225 RVA: 0x000610F3 File Offset: 0x0005F2F3
	public virtual void setAvatarNowMapIndex()
	{
		Tools.instance.fubenLastIndex = this.ComAvatar.NowMapIndex;
		this.ComAvatar.NowMapIndex = this.NodeIndex;
	}

	// Token: 0x06001082 RID: 4226 RVA: 0x0006111B File Offset: 0x0005F31B
	public virtual int getAvatarNowMapIndex()
	{
		return this.ComAvatar.NowMapIndex;
	}

	// Token: 0x06001083 RID: 4227 RVA: 0x00061128 File Offset: 0x0005F328
	public virtual void addOption(int talkID)
	{
		new AddOption().addOption(talkID);
	}

	// Token: 0x06001084 RID: 4228 RVA: 0x00004095 File Offset: 0x00002295
	public virtual void fuBenSetClick()
	{
	}

	// Token: 0x06001085 RID: 4229 RVA: 0x00004095 File Offset: 0x00002295
	public virtual void showLuDian()
	{
	}

	// Token: 0x06001086 RID: 4230 RVA: 0x00004095 File Offset: 0x00002295
	public virtual void CloseLuDian()
	{
	}

	// Token: 0x06001087 RID: 4231 RVA: 0x00004095 File Offset: 0x00002295
	public virtual void ResteAllMapNode()
	{
	}

	// Token: 0x06001088 RID: 4232 RVA: 0x00061138 File Offset: 0x0005F338
	public virtual void EventRandom()
	{
		if (!this.CanClick())
		{
			return;
		}
		this.fuBenSetClick();
		this.movaAvatar();
		if (this.IsStatic)
		{
			return;
		}
		Queue<UnityAction> queue = new Queue<UnityAction>();
		UnityAction item = delegate()
		{
			Avatar avatar = (Avatar)KBEngineApp.app.player();
			int num = avatar.nomelTaskMag.AutoAllMapPlaceHasNTask(new List<int>
			{
				this.NodeIndex
			});
			if (num != -1)
			{
				JSONObject jsonobject = avatar.nomelTaskMag.IsNTaskZiXiangInLuJin(num, new List<int>
				{
					this.NodeIndex
				});
				JSONObject nowChildIDSuiJiJson = avatar.nomelTaskMag.GetNowChildIDSuiJiJson(num);
				if (jsonobject["type"].I == 5)
				{
					avatar.randomFuBenMag.GetInRandomFuBen(this.NodeIndex, -1);
				}
				else
				{
					GlobalValue.Set(401, nowChildIDSuiJiJson["Value"].I, base.GetType().Name + ".EventRandom");
					GlobalValue.Set(402, num, base.GetType().Name + ".EventRandom");
					Object.Instantiate<GameObject>(Resources.Load<GameObject>("talkPrefab/TalkPrefab/talk" + jsonobject["talkID"].str));
				}
				YSFuncList.Ints.Continue();
				return;
			}
			int i = avatar.AllMapRandomNode[this.NodeIndex.ToString()]["EventId"].I;
			int num2 = (int)avatar.AllMapRandomNode[this.NodeIndex.ToString()]["Type"].n;
			if (num2 == 2 || num2 == 5 || avatar.AllMapRandomNode[this.NodeIndex.ToString()]["EventId"].I == 0)
			{
				if (FungusManager.Instance.jieShaBlock == null)
				{
					GameObject gameObject = Object.Instantiate<GameObject>(Resources.Load<GameObject>("talkPrefab/TalkPrefab/talk4010"));
					FungusManager.Instance.jieShaBlock = gameObject.GetComponentInChildren<Flowchart>();
				}
				else if (GlobalValue.Get(171, base.GetType().Name + ".EventRandom") == 1)
				{
					GlobalValue.Set(171, 0, base.GetType().Name + ".EventRandom");
				}
				else
				{
					FungusManager.Instance.jieShaBlock.Reset(false, true);
					FungusManager.Instance.jieShaBlock.ExecuteBlock("Splash");
				}
				this.ResteAllMapNode();
				Tools.instance.getPlayer().AllMapSetNode();
				YSFuncList.Ints.Continue();
				return;
			}
			int i2 = this.MapRandomJsonData[string.Concat(i)]["EventData"].I;
			int i3 = this.MapRandomJsonData[string.Concat(i)]["MosterID"].I;
			if (this.MapRandomJsonData[string.Concat(i)]["once"].I != 0)
			{
				if (!avatar.SuiJiShiJian.HasField(Tools.getScreenName()))
				{
					avatar.SuiJiShiJian.AddField(Tools.getScreenName(), new JSONObject(JSONObject.Type.ARRAY));
				}
				avatar.SuiJiShiJian[Tools.getScreenName()].Add(i);
			}
			switch ((int)this.MapRandomJsonData[string.Concat(i)]["EventList"].n)
			{
			case 0:
				Object.Instantiate<GameObject>(Resources.Load<GameObject>("talkPrefab/TalkPrefab/talk" + i2));
				break;
			case 1:
				this.addOption(i2);
				break;
			case 2:
				Tools.instance.MonstarID = i3;
				Object.Instantiate<GameObject>(Resources.Load<GameObject>("talkPrefab/FightPrefab/Fight" + i2));
				break;
			case 3:
				this.OpenDadituCaiJi();
				break;
			}
			this.ResteAllMapNode();
			Tools.instance.getPlayer().AllMapSetNode();
			YSFuncList.Ints.Continue();
		};
		queue.Enqueue(item);
		YSFuncList.Ints.AddFunc(queue);
	}

	// Token: 0x06001089 RID: 4233 RVA: 0x00061188 File Offset: 0x0005F388
	public void OpenDadituCaiJi()
	{
		ResManager.inst.LoadPrefab("CaiYaoEvent").Inst(null).GetComponent<CaiYaoUIMag>().ShowNomal();
	}

	// Token: 0x0600108A RID: 4234 RVA: 0x000611A9 File Offset: 0x0005F3A9
	public void closeOption(GameObject OptionObject)
	{
		UltimateSurvival.MonoSingleton<UI_Backgroud>.Instance.Value = false;
		OptionObject.gameObject.SetActive(false);
		Tools.canClickFlag = true;
	}

	// Token: 0x04000BE6 RID: 3046
	[NonSerialized]
	public int NodeIndex;

	// Token: 0x04000BE7 RID: 3047
	public List<int> nextIndex = new List<int>();

	// Token: 0x04000BE8 RID: 3048
	public Vector2 MapPositon;

	// Token: 0x04000BE9 RID: 3049
	[Tooltip("是否是固定场景，在副本中这个选项决定是否显示节点名称")]
	public bool IsStatic;

	// Token: 0x04000BEA RID: 3050
	public JSONObject MapRandomJsonData;

	// Token: 0x04000BEB RID: 3051
	public JSONObject AllMapCastTimeJsonData;

	// Token: 0x04000BEC RID: 3052
	protected Avatar ComAvatar;

	// Token: 0x04000BED RID: 3053
	protected GameObject enterScenes;

	// Token: 0x04000BEE RID: 3054
	protected Transform PlayerPosition;
}
