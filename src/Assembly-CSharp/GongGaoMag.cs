using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200055F RID: 1375
public class GongGaoMag : MonoBehaviour
{
	// Token: 0x0600231C RID: 8988 RVA: 0x0001C8D8 File Offset: 0x0001AAD8
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

	// Token: 0x0600231D RID: 8989 RVA: 0x00121EE4 File Offset: 0x001200E4
	public void showGongGao()
	{
		this.GongGaoUI.SetActive(true);
		Transform transform = this.GongGaoUI.transform.Find("win/text");
		transform.transform.Find("Title").GetComponent<UILabel>().text = this.title;
		transform.transform.Find("Desc").GetComponent<UILabel>().text = this.desc;
		transform.transform.Find("Time").GetComponent<UILabel>().text = this.time;
	}

	// Token: 0x0600231E RID: 8990 RVA: 0x00121F74 File Offset: 0x00120174
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

	// Token: 0x0600231F RID: 8991 RVA: 0x0001C8FC File Offset: 0x0001AAFC
	public void DebugLog()
	{
		selectBox.instence.setChoice("检测到您电脑中的注册表Time zones.reg被篡改，可能无法正常进行游戏，具体解决方法可以查看觅长生贴吧置顶帖或加入官方Q群。", new EventDelegate(new EventDelegate.Callback(this.quitgame)), new EventDelegate(new EventDelegate.Callback(this.quitgame)));
	}

	// Token: 0x06002320 RID: 8992 RVA: 0x0000EF72 File Offset: 0x0000D172
	public void quitgame()
	{
		Application.Quit();
	}

	// Token: 0x06002321 RID: 8993 RVA: 0x0001C92F File Offset: 0x0001AB2F
	private void Update()
	{
		this.banben.text = "当前版本：" + Application.version;
	}

	// Token: 0x04001E38 RID: 7736
	[Tooltip("是否显示公告")]
	public bool show;

	// Token: 0x04001E39 RID: 7737
	[Tooltip("公告标题")]
	public string title;

	// Token: 0x04001E3A RID: 7738
	[Tooltip("公告文字")]
	[TextArea(5, 10)]
	public string desc;

	// Token: 0x04001E3B RID: 7739
	[Tooltip("公告时间")]
	public string time;

	// Token: 0x04001E3C RID: 7740
	public Text banben;

	// Token: 0x04001E3D RID: 7741
	public GameObject GongGaoUI;
}
