using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private ColorTypes color;
    [SerializeField] private Material redMaterial;
    [SerializeField] private Material yellowMaterial;
    [SerializeField] private Material blueMaterial;
    [SerializeField] protected Material greenMaterial;

    [SerializeField] private GameObject playerSkin;
    [SerializeField] private GameObject Player;
    
    private GameObject enemy1;
    private GameObject enemy2;
    private GameObject enemy3;
    [SerializeField] private GameObject enemy;
    [SerializeField] private List<GameObject> GroundState;

    public bool endGame;
    public bool isWin;

    List<int> numColor = new List<int>(3) { 1, 2, 3,4 };
    private void Awake()
    {

        isWin = false;
        endGame = false;
    }
    void Start()
    {
        enemy1 = Instantiate(enemy, enemy.transform.position, Quaternion.identity);
        enemy1.SetActive(true);
        Enemy _enemy1 = enemy1.GetComponent<Enemy>();
        setColor(_enemy1.skin);
        SetGround(_enemy1);
        enemy2 = Instantiate(enemy, enemy.transform.position + new Vector3(3, 0, 0), Quaternion.identity);
        enemy2.SetActive(true);

        Enemy _enemy2 = enemy2.GetComponent<Enemy>();
        setColor(_enemy2.skin);
        SetGround(_enemy2);

        enemy3 = Instantiate(enemy, enemy.transform.position, Quaternion.identity);
        enemy3.SetActive(true);
        Enemy _enemy3 = enemy3.GetComponent<Enemy>();
        setColor(_enemy3.skin);
        SetGround(_enemy3);
        this.setColor(playerSkin);
    }

    private void SetGround(Enemy enemy)
    {

        enemy.ground = GroundState[0].GetComponent<Ground>();
    }

    private void Update()
    {
        if (endGame) return;
        deleteState(Player);
        deleteState(enemy1);
        deleteState(enemy2);
        deleteState(enemy3);
    }

    // Update is called once per frame
    private void deleteState(GameObject character)
    {
        
        foreach (GameObject ground in GroundState)
        {
            if (ground.name != character.GetComponent<Character>().groundState.name)
            {
                ground.GetComponent<Ground>().deActiveBrick(character.GetComponent<Character>().color);
            }
        }
    }
    public void deleteAllState()
    {
        foreach (GameObject ground in GroundState)
        {
            foreach(GameObject brick in ground.GetComponent<Ground>().listBricks )
            {
                Destroy(brick);
                ground.GetComponent<Ground>().listBricks = null;
            }
        }
    }
    private void setColor(GameObject character)
    {
        int rnd = 0;
        Character skin = character.GetComponentInParent<Character>();
        Material color = redMaterial;
        do
        {
            rnd = Random.Range(1, 5);
        } while (!numColor.Contains(rnd));
        switch(rnd)
        {
            case 1:
                    color = redMaterial;
                    skin.color = ColorTypes.Color.red;
                    break;
            case 2: 
                    color = yellowMaterial;
                    skin.color = ColorTypes.Color.yellow;
                    break;
            case 3:
                    color = blueMaterial;
                    skin.color = ColorTypes.Color.blue;
                    break;

            case 4:
                color = greenMaterial;
                skin.color = ColorTypes.Color.green;
                break;
        }
        numColor.Remove(rnd);
        Renderer renderer = character.GetComponent<Renderer>();
        renderer.material= color;
    }
}
