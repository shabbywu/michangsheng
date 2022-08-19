using System;
using UnityEngine;

// Token: 0x020000E1 RID: 225
public class EnemyDie : MonoBehaviour
{
	// Token: 0x06000B5B RID: 2907 RVA: 0x00045264 File Offset: 0x00043464
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

	// Token: 0x06000B5C RID: 2908 RVA: 0x000452AF File Offset: 0x000434AF
	private void OnApplicationQuit()
	{
		this.isShuttingDown = true;
	}

	// Token: 0x06000B5D RID: 2909 RVA: 0x000452B8 File Offset: 0x000434B8
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

	// Token: 0x0400078A RID: 1930
	public GameObject[] ItemDropAfterDead;

	// Token: 0x0400078B RID: 1931
	public int score = 1;

	// Token: 0x0400078C RID: 1932
	private bool isShuttingDown;
}
