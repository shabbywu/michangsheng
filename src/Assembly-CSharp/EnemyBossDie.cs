using System;
using UnityEngine;

// Token: 0x02000157 RID: 343
public class EnemyBossDie : MonoBehaviour
{
	// Token: 0x06000C46 RID: 3142 RVA: 0x00096CBC File Offset: 0x00094EBC
	private void OnDestroy()
	{
		if (!this.isShuttingDown)
		{
			this.DropItem();
			GameManager gameManager = (GameManager)Object.FindObjectOfType(typeof(GameManager));
			if (gameManager)
			{
				gameManager.Score += this.score;
			}
		}
	}

	// Token: 0x06000C47 RID: 3143 RVA: 0x0000E4E6 File Offset: 0x0000C6E6
	private void OnApplicationQuit()
	{
		this.isShuttingDown = true;
	}

	// Token: 0x06000C48 RID: 3144 RVA: 0x00096D08 File Offset: 0x00094F08
	private void DropItem()
	{
		if (this.ItemDropAfterDead.Length != 0)
		{
			int num = Random.Range(0, this.ItemDropAfterDead.Length);
			if (this.ItemDropAfterDead[num] != null)
			{
				GameObject gameObject = Object.Instantiate<GameObject>(this.ItemDropAfterDead[num], base.gameObject.transform.position + Vector3.up * 2f, base.gameObject.transform.rotation);
				if (gameObject.GetComponent<Rigidbody>())
				{
					gameObject.GetComponent<Rigidbody>().AddForce((-base.transform.forward + Vector3.up) * 100f);
					gameObject.GetComponent<Rigidbody>().AddTorque((-base.transform.forward + Vector3.up) * 100f);
				}
				Object.Destroy(gameObject, 5f);
			}
		}
	}

	// Token: 0x04000962 RID: 2402
	public GameObject[] ItemDropAfterDead;

	// Token: 0x04000963 RID: 2403
	public int score = 1;

	// Token: 0x04000964 RID: 2404
	private bool isShuttingDown;
}
