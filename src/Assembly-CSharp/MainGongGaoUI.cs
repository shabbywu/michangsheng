using System;
using System.Diagnostics;
using System.IO;
using TMPro;
using UnityEngine;

public class MainGongGaoUI : MonoBehaviour
{
	[Tooltip("是否显示公告")]
	public bool show;

	[SerializeField]
	private TextMeshProUGUI GongGaoText;

	[SerializeField]
	private GameObject gonggaoPanel;

	private void Start()
	{
		if (show)
		{
			string text = "";
			if (TryGetGongGao(out text))
			{
				((TMP_Text)GongGaoText).text = text;
			}
			gonggaoPanel.SetActive(true);
		}
		else
		{
			gonggaoPanel.SetActive(false);
			CheckTimeZone();
		}
		if (PreloadManager.IsException)
		{
			UBigCheckBox.Show("<color=red>在初始化时出现异常！可能会影响到游戏正常运行，请检查是否有Mod冲突!</color>\n" + PreloadManager.ExceptionData);
			PreloadManager.IsException = false;
		}
	}

	public void OpenGongGaoText()
	{
		Process.Start(Application.streamingAssetsPath + "/GongGao.txt");
	}

	public bool TryGetGongGao(out string text)
	{
		text = "";
		try
		{
			text = File.ReadAllText(Application.streamingAssetsPath + "/GongGao.txt");
			return true;
		}
		catch
		{
		}
		return false;
	}

	public void CloseGongGao()
	{
		gonggaoPanel.SetActive(false);
		CheckTimeZone();
	}

	public void CheckTimeZone()
	{
		try
		{
			TimeZoneInfo local = TimeZoneInfo.Local;
			if (local == null || local.Id == "")
			{
				selectBox.instence.setChoice("检测到您电脑中的注册表Time zones.reg被篡改，可能无法正常进行游戏，具体解决方法可以查看觅长生贴吧置顶帖或加入官方Q群。", new EventDelegate((EventDelegate.Callback)Application.Quit), new EventDelegate((EventDelegate.Callback)Application.Quit));
			}
		}
		catch (Exception ex)
		{
			selectBox.instence.setChoice("检测到您电脑中的注册表Time zones.reg被篡改，可能无法正常进行游戏，具体解决方法可以查看觅长生贴吧置顶帖或加入官方Q群。", new EventDelegate((EventDelegate.Callback)Application.Quit), new EventDelegate((EventDelegate.Callback)Application.Quit));
			Debug.Log((object)ex);
		}
	}
}
