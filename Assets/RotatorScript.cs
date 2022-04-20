using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorScript : MonoBehaviour
{
	public float angle;
	
    private void Update()
    {
        transform.Rotate(Vector3.up,angle * Time.deltaTime);
    }
}
