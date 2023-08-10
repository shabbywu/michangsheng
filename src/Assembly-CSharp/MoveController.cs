using System;
using UnityEngine;

[Obsolete]
public class MoveController : MonoBehaviour
{
	private Animator animator;

	private SmoothFollow sf;

	private Transform moveDes;

	private bool hasDes;

	private float minLen;

	private int skillId = 1;

	private GameObject UIGame;

	private GameEntity gameEntity;
}
