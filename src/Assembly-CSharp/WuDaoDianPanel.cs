using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

public class WuDaoDianPanel : MonoBehaviour, IESCClose
{
	[SerializeField]
	private Image Slider;

	[SerializeField]
	private Text curWuDaoDian;

	[SerializeField]
	private Text curWuDaoZhi;

	[SerializeField]
	private Text Tips;

	public void Show()
	{
		ESCCloseManager.Inst.RegisterClose(this);
		Avatar player = Tools.instance.getPlayer();
		curWuDaoDian.text = player.wuDaoMag.GetNowWuDaoDian().ToString();
		int wuDaoZhiLevel = player.WuDaoZhiLevel;
		int i = jsonData.instance.WuDaoZhiData[wuDaoZhiLevel.ToString()]["LevelUpExp"].I;
		int wuDaoZhi = player.WuDaoZhi;
		if (wuDaoZhi > i)
		{
			curWuDaoZhi.text = $"({player.WuDaoZhi}/上限)";
			Slider.fillAmount = 1f;
		}
		else
		{
			curWuDaoZhi.text = $"({player.WuDaoZhi}/{i})";
			Slider.fillAmount = (float)wuDaoZhi / (float)i;
		}
		Tips.SetText($"悟道值满后，会获得悟道点x1,已获得（{player.WuDaoZhiLevel}/{WuDaoZhiData.DataList.Count - 1}）");
		((Component)this).gameObject.SetActive(true);
	}

	public void Hide()
	{
		ESCCloseManager.Inst.UnRegisterClose(this);
		((Component)this).gameObject.SetActive(false);
	}

	private int GetPlayerLevel(int curLevel, int curExp, ref int endExp)
	{
		int i = jsonData.instance.WuDaoZhiData[curLevel.ToString()]["LevelUpExp"].I;
		int num = curLevel;
		while (curExp >= i)
		{
			num++;
			if (jsonData.instance.WuDaoZhiData.HasField(num.ToString()))
			{
				curExp -= i;
				i = jsonData.instance.WuDaoZhiData[num.ToString()]["LevelUpExp"].I;
				continue;
			}
			num--;
			break;
		}
		endExp = curExp;
		return num;
	}

	public bool TryEscClose()
	{
		Hide();
		return true;
	}
}
