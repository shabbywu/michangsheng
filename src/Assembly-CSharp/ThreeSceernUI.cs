using System;
using System.Collections.Generic;
using Fungus;
using GUIPackage;
using UnityEngine;

// Token: 0x02000428 RID: 1064
public class ThreeSceernUI : MonoBehaviour
{
	// Token: 0x060021FC RID: 8700 RVA: 0x000EA151 File Offset: 0x000E8351
	private void Awake()
	{
		ThreeSceernUI.inst = this;
	}

	// Token: 0x060021FD RID: 8701 RVA: 0x000EA159 File Offset: 0x000E8359
	public void init()
	{
		if (SceneBtnMag.inst == null)
		{
			Object.Instantiate<GameObject>(ResManager.inst.LoadPrefab("SceneBtnUI"));
		}
	}

	// Token: 0x060021FE RID: 8702 RVA: 0x000EA17D File Offset: 0x000E837D
	private void Start()
	{
		this.init();
	}

	// Token: 0x060021FF RID: 8703 RVA: 0x000EA188 File Offset: 0x000E8388
	public void openShop()
	{
		GameObject gameObject = Object.Instantiate<GameObject>(ResManager.inst.LoadPrefab("Shop"), UI_Manager.inst.gameObject.transform);
		gameObject.transform.localPosition = Vector3.zero;
		gameObject.transform.localScale = new Vector3(0.75f, 0.75f, 0f);
	}

	// Token: 0x06002200 RID: 8704 RVA: 0x000EA1E8 File Offset: 0x000E83E8
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

	// Token: 0x06002201 RID: 8705 RVA: 0x000EA2E0 File Offset: 0x000E84E0
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

	// Token: 0x06002202 RID: 8706 RVA: 0x000EA3B4 File Offset: 0x000E85B4
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

	// Token: 0x06002203 RID: 8707 RVA: 0x000EA480 File Offset: 0x000E8680
	private void OnDestroy()
	{
		ThreeSceernUI.inst = null;
		if (SceneBtnMag.inst != null)
		{
			Object.Destroy(SceneBtnMag.inst.gameObject);
		}
	}

	// Token: 0x06002204 RID: 8708 RVA: 0x00004095 File Offset: 0x00002295
	private void Update()
	{
	}

	// Token: 0x04001B67 RID: 7015
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

	// Token: 0x04001B68 RID: 7016
	public int showBtnNum;

	// Token: 0x04001B69 RID: 7017
	public static ThreeSceernUI inst;

	// Token: 0x04001B6A RID: 7018
	public List<GameObject> btnlist;

	// Token: 0x04001B6B RID: 7019
	public int startIndex;

	// Token: 0x04001B6C RID: 7020
	[SerializeField]
	private UIWidget uIWidget;
}
