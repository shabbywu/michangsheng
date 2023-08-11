using System.Collections;
using KBEngine;
using UnityEngine;

public class IslandHolderManager : MonoBehaviour
{
	private GameObject Kamera;

	private int[] Klik = new int[24]
	{
		0, 1, 2, 3, 4, 5, 6, 7, 8, 9,
		10, 11, 12, 13, 14, 15, 16, 17, 18, 19,
		20, 21, 22, 23
	};

	private int KliknutoNa;

	private float TrenutniX;

	private float TrenutniY;

	private float forceX;

	private float forceY;

	private float startX;

	private float endX;

	private float startY;

	private float endY;

	private void Awake()
	{
		Kamera = GameObject.Find("Main Camera");
	}

	private void Start()
	{
	}

	public void creatAvatar(int avaterID, int roleType, int HP_Max, Vector3 position, Vector3 direction)
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		KBEngineApp.app.Client_onCreatedProxies((ulong)avaterID, avaterID, "Avatar");
		Avatar obj = (Avatar)KBEngineApp.app.entities[avaterID];
		obj.roleTypeCell = (uint)roleType;
		obj.position = position;
		obj.direction = direction;
		obj.HP_Max = HP_Max;
		obj.HP = HP_Max;
	}

	private void Update()
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		if (Input.GetKeyUp((KeyCode)27))
		{
			Application.LoadLevel("All Maps");
		}
		Input.GetKeyUp((KeyCode)9);
		endX = Input.mousePosition.x;
		endY = Input.mousePosition.y;
		if (Input.GetMouseButtonDown(0))
		{
			startX = Input.mousePosition.x;
			startY = Input.mousePosition.y;
			string text = RaycastFunction(Input.mousePosition);
			for (int i = 0; i < 100; i++)
			{
				if (text == string.Concat(i))
				{
					KliknutoNa = i;
					break;
				}
			}
			if (RaycastFunction(Input.mousePosition) == "HouseShop")
			{
				KliknutoNa = 21;
			}
			else if (RaycastFunction(Input.mousePosition) == "HolderShipFreeCoins")
			{
				KliknutoNa = 22;
			}
			else if (RaycastFunction(Input.mousePosition) == "ButtonBack")
			{
				KliknutoNa = 23;
			}
		}
		if (!Input.GetMouseButtonUp(0))
		{
			return;
		}
		endX = Input.mousePosition.x;
		endY = Input.mousePosition.y;
		forceX = (0f - (endX - startX)) / (90f * ((Component)this).GetComponent<Camera>().aspect) * 5f;
		forceY = (0f - (endY - startY)) / (90f * ((Component)this).GetComponent<Camera>().aspect);
		string text2 = RaycastFunction(Input.mousePosition);
		for (int j = 0; j < 100; j++)
		{
			if (!(text2 == string.Concat(j)))
			{
				continue;
			}
			if (KliknutoNa == Klik[j])
			{
				Debug.Log((object)("Level " + j));
				if (!UINPCJiaoHu.Inst.NowIsJiaoHu)
				{
					RaycastHit val = RaycastObj(Input.mousePosition);
					((Component)((RaycastHit)(ref val)).collider).GetComponent<MapComponent>().EventRandom();
				}
			}
			else
			{
				PomeriKameru();
			}
			break;
		}
		if (RaycastFunction(Input.mousePosition) == "HouseShop")
		{
			if (KliknutoNa == Klik[21])
			{
				Debug.Log((object)"HouseShop");
			}
			else
			{
				PomeriKameru();
			}
		}
		else if (RaycastFunction(Input.mousePosition) == "HolderShipFreeCoins")
		{
			if (KliknutoNa == Klik[22])
			{
				Debug.Log((object)"HolderShipFreeCoins");
			}
			else
			{
				PomeriKameru();
			}
		}
		else if (RaycastFunction(Input.mousePosition) == "ButtonBack")
		{
			if (KliknutoNa == Klik[23])
			{
				Debug.Log((object)"ButtonBack");
				Debug.Log((object)"Objasni mi ovo");
				((MonoBehaviour)this).StartCoroutine(UcitajOstrvo("All Maps"));
			}
			else
			{
				PomeriKameru();
			}
		}
	}

	private void PomeriKameru()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		TrenutniX = Kamera.transform.position.x;
		TrenutniY = Kamera.transform.position.y;
		if (TrenutniX <= -11.87f && forceX < 0f)
		{
			Debug.Log((object)"Leva granica");
		}
		else if (TrenutniX >= 11f && forceX > 0f)
		{
			Debug.Log((object)"Desna  granica");
		}
		else if (TrenutniX + forceX >= -11.87f && TrenutniX + forceX <= 11f)
		{
			if (TrenutniX + forceX >= -11.87f)
			{
				Debug.Log((object)"Desno");
				Kamera.transform.Translate(forceX, forceY, 0f);
			}
			else if (TrenutniX + forceX <= 11f)
			{
				Debug.Log((object)"Levo");
				Kamera.transform.Translate(forceX, forceY, 0f);
			}
			else
			{
				Debug.Log((object)"Novo");
			}
		}
		else if (TrenutniX + forceX < -11.87f)
		{
			Kamera.GetComponent<Transform>().position = new Vector3(-11.87f, Kamera.transform.position.y, -10f);
		}
		else if (TrenutniX + forceX > 11f)
		{
			Kamera.GetComponent<Transform>().position = new Vector3(11f, Kamera.transform.position.y, -10f);
		}
	}

	private string RaycastFunction(Vector3 vector)
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		RaycastHit val = default(RaycastHit);
		if (Physics.Raycast(Camera.main.ScreenPointToRay(vector), ref val))
		{
			return ((Object)((RaycastHit)(ref val)).collider).name;
		}
		return "";
	}

	private RaycastHit RaycastObj(Vector3 vector)
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		RaycastHit result = default(RaycastHit);
		Physics.Raycast(Camera.main.ScreenPointToRay(vector), ref result);
		return result;
	}

	private IEnumerator UcitajOstrvo(string ime)
	{
		GameObject.Find("OblaciPomeranje").GetComponent<Animation>().Play("OblaciPostavljanje");
		yield return (object)new WaitForSeconds(0.85f);
		Application.LoadLevel(ime);
	}
}
