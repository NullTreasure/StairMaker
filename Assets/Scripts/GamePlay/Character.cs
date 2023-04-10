using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private GameObject stairBrick;
    [SerializeField] protected LayerMask BridgeBrick;
    [SerializeField] protected LayerMask Bridge;
    [SerializeField] protected LayerMask specialBridgeBrick;
    public List<GameObject> collectedBrick = new List<GameObject>();

    public GameObject skin;
    public GameObject groundState;

    public int countBrick;
    public ColorTypes.Color color;
    public void addBrick()
    {
        countBrick++;
        GameObject brick = Instantiate(brick);
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
                countBrick--;
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
                countBrick--;
            }
        }
    }
}
