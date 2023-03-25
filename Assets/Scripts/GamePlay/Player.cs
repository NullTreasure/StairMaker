using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Direct { Forward, Back, Left, Right }
public class Player : MonoBehaviour
{
    [SerializeField] private FloatingJoystick joystick;
    public GameObject player;

    private float horizontal;
    private float vertical;
    private Vector3 direct;
    private bool isMoving;
    private float speed;
    public int countBrick;

    void Start()
    {
        isMoving = false;
        direct = Vector3.zero;
        speed = 10;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = joystick.Horizontal;
        vertical = joystick.Vertical;
        direct = new Vector3(horizontal, 0, vertical);
        if (isMoving)
        {
            transform.position += direct * speed* Time.deltaTime;
            if (Vector3.Distance(Vector3.zero,direct) <= 0.01f)
            {
                isMoving = false;
            }
           
        }
        else
        {
            
            if (Vector3.Distance(Vector3.zero, direct) >= 0.01f)
            {
                isMoving = true;
            }
        }
    }

    public void addBrick()
    {
        countBrick++;
    }
}
