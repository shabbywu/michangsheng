using GUIPackage;
using UltimateSurvival;
using UnityEngine;

public class TuJianMag : MonoBehaviour
{
	public showDanFang showDan;

	public showYaoCaiMag showyaoCaiMag;

	[SerializeField]
	private Canvas TuJianCanvas;

	private void Awake()
	{
		((Component)this).transform.parent = ((Component)UI_Manager.inst).gameObject.transform;
		TuJianCanvas.worldCamera = UI_Manager.inst.RootCamera;
		open();
	}

	private void Start()
	{
		showyaoCaiMag.AddItems();
	}

	public void close()
	{
		Tools.canClickFlag = true;
		UltimateSurvival.MonoSingleton<UI_Backgroud>.Instance.Value = false;
		PanelMamager.inst.closePanel(PanelMamager.PanelType.图鉴);
	}

	public void open()
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		Tools.canClickFlag = false;
		UltimateSurvival.MonoSingleton<UI_Backgroud>.Instance.Value = true;
		showDanFang();
		((Component)this).transform.localPosition = Vector3.zero;
		((Component)this).transform.localScale = new Vector3(0.74f, 0.74f, 0.74f);
	}

	public void showDanFang()
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		showDan.lianDanDanFang.InitDanFang();
		foreach (Transform item in ((Component)showDan.lianDanDanFang).transform)
		{
			showDanfangImage component = ((Component)item).GetComponent<showDanfangImage>();
			if (((Component)item).gameObject.activeSelf && (Object)(object)component != (Object)null)
			{
				component.click();
				break;
			}
		}
	}

	public void showCaiLiao()
	{
	}

	public void OnDestroy()
	{
		Tools.canClickFlag = true;
		UltimateSurvival.MonoSingleton<UI_Backgroud>.Instance.Value = false;
		PanelMamager.inst.closePanel(PanelMamager.PanelType.图鉴, 1);
	}
}
