using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000393 RID: 915
public class TpUIMag : MonoBehaviour
{
	// Token: 0x06001E1C RID: 7708 RVA: 0x000D4B74 File Offset: 0x000D2D74
	private void Awake()
	{
		base.transform.SetParent(NewUICanvas.Inst.gameObject.transform);
		base.transform.localScale = Vector3.one;
		base.transform.SetAsLastSibling();
		base.transform.GetComponent<RectTransform>().offsetMin = new Vector2(0f, 0f);
		base.transform.GetComponent<RectTransform>().offsetMax = new Vector2(0f, 0f);
		TpUIMag.inst = this;
	}

	// Token: 0x06001E1D RID: 7709 RVA: 0x000D4BFA File Offset: 0x000D2DFA
	public void Init()
	{
		this.tpPanel.SetActive(true);
		this.tpEatDanYao.gameObject.SetActive(false);
	}

	// Token: 0x06001E1E RID: 7710 RVA: 0x000D4C19 File Offset: 0x000D2E19
	public void OpenEatDanYaoPanel()
	{
		this.tpPanel.SetActive(false);
		this.tpEatDanYao.Init();
	}

	// Token: 0x06001E1F RID: 7711 RVA: 0x000D4C32 File Offset: 0x000D2E32
	public void StartTupo()
	{
		this.call.Invoke();
		this.Close();
	}

	// Token: 0x06001E20 RID: 7712 RVA: 0x000D4C45 File Offset: 0x000D2E45
	public void GiveUp()
	{
		GlobalValue.SetTalk(1, 0, "TpUIMag.GiveUp");
		Tools.instance.monstarMag.monstarAddBuff = new Dictionary<int, int>();
		Tools.instance.monstarMag.HeroAddBuff = new Dictionary<int, int>();
		this.Close();
	}

	// Token: 0x06001E21 RID: 7713 RVA: 0x0005C928 File Offset: 0x0005AB28
	private void Close()
	{
		Object.Destroy(base.gameObject);
	}

	// Token: 0x06001E22 RID: 7714 RVA: 0x000D4C81 File Offset: 0x000D2E81
	public void ShowTuPoPanel()
	{
		this.tpPanel.SetActive(true);
	}

	// Token: 0x040018B8 RID: 6328
	public static TpUIMag inst;

	// Token: 0x040018B9 RID: 6329
	[SerializeField]
	private GameObject tpPanel;

	// Token: 0x040018BA RID: 6330
	public TpEatDanYao tpEatDanYao;

	// Token: 0x040018BB RID: 6331
	public UnityAction call;
}
