using UnityEngine;
using UnityEngine.AI;

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
    private void Awake()
    {

        groundState = GameObject.Find("Ground1");
    }
    void Start()
    {
        anim = character.GetComponent<Animator>();
        onBridge = false;
        isMoving = false;
        direct = Vector3.zero;
        speed = 10;
        rotationSpeed = 720;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.endGame)
        {
            return;
        }
        this.destroyCharacter();
        if (getHit)
        {
            anim.SetBool("fall", true);
            Invoke("standUp", 2.5f);
        }
        else
        {
            horizontal = joystick.Horizontal;
            vertical = joystick.Vertical;
            direct = new Vector3(horizontal, 0, vertical);
            if (isMoving)
            {
                anim.SetBool("run", true);
                RaycastHit hit;
                if (Physics.Raycast(transform.position + Vector3.up * 2, Vector3.down, out hit, Mathf.Infinity, Bridge))
                {
                    this._bridge.limitArea();
                    if (vertical > 0)
                    {
                        if (collectedBrick.Count > 0)
                        {
                            this.checkBrick();
                        }
                        else if (collectedBrick.Count == 0)
                        {
                            direct = new Vector3(horizontal, 0, 0);
                        }
                    }
                    this.MoveOnBridge(direct, hit);
                    onBridge = true;
                }
                else
                {
                    onBridge = false;
                    this.checkSpecialBrick();
                    transform.position = new Vector3(transform.position.x, positionGround.y + 0.9f, transform.position.z);
                    ground.limitArea();
                    Quaternion toRotation = Quaternion.LookRotation(direct, Vector3.up);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
                    transform.position += direct * speed * Time.deltaTime;
                }

                if (Vector3.Distance(Vector3.zero, direct) <= 0.01f)
                {
                    isMoving = false;
                }

            }
            else
            {
                anim.SetBool("run", false);
                if (Vector3.Distance(Vector3.zero, direct) >= 0.01f)
                {
                    isMoving = true;
                }
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

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
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
    protected void destroyCharacter()
    {
        int count = 0;
        foreach (GameObject brick in groundState.GetComponent<Ground>().listBricks)
        {
            if (brick.GetComponent<Brick>().color == color)
            {
                if (brick.active) count++;
            }
        }
        if (!onBridge && count == 0 && collectedBrick.Count == 0)
        {
            GameManager.Instance.endGame = true;
        }
    }
}
