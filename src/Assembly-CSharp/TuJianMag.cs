using System;
using GUIPackage;
using UltimateSurvival;
using UnityEngine;

// Token: 0x02000473 RID: 1139
public class TuJianMag : MonoBehaviour
{
	// Token: 0x0600239E RID: 9118 RVA: 0x000F3AEA File Offset: 0x000F1CEA
	private void Awake()
	{
		base.transform.parent = UI_Manager.inst.gameObject.transform;
		this.TuJianCanvas.worldCamera = UI_Manager.inst.RootCamera;
		this.open();
	}

	// Token: 0x0600239F RID: 9119 RVA: 0x000F3B21 File Offset: 0x000F1D21
	private void Start()
	{
		this.showyaoCaiMag.AddItems();
	}

	// Token: 0x060023A0 RID: 9120 RVA: 0x000F3B2E File Offset: 0x000F1D2E
	public void close()
	{
		Tools.canClickFlag = true;
		UltimateSurvival.MonoSingleton<UI_Backgroud>.Instance.Value = false;
		PanelMamager.inst.closePanel(PanelMamager.PanelType.图鉴, 0);
	}

	// Token: 0x060023A1 RID: 9121 RVA: 0x000F3B50 File Offset: 0x000F1D50
	public void open()
	{
		Tools.canClickFlag = false;
		UltimateSurvival.MonoSingleton<UI_Backgroud>.Instance.Value = true;
		this.showDanFang();
		base.transform.localPosition = Vector3.zero;
		base.transform.localScale = new Vector3(0.74f, 0.74f, 0.74f);
	}

	// Token: 0x060023A2 RID: 9122 RVA: 0x000F3BA4 File Offset: 0x000F1DA4
	public void showDanFang()
	{
		this.showDan.lianDanDanFang.InitDanFang();
		foreach (object obj in this.showDan.lianDanDanFang.transform)
		{
			Transform transform = (Transform)obj;
			showDanfangImage component = transform.GetComponent<showDanfangImage>();
			if (transform.gameObject.activeSelf && component != null)
			{
				component.click();
				break;
			}
		}
	}

	// Token: 0x060023A3 RID: 9123 RVA: 0x00004095 File Offset: 0x00002295
	public void showCaiLiao()
	{
	}

	// Token: 0x060023A4 RID: 9124 RVA: 0x000F3C34 File Offset: 0x000F1E34
	public void OnDestroy()
	{
		Tools.canClickFlag = true;
		UltimateSurvival.MonoSingleton<UI_Backgroud>.Instance.Value = false;
		PanelMamager.inst.closePanel(PanelMamager.PanelType.图鉴, 1);
	}

	// Token: 0x04001C85 RID: 7301
	public showDanFang showDan;

	// Token: 0x04001C86 RID: 7302
	public showYaoCaiMag showyaoCaiMag;

	// Token: 0x04001C87 RID: 7303
	[SerializeField]
	private Canvas TuJianCanvas;
}
