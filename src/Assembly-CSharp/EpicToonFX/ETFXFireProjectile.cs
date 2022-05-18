using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace EpicToonFX
{
	// Token: 0x02000E93 RID: 3731
	public class ETFXFireProjectile : MonoBehaviour
	{
		// Token: 0x0600599C RID: 22940 RVA: 0x0003F96D File Offset: 0x0003DB6D
		private void Start()
		{
			this.selectedProjectileButton = GameObject.Find("Button").GetComponent<ETFXButtonScript>();
		}

		// Token: 0x0600599D RID: 22941 RVA: 0x00249714 File Offset: 0x00247914
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

		// Token: 0x0600599E RID: 22942 RVA: 0x0003F984 File Offset: 0x0003DB84
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

		// Token: 0x0600599F RID: 22943 RVA: 0x0003F9BA File Offset: 0x0003DBBA
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

		// Token: 0x060059A0 RID: 22944 RVA: 0x0003F9F0 File Offset: 0x0003DBF0
		public void AdjustSpeed(float newSpeed)
		{
			this.speed = newSpeed;
		}

		// Token: 0x040058E7 RID: 22759
		private RaycastHit hit;

		// Token: 0x040058E8 RID: 22760
		public GameObject[] projectiles;

		// Token: 0x040058E9 RID: 22761
		public Transform spawnPosition;

		// Token: 0x040058EA RID: 22762
		[HideInInspector]
		public int currentProjectile;

		// Token: 0x040058EB RID: 22763
		public float speed = 1000f;

		// Token: 0x040058EC RID: 22764
		private ETFXButtonScript selectedProjectileButton;
	}
}
