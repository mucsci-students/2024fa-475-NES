using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirEnemyHealth : MonoBehaviour
{
    public GameObject GameObject;
    private EnemyStat enemyStat;
    private float health;
    private UnityEngine.Vector3 screenPosition;
    // Start is called before the first frame update
    void Start()
    {
        enemyStat = GameObject.GetComponent<EnemyStat>();
        health = enemyStat.GetEnemyHealth();
        screenPosition = Camera.main.WorldToScreenPoint(GameObject.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        health = enemyStat.GetEnemyHealth();
        screenPosition = Camera.main.WorldToScreenPoint(GameObject.transform.position);
    }

    void OnGUI()
    {
        float yPos = Screen.height - screenPosition.y - 40;
        GUI.Box(new Rect(screenPosition.x - 50, yPos, 100, 20), "Health: " + health);
    }
}
