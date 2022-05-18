using System;
using UnityEngine;

// Token: 0x02000158 RID: 344
public class EnemyDie : MonoBehaviour
{
	// Token: 0x06000C4A RID: 3146 RVA: 0x00096DFC File Offset: 0x00094FFC
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

	// Token: 0x06000C4B RID: 3147 RVA: 0x0000E4FE File Offset: 0x0000C6FE
	private void OnApplicationQuit()
	{
		this.isShuttingDown = true;
	}

	// Token: 0x06000C4C RID: 3148 RVA: 0x00096E48 File Offset: 0x00095048
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

	// Token: 0x04000965 RID: 2405
	public GameObject[] ItemDropAfterDead;

	// Token: 0x04000966 RID: 2406
	public int score = 1;

	// Token: 0x04000967 RID: 2407
	private bool isShuttingDown;
}
