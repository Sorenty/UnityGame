using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyBoard_Simple : MonoBehaviour
{


    // Start is called before the first frame update
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
    void Start() //Метод-процедура 
    {
        //Create a value for physic Body in 2 2D
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

    // Update is called once per frame
    void Update()
    {
        Vector2 vel = vig.linearVelocity; // сохраняем текущую вертикальную скорость

        if (Input.GetKey(KeyCode.D))
        {
            vel.x = x; // движение вправо
            vig.linearVelocity = vel;
            anim.Play("Vpravo");
        }
        else if (Input.GetKey(KeyCode.A))
        {
            vel.x = -x; // движение влево
            vig.linearVelocity = vel;
            anim.Play("Vlevo");
        }
        else
        {
            // если не нажимаем A/D — оставляем x-скорость
            vel.x = 0;
            vig.linearVelocity = vel;
            anim.Play("Stoit");
        }

        if (Input.GetKeyDown(KeyCode.Space) && isground)
        {
            Vector2 Force = new Vector2(0, 15f); // слабее прыжок
            vig.AddForce(Force, ForceMode2D.Impulse);
        }
    }


    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Health pack")
        {
            print("This is health pack!");

            Vector3 spawnPos = coll.transform.position;
            lastHealthPosition = coll.transform.position;
            Destroy(coll.gameObject);

            health += 50;
            num_of_eat_hp++;

            Invoke(nameof(RespawnHealth), 10f);
        }
        if (coll.gameObject.tag == "Axe")
        {
            print("I have found axe!");
            Destroy(coll.gameObject);
            haveAxe = true;
        }
        if ((haveAxe) && (coll.gameObject.tag == "tree") && (Input.GetMouseButtonDown(0)))
        {
            print("I am destroying this tree!");
            Destroy(coll.gameObject);
            tree = tree + 10;
        }
        if ((haveAxe) && (coll.gameObject.tag == "tree1") && (Input.GetMouseButtonDown(0)))
        {
            print("I am destroying this tree1!");
            Destroy(coll.gameObject);
            tree = tree + 10;
            lestva = lestva + 10;

        }
        if (coll.gameObject.tag == "Coster")
        {
            health = health - 1;

        }
    }
    void OnCollisionStay2D(Collision2D crash)
    {
        if (crash.gameObject.tag == "Map")
        {
            isground = true;
            return;
        }
        return;
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

    // vig:Rigitbody; a:longint; Rigitbody
