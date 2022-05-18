using System;
using System.Collections;
using UnityEngine;

// Token: 0x020007A4 RID: 1956
public class UnistiteljEvents : MonoBehaviour
{
	// Token: 0x060031C8 RID: 12744 RVA: 0x0018BE58 File Offset: 0x0018A058
	private void Start()
	{
		this.enemyPool = GameObject.Find("__EnemiesPool").transform;
		this.environmentPool = GameObject.Find("__EnvironmentPool").transform;
		this.coinsPool = GameObject.Find("__CoinsPool").transform;
		this.specialPool = GameObject.Find("__SpecialPool").transform;
	}

	// Token: 0x060031C9 RID: 12745 RVA: 0x0018BEBC File Offset: 0x0018A0BC
	private void OnTriggerEnter2D(Collider2D col)
	{
		if (this.start > 0)
		{
			if (col.name.Equals("__GranicaDesno"))
			{
				this.brojac++;
				base.StartCoroutine(this.vratiNazadUPool(col));
				return;
			}
		}
		else if (col.name.Equals("__GranicaDesno"))
		{
			this.brojac++;
			LevelFactory.instance.Reposition();
			this.start++;
			col.GetComponent<Collider2D>().enabled = false;
			this.TrenutnoSeKoristi();
		}
	}

	// Token: 0x060031CA RID: 12746 RVA: 0x00024697 File Offset: 0x00022897
	private IEnumerator vratiNazadUPool(Collider2D col)
	{
		LevelPrefabProperties component = col.transform.parent.GetComponent<LevelPrefabProperties>();
		col.GetComponent<Collider2D>().enabled = false;
		component.slobodanTeren = 1;
		for (int i = 0; i < this.enemyPool.childCount; i++)
		{
			EntityProperties component2 = this.enemyPool.GetChild(i).GetComponent<EntityProperties>();
			if (!component2.slobodanEntitet)
			{
				if (!component2.trenutnoJeAktivan)
				{
					component2.trenutnoJeAktivan = true;
				}
				else if (component2.instanciran)
				{
					Object.Destroy(component2.gameObject);
				}
				else
				{
					if (component2.Type == 18)
					{
						component2.transform.GetChild(0).GetChild(0).GetComponent<BarrelExplode>().ObnoviBure();
					}
					component2.slobodanEntitet = true;
				}
			}
		}
		yield return new WaitForSeconds(0.02f);
		for (int j = 0; j < this.environmentPool.childCount; j++)
		{
			EntityProperties component2 = this.environmentPool.GetChild(j).GetComponent<EntityProperties>();
			if (!component2.slobodanEntitet)
			{
				if (!component2.trenutnoJeAktivan)
				{
					component2.trenutnoJeAktivan = true;
				}
				else
				{
					component2.slobodanEntitet = true;
				}
			}
		}
		yield return new WaitForSeconds(0.02f);
		for (int k = 0; k < this.coinsPool.childCount; k++)
		{
			EntityProperties component2 = this.coinsPool.GetChild(k).GetComponent<EntityProperties>();
			if (!component2.slobodanEntitet)
			{
				if (!component2.trenutnoJeAktivan)
				{
					component2.trenutnoJeAktivan = true;
				}
				else
				{
					component2.slobodanEntitet = true;
				}
			}
		}
		yield return new WaitForSeconds(0.02f);
		for (int l = 0; l < this.specialPool.childCount; l++)
		{
			EntityProperties component2 = this.specialPool.GetChild(l).GetComponent<EntityProperties>();
			if (!component2.slobodanEntitet)
			{
				if (!component2.trenutnoJeAktivan)
				{
					component2.trenutnoJeAktivan = true;
				}
				else
				{
					if (component2.Type == 2)
					{
						component2.transform.GetChild(0).GetComponent<BarrelExplode>().ObnoviBure();
					}
					component2.slobodanEntitet = true;
				}
			}
		}
		yield return new WaitForSeconds(0.02f);
		if (!LevelFactory.trebaFinish)
		{
			LevelFactory.instance.Reposition();
		}
		yield break;
	}

	// Token: 0x060031CB RID: 12747 RVA: 0x0018BF4C File Offset: 0x0018A14C
	private void TrenutnoSeKoristi()
	{
		for (int i = 0; i < this.enemyPool.childCount; i++)
		{
			EntityProperties component = this.enemyPool.GetChild(i).GetComponent<EntityProperties>();
			if (!component.trenutnoJeAktivan)
			{
				component.trenutnoJeAktivan = true;
			}
		}
		for (int j = 0; j < this.environmentPool.childCount; j++)
		{
			EntityProperties component = this.environmentPool.GetChild(j).GetComponent<EntityProperties>();
			if (!component.trenutnoJeAktivan)
			{
				component.trenutnoJeAktivan = true;
			}
		}
		for (int k = 0; k < this.coinsPool.childCount; k++)
		{
			EntityProperties component = this.coinsPool.GetChild(k).GetComponent<EntityProperties>();
			if (!component.trenutnoJeAktivan)
			{
				component.trenutnoJeAktivan = true;
			}
		}
		for (int l = 0; l < this.specialPool.childCount; l++)
		{
			EntityProperties component = this.specialPool.GetChild(l).GetComponent<EntityProperties>();
			if (!component.trenutnoJeAktivan)
			{
				component.trenutnoJeAktivan = true;
			}
		}
	}

	// Token: 0x04002DFC RID: 11772
	public Transform terrainPool;

	// Token: 0x04002DFD RID: 11773
	private Transform enemyPool;

	// Token: 0x04002DFE RID: 11774
	private Transform environmentPool;

	// Token: 0x04002DFF RID: 11775
	private Transform coinsPool;

	// Token: 0x04002E00 RID: 11776
	private Transform specialPool;

	// Token: 0x04002E01 RID: 11777
	private int start;

	// Token: 0x04002E02 RID: 11778
	private int brojac;
}
