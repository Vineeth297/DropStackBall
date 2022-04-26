using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBox : MonoBehaviour
{
	public bool isOccupied;
	
	private void Start()
	{
		if (transform.childCount == 2)
			isOccupied = true;
	}
}
