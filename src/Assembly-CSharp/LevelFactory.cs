using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020006AD RID: 1709
public class LevelFactory : MonoBehaviour
{
	// Token: 0x06002AB9 RID: 10937 RVA: 0x000211CA File Offset: 0x0001F3CA
	private void Awake()
	{
		LevelFactory.instance = this;
		LevelFactory.level = StagesParser.currentLevel;
	}

	// Token: 0x06002ABA RID: 10938 RVA: 0x00149B68 File Offset: 0x00147D68
	private void Start()
	{
		if (!PlayerPrefs.HasKey("Tour"))
		{
			this.tour = 1;
		}
		else
		{
			this.tour = PlayerPrefs.GetInt("Tour");
		}
		if (StagesParser.bonusLevel)
		{
			MissionManager.OdrediMisiju(MissionManager.missions.Length - 1, false);
		}
		else if (StagesParser.odgledaoTutorial > 0)
		{
			MissionManager.OdrediMisiju(LevelFactory.level - 1, false);
		}
		this.Tezina();
		this.KorigujVerovatnocuZbogMisije(1, 0);
		LevelFactory.trebaFinish = false;
		this.enemyPool = GameObject.Find("__EnemiesPool").transform;
		this.environmentPool = GameObject.Find("__EnvironmentPool").transform;
		this.coinsPool = GameObject.Find("__CoinsPool").transform;
		this.specialPool = GameObject.Find("__SpecialPool").transform;
		this.unistitelj = Camera.main.transform.Find("Unistitelj");
		this.unistitelj.position = new Vector3(Camera.main.transform.position.x - Camera.main.orthographicSize * Camera.main.aspect, this.unistitelj.position.y, this.unistitelj.position.z);
		LevelFactory.GranicaDesno = GameObject.Find("Level_Start_01").transform.Find("__GranicaDesno");
		List<int> list = new List<int>();
		for (int i = 0; i < this.teren.Count; i++)
		{
			GameObject gameObject = this.teren[i];
			this.prefabProperties = gameObject.GetComponent<LevelPrefabProperties>();
			if (this.prefabProperties.minimumLevel <= LevelFactory.level)
			{
				list.Add(i);
			}
		}
		this.suzenaListaTerena = new List<int>();
		this.suzenaListaObjekata = new List<int>();
		this.prefaboviUIgri = new List<Transform>();
		this.brojacPrefaba = 0;
		for (int j = 0; j < this.terenaUPocetku; j++)
		{
			int index = Random.Range(0, list.Count);
			GameObject gameObject2 = this.teren[list[index]];
			list.RemoveAt(index);
			gameObject2.transform.position = LevelFactory.GranicaDesno.position;
			this.prefabProperties = gameObject2.GetComponent<LevelPrefabProperties>();
			this.prefabProperties.slobodanTeren = 0;
			this.prefabProperties.brojUNizu = j;
			LevelFactory.GranicaDesno = gameObject2.transform.Find("__GranicaDesno");
			this.TereniKojiMoguDaDodju = this.prefabProperties.moguDaSeNakace;
			this.prefaboviUIgri.Add(gameObject2.transform);
			if (j == 0)
			{
				this.brojacPrefaba++;
				base.StartCoroutine(this.DodavanjeNeprijatelja(this.prefabProperties));
				base.StartCoroutine(this.DodavanjeEnvironment(this.prefabProperties));
				base.StartCoroutine(this.DodavanjeNovcica(this.prefabProperties));
				base.StartCoroutine(this.DodavanjeSpecial(this.prefabProperties));
				this.aktivniPrefabUIgri++;
			}
		}
		if (StagesParser.bonusLevel)
		{
			Transform transform = this.terrainPool.Find("TerenPrefab11_Finish");
			transform.position = LevelFactory.GranicaDesno.position;
			LevelFactory.GranicaDesno = transform.Find("__GranicaDesno");
			LevelFactory.GranicaDesno.GetComponent<Collider2D>().enabled = false;
			transform.parent = null;
		}
	}

