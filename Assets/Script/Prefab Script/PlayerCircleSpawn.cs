using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCircleSpawn : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject circleGameObject;
    public MinigameHanlder minigameHanlder;


    private Vector3 position = new Vector3();
    private void Start()
    {


        position = new Vector3(spawnPoint.position.x, spawnPoint.position.y, spawnPoint.position.z);

        Instantiate(circleGameObject, position, Quaternion.identity);
    }

    

}
