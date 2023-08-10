using UnityEngine;
using UnityEngine.UI;

public class SelectDanFangPingJie : MonoBehaviour
{
	[SerializeField]
	private GameObject Selected;

	[SerializeField]
	private GameObject Content;

	[SerializeField]
	private Image SelectedImage;

	public void openSelectPanel()
	{
		Content.SetActive(true);
		Selected.SetActive(false);
	}

	public void selectPingJie(int pinjie)
	{
		LianDanSystemManager.inst.DanFangPageManager.setPingJie((DanFangPageManager.DanFangPingJie)pinjie);
		SelectedImage.sprite = LianDanSystemManager.inst.DanFangPageManager.pingJieSprites[pinjie];
		Content.SetActive(false);
		Selected.SetActive(true);
	}
}
