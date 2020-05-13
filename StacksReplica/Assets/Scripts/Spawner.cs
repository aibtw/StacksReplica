using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject cubePrefab; 
    public void SpawnCube()
    {
        GameObject Cube = GameObject.Instantiate(cubePrefab);
        Cube.transform.position = new Vector3(cubePrefab.transform.position.x,
            MoveCube.PreviosCube.transform.position.y + (MoveCube.PreviosCube.transform.localScale.y/2) +(cubePrefab.transform.localScale.y/2),
            cubePrefab.transform.position.z);

        gameObject.transform.position = new Vector3(transform.position.x, transform.position.y + MoveCube.PreviosCube.transform.localScale.y, transform.position.z);
    }
}
