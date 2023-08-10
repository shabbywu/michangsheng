using UnityEngine;
using UnityEngine.UI;

public class ChuShiLingLiManager : MonoBehaviour
{
	[SerializeField]
	private Image ChushiLingRing;

	[SerializeField]
	private Text chuShiLingLi;

	private float Max = 480f;

	public void init()
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		chuShiLingLi.text = "0";
		((Component)ChushiLingRing).transform.localScale = new Vector3(0.475f, 0.475f, 1f);
	}

	public void updateChushiLingLi()
	{
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		chuShiLingLi.text = getAllchuShiLingLi().ToString();
		float num = 0.475f + 0.525f * (float)getAllchuShiLingLi() / Max;
		((Component)ChushiLingRing).transform.localScale = new Vector3(num, num, 1f);
	}

	public int getAllchuShiLingLi()
	{
		int num = 0;
		for (int i = 25; i <= 34; i++)
		{
			num += LianQiTotalManager.inst.putMaterialPageManager.lianQiPageManager.GetCaiLiaoCellByName(i.ToString()).lingLi;
		}
		return num;
	}
}
