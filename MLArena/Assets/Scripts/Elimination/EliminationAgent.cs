using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class EliminationAgent : Agent
{

    [SerializeField] private float speed = 10;
    [SerializeField] private float turnspeed = 10;
    private Rigidbody2D rb;
    private Health hp;
    private MeshRenderer mr;
    [SerializeField] private Transform spawn;
    [SerializeField] private float meanReward;
    [SerializeField] private int team;
    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private GameObject bulletobject;

    [SerializeField] private float firerate;
    private float nextShoot;
    private bool canShoot = true;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        hp = GetComponent<Health>();

        //mr = GetComponent<MeshRenderer>();

        //update team colour----
        //if (team == 0)
        //{
        //    mr.material.color = Color.red;
        //}
        //else if (team == 1)
        //{
        //    mr.material.color = Color.blue;
        //}

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnEpisodeBegin()
    {
        this.gameObject.SetActive(true);
        rb.angularVelocity = 0;
        rb.velocity = Vector2.zero;
        this.transform.localPosition = spawn.position;
        hp.setHealth(3);

    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(canShoot); // check if we can shoot

        //sensor.AddObservation(rb.velocity.x);
        //sensor.AddObservation(rb.velocity.z);
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        // Actions

        Vector2 movedir = new Vector2(0, 0);

        int horizontalDir = actionBuffers.DiscreteActions[0];
        int verticalDir = actionBuffers.DiscreteActions[1];
        int rotDir = actionBuffers.DiscreteActions[2];
        int shooting = actionBuffers.DiscreteActions[3];

        switch (horizontalDir)
        {
            case 0: movedir.x = 0; break;
            case 1: movedir.x = 1; break;
            case 2: movedir.x = -1; break;
        }

        switch (verticalDir)
        {
            case 0: movedir.y = 0; break;
            case 1: movedir.y = 1; break;
            case 2: movedir.y = -1; break;
        }

        switch (rotDir)
        {
            case 0: rotDir = 0; break;
            case 1: rotDir = 10; break;
            case 2: rotDir = -10; break;
        }


        switch (shooting)
        {
            case 0: break;  
            case 1: Shoot(); break;
        }

        
        rb.MoveRotation(rb.rotation += rotDir * turnspeed);

        rb.AddForce(movedir.normalized * speed);


        //Rewards

        meanReward = GetCumulativeReward();
        
        AddReward(-1f / MaxStep);

        if (hp.getHealth() < 1)
        {
            AddReward(-1f);         
            Debug.Log(this.gameObject.name + " Died and Score: " + GetCumulativeReward());
            this.gameObject.SetActive(false);
        }

    }

    public override void Heuristic(in ActionBuffers actionsOut) // Player Control
    {
        var discreteActionsOut = actionsOut.DiscreteActions;

        switch (Input.GetAxisRaw("Horizontal"))
        {
            case -1: discreteActionsOut[0] = 2; break;
            case 0: discreteActionsOut[0] = 0; break;
            case 1: discreteActionsOut[0] = 1; break;
        }

        switch (Input.GetAxisRaw("Vertical"))
        {
            case -1: discreteActionsOut[1] = 2; break;
            case 0: discreteActionsOut[1] = 0; break;
            case 1: discreteActionsOut[1] = 1; break;
        }

        if (Input.GetKey(KeyCode.K))
        {
            discreteActionsOut[2] = 1;
        }
        else if (Input.GetKey(KeyCode.L))
        {
            discreteActionsOut[2] = 2;
        }
        else
        {
            discreteActionsOut[2] = 0;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            discreteActionsOut[3] = 1; 
        }
        else
        {
            discreteActionsOut[3] = 0;
        }


    }

    private void Shoot()
    {
        if (Time.time > nextShoot)
        {
            nextShoot = Time.time + firerate;

            canShoot = true;
        }

        if(canShoot == true)
        {
            GameObject _bullet = Instantiate(bulletobject, bulletSpawn.position, bulletSpawn.rotation);
            _bullet.GetComponent<Bullet>().setbulletOwner(this);
            _bullet.GetComponent<Bullet>().setbulletTeam(team);


            canShoot = false;
        }

        

    }

    public int getTeam()
    {
        return team;
    }



}
