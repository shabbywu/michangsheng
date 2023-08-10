using System.Collections.Generic;
using Fungus;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneBtnMag : MonoBehaviour
{
	private List<string> btnName = new List<string>
	{
		"likai", "caiji", "xiuxi", "biguan", "tupo", "shop", "kefang", "yaofang", "shenbingge", "chuhai",
		"shanglou", "liexi", "fubencaiji", "DFLingTian", "DFLianDan", "DFLianQi", "DFMode", "gaoshi", "chuansong", "xiuchuan",
		"dabi", "cangbaoge", "qixuan", "ganying", "zhuangban", "paifa", "jiaoyihui"
	};

	private Dictionary<string, FpBtn> btnDictionary = new Dictionary<string, FpBtn>();

	public static SceneBtnMag inst;

	[SerializeField]
	private Transform btnList;

	public static UnityAction FubenCaiJiAction;

	public static bool hasCaiJi;

	private void Awake()
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		((Component)this).transform.SetParent(((Component)NewUICanvas.Inst).gameObject.transform);
		((Component)this).transform.localScale = Vector3.one;
		((Component)this).transform.SetAsFirstSibling();
		((Component)((Component)this).transform).GetComponent<RectTransform>().offsetMin = new Vector2(0f, 0f);
		((Component)((Component)this).transform).GetComponent<RectTransform>().offsetMax = new Vector2(0f, 0f);
		inst = this;
		for (int i = 0; i < btnList.childCount; i++)
		{
			btnDictionary.Add(((Object)btnList.GetChild(i)).name, ((Component)btnList.GetChild(i)).GetComponent<FpBtn>());
		}
	}

	private void Start()
	{
		Init();
	}

	private void Update()
	{
		AutoHide();
	}

	private void AutoHide()
	{
		bool flag = true;
		if ((Object)(object)UINPCJiaoHu.Inst != (Object)null && (Object)(object)UINPCJiaoHu.Inst.JiaoHuPop != (Object)null && ((Component)UINPCJiaoHu.Inst.JiaoHuPop).gameObject.activeInHierarchy)
		{
			flag = false;
		}
		if (flag != ((Component)btnList).gameObject.activeSelf)
		{
			((Component)btnList).gameObject.SetActive(flag);
		}
	}

	public void Init()
	{
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0299: Expected O, but got Unknown
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Expected O, but got Unknown
		//IL_0308: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Expected O, but got Unknown
		//IL_0381: Unknown result type (might be due to invalid IL or missing references)
		//IL_038b: Expected O, but got Unknown
		//IL_03f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0401: Expected O, but got Unknown
		foreach (string item in btnName)
		{
			GameObject val = GameObject.Find(item);
			if (!((Object)(object)val != (Object)null) || !((Object)(object)val.GetComponentInChildren<Flowchart>() != (Object)null))
			{
				continue;
			}
			if (item == "jiaoyihui" && GlobalValue.Get(1020) != 1)
			{
				continue;
			}
			val.transform.localScale = Vector3.zero;
			Flowchart flowchat = val.GetComponentInChildren<Flowchart>();
			FpBtn btn = btnDictionary[item];
			if ((Object)(object)flowchat != (Object)null)
			{
				((UnityEventBase)btn.mouseUpEvent).RemoveAllListeners();
				btn.mouseUpEvent.AddListener((UnityAction)delegate
				{
					if (btn.IsInBtn && Tools.instance.canClick())
					{
						flowchat.ExecuteBlock("onClick");
					}
				});
				((Component)btn).gameObject.SetActive(true);
			}
			if (item == "tupo" && PlayerEx.Player.getLevelType() == 5)
			{
				SpriteRef component = ((Component)btn).GetComponent<SpriteRef>();
				((Component)btn).GetComponent<Image>().sprite = component.sprites[0];
				btn.nomalSprite = component.sprites[0];
				btn.mouseEnterSprite = component.sprites[1];
				btn.mouseDownSprite = component.sprites[2];
				btn.mouseUpSprite = component.sprites[0];
				btn.stopClickSprite = component.sprites[0];
				((Component)((Component)btn).transform.GetChild(0)).GetComponent<Image>().sprite = component.sprites[3];
			}
		}
		Scene activeScene = SceneManager.GetActiveScene();
		string scencName = ((Scene)(ref activeScene)).name;
		List<JSONObject> list = jsonData.instance.NomelShopJsonData.list.FindAll((JSONObject aa) => "S" + (int)aa["threeScene"].n == scencName);
		if (list.Count > 0)
		{
			if (list[0]["SType"].I == 1)
			{
				((UnityEventBase)btnDictionary["shop"].mouseUpEvent).RemoveAllListeners();
				btnDictionary["shop"].mouseUpEvent.AddListener(new UnityAction(openShop));
				((Component)btnDictionary["shop"]).gameObject.SetActive(true);
			}
			else if (list[0]["SType"].I == 2)
			{
				((UnityEventBase)btnDictionary["shenbingge"].mouseUpEvent).RemoveAllListeners();
				btnDictionary["shenbingge"].mouseUpEvent.AddListener(new UnityAction(openShop));
				((Component)btnDictionary["shenbingge"]).gameObject.SetActive(true);
			}
			else if (list[0]["SType"].I == 3)
			{
				((UnityEventBase)btnDictionary["yaofang"].mouseUpEvent).RemoveAllListeners();
				btnDictionary["yaofang"].mouseUpEvent.AddListener(new UnityAction(openShop));
				((Component)btnDictionary["yaofang"]).gameObject.SetActive(true);
			}
			else if (list[0]["SType"].I == 4)
			{
				((UnityEventBase)btnDictionary["cangbaoge"].mouseUpEvent).RemoveAllListeners();
				btnDictionary["cangbaoge"].mouseUpEvent.AddListener(new UnityAction(openShop));
				((Component)btnDictionary["cangbaoge"]).gameObject.SetActive(true);
			}
		}
		if (hasCaiJi)
		{
			hasCaiJi = false;
			SetFubenCaiJi(show: true, FubenCaiJiAction);
		}
	}

	public void SetFubenCaiJi(bool show, UnityAction clickAction = null)
	{
		if ((Object)(object)btnDictionary["fubencaiji"] != (Object)null)
		{
			((Component)btnDictionary["fubencaiji"]).gameObject.SetActive(show);
			((UnityEventBase)btnDictionary["fubencaiji"].mouseUpEvent).RemoveAllListeners();
			if (clickAction != null)
			{
				btnDictionary["fubencaiji"].mouseUpEvent.AddListener(clickAction);
			}
			((Component)btnDictionary["fubencaiji"]).transform.SetAsFirstSibling();
		}
	}

	public void openShop()
	{
		if (Tools.instance.canClick())
		{
			UIMenPaiShop.Inst.Show();
			UIMenPaiShop.Inst.RefreshUI();
		}
	}
}
