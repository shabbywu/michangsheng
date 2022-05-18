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

// Token: 0x02000261 RID: 609
public class BaseMapCompont : MonoBehaviour
{
	// Token: 0x060012BC RID: 4796 RVA: 0x00011C9E File Offset: 0x0000FE9E
	protected virtual void Awake()
	{
		this.NodeIndex = int.Parse(base.name);
		this.AllMapCastTimeJsonData = jsonData.instance.AllMapCastTimeJsonData;
		this.MapRandomJsonData = jsonData.instance.MapRandomJsonData;
	}

	// Token: 0x060012BD RID: 4797 RVA: 0x00011CD1 File Offset: 0x0000FED1
	protected virtual void Start()
	{
		this.StartSeting();
	}

	// Token: 0x060012BE RID: 4798 RVA: 0x00011CD9 File Offset: 0x0000FED9
	public virtual void Update()
	{
		this.SetStatic();
	}

	// Token: 0x060012BF RID: 4799 RVA: 0x000AF600 File Offset: 0x000AD800
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

	// Token: 0x060012C0 RID: 4800 RVA: 0x00011CE1 File Offset: 0x0000FEE1
	public virtual void BaseSetFlag()
	{
		this.setFlag();
	}

	// Token: 0x060012C1 RID: 4801 RVA: 0x000AF6E4 File Offset: 0x000AD8E4
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

	// Token: 0x060012C2 RID: 4802 RVA: 0x000AF784 File Offset: 0x000AD984
	public int getRandomNum()
	{
		byte[] array = new byte[8];
		new RNGCryptoServiceProvider().GetBytes(array);
		return Math.Abs(BitConverter.ToInt32(array, 0));
	}

