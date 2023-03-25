using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject prefabBrick;
    [SerializeField] private ColorTypes color;
    [SerializeField] private Material redMaterial;
    [SerializeField] private Material yellowMaterial;
    [SerializeField] private Material blueMaterial;
    [SerializeField] private GameObject player;
    private List<Vector3> Bricks = new List<Vector3>();
    private void Awake()
    {
        
    }
    void Start()
    {
        this.declarePositonList();
        this.setColor(player);
    }

    // Update is called once per frame
    void Update()
    {
        this.SpawnBrick();
    }


    public void SpawnBrick()
    {
        if (Bricks.Count== 0) return;
        else
        {
            int Index = Random.Range(0, Bricks.Count -1);
            Vector3 brickPosition = Bricks[Index];
            GameObject brick = Instantiate(prefabBrick, brickPosition, Quaternion.identity);
            this.setColor(brick);
            Bricks.RemoveAt(Index);
        }
    }
    private void declarePositonList()
    {
        for (int i = -6; i <= 6; i ++)
        {
            for (int j = -6; j <=6; j ++)
            {
                Vector3 postion = new Vector3(i, -0.9f, j);
                Bricks.Add(postion);
            }
        }
    }
    private void setColor(GameObject brick)
    {
        int rnd = Random.Range(1, 4);
        Material color = redMaterial;
        switch(rnd)
        {
            case 1:
                color = redMaterial; break;
            case 2: 
                color = yellowMaterial; break;
            case 3:
                color = blueMaterial; break;
        }
        Renderer renderer = brick.GetComponent<Renderer>();
        renderer.material= color;
    }
}
