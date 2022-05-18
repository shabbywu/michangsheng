using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020001E0 RID: 480
public class ETFXSceneManager : MonoBehaviour
{
	// Token: 0x06000F75 RID: 3957 RVA: 0x0000FB80 File Offset: 0x0000DD80
	public void LoadScene1()
	{
		SceneManager.LoadScene("etfx_explosions");
	}

	// Token: 0x06000F76 RID: 3958 RVA: 0x0000FB8C File Offset: 0x0000DD8C
	public void LoadScene2()
	{
		SceneManager.LoadScene("etfx_explosions2");
	}

	// Token: 0x06000F77 RID: 3959 RVA: 0x0000FB98 File Offset: 0x0000DD98
	public void LoadScene3()
	{
		SceneManager.LoadScene("etfx_portals");
	}

	// Token: 0x06000F78 RID: 3960 RVA: 0x0000FBA4 File Offset: 0x0000DDA4
	public void LoadScene4()
	{
		SceneManager.LoadScene("etfx_magic");
	}

	// Token: 0x06000F79 RID: 3961 RVA: 0x0000FBB0 File Offset: 0x0000DDB0
	public void LoadScene5()
	{
		SceneManager.LoadScene("etfx_emojis");
	}

	// Token: 0x06000F7A RID: 3962 RVA: 0x0000FBBC File Offset: 0x0000DDBC
	public void LoadScene6()
	{
		SceneManager.LoadScene("etfx_sparkles");
	}

	// Token: 0x06000F7B RID: 3963 RVA: 0x0000FBC8 File Offset: 0x0000DDC8
	public void LoadScene7()
	{
		SceneManager.LoadScene("etfx_fireworks");
	}

	// Token: 0x06000F7C RID: 3964 RVA: 0x0000FBD4 File Offset: 0x0000DDD4
	public void LoadScene8()
	{
		SceneManager.LoadScene("etfx_powerups");
	}

	// Token: 0x06000F7D RID: 3965 RVA: 0x0000FBE0 File Offset: 0x0000DDE0
	public void LoadScene9()
	{
		SceneManager.LoadScene("etfx_swordcombat");
	}

	// Token: 0x06000F7E RID: 3966 RVA: 0x0000FBEC File Offset: 0x0000DDEC
	public void LoadScene10()
	{
		SceneManager.LoadScene("etfx_maindemo");
	}

	// Token: 0x06000F7F RID: 3967 RVA: 0x0000FBF8 File Offset: 0x0000DDF8
	public void LoadScene11()
	{
		SceneManager.LoadScene("etfx_combat");
	}

	// Token: 0x06000F80 RID: 3968 RVA: 0x0000FC04 File Offset: 0x0000DE04
	public void LoadScene12()
	{
		SceneManager.LoadScene("etfx_2ddemo");
	}

	// Token: 0x06000F81 RID: 3969 RVA: 0x0000FC10 File Offset: 0x0000DE10
	public void LoadScene13()
	{
		SceneManager.LoadScene("etfx_missiles");
	}

	// Token: 0x06000F82 RID: 3970 RVA: 0x000A20C8 File Offset: 0x000A02C8
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

	// Token: 0x04000C24 RID: 3108
	public bool GUIHide;

	// Token: 0x04000C25 RID: 3109
	public bool GUIHide2;

	// Token: 0x04000C26 RID: 3110
	public bool GUIHide3;
}
