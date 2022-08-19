using System;
using System.Diagnostics;
using System.IO;
using TMPro;
using UnityEngine;

// Token: 0x02000335 RID: 821
public class MainGongGaoUI : MonoBehaviour
{
	// Token: 0x06001C3F RID: 7231 RVA: 0x000CA504 File Offset: 0x000C8704
	private void Start()
	{
		if (this.show)
		{
			string text = "";
			if (this.TryGetGongGao(out text))
			{
				this.GongGaoText.text = text;
			}
			this.gonggaoPanel.SetActive(true);
		}
		else
		{
			this.gonggaoPanel.SetActive(false);
			this.CheckTimeZone();
		}
		if (PreloadManager.IsException)
		{
			UBigCheckBox.Show("<color=red>在初始化时出现异常！可能会影响到游戏正常运行，请检查是否有Mod冲突!</color>\n" + PreloadManager.ExceptionData, null);
			PreloadManager.IsException = false;
		}
	}

	// Token: 0x06001C40 RID: 7232 RVA: 0x000CA577 File Offset: 0x000C8777
	public void OpenGongGaoText()
	{
		Process.Start(Application.streamingAssetsPath + "/GongGao.txt");
	}

	// Token: 0x06001C41 RID: 7233 RVA: 0x000CA590 File Offset: 0x000C8790
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

	// Token: 0x06001C42 RID: 7234 RVA: 0x000CA5D4 File Offset: 0x000C87D4
	public void CloseGongGao()
	{
		this.gonggaoPanel.SetActive(false);
		this.CheckTimeZone();
	}

	// Token: 0x06001C43 RID: 7235 RVA: 0x000CA5E8 File Offset: 0x000C87E8
	public void CheckTimeZone()
	{
		try
		{
			TimeZoneInfo local = TimeZoneInfo.Local;
			if (local == null || local.Id == "")
			{
				selectBox.instence.setChoice("检测到您电脑中的注册表Time zones.reg被篡改，可能无法正常进行游戏，具体解决方法可以查看觅长生贴吧置顶帖或加入官方Q群。", new EventDelegate(new EventDelegate.Callback(Application.Quit)), new EventDelegate(new EventDelegate.Callback(Application.Quit)));
			}
		}
		catch (Exception ex)
		{
			selectBox.instence.setChoice("检测到您电脑中的注册表Time zones.reg被篡改，可能无法正常进行游戏，具体解决方法可以查看觅长生贴吧置顶帖或加入官方Q群。", new EventDelegate(new EventDelegate.Callback(Application.Quit)), new EventDelegate(new EventDelegate.Callback(Application.Quit)));
			Debug.Log(ex);
		}
	}

	// Token: 0x040016C3 RID: 5827
	[Tooltip("是否显示公告")]
	public bool show;

	// Token: 0x040016C4 RID: 5828
	[SerializeField]
	private TextMeshProUGUI GongGaoText;

	// Token: 0x040016C5 RID: 5829
	[SerializeField]
	private GameObject gonggaoPanel;
}
