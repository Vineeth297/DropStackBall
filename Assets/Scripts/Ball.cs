using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
	[SerializeField] private float speed;

	private void Update()
	{
		if(Input.GetMouseButtonDown(0))
			print("Tap");
		if (Input.GetMouseButton(0))
		{
			print("Held");
			transform.Translate(Vector3.down * (Time.deltaTime * speed));
		}
		
	}
}
