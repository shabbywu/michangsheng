using System;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200052A RID: 1322
public class WuDaoDianPanel : MonoBehaviour, IESCClose
{
	// Token: 0x060021D5 RID: 8661 RVA: 0x001192BC File Offset: 0x001174BC
	public void Show()
	{
		ESCCloseManager.Inst.RegisterClose(this);
		Avatar player = Tools.instance.getPlayer();
		this.curWuDaoDian.text = player.wuDaoMag.GetNowWuDaoDian().ToString();
		int wuDaoZhiLevel = player.WuDaoZhiLevel;
		int i = jsonData.instance.WuDaoZhiData[wuDaoZhiLevel.ToString()]["LevelUpExp"].I;
		int wuDaoZhi = player.WuDaoZhi;
		if (wuDaoZhi > i)
		{
			this.curWuDaoZhi.text = string.Format("({0}/上限)", player.WuDaoZhi);
			this.Slider.fillAmount = 1f;
		}
		else
		{
			this.curWuDaoZhi.text = string.Format("({0}/{1})", player.WuDaoZhi, i);
			this.Slider.fillAmount = (float)wuDaoZhi / (float)i;
		}
		this.Tips.SetText(string.Format("悟道值满后，会获得悟道点x1,已获得（{0}/{1}）", player.WuDaoZhiLevel, WuDaoZhiData.DataList.Count - 1));
		base.gameObject.SetActive(true);
	}

	// Token: 0x060021D6 RID: 8662 RVA: 0x0001BCA3 File Offset: 0x00019EA3
	public void Hide()
	{
		ESCCloseManager.Inst.UnRegisterClose(this);
		base.gameObject.SetActive(false);
	}

	// Token: 0x060021D7 RID: 8663 RVA: 0x00109CA0 File Offset: 0x00107EA0
	private int GetPlayerLevel(int curLevel, int curExp, ref int endExp)
	{
		int i = jsonData.instance.WuDaoZhiData[curLevel.ToString()]["LevelUpExp"].I;
		int num = curLevel;
		while (curExp >= i)
		{
			num++;
			if (!jsonData.instance.WuDaoZhiData.HasField(num.ToString()))
			{
				num--;
				break;
			}
			curExp -= i;
			i = jsonData.instance.WuDaoZhiData[num.ToString()]["LevelUpExp"].I;
		}
		endExp = curExp;
		return num;
	}

	// Token: 0x060021D8 RID: 8664 RVA: 0x0001BCBC File Offset: 0x00019EBC
	public bool TryEscClose()
	{
		this.Hide();
		return true;
	}

	// Token: 0x04001D47 RID: 7495
	[SerializeField]
	private Image Slider;

	// Token: 0x04001D48 RID: 7496
	[SerializeField]
	private Text curWuDaoDian;

	// Token: 0x04001D49 RID: 7497
	[SerializeField]
	private Text curWuDaoZhi;

	// Token: 0x04001D4A RID: 7498
	[SerializeField]
	private Text Tips;
}
