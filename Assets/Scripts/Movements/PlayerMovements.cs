using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    // Data members
    [SerializeField] float playerSpeed = 1f;

    // Update is called once per frame
    void Update()
    {
        PlayerAutoMove();
    }

    public void SetPlayerSpeed(float newSpeed)
    {
        playerSpeed = newSpeed;
    }

    void PlayerAutoMove()
    {
        if (PlayerPrefs.GetInt("GamePaused") == 0) {
            transform.Translate(Vector3.right * playerSpeed * Time.fixedDeltaTime);
        }
    }
}
