using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003C9 RID: 969
public class GongGaoMag : MonoBehaviour
{
	// Token: 0x06001FA2 RID: 8098 RVA: 0x000DF339 File Offset: 0x000DD539
	private void Start()
	{
		if (this.show && jsonData.showGongGao)
		{
			jsonData.showGongGao = false;
			this.showGongGao();
			return;
		}
		this.closeGongGao();
	}

	// Token: 0x06001FA3 RID: 8099 RVA: 0x000DF360 File Offset: 0x000DD560
	public void showGongGao()
	{
		this.GongGaoUI.SetActive(true);
		Transform transform = this.GongGaoUI.transform.Find("win/text");
		transform.transform.Find("Title").GetComponent<UILabel>().text = this.title;
		transform.transform.Find("Desc").GetComponent<UILabel>().text = this.desc;
		transform.transform.Find("Time").GetComponent<UILabel>().text = this.time;
	}

	// Token: 0x06001FA4 RID: 8100 RVA: 0x000DF3F0 File Offset: 0x000DD5F0
	public void closeGongGao()
	{
		this.GongGaoUI.SetActive(false);
		try
		{
			TimeZoneInfo local = TimeZoneInfo.Local;
			if (local == null || local.Id == "")
			{
				this.DebugLog();
			}
		}
		catch (Exception ex)
		{
			this.DebugLog();
			Debug.Log(ex);
		}
	}

	// Token: 0x06001FA5 RID: 8101 RVA: 0x000DF44C File Offset: 0x000DD64C
	public void DebugLog()
	{
		selectBox.instence.setChoice("检测到您电脑中的注册表Time zones.reg被篡改，可能无法正常进行游戏，具体解决方法可以查看觅长生贴吧置顶帖或加入官方Q群。", new EventDelegate(new EventDelegate.Callback(this.quitgame)), new EventDelegate(new EventDelegate.Callback(this.quitgame)));
	}

	// Token: 0x06001FA6 RID: 8102 RVA: 0x00049258 File Offset: 0x00047458
	public void quitgame()
	{
		Application.Quit();
	}

	// Token: 0x06001FA7 RID: 8103 RVA: 0x000DF47F File Offset: 0x000DD67F
	private void Update()
	{
		this.banben.text = "当前版本：" + Application.version;
	}

	// Token: 0x040019B4 RID: 6580
	[Tooltip("是否显示公告")]
	public bool show;

	// Token: 0x040019B5 RID: 6581
	[Tooltip("公告标题")]
	public string title;

	// Token: 0x040019B6 RID: 6582
	[Tooltip("公告文字")]
	[TextArea(5, 10)]
	public string desc;

	// Token: 0x040019B7 RID: 6583
	[Tooltip("公告时间")]
	public string time;

	// Token: 0x040019B8 RID: 6584
	public Text banben;

	// Token: 0x040019B9 RID: 6585
	public GameObject GongGaoUI;
}
