using System;
using UnityEngine;
using UnityEngine.UI;

namespace EpicToonFX
{
	// Token: 0x02000B20 RID: 2848
	public class ETFXButtonScript : MonoBehaviour
	{
		// Token: 0x06004F7E RID: 20350 RVA: 0x00219790 File Offset: 0x00217990
		private void Start()
		{
			this.effectScript = GameObject.Find("ETFXFireProjectile").GetComponent<ETFXFireProjectile>();
			this.getProjectileNames();
			this.MyButtonText = this.Button.transform.Find("Text").GetComponent<Text>();
			this.MyButtonText.text = this.projectileParticleName;
		}

		// Token: 0x06004F7F RID: 20351 RVA: 0x002197E9 File Offset: 0x002179E9
		private void Update()
		{
			this.MyButtonText.text = this.projectileParticleName;
		}

		// Token: 0x06004F80 RID: 20352 RVA: 0x002197FC File Offset: 0x002179FC
		public void getProjectileNames()
		{
			this.projectileScript = this.effectScript.projectiles[this.effectScript.currentProjectile].GetComponent<ETFXProjectileScript>();
			this.projectileParticleName = this.projectileScript.projectileParticle.name;
		}

		// Token: 0x06004F81 RID: 20353 RVA: 0x00219838 File Offset: 0x00217A38
		public bool overButton()
		{
			Rect rect;
			rect..ctor(this.buttonsX, this.buttonsY, this.buttonsSizeX, this.buttonsSizeY);
			Rect rect2;
			rect2..ctor(this.buttonsX + this.buttonsDistance, this.buttonsY, this.buttonsSizeX, this.buttonsSizeY);
			return rect.Contains(new Vector2(Input.mousePosition.x, (float)Screen.height - Input.mousePosition.y)) || rect2.Contains(new Vector2(Input.mousePosition.x, (float)Screen.height - Input.mousePosition.y));
		}

		// Token: 0x04004E6D RID: 20077
		public GameObject Button;

		// Token: 0x04004E6E RID: 20078
		private Text MyButtonText;

		// Token: 0x04004E6F RID: 20079
		private string projectileParticleName;

		// Token: 0x04004E70 RID: 20080
		private ETFXFireProjectile effectScript;

		// Token: 0x04004E71 RID: 20081
		private ETFXProjectileScript projectileScript;

		// Token: 0x04004E72 RID: 20082
		public float buttonsX;

		// Token: 0x04004E73 RID: 20083
		public float buttonsY;

		// Token: 0x04004E74 RID: 20084
		public float buttonsSizeX;

		// Token: 0x04004E75 RID: 20085
		public float buttonsSizeY;

		// Token: 0x04004E76 RID: 20086
		public float buttonsDistance;
	}
}
