using UnityEngine;

public class UIDongFu : MonoBehaviour
{
	public static UIDongFu Inst;

	public GameObject ScaleObj;

	public UILingTianPanel LingTian;

	public UIJuLingZhenPanel JuLingZhen;

	[HideInInspector]
	public DongFuData DongFu;

	private void Awake()
	{
		Inst = this;
	}

	public void InitData()
	{
		DongFu = new DongFuData(DongFuManager.NowDongFuID);
		DongFu.Load();
	}

	public void ShowLingTianPanel()
	{
		InitData();
		ScaleObj.SetActive(true);
		((Component)LingTian).gameObject.SetActive(true);
		LingTian.RefreshUI();
		ESCCloseManager.Inst.RegisterClose(LingTian);
	}

	public void HideLingTianPanel()
	{
		((Component)LingTian).gameObject.SetActive(false);
		ScaleObj.SetActive(false);
		ESCCloseManager.Inst.UnRegisterClose(LingTian);
	}

	public void ShowJuLingZhenPanel()
	{
		InitData();
		ScaleObj.SetActive(true);
		((Component)JuLingZhen).gameObject.SetActive(true);
		JuLingZhen.RefreshUI();
		ESCCloseManager.Inst.RegisterClose(JuLingZhen);
	}

	public void HideJuLingZhenPanel()
	{
		((Component)JuLingZhen).gameObject.SetActive(false);
		ScaleObj.SetActive(false);
		ESCCloseManager.Inst.UnRegisterClose(JuLingZhen);
	}
}
