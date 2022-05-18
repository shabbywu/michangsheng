using System;
using System.Collections.Generic;
using Fungus;
using GUIPackage;
using UnityEngine;

// Token: 0x020005DD RID: 1501
public class ThreeSceernUI : MonoBehaviour
{
	// Token: 0x060025B6 RID: 9654 RVA: 0x0001E304 File Offset: 0x0001C504
	private void Awake()
	{
		ThreeSceernUI.inst = this;
	}

	// Token: 0x060025B7 RID: 9655 RVA: 0x0001E30C File Offset: 0x0001C50C
	public void init()
	{
		if (SceneBtnMag.inst == null)
		{
			Object.Instantiate<GameObject>(ResManager.inst.LoadPrefab("SceneBtnUI"));
		}
	}

	// Token: 0x060025B8 RID: 9656 RVA: 0x0001E330 File Offset: 0x0001C530
	private void Start()
	{
		this.init();
	}

	// Token: 0x060025B9 RID: 9657 RVA: 0x0012B52C File Offset: 0x0012972C
	public void openShop()
	{
		GameObject gameObject = Object.Instantiate<GameObject>(ResManager.inst.LoadPrefab("Shop"), UI_Manager.inst.gameObject.transform);
		gameObject.transform.localPosition = Vector3.zero;
		gameObject.transform.localScale = new Vector3(0.75f, 0.75f, 0f);
	}

	// Token: 0x060025BA RID: 9658 RVA: 0x0012B58C File Offset: 0x0012978C
	public void addBtn()
	{
		Transform transform = base.transform.Find("grid");
		int num = 0;
		foreach (string text in this.btnName)
		{
			GameObject gameObject = GameObject.Find(text);
			if (gameObject != null && gameObject.GetComponentInChildren<Flowchart>() != null && transform.childCount > num)
			{
				gameObject.transform.localScale = Vector3.zero;
				Flowchart flowchat = gameObject.GetComponentInChildren<Flowchart>();
				if (flowchat != null)
				{
					Transform child = transform.GetChild(num);
					child.GetComponentInChildren<UIButton>().onClick.Add(new EventDelegate(delegate()
					{
						if (Tools.instance.canClick(false, true))
						{
							flowchat.ExecuteBlock("onClick");
						}
					}));
					this.setPostion(child);
				}
			}
			num++;
		}
	}

	// Token: 0x060025BB RID: 9659 RVA: 0x0012B684 File Offset: 0x00129884
	public void setPostion(Transform chilidf)
	{
		chilidf.gameObject.transform.localPosition = new Vector3(300f, (float)(137 * this.showBtnNum), 0f);
		iTween.MoveTo(chilidf.gameObject, iTween.Hash(new object[]
		{
			"x",
			0,
			"y",
			137f * (float)this.showBtnNum,
			"z",
			0,
			"time",
			2f + (float)this.showBtnNum * 1f,
			"islocal",
			true
		}));
		this.showBtnNum++;
	}

	// Token: 0x060025BC RID: 9660 RVA: 0x0012B758 File Offset: 0x00129958
	public void setPos()
	{
		Transform transform = base.transform.Find("grid");
		int num = 0;
		int num2 = 0;
		using (List<string>.Enumerator enumerator = this.btnName.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (GameObject.Find(enumerator.Current) != null && transform.childCount > num)
				{
					Transform child = transform.GetChild(num);
					child.transform.localPosition = new Vector3(0f, 0f, 0f);
					child.GetComponentInChildren<UIButton>().transform.localPosition = new Vector3(0f, (float)(137 * num2), 0f);
					num2++;
				}
				num++;
			}
		}
	}

	// Token: 0x060025BD RID: 9661 RVA: 0x0001E338 File Offset: 0x0001C538
	private void OnDestroy()
	{
		ThreeSceernUI.inst = null;
		if (SceneBtnMag.inst != null)
		{
			Object.Destroy(SceneBtnMag.inst.gameObject);
		}
	}

	// Token: 0x060025BE RID: 9662 RVA: 0x000042DD File Offset: 0x000024DD
	private void Update()
	{
	}

	// Token: 0x04002030 RID: 8240
	private List<string> btnName = new List<string>
	{
		"likai",
		"caiji",
		"xiuxi",
		"biguan",
		"tupo",
		"shop",
		"kefang",
		"ui8",
		"yaofang",
		"shenbingge",
		"wudao",
		"chuhai",
		"shanglou",
		"liexi"
	};

	// Token: 0x04002031 RID: 8241
	public int showBtnNum;

	// Token: 0x04002032 RID: 8242
	public static ThreeSceernUI inst;

	// Token: 0x04002033 RID: 8243
	public List<GameObject> btnlist;

	// Token: 0x04002034 RID: 8244
	public int startIndex;

	// Token: 0x04002035 RID: 8245
	[SerializeField]
	private UIWidget uIWidget;
}