	// Token: 0x06002ABB RID: 10939 RVA: 0x00149EA8 File Offset: 0x001480A8
	public bool mozeDaSeNakaci(int tipTerena)
	{
		for (int i = 0; i < this.TereniKojiMoguDaDodju.Length; i++)
		{
			if (tipTerena == this.TereniKojiMoguDaDodju[i])
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06002ABC RID: 10940 RVA: 0x000211DC File Offset: 0x0001F3DC
	public void Reposition()
	{
		base.StartCoroutine(this.RepositionAndFillStuffCoroutine());
	}

	// Token: 0x06002ABD RID: 10941 RVA: 0x000211EB File Offset: 0x0001F3EB
	public IEnumerator RepositionAndFillStuffCoroutine()
	{
		yield return new WaitForSeconds(0.25f);
		GameObject temp = null;
		if (this.suzenaListaTerena.Count > 0)
		{
			this.suzenaListaTerena.Clear();
		}
		for (int i = 0; i < this.terrainPool.childCount; i++)
		{
			LevelPrefabProperties prefabProperties = this.terrainPool.GetChild(i).GetComponent<LevelPrefabProperties>();
			if (prefabProperties.slobodanTeren == 2)
			{
				if ((Array.IndexOf<int>(this.TereniKojiMoguDaDodju, prefabProperties.tipTerena) > -1 && prefabProperties.minimumLevel <= LevelFactory.level && prefabProperties.maximumLevel >= LevelFactory.level) || (this.TereniKojiMoguDaDodju.Length == 0 && prefabProperties.tipTerena != -10))
				{
					this.suzenaListaTerena.Add(i);
				}
			}
			else if (prefabProperties.slobodanTeren == 1)
			{
				prefabProperties.slobodanTeren = 2;
			}
		}
		if (this.suzenaListaTerena.Count > 0)
		{
			int index = Random.Range(0, this.suzenaListaTerena.Count);
			temp = this.terrainPool.GetChild(this.suzenaListaTerena[index]).gameObject;
		}
		yield return new WaitForSeconds(0.1f);
		if (!LevelFactory.trebaFinish)
		{
			this.brojacPrefaba++;
			temp.transform.position = LevelFactory.GranicaDesno.position;
			LevelPrefabProperties prefabProperties = temp.GetComponent<LevelPrefabProperties>();
			prefabProperties.slobodanTeren = 0;
			LevelFactory.GranicaDesno = temp.transform.Find("__GranicaDesno");
			LevelFactory.GranicaDesno.GetComponent<Collider2D>().enabled = true;
			this.TereniKojiMoguDaDodju = prefabProperties.moguDaSeNakace;
			yield return new WaitForSeconds(0.1f);
			this.prefaboviUIgri.Add(temp.transform);
			this.vremeTriggeraNovogPrefaba = Manage.Instance.aktivnoVreme;
			if (this.vremeTriggeraNovogPrefaba - this.vremeTriggeraPoslednjegPrefaba >= 15f)
			{
				this.aktivniPrefabUIgri++;
			}
			this.vremeTriggeraPoslednjegPrefaba = this.vremeTriggeraNovogPrefaba;
			prefabProperties = this.prefaboviUIgri[this.aktivniPrefabUIgri].GetComponent<LevelPrefabProperties>();
			this.aktivniPrefabUIgri++;
			base.StartCoroutine(this.DodavanjeNeprijatelja(prefabProperties));
			yield return new WaitForSeconds(0.35f);
			base.StartCoroutine(this.DodavanjeEnvironment(prefabProperties));
			yield return new WaitForSeconds(0.35f);
			base.StartCoroutine(this.DodavanjeNovcica(prefabProperties));
			yield return new WaitForSeconds(0.35f);
			base.StartCoroutine(this.DodavanjeSpecial(prefabProperties));
		}
		yield break;
	}

	// Token: 0x06002ABE RID: 10942 RVA: 0x000211FA File Offset: 0x0001F3FA
	public IEnumerator RepositionCoroutine()
	{
		yield return new WaitForSeconds(0.25f);
		GameObject temp = null;
		if (this.suzenaListaTerena.Count > 0)
		{
			this.suzenaListaTerena.Clear();
		}
		for (int i = 0; i < this.terrainPool.childCount; i++)
		{
			LevelPrefabProperties prefabProperties = this.terrainPool.GetChild(i).GetComponent<LevelPrefabProperties>();
			if (prefabProperties.slobodanTeren == 2)
			{
				if ((Array.IndexOf<int>(this.TereniKojiMoguDaDodju, prefabProperties.tipTerena) > -1 && prefabProperties.minimumLevel <= LevelFactory.level && prefabProperties.maximumLevel >= LevelFactory.level) || (this.TereniKojiMoguDaDodju.Length == 0 && prefabProperties.tipTerena != -10))
				{
					this.suzenaListaTerena.Add(i);
				}
			}
			else if (prefabProperties.slobodanTeren == 1)
			{
				prefabProperties.slobodanTeren = 2;
			}
		}
		if (this.suzenaListaTerena.Count > 0)
		{
			int index = Random.Range(0, this.suzenaListaTerena.Count);
			temp = this.terrainPool.GetChild(this.suzenaListaTerena[index]).gameObject;
		}
		yield return new WaitForSeconds(0.1f);
		if (!LevelFactory.trebaFinish)
		{
			this.brojacPrefaba++;
			temp.transform.position = LevelFactory.GranicaDesno.position;
			LevelPrefabProperties prefabProperties = temp.GetComponent<LevelPrefabProperties>();
			prefabProperties.slobodanTeren = 0;
			LevelFactory.GranicaDesno = temp.transform.Find("__GranicaDesno");
			LevelFactory.GranicaDesno.GetComponent<Collider2D>().enabled = true;
			this.TereniKojiMoguDaDodju = prefabProperties.moguDaSeNakace;
			yield return new WaitForSeconds(0.1f);
			base.StartCoroutine(this.DodavanjeNeprijatelja(prefabProperties));
			yield return new WaitForSeconds(0.35f);
			base.StartCoroutine(this.DodavanjeEnvironment(prefabProperties));
			yield return new WaitForSeconds(0.35f);
			base.StartCoroutine(this.DodavanjeNovcica(prefabProperties));
			yield return new WaitForSeconds(0.35f);
			base.StartCoroutine(this.DodavanjeSpecial(prefabProperties));
		}
		yield break;
	}

	// Token: 0x06002ABF RID: 10943 RVA: 0x00021209 File Offset: 0x0001F409
	private IEnumerator DodavanjeNovcica(LevelPrefabProperties prefabPropertiess)
	{
		if (prefabPropertiess.coins_Slots_Count > 0)
		{
			if (this.suzenaListaObjekata.Count > 0)
			{
				this.suzenaListaObjekata.Clear();
			}
			int trenutnaVerovatnoca = Random.Range(0, 100);
			int num;
			for (int i = 0; i < prefabPropertiess.coinsSlots.Count; i = num + 1)
			{
				this.slotProperties = prefabPropertiess.coinsSlots[i].GetComponent<SlotProperties>();
				if (this.slotProperties.Verovatnoca >= 100 - trenutnaVerovatnoca && this.coinsPool.childCount > 0)
				{
					if (this.suzenaListaObjekata.Count > 0)
					{
						this.suzenaListaObjekata.Clear();
					}
					for (int j = 0; j < this.coinsPool.childCount; j++)
					{
						this.entityProperties = this.coinsPool.GetChild(j).GetComponent<EntityProperties>();
						if (this.entityProperties.slobodanEntitet && this.entityProperties.minimumLevel <= LevelFactory.level && (Array.IndexOf<int>(this.slotProperties.availableEntities, this.entityProperties.Type) > -1 || this.slotProperties.availableEntities.Length == 0))
						{
							this.suzenaListaObjekata.Add(j);
						}
					}
					if (this.suzenaListaObjekata.Count > 0)
					{
						int index = Random.Range(0, this.suzenaListaObjekata.Count);
						Transform child = this.coinsPool.GetChild(this.suzenaListaObjekata[index]);
						this.suzenaListaObjekata.RemoveAt(index);
						if (child != null)
						{
							this.entityProperties = child.GetComponent<EntityProperties>();
							if (Random.Range(0, 100) >= 100 - this.entityProperties.Verovatnoca && (this.entityProperties.brojPojavljivanja < this.entityProperties.maxBrojPojavljivanja || this.entityProperties.maxBrojPojavljivanja == 0))
							{
								float z = child.localPosition.z;
								this.entityProperties.slobodanEntitet = false;
								this.entityProperties.trenutnoJeAktivan = false;
								child.position = new Vector3(prefabPropertiess.coinsSlots[i].position.x, prefabPropertiess.coinsSlots[i].position.y, z);
								this.entityProperties.brojPojavljivanja++;
							}
						}
					}
				}
				yield return new WaitForSeconds(0.05f);
				num = i;
			}
			GC.Collect();
		}
		yield break;
	}

	// Token: 0x06002AC0 RID: 10944 RVA: 0x0002121F File Offset: 0x0001F41F
	private IEnumerator DodavanjeEnvironment(LevelPrefabProperties prefabPropertiess)
	{
		if (prefabPropertiess.environment_Slots_Count > 0)
		{
			if (this.suzenaListaObjekata.Count > 0)
			{
				this.suzenaListaObjekata.Clear();
			}
			int trenutnaVerovatnoca = Random.Range(0, 100);
			int num2;
			for (int i = 0; i < prefabPropertiess.environmentsSlots.Count; i = num2 + 1)
			{
				this.slotProperties = prefabPropertiess.environmentsSlots[i].GetComponent<SlotProperties>();
				if (this.slotProperties.Verovatnoca >= 100 - trenutnaVerovatnoca && this.environmentPool.childCount > 0)
				{
					if (this.suzenaListaObjekata.Count > 0)
					{
						this.suzenaListaObjekata.Clear();
					}
					for (int j = 0; j < this.environmentPool.childCount; j++)
					{
						this.entityProperties = this.environmentPool.GetChild(j).GetComponent<EntityProperties>();
						if (this.entityProperties.slobodanEntitet && this.entityProperties.minimumLevel <= LevelFactory.level && (Array.IndexOf<int>(this.slotProperties.availableEntities, this.entityProperties.Type) > -1 || this.slotProperties.availableEntities.Length == 0))
						{
							this.suzenaListaObjekata.Add(j);
						}
					}
					if (this.suzenaListaObjekata.Count > 0)
					{
						int index = Random.Range(0, this.suzenaListaObjekata.Count);
						Transform child = this.environmentPool.GetChild(this.suzenaListaObjekata[index]);
						this.suzenaListaObjekata.RemoveAt(index);
						if (child != null)
						{
							this.entityProperties = child.GetComponent<EntityProperties>();
							if (Random.Range(0, 100) >= 100 - this.entityProperties.Verovatnoca)
							{
								if (this.entityProperties.DozvoljenoSkaliranje)
								{
									float num = Random.Range(this.entityProperties.originalScale.x, 2f * this.entityProperties.originalScale.x);
									child.localScale = new Vector3(num, num, num);
								}
								float z = child.localPosition.z;
								this.entityProperties.slobodanEntitet = false;
								this.entityProperties.trenutnoJeAktivan = false;
								child.position = new Vector3(prefabPropertiess.environmentsSlots[i].position.x, prefabPropertiess.environmentsSlots[i].position.y, z);
							}
						}
					}
				}
				yield return new WaitForSeconds(0.05f);
				num2 = i;
			}
			GC.Collect();
		}
		yield break;
	}

	// Token: 0x06002AC1 RID: 10945 RVA: 0x00021235 File Offset: 0x0001F435
	private IEnumerator DodavanjeNeprijatelja(LevelPrefabProperties prefabPropertiess)
	{
		this.postaviInicijalnuTezinu(prefabPropertiess);
		if (prefabPropertiess.enemies_Slots_Count > 0)
		{
			if (this.suzenaListaObjekata.Count > 0)
			{
				this.suzenaListaObjekata.Clear();
			}
			int num22;
			for (int i = 0; i < prefabPropertiess.enemiesSlots.Count; i = num22 + 1)
			{
				int num = Random.Range(0, 100);
				this.slotProperties = prefabPropertiess.enemiesSlots[i].GetComponent<SlotProperties>();
				bool flag = true;
				if (this.slotProperties.Verovatnoca >= 100 - num)
				{
					if (this.enemyPool.childCount > 0)
					{
						if (this.suzenaListaObjekata.Count > 0)
						{
							this.suzenaListaObjekata.Clear();
						}
						for (int j = 0; j < this.enemyPool.childCount; j++)
						{
							this.entityProperties = this.enemyPool.GetChild(j).GetComponent<EntityProperties>();
							if ((this.entityProperties.Type != 3 || this.leteciBabuni != 1) && (this.entityProperties.Type != 7 || this.boomerangBabuni != 1) && (this.entityProperties.Type != 10 || this.leteceGorile != 1) && (this.entityProperties.Type != 14 || this.kopljeGorile != 1))
							{
								if (this.brojacPrefaba == (int)this.leteciBabuni_Kvota && this.brojacPrefaba <= 7 && this.leteciBabuni == 0 && this.entityProperties.Type == 3)
								{
									if (Array.IndexOf<int>(this.slotProperties.availableEntities, this.entityProperties.Type) > -1 || this.slotProperties.availableEntities.Length == 0)
									{
										if (this.entityProperties.slobodanEntitet)
										{
											this.leteciBabuni_postoji_u_poolu = 1;
											this.KorigujVerovatnocuZbogMisije(2, 1);
											this.leteciBabuni_Kvota += this.leteciBabuni_Kvota_locked;
											Transform child = this.enemyPool.GetChild(j);
											float z = child.localPosition.z;
											this.entityProperties.slobodanEntitet = false;
											this.entityProperties.trenutnoJeAktivan = false;
											if (this.entityProperties.Type != 15 || this.entityProperties.Type != 16 || this.entityProperties.Type != 17 || this.entityProperties.Type != 18)
											{
												for (int k = 0; k < child.childCount; k++)
												{
													child.GetChild(k).Find("BaboonReal").Find("_MajmunceNadrlja").GetComponent<BabunDogadjaji_new>().ResetujBabuna();
												}
											}
											child.position = new Vector3(prefabPropertiess.enemiesSlots[i].position.x, prefabPropertiess.enemiesSlots[i].position.y, z);
											this.entityProperties.brojPojavljivanja++;
											flag = false;
											this.postavljen_u_prefabu_leteciBabuni = 1;
										}
										else
										{
											this.leteciBabuni_postoji_u_poolu = 2;
											this.postavljen_u_prefabu_leteciBabuni = 2;
										}
									}
									else
									{
										this.postavljen_u_prefabu_leteciBabuni = 2;
									}
								}
								if (this.brojacPrefaba == (int)this.leteceGorile_Kvota && this.brojacPrefaba <= 7 && this.leteceGorile == 0 && (this.entityProperties.Type == 10 && flag))
								{
									if (Array.IndexOf<int>(this.slotProperties.availableEntities, this.entityProperties.Type) > -1 || this.slotProperties.availableEntities.Length == 0)
									{
										if (this.entityProperties.slobodanEntitet)
										{
											this.leteceGorile_postoji_u_poolu = 1;
											this.KorigujVerovatnocuZbogMisije(2, 2);
											this.leteceGorile_Kvota += this.leteceGorile_Kvota_locked;
											Transform child2 = this.enemyPool.GetChild(j);
											float z2 = child2.localPosition.z;
											this.entityProperties.slobodanEntitet = false;
											this.entityProperties.trenutnoJeAktivan = false;
											if (this.entityProperties.Type != 15 || this.entityProperties.Type != 16 || this.entityProperties.Type != 17 || this.entityProperties.Type != 18)
											{
												for (int l = 0; l < child2.childCount; l++)
												{
													child2.GetChild(l).Find("BaboonReal").Find("_MajmunceNadrlja").GetComponent<BabunDogadjaji_new>().ResetujBabuna();
												}
											}
											child2.position = new Vector3(prefabPropertiess.enemiesSlots[i].position.x, prefabPropertiess.enemiesSlots[i].position.y, z2);
											this.entityProperties.brojPojavljivanja++;
											flag = false;
											this.postavljen_u_prefabu_leteceGorile = 1;
										}
										else
										{
											this.leteceGorile_postoji_u_poolu = 2;
											this.postavljen_u_prefabu_leteceGorile = 2;
										}
									}
									else
									{
										this.postavljen_u_prefabu_leteceGorile = 2;
									}
								}
								if (this.brojacPrefaba == (int)this.boomerangBabuni_Kvota && this.brojacPrefaba <= 7 && this.boomerangBabuni == 0 && (this.entityProperties.Type == 7 && flag))
								{
									if (Array.IndexOf<int>(this.slotProperties.availableEntities, this.entityProperties.Type) > -1 || this.slotProperties.availableEntities.Length == 0)
									{
										if (this.entityProperties.slobodanEntitet)
										{
											this.boomerangBabuni_postoji_u_poolu = 1;
											this.KorigujVerovatnocuZbogMisije(2, 3);
											this.boomerangBabuni_Kvota += this.boomerangBabuni_Kvota_locked;
											Transform child3 = this.enemyPool.GetChild(j);
											float z3 = child3.localPosition.z;
											this.entityProperties.slobodanEntitet = false;
											this.entityProperties.trenutnoJeAktivan = false;
											if (this.entityProperties.Type != 15 || this.entityProperties.Type != 16 || this.entityProperties.Type != 17 || this.entityProperties.Type != 18)
											{
												for (int m = 0; m < child3.childCount; m++)
												{
													child3.GetChild(m).Find("BaboonReal").Find("_MajmunceNadrlja").GetComponent<BabunDogadjaji_new>().ResetujBabuna();
												}
											}
											child3.position = new Vector3(prefabPropertiess.enemiesSlots[i].position.x, prefabPropertiess.enemiesSlots[i].position.y, z3);
											this.entityProperties.brojPojavljivanja++;
											flag = false;
											this.postavljen_u_prefabu_boomerangBabuni = 1;
										}
										else
										{
											this.boomerangBabuni_postoji_u_poolu = 2;
											this.postavljen_u_prefabu_boomerangBabuni = 2;
										}
									}
									else
									{
										this.postavljen_u_prefabu_boomerangBabuni = 2;
									}
								}
								if (this.brojacPrefaba == (int)this.kopljeGorile_Kvota && this.brojacPrefaba <= 7 && this.kopljeGorile == 0 && (this.entityProperties.Type == 14 && flag))
								{
									if (Array.IndexOf<int>(this.slotProperties.availableEntities, this.entityProperties.Type) > -1 || this.slotProperties.availableEntities.Length == 0)
									{
										if (this.entityProperties.slobodanEntitet)
										{
											this.kopljeGorile_postoji_u_poolu = 1;
											this.KorigujVerovatnocuZbogMisije(2, 4);
											this.kopljeGorile_Kvota += this.kopljeGorile_Kvota_locked;
											Transform child4 = this.enemyPool.GetChild(j);
											float z4 = child4.localPosition.z;
											this.entityProperties.slobodanEntitet = false;
											this.entityProperties.trenutnoJeAktivan = false;
											if (this.entityProperties.Type != 15 || this.entityProperties.Type != 16 || this.entityProperties.Type != 17 || this.entityProperties.Type != 18)
											{
												for (int n = 0; n < child4.childCount; n++)
												{
													child4.GetChild(n).Find("BaboonReal").Find("_MajmunceNadrlja").GetComponent<BabunDogadjaji_new>().ResetujBabuna();
												}
											}
											child4.position = new Vector3(prefabPropertiess.enemiesSlots[i].position.x, prefabPropertiess.enemiesSlots[i].position.y, z4);
											this.entityProperties.brojPojavljivanja++;
											flag = false;
											this.postavljen_u_prefabu_kopljeGorile = 1;
										}
										else
										{
											this.kopljeGorile_postoji_u_poolu = 2;
											this.postavljen_u_prefabu_kopljeGorile = 2;
										}
									}
									else
									{
										this.postavljen_u_prefabu_kopljeGorile = 2;
									}
								}
								if (this.entityProperties.slobodanEntitet && flag && this.entityProperties.minimumLevel <= LevelFactory.level && (Array.IndexOf<int>(this.slotProperties.availableEntities, this.entityProperties.Type) > -1 || this.slotProperties.availableEntities.Length == 0))
								{
									this.suzenaListaObjekata.Add(j);
								}
							}
						}
						if (this.leteciBabuni_postoji_u_poolu == 2 && this.brojacPrefaba == (int)this.leteciBabuni_Kvota && this.brojacPrefaba <= 7 && this.leteciBabuni == 0 && flag)
						{
							this.KorigujVerovatnocuZbogMisije(2, 1);
							this.leteciBabuni_Kvota += this.leteciBabuni_Kvota_locked;
							GameObject gameObject = Object.Instantiate<GameObject>(this.enemiesForInstantiate[2], new Vector3(prefabPropertiess.enemiesSlots[i].position.x, prefabPropertiess.enemiesSlots[i].position.y, prefabPropertiess.enemiesSlots[i].position.z), Quaternion.identity);
							for (int num2 = 0; num2 < gameObject.transform.childCount; num2++)
							{
								gameObject.transform.GetChild(num2).Find("BaboonReal").Find("_MajmunceNadrlja").GetComponent<BabunDogadjaji_new>().ResetujBabuna();
							}
							gameObject.transform.parent = this.enemyPool;
							this.entityProperties = gameObject.GetComponent<EntityProperties>();
							this.entityProperties.instanciran = true;
							this.entityProperties.slobodanEntitet = false;
							this.entityProperties.trenutnoJeAktivan = false;
							flag = false;
							this.leteciBabuni_postoji_u_poolu = 0;
							this.postavljen_u_prefabu_leteciBabuni = 1;
						}
						if (this.leteceGorile_postoji_u_poolu == 2 && this.brojacPrefaba == (int)this.leteceGorile_Kvota && this.brojacPrefaba <= 7 && this.leteceGorile == 0 && flag)
						{
							this.KorigujVerovatnocuZbogMisije(2, 2);
							this.leteceGorile_Kvota += this.leteceGorile_Kvota_locked;
							GameObject gameObject2 = Object.Instantiate<GameObject>(this.enemiesForInstantiate[9], new Vector3(prefabPropertiess.enemiesSlots[i].position.x, prefabPropertiess.enemiesSlots[i].position.y, prefabPropertiess.enemiesSlots[i].position.z), Quaternion.identity);
							for (int num3 = 0; num3 < gameObject2.transform.childCount; num3++)
							{
								gameObject2.transform.GetChild(num3).Find("BaboonReal").Find("_MajmunceNadrlja").GetComponent<BabunDogadjaji_new>().ResetujBabuna();
							}
							gameObject2.transform.parent = this.enemyPool;
							this.entityProperties = gameObject2.GetComponent<EntityProperties>();
							this.entityProperties.instanciran = true;
							this.entityProperties.slobodanEntitet = false;
							this.entityProperties.trenutnoJeAktivan = false;
							flag = false;
							this.leteceGorile_postoji_u_poolu = 0;
							this.postavljen_u_prefabu_leteceGorile = 1;
						}
						if (this.boomerangBabuni_postoji_u_poolu == 2 && this.brojacPrefaba == (int)this.boomerangBabuni_Kvota && this.brojacPrefaba <= 7 && this.boomerangBabuni == 0 && flag)
						{
							this.KorigujVerovatnocuZbogMisije(2, 3);
							this.boomerangBabuni_Kvota += this.boomerangBabuni_Kvota_locked;
							GameObject gameObject3 = Object.Instantiate<GameObject>(this.enemiesForInstantiate[6], new Vector3(prefabPropertiess.enemiesSlots[i].position.x, prefabPropertiess.enemiesSlots[i].position.y, prefabPropertiess.enemiesSlots[i].position.z), Quaternion.identity);
							for (int num4 = 0; num4 < gameObject3.transform.childCount; num4++)
							{
								gameObject3.transform.GetChild(num4).Find("BaboonReal").Find("_MajmunceNadrlja").GetComponent<BabunDogadjaji_new>().ResetujBabuna();
							}
							gameObject3.transform.parent = this.enemyPool;
							this.entityProperties = gameObject3.GetComponent<EntityProperties>();
							this.entityProperties.instanciran = true;
							this.entityProperties.slobodanEntitet = false;
							this.entityProperties.trenutnoJeAktivan = false;
							flag = false;
							this.boomerangBabuni_postoji_u_poolu = 0;
							this.postavljen_u_prefabu_boomerangBabuni = 1;
						}
						if (this.kopljeGorile_postoji_u_poolu == 2 && this.brojacPrefaba == (int)this.kopljeGorile_Kvota && this.brojacPrefaba <= 7 && this.kopljeGorile == 0 && flag)
						{
							this.KorigujVerovatnocuZbogMisije(2, 4);
							this.kopljeGorile_Kvota += this.kopljeGorile_Kvota_locked;
							GameObject gameObject4 = Object.Instantiate<GameObject>(this.enemiesForInstantiate[13], new Vector3(prefabPropertiess.enemiesSlots[i].position.x, prefabPropertiess.enemiesSlots[i].position.y, prefabPropertiess.enemiesSlots[i].position.z), Quaternion.identity);
							for (int num5 = 0; num5 < gameObject4.transform.childCount; num5++)
							{
								gameObject4.transform.GetChild(num5).Find("BaboonReal").Find("_MajmunceNadrlja").GetComponent<BabunDogadjaji_new>().ResetujBabuna();
							}
							gameObject4.transform.parent = this.enemyPool;
							this.entityProperties = gameObject4.GetComponent<EntityProperties>();
							this.entityProperties.instanciran = true;
							this.entityProperties.slobodanEntitet = false;
							this.entityProperties.trenutnoJeAktivan = false;
							flag = false;
							this.kopljeGorile_postoji_u_poolu = 0;
							this.postavljen_u_prefabu_kopljeGorile = 1;
						}
						if (this.suzenaListaObjekata.Count > 0 && flag)
						{
							int index = Random.Range(0, this.suzenaListaObjekata.Count);
							Transform child5 = this.enemyPool.GetChild(this.suzenaListaObjekata[index]);
							this.suzenaListaObjekata.RemoveAt(index);
							if (child5 != null)
							{
								this.entityProperties = child5.GetComponent<EntityProperties>();
								if (Random.Range(0, 100) >= 100 - this.entityProperties.Verovatnoca)
								{
									if (this.entityProperties.Type != 15 && this.entityProperties.Type != 16 && this.entityProperties.Type != 17 && this.entityProperties.Type != 18)
									{
										for (int num6 = 0; num6 < child5.childCount; num6++)
										{
											child5.GetChild(num6).Find("BaboonReal").Find("_MajmunceNadrlja").GetComponent<BabunDogadjaji_new>().ResetujBabuna();
										}
									}
									float z5 = child5.localPosition.z;
									this.entityProperties.slobodanEntitet = false;
									this.entityProperties.trenutnoJeAktivan = false;
									child5.position = new Vector3(prefabPropertiess.enemiesSlots[i].position.x, prefabPropertiess.enemiesSlots[i].position.y, z5);
								}
							}
						}
						else if (this.suzenaListaObjekata.Count == 0 && flag)
						{
							int num7 = Random.Range(0, this.slotProperties.availableEntities.Length);
							int num8 = this.slotProperties.availableEntities[num7];
							GameObject gameObject5 = Object.Instantiate<GameObject>(this.enemiesForInstantiate[num8 - 1], new Vector3(prefabPropertiess.enemiesSlots[i].position.x, prefabPropertiess.enemiesSlots[i].position.y, prefabPropertiess.enemiesSlots[i].position.z), Quaternion.identity);
							this.entityProperties = gameObject5.GetComponent<EntityProperties>();
							gameObject5.transform.parent = this.enemyPool;
							this.entityProperties.instanciran = true;
							this.entityProperties.slobodanEntitet = false;
							this.entityProperties.trenutnoJeAktivan = false;
							if (this.entityProperties.Type != 15 && this.entityProperties.Type != 16 && this.entityProperties.Type != 17 && this.entityProperties.Type != 18)
							{
								for (int num9 = 0; num9 < gameObject5.transform.childCount; num9++)
								{
									gameObject5.transform.GetChild(num9).Find("BaboonReal").Find("_MajmunceNadrlja").GetComponent<BabunDogadjaji_new>().ResetujBabuna();
								}
							}
						}
					}
				}
				else
				{
					if (this.leteciBabuni == 0 && this.enemyPool.childCount > 0)
					{
						if (this.suzenaListaObjekata.Count > 0)
						{
							this.suzenaListaObjekata.Clear();
						}
						for (int num10 = 0; num10 < this.enemyPool.childCount; num10++)
						{
							this.entityProperties = this.enemyPool.GetChild(num10).GetComponent<EntityProperties>();
							if (this.brojacPrefaba == (int)this.leteciBabuni_Kvota && this.brojacPrefaba <= 7 && this.entityProperties.Type == 3)
							{
								if (Array.IndexOf<int>(this.slotProperties.availableEntities, this.entityProperties.Type) > -1 || this.slotProperties.availableEntities.Length == 0)
								{
									if (this.entityProperties.slobodanEntitet)
									{
										this.leteciBabuni_postoji_u_poolu = 1;
										this.KorigujVerovatnocuZbogMisije(2, 1);
										this.leteciBabuni_Kvota += this.leteciBabuni_Kvota_locked;
										Transform child6 = this.enemyPool.GetChild(num10);
										float z6 = child6.localPosition.z;
										this.entityProperties.slobodanEntitet = false;
										this.entityProperties.trenutnoJeAktivan = false;
										if (this.entityProperties.Type != 15 || this.entityProperties.Type != 16 || this.entityProperties.Type != 17 || this.entityProperties.Type != 18)
										{
											for (int num11 = 0; num11 < child6.childCount; num11++)
											{
												child6.GetChild(num11).Find("BaboonReal").Find("_MajmunceNadrlja").GetComponent<BabunDogadjaji_new>().ResetujBabuna();
											}
										}
										child6.position = new Vector3(prefabPropertiess.enemiesSlots[i].position.x, prefabPropertiess.enemiesSlots[i].position.y, z6);
										this.entityProperties.brojPojavljivanja++;
										flag = false;
										this.postavljen_u_prefabu_leteciBabuni = 1;
									}
									else
									{
										this.leteciBabuni_postoji_u_poolu = 2;
										this.postavljen_u_prefabu_leteciBabuni = 2;
									}
								}
								else
								{
									this.postavljen_u_prefabu_leteciBabuni = 2;
								}
							}
						}
						if (this.leteciBabuni_postoji_u_poolu == 2 && this.brojacPrefaba == (int)this.leteciBabuni_Kvota && this.brojacPrefaba <= 7 && flag)
						{
							this.KorigujVerovatnocuZbogMisije(2, 1);
							this.leteciBabuni_Kvota += this.leteciBabuni_Kvota_locked;
							GameObject gameObject6 = Object.Instantiate<GameObject>(this.enemiesForInstantiate[2], new Vector3(prefabPropertiess.enemiesSlots[i].position.x, prefabPropertiess.enemiesSlots[i].position.y, prefabPropertiess.enemiesSlots[i].position.z), Quaternion.identity);
							for (int num12 = 0; num12 < gameObject6.transform.childCount; num12++)
							{
								gameObject6.transform.GetChild(num12).Find("BaboonReal").Find("_MajmunceNadrlja").GetComponent<BabunDogadjaji_new>().ResetujBabuna();
							}
							gameObject6.transform.parent = this.enemyPool;
							this.entityProperties = gameObject6.GetComponent<EntityProperties>();
							this.entityProperties.instanciran = true;
							this.entityProperties.slobodanEntitet = false;
							this.entityProperties.trenutnoJeAktivan = false;
							flag = false;
							this.leteciBabuni_postoji_u_poolu = 0;
							this.postavljen_u_prefabu_leteciBabuni = 1;
						}
					}
					if (this.leteceGorile == 0 && this.enemyPool.childCount > 0)
					{
						if (this.suzenaListaObjekata.Count > 0)
						{
							this.suzenaListaObjekata.Clear();
						}
						for (int num13 = 0; num13 < this.enemyPool.childCount; num13++)
						{
							this.entityProperties = this.enemyPool.GetChild(num13).GetComponent<EntityProperties>();
							if (this.brojacPrefaba == (int)this.leteceGorile_Kvota && this.brojacPrefaba <= 7 && (this.entityProperties.Type == 10 && flag))
							{
								if (Array.IndexOf<int>(this.slotProperties.availableEntities, this.entityProperties.Type) > -1 || this.slotProperties.availableEntities.Length == 0)
								{
									if (this.entityProperties.slobodanEntitet)
									{
										this.leteceGorile_postoji_u_poolu = 1;
										this.KorigujVerovatnocuZbogMisije(2, 2);
										this.leteceGorile_Kvota += this.leteceGorile_Kvota_locked;
										Transform child7 = this.enemyPool.GetChild(num13);
										float z7 = child7.localPosition.z;
										this.entityProperties.slobodanEntitet = false;
										this.entityProperties.trenutnoJeAktivan = false;
										if (this.entityProperties.Type != 15 || this.entityProperties.Type != 16 || this.entityProperties.Type != 17 || this.entityProperties.Type != 18)
										{
											for (int num14 = 0; num14 < child7.childCount; num14++)
											{
												child7.GetChild(num14).Find("BaboonReal").Find("_MajmunceNadrlja").GetComponent<BabunDogadjaji_new>().ResetujBabuna();
											}
										}
										child7.position = new Vector3(prefabPropertiess.enemiesSlots[i].position.x, prefabPropertiess.enemiesSlots[i].position.y, z7);
										this.entityProperties.brojPojavljivanja++;
										flag = false;
										this.postavljen_u_prefabu_leteceGorile = 1;
									}
									else
									{
										this.leteceGorile_postoji_u_poolu = 2;
										this.postavljen_u_prefabu_leteceGorile = 2;
									}
								}
								else
								{
									this.postavljen_u_prefabu_leteceGorile = 2;
								}
							}
						}
						if (this.leteceGorile_postoji_u_poolu == 2 && this.brojacPrefaba == (int)this.leteceGorile_Kvota && this.brojacPrefaba <= 7 && flag)
						{
							this.KorigujVerovatnocuZbogMisije(2, 2);
							this.leteceGorile_Kvota += this.leteceGorile_Kvota_locked;
							GameObject gameObject7 = Object.Instantiate<GameObject>(this.enemiesForInstantiate[9], new Vector3(prefabPropertiess.enemiesSlots[i].position.x, prefabPropertiess.enemiesSlots[i].position.y, prefabPropertiess.enemiesSlots[i].position.z), Quaternion.identity);
							for (int num15 = 0; num15 < gameObject7.transform.childCount; num15++)
							{
								gameObject7.transform.GetChild(num15).Find("BaboonReal").Find("_MajmunceNadrlja").GetComponent<BabunDogadjaji_new>().ResetujBabuna();
							}
							gameObject7.transform.parent = this.enemyPool;
							this.entityProperties = gameObject7.GetComponent<EntityProperties>();
							this.entityProperties.instanciran = true;
							this.entityProperties.slobodanEntitet = false;
							this.entityProperties.trenutnoJeAktivan = false;
							flag = false;
							this.leteceGorile_postoji_u_poolu = 0;
							this.postavljen_u_prefabu_leteceGorile = 1;
						}
					}
					if (this.boomerangBabuni == 0 && this.enemyPool.childCount > 0)
					{
						if (this.suzenaListaObjekata.Count > 0)
						{
							this.suzenaListaObjekata.Clear();
						}
						for (int num16 = 0; num16 < this.enemyPool.childCount; num16++)
						{
							this.entityProperties = this.enemyPool.GetChild(num16).GetComponent<EntityProperties>();
							if (this.brojacPrefaba == (int)this.boomerangBabuni_Kvota && this.brojacPrefaba <= 7 && (this.entityProperties.Type == 7 && flag))
							{
								if (Array.IndexOf<int>(this.slotProperties.availableEntities, this.entityProperties.Type) > -1 || this.slotProperties.availableEntities.Length == 0)
								{
									if (this.entityProperties.slobodanEntitet)
									{
										this.boomerangBabuni_postoji_u_poolu = 1;
										this.KorigujVerovatnocuZbogMisije(2, 3);
										this.boomerangBabuni_Kvota += this.boomerangBabuni_Kvota_locked;
										Transform child8 = this.enemyPool.GetChild(num16);
										float z8 = child8.localPosition.z;
										this.entityProperties.slobodanEntitet = false;
										this.entityProperties.trenutnoJeAktivan = false;
										if (this.entityProperties.Type != 15 || this.entityProperties.Type != 16 || this.entityProperties.Type != 17 || this.entityProperties.Type != 18)
										{
											for (int num17 = 0; num17 < child8.childCount; num17++)
											{
												child8.GetChild(num17).Find("BaboonReal").Find("_MajmunceNadrlja").GetComponent<BabunDogadjaji_new>().ResetujBabuna();
											}
										}
										child8.position = new Vector3(prefabPropertiess.enemiesSlots[i].position.x, prefabPropertiess.enemiesSlots[i].position.y, z8);
										this.entityProperties.brojPojavljivanja++;
										flag = false;
										this.postavljen_u_prefabu_boomerangBabuni = 1;
									}
									else
									{
										this.boomerangBabuni_postoji_u_poolu = 2;
										this.postavljen_u_prefabu_boomerangBabuni = 2;
									}
								}
								else
								{
									this.postavljen_u_prefabu_boomerangBabuni = 2;
								}
							}
						}
						if (this.boomerangBabuni_postoji_u_poolu == 2 && this.brojacPrefaba == (int)this.boomerangBabuni_Kvota && this.brojacPrefaba <= 7 && flag)
						{
							this.KorigujVerovatnocuZbogMisije(2, 3);
							this.boomerangBabuni_Kvota += this.boomerangBabuni_Kvota_locked;
							GameObject gameObject8 = Object.Instantiate<GameObject>(this.enemiesForInstantiate[6], new Vector3(prefabPropertiess.enemiesSlots[i].position.x, prefabPropertiess.enemiesSlots[i].position.y, prefabPropertiess.enemiesSlots[i].position.z), Quaternion.identity);
							for (int num18 = 0; num18 < gameObject8.transform.childCount; num18++)
							{
								gameObject8.transform.GetChild(num18).Find("BaboonReal").Find("_MajmunceNadrlja").GetComponent<BabunDogadjaji_new>().ResetujBabuna();
							}
							gameObject8.transform.parent = this.enemyPool;
							this.entityProperties = gameObject8.GetComponent<EntityProperties>();
							this.entityProperties.instanciran = true;
							this.entityProperties.slobodanEntitet = false;
							this.entityProperties.trenutnoJeAktivan = false;
							flag = false;
							this.boomerangBabuni_postoji_u_poolu = 0;
							this.postavljen_u_prefabu_boomerangBabuni = 1;
						}
					}
					if (this.kopljeGorile == 0 && this.enemyPool.childCount > 0)
					{
						if (this.suzenaListaObjekata.Count > 0)
						{
							this.suzenaListaObjekata.Clear();
						}
						for (int num19 = 0; num19 < this.enemyPool.childCount; num19++)
						{
							this.entityProperties = this.enemyPool.GetChild(num19).GetComponent<EntityProperties>();
							if (this.brojacPrefaba == (int)this.kopljeGorile_Kvota && this.brojacPrefaba <= 7 && (this.entityProperties.Type == 14 && flag))
							{
								if (Array.IndexOf<int>(this.slotProperties.availableEntities, this.entityProperties.Type) > -1 || this.slotProperties.availableEntities.Length == 0)
								{
									if (this.entityProperties.slobodanEntitet)
									{
										this.kopljeGorile_postoji_u_poolu = 1;
										this.KorigujVerovatnocuZbogMisije(2, 4);
										this.kopljeGorile_Kvota += this.kopljeGorile_Kvota_locked;
										Transform child9 = this.enemyPool.GetChild(num19);
										float z9 = child9.localPosition.z;
										this.entityProperties.slobodanEntitet = false;
										this.entityProperties.trenutnoJeAktivan = false;
										if (this.entityProperties.Type != 15 || this.entityProperties.Type != 16 || this.entityProperties.Type != 17 || this.entityProperties.Type != 18)
										{
											for (int num20 = 0; num20 < child9.childCount; num20++)
											{
												child9.GetChild(num20).Find("BaboonReal").Find("_MajmunceNadrlja").GetComponent<BabunDogadjaji_new>().ResetujBabuna();
											}
										}
										child9.position = new Vector3(prefabPropertiess.enemiesSlots[i].position.x, prefabPropertiess.enemiesSlots[i].position.y, z9);
										this.entityProperties.brojPojavljivanja++;
										flag = false;
										this.postavljen_u_prefabu_kopljeGorile = 1;
									}
									else
									{
										this.kopljeGorile_postoji_u_poolu = 2;
										this.postavljen_u_prefabu_kopljeGorile = 2;
									}
								}
								else
								{
									this.postavljen_u_prefabu_kopljeGorile = 2;
								}
							}
						}
						if (this.kopljeGorile_postoji_u_poolu == 2 && this.brojacPrefaba == (int)this.kopljeGorile_Kvota && this.brojacPrefaba <= 7 && flag)
						{
							this.KorigujVerovatnocuZbogMisije(2, 4);
							this.kopljeGorile_Kvota += this.kopljeGorile_Kvota_locked;
							GameObject gameObject9 = Object.Instantiate<GameObject>(this.enemiesForInstantiate[13], new Vector3(prefabPropertiess.enemiesSlots[i].position.x, prefabPropertiess.enemiesSlots[i].position.y, prefabPropertiess.enemiesSlots[i].position.z), Quaternion.identity);
							for (int num21 = 0; num21 < gameObject9.transform.childCount; num21++)
							{
								gameObject9.transform.GetChild(num21).Find("BaboonReal").Find("_MajmunceNadrlja").GetComponent<BabunDogadjaji_new>().ResetujBabuna();
							}
							gameObject9.transform.parent = this.enemyPool;
							this.entityProperties = gameObject9.GetComponent<EntityProperties>();
							this.entityProperties.instanciran = true;
							this.entityProperties.slobodanEntitet = false;
							this.entityProperties.trenutnoJeAktivan = false;
							this.kopljeGorile_postoji_u_poolu = 0;
							this.postavljen_u_prefabu_kopljeGorile = 1;
						}
					}
				}
				yield return new WaitForSeconds(0.05f);
				if (i == prefabPropertiess.enemiesSlots.Count - 1)
				{
					if (this.postavljen_u_prefabu_leteciBabuni == 2 && this.brojacPrefaba == (int)this.leteciBabuni_Kvota && this.brojacPrefaba <= 7)
					{
						this.leteciBabuni_Kvota += this.leteciBabuni_Kvota_locked;
					}
					if (this.postavljen_u_prefabu_leteceGorile == 2 && this.brojacPrefaba == (int)this.leteceGorile_Kvota && this.brojacPrefaba <= 7)
					{
						this.leteceGorile_Kvota += this.leteceGorile_Kvota_locked;
					}
					if (this.postavljen_u_prefabu_boomerangBabuni == 2 && this.brojacPrefaba == (int)this.boomerangBabuni_Kvota && this.brojacPrefaba <= 7)
					{
						this.boomerangBabuni_Kvota += this.boomerangBabuni_Kvota_locked;
					}
					if (this.postavljen_u_prefabu_kopljeGorile == 2 && this.brojacPrefaba == (int)this.kopljeGorile_Kvota && this.brojacPrefaba <= 7)
					{
						this.kopljeGorile_Kvota += this.kopljeGorile_Kvota_locked;
					}
				}
				if (this.leteciBabuni == 2)
				{
					this.KorigujVerovatnocuZbogMisije(0, 1);
				}
				if (this.leteceGorile == 2)
				{
					this.KorigujVerovatnocuZbogMisije(0, 2);
				}
				if (this.boomerangBabuni == 2)
				{
					this.KorigujVerovatnocuZbogMisije(0, 3);
				}
				if (this.kopljeGorile == 2)
				{
					this.KorigujVerovatnocuZbogMisije(0, 4);
				}
				num22 = i;
			}
			GC.Collect();
		}
		yield break;
	}

	// Token: 0x06002AC2 RID: 10946 RVA: 0x0002124B File Offset: 0x0001F44B
	private IEnumerator DodavanjeSpecial(LevelPrefabProperties prefabPropertiess)
	{
		if (prefabPropertiess.special_Slots_Count > 0)
		{
			if (this.suzenaListaObjekata.Count > 0)
			{
				this.suzenaListaObjekata.Clear();
			}
			int trenutnaVerovatnoca = Random.Range(0, 100);
			int num2;
			for (int i = 0; i < prefabPropertiess.specialSlots.Count; i = num2 + 1)
			{
				this.slotProperties = prefabPropertiess.specialSlots[i].GetComponent<SlotProperties>();
				bool flag = true;
				if (this.slotProperties.Verovatnoca >= 100 - trenutnaVerovatnoca && this.specialPool.childCount > 0)
				{
					if (this.suzenaListaObjekata.Count > 0)
					{
						this.suzenaListaObjekata.Clear();
					}
					for (int j = 0; j < this.specialPool.childCount; j++)
					{
						this.entityProperties = this.specialPool.GetChild(j).GetComponent<EntityProperties>();
						if ((this.entityProperties.Type != 5 || this.crveniDijamant != 1) && (this.entityProperties.Type != 6 || this.plaviDijamant != 1) && (this.entityProperties.Type != 7 || this.zeleniDijamant != 1))
						{
							if (this.brojacPrefaba == (int)this.plaviDijamant_Kvota && this.brojacPrefaba <= 7 && this.plaviDijamant == 0 && (this.entityProperties.Type == 6 && flag) && (Array.IndexOf<int>(this.slotProperties.availableEntities, this.entityProperties.Type) > -1 || this.slotProperties.availableEntities.Length == 0))
							{
								if (this.entityProperties.slobodanEntitet)
								{
									this.plaviDijamant_postoji_u_poolu = 1;
									this.KorigujVerovatnocuZbogMisije(2, 5);
									this.plaviDijamant_Kvota += this.plaviDijamant_Kvota_locked;
									Transform child = this.specialPool.GetChild(j);
									float z = child.localPosition.z;
									this.entityProperties.slobodanEntitet = false;
									this.entityProperties.trenutnoJeAktivan = false;
									child.position = new Vector3(prefabPropertiess.specialSlots[i].position.x, prefabPropertiess.specialSlots[i].position.y, z);
									this.entityProperties.brojPojavljivanja++;
									flag = false;
								}
								else
								{
									this.plaviDijamant_postoji_u_poolu = 2;
								}
							}
							if (this.brojacPrefaba == (int)this.crveniDijamant_Kvota && this.brojacPrefaba <= 7 && this.crveniDijamant == 0 && (this.entityProperties.Type == 5 && flag))
							{
								if (this.entityProperties.slobodanEntitet)
								{
									this.crveniDijamant_postoji_u_poolu = 1;
									this.KorigujVerovatnocuZbogMisije(2, 6);
									this.crveniDijamant_Kvota += this.crveniDijamant_Kvota_locked;
									Transform child2 = this.specialPool.GetChild(j);
									float z2 = child2.localPosition.z;
									this.entityProperties.slobodanEntitet = false;
									this.entityProperties.trenutnoJeAktivan = false;
									child2.position = new Vector3(prefabPropertiess.specialSlots[i].position.x, prefabPropertiess.specialSlots[i].position.y, z2);
									this.entityProperties.brojPojavljivanja++;
									flag = false;
								}
								else
								{
									this.crveniDijamant_postoji_u_poolu = 2;
								}
							}
							if (this.brojacPrefaba == (int)this.zeleniDijamant_Kvota && this.brojacPrefaba <= 7 && this.zeleniDijamant == 0 && (this.entityProperties.Type == 7 && flag))
							{
								if (this.entityProperties.slobodanEntitet)
								{
									this.zeleniDijamant_postoji_u_poolu = 1;
									this.KorigujVerovatnocuZbogMisije(2, 7);
									this.zeleniDijamant_Kvota += this.zeleniDijamant_Kvota_locked;
									Transform child3 = this.specialPool.GetChild(j);
									float z3 = child3.localPosition.z;
									this.entityProperties.slobodanEntitet = false;
									this.entityProperties.trenutnoJeAktivan = false;
									child3.position = new Vector3(prefabPropertiess.specialSlots[i].position.x, prefabPropertiess.specialSlots[i].position.y, z3);
									this.entityProperties.brojPojavljivanja++;
									flag = false;
								}
								else
								{
									this.zeleniDijamant_postoji_u_poolu = 2;
								}
							}
							if (this.entityProperties.slobodanEntitet && flag && this.entityProperties.minimumLevel <= LevelFactory.level && (Array.IndexOf<int>(this.slotProperties.availableEntities, this.entityProperties.Type) > -1 || this.slotProperties.availableEntities.Length == 0) && (!this.entityProperties.name.Contains("Magnet") || !this.magnetCollected) && (!this.entityProperties.name.Contains("Magnet") || this.brojacPrefaba >= 2) && (!this.entityProperties.name.Contains("DoubleCoins") || !this.doubleCoinsCollected) && (!this.entityProperties.name.Contains("DoubleCoins") || this.brojacPrefaba >= 2) && (!this.entityProperties.name.Contains("Shield") || !this.shieldCollected) && (!this.entityProperties.name.Contains("Shield") || this.brojacPrefaba >= 2))
							{
								this.suzenaListaObjekata.Add(j);
							}
						}
					}
					if (this.plaviDijamant_postoji_u_poolu == 2 && this.brojacPrefaba == (int)this.plaviDijamant_Kvota && this.brojacPrefaba <= 7 && this.plaviDijamant == 0 && flag)
					{
						this.KorigujVerovatnocuZbogMisije(2, 5);
						this.plaviDijamant_Kvota += this.plaviDijamant_Kvota_locked;
						Object.Instantiate<GameObject>(this.specialsForInstantiate[1], new Vector3(prefabPropertiess.specialSlots[i].position.x, prefabPropertiess.specialSlots[i].position.y, prefabPropertiess.specialSlots[i].position.z), Quaternion.identity);
						flag = false;
						this.plaviDijamant_postoji_u_poolu = 0;
					}
					if (this.crveniDijamant_postoji_u_poolu == 2 && this.brojacPrefaba == (int)this.crveniDijamant_Kvota && this.brojacPrefaba <= 7 && this.crveniDijamant == 0 && flag)
					{
						this.KorigujVerovatnocuZbogMisije(2, 6);
						this.crveniDijamant_Kvota += this.crveniDijamant_Kvota_locked;
						Object.Instantiate<GameObject>(this.specialsForInstantiate[0], new Vector3(prefabPropertiess.specialSlots[i].position.x, prefabPropertiess.specialSlots[i].position.y, prefabPropertiess.specialSlots[i].position.z), Quaternion.identity);
						flag = false;
						this.crveniDijamant_postoji_u_poolu = 0;
					}
					if (this.zeleniDijamant_postoji_u_poolu == 2 && this.brojacPrefaba == (int)this.zeleniDijamant_Kvota && this.brojacPrefaba <= 7 && this.zeleniDijamant == 0 && flag)
					{
						this.KorigujVerovatnocuZbogMisije(2, 7);
						this.zeleniDijamant_Kvota += this.zeleniDijamant_Kvota_locked;
						Object.Instantiate<GameObject>(this.specialsForInstantiate[2], new Vector3(prefabPropertiess.specialSlots[i].position.x, prefabPropertiess.specialSlots[i].position.y, prefabPropertiess.specialSlots[i].position.z), Quaternion.identity);
						flag = false;
						this.zeleniDijamant_postoji_u_poolu = 0;
					}
					if (this.suzenaListaObjekata.Count > 0 && flag)
					{
						int index = Random.Range(0, this.suzenaListaObjekata.Count);
						Transform child4 = this.specialPool.GetChild(this.suzenaListaObjekata[index]);
						this.suzenaListaObjekata.RemoveAt(index);
						if (child4 != null)
						{
							this.entityProperties = child4.GetComponent<EntityProperties>();
							if (Random.Range(0, 100) >= 100 - this.entityProperties.Verovatnoca && (this.entityProperties.brojPojavljivanja < this.entityProperties.maxBrojPojavljivanja || this.entityProperties.maxBrojPojavljivanja == 0))
							{
								if (this.entityProperties.DozvoljenoSkaliranje)
								{
									float num = Random.Range(this.entityProperties.originalScale.x, 2f * this.entityProperties.originalScale.x);
									child4.transform.localScale = new Vector3(num, num, num);
								}
								float z4 = child4.localPosition.z;
								this.entityProperties.slobodanEntitet = false;
								this.entityProperties.trenutnoJeAktivan = false;
								child4.position = new Vector3(prefabPropertiess.specialSlots[i].position.x, prefabPropertiess.specialSlots[i].position.y, z4);
								this.entityProperties.brojPojavljivanja++;
							}
						}
					}
				}
				yield return new WaitForSeconds(0.05f);
				if (this.plaviDijamant == 2)
				{
					this.KorigujVerovatnocuZbogMisije(0, 5);
				}
				if (this.crveniDijamant == 2)
				{
					this.KorigujVerovatnocuZbogMisije(0, 6);
				}
				if (this.zeleniDijamant == 2)
				{
					this.KorigujVerovatnocuZbogMisije(0, 7);
				}
				num2 = i;
			}
			yield return new WaitForSeconds(0.5f);
			if (this.prviPrefab)
			{
				this.prviPrefab = false;
				this.KorigujVerovatnocuZbogMisije(0, 0);
			}
			GC.Collect();
		}
		yield break;
	}

	// Token: 0x06002AC3 RID: 10947 RVA: 0x00021261 File Offset: 0x0001F461
	public void PostaviFinish()
	{
		LevelFactory.trebaFinish = true;
		MonkeyController2D.canRespawnThings = false;
	}

	// Token: 0x06002AC4 RID: 10948 RVA: 0x00149ED8 File Offset: 0x001480D8
	private void Tezina()
	{
		int num = LevelFactory.level / 20 + 1;
		int num2 = LevelFactory.level % 20;
		if (num2 == 0)
		{
			num2 = 20;
			num--;
		}
		if (this.tour == 1)
		{
			switch (num2)
			{
			case 1:
			case 2:
			case 3:
			case 4:
				this.overallDifficulty = num;
				break;
			case 5:
			case 6:
			case 7:
			case 8:
				this.overallDifficulty = num + 1;
				break;
			case 9:
			case 10:
			case 11:
			case 12:
				this.overallDifficulty = num + 2;
				break;
			case 13:
			case 14:
			case 15:
			case 16:
				this.overallDifficulty = num + 3;
				break;
			case 17:
			case 18:
			case 19:
			case 20:
				this.overallDifficulty = num + 4;
				break;
			}
		}
		else if (this.tour == 2)
		{
			switch (num2)
			{
			case 1:
			case 2:
			case 3:
			case 4:
				this.overallDifficulty = num + 2;
				break;
			case 5:
			case 6:
			case 7:
			case 8:
				this.overallDifficulty = num + 3;
				break;
			case 9:
			case 10:
			case 11:
			case 12:
				this.overallDifficulty = num + 4;
				break;
			case 13:
			case 14:
			case 15:
			case 16:
				this.overallDifficulty = num + 5;
				break;
			case 17:
			case 18:
			case 19:
			case 20:
				this.overallDifficulty = num + 6;
				break;
			}
		}
		else if (this.tour == 3)
		{
			switch (num2)
			{
			case 1:
			case 2:
			case 3:
			case 4:
				this.overallDifficulty = num + 4;
				break;
			case 5:
			case 6:
			case 7:
			case 8:
				this.overallDifficulty = num + 5;
				break;
			case 9:
			case 10:
			case 11:
			case 12:
				this.overallDifficulty = num + 6;
				break;
			case 13:
			case 14:
			case 15:
			case 16:
				this.overallDifficulty = num + 7;
				break;
			case 17:
			case 18:
			case 19:
			case 20:
				this.overallDifficulty = num + 8;
				break;
			}
		}
		switch (this.overallDifficulty)
		{
		case 1:
			this.dg = 10;
			this.gg = 15;
			return;
		case 2:
			this.dg = 10;
			this.gg = 25;
			return;
		case 3:
			this.dg = 15;
			this.gg = 30;
			return;
		case 4:
			this.dg = 20;
			this.gg = 30;
			return;
		case 5:
			this.dg = 20;
			this.gg = 40;
			return;
		case 6:
			this.dg = 25;
			this.gg = 40;
			return;
		case 7:
			this.dg = 30;
			this.gg = 50;
			return;
		case 8:
			this.dg = 40;
			this.gg = 60;
			return;
		case 9:
			this.dg = 50;
			this.gg = 70;
			return;
		case 10:
			this.dg = 65;
			this.gg = 80;
			return;
		case 11:
			this.dg = 70;
			this.gg = 80;
			return;
		case 12:
			this.dg = 75;
			this.gg = 80;
			return;
		case 13:
			this.dg = 75;
			this.gg = 85;
			return;
		case 14:
			this.dg = 80;
			this.gg = 90;
			return;
		case 15:
			this.dg = 90;
			this.gg = 100;
			return;
		case 16:
			this.dg = 100;
			this.gg = 100;
			return;
		default:
			return;
		}
	}

	// Token: 0x06002AC5 RID: 10949 RVA: 0x0014A25C File Offset: 0x0014845C
	private void postaviInicijalnuTezinu(LevelPrefabProperties prefabPropertiess)
	{
		switch (this.overallDifficulty)
		{
		case 1:
			this.dg = 10;
			this.gg = 15;
			break;
		case 2:
			this.dg = 10;
			this.gg = 25;
			break;
		case 3:
			this.dg = 15;
			this.gg = 30;
			break;
		case 4:
			this.dg = 20;
			this.gg = 30;
			break;
		case 5:
			this.dg = 20;
			this.gg = 40;
			break;
		case 6:
			this.dg = 25;
			this.gg = 40;
			break;
		case 7:
			this.dg = 30;
			this.gg = 50;
			break;
		case 8:
			this.dg = 40;
			this.gg = 60;
			break;
		case 9:
			this.dg = 50;
			this.gg = 70;
			break;
		case 10:
			this.dg = 65;
			this.gg = 80;
			break;
		case 11:
			this.dg = 70;
			this.gg = 80;
			break;
		case 12:
			this.dg = 75;
			this.gg = 80;
			break;
		case 13:
			this.dg = 75;
			this.gg = 85;
			break;
		case 14:
			this.dg = 80;
			this.gg = 90;
			break;
		case 15:
			this.dg = 90;
			this.gg = 100;
			break;
		case 16:
			this.dg = 100;
			this.gg = 100;
			break;
		}
		int verovatnoca = Random.Range(this.dg, this.gg);
		for (int i = 0; i < prefabPropertiess.enemiesSlots.Count; i++)
		{
			prefabPropertiess.enemiesSlots[i].GetComponent<SlotProperties>().Verovatnoca = verovatnoca;
		}
	}

	// Token: 0x06002AC6 RID: 10950 RVA: 0x0014A434 File Offset: 0x00148634
	public void KorigujVerovatnocuZbogMisije(int value, int tip)
	{
		if (MissionManager.activeFly_BaboonsMission && (tip == 0 || tip == 1))
		{
			if (value == 1)
			{
				if ((int)this.leteciBabuni_Kvota == 7)
				{
					this.leteciBabuni = value;
				}
			}
			else
			{
				this.leteciBabuni = value;
			}
		}
		if (MissionManager.activeFly_GorillaMission && (tip == 0 || tip == 2))
		{
			if (value == 1)
			{
				if ((int)this.leteceGorile_Kvota == 7)
				{
					this.leteceGorile = value;
				}
			}
			else
			{
				this.leteceGorile = value;
			}
		}
		if (MissionManager.activeBoomerang_BaboonsMission && (tip == 0 || tip == 3))
		{
			if (value == 1)
			{
				if ((int)this.boomerangBabuni_Kvota == 7)
				{
					this.boomerangBabuni = value;
				}
			}
			else
			{
				this.boomerangBabuni = value;
			}
		}
		if (MissionManager.activeKoplje_GorillaMission && (tip == 0 || tip == 4))
		{
			if (value == 1)
			{
				if ((int)this.kopljeGorile_Kvota == 7)
				{
					this.kopljeGorile = value;
				}
			}
			else
			{
				this.kopljeGorile = value;
			}
		}
		if (MissionManager.activeBlueDiamondsMission && (tip == 0 || tip == 5))
		{
			if (value == 1)
			{
				if ((int)this.plaviDijamant_Kvota == 7)
				{
					this.plaviDijamant = value;
				}
			}
			else
			{
				this.plaviDijamant = value;
			}
		}
		if (MissionManager.activeRedDiamondsMission && (tip == 0 || tip == 6))
		{
			if (value == 1)
			{
				if ((int)this.crveniDijamant_Kvota == 7)
				{
					this.crveniDijamant = value;
				}
			}
			else
			{
				this.crveniDijamant = value;
			}
		}
		if (MissionManager.activeGreenDiamondsMission && (tip == 0 || tip == 7))
		{
			if (value == 1)
			{
				if ((int)this.zeleniDijamant_Kvota == 7)
				{
					this.zeleniDijamant = value;
					return;
				}
			}
			else
			{
				this.zeleniDijamant = value;
			}
		}
	}

	// Token: 0x06002AC7 RID: 10951 RVA: 0x0014A574 File Offset: 0x00148774
	public void izbrojPosebne()
	{
		if (MissionManager.activeFly_BaboonsMission)
		{
			this.brojPosebnihNeprijatelja++;
		}
		if (MissionManager.activeFly_GorillaMission)
		{
			this.brojPosebnihNeprijatelja++;
		}
		if (MissionManager.activeBoomerang_BaboonsMission)
		{
			this.brojPosebnihNeprijatelja++;
		}
		if (MissionManager.activeKoplje_GorillaMission)
		{
			this.brojPosebnihNeprijatelja++;
		}
		if (MissionManager.activeBlueDiamondsMission)
		{
			this.brojPosebnihDijamanata++;
		}
		if (MissionManager.activeRedDiamondsMission)
		{
			this.brojPosebnihDijamanata++;
		}
		if (MissionManager.activeGreenDiamondsMission)
		{
			this.brojPosebnihDijamanata++;
		}
	}

	// Token: 0x06002AC8 RID: 10952 RVA: 0x0002126F File Offset: 0x0001F46F
	private bool proveriDaLiNeprijateljUcestvujeUMisiji(int type)
	{
		return (type == 3 && this.leteciBabuni == 2) || (type == 4 && this.boomerangBabuni == 2) || (type == 5 && this.leteceGorile == 2) || (type == 6 && this.kopljeGorile == 2);
	}

	// Token: 0x06002AC9 RID: 10953 RVA: 0x000212AE File Offset: 0x0001F4AE
	private bool proveriDaLiDijamantUcestvujeUMisiji(int type)
	{
		return (type == 5 && this.crveniDijamant == 2) || (type == 6 && this.plaviDijamant == 2) || (type == 7 && this.zeleniDijamant == 2);
	}

	// Token: 0x040024B6 RID: 9398
	public Transform currentLevelComponents;

	// Token: 0x040024B7 RID: 9399
	public Transform terrainPool;

	// Token: 0x040024B8 RID: 9400
	public int terenaUPocetku;

	// Token: 0x040024B9 RID: 9401
	public List<GameObject> teren;

	// Token: 0x040024BA RID: 9402
	private static Transform GranicaDesno;

	// Token: 0x040024BB RID: 9403
	private Transform unistitelj;

	// Token: 0x040024BC RID: 9404
	public static LevelFactory instance;

	// Token: 0x040024BD RID: 9405
	public static int level = 1;

	// Token: 0x040024BE RID: 9406
	public int levelRuchno;

	// Token: 0x040024BF RID: 9407
	public int overallDifficulty;

	// Token: 0x040024C0 RID: 9408
	private Transform enemyPool;

	// Token: 0x040024C1 RID: 9409
	private Transform environmentPool;

	// Token: 0x040024C2 RID: 9410
	private Transform coinsPool;

	// Token: 0x040024C3 RID: 9411
	private Transform specialPool;

	// Token: 0x040024C4 RID: 9412
	private int[] TereniKojiMoguDaDodju;

	// Token: 0x040024C5 RID: 9413
	private List<int> suzenaListaTerena;

	// Token: 0x040024C6 RID: 9414
	private List<int> suzenaListaObjekata;

	// Token: 0x040024C7 RID: 9415
	private LevelPrefabProperties prefabProperties;

	// Token: 0x040024C8 RID: 9416
	private EntityProperties entityProperties;

	// Token: 0x040024C9 RID: 9417
	private SlotProperties slotProperties;

	// Token: 0x040024CA RID: 9418
	private Transform finishHolder;

	// Token: 0x040024CB RID: 9419
	public static bool trebaFinish = false;

	// Token: 0x040024CC RID: 9420
	private int dg;

	// Token: 0x040024CD RID: 9421
	private int gg;

	// Token: 0x040024CE RID: 9422
	private int tour;

	// Token: 0x040024CF RID: 9423
	private TextMesh missionDescription;

	// Token: 0x040024D0 RID: 9424
	public int leteciBabuni;

	// Token: 0x040024D1 RID: 9425
	public int leteceGorile;

	// Token: 0x040024D2 RID: 9426
	public int boomerangBabuni;

	// Token: 0x040024D3 RID: 9427
	public int kopljeGorile;

	// Token: 0x040024D4 RID: 9428
	public int plaviDijamant;

	// Token: 0x040024D5 RID: 9429
	public int crveniDijamant;

	// Token: 0x040024D6 RID: 9430
	public int zeleniDijamant;

	// Token: 0x040024D7 RID: 9431
	[HideInInspector]
	public float leteciBabuni_Kvota;

	// Token: 0x040024D8 RID: 9432
	[HideInInspector]
	public float leteceGorile_Kvota;

	// Token: 0x040024D9 RID: 9433
	[HideInInspector]
	public float boomerangBabuni_Kvota;

	// Token: 0x040024DA RID: 9434
	[HideInInspector]
	public float kopljeGorile_Kvota;

	// Token: 0x040024DB RID: 9435
	[HideInInspector]
	public float plaviDijamant_Kvota;

	// Token: 0x040024DC RID: 9436
	[HideInInspector]
	public float crveniDijamant_Kvota;

	// Token: 0x040024DD RID: 9437
	[HideInInspector]
	public float zeleniDijamant_Kvota;

	// Token: 0x040024DE RID: 9438
	[HideInInspector]
	public float leteciBabuni_Kvota_locked;

	// Token: 0x040024DF RID: 9439
	[HideInInspector]
	public float leteceGorile_Kvota_locked;

	// Token: 0x040024E0 RID: 9440
	[HideInInspector]
	public float boomerangBabuni_Kvota_locked;

	// Token: 0x040024E1 RID: 9441
	[HideInInspector]
	public float kopljeGorile_Kvota_locked;

	// Token: 0x040024E2 RID: 9442
	[HideInInspector]
	public float plaviDijamant_Kvota_locked;

	// Token: 0x040024E3 RID: 9443
	[HideInInspector]
	public float crveniDijamant_Kvota_locked;

	// Token: 0x040024E4 RID: 9444
	[HideInInspector]
	public float zeleniDijamant_Kvota_locked;

	// Token: 0x040024E5 RID: 9445
	private int leteciBabuni_postoji_u_poolu;

	// Token: 0x040024E6 RID: 9446
	private int leteceGorile_postoji_u_poolu;

	// Token: 0x040024E7 RID: 9447
	private int boomerangBabuni_postoji_u_poolu;

	// Token: 0x040024E8 RID: 9448
	private int kopljeGorile_postoji_u_poolu;

	// Token: 0x040024E9 RID: 9449
	private int plaviDijamant_postoji_u_poolu;

	// Token: 0x040024EA RID: 9450
	private int crveniDijamant_postoji_u_poolu;

	// Token: 0x040024EB RID: 9451
	private int zeleniDijamant_postoji_u_poolu;

	// Token: 0x040024EC RID: 9452
	private int postavljen_u_prefabu_leteciBabuni;

	// Token: 0x040024ED RID: 9453
	private int postavljen_u_prefabu_leteceGorile;

	// Token: 0x040024EE RID: 9454
	private int postavljen_u_prefabu_boomerangBabuni;

	// Token: 0x040024EF RID: 9455
	private int postavljen_u_prefabu_kopljeGorile;

	// Token: 0x040024F0 RID: 9456
	private bool prviPrefab = true;

	// Token: 0x040024F1 RID: 9457
	private int brojPosebnihNeprijatelja;

	// Token: 0x040024F2 RID: 9458
	private int brojPosebnihDijamanata;

	// Token: 0x040024F3 RID: 9459
	public GameObject[] enemiesForInstantiate;

	// Token: 0x040024F4 RID: 9460
	public GameObject[] specialsForInstantiate;

	// Token: 0x040024F5 RID: 9461
	[HideInInspector]
	public bool magnetCollected;

	// Token: 0x040024F6 RID: 9462
	[HideInInspector]
	public bool doubleCoinsCollected;

	// Token: 0x040024F7 RID: 9463
	[HideInInspector]
	public bool shieldCollected;

	// Token: 0x040024F8 RID: 9464
	private List<Transform> prefaboviUIgri;

	// Token: 0x040024F9 RID: 9465
	private int aktivniPrefabUIgri;

	// Token: 0x040024FA RID: 9466
	private float vremeTriggeraPoslednjegPrefaba;

	// Token: 0x040024FB RID: 9467
	private float vremeTriggeraNovogPrefaba;

	// Token: 0x040024FC RID: 9468
	private int brojacPrefaba;
}
