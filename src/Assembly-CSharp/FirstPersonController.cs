using System;
using KBEngine;
using UnityEngine;

// Token: 0x0200012C RID: 300
[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
	// Token: 0x06000E1B RID: 3611 RVA: 0x00053540 File Offset: 0x00051740
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

	// Token: 0x06000E1C RID: 3612 RVA: 0x000535F0 File Offset: 0x000517F0
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

	// Token: 0x06000E1D RID: 3613 RVA: 0x00053730 File Offset: 0x00051930
	private bool lockMovement()
	{
		return (this.inventory != null && this.inventory.activeSelf) || (this.characterSystem != null && this.characterSystem.activeSelf) || (this.craftSystem != null && this.craftSystem.activeSelf);
	}

	// Token: 0x04000A06 RID: 2566
	public float movementspeed = 5f;

	// Token: 0x04000A07 RID: 2567
	public float mouseSensitivity = 2f;

	// Token: 0x04000A08 RID: 2568
	public float verticalAngleLimit = 60f;

	// Token: 0x04000A09 RID: 2569
	public float jumpSpeed = 5f;

	// Token: 0x04000A0A RID: 2570
	private float verticalRotation;

	// Token: 0x04000A0B RID: 2571
	private GameObject _inventory;

	// Token: 0x04000A0C RID: 2572
	private GameObject _tooltip;

	// Token: 0x04000A0D RID: 2573
	private GameObject _character;

	// Token: 0x04000A0E RID: 2574
	private GameObject _dropBox;

	// Token: 0x04000A0F RID: 2575
	public bool showInventory;

	// Token: 0x04000A10 RID: 2576
	private float verticalVelocity;

	// Token: 0x04000A11 RID: 2577
	private GameObject inventory;

	// Token: 0x04000A12 RID: 2578
	private GameObject craftSystem;

	// Token: 0x04000A13 RID: 2579
	private GameObject characterSystem;

	// Token: 0x04000A14 RID: 2580
	private Camera firstPersonCamera;

	// Token: 0x04000A15 RID: 2581
	private CharacterController characterController;
}
