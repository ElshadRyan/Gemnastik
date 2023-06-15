using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle : MonoBehaviour
{
    private Player player;

    
    private void Start()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
     
    }

    public void Attack()
    {
        transform.position = new Vector3(-4, 0, 0);
    }

    IEnumerator AnimationTiming()
    {
        yield return new WaitForSeconds(.5f);
        player.PlayAnimation(false);
    }
}
