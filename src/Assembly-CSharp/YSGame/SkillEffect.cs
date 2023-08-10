using UnityEngine;

namespace YSGame;

public class SkillEffect : MonoBehaviour
{
	public GameObject FirePoint;

	public Camera Cam;

	public float MaxLength;

	public GameObject[] Prefabs;

	private Ray RayMouse;

	private Vector3 direction;

	private Quaternion rotation;

	[Header("GUI")]
	private float windowDpi;

	private int Prefab;

	private GameObject Instance;

	private float hSliderValue = 0.1f;

	private float fireCountdown;

	private float buttonSaver;

	private void Start()
	{
	}

	public void createSkillEffect()
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		Object.Instantiate<GameObject>(Prefabs[Prefab], FirePoint.transform.position, FirePoint.transform.rotation);
	}
}
