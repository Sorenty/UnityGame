using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyBoard_Simple : MonoBehaviour
{
    // ===== VFX =====
    public ParticleSystem pickupVfxPrefab;
    public ParticleSystem chopVfxPrefab;
    public ParticleSystem jumpVfxPrefab;

    // ===== AUDIO =====
    public AudioSource sfxSource;
    public AudioClip chopSound;
    public AudioClip pickupSound;

    float x, y;
    Rigidbody2D vig;
    Animator anim;

    public int health;
    bool isground;

    public int num_of_eat_hp;
    public int tree;
    public int lestva;

    public ItemSpawner spawner;

    Vector3 lastHealthPosition;
    bool haveAxe;

    void Start()
    {
        num_of_eat_hp = 0;
        vig = GetComponent<Rigidbody2D>();
        x = 60F;
        anim = GetComponent<Animator>();

        health = 100;
        tree = 0;
        lestva = 0;

        haveAxe = false;
        isground = true;
    }

    void Update()
    {
        Vector2 vel = vig.linearVelocity;

        if (Input.GetKey(KeyCode.D))
        {
            vel.x = x;
            vig.linearVelocity = vel;
            anim.Play("Vpravo");
        }
        else if (Input.GetKey(KeyCode.A))
        {
            vel.x = -x;
            vig.linearVelocity = vel;
            anim.Play("Vlevo");
        }
        else
        {
            vel.x = 0;
            vig.linearVelocity = vel;
            anim.Play("Stoit");
        }

        if (Input.GetKeyDown(KeyCode.Space) && isground)
        {
            Vector2 Force = new Vector2(0, 15f);
            vig.AddForce(Force, ForceMode2D.Impulse);
        }
    }

    void SpawnVfx(ParticleSystem prefab, Vector3 position)
    {
        if (prefab == null)
        {
            Debug.LogWarning("VFX prefab не назначен");
            return;
        }

        ParticleSystem vfx = Instantiate(prefab, position, Quaternion.identity);
        vfx.Play();

        Destroy(vfx.gameObject, 2f);
    }

    void PlaySound(AudioClip clip)
    {
        if (sfxSource != null && clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    void OnTriggerStay2D(Collider2D coll)
    {
        // ===== HEALTH PACK =====
        if (coll.gameObject.tag == "Health pack")
        {
            print("This is health pack!");

            lastHealthPosition = coll.transform.position;

            SpawnVfx(pickupVfxPrefab, coll.transform.position);
            PlaySound(pickupSound);

            Destroy(coll.gameObject);

            health += 50;
            num_of_eat_hp++;

            Invoke(nameof(RespawnHealth), 10f);
        }

        // ===== AXE =====
        if (coll.gameObject.tag == "Axe")
        {
            print("I have found axe!");

            SpawnVfx(pickupVfxPrefab, coll.transform.position);
            PlaySound(pickupSound);

            Destroy(coll.gameObject);
            haveAxe = true;
        }

        // ===== TREE =====
        if ((haveAxe) && (coll.gameObject.tag == "tree") && (Input.GetMouseButtonDown(0)))
        {
            print("I am destroying this tree!");

            Vector3 pos = coll.bounds.center;
            pos.y = coll.bounds.min.y;

            SpawnVfx(chopVfxPrefab, pos);
            PlaySound(chopSound);

            Destroy(coll.gameObject);
            tree += 10;
        }

        // ===== TREE1 =====
        if ((haveAxe) && (coll.gameObject.tag == "tree1") && (Input.GetMouseButtonDown(0)))
        {
            print("I am destroying this tree1!");

            Vector3 pos = coll.bounds.center;
            pos.y = coll.bounds.min.y;

            SpawnVfx(chopVfxPrefab, pos);
            PlaySound(chopSound);

            Destroy(coll.gameObject);
            tree += 10;
            lestva += 10;
        }

        if (coll.gameObject.tag == "Coster")
        {
            health -= 1;
        }
    }

    void OnCollisionStay2D(Collision2D crash)
    {
        if (crash.gameObject.tag == "Map")
        {
            isground = true;
            return;
        }
    }

    void OnCollisionExit2D(Collision2D crash)
    {
        if (crash.gameObject.tag == "Map")
        {
            isground = false;
        }
    }

    void RespawnHealth()
    {
        if (spawner != null)
        {
            spawner.SpawnHealthPack(lastHealthPosition);
        }
    }
}