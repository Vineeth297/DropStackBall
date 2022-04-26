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

	public Vector3[] checkBoxPositions = new Vector3[64];
	
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
					//SoldierFunction(finalTransform);
					RookFunction(finalTransform);
				}

				if (!pieceSelected)
				{
					if (hit.collider.CompareTag("Piece"))
					{
						selectedTransform = hit.collider.gameObject.transform;
						pieceSelected = !pieceSelected;
						//check if its valid move ??

					}
				}
			}
		}
	}

	private void SoldierFunction(Transform finalMove)
	{
		var currentIndex = Array.IndexOf(checkBoxPositions, selectedTransform.parent.position);
		var validMoveIndex = currentIndex + 8;
		var killerMoveIndex1 = currentIndex + 7;
		var killerMoveIndex2 = currentIndex + 9;
		
		if (Array.IndexOf(checkBoxPositions, finalMove.position) == validMoveIndex)
		{
			//if target checkBox is not occupied => then move
			ValidateOccupancyAndMove(finalMove);
		}
		else if (Array.IndexOf(checkBoxPositions, finalMove.position) == killerMoveIndex1 || 
				 Array.IndexOf(checkBoxPositions, finalMove.position) == killerMoveIndex2)
		{
			//if the positions are occupied by enemy => kill
			KillTheEnemy(finalMove);
		}
		else
			InvalidMove();
	}

	private void FillTheBoxes()
	{
		for (var i = 0; i < 64; i++)
		{
			checkBoxPositions[i] = checkBoxes[i].transform.position;
			Debug.DrawRay(checkBoxPositions[i], Vector3.up, Color.red, 2f);
		}
	}

	private static void InvalidMove()
	{
		print("Invalid Move");
	}

	private void RookFunction(Transform finalMove)
	{
		//final position is divisible by 8
		var currentIndex = Array.IndexOf(checkBoxPositions, selectedTransform.parent.position);
		var finalPositionIndex = Array.IndexOf(checkBoxPositions, finalMove.position);
		var remainder = (Mathf.Abs(currentIndex - finalPositionIndex)) % 8;
		
		//Vertical Switch
		if (remainder == 0)
		{
			var maxIterationNumber = Mathf.Abs(currentIndex - finalPositionIndex) / 8;
			var numberOfEmptyBoxes = 0;
			
			for (var i = 1; i <= maxIterationNumber; i++)
			{
				
				if (currentIndex < finalPositionIndex)
				{
					if (!checkBoxes[currentIndex + (8 * i)].GetComponent<CheckBox>().isOccupied)
					{
						print(checkBoxes[currentIndex + (8 * i)].GetComponent<CheckBox>().isOccupied);
						numberOfEmptyBoxes += 1;
					}
					else
					{
						InvalidMove();
						return;
					}
				}
				if (currentIndex > finalPositionIndex)
				{
					if (!checkBoxes[currentIndex - (8 * i)].GetComponent<CheckBox>().isOccupied)
					{
						print(checkBoxes[currentIndex - (8 * i)].GetComponent<CheckBox>().isOccupied);
						numberOfEmptyBoxes += 1;
					}
					else
					{
						InvalidMove();
						return;
					}
				}
			}

			if(numberOfEmptyBoxes == maxIterationNumber)
			{
				//if there is enemy on final Check box => kill
				ValidateOccupancyAndMove(finalMove);
				print("CanMove");
			}
		}
		//Horizontal Switch
		else if(MathF.Abs(currentIndex - finalPositionIndex) < 8)
		{
			var maxIterationNumber = Mathf.Abs(currentIndex - finalPositionIndex);
			var lowestAllowedIndex = currentIndex - 8;
			var hightestAllowedIndex = currentIndex + lowestAllowedIndex + 1;
			var numberOfEmptyBoxes = 0;
			
			RookInvalidCases();
			
			for (var i = 1; i <= maxIterationNumber; i++)
			{
				print(currentIndex);
				if (currentIndex < finalPositionIndex)
				{
					if (!checkBoxes[currentIndex + i].GetComponent<CheckBox>().isOccupied)
					{
						print(checkBoxes[currentIndex + i].GetComponent<CheckBox>().isOccupied);
						numberOfEmptyBoxes += 1;
					}
					else
					{
						InvalidMove();
						return;
					}
				}
				if (currentIndex > finalPositionIndex)
				{
					if (!checkBoxes[currentIndex - i].GetComponent<CheckBox>().isOccupied)
					{
						print(checkBoxes[currentIndex - i].GetComponent<CheckBox>().isOccupied);
						numberOfEmptyBoxes += 1;
					}
					else
					{
						InvalidMove();
						return;
					}
				}
			}
			print("Number of Empty Boxes " + numberOfEmptyBoxes);
			
			if(numberOfEmptyBoxes == maxIterationNumber)
			{
				ValidateOccupancyAndMove(finalMove);
				print("CanMove");
			}
			InvalidMove();
		}
	}

	private void ValidateOccupancyAndMove(Transform finalMove)
	{
		if(!finalMove.GetComponent<CheckBox>().isOccupied)
		{
			MoveThePiece(finalMove);
		}
		else
			InvalidMove();
	}

	private void MoveThePiece(Transform finalMove)
	{
		selectedTransform.position = finalMove.GetChild(0).position;
		selectedTransform.parent.GetComponent<CheckBox>().isOccupied = false;
		pieceSelected = !pieceSelected;
		selectedTransform.parent = finalMove;
		finalMove.GetComponent<CheckBox>().isOccupied = true;
		selectedTransform = null;
		finalTransform = null;
	}

	private void KillTheEnemy(Transform finalMove)
	{
		if (finalMove.childCount == 2)
		{
			//if the target piece is not of same color => kill
			if (selectedTransform.GetComponent<Soldier>().isWhite !=
				finalTransform.GetChild(1).GetComponent<Soldier>().isWhite)
			{
				selectedTransform.position = finalMove.GetChild(0).position;
				pieceSelected = !pieceSelected;
				selectedTransform.parent = finalMove;	
			}
			else
				InvalidMove();
		}
		//else invalid move
		else
			InvalidMove();
	}

	private void RookInvalidCases()
	{
		var currentIndex = Array.IndexOf(checkBoxPositions, selectedTransform.parent.position);
		var finalPositionIndex = Array.IndexOf(checkBoxPositions, finalTransform.position);

		/*
		if (Array.IndexOf(checkBoxPositions, finalTransform.position) != (currentIndex + 7) ||
			Array.IndexOf(checkBoxPositions, finalTransform.position) != (currentIndex + 9) ||
			Array.IndexOf(checkBoxPositions, finalTransform.position) != (currentIndex - 7) ||
			Array.IndexOf(checkBoxPositions, finalTransform.position) != (currentIndex - 9))
		{
			
		}*/

		var minIndex = 0;
		var maxIndex = 0;

		if (currentIndex >= 0 && currentIndex < 8)
		{
			minIndex = 0;
			maxIndex = 7;
		}
		if (currentIndex >= 8 && currentIndex < 16)
		{
			minIndex = 8;
			maxIndex = 15;
		}
		if (currentIndex >= 16 && currentIndex < 24)
		{
			minIndex = 16;
			maxIndex = 23;
		}
		if (currentIndex >= 24 && currentIndex < 32)
		{
			minIndex = 24;
			maxIndex = 31;
		}
		if (currentIndex >= 32 && currentIndex < 40)
		{
			minIndex = 32;
			maxIndex = 39;
		}
		if (currentIndex >= 40 && currentIndex < 48)
		{
			minIndex = 40;
			maxIndex = 47;
		}
		if (currentIndex >= 48 && currentIndex < 56)
		{
			minIndex = 48;
			maxIndex = 56;
		}
		if (currentIndex >= 56 && currentIndex < 64)
		{
			minIndex = 56;
			maxIndex = 63;
		}
		
		
	}
}
