using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000514 RID: 1300
public class SplashScript : MonoBehaviour
{
	// Token: 0x060029C6 RID: 10694 RVA: 0x0013F26E File Offset: 0x0013D46E
	private void Start()
	{
		MessageMag.Instance.Register(MessageName.MSG_PreloadFinish, new Action<MessageData>(this.OnJsonDataInited));
	}

	// Token: 0x060029C7 RID: 10695 RVA: 0x0013F28B File Offset: 0x0013D48B
	public void OnJsonDataInited(MessageData data)
	{
		this.ToInitScene();
	}

	// Token: 0x060029C8 RID: 10696 RVA: 0x0013F293 File Offset: 0x0013D493
	public void ToInitScene()
	{
		SceneManager.LoadSceneAsync("InitScene");
	}
}
