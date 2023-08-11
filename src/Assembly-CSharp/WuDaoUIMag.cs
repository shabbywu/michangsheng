using System.Collections;
using System.Collections.Generic;
using Coffee.UIEffects;
using KBEngine;
using UltimateSurvival;
using UnityEngine;
using UnityEngine.UI;

public class WuDaoUIMag : MonoBehaviour
{
	public GameObject TypeCell;

	public static WuDaoUIMag inst;

	public List<Sprite> TuBiaoList = new List<Sprite>();

	public List<Sprite> BGList = new List<Sprite>();

	public List<UI2DSprite> BGs = new List<UI2DSprite>();

	public Text WuDaoDian;

	public Text ShiXu;

	[SerializeField]
	private GameObject wuDaoEmpty;

	public List<Sprite> Sprites_Lv = new List<Sprite>();

	public Slider slider;

	public Text shengyudianshu;

	public List<GameObject> WuDaoCententList;

	public List<Text> labelText;

	public WuDaoCellTooltip wuDaoCellTooltip;

	public WuDaoHelp wuDaoHelp1;

	public List<Sprite> IconBgSprite;

	public int NowType = -1;

	private void Awake()
	{
		inst = this;
	}

	private void OnDestroy()
	{
		inst = null;
	}

	private void Start()
	{
		init();
	}

	public void open()
	{
		UltimateSurvival.MonoSingleton<UI_Backgroud>.Instance.Value = true;
		((Component)this).gameObject.SetActive(true);
		init();
	}

	public void close()
	{
		UltimateSurvival.MonoSingleton<UI_Backgroud>.Instance.Value = false;
		((Component)this).gameObject.SetActive(false);
	}

	public void init()
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown
		foreach (Transform item in TypeCell.transform.parent)
		{
			Transform val = item;
			if (((Component)val).gameObject.activeSelf)
			{
				Object.Destroy((Object)(object)((Component)val).gameObject);
			}
		}
		int num = 0;
		bool flag = false;
		GameObject obj = null;
		wudaoTypeCell wudaocell = null;
		foreach (JSONObject item2 in jsonData.instance.WuDaoAllTypeJson.list)
		{
			GameObject val2 = Tools.InstantiateGameObject(TypeCell, TypeCell.transform.parent);
			wudaoTypeCell component = val2.GetComponent<wudaoTypeCell>();
			component.TitleName.text = Tools.Code64(item2["name"].str);
			if (!flag)
			{
				obj = val2;
				wudaocell = component;
				flag = true;
			}
			component.Type = item2["id"].I;
			if (num % 2 == 1)
			{
				component.ImageBG.gameObject.SetActive(false);
			}
			component.typeIcon.sprite = TuBiaoList[num];
			num++;
		}
		Tools.InstantiateGameObject(wuDaoEmpty, wuDaoEmpty.transform.parent);
		if (NowType != -1)
		{
			Avatar player = Tools.instance.getPlayer();
			wuDaoHelp1.text = string.Concat("[24a5d6]大道感悟：[-]通过领悟功法、神通能够提升你对大道的感悟。\n[24a5d6]触类旁通：[-]任意一系悟道达到融会贯通时，获得悟道点*1\n\n[ffe34b]当前进度:[-]", player.wuDaoMag.getWuDaoEx(NowType), "/", player.wuDaoMag.getNowTypeExMax(NowType));
		}
		setShengyuDianshu();
		ResetCellButton();
		((MonoBehaviour)this).StartCoroutine(selectDefault(obj, wudaocell));
	}

	private IEnumerator selectDefault(GameObject _obj, wudaoTypeCell wudaocell)
	{
		yield return (object)new WaitForSeconds(0.01f);
		_obj.GetComponentInChildren<Toggle>().isOn = true;
	}

	public static string getWuDaoTypeName(int type)
	{
		Avatar player = Tools.instance.getPlayer();
		return Tools.Code64(jsonData.instance.WuDaoJinJieJson[player.wuDaoMag.getWuDaoLevelByType(type).ToString()]["Text"].str);
	}

	public void ClearCenten()
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Expected O, but got Unknown
		foreach (GameObject wuDaoCentent in WuDaoCententList)
		{
			foreach (Transform item in wuDaoCentent.transform)
			{
				Transform val = item;
				if (((Component)val).gameObject.activeSelf)
				{
					Object.Destroy((Object)(object)((Component)val).gameObject);
				}
			}
		}
	}

	public void ResetCellButton()
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Expected O, but got Unknown
		foreach (GameObject wuDaoCentent in WuDaoCententList)
		{
			foreach (Transform item in wuDaoCentent.transform)
			{
				Transform val = item;
				if (!((Component)val).gameObject.activeSelf)
				{
					continue;
				}
				wuDaoUICell component = ((Component)val).gameObject.GetComponent<wuDaoUICell>();
				if (component.IsStudy())
				{
					((Component)component.bg).GetComponent<UIEffect>().effectFactor = 0f;
					((Component)component.icon).GetComponent<UIEffect>().effectFactor = 0f;
					((Component)component.bg).gameObject.SetActive(false);
					((Component)((Component)component.castNum).transform.parent).gameObject.SetActive(false);
					continue;
				}
				((Component)component.icon).GetComponent<UIEffect>().effectFactor = 0.85f;
				if (!component.CanStudyWuDao())
				{
					((Component)component.bg).gameObject.SetActive(false);
					((Component)((Component)component.castNum).transform.parent).gameObject.SetActive(false);
				}
				else
				{
					((Component)component.bg).gameObject.SetActive(true);
					((Component)((Component)component.castNum).transform.parent).gameObject.SetActive(true);
				}
			}
		}
		setShengyuDianshu();
	}

	public void setShengyuDianshu()
	{
	}

	public void ResetEx(int wudaoType)
	{
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		slider.value = Tools.instance.getPlayer().wuDaoMag.getWuDaoExPercent(wudaoType) / 100f;
		int num = 0;
		foreach (Text item in labelText)
		{
			_ = item;
			if (num + 1 > Tools.instance.getPlayer().wuDaoMag.getWuDaoLevelByType(wudaoType))
			{
				((Graphic)labelText[num]).color = new Color(188f, 180f, 175f);
			}
			else
			{
				((Graphic)labelText[num]).color = new Color(205f, 189f, 172f);
			}
			num++;
		}
	}

	public void upWuDaoDate()
	{
		WuDaoDian.text = Tools.instance.getPlayer().wuDaoMag.GetNowWuDaoDian().ToString();
		ShiXu.text = Tools.instance.getPlayer().LingGuang.list.Count.ToString();
	}

	private void Update()
	{
	}
}
