using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Bounds bounds;

    [SerializeField] private GameObject prefabBrick;
    [SerializeField] private Material redMaterial;
    [SerializeField] private Material yellowMaterial;
    [SerializeField] private Material blueMaterial;
    [SerializeField] private Material greenMaterial;

    public List<Vector3> Bricks = new List<Vector3>();
    
    public List<GameObject> listBricks= new List<GameObject>();
    private void Awake()
    {
        bounds.center = transform.position;
        bounds.extents = new Vector3(10.0f, 0, 10.0f);
        this.declarePositonList();
        this.SpawnBrick();
    }
    private void Start()
    {
        
    }
    public void limitArea()
    {
        if (player.position.x >= bounds.max.x) 
        { 
            player.position = new Vector3(bounds.max.x,player.position.y,player.position.z);
        }
        if (player.position.z >= bounds.max.z)
        {
            player.position = new Vector3(player.position.x, player.position.y, bounds.max.z);
        }
        if (player.position.x <= bounds.min.x)
        {
            player.position = new Vector3(bounds.min.x, player.position.y, player.position.z);
        }
        if (player.position.z <= bounds.min.z)
        {
            player.position = new Vector3(player.position.x, player.position.y, bounds.min.z);
        }

    }
    public void banBridge()
    {
        if (player.position.z >= bounds.max.z - 1f)
        {
            player.position = new Vector3(player.position.x, player.position.y, bounds.max.z -1f);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player" || other.name.StartsWith("Enemy"))
        {
            this.turnActiveBrick(other.GetComponent<Character>().color);
        }
    }
    public void SpawnBrick()
    {
        while(Bricks.Count > 0)
        {
            int Index = Random.Range(0, Bricks.Count - 1);
            Vector3 brickPosition = Bricks[Index];
            GameObject brick = Instantiate(prefabBrick, brickPosition + new Vector3(0,0.6f,0), Quaternion.identity);
            this.setColor(brick);
            Bricks.RemoveAt(Index);
            listBricks.Add(brick);
        } 
    }
    private void turnActiveBrick(ColorTypes.Color color)
    {
        foreach(GameObject brick in listBricks) 
        {
            ColorTypes.Color colorBrick = brick.GetComponent<Brick>().color;
            if (colorBrick == color)
            {
                brick.SetActive(true);
            }
        }
    }
    public void deActiveBrick(ColorTypes.Color color)
    {
        foreach (GameObject brick in listBricks)
        {
            ColorTypes.Color colorBrick = brick.GetComponent<Brick>().color;
            if (colorBrick == color)
            {
                brick.SetActive(false);
            }
        }
    }
    private void declarePositonList()
    {
        for (float i =  bounds.center.x- 6; i <= bounds.center.x + 6; i+=2)
        {
            for (float j =bounds.center.z -6; j <=bounds.center.z + 6; j+=2)
            {
                Vector3 postion = new Vector3(i, this.transform.position.y, j);
                Bricks.Add(postion);
            }
        }
    }
    private void setColor(GameObject brick)
    {
        int rnd = Random.Range(1, 5);
        Brick brickScript = brick.GetComponent<Brick>();
        Material colorMaterial = redMaterial;
        switch (rnd)
        {
            case 1:
                colorMaterial = redMaterial;
                brickScript.color = ColorTypes.Color.red;
                break;
            case 2:
                colorMaterial = yellowMaterial;
                brickScript.color = ColorTypes.Color.yellow;
                break;
            case 3:
                colorMaterial = blueMaterial;
                brickScript.color = ColorTypes.Color.blue; break;
            case 4:
                colorMaterial= greenMaterial;
                brickScript.color = ColorTypes.Color.green;break;
        }
        Renderer renderer = brick.GetComponent<Renderer>();
        renderer.material = colorMaterial;
    }

}
