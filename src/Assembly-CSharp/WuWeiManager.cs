using UnityEngine;
using UnityEngine.UI;

public class WuWeiManager : MonoBehaviour
{
	[SerializeField]
	private Text qinHe;

	[SerializeField]
	private Text caoKong;

	[SerializeField]
	private Text linxing;

	[SerializeField]
	private Text jianGuo;

	[SerializeField]
	private Text renXing;

	[SerializeField]
	private MyUIPolygon wuWeiRing;

	private float Max = 500f;

	private float Min = 2200f;

	public void init()
	{
		qinHe.text = "0";
		caoKong.text = "0";
		linxing.text = "0";
		jianGuo.text = "0";
		renXing.text = "0";
	}

	public void updateWuWei()
	{
		qinHe.text = getAllQingHe().ToString();
		caoKong.text = getAllCaoKong().ToString();
		linxing.text = getAllLinxing().ToString();
		jianGuo.text = getAllJianGuo().ToString();
		renXing.text = getAllRenXing().ToString();
		updateWuWeiRing();
	}

	private void updateWuWeiRing()
	{
		float num = 0.325f + getAllQingHe() / Max * 0.675f;
		float num2 = 0.325f + getAllCaoKong() / Max * 0.675f;
		float num3 = 0.325f + getAllLinxing() / Max * 0.675f;
		float num4 = 0.325f + getAllJianGuo() / Max * 0.675f;
		float num5 = 0.325f + getAllRenXing() / Max * 0.675f;
		wuWeiRing.VerticesDistances[0] = num;
		wuWeiRing.VerticesDistances[1] = num2;
		wuWeiRing.VerticesDistances[2] = num3;
		wuWeiRing.VerticesDistances[3] = num4;
		wuWeiRing.VerticesDistances[4] = num5;
		wuWeiRing.updateImage();
	}

	public bool checkWuWeiIsDaoBiao()
	{
		if (getAllWuWei() >= Min)
		{
			return true;
		}
		return false;
	}

	public bool checkIsHasWuWeiZero()
	{
		if (getAllQingHe() * getAllCaoKong() * getAllLinxing() * getAllJianGuo() * getAllRenXing() != 0f)
		{
			return false;
		}
		return true;
	}

	public float getAllWuWei()
	{
		return getAllQingHe() + getAllCaoKong() + getAllLinxing() + getAllJianGuo() + getAllRenXing();
	}

	public float getWuWeiBaiFenBi()
	{
		float num = getAllWuWei() / Min;
		if (num > 1f)
		{
			num = 1f;
		}
		return num;
	}

	private void initWuWeiRing()
	{
		for (int i = 0; i < 5; i++)
		{
			wuWeiRing.VerticesDistances[i] = 0.325f;
		}
		wuWeiRing.updateImage();
	}

	public float getAllQingHe()
	{
		float num = 0f;
		for (int i = 25; i <= 34; i++)
		{
			num += (float)LianQiTotalManager.inst.putMaterialPageManager.lianQiPageManager.GetCaiLiaoCellByName(i.ToString()).qinHe;
		}
		if (num > 500f)
		{
			num = 500f;
		}
		return num;
	}

	public float getAllCaoKong()
	{
		float num = 0f;
		for (int i = 25; i <= 34; i++)
		{
			num += (float)LianQiTotalManager.inst.putMaterialPageManager.lianQiPageManager.GetCaiLiaoCellByName(i.ToString()).caoKong;
		}
		if (num > 500f)
		{
			num = 500f;
		}
		return num;
	}

	public float getAllLinxing()
	{
		float num = 0f;
		for (int i = 25; i <= 34; i++)
		{
			num += (float)LianQiTotalManager.inst.putMaterialPageManager.lianQiPageManager.GetCaiLiaoCellByName(i.ToString()).linxing;
		}
		if (num > 500f)
		{
			num = 500f;
		}
		return num;
	}

	public float getAllJianGuo()
	{
		float num = 0f;
		for (int i = 25; i <= 34; i++)
		{
			num += (float)LianQiTotalManager.inst.putMaterialPageManager.lianQiPageManager.GetCaiLiaoCellByName(i.ToString()).jianGuo;
		}
		if (num > 500f)
		{
			num = 500f;
		}
		return num;
	}

	public float getAllRenXing()
	{
		float num = 0f;
		for (int i = 25; i <= 34; i++)
		{
			num += (float)LianQiTotalManager.inst.putMaterialPageManager.lianQiPageManager.GetCaiLiaoCellByName(i.ToString()).renXing;
		}
		if (num > 500f)
		{
			num = 500f;
		}
		return num;
	}
}