	// Token: 0x060012C3 RID: 4803 RVA: 0x000AF7B0 File Offset: 0x000AD9B0
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
					result = (int)jsonobject["id"].n;
					break;
				}
				num -= (int)jsonobject["percent"].n;
			}
		}
		return result;
	}

	// Token: 0x060012C4 RID: 4804 RVA: 0x000042DD File Offset: 0x000024DD
	public void Talk()
	{
	}

	// Token: 0x060012C5 RID: 4805 RVA: 0x000042DD File Offset: 0x000024DD
	public void StartGame()
	{
	}

	// Token: 0x060012C6 RID: 4806 RVA: 0x00004050 File Offset: 0x00002250
	public virtual bool YuJianFeiXing()
	{
		return false;
	}

	// Token: 0x060012C7 RID: 4807 RVA: 0x000AF8DC File Offset: 0x000ADADC
	public bool CanClick()
	{
		return !AllMapManage.instance.isPlayMove && (this.YuJianFeiXing() || (AllMapManage.instance.mapIndex[this.getAvatarNowMapIndex()].nextIndex.Contains(this.NodeIndex) && this.getAvatarNowMapIndex() != this.NodeIndex));
	}

	// Token: 0x060012C8 RID: 4808 RVA: 0x000AF93C File Offset: 0x000ADB3C
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

	// Token: 0x060012C9 RID: 4809 RVA: 0x000AFA3C File Offset: 0x000ADC3C
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

	// Token: 0x060012CA RID: 4810 RVA: 0x000AFB0C File Offset: 0x000ADD0C
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

	// Token: 0x060012CB RID: 4811 RVA: 0x000AFC58 File Offset: 0x000ADE58
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

	// Token: 0x060012CC RID: 4812 RVA: 0x000AFCDC File Offset: 0x000ADEDC
	public virtual void BaseAddTime()
	{
		JSONObject jsonobject = this.AllMapCastTimeJsonData.list.Find((JSONObject aa) => (int)aa["dunSu"].n >= this.ComAvatar.dunSu);
		if (jsonobject != null)
		{
			this.ComAvatar.AddTime((int)jsonobject["XiaoHao"].n, 0, 0);
		}
	}

	// Token: 0x060012CD RID: 4813 RVA: 0x00011CE9 File Offset: 0x0000FEE9
	public virtual void AvatarMoveToThis()
	{
		if (AllMapManage.instance != null)
		{
			AllMapManage.instance.MapPlayerController.transform.position = base.transform.position;
			this.setAvatarNowMapIndex();
		}
	}

	// Token: 0x060012CE RID: 4814 RVA: 0x00011D1D File Offset: 0x0000FF1D
	public virtual void setAvatarNowMapIndex()
	{
		Tools.instance.fubenLastIndex = this.ComAvatar.NowMapIndex;
		this.ComAvatar.NowMapIndex = this.NodeIndex;
	}

	// Token: 0x060012CF RID: 4815 RVA: 0x00011D45 File Offset: 0x0000FF45
	public virtual int getAvatarNowMapIndex()
	{
		return this.ComAvatar.NowMapIndex;
	}

	// Token: 0x060012D0 RID: 4816 RVA: 0x00011D52 File Offset: 0x0000FF52
	public virtual void addOption(int talkID)
	{
		new AddOption().addOption(talkID);
	}

	// Token: 0x060012D1 RID: 4817 RVA: 0x000042DD File Offset: 0x000024DD
	public virtual void fuBenSetClick()
	{
	}

	// Token: 0x060012D2 RID: 4818 RVA: 0x000042DD File Offset: 0x000024DD
	public virtual void showLuDian()
	{
	}

	// Token: 0x060012D3 RID: 4819 RVA: 0x000042DD File Offset: 0x000024DD
	public virtual void CloseLuDian()
	{
	}

	// Token: 0x060012D4 RID: 4820 RVA: 0x000042DD File Offset: 0x000024DD
	public virtual void ResteAllMapNode()
	{
	}

	// Token: 0x060012D5 RID: 4821 RVA: 0x000AFD28 File Offset: 0x000ADF28
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
			int num2 = (int)avatar.AllMapRandomNode[this.NodeIndex.ToString()]["EventId"].n;
			int num3 = (int)avatar.AllMapRandomNode[this.NodeIndex.ToString()]["Type"].n;
			if (num3 == 2 || num3 == 5 || (int)avatar.AllMapRandomNode[this.NodeIndex.ToString()]["EventId"].n == 0)
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
			int num4 = (int)this.MapRandomJsonData[string.Concat(num2)]["EventData"].n;
			int monstarID = (int)this.MapRandomJsonData[string.Concat(num2)]["MosterID"].n;
			if ((int)this.MapRandomJsonData[string.Concat(num2)]["once"].n != 0)
			{
				if (!avatar.SuiJiShiJian.HasField(Tools.getScreenName()))
				{
					avatar.SuiJiShiJian.AddField(Tools.getScreenName(), new JSONObject(JSONObject.Type.ARRAY));
				}
				avatar.SuiJiShiJian[Tools.getScreenName()].Add(num2);
			}
			switch ((int)this.MapRandomJsonData[string.Concat(num2)]["EventList"].n)
			{
			case 0:
				Object.Instantiate<GameObject>(Resources.Load<GameObject>("talkPrefab/TalkPrefab/talk" + num4));
				break;
			case 1:
				this.addOption(num4);
				break;
			case 2:
				Tools.instance.MonstarID = monstarID;
				Object.Instantiate<GameObject>(Resources.Load<GameObject>("talkPrefab/FightPrefab/Fight" + num4));
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

	// Token: 0x060012D6 RID: 4822 RVA: 0x00011D5F File Offset: 0x0000FF5F
	public void OpenDadituCaiJi()
	{
		ResManager.inst.LoadPrefab("CaiYaoEvent").Inst(null).GetComponent<CaiYaoUIMag>().ShowNomal();
	}

	// Token: 0x060012D7 RID: 4823 RVA: 0x00011D80 File Offset: 0x0000FF80
	public void closeOption(GameObject OptionObject)
	{
		UltimateSurvival.MonoSingleton<UI_Backgroud>.Instance.Value = false;
		OptionObject.gameObject.SetActive(false);
		Tools.canClickFlag = true;
	}

	// Token: 0x04000EB7 RID: 3767
	[NonSerialized]
	public int NodeIndex;

	// Token: 0x04000EB8 RID: 3768
	public List<int> nextIndex = new List<int>();

	// Token: 0x04000EB9 RID: 3769
	public Vector2 MapPositon;

	// Token: 0x04000EBA RID: 3770
	[Tooltip("是否是固定场景，在副本中这个选项决定是否显示节点名称")]
	public bool IsStatic;

	// Token: 0x04000EBB RID: 3771
	public JSONObject MapRandomJsonData;

	// Token: 0x04000EBC RID: 3772
	public JSONObject AllMapCastTimeJsonData;

	// Token: 0x04000EBD RID: 3773
	protected Avatar ComAvatar;

	// Token: 0x04000EBE RID: 3774
	protected GameObject enterScenes;

	// Token: 0x04000EBF RID: 3775
	protected Transform PlayerPosition;
}
