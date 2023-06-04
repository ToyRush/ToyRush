using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public abstract class Bullet : MonoBehaviour
 {
    [SerializeField] AudioClip shootSound;
    protected AudioSource audioSource;
    protected Rigidbody2D rb;
    public float damage;
    public int speed;
    public float loadBullet;
    public WaitForSeconds shootDelay;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        SetBulletDelay();
    }

    public virtual void Init(Vector3 dir)
    {
        audioSource.PlayOneShot(shootSound, 0.3f);
        dir = dir.normalized;
        transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        rb.velocity = speed * dir;
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            Monster monster = collision.gameObject.GetComponent<Monster>();
            monster.Damaged((int)damage);
            gameObject.SetActive(false);
        }
        else
            gameObject.SetActive(false);
    }

    public abstract void SetBulletDelay();
}
