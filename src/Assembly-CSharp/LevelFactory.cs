using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFactory : MonoBehaviour
{
	public Transform currentLevelComponents;

	public Transform terrainPool;

	public int terenaUPocetku;

	public List<GameObject> teren;

	private static Transform GranicaDesno;

	private Transform unistitelj;

	public static LevelFactory instance;

	public static int level = 1;

	public int levelRuchno;

	public int overallDifficulty;

	private Transform enemyPool;

	private Transform environmentPool;

	private Transform coinsPool;

	private Transform specialPool;

	private int[] TereniKojiMoguDaDodju;

	private List<int> suzenaListaTerena;

	private List<int> suzenaListaObjekata;

	private LevelPrefabProperties prefabProperties;

	private EntityProperties entityProperties;

	private SlotProperties slotProperties;

	private Transform finishHolder;

	public static bool trebaFinish = false;

	private int dg;

	private int gg;

	private int tour;

	private TextMesh missionDescription;

	public int leteciBabuni;

	public int leteceGorile;

	public int boomerangBabuni;

	public int kopljeGorile;

	public int plaviDijamant;

	public int crveniDijamant;

	public int zeleniDijamant;

	[HideInInspector]
	public float leteciBabuni_Kvota;

	[HideInInspector]
	public float leteceGorile_Kvota;

	[HideInInspector]
	public float boomerangBabuni_Kvota;

	[HideInInspector]
	public float kopljeGorile_Kvota;

	[HideInInspector]
	public float plaviDijamant_Kvota;

	[HideInInspector]
	public float crveniDijamant_Kvota;

	[HideInInspector]
	public float zeleniDijamant_Kvota;

	[HideInInspector]
	public float leteciBabuni_Kvota_locked;

	[HideInInspector]
	public float leteceGorile_Kvota_locked;

	[HideInInspector]
	public float boomerangBabuni_Kvota_locked;

	[HideInInspector]
	public float kopljeGorile_Kvota_locked;

	[HideInInspector]
	public float plaviDijamant_Kvota_locked;

	[HideInInspector]
	public float crveniDijamant_Kvota_locked;

	[HideInInspector]
	public float zeleniDijamant_Kvota_locked;

	private int leteciBabuni_postoji_u_poolu;

	private int leteceGorile_postoji_u_poolu;

	private int boomerangBabuni_postoji_u_poolu;

	private int kopljeGorile_postoji_u_poolu;

	private int plaviDijamant_postoji_u_poolu;

	private int crveniDijamant_postoji_u_poolu;

	private int zeleniDijamant_postoji_u_poolu;

	private int postavljen_u_prefabu_leteciBabuni;

	private int postavljen_u_prefabu_leteceGorile;

	private int postavljen_u_prefabu_boomerangBabuni;

	private int postavljen_u_prefabu_kopljeGorile;

	private bool prviPrefab = true;

	private int brojPosebnihNeprijatelja;

	private int brojPosebnihDijamanata;

	public GameObject[] enemiesForInstantiate;

	public GameObject[] specialsForInstantiate;

	[HideInInspector]
	public bool magnetCollected;

	[HideInInspector]
	public bool doubleCoinsCollected;

	[HideInInspector]
	public bool shieldCollected;

	private List<Transform> prefaboviUIgri;

	private int aktivniPrefabUIgri;

	private float vremeTriggeraPoslednjegPrefaba;

	private float vremeTriggeraNovogPrefaba;

	private int brojacPrefaba;

	private void Awake()
	{
		instance = this;
		level = StagesParser.currentLevel;
	}

	private void Start()
	{
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0300: Unknown result type (might be due to invalid IL or missing references)
		if (!PlayerPrefs.HasKey("Tour"))
		{
			tour = 1;
		}
		else
		{
			tour = PlayerPrefs.GetInt("Tour");
		}
		if (StagesParser.bonusLevel)
		{
			MissionManager.OdrediMisiju(MissionManager.missions.Length - 1, mapa: false);
		}
		else if (StagesParser.odgledaoTutorial > 0)
		{
			MissionManager.OdrediMisiju(level - 1, mapa: false);
		}
		Tezina();
		KorigujVerovatnocuZbogMisije(1, 0);
		trebaFinish = false;
		enemyPool = GameObject.Find("__EnemiesPool").transform;
		environmentPool = GameObject.Find("__EnvironmentPool").transform;
		coinsPool = GameObject.Find("__CoinsPool").transform;
		specialPool = GameObject.Find("__SpecialPool").transform;
		unistitelj = ((Component)Camera.main).transform.Find("Unistitelj");
		unistitelj.position = new Vector3(((Component)Camera.main).transform.position.x - Camera.main.orthographicSize * Camera.main.aspect, unistitelj.position.y, unistitelj.position.z);
		GranicaDesno = GameObject.Find("Level_Start_01").transform.Find("__GranicaDesno");
		List<int> list = new List<int>();
		for (int i = 0; i < teren.Count; i++)
		{
			GameObject val = teren[i];
			prefabProperties = val.GetComponent<LevelPrefabProperties>();
			if (prefabProperties.minimumLevel <= level)
			{
				list.Add(i);
			}
		}
		suzenaListaTerena = new List<int>();
		suzenaListaObjekata = new List<int>();
		prefaboviUIgri = new List<Transform>();
		brojacPrefaba = 0;
		for (int j = 0; j < terenaUPocetku; j++)
		{
			int index = Random.Range(0, list.Count);
			GameObject val2 = teren[list[index]];
			list.RemoveAt(index);
			val2.transform.position = GranicaDesno.position;
			prefabProperties = val2.GetComponent<LevelPrefabProperties>();
			prefabProperties.slobodanTeren = 0;
			prefabProperties.brojUNizu = j;
			GranicaDesno = val2.transform.Find("__GranicaDesno");
			TereniKojiMoguDaDodju = prefabProperties.moguDaSeNakace;
			prefaboviUIgri.Add(val2.transform);
			if (j == 0)
			{
				brojacPrefaba++;
				((MonoBehaviour)this).StartCoroutine(DodavanjeNeprijatelja(prefabProperties));
				((MonoBehaviour)this).StartCoroutine(DodavanjeEnvironment(prefabProperties));
				((MonoBehaviour)this).StartCoroutine(DodavanjeNovcica(prefabProperties));
				((MonoBehaviour)this).StartCoroutine(DodavanjeSpecial(prefabProperties));
				aktivniPrefabUIgri++;
			}
		}
		if (StagesParser.bonusLevel)
		{
			Transform obj = terrainPool.Find("TerenPrefab11_Finish");
			obj.position = GranicaDesno.position;
			GranicaDesno = obj.Find("__GranicaDesno");
			((Behaviour)((Component)GranicaDesno).GetComponent<Collider2D>()).enabled = false;
			obj.parent = null;
		}
	}

	public bool mozeDaSeNakaci(int tipTerena)
	{
		for (int i = 0; i < TereniKojiMoguDaDodju.Length; i++)
		{
			if (tipTerena == TereniKojiMoguDaDodju[i])
			{
				return true;
			}
		}
		return false;
	}

	public void Reposition()
	{
		((MonoBehaviour)this).StartCoroutine(RepositionAndFillStuffCoroutine());
	}

	public IEnumerator RepositionAndFillStuffCoroutine()
	{
		yield return (object)new WaitForSeconds(0.25f);
		GameObject temp = null;
		if (suzenaListaTerena.Count > 0)
		{
			suzenaListaTerena.Clear();
		}
		for (int i = 0; i < terrainPool.childCount; i++)
		{
			LevelPrefabProperties prefabProperties = ((Component)terrainPool.GetChild(i)).GetComponent<LevelPrefabProperties>();
			if (prefabProperties.slobodanTeren == 2)
			{
				if ((Array.IndexOf(TereniKojiMoguDaDodju, prefabProperties.tipTerena) > -1 && prefabProperties.minimumLevel <= level && prefabProperties.maximumLevel >= level) || (TereniKojiMoguDaDodju.Length == 0 && prefabProperties.tipTerena != -10))
				{
					suzenaListaTerena.Add(i);
				}
			}
			else if (prefabProperties.slobodanTeren == 1)
			{
				prefabProperties.slobodanTeren = 2;
			}
		}
		if (suzenaListaTerena.Count > 0)
		{
			int index = Random.Range(0, suzenaListaTerena.Count);
			temp = ((Component)terrainPool.GetChild(suzenaListaTerena[index])).gameObject;
		}
		yield return (object)new WaitForSeconds(0.1f);
		if (!trebaFinish)
		{
			brojacPrefaba++;
			temp.transform.position = GranicaDesno.position;
			LevelPrefabProperties prefabProperties = temp.GetComponent<LevelPrefabProperties>();
			prefabProperties.slobodanTeren = 0;
			GranicaDesno = temp.transform.Find("__GranicaDesno");
			((Behaviour)((Component)GranicaDesno).GetComponent<Collider2D>()).enabled = true;
			TereniKojiMoguDaDodju = prefabProperties.moguDaSeNakace;
			yield return (object)new WaitForSeconds(0.1f);
			prefaboviUIgri.Add(temp.transform);
			vremeTriggeraNovogPrefaba = Manage.Instance.aktivnoVreme;
			if (vremeTriggeraNovogPrefaba - vremeTriggeraPoslednjegPrefaba >= 15f)
			{
				aktivniPrefabUIgri++;
			}
			vremeTriggeraPoslednjegPrefaba = vremeTriggeraNovogPrefaba;
			prefabProperties = ((Component)prefaboviUIgri[aktivniPrefabUIgri]).GetComponent<LevelPrefabProperties>();
			aktivniPrefabUIgri++;
			((MonoBehaviour)this).StartCoroutine(DodavanjeNeprijatelja(prefabProperties));
			yield return (object)new WaitForSeconds(0.35f);
			((MonoBehaviour)this).StartCoroutine(DodavanjeEnvironment(prefabProperties));
			yield return (object)new WaitForSeconds(0.35f);
			((MonoBehaviour)this).StartCoroutine(DodavanjeNovcica(prefabProperties));
			yield return (object)new WaitForSeconds(0.35f);
			((MonoBehaviour)this).StartCoroutine(DodavanjeSpecial(prefabProperties));
		}
	}

	public IEnumerator RepositionCoroutine()
	{
		yield return (object)new WaitForSeconds(0.25f);
		GameObject temp = null;
		if (suzenaListaTerena.Count > 0)
		{
			suzenaListaTerena.Clear();
		}
		for (int i = 0; i < terrainPool.childCount; i++)
		{
			LevelPrefabProperties prefabProperties = ((Component)terrainPool.GetChild(i)).GetComponent<LevelPrefabProperties>();
			if (prefabProperties.slobodanTeren == 2)
			{
				if ((Array.IndexOf(TereniKojiMoguDaDodju, prefabProperties.tipTerena) > -1 && prefabProperties.minimumLevel <= level && prefabProperties.maximumLevel >= level) || (TereniKojiMoguDaDodju.Length == 0 && prefabProperties.tipTerena != -10))
				{
					suzenaListaTerena.Add(i);
				}
			}
			else if (prefabProperties.slobodanTeren == 1)
			{
				prefabProperties.slobodanTeren = 2;
			}
		}
		if (suzenaListaTerena.Count > 0)
		{
			int index = Random.Range(0, suzenaListaTerena.Count);
			temp = ((Component)terrainPool.GetChild(suzenaListaTerena[index])).gameObject;
		}
		yield return (object)new WaitForSeconds(0.1f);
		if (!trebaFinish)
		{
			brojacPrefaba++;
			temp.transform.position = GranicaDesno.position;
			LevelPrefabProperties prefabProperties = temp.GetComponent<LevelPrefabProperties>();
			prefabProperties.slobodanTeren = 0;
			GranicaDesno = temp.transform.Find("__GranicaDesno");
			((Behaviour)((Component)GranicaDesno).GetComponent<Collider2D>()).enabled = true;
			TereniKojiMoguDaDodju = prefabProperties.moguDaSeNakace;
			yield return (object)new WaitForSeconds(0.1f);
			((MonoBehaviour)this).StartCoroutine(DodavanjeNeprijatelja(prefabProperties));
			yield return (object)new WaitForSeconds(0.35f);
			((MonoBehaviour)this).StartCoroutine(DodavanjeEnvironment(prefabProperties));
			yield return (object)new WaitForSeconds(0.35f);
			((MonoBehaviour)this).StartCoroutine(DodavanjeNovcica(prefabProperties));
			yield return (object)new WaitForSeconds(0.35f);
			((MonoBehaviour)this).StartCoroutine(DodavanjeSpecial(prefabProperties));
		}
	}

	private IEnumerator DodavanjeNovcica(LevelPrefabProperties prefabPropertiess)
	{
		if (prefabPropertiess.coins_Slots_Count <= 0)
		{
			yield break;
		}
		if (suzenaListaObjekata.Count > 0)
		{
			suzenaListaObjekata.Clear();
		}
		int trenutnaVerovatnoca = Random.Range(0, 100);
		for (int i = 0; i < prefabPropertiess.coinsSlots.Count; i++)
		{
			slotProperties = ((Component)prefabPropertiess.coinsSlots[i]).GetComponent<SlotProperties>();
			if (slotProperties.Verovatnoca >= 100 - trenutnaVerovatnoca && coinsPool.childCount > 0)
			{
				if (suzenaListaObjekata.Count > 0)
				{
					suzenaListaObjekata.Clear();
				}
				for (int j = 0; j < coinsPool.childCount; j++)
				{
					entityProperties = ((Component)coinsPool.GetChild(j)).GetComponent<EntityProperties>();
					if (entityProperties.slobodanEntitet && entityProperties.minimumLevel <= level && (Array.IndexOf(slotProperties.availableEntities, entityProperties.Type) > -1 || slotProperties.availableEntities.Length == 0))
					{
						suzenaListaObjekata.Add(j);
					}
				}
				if (suzenaListaObjekata.Count > 0)
				{
					int index = Random.Range(0, suzenaListaObjekata.Count);
					Transform child = coinsPool.GetChild(suzenaListaObjekata[index]);
					suzenaListaObjekata.RemoveAt(index);
					if ((Object)(object)child != (Object)null)
					{
						entityProperties = ((Component)child).GetComponent<EntityProperties>();
						if (Random.Range(0, 100) >= 100 - entityProperties.Verovatnoca && (entityProperties.brojPojavljivanja < entityProperties.maxBrojPojavljivanja || entityProperties.maxBrojPojavljivanja == 0))
						{
							float z = child.localPosition.z;
							entityProperties.slobodanEntitet = false;
							entityProperties.trenutnoJeAktivan = false;
							child.position = new Vector3(prefabPropertiess.coinsSlots[i].position.x, prefabPropertiess.coinsSlots[i].position.y, z);
							entityProperties.brojPojavljivanja++;
						}
					}
				}
			}
			yield return (object)new WaitForSeconds(0.05f);
		}
		GC.Collect();
	}

	private IEnumerator DodavanjeEnvironment(LevelPrefabProperties prefabPropertiess)
	{
		if (prefabPropertiess.environment_Slots_Count <= 0)
		{
			yield break;
		}
		if (suzenaListaObjekata.Count > 0)
		{
			suzenaListaObjekata.Clear();
		}
		int trenutnaVerovatnoca = Random.Range(0, 100);
		for (int i = 0; i < prefabPropertiess.environmentsSlots.Count; i++)
		{
			slotProperties = ((Component)prefabPropertiess.environmentsSlots[i]).GetComponent<SlotProperties>();
			if (slotProperties.Verovatnoca >= 100 - trenutnaVerovatnoca && environmentPool.childCount > 0)
			{
				if (suzenaListaObjekata.Count > 0)
				{
					suzenaListaObjekata.Clear();
				}
				for (int j = 0; j < environmentPool.childCount; j++)
				{
					entityProperties = ((Component)environmentPool.GetChild(j)).GetComponent<EntityProperties>();
					if (entityProperties.slobodanEntitet && entityProperties.minimumLevel <= level && (Array.IndexOf(slotProperties.availableEntities, entityProperties.Type) > -1 || slotProperties.availableEntities.Length == 0))
					{
						suzenaListaObjekata.Add(j);
					}
				}
				if (suzenaListaObjekata.Count > 0)
				{
					int index = Random.Range(0, suzenaListaObjekata.Count);
					Transform child = environmentPool.GetChild(suzenaListaObjekata[index]);
					suzenaListaObjekata.RemoveAt(index);
					if ((Object)(object)child != (Object)null)
					{
						entityProperties = ((Component)child).GetComponent<EntityProperties>();
						if (Random.Range(0, 100) >= 100 - entityProperties.Verovatnoca)
						{
							if (entityProperties.DozvoljenoSkaliranje)
							{
								float num = Random.Range(entityProperties.originalScale.x, 2f * entityProperties.originalScale.x);
								child.localScale = new Vector3(num, num, num);
							}
							float z = child.localPosition.z;
							entityProperties.slobodanEntitet = false;
							entityProperties.trenutnoJeAktivan = false;
							child.position = new Vector3(prefabPropertiess.environmentsSlots[i].position.x, prefabPropertiess.environmentsSlots[i].position.y, z);
						}
					}
				}
			}
			yield return (object)new WaitForSeconds(0.05f);
		}
		GC.Collect();
	}

	private IEnumerator DodavanjeNeprijatelja(LevelPrefabProperties prefabPropertiess)
	{
		postaviInicijalnuTezinu(prefabPropertiess);
		if (prefabPropertiess.enemies_Slots_Count <= 0)
		{
			yield break;
		}
		if (suzenaListaObjekata.Count > 0)
		{
			suzenaListaObjekata.Clear();
		}
		for (int i = 0; i < prefabPropertiess.enemiesSlots.Count; i++)
		{
			int num = Random.Range(0, 100);
			slotProperties = ((Component)prefabPropertiess.enemiesSlots[i]).GetComponent<SlotProperties>();
			bool flag = true;
			if (slotProperties.Verovatnoca >= 100 - num)
			{
				if (enemyPool.childCount > 0)
				{
					if (suzenaListaObjekata.Count > 0)
					{
						suzenaListaObjekata.Clear();
					}
					for (int j = 0; j < enemyPool.childCount; j++)
					{
						entityProperties = ((Component)enemyPool.GetChild(j)).GetComponent<EntityProperties>();
						if ((entityProperties.Type == 3 && leteciBabuni == 1) || (entityProperties.Type == 7 && boomerangBabuni == 1) || (entityProperties.Type == 10 && leteceGorile == 1) || (entityProperties.Type == 14 && kopljeGorile == 1))
						{
							continue;
						}
						if (brojacPrefaba == (int)leteciBabuni_Kvota && brojacPrefaba <= 7 && leteciBabuni == 0 && entityProperties.Type == 3)
						{
							if (Array.IndexOf(slotProperties.availableEntities, entityProperties.Type) > -1 || slotProperties.availableEntities.Length == 0)
							{
								if (entityProperties.slobodanEntitet)
								{
									leteciBabuni_postoji_u_poolu = 1;
									KorigujVerovatnocuZbogMisije(2, 1);
									leteciBabuni_Kvota += leteciBabuni_Kvota_locked;
									Transform child = enemyPool.GetChild(j);
									float z = child.localPosition.z;
									entityProperties.slobodanEntitet = false;
									entityProperties.trenutnoJeAktivan = false;
									if (entityProperties.Type != 15 || entityProperties.Type != 16 || entityProperties.Type != 17 || entityProperties.Type != 18)
									{
										for (int k = 0; k < child.childCount; k++)
										{
											((Component)child.GetChild(k).Find("BaboonReal").Find("_MajmunceNadrlja")).GetComponent<BabunDogadjaji_new>().ResetujBabuna();
										}
									}
									child.position = new Vector3(prefabPropertiess.enemiesSlots[i].position.x, prefabPropertiess.enemiesSlots[i].position.y, z);
									entityProperties.brojPojavljivanja++;
									flag = false;
									postavljen_u_prefabu_leteciBabuni = 1;
								}
								else
								{
									leteciBabuni_postoji_u_poolu = 2;
									postavljen_u_prefabu_leteciBabuni = 2;
								}
							}
							else
							{
								postavljen_u_prefabu_leteciBabuni = 2;
							}
						}
						if (brojacPrefaba == (int)leteceGorile_Kvota && brojacPrefaba <= 7 && leteceGorile == 0 && entityProperties.Type == 10 && flag)
						{
							if (Array.IndexOf(slotProperties.availableEntities, entityProperties.Type) > -1 || slotProperties.availableEntities.Length == 0)
							{
								if (entityProperties.slobodanEntitet)
								{
									leteceGorile_postoji_u_poolu = 1;
									KorigujVerovatnocuZbogMisije(2, 2);
									leteceGorile_Kvota += leteceGorile_Kvota_locked;
									Transform child2 = enemyPool.GetChild(j);
									float z2 = child2.localPosition.z;
									entityProperties.slobodanEntitet = false;
									entityProperties.trenutnoJeAktivan = false;
									if (entityProperties.Type != 15 || entityProperties.Type != 16 || entityProperties.Type != 17 || entityProperties.Type != 18)
									{
										for (int l = 0; l < child2.childCount; l++)
										{
											((Component)child2.GetChild(l).Find("BaboonReal").Find("_MajmunceNadrlja")).GetComponent<BabunDogadjaji_new>().ResetujBabuna();
										}
									}
									child2.position = new Vector3(prefabPropertiess.enemiesSlots[i].position.x, prefabPropertiess.enemiesSlots[i].position.y, z2);
									entityProperties.brojPojavljivanja++;
									flag = false;
									postavljen_u_prefabu_leteceGorile = 1;
								}
								else
								{
									leteceGorile_postoji_u_poolu = 2;
									postavljen_u_prefabu_leteceGorile = 2;
								}
							}
							else
							{
								postavljen_u_prefabu_leteceGorile = 2;
							}
						}
						if (brojacPrefaba == (int)boomerangBabuni_Kvota && brojacPrefaba <= 7 && boomerangBabuni == 0 && entityProperties.Type == 7 && flag)
						{
							if (Array.IndexOf(slotProperties.availableEntities, entityProperties.Type) > -1 || slotProperties.availableEntities.Length == 0)
							{
								if (entityProperties.slobodanEntitet)
								{
									boomerangBabuni_postoji_u_poolu = 1;
									KorigujVerovatnocuZbogMisije(2, 3);
									boomerangBabuni_Kvota += boomerangBabuni_Kvota_locked;
									Transform child3 = enemyPool.GetChild(j);
									float z3 = child3.localPosition.z;
									entityProperties.slobodanEntitet = false;
									entityProperties.trenutnoJeAktivan = false;
									if (entityProperties.Type != 15 || entityProperties.Type != 16 || entityProperties.Type != 17 || entityProperties.Type != 18)
									{
										for (int m = 0; m < child3.childCount; m++)
										{
											((Component)child3.GetChild(m).Find("BaboonReal").Find("_MajmunceNadrlja")).GetComponent<BabunDogadjaji_new>().ResetujBabuna();
										}
									}
									child3.position = new Vector3(prefabPropertiess.enemiesSlots[i].position.x, prefabPropertiess.enemiesSlots[i].position.y, z3);
									entityProperties.brojPojavljivanja++;
									flag = false;
									postavljen_u_prefabu_boomerangBabuni = 1;
								}
								else
								{
									boomerangBabuni_postoji_u_poolu = 2;
									postavljen_u_prefabu_boomerangBabuni = 2;
								}
							}
							else
							{
								postavljen_u_prefabu_boomerangBabuni = 2;
							}
						}
						if (brojacPrefaba == (int)kopljeGorile_Kvota && brojacPrefaba <= 7 && kopljeGorile == 0 && entityProperties.Type == 14 && flag)
						{
							if (Array.IndexOf(slotProperties.availableEntities, entityProperties.Type) > -1 || slotProperties.availableEntities.Length == 0)
							{
								if (entityProperties.slobodanEntitet)
								{
									kopljeGorile_postoji_u_poolu = 1;
									KorigujVerovatnocuZbogMisije(2, 4);
									kopljeGorile_Kvota += kopljeGorile_Kvota_locked;
									Transform child4 = enemyPool.GetChild(j);
									float z4 = child4.localPosition.z;
									entityProperties.slobodanEntitet = false;
									entityProperties.trenutnoJeAktivan = false;
									if (entityProperties.Type != 15 || entityProperties.Type != 16 || entityProperties.Type != 17 || entityProperties.Type != 18)
									{
										for (int n = 0; n < child4.childCount; n++)
										{
											((Component)child4.GetChild(n).Find("BaboonReal").Find("_MajmunceNadrlja")).GetComponent<BabunDogadjaji_new>().ResetujBabuna();
										}
									}
									child4.position = new Vector3(prefabPropertiess.enemiesSlots[i].position.x, prefabPropertiess.enemiesSlots[i].position.y, z4);
									entityProperties.brojPojavljivanja++;
									flag = false;
									postavljen_u_prefabu_kopljeGorile = 1;
								}
								else
								{
									kopljeGorile_postoji_u_poolu = 2;
									postavljen_u_prefabu_kopljeGorile = 2;
								}
							}
							else
							{
								postavljen_u_prefabu_kopljeGorile = 2;
							}
						}
						if (entityProperties.slobodanEntitet && flag && entityProperties.minimumLevel <= level && (Array.IndexOf(slotProperties.availableEntities, entityProperties.Type) > -1 || slotProperties.availableEntities.Length == 0))
						{
							suzenaListaObjekata.Add(j);
						}
					}
					if (leteciBabuni_postoji_u_poolu == 2 && brojacPrefaba == (int)leteciBabuni_Kvota && brojacPrefaba <= 7 && leteciBabuni == 0 && flag)
					{
						KorigujVerovatnocuZbogMisije(2, 1);
						leteciBabuni_Kvota += leteciBabuni_Kvota_locked;
						GameObject val = Object.Instantiate<GameObject>(enemiesForInstantiate[2], new Vector3(prefabPropertiess.enemiesSlots[i].position.x, prefabPropertiess.enemiesSlots[i].position.y, prefabPropertiess.enemiesSlots[i].position.z), Quaternion.identity);
						for (int num2 = 0; num2 < val.transform.childCount; num2++)
						{
							((Component)val.transform.GetChild(num2).Find("BaboonReal").Find("_MajmunceNadrlja")).GetComponent<BabunDogadjaji_new>().ResetujBabuna();
						}
						val.transform.parent = enemyPool;
						entityProperties = val.GetComponent<EntityProperties>();
						entityProperties.instanciran = true;
						entityProperties.slobodanEntitet = false;
						entityProperties.trenutnoJeAktivan = false;
						flag = false;
						leteciBabuni_postoji_u_poolu = 0;
						postavljen_u_prefabu_leteciBabuni = 1;
					}
					if (leteceGorile_postoji_u_poolu == 2 && brojacPrefaba == (int)leteceGorile_Kvota && brojacPrefaba <= 7 && leteceGorile == 0 && flag)
					{
						KorigujVerovatnocuZbogMisije(2, 2);
						leteceGorile_Kvota += leteceGorile_Kvota_locked;
						GameObject val2 = Object.Instantiate<GameObject>(enemiesForInstantiate[9], new Vector3(prefabPropertiess.enemiesSlots[i].position.x, prefabPropertiess.enemiesSlots[i].position.y, prefabPropertiess.enemiesSlots[i].position.z), Quaternion.identity);
						for (int num3 = 0; num3 < val2.transform.childCount; num3++)
						{
							((Component)val2.transform.GetChild(num3).Find("BaboonReal").Find("_MajmunceNadrlja")).GetComponent<BabunDogadjaji_new>().ResetujBabuna();
						}
						val2.transform.parent = enemyPool;
						entityProperties = val2.GetComponent<EntityProperties>();
						entityProperties.instanciran = true;
						entityProperties.slobodanEntitet = false;
						entityProperties.trenutnoJeAktivan = false;
						flag = false;
						leteceGorile_postoji_u_poolu = 0;
						postavljen_u_prefabu_leteceGorile = 1;
					}
					if (boomerangBabuni_postoji_u_poolu == 2 && brojacPrefaba == (int)boomerangBabuni_Kvota && brojacPrefaba <= 7 && boomerangBabuni == 0 && flag)
					{
						KorigujVerovatnocuZbogMisije(2, 3);
						boomerangBabuni_Kvota += boomerangBabuni_Kvota_locked;
						GameObject val3 = Object.Instantiate<GameObject>(enemiesForInstantiate[6], new Vector3(prefabPropertiess.enemiesSlots[i].position.x, prefabPropertiess.enemiesSlots[i].position.y, prefabPropertiess.enemiesSlots[i].position.z), Quaternion.identity);
						for (int num4 = 0; num4 < val3.transform.childCount; num4++)
						{
							((Component)val3.transform.GetChild(num4).Find("BaboonReal").Find("_MajmunceNadrlja")).GetComponent<BabunDogadjaji_new>().ResetujBabuna();
						}
						val3.transform.parent = enemyPool;
						entityProperties = val3.GetComponent<EntityProperties>();
						entityProperties.instanciran = true;
						entityProperties.slobodanEntitet = false;
						entityProperties.trenutnoJeAktivan = false;
						flag = false;
						boomerangBabuni_postoji_u_poolu = 0;
						postavljen_u_prefabu_boomerangBabuni = 1;
					}
					if (kopljeGorile_postoji_u_poolu == 2 && brojacPrefaba == (int)kopljeGorile_Kvota && brojacPrefaba <= 7 && kopljeGorile == 0 && flag)
					{
						KorigujVerovatnocuZbogMisije(2, 4);
						kopljeGorile_Kvota += kopljeGorile_Kvota_locked;
						GameObject val4 = Object.Instantiate<GameObject>(enemiesForInstantiate[13], new Vector3(prefabPropertiess.enemiesSlots[i].position.x, prefabPropertiess.enemiesSlots[i].position.y, prefabPropertiess.enemiesSlots[i].position.z), Quaternion.identity);
						for (int num5 = 0; num5 < val4.transform.childCount; num5++)
						{
							((Component)val4.transform.GetChild(num5).Find("BaboonReal").Find("_MajmunceNadrlja")).GetComponent<BabunDogadjaji_new>().ResetujBabuna();
						}
						val4.transform.parent = enemyPool;
						entityProperties = val4.GetComponent<EntityProperties>();
						entityProperties.instanciran = true;
						entityProperties.slobodanEntitet = false;
						entityProperties.trenutnoJeAktivan = false;
						flag = false;
						kopljeGorile_postoji_u_poolu = 0;
						postavljen_u_prefabu_kopljeGorile = 1;
					}
					if (suzenaListaObjekata.Count > 0 && flag)
					{
						int index = Random.Range(0, suzenaListaObjekata.Count);
						Transform child5 = enemyPool.GetChild(suzenaListaObjekata[index]);
						suzenaListaObjekata.RemoveAt(index);
						if ((Object)(object)child5 != (Object)null)
						{
							entityProperties = ((Component)child5).GetComponent<EntityProperties>();
							if (Random.Range(0, 100) >= 100 - entityProperties.Verovatnoca)
							{
								if (entityProperties.Type != 15 && entityProperties.Type != 16 && entityProperties.Type != 17 && entityProperties.Type != 18)
								{
									for (int num6 = 0; num6 < child5.childCount; num6++)
									{
										((Component)child5.GetChild(num6).Find("BaboonReal").Find("_MajmunceNadrlja")).GetComponent<BabunDogadjaji_new>().ResetujBabuna();
									}
								}
								float z5 = child5.localPosition.z;
								entityProperties.slobodanEntitet = false;
								entityProperties.trenutnoJeAktivan = false;
								child5.position = new Vector3(prefabPropertiess.enemiesSlots[i].position.x, prefabPropertiess.enemiesSlots[i].position.y, z5);
							}
						}
					}
					else if (suzenaListaObjekata.Count == 0 && flag)
					{
						int num7 = Random.Range(0, slotProperties.availableEntities.Length);
						int num8 = slotProperties.availableEntities[num7];
						GameObject val5 = Object.Instantiate<GameObject>(enemiesForInstantiate[num8 - 1], new Vector3(prefabPropertiess.enemiesSlots[i].position.x, prefabPropertiess.enemiesSlots[i].position.y, prefabPropertiess.enemiesSlots[i].position.z), Quaternion.identity);
						entityProperties = val5.GetComponent<EntityProperties>();
						val5.transform.parent = enemyPool;
						entityProperties.instanciran = true;
						entityProperties.slobodanEntitet = false;
						entityProperties.trenutnoJeAktivan = false;
						if (entityProperties.Type != 15 && entityProperties.Type != 16 && entityProperties.Type != 17 && entityProperties.Type != 18)
						{
							for (int num9 = 0; num9 < val5.transform.childCount; num9++)
							{
								((Component)val5.transform.GetChild(num9).Find("BaboonReal").Find("_MajmunceNadrlja")).GetComponent<BabunDogadjaji_new>().ResetujBabuna();
							}
						}
					}
				}
			}
			else
			{
				if (leteciBabuni == 0 && enemyPool.childCount > 0)
				{
					if (suzenaListaObjekata.Count > 0)
					{
						suzenaListaObjekata.Clear();
					}
					for (int num10 = 0; num10 < enemyPool.childCount; num10++)
					{
						entityProperties = ((Component)enemyPool.GetChild(num10)).GetComponent<EntityProperties>();
						if (brojacPrefaba != (int)leteciBabuni_Kvota || brojacPrefaba > 7 || entityProperties.Type != 3)
						{
							continue;
						}
						if (Array.IndexOf(slotProperties.availableEntities, entityProperties.Type) > -1 || slotProperties.availableEntities.Length == 0)
						{
							if (entityProperties.slobodanEntitet)
							{
								leteciBabuni_postoji_u_poolu = 1;
								KorigujVerovatnocuZbogMisije(2, 1);
								leteciBabuni_Kvota += leteciBabuni_Kvota_locked;
								Transform child6 = enemyPool.GetChild(num10);
								float z6 = child6.localPosition.z;
								entityProperties.slobodanEntitet = false;
								entityProperties.trenutnoJeAktivan = false;
								if (entityProperties.Type != 15 || entityProperties.Type != 16 || entityProperties.Type != 17 || entityProperties.Type != 18)
								{
									for (int num11 = 0; num11 < child6.childCount; num11++)
									{
										((Component)child6.GetChild(num11).Find("BaboonReal").Find("_MajmunceNadrlja")).GetComponent<BabunDogadjaji_new>().ResetujBabuna();
									}
								}
								child6.position = new Vector3(prefabPropertiess.enemiesSlots[i].position.x, prefabPropertiess.enemiesSlots[i].position.y, z6);
								entityProperties.brojPojavljivanja++;
								flag = false;
								postavljen_u_prefabu_leteciBabuni = 1;
							}
							else
							{
								leteciBabuni_postoji_u_poolu = 2;
								postavljen_u_prefabu_leteciBabuni = 2;
							}
						}
						else
						{
							postavljen_u_prefabu_leteciBabuni = 2;
						}
					}
					if (leteciBabuni_postoji_u_poolu == 2 && brojacPrefaba == (int)leteciBabuni_Kvota && brojacPrefaba <= 7 && flag)
					{
						KorigujVerovatnocuZbogMisije(2, 1);
						leteciBabuni_Kvota += leteciBabuni_Kvota_locked;
						GameObject val6 = Object.Instantiate<GameObject>(enemiesForInstantiate[2], new Vector3(prefabPropertiess.enemiesSlots[i].position.x, prefabPropertiess.enemiesSlots[i].position.y, prefabPropertiess.enemiesSlots[i].position.z), Quaternion.identity);
						for (int num12 = 0; num12 < val6.transform.childCount; num12++)
						{
							((Component)val6.transform.GetChild(num12).Find("BaboonReal").Find("_MajmunceNadrlja")).GetComponent<BabunDogadjaji_new>().ResetujBabuna();
						}
						val6.transform.parent = enemyPool;
						entityProperties = val6.GetComponent<EntityProperties>();
						entityProperties.instanciran = true;
						entityProperties.slobodanEntitet = false;
						entityProperties.trenutnoJeAktivan = false;
						flag = false;
						leteciBabuni_postoji_u_poolu = 0;
						postavljen_u_prefabu_leteciBabuni = 1;
					}
				}
				if (leteceGorile == 0 && enemyPool.childCount > 0)
				{
					if (suzenaListaObjekata.Count > 0)
					{
						suzenaListaObjekata.Clear();
					}
					for (int num13 = 0; num13 < enemyPool.childCount; num13++)
					{
						entityProperties = ((Component)enemyPool.GetChild(num13)).GetComponent<EntityProperties>();
						if (brojacPrefaba != (int)leteceGorile_Kvota || brojacPrefaba > 7 || !(entityProperties.Type == 10 && flag))
						{
							continue;
						}
						if (Array.IndexOf(slotProperties.availableEntities, entityProperties.Type) > -1 || slotProperties.availableEntities.Length == 0)
						{
							if (entityProperties.slobodanEntitet)
							{
								leteceGorile_postoji_u_poolu = 1;
								KorigujVerovatnocuZbogMisije(2, 2);
								leteceGorile_Kvota += leteceGorile_Kvota_locked;
								Transform child7 = enemyPool.GetChild(num13);
								float z7 = child7.localPosition.z;
								entityProperties.slobodanEntitet = false;
								entityProperties.trenutnoJeAktivan = false;
								if (entityProperties.Type != 15 || entityProperties.Type != 16 || entityProperties.Type != 17 || entityProperties.Type != 18)
								{
									for (int num14 = 0; num14 < child7.childCount; num14++)
									{
										((Component)child7.GetChild(num14).Find("BaboonReal").Find("_MajmunceNadrlja")).GetComponent<BabunDogadjaji_new>().ResetujBabuna();
									}
								}
								child7.position = new Vector3(prefabPropertiess.enemiesSlots[i].position.x, prefabPropertiess.enemiesSlots[i].position.y, z7);
								entityProperties.brojPojavljivanja++;
								flag = false;
								postavljen_u_prefabu_leteceGorile = 1;
							}
							else
							{
								leteceGorile_postoji_u_poolu = 2;
								postavljen_u_prefabu_leteceGorile = 2;
							}
						}
						else
						{
							postavljen_u_prefabu_leteceGorile = 2;
						}
					}
					if (leteceGorile_postoji_u_poolu == 2 && brojacPrefaba == (int)leteceGorile_Kvota && brojacPrefaba <= 7 && flag)
					{
						KorigujVerovatnocuZbogMisije(2, 2);
						leteceGorile_Kvota += leteceGorile_Kvota_locked;
						GameObject val7 = Object.Instantiate<GameObject>(enemiesForInstantiate[9], new Vector3(prefabPropertiess.enemiesSlots[i].position.x, prefabPropertiess.enemiesSlots[i].position.y, prefabPropertiess.enemiesSlots[i].position.z), Quaternion.identity);
						for (int num15 = 0; num15 < val7.transform.childCount; num15++)
						{
							((Component)val7.transform.GetChild(num15).Find("BaboonReal").Find("_MajmunceNadrlja")).GetComponent<BabunDogadjaji_new>().ResetujBabuna();
						}
						val7.transform.parent = enemyPool;
						entityProperties = val7.GetComponent<EntityProperties>();
						entityProperties.instanciran = true;
						entityProperties.slobodanEntitet = false;
						entityProperties.trenutnoJeAktivan = false;
						flag = false;
						leteceGorile_postoji_u_poolu = 0;
						postavljen_u_prefabu_leteceGorile = 1;
					}
				}
				if (boomerangBabuni == 0 && enemyPool.childCount > 0)
				{
					if (suzenaListaObjekata.Count > 0)
					{
						suzenaListaObjekata.Clear();
					}
					for (int num16 = 0; num16 < enemyPool.childCount; num16++)
					{
						entityProperties = ((Component)enemyPool.GetChild(num16)).GetComponent<EntityProperties>();
						if (brojacPrefaba != (int)boomerangBabuni_Kvota || brojacPrefaba > 7 || !(entityProperties.Type == 7 && flag))
						{
							continue;
						}
						if (Array.IndexOf(slotProperties.availableEntities, entityProperties.Type) > -1 || slotProperties.availableEntities.Length == 0)
						{
							if (entityProperties.slobodanEntitet)
							{
								boomerangBabuni_postoji_u_poolu = 1;
								KorigujVerovatnocuZbogMisije(2, 3);
								boomerangBabuni_Kvota += boomerangBabuni_Kvota_locked;
								Transform child8 = enemyPool.GetChild(num16);
								float z8 = child8.localPosition.z;
								entityProperties.slobodanEntitet = false;
								entityProperties.trenutnoJeAktivan = false;
								if (entityProperties.Type != 15 || entityProperties.Type != 16 || entityProperties.Type != 17 || entityProperties.Type != 18)
								{
									for (int num17 = 0; num17 < child8.childCount; num17++)
									{
										((Component)child8.GetChild(num17).Find("BaboonReal").Find("_MajmunceNadrlja")).GetComponent<BabunDogadjaji_new>().ResetujBabuna();
									}
								}
								child8.position = new Vector3(prefabPropertiess.enemiesSlots[i].position.x, prefabPropertiess.enemiesSlots[i].position.y, z8);
								entityProperties.brojPojavljivanja++;
								flag = false;
								postavljen_u_prefabu_boomerangBabuni = 1;
							}
							else
							{
								boomerangBabuni_postoji_u_poolu = 2;
								postavljen_u_prefabu_boomerangBabuni = 2;
							}
						}
						else
						{
							postavljen_u_prefabu_boomerangBabuni = 2;
						}
					}
					if (boomerangBabuni_postoji_u_poolu == 2 && brojacPrefaba == (int)boomerangBabuni_Kvota && brojacPrefaba <= 7 && flag)
					{
						KorigujVerovatnocuZbogMisije(2, 3);
						boomerangBabuni_Kvota += boomerangBabuni_Kvota_locked;
						GameObject val8 = Object.Instantiate<GameObject>(enemiesForInstantiate[6], new Vector3(prefabPropertiess.enemiesSlots[i].position.x, prefabPropertiess.enemiesSlots[i].position.y, prefabPropertiess.enemiesSlots[i].position.z), Quaternion.identity);
						for (int num18 = 0; num18 < val8.transform.childCount; num18++)
						{
							((Component)val8.transform.GetChild(num18).Find("BaboonReal").Find("_MajmunceNadrlja")).GetComponent<BabunDogadjaji_new>().ResetujBabuna();
						}
						val8.transform.parent = enemyPool;
						entityProperties = val8.GetComponent<EntityProperties>();
						entityProperties.instanciran = true;
						entityProperties.slobodanEntitet = false;
						entityProperties.trenutnoJeAktivan = false;
						flag = false;
						boomerangBabuni_postoji_u_poolu = 0;
						postavljen_u_prefabu_boomerangBabuni = 1;
					}
				}
				if (kopljeGorile == 0 && enemyPool.childCount > 0)
				{
					if (suzenaListaObjekata.Count > 0)
					{
						suzenaListaObjekata.Clear();
					}
					for (int num19 = 0; num19 < enemyPool.childCount; num19++)
					{
						entityProperties = ((Component)enemyPool.GetChild(num19)).GetComponent<EntityProperties>();
						if (brojacPrefaba != (int)kopljeGorile_Kvota || brojacPrefaba > 7 || !(entityProperties.Type == 14 && flag))
						{
							continue;
						}
						if (Array.IndexOf(slotProperties.availableEntities, entityProperties.Type) > -1 || slotProperties.availableEntities.Length == 0)
						{
							if (entityProperties.slobodanEntitet)
							{
								kopljeGorile_postoji_u_poolu = 1;
								KorigujVerovatnocuZbogMisije(2, 4);
								kopljeGorile_Kvota += kopljeGorile_Kvota_locked;
								Transform child9 = enemyPool.GetChild(num19);
								float z9 = child9.localPosition.z;
								entityProperties.slobodanEntitet = false;
								entityProperties.trenutnoJeAktivan = false;
								if (entityProperties.Type != 15 || entityProperties.Type != 16 || entityProperties.Type != 17 || entityProperties.Type != 18)
								{
									for (int num20 = 0; num20 < child9.childCount; num20++)
									{
										((Component)child9.GetChild(num20).Find("BaboonReal").Find("_MajmunceNadrlja")).GetComponent<BabunDogadjaji_new>().ResetujBabuna();
									}
								}
								child9.position = new Vector3(prefabPropertiess.enemiesSlots[i].position.x, prefabPropertiess.enemiesSlots[i].position.y, z9);
								entityProperties.brojPojavljivanja++;
								flag = false;
								postavljen_u_prefabu_kopljeGorile = 1;
							}
							else
							{
								kopljeGorile_postoji_u_poolu = 2;
								postavljen_u_prefabu_kopljeGorile = 2;
							}
						}
						else
						{
							postavljen_u_prefabu_kopljeGorile = 2;
						}
					}
					if (kopljeGorile_postoji_u_poolu == 2 && brojacPrefaba == (int)kopljeGorile_Kvota && brojacPrefaba <= 7 && flag)
					{
						KorigujVerovatnocuZbogMisije(2, 4);
						kopljeGorile_Kvota += kopljeGorile_Kvota_locked;
						GameObject val9 = Object.Instantiate<GameObject>(enemiesForInstantiate[13], new Vector3(prefabPropertiess.enemiesSlots[i].position.x, prefabPropertiess.enemiesSlots[i].position.y, prefabPropertiess.enemiesSlots[i].position.z), Quaternion.identity);
						for (int num21 = 0; num21 < val9.transform.childCount; num21++)
						{
							((Component)val9.transform.GetChild(num21).Find("BaboonReal").Find("_MajmunceNadrlja")).GetComponent<BabunDogadjaji_new>().ResetujBabuna();
						}
						val9.transform.parent = enemyPool;
						entityProperties = val9.GetComponent<EntityProperties>();
						entityProperties.instanciran = true;
						entityProperties.slobodanEntitet = false;
						entityProperties.trenutnoJeAktivan = false;
						kopljeGorile_postoji_u_poolu = 0;
						postavljen_u_prefabu_kopljeGorile = 1;
					}
				}
			}
			yield return (object)new WaitForSeconds(0.05f);
			if (i == prefabPropertiess.enemiesSlots.Count - 1)
			{
				if (postavljen_u_prefabu_leteciBabuni == 2 && brojacPrefaba == (int)leteciBabuni_Kvota && brojacPrefaba <= 7)
				{
					leteciBabuni_Kvota += leteciBabuni_Kvota_locked;
				}
				if (postavljen_u_prefabu_leteceGorile == 2 && brojacPrefaba == (int)leteceGorile_Kvota && brojacPrefaba <= 7)
				{
					leteceGorile_Kvota += leteceGorile_Kvota_locked;
				}
				if (postavljen_u_prefabu_boomerangBabuni == 2 && brojacPrefaba == (int)boomerangBabuni_Kvota && brojacPrefaba <= 7)
				{
					boomerangBabuni_Kvota += boomerangBabuni_Kvota_locked;
				}
				if (postavljen_u_prefabu_kopljeGorile == 2 && brojacPrefaba == (int)kopljeGorile_Kvota && brojacPrefaba <= 7)
				{
					kopljeGorile_Kvota += kopljeGorile_Kvota_locked;
				}
			}
			if (leteciBabuni == 2)
			{
				KorigujVerovatnocuZbogMisije(0, 1);
			}
			if (leteceGorile == 2)
			{
				KorigujVerovatnocuZbogMisije(0, 2);
			}
			if (boomerangBabuni == 2)
			{
				KorigujVerovatnocuZbogMisije(0, 3);
			}
			if (kopljeGorile == 2)
			{
				KorigujVerovatnocuZbogMisije(0, 4);
			}
		}
		GC.Collect();
	}

	private IEnumerator DodavanjeSpecial(LevelPrefabProperties prefabPropertiess)
	{
		if (prefabPropertiess.special_Slots_Count <= 0)
		{
			yield break;
		}
		if (suzenaListaObjekata.Count > 0)
		{
			suzenaListaObjekata.Clear();
		}
		int trenutnaVerovatnoca = Random.Range(0, 100);
		for (int i = 0; i < prefabPropertiess.specialSlots.Count; i++)
		{
			slotProperties = ((Component)prefabPropertiess.specialSlots[i]).GetComponent<SlotProperties>();
			bool flag = true;
			if (slotProperties.Verovatnoca >= 100 - trenutnaVerovatnoca && specialPool.childCount > 0)
			{
				if (suzenaListaObjekata.Count > 0)
				{
					suzenaListaObjekata.Clear();
				}
				for (int j = 0; j < specialPool.childCount; j++)
				{
					entityProperties = ((Component)specialPool.GetChild(j)).GetComponent<EntityProperties>();
					if ((entityProperties.Type == 5 && crveniDijamant == 1) || (entityProperties.Type == 6 && plaviDijamant == 1) || (entityProperties.Type == 7 && zeleniDijamant == 1))
					{
						continue;
					}
					if (brojacPrefaba == (int)plaviDijamant_Kvota && brojacPrefaba <= 7 && plaviDijamant == 0 && entityProperties.Type == 6 && flag && (Array.IndexOf(slotProperties.availableEntities, entityProperties.Type) > -1 || slotProperties.availableEntities.Length == 0))
					{
						if (entityProperties.slobodanEntitet)
						{
							plaviDijamant_postoji_u_poolu = 1;
							KorigujVerovatnocuZbogMisije(2, 5);
							plaviDijamant_Kvota += plaviDijamant_Kvota_locked;
							Transform child = specialPool.GetChild(j);
							float z = child.localPosition.z;
							entityProperties.slobodanEntitet = false;
							entityProperties.trenutnoJeAktivan = false;
							child.position = new Vector3(prefabPropertiess.specialSlots[i].position.x, prefabPropertiess.specialSlots[i].position.y, z);
							entityProperties.brojPojavljivanja++;
							flag = false;
						}
						else
						{
							plaviDijamant_postoji_u_poolu = 2;
						}
					}
					if (brojacPrefaba == (int)crveniDijamant_Kvota && brojacPrefaba <= 7 && crveniDijamant == 0 && entityProperties.Type == 5 && flag)
					{
						if (entityProperties.slobodanEntitet)
						{
							crveniDijamant_postoji_u_poolu = 1;
							KorigujVerovatnocuZbogMisije(2, 6);
							crveniDijamant_Kvota += crveniDijamant_Kvota_locked;
							Transform child2 = specialPool.GetChild(j);
							float z2 = child2.localPosition.z;
							entityProperties.slobodanEntitet = false;
							entityProperties.trenutnoJeAktivan = false;
							child2.position = new Vector3(prefabPropertiess.specialSlots[i].position.x, prefabPropertiess.specialSlots[i].position.y, z2);
							entityProperties.brojPojavljivanja++;
							flag = false;
						}
						else
						{
							crveniDijamant_postoji_u_poolu = 2;
						}
					}
					if (brojacPrefaba == (int)zeleniDijamant_Kvota && brojacPrefaba <= 7 && zeleniDijamant == 0 && entityProperties.Type == 7 && flag)
					{
						if (entityProperties.slobodanEntitet)
						{
							zeleniDijamant_postoji_u_poolu = 1;
							KorigujVerovatnocuZbogMisije(2, 7);
							zeleniDijamant_Kvota += zeleniDijamant_Kvota_locked;
							Transform child3 = specialPool.GetChild(j);
							float z3 = child3.localPosition.z;
							entityProperties.slobodanEntitet = false;
							entityProperties.trenutnoJeAktivan = false;
							child3.position = new Vector3(prefabPropertiess.specialSlots[i].position.x, prefabPropertiess.specialSlots[i].position.y, z3);
							entityProperties.brojPojavljivanja++;
							flag = false;
						}
						else
						{
							zeleniDijamant_postoji_u_poolu = 2;
						}
					}
					if (entityProperties.slobodanEntitet && flag && entityProperties.minimumLevel <= level && (Array.IndexOf(slotProperties.availableEntities, entityProperties.Type) > -1 || slotProperties.availableEntities.Length == 0) && (!((Object)entityProperties).name.Contains("Magnet") || !magnetCollected) && (!((Object)entityProperties).name.Contains("Magnet") || brojacPrefaba >= 2) && (!((Object)entityProperties).name.Contains("DoubleCoins") || !doubleCoinsCollected) && (!((Object)entityProperties).name.Contains("DoubleCoins") || brojacPrefaba >= 2) && (!((Object)entityProperties).name.Contains("Shield") || !shieldCollected) && (!((Object)entityProperties).name.Contains("Shield") || brojacPrefaba >= 2))
					{
						suzenaListaObjekata.Add(j);
					}
				}
				if (plaviDijamant_postoji_u_poolu == 2 && brojacPrefaba == (int)plaviDijamant_Kvota && brojacPrefaba <= 7 && plaviDijamant == 0 && flag)
				{
					KorigujVerovatnocuZbogMisije(2, 5);
					plaviDijamant_Kvota += plaviDijamant_Kvota_locked;
					Object.Instantiate<GameObject>(specialsForInstantiate[1], new Vector3(prefabPropertiess.specialSlots[i].position.x, prefabPropertiess.specialSlots[i].position.y, prefabPropertiess.specialSlots[i].position.z), Quaternion.identity);
					flag = false;
					plaviDijamant_postoji_u_poolu = 0;
				}
				if (crveniDijamant_postoji_u_poolu == 2 && brojacPrefaba == (int)crveniDijamant_Kvota && brojacPrefaba <= 7 && crveniDijamant == 0 && flag)
				{
					KorigujVerovatnocuZbogMisije(2, 6);
					crveniDijamant_Kvota += crveniDijamant_Kvota_locked;
					Object.Instantiate<GameObject>(specialsForInstantiate[0], new Vector3(prefabPropertiess.specialSlots[i].position.x, prefabPropertiess.specialSlots[i].position.y, prefabPropertiess.specialSlots[i].position.z), Quaternion.identity);
					flag = false;
					crveniDijamant_postoji_u_poolu = 0;
				}
				if (zeleniDijamant_postoji_u_poolu == 2 && brojacPrefaba == (int)zeleniDijamant_Kvota && brojacPrefaba <= 7 && zeleniDijamant == 0 && flag)
				{
					KorigujVerovatnocuZbogMisije(2, 7);
					zeleniDijamant_Kvota += zeleniDijamant_Kvota_locked;
					Object.Instantiate<GameObject>(specialsForInstantiate[2], new Vector3(prefabPropertiess.specialSlots[i].position.x, prefabPropertiess.specialSlots[i].position.y, prefabPropertiess.specialSlots[i].position.z), Quaternion.identity);
					flag = false;
					zeleniDijamant_postoji_u_poolu = 0;
				}
				if (suzenaListaObjekata.Count > 0 && flag)
				{
					int index = Random.Range(0, suzenaListaObjekata.Count);
					Transform child4 = specialPool.GetChild(suzenaListaObjekata[index]);
					suzenaListaObjekata.RemoveAt(index);
					if ((Object)(object)child4 != (Object)null)
					{
						entityProperties = ((Component)child4).GetComponent<EntityProperties>();
						if (Random.Range(0, 100) >= 100 - entityProperties.Verovatnoca && (entityProperties.brojPojavljivanja < entityProperties.maxBrojPojavljivanja || entityProperties.maxBrojPojavljivanja == 0))
						{
							if (entityProperties.DozvoljenoSkaliranje)
							{
								float num = Random.Range(entityProperties.originalScale.x, 2f * entityProperties.originalScale.x);
								((Component)child4).transform.localScale = new Vector3(num, num, num);
							}
							float z4 = child4.localPosition.z;
							entityProperties.slobodanEntitet = false;
							entityProperties.trenutnoJeAktivan = false;
							child4.position = new Vector3(prefabPropertiess.specialSlots[i].position.x, prefabPropertiess.specialSlots[i].position.y, z4);
							entityProperties.brojPojavljivanja++;
						}
					}
				}
			}
			yield return (object)new WaitForSeconds(0.05f);
			if (plaviDijamant == 2)
			{
				KorigujVerovatnocuZbogMisije(0, 5);
			}
			if (crveniDijamant == 2)
			{
				KorigujVerovatnocuZbogMisije(0, 6);
			}
			if (zeleniDijamant == 2)
			{
				KorigujVerovatnocuZbogMisije(0, 7);
			}
		}
		yield return (object)new WaitForSeconds(0.5f);
		if (prviPrefab)
		{
			prviPrefab = false;
			KorigujVerovatnocuZbogMisije(0, 0);
		}
		GC.Collect();
	}

	public void PostaviFinish()
	{
		trebaFinish = true;
		MonkeyController2D.canRespawnThings = false;
	}

	private void Tezina()
	{
		int num = level / 20 + 1;
		int num2 = level % 20;
		if (num2 == 0)
		{
			num2 = 20;
			num--;
		}
		if (tour == 1)
		{
			switch (num2)
			{
			case 1:
			case 2:
			case 3:
			case 4:
				overallDifficulty = num;
				break;
			case 5:
			case 6:
			case 7:
			case 8:
				overallDifficulty = num + 1;
				break;
			case 9:
			case 10:
			case 11:
			case 12:
				overallDifficulty = num + 2;
				break;
			case 13:
			case 14:
			case 15:
			case 16:
				overallDifficulty = num + 3;
				break;
			case 17:
			case 18:
			case 19:
			case 20:
				overallDifficulty = num + 4;
				break;
			}
		}
		else if (tour == 2)
		{
			switch (num2)
			{
			case 1:
			case 2:
			case 3:
			case 4:
				overallDifficulty = num + 2;
				break;
			case 5:
			case 6:
			case 7:
			case 8:
				overallDifficulty = num + 3;
				break;
			case 9:
			case 10:
			case 11:
			case 12:
				overallDifficulty = num + 4;
				break;
			case 13:
			case 14:
			case 15:
			case 16:
				overallDifficulty = num + 5;
				break;
			case 17:
			case 18:
			case 19:
			case 20:
				overallDifficulty = num + 6;
				break;
			}
		}
		else if (tour == 3)
		{
			switch (num2)
			{
			case 1:
			case 2:
			case 3:
			case 4:
				overallDifficulty = num + 4;
				break;
			case 5:
			case 6:
			case 7:
			case 8:
				overallDifficulty = num + 5;
				break;
			case 9:
			case 10:
			case 11:
			case 12:
				overallDifficulty = num + 6;
				break;
			case 13:
			case 14:
			case 15:
			case 16:
				overallDifficulty = num + 7;
				break;
			case 17:
			case 18:
			case 19:
			case 20:
				overallDifficulty = num + 8;
				break;
			}
		}
		switch (overallDifficulty)
		{
		case 1:
			dg = 10;
			gg = 15;
			break;
		case 2:
			dg = 10;
			gg = 25;
			break;
		case 3:
			dg = 15;
			gg = 30;
			break;
		case 4:
			dg = 20;
			gg = 30;
			break;
		case 5:
			dg = 20;
			gg = 40;
			break;
		case 6:
			dg = 25;
			gg = 40;
			break;
		case 7:
			dg = 30;
			gg = 50;
			break;
		case 8:
			dg = 40;
			gg = 60;
			break;
		case 9:
			dg = 50;
			gg = 70;
			break;
		case 10:
			dg = 65;
			gg = 80;
			break;
		case 11:
			dg = 70;
			gg = 80;
			break;
		case 12:
			dg = 75;
			gg = 80;
			break;
		case 13:
			dg = 75;
			gg = 85;
			break;
		case 14:
			dg = 80;
			gg = 90;
			break;
		case 15:
			dg = 90;
			gg = 100;
			break;
		case 16:
			dg = 100;
			gg = 100;
			break;
		}
	}

	private void postaviInicijalnuTezinu(LevelPrefabProperties prefabPropertiess)
	{
		switch (overallDifficulty)
		{
		case 1:
			dg = 10;
			gg = 15;
			break;
		case 2:
			dg = 10;
			gg = 25;
			break;
		case 3:
			dg = 15;
			gg = 30;
			break;
		case 4:
			dg = 20;
			gg = 30;
			break;
		case 5:
			dg = 20;
			gg = 40;
			break;
		case 6:
			dg = 25;
			gg = 40;
			break;
		case 7:
			dg = 30;
			gg = 50;
			break;
		case 8:
			dg = 40;
			gg = 60;
			break;
		case 9:
			dg = 50;
			gg = 70;
			break;
		case 10:
			dg = 65;
			gg = 80;
			break;
		case 11:
			dg = 70;
			gg = 80;
			break;
		case 12:
			dg = 75;
			gg = 80;
			break;
		case 13:
			dg = 75;
			gg = 85;
			break;
		case 14:
			dg = 80;
			gg = 90;
			break;
		case 15:
			dg = 90;
			gg = 100;
			break;
		case 16:
			dg = 100;
			gg = 100;
			break;
		}
		int verovatnoca = Random.Range(dg, gg);
		for (int i = 0; i < prefabPropertiess.enemiesSlots.Count; i++)
		{
			((Component)prefabPropertiess.enemiesSlots[i]).GetComponent<SlotProperties>().Verovatnoca = verovatnoca;
		}
	}

	public void KorigujVerovatnocuZbogMisije(int value, int tip)
	{
		if (MissionManager.activeFly_BaboonsMission && (tip == 0 || tip == 1))
		{
			if (value == 1)
			{
				if ((int)leteciBabuni_Kvota == 7)
				{
					leteciBabuni = value;
				}
			}
			else
			{
				leteciBabuni = value;
			}
		}
		if (MissionManager.activeFly_GorillaMission && (tip == 0 || tip == 2))
		{
			if (value == 1)
			{
				if ((int)leteceGorile_Kvota == 7)
				{
					leteceGorile = value;
				}
			}
			else
			{
				leteceGorile = value;
			}
		}
		if (MissionManager.activeBoomerang_BaboonsMission && (tip == 0 || tip == 3))
		{
			if (value == 1)
			{
				if ((int)boomerangBabuni_Kvota == 7)
				{
					boomerangBabuni = value;
				}
			}
			else
			{
				boomerangBabuni = value;
			}
		}
		if (MissionManager.activeKoplje_GorillaMission && (tip == 0 || tip == 4))
		{
			if (value == 1)
			{
				if ((int)kopljeGorile_Kvota == 7)
				{
					kopljeGorile = value;
				}
			}
			else
			{
				kopljeGorile = value;
			}
		}
		if (MissionManager.activeBlueDiamondsMission && (tip == 0 || tip == 5))
		{
			if (value == 1)
			{
				if ((int)plaviDijamant_Kvota == 7)
				{
					plaviDijamant = value;
				}
			}
			else
			{
				plaviDijamant = value;
			}
		}
		if (MissionManager.activeRedDiamondsMission && (tip == 0 || tip == 6))
		{
			if (value == 1)
			{
				if ((int)crveniDijamant_Kvota == 7)
				{
					crveniDijamant = value;
				}
			}
			else
			{
				crveniDijamant = value;
			}
		}
		if (!MissionManager.activeGreenDiamondsMission || (tip != 0 && tip != 7))
		{
			return;
		}
		if (value == 1)
		{
			if ((int)zeleniDijamant_Kvota == 7)
			{
				zeleniDijamant = value;
			}
		}
		else
		{
			zeleniDijamant = value;
		}
	}

	public void izbrojPosebne()
	{
		if (MissionManager.activeFly_BaboonsMission)
		{
			brojPosebnihNeprijatelja++;
		}
		if (MissionManager.activeFly_GorillaMission)
		{
			brojPosebnihNeprijatelja++;
		}
		if (MissionManager.activeBoomerang_BaboonsMission)
		{
			brojPosebnihNeprijatelja++;
		}
		if (MissionManager.activeKoplje_GorillaMission)
		{
			brojPosebnihNeprijatelja++;
		}
		if (MissionManager.activeBlueDiamondsMission)
		{
			brojPosebnihDijamanata++;
		}
		if (MissionManager.activeRedDiamondsMission)
		{
			brojPosebnihDijamanata++;
		}
		if (MissionManager.activeGreenDiamondsMission)
		{
			brojPosebnihDijamanata++;
		}
	}

	private bool proveriDaLiNeprijateljUcestvujeUMisiji(int type)
	{
		if (type == 3 && leteciBabuni == 2)
		{
			return true;
		}
		if (type == 4 && boomerangBabuni == 2)
		{
			return true;
		}
		if (type == 5 && leteceGorile == 2)
		{
			return true;
		}
		if (type == 6 && kopljeGorile == 2)
		{
			return true;
		}
		return false;
	}

	private bool proveriDaLiDijamantUcestvujeUMisiji(int type)
	{
		if (type == 5 && crveniDijamant == 2)
		{
			return true;
		}
		if (type == 6 && plaviDijamant == 2)
		{
			return true;
		}
		if (type == 7 && zeleniDijamant == 2)
		{
			return true;
		}
		return false;
	}
}
