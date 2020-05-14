using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCube : MonoBehaviour
{
    public float MSpeed = 1.5f;
    private float hangedCubeScale;
    private float hangedCubePos;
    private float placedCubeScale;
    private float placedCubePos;

    public GameObject fallingCube;

    public static MoveCube CurrentCube { get; private set; }
    public static MoveCube PreviosCube { get;  set; }
    public Direction Direction { get;  set; }
    public static int Stacks { get; private set; } //stacks counter

    //Initialize The Moving cube:
    private void OnEnable()
    {
        if (PreviosCube == null)
        {
            PreviosCube = this;
        }
        
        CurrentCube = this;

        transform.localScale = new Vector3(PreviosCube.transform.localScale.x, transform.localScale.y, PreviosCube.transform.localScale.z);
    }

    //Move The Cube in the Corrent Direction:
    void Update()
    {
        if (Direction == Direction.Zdirection)
            transform.position += transform.forward * Time.deltaTime * MSpeed;
        else
            transform.position += transform.right * Time.deltaTime * MSpeed;
    }

    public void StopCube()
    {
        if (CurrentCube != PreviosCube)
        {
            Stacks++; //increase stacks counter

            MSpeed = 0; //Stop the cube

            if(Mathf.Abs(CurrentCube.transform.position.x - PreviosCube.transform.position.x) <= 0.03f && Direction == Direction.Xdirection) //snip on x direction
            {
                CurrentCube.transform.position = new Vector3(PreviosCube.transform.position.x, transform.position.y, PreviosCube.transform.position.z);
                Stacks++; //increase stacks counter for snipping
            }
            else if(Mathf.Abs(CurrentCube.transform.position.z - PreviosCube.transform.position.z) <= 0.03f && Direction == Direction.Zdirection) //snip on z direction
            {
                CurrentCube.transform.position = new Vector3(PreviosCube.transform.position.x, transform.position.y, PreviosCube.transform.position.z);
                Stacks++; //increase stacks counter for snipping
            }
            else //start the cutting process
            {
                Calculations();
                GameObject fallingCube = SpawnFallingCube();
                AdjustCubes(fallingCube);
            }

            PreviosCube = this;
        }
    }


    private void Calculations() //Calculate hanged and placed cubes scales and positions, in the correct direction:
    {
        //Z Direction: 
        if(Direction == Direction.Zdirection) 
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

        //X Direction:
        else
        {
            //Two scales:
            float distance = PreviosCube.transform.position.x - CurrentCube.transform.position.x;
            hangedCubeScale = Mathf.Abs(distance);
            placedCubeScale = CurrentCube.transform.localScale.x - hangedCubeScale;

            //Two Positions:
            float stopPos = CurrentCube.transform.position.x;
            placedCubePos = CurrentCube.transform.position.x - PreviosCube.transform.position.x < 0 ? stopPos - (placedCubeScale / 2) + (CurrentCube.transform.localScale.x / 2) : stopPos + (placedCubeScale / 2) - (CurrentCube.transform.localScale.x / 2);
            hangedCubePos = CurrentCube.transform.position.x - PreviosCube.transform.position.x < 0 ? placedCubePos - (placedCubeScale / 2) - (hangedCubeScale / 2) : placedCubePos + (placedCubeScale / 2) + (hangedCubeScale / 2);
        }
    }


    private GameObject SpawnFallingCube()
    {
        GameObject fallingCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        fallingCube.AddComponent<Rigidbody>();
        return fallingCube;
    }

    private void AdjustCubes(GameObject fallingCube) // Adjust the position and the scale of the placed and falling cubes:
    {
        if (Direction == Direction.Zdirection)
        {
            CurrentCube.transform.localScale = new Vector3(CurrentCube.transform.localScale.x, CurrentCube.transform.localScale.y, placedCubeScale);
            CurrentCube.transform.position = new Vector3(CurrentCube.transform.position.x, CurrentCube.transform.position.y, placedCubePos);
            fallingCube.transform.localScale = new Vector3(CurrentCube.transform.localScale.x, CurrentCube.transform.localScale.y, hangedCubeScale);
            fallingCube.transform.position = new Vector3(CurrentCube.transform.position.x, CurrentCube.transform.position.y, hangedCubePos);
        }
        else
        {
            CurrentCube.transform.localScale = new Vector3(placedCubeScale, CurrentCube.transform.localScale.y, CurrentCube.transform.localScale.z);
            CurrentCube.transform.position = new Vector3(placedCubePos, CurrentCube.transform.position.y, CurrentCube.transform.position.z);
            fallingCube.transform.localScale = new Vector3(hangedCubeScale, CurrentCube.transform.localScale.y, CurrentCube.transform.localScale.z);
            fallingCube.transform.position = new Vector3(hangedCubePos, CurrentCube.transform.position.y, CurrentCube.transform.position.z);
        }
    }
   
    internal static void ResetStaticVariables()
    {
        CurrentCube = null;
        PreviosCube = null;
        Stacks = 0;
    }

}
