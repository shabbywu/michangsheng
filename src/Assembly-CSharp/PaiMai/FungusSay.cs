using Fungus;
using UnityEngine;

namespace PaiMai;

public class FungusSay : MonoBehaviour
{
	[SerializeField]
	private Flowchart _flowchart;

	public void Say(PaiMaiSayData sayData)
	{
		((Component)this).gameObject.SetActive(true);
		if (sayData.Action == null)
		{
			sayData.Action = Hide;
		}
		BindData.Bind("PaiMaiSayData", sayData);
		_flowchart.StopBlock("SayWord");
		_flowchart.ExecuteBlock("SayWord");
	}

	private void Hide()
	{
		((Component)this).gameObject.SetActive(false);
	}
}
