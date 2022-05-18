using System;
using System.Collections.Generic;
using Fungus;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x020004F3 RID: 1267
public class SceneBtnMag : MonoBehaviour
{
	// Token: 0x060020F7 RID: 8439 RVA: 0x00114C80 File Offset: 0x00112E80
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

	// Token: 0x060020F8 RID: 8440 RVA: 0x0001B2E3 File Offset: 0x000194E3
	private void Start()
	{
		this.Init();
	}

	// Token: 0x060020F9 RID: 8441 RVA: 0x0001B2EB File Offset: 0x000194EB
	private void Update()
	{
		this.AutoHide();
	}

	// Token: 0x060020FA RID: 8442 RVA: 0x00114D4C File Offset: 0x00112F4C
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

	// Token: 0x060020FB RID: 8443 RVA: 0x00114DB8 File Offset: 0x00112FB8
	public void Init()
	{
		foreach (string text in this.btnName)
		{
			GameObject gameObject = GameObject.Find(text);
			if (gameObject != null && gameObject.GetComponentInChildren<Flowchart>() != null)
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

	// Token: 0x060020FC RID: 8444 RVA: 0x001151F4 File Offset: 0x001133F4
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

	// Token: 0x060020FD RID: 8445 RVA: 0x0001B2F3 File Offset: 0x000194F3
	public void openShop()
	{
		if (Tools.instance.canClick(false, true))
		{
			UIMenPaiShop.Inst.Show();
			UIMenPaiShop.Inst.RefreshUI();
		}
	}

	// Token: 0x04001C70 RID: 7280
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
		"ganying"
	};

	// Token: 0x04001C71 RID: 7281
	private Dictionary<string, FpBtn> btnDictionary = new Dictionary<string, FpBtn>();

	// Token: 0x04001C72 RID: 7282
	public static SceneBtnMag inst;

	// Token: 0x04001C73 RID: 7283
	[SerializeField]
	private Transform btnList;

	// Token: 0x04001C74 RID: 7284
	public static UnityAction FubenCaiJiAction;

	// Token: 0x04001C75 RID: 7285
	public static bool hasCaiJi;
}
