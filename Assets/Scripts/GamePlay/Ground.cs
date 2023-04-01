using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Bounds bounds;

    [SerializeField] private GameObject prefabBrick;
    [SerializeField] private ColorTypes color;
    [SerializeField] private Material redMaterial;
    [SerializeField] private Material yellowMaterial;
    [SerializeField] private Material blueMaterial;
    public List<Vector3> Bricks = new List<Vector3>();
    
    private List<GameObject> listBricks= new List<GameObject>();
    private void Start()
    {
        bounds.center = transform.position;
        this.declarePositonList();
        bounds.extents = new Vector3(10.0f,0,10.0f);
        this.SpawnBrick();
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
        if (other.name == "Player")
        {
            this.turnActiveBrick(other.GetComponent<Player>().player);
        }
        if (other.name.StartsWith("Enemy"))
        {
            this.turnActiveBrick(other.GetComponent<Enemy>().enemy);
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
    private void turnActiveBrick(GameObject check)
    {
        Renderer color = check.GetComponent<Renderer>();
        foreach(GameObject brick in listBricks) 
        {
            if (color.material.name.StartsWith(brick.GetComponent<Renderer>().material.name))
            {
                brick.SetActive(true);
            }
        }
    }
    public void deActiveBrick(GameObject check)
    {
        Renderer color = check.GetComponent<Renderer>();
        foreach (GameObject brick in listBricks)
        {
            if (color.material.name.StartsWith(brick.GetComponent<Renderer>().material.name))
            {
                brick.SetActive(false);
            }
        }
    }
    private void declarePositonList()
    {
        for (float i =  bounds.center.x- 6; i <= bounds.center.x + 6; i++)
        {
            for (float j =bounds.center.z -6; j <=bounds.center.z + 6; j++)
            {
                Vector3 postion = new Vector3(i, this.transform.position.y, j);
                Bricks.Add(postion);
            }
        }
    }
    private void setColor(GameObject brick)
    {
        int rnd = Random.Range(1, 4);
        Material color = redMaterial;
        switch (rnd)
        {
            case 1:
                color = redMaterial; break;
            case 2:
                color = yellowMaterial; break;
            case 3:
                color = blueMaterial; break;
        }
        Renderer renderer = brick.GetComponent<Renderer>();
        renderer.material = color;
    }

}
