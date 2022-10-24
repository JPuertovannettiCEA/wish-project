using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEffect : MonoBehaviour
{
    public GameObject effect;
    private Transform player;

    private void Start()
    {
        player = GameManager.instance.player.transform;
    }

    public void Use()
    {
        Instantiate(effect,player.position, Quaternion.identity);
        if(this.tag == "Health")
        {
            //REGAIN HEALTH
        }
        Destroy(gameObject);
    }
}
