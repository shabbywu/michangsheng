using UnityEngine;
using UnityEngine.EventSystems;

namespace EpicToonFX;

public class ETFXFireProjectile : MonoBehaviour
{
	private RaycastHit hit;

	public GameObject[] projectiles;

	public Transform spawnPosition;

	[HideInInspector]
	public int currentProjectile;

	public float speed = 1000f;

	private ETFXButtonScript selectedProjectileButton;

	private void Start()
	{
		selectedProjectileButton = GameObject.Find("Button").GetComponent<ETFXButtonScript>();
	}

	private void Update()
	{
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		if (Input.GetKeyDown((KeyCode)275))
		{
			nextEffect();
		}
		if (Input.GetKeyDown((KeyCode)100))
		{
			nextEffect();
		}
		if (Input.GetKeyDown((KeyCode)97))
		{
			previousEffect();
		}
		else if (Input.GetKeyDown((KeyCode)276))
		{
			previousEffect();
		}
		if (Input.GetKeyDown((KeyCode)323) && !EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), ref hit, 100f))
		{
			GameObject val = Object.Instantiate<GameObject>(projectiles[currentProjectile], spawnPosition.position, Quaternion.identity);
			val.transform.LookAt(((RaycastHit)(ref hit)).point);
			val.GetComponent<Rigidbody>().AddForce(val.transform.forward * speed);
			val.GetComponent<ETFXProjectileScript>().impactNormal = ((RaycastHit)(ref hit)).normal;
		}
		Ray val2 = Camera.main.ScreenPointToRay(Input.mousePosition);
		Vector3 origin = ((Ray)(ref val2)).origin;
		val2 = Camera.main.ScreenPointToRay(Input.mousePosition);
		Debug.DrawRay(origin, ((Ray)(ref val2)).direction * 100f, Color.yellow);
	}

	public void nextEffect()
	{
		if (currentProjectile < projectiles.Length - 1)
		{
			currentProjectile++;
		}
		else
		{
			currentProjectile = 0;
		}
		selectedProjectileButton.getProjectileNames();
	}

	public void previousEffect()
	{
		if (currentProjectile > 0)
		{
			currentProjectile--;
		}
		else
		{
			currentProjectile = projectiles.Length - 1;
		}
		selectedProjectileButton.getProjectileNames();
	}

	public void AdjustSpeed(float newSpeed)
	{
		speed = newSpeed;
	}
}
