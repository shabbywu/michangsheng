using System;
using System.Collections.Generic;
using JSONClass;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000429 RID: 1065
public class UIJianLingXianSuoPanel : MonoBehaviour, IESCClose
{
	// Token: 0x06001C69 RID: 7273 RVA: 0x00017B80 File Offset: 0x00015D80
	private void Start()
	{
		UIJianLingXianSuoPanel.Inst = this;
		ESCCloseManager.Inst.RegisterClose(this);
		this.BackBtn.mouseUpEvent.AddListener(new UnityAction(this.Close));
		this.Refresh();
	}

	// Token: 0x06001C6A RID: 7274 RVA: 0x000FB8FC File Offset: 0x000F9AFC
	public void Refresh()
	{
		this.UnlockDefaultXianSuo();
		JianLingManager jianLingManager = PlayerEx.Player.jianLingManager;
		List<int> list = new List<int>();
		foreach (JianLingXianSuo jianLingXianSuo in JianLingXianSuo.DataList)
		{
			if (!list.Contains(jianLingXianSuo.Type) && jianLingManager.IsXianSuoUnlocked(jianLingXianSuo.id))
			{
				list.Add(jianLingXianSuo.Type);
			}
		}
		for (int i = 1; i <= 4; i++)
		{
			int index = i;
			bool flag = list.Contains(index);
			FpBtn fpBtn = this.XianSuoBtns[index - 1];
			GameObject gameObject = this.XianSuoBtnLocks[index - 1];
			fpBtn.gameObject.SetActive(flag);
			gameObject.SetActive(!flag);
			if (flag)
			{
				fpBtn.mouseUpEvent.AddListener(delegate()
				{
					this.SelectXianSuo(index, true);
				});
			}
		}
		this.SelectXianSuo(1, false);
	}

	// Token: 0x06001C6B RID: 7275 RVA: 0x000FBA2C File Offset: 0x000F9C2C
	public void SelectXianSuo(int type, bool laoYeYeTalk = false)
	{
		this.nowSelectedType = type;
		this.RightTitleText.text = UIJianLingXianSuoPanel.XianSuoTypeNameDict[type];
		for (int i = 0; i < this.XianSuoBtns.Count; i++)
		{
			if (this.XianSuoBtns[i].gameObject.activeSelf)
			{
				this.XianSuoBtns[i].transform.GetChild(0).gameObject.SetActive(type == i + 1);
			}
		}
		string text = "";
		JianLingManager jianLingManager = PlayerEx.Player.jianLingManager;
		List<JianLingXianSuo> list = new List<JianLingXianSuo>();
		int num = 0;
		foreach (JianLingXianSuo jianLingXianSuo in JianLingXianSuo.DataList)
		{
			if (jianLingXianSuo.Type == type)
			{
				if (jianLingManager.IsXianSuoUnlocked(jianLingXianSuo.id))
				{
					list.Add(jianLingXianSuo);
					if (jianLingXianSuo.XianSuoLV > num)
					{
						num = jianLingXianSuo.XianSuoLV;
					}
					text = text + jianLingXianSuo.desc + "\n";
				}
				else
				{
					text += "           ？？？\n";
				}
				text += "\n";
			}
		}
		this.RightText.text = text;
		JianLingZhenXiang jianLingZhenXiang = JianLingZhenXiang.DataList[type - 1];
		this.ZhenXiangBtn.mouseUpEvent.RemoveAllListeners();
		bool flag = jianLingManager.IsZhenXiangUnlocked(jianLingZhenXiang.id);
		this.ZhenXiangBtn.gameObject.SetActive(flag);
		this.ZhenXiangBtnLock.gameObject.SetActive(!flag);
		if (flag)
		{
			this.ZhenXiangBtn.mouseUpEvent.AddListener(new UnityAction(this.ShowZhenXiang));
		}
		if (laoYeYeTalk)
		{
			for (int j = list.Count - 1; j >= 0; j--)
			{
				if (list[j].XianSuoLV < num)
				{
					list.RemoveAt(j);
				}
			}
			if (list.Count > 0)
			{
				List<string> list2 = new List<string>();
				foreach (JianLingXianSuo jianLingXianSuo2 in list)
				{
					list2.Add(jianLingXianSuo2.XianSuoDuiHua1);
					list2.Add(jianLingXianSuo2.XianSuoDuiHua2);
				}
				int index = Random.Range(0, list2.Count);
				this.LaoYeYeSay(list2[index]);
				return;
			}
		}
		else
		{
			this.LaoYeYeTalkObj.SetActive(false);
		}
	}

