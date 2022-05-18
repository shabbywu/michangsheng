using System;
using UnityEngine;
using UnityEngine.UI;

namespace EpicToonFX
{
	// Token: 0x02000E92 RID: 3730
	public class ETFXButtonScript : MonoBehaviour
	{
		// Token: 0x06005997 RID: 22935 RVA: 0x00249610 File Offset: 0x00247810
		private void Start()
		{
			this.effectScript = GameObject.Find("ETFXFireProjectile").GetComponent<ETFXFireProjectile>();
			this.getProjectileNames();
			this.MyButtonText = this.Button.transform.Find("Text").GetComponent<Text>();
			this.MyButtonText.text = this.projectileParticleName;
		}

		// Token: 0x06005998 RID: 22936 RVA: 0x0003F920 File Offset: 0x0003DB20
		private void Update()
		{
			this.MyButtonText.text = this.projectileParticleName;
		}

		// Token: 0x06005999 RID: 22937 RVA: 0x0003F933 File Offset: 0x0003DB33
		public void getProjectileNames()
		{
			this.projectileScript = this.effectScript.projectiles[this.effectScript.currentProjectile].GetComponent<ETFXProjectileScript>();
			this.projectileParticleName = this.projectileScript.projectileParticle.name;
		}

		// Token: 0x0600599A RID: 22938 RVA: 0x0024966C File Offset: 0x0024786C
		public bool overButton()
		{
			Rect rect;
			rect..ctor(this.buttonsX, this.buttonsY, this.buttonsSizeX, this.buttonsSizeY);
			Rect rect2;
			rect2..ctor(this.buttonsX + this.buttonsDistance, this.buttonsY, this.buttonsSizeX, this.buttonsSizeY);
			return rect.Contains(new Vector2(Input.mousePosition.x, (float)Screen.height - Input.mousePosition.y)) || rect2.Contains(new Vector2(Input.mousePosition.x, (float)Screen.height - Input.mousePosition.y));
		}

		// Token: 0x040058DD RID: 22749
		public GameObject Button;

		// Token: 0x040058DE RID: 22750
		private Text MyButtonText;

		// Token: 0x040058DF RID: 22751
		private string projectileParticleName;

		// Token: 0x040058E0 RID: 22752
		private ETFXFireProjectile effectScript;

		// Token: 0x040058E1 RID: 22753
		private ETFXProjectileScript projectileScript;

		// Token: 0x040058E2 RID: 22754
		public float buttonsX;

		// Token: 0x040058E3 RID: 22755
		public float buttonsY;

		// Token: 0x040058E4 RID: 22756
		public float buttonsSizeX;

		// Token: 0x040058E5 RID: 22757
		public float buttonsSizeY;

		// Token: 0x040058E6 RID: 22758
		public float buttonsDistance;
	}
}
