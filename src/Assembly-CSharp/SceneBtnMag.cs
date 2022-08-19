using System;
using System.Collections.Generic;
using Fungus;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x02000376 RID: 886
public class SceneBtnMag : MonoBehaviour
{
	// Token: 0x06001D92 RID: 7570 RVA: 0x000D0C94 File Offset: 0x000CEE94
	private void Awake()
	{
		base.transform.SetParent(NewUICanvas.Inst.gameObject.transform);
		base.transform.localScale = Vector3.one;
		base.transform.SetAsFirstSibling();
		base.transform.GetComponent<RectTransform>().offsetMin = new Vector2(0f, 0f);
		base.transform.GetComponent<RectTransform>().offsetMax = new Vector2(0f, 0f);
		SceneBtnMag.inst = this;
		for (int i = 0; i < this.btnList.childCount; i++)
		{
			this.btnDictionary.Add(this.btnList.GetChild(i).name, this.btnList.GetChild(i).GetComponent<FpBtn>());
		}
	}

	// Token: 0x06001D93 RID: 7571 RVA: 0x000D0D5D File Offset: 0x000CEF5D
	private void Start()
	{
		this.Init();
	}

	// Token: 0x06001D94 RID: 7572 RVA: 0x000D0D65 File Offset: 0x000CEF65
	private void Update()
	{
		this.AutoHide();
	}

	// Token: 0x06001D95 RID: 7573 RVA: 0x000D0D70 File Offset: 0x000CEF70
	private void AutoHide()
	{
		bool flag = true;
		if (UINPCJiaoHu.Inst != null && UINPCJiaoHu.Inst.JiaoHuPop != null && UINPCJiaoHu.Inst.JiaoHuPop.gameObject.activeInHierarchy)
		{
			flag = false;
		}
		if (flag != this.btnList.gameObject.activeSelf)
		{
			this.btnList.gameObject.SetActive(flag);
		}
	}

	// Token: 0x06001D96 RID: 7574 RVA: 0x000D0DDC File Offset: 0x000CEFDC
	public void Init()
	{
		foreach (string text in this.btnName)
		{
			GameObject gameObject = GameObject.Find(text);
			if (gameObject != null && gameObject.GetComponentInChildren<Flowchart>() != null)
			{
				if (!(text == "jiaoyihui") || GlobalValue.Get(1020, "unknow") == 1)
				{
					gameObject.transform.localScale = Vector3.zero;
					Flowchart flowchat = gameObject.GetComponentInChildren<Flowchart>();
					FpBtn btn = this.btnDictionary[text];
					if (flowchat != null)
					{
						btn.mouseUpEvent.RemoveAllListeners();
						btn.mouseUpEvent.AddListener(delegate()
						{
							if (btn.IsInBtn && Tools.instance.canClick(false, true))
							{
								flowchat.ExecuteBlock("onClick");
							}
						});
						btn.gameObject.SetActive(true);
					}
					if (text == "tupo" && PlayerEx.Player.getLevelType() == 5)
					{
						SpriteRef component = btn.GetComponent<SpriteRef>();
						btn.GetComponent<Image>().sprite = component.sprites[0];
						btn.nomalSprite = component.sprites[0];
						btn.mouseEnterSprite = component.sprites[1];
						btn.mouseDownSprite = component.sprites[2];
						btn.mouseUpSprite = component.sprites[0];
						btn.stopClickSprite = component.sprites[0];
						btn.transform.GetChild(0).GetComponent<Image>().sprite = component.sprites[3];
					}
				}
			}
		}
		string scencName = SceneManager.GetActiveScene().name;
		List<JSONObject> list = jsonData.instance.NomelShopJsonData.list.FindAll((JSONObject aa) => "S" + (int)aa["threeScene"].n == scencName);
		if (list.Count > 0)
		{
			if (list[0]["SType"].I == 1)
			{
				this.btnDictionary["shop"].mouseUpEvent.RemoveAllListeners();
				this.btnDictionary["shop"].mouseUpEvent.AddListener(new UnityAction(this.openShop));
				this.btnDictionary["shop"].gameObject.SetActive(true);
			}
			else if (list[0]["SType"].I == 2)
			{
				this.btnDictionary["shenbingge"].mouseUpEvent.RemoveAllListeners();
				this.btnDictionary["shenbingge"].mouseUpEvent.AddListener(new UnityAction(this.openShop));
				this.btnDictionary["shenbingge"].gameObject.SetActive(true);
			}
			else if (list[0]["SType"].I == 3)
			{
				this.btnDictionary["yaofang"].mouseUpEvent.RemoveAllListeners();
				this.btnDictionary["yaofang"].mouseUpEvent.AddListener(new UnityAction(this.openShop));
				this.btnDictionary["yaofang"].gameObject.SetActive(true);
			}
			else if (list[0]["SType"].I == 4)
			{
				this.btnDictionary["cangbaoge"].mouseUpEvent.RemoveAllListeners();
				this.btnDictionary["cangbaoge"].mouseUpEvent.AddListener(new UnityAction(this.openShop));
				this.btnDictionary["cangbaoge"].gameObject.SetActive(true);
			}
		}
		if (SceneBtnMag.hasCaiJi)
		{
			SceneBtnMag.hasCaiJi = false;
			this.SetFubenCaiJi(true, SceneBtnMag.FubenCaiJiAction);
		}
	}

	// Token: 0x06001D97 RID: 7575 RVA: 0x000D123C File Offset: 0x000CF43C
	public void SetFubenCaiJi(bool show, UnityAction clickAction = null)
	{
		if (this.btnDictionary["fubencaiji"] != null)
		{
			this.btnDictionary["fubencaiji"].gameObject.SetActive(show);
			this.btnDictionary["fubencaiji"].mouseUpEvent.RemoveAllListeners();
			if (clickAction != null)
			{
				this.btnDictionary["fubencaiji"].mouseUpEvent.AddListener(clickAction);
			}
			this.btnDictionary["fubencaiji"].transform.SetAsFirstSibling();
		}
	}

	// Token: 0x06001D98 RID: 7576 RVA: 0x000D12CE File Offset: 0x000CF4CE
	public void openShop()
	{
		if (Tools.instance.canClick(false, true))
		{
			UIMenPaiShop.Inst.Show();
			UIMenPaiShop.Inst.RefreshUI();
		}
	}

	// Token: 0x04001823 RID: 6179
	private List<string> btnName = new List<string>
	{
		"likai",
		"caiji",
		"xiuxi",
		"biguan",
		"tupo",
		"shop",
		"kefang",
		"yaofang",
		"shenbingge",
		"chuhai",
		"shanglou",
		"liexi",
		"fubencaiji",
		"DFLingTian",
		"DFLianDan",
		"DFLianQi",
		"DFMode",
		"gaoshi",
		"chuansong",
		"xiuchuan",
		"dabi",
		"cangbaoge",
		"qixuan",
		"ganying",
		"zhuangban",
		"paifa",
		"jiaoyihui"
	};

	// Token: 0x04001824 RID: 6180
	private Dictionary<string, FpBtn> btnDictionary = new Dictionary<string, FpBtn>();

	// Token: 0x04001825 RID: 6181
	public static SceneBtnMag inst;

	// Token: 0x04001826 RID: 6182
	[SerializeField]
	private Transform btnList;

	// Token: 0x04001827 RID: 6183
	public static UnityAction FubenCaiJiAction;

	// Token: 0x04001828 RID: 6184
	public static bool hasCaiJi;
}