	// Token: 0x06001C6C RID: 7276 RVA: 0x000FBCB0 File Offset: 0x000F9EB0
	public void ShowZhenXiang()
	{
		JianLingZhenXiang jianLingZhenXiang = JianLingZhenXiang.DataList[this.nowSelectedType - 1];
		this.RightText.text = jianLingZhenXiang.desc;
		this.ZhenXiangBtn.gameObject.SetActive(false);
		this.ZhenXiangBtnLock.gameObject.SetActive(false);
	}

	// Token: 0x06001C6D RID: 7277 RVA: 0x000FBD04 File Offset: 0x000F9F04
	public void UnlockDefaultXianSuo()
	{
		JianLingManager jianLingManager = PlayerEx.Player.jianLingManager;
		if (!jianLingManager.IsXianSuoUnlocked("神秘铁剑"))
		{
			jianLingManager.UnlockXianSuo("神秘铁剑");
		}
		if (!jianLingManager.IsXianSuoUnlocked("往昔追忆开局"))
		{
			jianLingManager.UnlockXianSuo("往昔追忆开局");
		}
	}

	// Token: 0x06001C6E RID: 7278 RVA: 0x000FBD4C File Offset: 0x000F9F4C
	public void LaoYeYeSay(string msg)
	{
		string text = msg.ReplaceTalkWord();
		this.LaoYeYeTalkText.text = text;
		this.LaoYeYeTalkObj.SetActive(true);
	}

	// Token: 0x06001C6F RID: 7279 RVA: 0x00017BB5 File Offset: 0x00015DB5
	bool IESCClose.TryEscClose()
	{
		this.Close();
		return true;
	}

	// Token: 0x06001C70 RID: 7280 RVA: 0x00017B50 File Offset: 0x00015D50
	public void Close()
	{
		ESCCloseManager.Inst.UnRegisterClose(this);
		Object.Destroy(base.gameObject);
		UIJianLingPanel.OpenPanel();
	}

	// Token: 0x0400185E RID: 6238
	public static UIJianLingXianSuoPanel Inst;

	// Token: 0x0400185F RID: 6239
	public List<FpBtn> XianSuoBtns;

	// Token: 0x04001860 RID: 6240
	public List<GameObject> XianSuoBtnLocks;

	// Token: 0x04001861 RID: 6241
	public FpBtn ZhenXiangBtn;

	// Token: 0x04001862 RID: 6242
	public GameObject ZhenXiangBtnLock;

	// Token: 0x04001863 RID: 6243
	public FpBtn BackBtn;

	// Token: 0x04001864 RID: 6244
	public Text RightTitleText;

	// Token: 0x04001865 RID: 6245
	public Text RightText;

	// Token: 0x04001866 RID: 6246
	private int nowSelectedType;

	// Token: 0x04001867 RID: 6247
	public GameObject LaoYeYeTalkObj;

	// Token: 0x04001868 RID: 6248
	public Text LaoYeYeTalkText;

	// Token: 0x04001869 RID: 6249
	private static Dictionary<int, string> XianSuoTypeNameDict = new Dictionary<int, string>
	{
		{
			1,
			"神秘铁剑"
		},
		{
			2,
			"昔日身份"
		},
		{
			3,
			"魔道踪影"
		},
		{
			4,
			"御剑门之谜"
		}
	};
}
