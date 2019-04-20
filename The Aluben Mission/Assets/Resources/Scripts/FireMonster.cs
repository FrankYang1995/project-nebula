﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMonster : MonoBehaviour {

    private float speed = 0.25f;
    private Animator fireAnim;

    private float timeBtwExplosion;
    private float startTimeBtwExplosion;

    private GameObject player1;
    private GameObject player2;

    private Color myColor;

    private GameObject p1Bullet;
    private GameObject p2Bullet;

    private float health=100;

    protected float damaged;

    // Use this for initialization
    void Start () {

        player1 = GameObject.Find("Player 1");
        player2 = GameObject.Find("Player 2");

        fireAnim = GetComponent<Animator>();

        startTimeBtwExplosion = 0.7f;
        timeBtwExplosion = startTimeBtwExplosion;

        myColor = new Color(0.3443f, 0.9035f, 1f, 1f);
        p1Bullet = Resources.Load<GameObject>("Prefabs/P1Projectile");
        p2Bullet = Resources.Load<GameObject>("Prefabs/P2Projectile");
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        TakeDamagedAnimation();
        if (FindClosestFuel() != null)
        {
            if (fireAnim.GetBool("isExplosion") == false)
            {
                transform.position = Vector2.MoveTowards(transform.position, FindClosestFuel().transform.position, speed * Time.deltaTime);
            }
            else
            {
                Explosion();
            }
        }
        if(FindClosestFuel() == null)
        {
            fireAnim.SetBool("isExplosion", false);
        }
    }

    public void Explosion()
    {
        if (timeBtwExplosion  <= 0)
        {
            fireAnim.SetBool("isExplosion", false);

            timeBtwExplosion = startTimeBtwExplosion;
        }
        else
        {
            fireAnim.SetBool("isExplosion", true);

            timeBtwExplosion -= Time.deltaTime;
        }
    }
    public GameObject FindClosestFuel()
    {
        GameObject[] fuels;
        fuels = GameObject.FindGameObjectsWithTag("Fuel");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject fuel in fuels)
        {
            Vector3 diff = fuel.transform.position - position;
            float curDistace = diff.sqrMagnitude;
            if(curDistace < distance)
            {
                closest = fuel;
                distance = curDistace;
            }
        }
        if (fuels.Length == 0)
        {
            return player1;
        }
        else {
            return closest;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Fuel")
        {
            fireAnim.SetBool("isExplosion", true);

            player1.GetComponent<Player1Controller>().Damage(10);
            player2.GetComponent<Player2Controller>().Damage(10);

            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "P1Bullet" && p1Bullet.GetComponent<SpriteRenderer>().color == myColor || other.gameObject.tag == "P2Bullet" && p2Bullet.GetComponent<SpriteRenderer>().color == myColor)
        {
            health -= 20;
            Destroy(other.gameObject);
            damaged = 0;
        }
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    
    public void TakeDamagedAnimation()
    {
        this.GetComponent<SpriteRenderer>().color = new Color(255, damaged, damaged);
        damaged += 0.1f;
    }
    
}