using UnityEngine;
using System.Collections;

public class ClassPlayer : ClassGeneric
{

    public float flySpeed;
    public float shootSpeed;
    public GameObject bullet;
    public GameObject death;
    public GameObject hit;

    private ScriptGame sg;
    private int hp;

    private Transform machineGun1;
    private Transform machineGun2;
    private bool machine;

    private GameObject hp0;
    private GameObject hp00;
    private GameObject hp000;

    public bool lockShoot;
    private float overHeat;
    private GameObject heat;

    private float coldSpeed;

    private int hpCache;
    private int hpHud;

    public Shaker shaker;
    void Awake()
    {
        sg = GameObject.Find("GameManager").GetComponent<ScriptGame>();
        machineGun1 = GameObject.Find("Player/Machine Gun 1").transform;
        machineGun2 = GameObject.Find("Player/Machine Gun 2").transform;

        overHeat = 1;
        coldSpeed = 0.5f+(0.33f * PlayerPrefs.GetInt("AT",0));

        flySpeed = 50 + (10 * PlayerPrefs.GetInt("SP", 0));

        hp = 50 + (50 * PlayerPrefs.GetInt("HP", 0));
        hpCache = 0;
        hpHud = hp;

        hp0 = GameObject.Find("HP/0");
        hp00 = GameObject.Find("HP/00");
        hp000 = GameObject.Find("HP/000");

        lockShoot = false;
        heat = GameObject.Find("Heat");
    }

    void Start()
    {
        int aux = hpHud;
        BehaviourAnimation.Play(hp0, (aux % 10).ToString());
        aux /= 10;
        BehaviourAnimation.Play(hp00, (aux % 10).ToString());
        aux /= 10;
        BehaviourAnimation.Play(hp000, (aux % 10).ToString());
    }

    void Update()
    {
        if(hpCache > 0)
        {
            if (hpCache >= 100)
            {
                hpCache -= 100;
                hpHud -= 100;
            }
            else
            {
                hpCache--;
                hpHud--;
            }

            int aux = hpHud;
            BehaviourAnimation.Play(hp0, (aux % 10).ToString());
            aux /= 10;
            BehaviourAnimation.Play(hp00, (aux % 10).ToString());
            aux /= 10;
            BehaviourAnimation.Play(hp000, (aux % 10).ToString());
        }

        if(overHeat <= 0)
        {
            lockShoot = false;
            BehaviourAnimation.Play(heat, "Okay");
            overHeat = 1;
        }
        overHeat -= coldSpeed;
        heat.transform.localPosition = new Vector2(71.5f, -24.0f + (24.5f * overHeat/100));
    }

    public void Shoot()
    {
        GameObject obj;

        if(machine)
        {
            obj = bullet.Spawn(machineGun1);
            obj.transform.eulerAngles = Vector3.forward * Random.Range(3f, -1f);
            machine = false;
        }
        else
        {
            obj = bullet.Spawn(machineGun2);
            obj.transform.eulerAngles = Vector3.forward * Random.Range(1f, -3f);
            machine = true;
        }
        obj.transform.parent = null;
        BehaviourPhysics.Move(obj, obj.transform.up, shootSpeed);

        overHeat += 1.8f;
        if(overHeat >= 100)
        {
            lockShoot = true;
            BehaviourAnimation.Play(heat, "Crit");
            overHeat = 100;
        }
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        GameObject aux;
        if(c.name == "Bullet Enemy")
        {
            aux = hit.Spawn(c.transform);
            aux.transform.parent = null;
            shaker.Shake(1, 0.5f);
            Damage(3);
        }
        else if(c.CompareTag("Enemy"))
        {
            aux = hit.Spawn(c.transform);
            aux.transform.parent = null;
            aux = hit.Spawn(c.transform);
            aux.transform.parent = null;
            shaker.Shake(2, 1f);
            Damage(6);
        }
        else if(c.name == "Bomb")
        {
            aux = hit.Spawn(c.transform);
            aux.transform.parent = null;
            aux = hit.Spawn(c.transform);
            aux.transform.parent = null;
            aux = hit.Spawn(c.transform);
            aux.transform.parent = null;
            shaker.Shake(3, 1.5f);
            Damage(9);
        }
    }

    void OnCollisionStay2D(Collision2D c)
    {
        if(c.gameObject.name == "Core")
        {
            GameObject aux;
            aux = hit.Spawn(transform);
            aux.transform.parent = null;
            shaker.Shake(1, 0.5f);
            Damage(1);
        }
    }

    private void Damage(int dmg)
    {
        hp -= dmg;
        hpCache += dmg;
        if (hp <= 0)
        {
            hpCache = 0;
            hpHud = 0;
            GameObject obj = death.Spawn(transform);
            obj.transform.parent = null;
            gameObject.SetActive(false);
            sg.GameOver();
        }
    }
}
