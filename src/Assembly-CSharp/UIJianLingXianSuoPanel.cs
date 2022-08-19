using System;
using System.Collections.Generic;
using JSONClass;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020002D9 RID: 729
public class UIJianLingXianSuoPanel : MonoBehaviour, IESCClose
{
	// Token: 0x06001958 RID: 6488 RVA: 0x000B580A File Offset: 0x000B3A0A
	private void Start()
	{
		UIJianLingXianSuoPanel.Inst = this;
		ESCCloseManager.Inst.RegisterClose(this);
		this.BackBtn.mouseUpEvent.AddListener(new UnityAction(this.Close));
		this.Refresh();
	}

	// Token: 0x06001959 RID: 6489 RVA: 0x000B5840 File Offset: 0x000B3A40
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

	// Token: 0x0600195A RID: 6490 RVA: 0x000B5970 File Offset: 0x000B3B70
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

	// Token: 0x0600195B RID: 6491 RVA: 0x000B5BF4 File Offset: 0x000B3DF4
	public void ShowZhenXiang()
	{
		JianLingZhenXiang jianLingZhenXiang = JianLingZhenXiang.DataList[this.nowSelectedType - 1];
		this.RightText.text = jianLingZhenXiang.desc;
		this.ZhenXiangBtn.gameObject.SetActive(false);
		this.ZhenXiangBtnLock.gameObject.SetActive(false);
	}

	// Token: 0x0600195C RID: 6492 RVA: 0x000B5C48 File Offset: 0x000B3E48
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

	// Token: 0x0600195D RID: 6493 RVA: 0x000B5C90 File Offset: 0x000B3E90
	public void LaoYeYeSay(string msg)
	{
		string text = msg.ReplaceTalkWord();
		this.LaoYeYeTalkText.text = text;
		this.LaoYeYeTalkObj.SetActive(true);
	}

	// Token: 0x0600195E RID: 6494 RVA: 0x000B5CBC File Offset: 0x000B3EBC
	bool IESCClose.TryEscClose()
	{
		this.Close();
		return true;
	}

	// Token: 0x0600195F RID: 6495 RVA: 0x000B57ED File Offset: 0x000B39ED
	public void Close()
	{
		ESCCloseManager.Inst.UnRegisterClose(this);
		Object.Destroy(base.gameObject);
		UIJianLingPanel.OpenPanel();
	}

	// Token: 0x04001483 RID: 5251
	public static UIJianLingXianSuoPanel Inst;

	// Token: 0x04001484 RID: 5252
	public List<FpBtn> XianSuoBtns;

	// Token: 0x04001485 RID: 5253
	public List<GameObject> XianSuoBtnLocks;

	// Token: 0x04001486 RID: 5254
	public FpBtn ZhenXiangBtn;

	// Token: 0x04001487 RID: 5255
	public GameObject ZhenXiangBtnLock;

	// Token: 0x04001488 RID: 5256
	public FpBtn BackBtn;

	// Token: 0x04001489 RID: 5257
	public Text RightTitleText;

	// Token: 0x0400148A RID: 5258
	public Text RightText;

	// Token: 0x0400148B RID: 5259
	private int nowSelectedType;

	// Token: 0x0400148C RID: 5260
	public GameObject LaoYeYeTalkObj;

	// Token: 0x0400148D RID: 5261
	public Text LaoYeYeTalkText;

	// Token: 0x0400148E RID: 5262
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
