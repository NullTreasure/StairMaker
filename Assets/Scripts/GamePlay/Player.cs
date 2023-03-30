using UnityEngine;
public enum Direct { Forward, Back, Left, Right }
public class Player : MonoBehaviour
{
    [SerializeField] private FloatingJoystick joystick;
    [SerializeField] private GameObject stairBrick;
    [SerializeField] private GameObject skin;
    [SerializeField] private LayerMask BridgeBrick;
    [SerializeField] private LayerMask Bridge;

    public GameObject player;

    private Bridge _bridge;
    private Ground ground;
    private Vector3 positionGround;
    private float horizontal;
    private float vertical;
    private Vector3 direct;
    private bool isMoving;
    private float speed;
    private float rotationSpeed;
    public int countBrick;
    private float maxSlopeAngle = 90.0f;

    private Vector3 slopeNormal;

    void Start()
    {
        isMoving = false;
        direct = Vector3.zero;
        speed = 10;
        rotationSpeed = 720;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = joystick.Horizontal;
        vertical = joystick.Vertical;
        direct = new Vector3(horizontal, 0, vertical);
        if (isMoving)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position + Vector3.up *2, Vector3.down, out hit, Mathf.Infinity, Bridge))
            {
                this._bridge.limitArea();
                this.MoveOnBridge(direct, hit);
                if (vertical > 0)
                {
                    this.checkBrick();   
                }
            }
            else
            {
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

    public void addBrick()
    {
        countBrick++;
    }
    private void checkBrick()
    {
        RaycastHit Hit;
        if (Physics.Raycast(transform.position + Vector3.up *2, Vector3.down, out Hit, Mathf.Infinity, BridgeBrick))
        {
            MeshRenderer meshRenderer = Hit.transform.GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                meshRenderer.enabled = true;
            }
            Renderer check = Hit.transform.GetComponent<Renderer>();
            if (!check.material.name.StartsWith(player.GetComponent<Renderer>().material.name))
            {
                check.material = player.GetComponent<Renderer>().material;
                countBrick--;
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
