using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class TriggerSpawn : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform gunTransform;
    GameObject player;
    public float triggerRayDistance = 14f;
    public float triggerRayAngle = 0f;
    public GameObject spawningObject1;
    public GameObject spawningObject2;
    public GameObject spawningObject3;
    public GameObject spawningObject4;
    bool isStage4 = false;

    void Start()
    {
        spawningObject1.SetActive(false);
        spawningObject2.SetActive(false);
        spawningObject3.SetActive(false);
        spawningObject4.SetActive(false);
        if (player == null)
            player = GameObject.Find("PlayerSetup");
    }

    void Update()
    {
        RayCastTrigger();
    }

    void RayCastTrigger()
    {
        RaycastHit2D hit;
        Vector2 firstDirection = Quaternion.Euler(0, 0, triggerRayAngle) * Vector2.right;
        Debug.DrawRay(gunTransform.position, firstDirection * triggerRayDistance, Color.cyan);
        hit = Physics2D.Raycast(gunTransform.position, firstDirection, triggerRayDistance);
        if (hit.collider != null && hit.collider.CompareTag("Stage1"))
        {
            spawningObject1.SetActive(true);
            //Debug.Log("First Check Point!");
        }
        else if (hit.collider != null && hit.collider.CompareTag("Stage2"))
        { 
            spawningObject1.SetActive(false);
            spawningObject2.SetActive(true);
            //Debug.Log("Second Check Point!");
        }
        else if (hit.collider != null && hit.collider.CompareTag("Stage3"))
        {
            spawningObject2.SetActive(false);
            spawningObject3.SetActive(true);
            //Debug.Log("Third Check Point!");
        }
        else if (hit.collider != null && hit.collider.CompareTag("Stage4"))
        {
            isStage4 = true;
            if (isStage4)
            {
                spawningObject3.SetActive(false);
                spawningObject4.SetActive(true);
                PlayerMovements movement = player.GetComponent<PlayerMovements>();
                movement.SetPlayerSpeed(0f);
            }
            Invoke("SetBossSpawnFalse", 5f);
            //Debug.Log("Fourth Check Point!");
        }
    }

    void SetBossSpawnFalse()
    {
        isStage4 = false;
        spawningObject4.SetActive(false);
    }
}
