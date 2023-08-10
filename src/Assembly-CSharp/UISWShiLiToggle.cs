using UnityEngine;
using UnityEngine.UI;

public class UISWShiLiToggle : MonoBehaviour
{
	public int ShiLiID;

	public string ShiLiName;

	public bool MoRenOn;

	private Toggle ShiLiToggle;

	private void Start()
	{
		ShiLiToggle = ((Component)this).GetComponent<Toggle>();
		if (MoRenOn)
		{
			ShiLiToggle.isOn = true;
			UIShengWangManager.Inst.NowShowShiLiID = ShiLiID;
			UIShengWangManager.Inst.NowShowShiLiName = ShiLiName;
		}
	}

	public void OnValueChanged(bool isOn)
	{
		if (isOn)
		{
			UIShengWangManager.Inst.NowShowShiLiID = ShiLiID;
			UIShengWangManager.Inst.NowShowShiLiName = ShiLiName;
			UIShengWangManager.Inst.SetShiLiInfo(ShiLiID, ShiLiName);
		}
	}
}
