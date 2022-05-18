using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000229 RID: 553
public class ReloadSceneOnKeyDown : MonoBehaviour
{
	// Token: 0x06001126 RID: 4390 RVA: 0x000AB484 File Offset: 0x000A9684
	private void Update()
	{
		if (Input.GetKeyDown(this.reloadKey))
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, 0);
		}
	}

	// Token: 0x04000DDF RID: 3551
	public KeyCode reloadKey = 114;
}
