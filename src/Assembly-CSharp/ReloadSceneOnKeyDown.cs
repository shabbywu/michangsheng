using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000152 RID: 338
public class ReloadSceneOnKeyDown : MonoBehaviour
{
	// Token: 0x06000EFA RID: 3834 RVA: 0x0005B304 File Offset: 0x00059504
	private void Update()
	{
		if (Input.GetKeyDown(this.reloadKey))
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, 0);
		}
	}

	// Token: 0x04000B3A RID: 2874
	public KeyCode reloadKey = 114;
}
