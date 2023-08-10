using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using XCharts;

public class CreatLinGen : MonoBehaviour
{
	public GameObject zizhiLabel;

	public List<int> createLingen = new List<int>();

	public RadarChart radarChart;

	private List<int> TuBiaoList = new List<int> { 0, 2, 3, 4, 1 };

	private void Start()
	{
	}

	public void resetLinGen()
	{
		for (int i = 0; i < createLingen.Count; i++)
		{
			createLingen[i] = 10;
		}
		List<int> list = new List<int> { 0, 1, 2, 3, 4 };
		for (int j = 0; j < CreateAvatarMag.inst.tianfuUI.LinGengZiZhi; j++)
		{
			int index = jsonData.instance.getRandom() % list.Count;
			createLingen[list[index]] = 20;
			list.Remove(list[index]);
		}
	}

	private void Update()
	{
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		int num = 0;
		float sum = 0f;
		createLingen.ForEach(delegate(int aa)
		{
			sum += aa;
		});
		if (sum == 0f)
		{
			return;
		}
		int num2 = createLingen.Max();
		foreach (Transform item in zizhiLabel.transform)
		{
			((Component)item).GetComponent<UILabel>().text = (int)((float)createLingen[num] / sum * 100f) + "%";
			UI2DSprite componentInChildren = ((Component)item).GetComponentInChildren<UI2DSprite>();
			if (createLingen[num] < num2)
			{
				componentInChildren.alpha = 0.5f;
				((Component)componentInChildren).transform.localScale = Vector3.one * 0.8f;
			}
			else
			{
				componentInChildren.alpha = 1f;
				((Component)componentInChildren).transform.localScale = Vector3.one * 1f;
			}
			((BaseChart)radarChart).series.list[0].data[0].data[num] = createLingen[TuBiaoList[num]];
			num++;
		}
		((BaseChart)radarChart).RefreshChart();
	}
}
