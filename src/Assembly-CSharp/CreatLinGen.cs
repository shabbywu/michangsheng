using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using XCharts;

// Token: 0x0200059E RID: 1438
public class CreatLinGen : MonoBehaviour
{
	// Token: 0x0600244A RID: 9290 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x0600244B RID: 9291 RVA: 0x00127E10 File Offset: 0x00126010
	public void resetLinGen()
	{
		for (int i = 0; i < this.createLingen.Count; i++)
		{
			this.createLingen[i] = 10;
		}
		List<int> list = new List<int>
		{
			0,
			1,
			2,
			3,
			4
		};
		for (int j = 0; j < CreateAvatarMag.inst.tianfuUI.LinGengZiZhi; j++)
		{
			int index = jsonData.instance.getRandom() % list.Count;
			this.createLingen[list[index]] = 20;
			list.Remove(list[index]);
		}
	}

	// Token: 0x0600244C RID: 9292 RVA: 0x00127EB8 File Offset: 0x001260B8
	private void Update()
	{
		int num = 0;
		float sum = 0f;
		this.createLingen.ForEach(delegate(int aa)
		{
			sum += (float)aa;
		});
		if (sum == 0f)
		{
			return;
		}
		int num2 = this.createLingen.Max();
		foreach (object obj in this.zizhiLabel.transform)
		{
			Transform transform = (Transform)obj;
			transform.GetComponent<UILabel>().text = (int)((float)this.createLingen[num] / sum * 100f) + "%";
			UI2DSprite componentInChildren = transform.GetComponentInChildren<UI2DSprite>();
			if (this.createLingen[num] < num2)
			{
				componentInChildren.alpha = 0.5f;
				componentInChildren.transform.localScale = Vector3.one * 0.8f;
			}
			else
			{
				componentInChildren.alpha = 1f;
				componentInChildren.transform.localScale = Vector3.one * 1f;
			}
			this.radarChart.series.list[0].data[0].data[num] = (float)this.createLingen[this.TuBiaoList[num]];
			num++;
		}
		this.radarChart.RefreshChart();
	}

	// Token: 0x04001F38 RID: 7992
	public GameObject zizhiLabel;

	// Token: 0x04001F39 RID: 7993
	public List<int> createLingen = new List<int>();

	// Token: 0x04001F3A RID: 7994
	public RadarChart radarChart;

	// Token: 0x04001F3B RID: 7995
	private List<int> TuBiaoList = new List<int>
	{
		0,
		2,
		3,
		4,
		1
	};
}
