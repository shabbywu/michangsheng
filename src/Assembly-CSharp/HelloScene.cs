using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020001E8 RID: 488
public class HelloScene : MonoBehaviour
{
	// Token: 0x06001455 RID: 5205 RVA: 0x00082F88 File Offset: 0x00081188
	private void Start()
	{
		this.scene = SceneManager.LoadSceneAsync("MainMenu");
		this.scene.allowSceneActivation = false;
		base.Invoke("GameStart", 6f);
	}

	// Token: 0x06001456 RID: 5206 RVA: 0x00082FB6 File Offset: 0x000811B6
	public void GameStart()
	{
		this.scene.allowSceneActivation = true;
	}

	// Token: 0x06001457 RID: 5207 RVA: 0x00004095 File Offset: 0x00002295
	private void Update()
	{
	}

	// Token: 0x04000F1D RID: 3869
	private AsyncOperation scene;
}
