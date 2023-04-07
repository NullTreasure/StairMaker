using UnityEngine;
public enum Direct { Forward, Back, Left, Right }
public class Player : Character
{
    [SerializeField] private FloatingJoystick joystick;

    private Bridge _bridge;
    private Ground ground;
    public Vector3 positionGround;
    private float horizontal;
    private float vertical;
    private Vector3 direct;
    private bool isMoving;
    private float speed;
    private float rotationSpeed;
    private float maxSlopeAngle = 90.0f;

    private Vector3 slopeNormal;

    void Start()
    {
        countBrick = 0;
        isMoving = false;
        direct = Vector3.zero;
        speed = 10;
        rotationSpeed = 720;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.endGame) return;
        horizontal = joystick.Horizontal;
        vertical = joystick.Vertical;
        direct = new Vector3(horizontal, 0, vertical);
        if (isMoving)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position + Vector3.up *2, Vector3.down, out hit, Mathf.Infinity, Bridge))
            {
                this._bridge.limitArea();
                if (vertical > 0)
                {
                    if (countBrick > 0 )
                    {
                        this.checkBrick();
                    }
                    else if (countBrick == 0)
                    {
                        direct = new Vector3(horizontal, 0, 0);
                    }
                }
                this.MoveOnBridge(direct, hit);
            }
            else
            {
                this.checkSpecialBrick();
                transform.position = new Vector3(transform.position.x, positionGround.y + 0.9f, transform.position.z);
                ground.limitArea();
                Quaternion toRotation = Quaternion.LookRotation(direct, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation,toRotation, rotationSpeed * Time.deltaTime);
                transform.position += direct * speed * Time.deltaTime; 
            }

            if (Vector3.Distance(Vector3.zero, direct) <= 0.01f)
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

    
    private void MoveOnBridge(Vector3 direction, RaycastHit hit)
    {
        slopeNormal = hit.normal;
        Vector3 movementDirection = Vector3.zero;
        float slopeAngle = Vector3.Angle(slopeNormal, Vector3.up);
        if (slopeAngle <= maxSlopeAngle)
        {
            movementDirection = Vector3.ProjectOnPlane(direction, slopeNormal).normalized;
        }

        Quaternion toRotation = Quaternion.LookRotation(direct, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        transform.position += movementDirection * speed * Time.deltaTime;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.StartsWith("Ground"))
        {
            groundState = other.gameObject;
            positionGround = other.transform.position;
            ground = other.GetComponent<Ground>();
        }
        if (other.name.StartsWith("Real Bridge"))
        {
            GameObject bridge = other.transform.parent.gameObject;
            _bridge = bridge.GetComponent<Bridge>();
        }
    }
}
