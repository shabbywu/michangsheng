using System.Collections.Generic;
using UnityEngine;

public class CameraHolder : MonoBehaviour
{
	public struct SVA
	{
		public float S;

		public float V;

		public float A;
	}

	public Transform Holder;

	public float currDistance = 5f;

	public float xRotate = 250f;

	public float yRotate = 120f;

	public float yMinLimit = -20f;

	public float yMaxLimit = 80f;

	public float prevDistance;

	private float x;

	private float y;

	[Header("GUI")]
	private float windowDpi;

	public GameObject[] Prefabs;

	private int Prefab;

	private GameObject Instance;

	private float StartColor;

	private float HueColor;

	public Texture HueTexture;

	private ParticleSystem[] particleSystems = (ParticleSystem[])(object)new ParticleSystem[0];

	private List<SVA> svList = new List<SVA>();

	private float H;

	private void Start()
	{
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		if (Screen.dpi < 1f)
		{
			windowDpi = 1f;
		}
		if (Screen.dpi < 200f)
		{
			windowDpi = 1f;
		}
		else
		{
			windowDpi = Screen.dpi / 200f;
		}
		Vector3 eulerAngles = ((Component)this).transform.eulerAngles;
		x = eulerAngles.y;
		y = eulerAngles.x;
		Counter(0);
	}

	private void OnGUI()
	{
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		if (GUI.Button(new Rect(5f * windowDpi, 5f * windowDpi, 110f * windowDpi, 35f * windowDpi), "Previous effect"))
		{
			Counter(-1);
		}
		if (GUI.Button(new Rect(120f * windowDpi, 5f * windowDpi, 110f * windowDpi, 35f * windowDpi), "Play again"))
		{
			Counter(0);
		}
		if (GUI.Button(new Rect(235f * windowDpi, 5f * windowDpi, 110f * windowDpi, 35f * windowDpi), "Next effect"))
		{
			Counter(1);
		}
		StartColor = HueColor;
		HueColor = GUI.HorizontalSlider(new Rect(5f * windowDpi, 45f * windowDpi, 340f * windowDpi, 35f * windowDpi), HueColor, 0f, 1f);
		GUI.DrawTexture(new Rect(5f * windowDpi, 65f * windowDpi, 340f * windowDpi, 15f * windowDpi), HueTexture, (ScaleMode)0, false, 0f);
		if (HueColor != StartColor)
		{
			int num = 0;
			ParticleSystem[] array = particleSystems;
			for (int i = 0; i < array.Length; i++)
			{
				MainModule main = array[i].main;
				Color val = Color.HSVToRGB(HueColor + H * 0f, svList[num].S, svList[num].V);
				((MainModule)(ref main)).startColor = MinMaxGradient.op_Implicit(new Color(val.r, val.g, val.b, svList[num].A));
				num++;
			}
		}
	}

	private void Counter(int count)
	{
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		Prefab += count;
		if (Prefab > Prefabs.Length - 1)
		{
			Prefab = 0;
		}
		else if (Prefab < 0)
		{
			Prefab = Prefabs.Length - 1;
		}
		if ((Object)(object)Instance != (Object)null)
		{
			Object.Destroy((Object)(object)Instance);
		}
		Instance = Object.Instantiate<GameObject>(Prefabs[Prefab]);
		particleSystems = Instance.GetComponentsInChildren<ParticleSystem>();
		svList.Clear();
		ParticleSystem[] array = particleSystems;
		for (int i = 0; i < array.Length; i++)
		{
			MainModule main = array[i].main;
			MinMaxGradient startColor = ((MainModule)(ref main)).startColor;
			Color color = ((MinMaxGradient)(ref startColor)).color;
			SVA item = default(SVA);
			Color.RGBToHSV(color, ref H, ref item.S, ref item.V);
			item.A = color.a;
			svList.Add(item);
		}
	}

	private void LateUpdate()
	{
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		if (currDistance < 2f)
		{
			currDistance = 2f;
		}
		currDistance -= Input.GetAxis("Mouse ScrollWheel") * 2f;
		if (Object.op_Implicit((Object)(object)Holder) && (Input.GetMouseButton(0) || Input.GetMouseButton(1)))
		{
			Vector3 mousePosition = Input.mousePosition;
			float num = 1f;
			if (Screen.dpi < 1f)
			{
				num = 1f;
			}
			num = ((!(Screen.dpi < 200f)) ? (Screen.dpi / 200f) : 1f);
			if (mousePosition.x < 380f * num && (float)Screen.height - mousePosition.y < 250f * num)
			{
				return;
			}
			Cursor.visible = false;
			Cursor.lockState = (CursorLockMode)1;
			x += (float)((double)(Input.GetAxis("Mouse X") * xRotate) * 0.02);
			y -= (float)((double)(Input.GetAxis("Mouse Y") * yRotate) * 0.02);
			y = ClampAngle(y, yMinLimit, yMaxLimit);
			Quaternion val = Quaternion.Euler(y, x, 0f);
			Vector3 position = val * new Vector3(0f, 0f, 0f - currDistance) + Holder.position;
			((Component)this).transform.rotation = val;
			((Component)this).transform.position = position;
		}
		else
		{
			Cursor.visible = true;
			Cursor.lockState = (CursorLockMode)0;
		}
		if (prevDistance != currDistance)
		{
			prevDistance = currDistance;
			Quaternion val2 = Quaternion.Euler(y, x, 0f);
			Vector3 position2 = val2 * new Vector3(0f, 0f, 0f - currDistance) + Holder.position;
			((Component)this).transform.rotation = val2;
			((Component)this).transform.position = position2;
		}
	}

	private static float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360f)
		{
			angle += 360f;
		}
		if (angle > 360f)
		{
			angle -= 360f;
		}
		return Mathf.Clamp(angle, min, max);
	}
}
