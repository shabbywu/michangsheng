using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace EpicToonFX
{
	// Token: 0x02000B21 RID: 2849
	public class ETFXFireProjectile : MonoBehaviour
	{
		// Token: 0x06004F83 RID: 20355 RVA: 0x002198DF File Offset: 0x00217ADF
		private void Start()
		{
			this.selectedProjectileButton = GameObject.Find("Button").GetComponent<ETFXButtonScript>();
		}

		// Token: 0x06004F84 RID: 20356 RVA: 0x002198F8 File Offset: 0x00217AF8
		private void Update()
		{
			if (Input.GetKeyDown(275))
			{
				this.nextEffect();
			}
			if (Input.GetKeyDown(100))
			{
				this.nextEffect();
			}
			if (Input.GetKeyDown(97))
			{
				this.previousEffect();
			}
			else if (Input.GetKeyDown(276))
			{
				this.previousEffect();
			}
			if (Input.GetKeyDown(323) && !EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), ref this.hit, 100f))
			{
				GameObject gameObject = Object.Instantiate<GameObject>(this.projectiles[this.currentProjectile], this.spawnPosition.position, Quaternion.identity);
				gameObject.transform.LookAt(this.hit.point);
				gameObject.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * this.speed);
				gameObject.GetComponent<ETFXProjectileScript>().impactNormal = this.hit.normal;
			}
			Debug.DrawRay(Camera.main.ScreenPointToRay(Input.mousePosition).origin, Camera.main.ScreenPointToRay(Input.mousePosition).direction * 100f, Color.yellow);
		}

		// Token: 0x06004F85 RID: 20357 RVA: 0x00219A3A File Offset: 0x00217C3A
		public void nextEffect()
		{
			if (this.currentProjectile < this.projectiles.Length - 1)
			{
				this.currentProjectile++;
			}
			else
			{
				this.currentProjectile = 0;
			}
			this.selectedProjectileButton.getProjectileNames();
		}

		// Token: 0x06004F86 RID: 20358 RVA: 0x00219A70 File Offset: 0x00217C70
		public void previousEffect()
		{
			if (this.currentProjectile > 0)
			{
				this.currentProjectile--;
			}
			else
			{
				this.currentProjectile = this.projectiles.Length - 1;
			}
			this.selectedProjectileButton.getProjectileNames();
		}

		// Token: 0x06004F87 RID: 20359 RVA: 0x00219AA6 File Offset: 0x00217CA6
		public void AdjustSpeed(float newSpeed)
		{
			this.speed = newSpeed;
		}

		// Token: 0x04004E77 RID: 20087
		private RaycastHit hit;

		// Token: 0x04004E78 RID: 20088
		public GameObject[] projectiles;

		// Token: 0x04004E79 RID: 20089
		public Transform spawnPosition;

		// Token: 0x04004E7A RID: 20090
		[HideInInspector]
		public int currentProjectile;

		// Token: 0x04004E7B RID: 20091
		public float speed = 1000f;

		// Token: 0x04004E7C RID: 20092
		private ETFXButtonScript selectedProjectileButton;
	}
}
