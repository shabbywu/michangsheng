using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020007A8 RID: 1960
public class SplashScript : MonoBehaviour
{
	// Token: 0x060031D9 RID: 12761 RVA: 0x000246D3 File Offset: 0x000228D3
	private void Start()
	{
		MessageMag.Instance.Register(MessageName.MSG_PreloadFinish, new Action<MessageData>(this.OnJsonDataInited));
	}

	// Token: 0x060031DA RID: 12762 RVA: 0x000246F0 File Offset: 0x000228F0
	public void OnJsonDataInited(MessageData data)
	{
		this.ToInitScene();
	}

	// Token: 0x060031DB RID: 12763 RVA: 0x000246F8 File Offset: 0x000228F8
	public void ToInitScene()
	{
		SceneManager.LoadSceneAsync("InitScene");
	}
}
