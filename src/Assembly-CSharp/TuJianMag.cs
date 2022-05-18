using System;
using GUIPackage;
using UltimateSurvival;
using UnityEngine;

// Token: 0x02000630 RID: 1584
public class TuJianMag : MonoBehaviour
{
	// Token: 0x06002757 RID: 10071 RVA: 0x0001F323 File Offset: 0x0001D523
	private void Awake()
	{
		base.transform.parent = UI_Manager.inst.gameObject.transform;
		this.TuJianCanvas.worldCamera = UI_Manager.inst.RootCamera;
		this.open();
	}

	// Token: 0x06002758 RID: 10072 RVA: 0x0001F35A File Offset: 0x0001D55A
	private void Start()
	{
		this.showyaoCaiMag.AddItems();
	}

	// Token: 0x06002759 RID: 10073 RVA: 0x0001F367 File Offset: 0x0001D567
	public void close()
	{
		Tools.canClickFlag = true;
		UltimateSurvival.MonoSingleton<UI_Backgroud>.Instance.Value = false;
		PanelMamager.inst.closePanel(PanelMamager.PanelType.图鉴, 0);
	}

	// Token: 0x0600275A RID: 10074 RVA: 0x00133B1C File Offset: 0x00131D1C
	public void open()
	{
		Tools.canClickFlag = false;
		UltimateSurvival.MonoSingleton<UI_Backgroud>.Instance.Value = true;
		this.showDanFang();
		base.transform.localPosition = Vector3.zero;
		base.transform.localScale = new Vector3(0.74f, 0.74f, 0.74f);
	}

	// Token: 0x0600275B RID: 10075 RVA: 0x00133B70 File Offset: 0x00131D70
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

	// Token: 0x0600275C RID: 10076 RVA: 0x000042DD File Offset: 0x000024DD
	public void showCaiLiao()
	{
	}

	// Token: 0x0600275D RID: 10077 RVA: 0x0001F386 File Offset: 0x0001D586
	public void OnDestroy()
	{
		Tools.canClickFlag = true;
		UltimateSurvival.MonoSingleton<UI_Backgroud>.Instance.Value = false;
		PanelMamager.inst.closePanel(PanelMamager.PanelType.图鉴, 1);
	}

	// Token: 0x0400215D RID: 8541
	public showDanFang showDan;

	// Token: 0x0400215E RID: 8542
	public showYaoCaiMag showyaoCaiMag;

	// Token: 0x0400215F RID: 8543
	[SerializeField]
	private Canvas TuJianCanvas;
}
