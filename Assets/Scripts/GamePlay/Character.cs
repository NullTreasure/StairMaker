using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
    [SerializeField] private GameObject stairBrick;
    [SerializeField] protected LayerMask BridgeBrick;
    [SerializeField] protected LayerMask Bridge;
    [SerializeField] protected LayerMask specialBridgeBrick;
    [SerializeField] protected GameObject prefabBrick;
    [SerializeField] protected GameObject character;
    public bool getHit;
    public Animator anim;
    public List<GameObject> collectedBrick = new List<GameObject>();

    public GameObject skin;
    public GameObject groundState;
    public bool onBridge;


    public ColorTypes.Color color;
    public void addBrick()
    {
        GameObject brick = Instantiate(prefabBrick,this.transform);
        brick.transform.localPosition = new Vector3(0, 0.3f, -0.5f) + Vector3.up * 0.21f * collectedBrick.Count;
        brick.GetComponent<Renderer>().material = skin.GetComponent<Renderer>().material;
        collectedBrick.Add(brick);
    }
    public void checkBrick()
    {
        RaycastHit Hit;
        if (Physics.Raycast(transform.position + Vector3.up * 2, Vector3.down, out Hit, Mathf.Infinity, BridgeBrick))
        {
            MeshRenderer meshRenderer = Hit.transform.GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                meshRenderer.enabled = true;
            }
            Renderer check = Hit.transform.GetComponent<Renderer>();
            if (!check.material.name.StartsWith(skin.GetComponent<Renderer>().material.name))
            {
                check.material = skin.GetComponent<Renderer>().material;
                Destroy(collectedBrick[collectedBrick.Count - 1]);
                collectedBrick.RemoveAt(collectedBrick.Count - 1);
            }
        }
    }
    public void checkSpecialBrick()
    {
        RaycastHit Hit;
        if (Physics.Raycast(transform.position + Vector3.up * 2, Vector3.down, out Hit, Mathf.Infinity, specialBridgeBrick))
        {
            MeshRenderer meshRenderer = Hit.transform.GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                meshRenderer.enabled = true;
            }
            Renderer check = Hit.transform.GetComponent<Renderer>();
            if (!check.material.name.StartsWith(skin.GetComponent<Renderer>().material.name))
            {
                check.material = skin.GetComponent<Renderer>().material;
                if (collectedBrick.Count > 0)
                {
                    Destroy(collectedBrick[collectedBrick.Count - 1]);
                    collectedBrick.RemoveAt(collectedBrick.Count - 1);
                }
            }
        }
    }
    public void removeAllBrick()
    {
        foreach (GameObject brick in collectedBrick) Destroy(brick);
        collectedBrick.Clear();
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (!onBridge)
        {
            if (other.name == "Player" || other.name.StartsWith("Enemy"))
            {
                if (collectedBrick.Count < other.GetComponent<Character>().collectedBrick.Count)
                {
                    getHit = true;
                    for (int i = 0; i < collectedBrick.Count; i++)
                    {
                        other.GetComponent<Character>().addBrick();
                    }
                    removeAllBrick();
                }
            }
        }
    }

    protected virtual void standUp()
    {
        anim.SetBool("fall", false);
        getHit = false;
    }

    
}
