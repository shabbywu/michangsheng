using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x0200011E RID: 286
public class ETFXSceneManager : MonoBehaviour
{
	// Token: 0x06000D9C RID: 3484 RVA: 0x000515FB File Offset: 0x0004F7FB
	public void LoadScene1()
	{
		SceneManager.LoadScene("etfx_explosions");
	}

	// Token: 0x06000D9D RID: 3485 RVA: 0x00051607 File Offset: 0x0004F807
	public void LoadScene2()
	{
		SceneManager.LoadScene("etfx_explosions2");
	}

	// Token: 0x06000D9E RID: 3486 RVA: 0x00051613 File Offset: 0x0004F813
	public void LoadScene3()
	{
		SceneManager.LoadScene("etfx_portals");
	}

	// Token: 0x06000D9F RID: 3487 RVA: 0x0005161F File Offset: 0x0004F81F
	public void LoadScene4()
	{
		SceneManager.LoadScene("etfx_magic");
	}

	// Token: 0x06000DA0 RID: 3488 RVA: 0x0005162B File Offset: 0x0004F82B
	public void LoadScene5()
	{
		SceneManager.LoadScene("etfx_emojis");
	}

	// Token: 0x06000DA1 RID: 3489 RVA: 0x00051637 File Offset: 0x0004F837
	public void LoadScene6()
	{
		SceneManager.LoadScene("etfx_sparkles");
	}

	// Token: 0x06000DA2 RID: 3490 RVA: 0x00051643 File Offset: 0x0004F843
	public void LoadScene7()
	{
		SceneManager.LoadScene("etfx_fireworks");
	}

	// Token: 0x06000DA3 RID: 3491 RVA: 0x0005164F File Offset: 0x0004F84F
	public void LoadScene8()
	{
		SceneManager.LoadScene("etfx_powerups");
	}

	// Token: 0x06000DA4 RID: 3492 RVA: 0x0005165B File Offset: 0x0004F85B
	public void LoadScene9()
	{
		SceneManager.LoadScene("etfx_swordcombat");
	}

	// Token: 0x06000DA5 RID: 3493 RVA: 0x00051667 File Offset: 0x0004F867
	public void LoadScene10()
	{
		SceneManager.LoadScene("etfx_maindemo");
	}

	// Token: 0x06000DA6 RID: 3494 RVA: 0x00051673 File Offset: 0x0004F873
	public void LoadScene11()
	{
		SceneManager.LoadScene("etfx_combat");
	}

	// Token: 0x06000DA7 RID: 3495 RVA: 0x0005167F File Offset: 0x0004F87F
	public void LoadScene12()
	{
		SceneManager.LoadScene("etfx_2ddemo");
	}

	// Token: 0x06000DA8 RID: 3496 RVA: 0x0005168B File Offset: 0x0004F88B
	public void LoadScene13()
	{
		SceneManager.LoadScene("etfx_missiles");
	}

	// Token: 0x06000DA9 RID: 3497 RVA: 0x00051698 File Offset: 0x0004F898
	private void Update()
	{
		if (Input.GetKeyDown(108))
		{
			this.GUIHide = !this.GUIHide;
			if (this.GUIHide)
			{
				GameObject.Find("CanvasSceneSelect").GetComponent<Canvas>().enabled = false;
			}
			else
			{
				GameObject.Find("CanvasSceneSelect").GetComponent<Canvas>().enabled = true;
			}
		}
		if (Input.GetKeyDown(106))
		{
			this.GUIHide2 = !this.GUIHide2;
			if (this.GUIHide2)
			{
				GameObject.Find("Canvas").GetComponent<Canvas>().enabled = false;
			}
			else
			{
				GameObject.Find("Canvas").GetComponent<Canvas>().enabled = true;
			}
		}
		if (Input.GetKeyDown(104))
		{
			this.GUIHide3 = !this.GUIHide3;
			if (this.GUIHide3)
			{
				GameObject.Find("ParticleSysDisplayCanvas").GetComponent<Canvas>().enabled = false;
				return;
			}
			GameObject.Find("ParticleSysDisplayCanvas").GetComponent<Canvas>().enabled = true;
		}
	}

	// Token: 0x040009A0 RID: 2464
	public bool GUIHide;

	// Token: 0x040009A1 RID: 2465
	public bool GUIHide2;

	// Token: 0x040009A2 RID: 2466
	public bool GUIHide3;
}
