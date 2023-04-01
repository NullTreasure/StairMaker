using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ColorTypes color;
    [SerializeField] private Material redMaterial;
    [SerializeField] private Material yellowMaterial;
    [SerializeField] private Material blueMaterial;
    [SerializeField] private GameObject playerSkin;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject enemy;
    [SerializeField] private List<GameObject> GroundState;

    List<int> numColor = new List<int>(3) { 1, 2, 3 };
    private void Awake()
    {
        
    }
    void Start()
    {
        GameObject enemy1 = Instantiate(enemy, enemy.transform.position,Quaternion.identity);
        enemy1.SetActive(true);
        this.setColor(enemy1.GetComponent<Enemy>().enemy);
        GameObject enemy2 = Instantiate(enemy, enemy.transform.position + new Vector3(3,0,0), Quaternion.identity);
        enemy2.SetActive(true);
        this.setColor(enemy2.GetComponent<Enemy>().enemy);
        this.setColor(playerSkin);
    }
    private void Update()
    {
        foreach(GameObject ground in GroundState)
        {
            if (ground.name != Player.GetComponent<Player>().groundState.name)
            {
                ground.GetComponent<Ground>().deActiveBrick(Player.GetComponent<Player>().player);
            }
        }
    }

    // Update is called once per frame
    private void setColor(GameObject brick)
    {
        int rnd = 0;
        Material color = redMaterial;
        do
        {
            rnd = Random.Range(1, 4);
        } while (!numColor.Contains(rnd));
        switch(rnd)
        {
            case 1:
                    color = redMaterial; break;
            case 2: 
                    color = yellowMaterial; break;
            case 3:
                    color = blueMaterial; break;
        }
        numColor.Remove(rnd);
        Renderer renderer = brick.GetComponent<Renderer>();
        renderer.material= color;
    }
}
