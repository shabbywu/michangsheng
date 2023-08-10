using System.Collections.Generic;
using JSONClass;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIJianLingXianSuoPanel : MonoBehaviour, IESCClose
{
	public static UIJianLingXianSuoPanel Inst;

	public List<FpBtn> XianSuoBtns;

	public List<GameObject> XianSuoBtnLocks;

	public FpBtn ZhenXiangBtn;

	public GameObject ZhenXiangBtnLock;

	public FpBtn BackBtn;

	public Text RightTitleText;

	public Text RightText;

	private int nowSelectedType;

	public GameObject LaoYeYeTalkObj;

	public Text LaoYeYeTalkText;

	private static Dictionary<int, string> XianSuoTypeNameDict = new Dictionary<int, string>
	{
		{ 1, "神秘铁剑" },
		{ 2, "昔日身份" },
		{ 3, "魔道踪影" },
		{ 4, "御剑门之谜" }
	};

	private void Start()
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Expected O, but got Unknown
		Inst = this;
		ESCCloseManager.Inst.RegisterClose(this);
		BackBtn.mouseUpEvent.AddListener(new UnityAction(Close));
		Refresh();
	}

	public void Refresh()
	{
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Expected O, but got Unknown
		UnlockDefaultXianSuo();
		JianLingManager jianLingManager = PlayerEx.Player.jianLingManager;
		List<int> list = new List<int>();
		foreach (JianLingXianSuo data in JianLingXianSuo.DataList)
		{
			if (!list.Contains(data.Type) && jianLingManager.IsXianSuoUnlocked(data.id))
			{
				list.Add(data.Type);
			}
		}
		for (int i = 1; i <= 4; i++)
		{
			int index = i;
			bool flag = list.Contains(index);
			FpBtn fpBtn = XianSuoBtns[index - 1];
			GameObject obj = XianSuoBtnLocks[index - 1];
			((Component)fpBtn).gameObject.SetActive(flag);
			obj.SetActive(!flag);
			if (flag)
			{
				fpBtn.mouseUpEvent.AddListener((UnityAction)delegate
				{
					SelectXianSuo(index, laoYeYeTalk: true);
				});
			}
		}
		SelectXianSuo(1);
	}

	public void SelectXianSuo(int type, bool laoYeYeTalk = false)
	{
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Expected O, but got Unknown
		nowSelectedType = type;
		RightTitleText.text = XianSuoTypeNameDict[type];
		for (int i = 0; i < XianSuoBtns.Count; i++)
		{
			if (((Component)XianSuoBtns[i]).gameObject.activeSelf)
			{
				((Component)((Component)XianSuoBtns[i]).transform.GetChild(0)).gameObject.SetActive(type == i + 1);
			}
		}
		string text = "";
		JianLingManager jianLingManager = PlayerEx.Player.jianLingManager;
		List<JianLingXianSuo> list = new List<JianLingXianSuo>();
		int num = 0;
		foreach (JianLingXianSuo data in JianLingXianSuo.DataList)
		{
			if (data.Type != type)
			{
				continue;
			}
			if (jianLingManager.IsXianSuoUnlocked(data.id))
			{
				list.Add(data);
				if (data.XianSuoLV > num)
				{
					num = data.XianSuoLV;
				}
				text = text + data.desc + "\n";
			}
			else
			{
				text += "           ？？？\n";
			}
			text += "\n";
		}
		RightText.text = text;
		JianLingZhenXiang jianLingZhenXiang = JianLingZhenXiang.DataList[type - 1];
		((UnityEventBase)ZhenXiangBtn.mouseUpEvent).RemoveAllListeners();
		bool flag = jianLingManager.IsZhenXiangUnlocked(jianLingZhenXiang.id);
		((Component)ZhenXiangBtn).gameObject.SetActive(flag);
		ZhenXiangBtnLock.gameObject.SetActive(!flag);
		if (flag)
		{
			ZhenXiangBtn.mouseUpEvent.AddListener(new UnityAction(ShowZhenXiang));
		}
		if (laoYeYeTalk)
		{
			for (int num2 = list.Count - 1; num2 >= 0; num2--)
			{
				if (list[num2].XianSuoLV < num)
				{
					list.RemoveAt(num2);
				}
			}
			if (list.Count <= 0)
			{
				return;
			}
			List<string> list2 = new List<string>();
			foreach (JianLingXianSuo item in list)
			{
				list2.Add(item.XianSuoDuiHua1);
				list2.Add(item.XianSuoDuiHua2);
			}
			int index = Random.Range(0, list2.Count);
			LaoYeYeSay(list2[index]);
		}
		else
		{
			LaoYeYeTalkObj.SetActive(false);
		}
	}

	public void ShowZhenXiang()
	{
		JianLingZhenXiang jianLingZhenXiang = JianLingZhenXiang.DataList[nowSelectedType - 1];
		RightText.text = jianLingZhenXiang.desc;
		((Component)ZhenXiangBtn).gameObject.SetActive(false);
		ZhenXiangBtnLock.gameObject.SetActive(false);
	}

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

	public void LaoYeYeSay(string msg)
	{
		string text = msg.ReplaceTalkWord();
		LaoYeYeTalkText.text = text;
		LaoYeYeTalkObj.SetActive(true);
	}

	bool IESCClose.TryEscClose()
	{
		Close();
		return true;
	}

	public void Close()
	{
		ESCCloseManager.Inst.UnRegisterClose(this);
		Object.Destroy((Object)(object)((Component)this).gameObject);
		UIJianLingPanel.OpenPanel();
	}
}
