using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
	[SerializeField] private float speed;

	private void Update()
	{
		if (Input.GetMouseButton(0))
		{
			transform.Translate(Vector3.down * (Time.deltaTime * speed));
		}
	}
}
