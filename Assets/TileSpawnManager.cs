using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawnManager : MonoBehaviour
{
    //SpawnTiles One above The Other
	//Arrange their rotations in circular manner
	//Rotate Them

	public GameObject parentObject;
	public GameObject tile;
	public List<GameObject> tilesList;

	public float angleDifference;
	public float platformHeight;
	private void Start()
	{
	
		
		tilesList = new List<GameObject>();
		
		for (var i = 0; i < 100; i++)
		{
			GameObject obj = Instantiate(tile,Vector3.up * i * -platformHeight,Quaternion.Euler(0f,angleDifference * i,0f),parentObject.transform);
			//parentObject.transform.SetParent(obj.transform.parent);
			tilesList.Add(obj);
		}
	}
	
	

}
