using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Player player;
    public float speed;

    protected Vector3 vel = Vector3.zero;


    public List<Dialogue> dialoguesToLoad = new List<Dialogue>();

    void LateUpdate()
    {
        Vector3 pos = transform.position;
        pos = Vector3.SmoothDamp(transform.position, new Vector3(0.5f,player.transform.position.y,-10), ref vel, speed);
        pos.y = Mathf.Clamp(pos.y, 0, Mathf.Infinity);
        transform.position = pos;
    }
}
