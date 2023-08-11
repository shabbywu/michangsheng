using GUIPackage;
using UnityEngine;

public class LianDanFinsh : MonoBehaviour
{
	public Inventory2 inventory2show;

	public GameObject SuccessPlan;

	public GameObject FailPlan;

	public UILabel text;

	public void close()
	{
		((Component)this).gameObject.SetActive(false);
	}

	public void Open()
	{
		((Component)this).gameObject.SetActive(true);
	}

	public void succes(int index, int num)
	{
		Open();
		text.text = Tools.Code64(jsonData.instance.LianDanSuccessItemLeiXin[index.ToString()]["desc"].str).Replace("{Num}", num.ToString());
		SuccessPlan.SetActive(true);
		FailPlan.SetActive(false);
	}

	public void fail(int index, int num)
	{
		Open();
		text.text = Tools.Code64(jsonData.instance.LianDanSuccessItemLeiXin[index.ToString()]["desc"].str).Replace("{Num}", num.ToString());
		SuccessPlan.SetActive(false);
		FailPlan.SetActive(true);
	}

	public void zhalue()
	{
		((Component)this).gameObject.SetActive(false);
	}

	public void SHowZhalu()
	{
		((Component)this).gameObject.SetActive(true);
	}

	private void Awake()
	{
		close();
	}

	private void Start()
	{
	}

	private void Update()
	{
	}
}
