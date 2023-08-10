using System;
using UnityEngine;
using UnityEngine.UI;

public class LingWenCell : MonoBehaviour
{
	[SerializeField]
	private GameObject daoSanJiao;

	[SerializeField]
	private GameObject fenGeXian;

	[SerializeField]
	private Text desc;

	public int lingWenID;

	public Action<string> clickCallBack;

	public Action<int, string> xuanWuBuffIDCallBack;

	public Action<int, int, string> xuanWuBuffSumCallBack;

	public int buffID;

	public int buffSum;

	public void showDaoSanJiao()
	{
		daoSanJiao.SetActive(true);
	}

	public void hideFenGeXian()
	{
		fenGeXian.SetActive(false);
	}

	public void setDesc(string str)
	{
		desc.text = Tools.Code64(str);
	}

	public void lingWenCiTiaoOnclick()
	{
		LianQiTotalManager.inst.putMaterialPageManager.lingWenManager.setSelectLinWenID(lingWenID);
		clickCallBack(desc.text);
	}

	public void buffIDOnclick()
	{
		xuanWuBuffIDCallBack(buffID, desc.text);
	}

	public void buffChengShu()
	{
		xuanWuBuffSumCallBack(buffID, buffSum, desc.text);
	}
}
