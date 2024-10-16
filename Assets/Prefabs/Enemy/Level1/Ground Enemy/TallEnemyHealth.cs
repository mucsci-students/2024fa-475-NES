using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public GameObject GameObject;
    private EnemyStat enemyStat;
    private float health;
    private UnityEngine.Vector3 screenPosition;
    // Start is called before the first frame update

    void Start()
    {
        Application.targetFrameRate = 60;
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
        GUI.Box(new Rect(screenPosition.x - 50, Screen.height - screenPosition.y - 75, 100, 20), "Health: " + health);
    }
}
