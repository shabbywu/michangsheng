using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020004A5 RID: 1189
public class MainGongGaoUI : MonoBehaviour
{
	// Token: 0x06001F8F RID: 8079 RVA: 0x0010F418 File Offset: 0x0010D618
	private void Start()
	{
		if (this.show)
		{
			this.titleText.text = this.title;
			this.content.text = this.desc;
			this.timeText.text = this.time;
			this.gonggaoPanel.SetActive(true);
			return;
		}
		this.gonggaoPanel.SetActive(false);
		this.CheckTimeZone();
	}

	// Token: 0x06001F90 RID: 8080 RVA: 0x0001A0EE File Offset: 0x000182EE
	public void CloseGongGao()
	{
		this.gonggaoPanel.SetActive(false);
		this.CheckTimeZone();
	}

	// Token: 0x06001F91 RID: 8081 RVA: 0x0010F480 File Offset: 0x0010D680
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

	// Token: 0x04001AF9 RID: 6905
	[Tooltip("是否显示公告")]
	public bool show;

	// Token: 0x04001AFA RID: 6906
	[Tooltip("公告标题")]
	public string title;

	// Token: 0x04001AFB RID: 6907
	[Tooltip("公告文字")]
	[TextArea(5, 10)]
	public string desc;

	// Token: 0x04001AFC RID: 6908
	[Tooltip("公告时间")]
	public string time;

	// Token: 0x04001AFD RID: 6909
	[SerializeField]
	private Text titleText;

	// Token: 0x04001AFE RID: 6910
	[SerializeField]
	private Text content;

	// Token: 0x04001AFF RID: 6911
	[SerializeField]
	private Text timeText;

	// Token: 0x04001B00 RID: 6912
	[SerializeField]
	private GameObject gonggaoPanel;
}
