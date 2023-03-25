using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
    [SerializeField] private Transform player;
    private float speed = 5f;
    // Update is called once per frame
    void Update()
    {

        transform.position = Vector3.Lerp(transform.position, player.transform.position + new Vector3(0, 15, -15), Time.deltaTime * speed);
        transform.transform.rotation = Quaternion.Euler(45, 0, 0);
    }
}
