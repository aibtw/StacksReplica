using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject cubePrefab;

    private float spawningPoint = -1.75f;

    public void SpawnCube(Direction direction)
    {
        GameObject Cube = Instantiate(cubePrefab);
        Cube.GetComponent<MoveCube>().Direction = direction;


        if(direction == Direction.Zdirection)
        {
            Cube.transform.position = new Vector3(MoveCube.PreviosCube.transform.position.x,
                MoveCube.PreviosCube.transform.position.y + (MoveCube.PreviosCube.transform.localScale.y/2) +(cubePrefab.transform.localScale.y/2),
                spawningPoint);
        }
        else if(direction == Direction.Xdirection)
        {
            Cube.transform.position = new Vector3(spawningPoint,
                MoveCube.PreviosCube.transform.position.y + (MoveCube.PreviosCube.transform.localScale.y/2) +(cubePrefab.transform.localScale.y/2),
                MoveCube.PreviosCube.transform.position.z);
        }

    }
}
