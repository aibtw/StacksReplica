using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using System.Xml.Schema;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(MoveCube.CurrentCube!=null)
                MoveCube.CurrentCube.StopCube();

            FindObjectOfType<Spawner>().SpawnCube();
        }
    }

}
