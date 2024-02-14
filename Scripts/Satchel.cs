using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class Satchel : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public Player player;
    public GameObject explosion;
    public Sprite landedSprite;
    SpriteRenderer sprite;

    public float blastForce;
    public float blastRadius;

    // Update is called once per frame
    void Update()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    public void Explode()
    {
        Vector2 direction = player.transform.position - this.transform.position;

        float distance = Vector2.Distance(player.transform.position, transform.position);

        float force = (blastRadius-distance)/blastRadius;
        
        if(force < 0)
        {
            force = 0;
        }

        if (force > 0)
        {
            Vector2 velDirection = player.rb2d.velocity;

            
            if(Vector2.Distance(direction.normalized,velDirection.normalized) > .45f)
            {
                player.rb2d.velocity = Vector2.zero;
                player.velocity = Vector2.zero;
            }
            else
            {
                player.rb2d.velocity = new Vector2(player.rb2d.velocity.x, 0);
                player.velocity.y = 0;
            }

            player.rb2d.AddForce(direction.normalized * force * blastForce);
        }
        Instantiate(explosion, this.transform.position,Quaternion.identity);
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(sprite !=null)
        sprite.sprite = landedSprite;
        if(other != null)
        {
            ContactPoint2D contact = other.GetContact(0);

            transform.up = new Vector2(transform.position.x, transform.position.y) - contact.point;

            player.PlaySound(player.sm.stickSound, .7f);

            rb2d.bodyType = RigidbodyType2D.Static;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position,blastRadius);
    }

}
