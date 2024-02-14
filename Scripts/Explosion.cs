using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        float t = anim.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        Invoke("Die",t-.05f);   
    }

    void Die()
    {
        Destroy(this.gameObject);
    }
}
