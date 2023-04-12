using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player" || other.name.StartsWith("Enemy"))
        {
            GameManager.Instance.endGame = true;
            GameManager.Instance.deleteAllState();
            CameraFollowing.Instance.followTheWinner(other.gameObject);
            other.GetComponent<Character>().removeAllBrick();
            other.GetComponent<Character>().anim.SetBool("win", true);
            other.GetComponent<Character>().transform.position = new Vector3(0.01962265f, 10.02f, 80.29436f);
        }
    }
}
