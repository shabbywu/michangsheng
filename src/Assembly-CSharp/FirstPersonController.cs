using KBEngine;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
	public float movementspeed = 5f;

	public float mouseSensitivity = 2f;

	public float verticalAngleLimit = 60f;

	public float jumpSpeed = 5f;

	private float verticalRotation;

	private GameObject _inventory;

	private GameObject _tooltip;

	private GameObject _character;

	private GameObject _dropBox;

	public bool showInventory;

	private float verticalVelocity;

	private GameObject inventory;

	private GameObject craftSystem;

	private GameObject characterSystem;

	private Camera firstPersonCamera;

	private CharacterController characterController;

	private void Start()
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Expected O, but got Unknown
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		firstPersonCamera = ((Component)Camera.main).GetComponent<Camera>();
		characterController = ((Component)this).GetComponent<CharacterController>();
		if ((Object)(GameObject)KBEngineApp.app.player().renderObj != (Object)null)
		{
			PlayerInventory component = ((GameObject)KBEngineApp.app.player().renderObj).GetComponent<PlayerInventory>();
			if ((Object)(object)component.inventory != (Object)null)
			{
				inventory = component.inventory;
			}
			if ((Object)(object)component.craftSystem != (Object)null)
			{
				craftSystem = component.craftSystem;
			}
			if ((Object)(object)component.characterSystem != (Object)null)
			{
				characterSystem = component.characterSystem;
			}
		}
	}

	private void Update()
	{
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		if (!lockMovement())
		{
			float num = Input.GetAxis("Mouse X") * mouseSensitivity;
			((Component)this).transform.Rotate(0f, num, 0f);
			verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
			verticalRotation = Mathf.Clamp(verticalRotation, 0f - verticalAngleLimit, verticalAngleLimit);
			((Component)firstPersonCamera).transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
			float num2 = Input.GetAxis("Vertical") * movementspeed;
			float num3 = Input.GetAxis("Horizontal") * movementspeed;
			verticalVelocity += Physics.gravity.y * Time.deltaTime;
			if (Input.GetButtonDown("Jump") && characterController.isGrounded)
			{
				verticalVelocity = jumpSpeed;
			}
			Vector3 val = default(Vector3);
			((Vector3)(ref val))._002Ector(num3, verticalVelocity, num2);
			val = ((Component)this).transform.rotation * val;
			characterController.Move(val * Time.deltaTime);
		}
	}

	private bool lockMovement()
	{
		if ((Object)(object)inventory != (Object)null && inventory.activeSelf)
		{
			return true;
		}
		if ((Object)(object)characterSystem != (Object)null && characterSystem.activeSelf)
		{
			return true;
		}
		if ((Object)(object)craftSystem != (Object)null && craftSystem.activeSelf)
		{
			return true;
		}
		return false;
	}
}
