using System;
using KBEngine;
using UnityEngine;

// Token: 0x020001FD RID: 509
[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
	// Token: 0x06001029 RID: 4137 RVA: 0x000A3C90 File Offset: 0x000A1E90
	private void Start()
	{
		this.firstPersonCamera = Camera.main.GetComponent<Camera>();
		this.characterController = base.GetComponent<CharacterController>();
		if ((GameObject)KBEngineApp.app.player().renderObj != null)
		{
			PlayerInventory component = ((GameObject)KBEngineApp.app.player().renderObj).GetComponent<PlayerInventory>();
			if (component.inventory != null)
			{
				this.inventory = component.inventory;
			}
			if (component.craftSystem != null)
			{
				this.craftSystem = component.craftSystem;
			}
			if (component.characterSystem != null)
			{
				this.characterSystem = component.characterSystem;
			}
		}
	}

	// Token: 0x0600102A RID: 4138 RVA: 0x000A3D40 File Offset: 0x000A1F40
	private void Update()
	{
		if (!this.lockMovement())
		{
			float num = Input.GetAxis("Mouse X") * this.mouseSensitivity;
			base.transform.Rotate(0f, num, 0f);
			this.verticalRotation -= Input.GetAxis("Mouse Y") * this.mouseSensitivity;
			this.verticalRotation = Mathf.Clamp(this.verticalRotation, -this.verticalAngleLimit, this.verticalAngleLimit);
			this.firstPersonCamera.transform.localRotation = Quaternion.Euler(this.verticalRotation, 0f, 0f);
			float num2 = Input.GetAxis("Vertical") * this.movementspeed;
			float num3 = Input.GetAxis("Horizontal") * this.movementspeed;
			this.verticalVelocity += Physics.gravity.y * Time.deltaTime;
			if (Input.GetButtonDown("Jump") && this.characterController.isGrounded)
			{
				this.verticalVelocity = this.jumpSpeed;
			}
			Vector3 vector;
			vector..ctor(num3, this.verticalVelocity, num2);
			vector = base.transform.rotation * vector;
			this.characterController.Move(vector * Time.deltaTime);
		}
	}

	// Token: 0x0600102B RID: 4139 RVA: 0x000A3E80 File Offset: 0x000A2080
	private bool lockMovement()
	{
		return (this.inventory != null && this.inventory.activeSelf) || (this.characterSystem != null && this.characterSystem.activeSelf) || (this.craftSystem != null && this.craftSystem.activeSelf);
	}

	// Token: 0x04000C9E RID: 3230
	public float movementspeed = 5f;

	// Token: 0x04000C9F RID: 3231
	public float mouseSensitivity = 2f;

	// Token: 0x04000CA0 RID: 3232
	public float verticalAngleLimit = 60f;

	// Token: 0x04000CA1 RID: 3233
	public float jumpSpeed = 5f;

	// Token: 0x04000CA2 RID: 3234
	private float verticalRotation;

	// Token: 0x04000CA3 RID: 3235
	private GameObject _inventory;

	// Token: 0x04000CA4 RID: 3236
	private GameObject _tooltip;

	// Token: 0x04000CA5 RID: 3237
	private GameObject _character;

	// Token: 0x04000CA6 RID: 3238
	private GameObject _dropBox;

	// Token: 0x04000CA7 RID: 3239
	public bool showInventory;

	// Token: 0x04000CA8 RID: 3240
	private float verticalVelocity;

	// Token: 0x04000CA9 RID: 3241
	private GameObject inventory;

	// Token: 0x04000CAA RID: 3242
	private GameObject craftSystem;

	// Token: 0x04000CAB RID: 3243
	private GameObject characterSystem;

	// Token: 0x04000CAC RID: 3244
	private Camera firstPersonCamera;

	// Token: 0x04000CAD RID: 3245
	private CharacterController characterController;
}
