using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowing : Singleton<CameraFollowing>
{
    [SerializeField] private Transform player;
    private float speed = 5f;
    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.endGame) return;
        transform.position = Vector3.Lerp(transform.position, player.transform.position + new Vector3(0, 15, -15), Time.deltaTime * speed);
        transform.transform.rotation = Quaternion.Euler(45, 0, 0);
    }

    public void followTheWinner(GameObject winner)
    {
        transform.position = winner.transform.position + new Vector3(0, 3, -5);
        transform.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
