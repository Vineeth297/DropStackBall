using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceMoverScript : MonoBehaviour
{
	private Camera _camera;
	public LayerMask layerMask;

	public Transform selectedTransform;
	public Transform finalTransform;

	public bool pieceSelected;

	public List<GameObject> checkBoxes;

	public Vector3[,] checkBoxPositions = new Vector3[8,8];
	private void Start()
	{
		_camera = Camera.main;
		FillTheBoxes();
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			var ray =  _camera.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out var hit, 50f,layerMask))
			{
				if (pieceSelected)
				{
					if(hit.collider.CompareTag("Piece")) return;
					finalTransform = hit.collider.transform;
					selectedTransform.position = finalTransform.GetChild(0).position;
					/*selectedTransform.position = Vector3.MoveTowards(selectedTransform.position,
						finalTransform.GetChild(0).position, Time.deltaTime * moveSpeed);*/
					pieceSelected = !pieceSelected;
				}
				if(!pieceSelected)
					if (hit.collider.CompareTag("Piece"))
					{
						selectedTransform = hit.collider.gameObject.transform;
						pieceSelected = !pieceSelected;
					}
			}
		}
	}

	private void SoldierFunction()
	{
		
		//can only move one step forward or crossways
		if (selectedTransform.parent.CompareTag("Position"))
		{
			
		}
	}

	private void FillTheBoxes()
	{
		for (var i = 0; i < 8; i++)
		{
			for (var j = 0; j < 8; j++)
			{
				checkBoxPositions[i, j] = checkBoxes[i * 8 + j].transform.position;
				Debug.DrawRay(checkBoxPositions[i, j], Vector3.up, Color.red, 2f);
			}
		}
	}
}
