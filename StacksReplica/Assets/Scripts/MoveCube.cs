using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCube : MonoBehaviour
{
    public GameObject fallingCube;
    public float MSpeed = 1.5f;

    private float hangedCubeScale;
    private float hangedCubePos;
    private float placedCubeScale;
    private float placedCubePos;

    enum Direction
    { Xdirection, Ydirection };

    public static MoveCube CurrentCube { get; private set; }
    public static MoveCube PreviosCube { get;  set; }


    
    private void OnEnable()
    {
        if (PreviosCube == null)
        {
            PreviosCube = this;
        }
        
        CurrentCube = this;

        transform.localScale = new Vector3(PreviosCube.transform.localScale.x, transform.localScale.y, PreviosCube.transform.localScale.z);
    }

    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * MSpeed;
    }

    public void StopCube()
    {
        if (CurrentCube != PreviosCube)
        {
            MSpeed = 0;
            Calculations();
            GameObject fallingCube = SpawnFallingCube();
            AdjustCubes(fallingCube);

            PreviosCube = this;
        }
    }

    private void Calculations() //Calculate hanged and placed cubes scales and positions.
    {
        //Two scales:
        float distance = PreviosCube.transform.position.z - CurrentCube.transform.position.z;
        hangedCubeScale = Mathf.Abs(distance);
        placedCubeScale = CurrentCube.transform.localScale.z - hangedCubeScale;

        //Two Positions:
        float stopPos = CurrentCube.transform.position.z;
        placedCubePos = CurrentCube.transform.position.z - PreviosCube.transform.position.z < 0 ? stopPos - (placedCubeScale/2) + (CurrentCube.transform.localScale.z/2) : stopPos + (placedCubeScale / 2) - (CurrentCube.transform.localScale.z / 2);
        hangedCubePos = CurrentCube.transform.position.z - PreviosCube.transform.position.z < 0? placedCubePos - (placedCubeScale/2) - (hangedCubeScale/2) : placedCubePos + (placedCubeScale / 2) + (hangedCubeScale / 2);
    }

    private GameObject SpawnFallingCube()
    {
        GameObject fallingCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        fallingCube.AddComponent<Rigidbody>();
        return fallingCube;
    }

    private void AdjustCubes(GameObject fallingCube)
    {
        CurrentCube.transform.localScale = new Vector3(CurrentCube.transform.localScale.x, CurrentCube.transform.localScale.y, placedCubeScale);
        CurrentCube.transform.position = new Vector3(CurrentCube.transform.position.x, CurrentCube.transform.position.y, placedCubePos);
        fallingCube.transform.localScale = new Vector3(CurrentCube.transform.localScale.x, CurrentCube.transform.localScale.y, hangedCubeScale);
        fallingCube.transform.position = new Vector3(CurrentCube.transform.position.x, CurrentCube.transform.position.y, hangedCubePos);
    }
}
