using System.Collections.Generic;
using Fungus;
using GUIPackage;
using UnityEngine;

public class ThreeSceernUI : MonoBehaviour
{
	private List<string> btnName = new List<string>
	{
		"likai", "caiji", "xiuxi", "biguan", "tupo", "shop", "kefang", "ui8", "yaofang", "shenbingge",
		"wudao", "chuhai", "shanglou", "liexi"
	};

	public int showBtnNum;

	public static ThreeSceernUI inst;

	public List<GameObject> btnlist;

	public int startIndex;

	[SerializeField]
	private UIWidget uIWidget;

	private void Awake()
	{
		inst = this;
	}

	public void init()
	{
		if ((Object)(object)SceneBtnMag.inst == (Object)null)
		{
			Object.Instantiate<GameObject>(ResManager.inst.LoadPrefab("SceneBtnUI"));
		}
	}

	private void Start()
	{
		init();
	}

	public void openShop()
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		GameObject obj = Object.Instantiate<GameObject>(ResManager.inst.LoadPrefab("Shop"), ((Component)UI_Manager.inst).gameObject.transform);
		obj.transform.localPosition = Vector3.zero;
		obj.transform.localScale = new Vector3(0.75f, 0.75f, 0f);
	}

	public void addBtn()
	{
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		Transform val = ((Component)this).transform.Find("grid");
		int num = 0;
		foreach (string item in btnName)
		{
			GameObject val2 = GameObject.Find(item);
			if ((Object)(object)val2 != (Object)null && (Object)(object)val2.GetComponentInChildren<Flowchart>() != (Object)null && val.childCount > num)
			{
				val2.transform.localScale = Vector3.zero;
				Flowchart flowchat = val2.GetComponentInChildren<Flowchart>();
				if ((Object)(object)flowchat != (Object)null)
				{
					Transform child = val.GetChild(num);
					((Component)child).GetComponentInChildren<UIButton>().onClick.Add(new EventDelegate(delegate
					{
						if (Tools.instance.canClick())
						{
							flowchat.ExecuteBlock("onClick");
						}
					}));
					setPostion(child);
				}
			}
			num++;
		}
	}

	public void setPostion(Transform chilidf)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		((Component)chilidf).gameObject.transform.localPosition = new Vector3(300f, (float)(137 * showBtnNum), 0f);
		iTween.MoveTo(((Component)chilidf).gameObject, iTween.Hash(new object[10]
		{
			"x",
			0,
			"y",
			137f * (float)showBtnNum,
			"z",
			0,
			"time",
			2f + (float)showBtnNum * 1f,
			"islocal",
			true
		}));
		showBtnNum++;
	}

	public void setPos()
	{
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		Transform val = ((Component)this).transform.Find("grid");
		int num = 0;
		int num2 = 0;
		foreach (string item in btnName)
		{
			if ((Object)(object)GameObject.Find(item) != (Object)null && val.childCount > num)
			{
				Transform child = val.GetChild(num);
				((Component)child).transform.localPosition = new Vector3(0f, 0f, 0f);
				((Component)((Component)child).GetComponentInChildren<UIButton>()).transform.localPosition = new Vector3(0f, (float)(137 * num2), 0f);
				num2++;
			}
			num++;
		}
	}

	private void OnDestroy()
	{
		inst = null;
		if ((Object)(object)SceneBtnMag.inst != (Object)null)
		{
			Object.Destroy((Object)(object)((Component)SceneBtnMag.inst).gameObject);
		}
	}

	private void Update()
	{
	}
}
