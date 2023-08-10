using KBEngine;
using UnityEngine;
using UnityEngine.UI;

public class ZhongLingLiManager : MonoBehaviour
{
	private float LingLiMax = 2160f;

	[SerializeField]
	private GameObject zhongLingLiRing;

	[SerializeField]
	private Text yunSuanFu;

	[SerializeField]
	private Text totalZongLingLi;

	[SerializeField]
	private Text lingWenXiaoGuo;

	public void init()
	{
		yunSuanFu.text = "";
		totalZongLingLi.text = "0";
		lingWenXiaoGuo.text = "";
		initZhongLingLiRing();
	}

	public void updateZhongLingLi()
	{
		updateZhongLingLiRing();
		totalZongLingLi.text = string.Concat((int)getTotalZongLingLi());
	}

	private void updateZhongLingLiRing()
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		float num = getTotalZongLingLi() / LingLiMax * 0.32f + 0.68f;
		zhongLingLiRing.transform.localScale = new Vector3(num, num, 1f);
	}

	public int getRealZongLingLi()
	{
		if (LianQiTotalManager.inst.putMaterialPageManager.wuWeiManager.checkWuWeiIsDaoBiao())
		{
			return LianQiTotalManager.inst.putMaterialPageManager.chuShiLingLiManager.getAllchuShiLingLi();
		}
		float wuWeiBaiFenBi = LianQiTotalManager.inst.putMaterialPageManager.wuWeiManager.getWuWeiBaiFenBi();
		return (int)((float)LianQiTotalManager.inst.putMaterialPageManager.chuShiLingLiManager.getAllchuShiLingLi() * wuWeiBaiFenBi);
	}

	public float getTotalZongLingLi()
	{
		Avatar player = Tools.instance.getPlayer();
		float num = getRealZongLingLi();
		int selectLingWenID = LianQiTotalManager.inst.putMaterialPageManager.lingWenManager.getSelectLingWenID();
		if (selectLingWenID == -1)
		{
			yunSuanFu.text = "";
			lingWenXiaoGuo.text = "";
		}
		else
		{
			JSONObject jSONObject = jsonData.instance.LianQiLingWenBiao[selectLingWenID.ToString()];
			float n = jSONObject["value4"].n;
			if (jSONObject["value3"].I == 1)
			{
				num *= jSONObject["value4"].n;
				yunSuanFu.text = "x";
			}
			else
			{
				num += n;
				yunSuanFu.text = "+";
			}
			lingWenXiaoGuo.text = n.ToString();
		}
		if (player.checkHasStudyWuDaoSkillByID(2241))
		{
			num *= 1.5f;
		}
		return num;
	}

	private void initZhongLingLiRing()
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		zhongLingLiRing.transform.localScale = new Vector3(0.68f, 0.68f, 1f);
	}
}
