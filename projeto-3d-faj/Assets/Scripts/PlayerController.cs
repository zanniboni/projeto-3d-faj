using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        verificaMorte();
    }

    void OnCollisionEnter(Collision col)
    {


    }

    void verificaMorte()
    {

        if (gameObject.GetComponent<PlayerHealth>().getHealth() < 0.1)
        {
            Destroy(gameObject, 0.8f);
            FindObjectOfType<GameManager>().EndGame();
        }
    }
}
