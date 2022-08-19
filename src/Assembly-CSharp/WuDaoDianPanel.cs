using System;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003A1 RID: 929
public class WuDaoDianPanel : MonoBehaviour, IESCClose
{
	// Token: 0x06001E54 RID: 7764 RVA: 0x000D59B4 File Offset: 0x000D3BB4
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

	// Token: 0x06001E55 RID: 7765 RVA: 0x000D5AD3 File Offset: 0x000D3CD3
	public void Hide()
	{
		ESCCloseManager.Inst.UnRegisterClose(this);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001E56 RID: 7766 RVA: 0x000D5AEC File Offset: 0x000D3CEC
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

	// Token: 0x06001E57 RID: 7767 RVA: 0x000D5B7A File Offset: 0x000D3D7A
	public bool TryEscClose()
	{
		this.Hide();
		return true;
	}

	// Token: 0x040018DE RID: 6366
	[SerializeField]
	private Image Slider;

	// Token: 0x040018DF RID: 6367
	[SerializeField]
	private Text curWuDaoDian;

	// Token: 0x040018E0 RID: 6368
	[SerializeField]
	private Text curWuDaoZhi;

	// Token: 0x040018E1 RID: 6369
	[SerializeField]
	private Text Tips;
}
