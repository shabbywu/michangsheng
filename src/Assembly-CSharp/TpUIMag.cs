using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200051A RID: 1306
public class TpUIMag : MonoBehaviour
{
	// Token: 0x0600219B RID: 8603 RVA: 0x00118758 File Offset: 0x00116958
	private void Awake()
	{
		base.transform.SetParent(NewUICanvas.Inst.gameObject.transform);
		base.transform.localScale = Vector3.one;
		base.transform.SetAsLastSibling();
		base.transform.GetComponent<RectTransform>().offsetMin = new Vector2(0f, 0f);
		base.transform.GetComponent<RectTransform>().offsetMax = new Vector2(0f, 0f);
		TpUIMag.inst = this;
	}

	// Token: 0x0600219C RID: 8604 RVA: 0x0001B9CC File Offset: 0x00019BCC
	public void Init()
	{
		this.tpPanel.SetActive(true);
		this.tpEatDanYao.gameObject.SetActive(false);
	}

	// Token: 0x0600219D RID: 8605 RVA: 0x0001B9EB File Offset: 0x00019BEB
	public void OpenEatDanYaoPanel()
	{
		this.tpPanel.SetActive(false);
		this.tpEatDanYao.Init();
	}

	// Token: 0x0600219E RID: 8606 RVA: 0x0001BA04 File Offset: 0x00019C04
	public void StartTupo()
	{
		this.call.Invoke();
		this.Close();
	}

	// Token: 0x0600219F RID: 8607 RVA: 0x0001BA17 File Offset: 0x00019C17
	public void GiveUp()
	{
		GlobalValue.SetTalk(1, 0, "TpUIMag.GiveUp");
		Tools.instance.monstarMag.monstarAddBuff = new Dictionary<int, int>();
		Tools.instance.monstarMag.HeroAddBuff = new Dictionary<int, int>();
		this.Close();
	}

	// Token: 0x060021A0 RID: 8608 RVA: 0x000111B3 File Offset: 0x0000F3B3
	private void Close()
	{
		Object.Destroy(base.gameObject);
	}

	// Token: 0x060021A1 RID: 8609 RVA: 0x0001BA53 File Offset: 0x00019C53
	public void ShowTuPoPanel()
	{
		this.tpPanel.SetActive(true);
	}

	// Token: 0x04001D1C RID: 7452
	public static TpUIMag inst;

	// Token: 0x04001D1D RID: 7453
	[SerializeField]
	private GameObject tpPanel;

	// Token: 0x04001D1E RID: 7454
	public TpEatDanYao tpEatDanYao;

	// Token: 0x04001D1F RID: 7455
	public UnityAction call;
}
