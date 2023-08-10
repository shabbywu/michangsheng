using System;
using UnityEngine;
using UnityEngine.UI;

public class GongGaoMag : MonoBehaviour
{
	[Tooltip("是否显示公告")]
	public bool show;

	[Tooltip("公告标题")]
	public string title;

	[Tooltip("公告文字")]
	[TextArea(5, 10)]
	public string desc;

	[Tooltip("公告时间")]
	public string time;

	public Text banben;

	public GameObject GongGaoUI;

	private void Start()
	{
		if (show && jsonData.showGongGao)
		{
			jsonData.showGongGao = false;
			showGongGao();
		}
		else
		{
			closeGongGao();
		}
	}

	public void showGongGao()
	{
		GongGaoUI.SetActive(true);
		Transform obj = GongGaoUI.transform.Find("win/text");
		((Component)((Component)obj).transform.Find("Title")).GetComponent<UILabel>().text = title;
		((Component)((Component)obj).transform.Find("Desc")).GetComponent<UILabel>().text = desc;
		((Component)((Component)obj).transform.Find("Time")).GetComponent<UILabel>().text = time;
	}

	public void closeGongGao()
	{
		GongGaoUI.SetActive(false);
		try
		{
			TimeZoneInfo local = TimeZoneInfo.Local;
			if (local == null || local.Id == "")
			{
				DebugLog();
			}
		}
		catch (Exception ex)
		{
			DebugLog();
			Debug.Log((object)ex);
		}
	}

	public void DebugLog()
	{
		selectBox.instence.setChoice("检测到您电脑中的注册表Time zones.reg被篡改，可能无法正常进行游戏，具体解决方法可以查看觅长生贴吧置顶帖或加入官方Q群。", new EventDelegate(quitgame), new EventDelegate(quitgame));
	}

	public void quitgame()
	{
		Application.Quit();
	}

	private void Update()
	{
		banben.text = "当前版本：" + Application.version;
	}
}
