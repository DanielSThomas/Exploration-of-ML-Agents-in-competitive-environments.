using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using UnityEngine.UI;

public class EliminationAgent : Agent
{
    private EliminationGameManager eliminationGameManager;

    [SerializeField] private Text healthText;
    [SerializeField] private float speed = 10;
    [SerializeField] private float turnspeed = 10;
    [SerializeField] private Rigidbody2D rb;
    private Health hp;
    private Transform spawn;
    
    [SerializeField] private float meanReward;
    [SerializeField] private int team; // 0 = Red Team  1 = Blue Team
    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private GameObject bulletobject;
    [SerializeField] private Rigidbody2D turretPivot;

    [SerializeField] private GameObject[] enemyRadar;
    [SerializeField] private GameObject[] bulletRadar; 
    [SerializeField] private BufferSensorComponent bufferSensorBullet;
    //[SerializeField] private BufferSensorComponent bufferSensorEnemy;
    [SerializeField] private int bulletNumber = 0;

    [SerializeField] private float firerate;
    private float nextShoot;
    private bool canShoot = true;
    

     

    // Start is called before the first frame update
    void Awake()
    {
        
        eliminationGameManager = GameObject.Find("EliminationGameManager").GetComponent<EliminationGameManager>();
        MaxStep = eliminationGameManager.getMaxStep();
        
        hp = GetComponent<Health>();
       

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        healthText.text = hp.getHealth().ToString();

    }

    public override void OnEpisodeBegin()
    {
        rb.angularVelocity = 0;
        rb.velocity = Vector2.zero;
        rb.rotation = 0;
        turretPivot.rotation = 0;
        
       
        
    }



    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(canShoot); // check if we can shoot

        //sensor.AddObservation(this.transform.position);


        switch (team)
        {
            case 0:

                bulletRadar = GameObject.FindGameObjectsWithTag("BlueBullet");
                //enemyRadar = GameObject.FindGameObjectsWithTag("BlueTeamParent"); // Needs fixing
                
                break;

            case 1:

                bulletRadar = GameObject.FindGameObjectsWithTag("RedBullet");
                //enemyRadar = GameObject.FindGameObjectsWithTag("RedTeamParent"); // Needs fixing
                break;         
        }

        System.Array.Sort(bulletRadar, (a, b) => (Vector3.Distance(a.transform.position, transform.position)).CompareTo(Vector3.Distance(b.transform.position, transform.position)));

       // System.Array.Sort(enemyRadar, (a, b) => (Vector3.Distance(a.transform.position, transform.position)).CompareTo(Vector3.Distance(b.transform.position, transform.position)));

        bulletNumber = 0;

        //Enemy and bullet buffer observastions
        //for (int i = 0; i < enemyRadar.Length; i++)
        //{

        //    if (enemyRadar[i] == null)
        //    {
        //        return;
        //    }

        //    float[] enemyObservation = new float[]
        //    {
               
        //        enemyRadar[i].transform.position.x - transform.position.x,
        //        enemyRadar[i].transform.position.y - transform.position.y
                           
        //    };

        //    bufferSensorEnemy.AppendObservation(enemyObservation);
        //}

        for (int i = 0; i < bulletRadar.Length; i++)
        {
            if(bulletRadar[i] == null || bulletNumber >= 10)
            {
                return;
            }

            float[] bulletObservation = new float[]
            {
                bulletRadar[i].transform.position.x - transform.position.x,
                bulletRadar[i].transform.position.y - transform.position.y
            };

            bulletNumber ++;

            bufferSensorBullet.AppendObservation(bulletObservation);
        }
       

      
        //Waypoint sensor ? Would be needed for waypoints
        

    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        // Actions

        Vector2 movedir = new Vector2(0, 0);

        int horizontalDir = actionBuffers.DiscreteActions[0];
        int verticalDir = actionBuffers.DiscreteActions[1];
        int turretRotDir = actionBuffers.DiscreteActions[2];
        int shooting = actionBuffers.DiscreteActions[3];

        switch (horizontalDir)
        {
            case 0: horizontalDir = 0; break;
            case 1: horizontalDir = -10; break;
            case 2: horizontalDir = 10; break;
        }

        switch (verticalDir)
        {
            case 0: movedir.y = 0; break;
            case 1: movedir.y = 1; break;
            case 2: movedir.y = -1; break;
        }

        switch (turretRotDir)
        {
            case 0: turretRotDir = 0; break;
            case 1: turretRotDir = 10; break;
            case 2: turretRotDir = -10; break;
        }


        switch (shooting)
        {
            case 0: break;  
            case 1: Shoot(); break;
        }

        
        rb.MoveRotation(rb.rotation += horizontalDir * turnspeed * Time.deltaTime);

        turretPivot.MoveRotation(turretPivot.rotation += turretRotDir * turnspeed * Time.deltaTime);

        rb.velocity = (Vector2)transform.up * movedir.y * speed * Time.deltaTime;


        //Rewards

        meanReward = GetCumulativeReward();

        //Idle Penalty

        AddReward(-1f / MaxStep);
        

        if (hp.getHealth() < 1)
        {
                       
            //Give blue team a score
            if(team == 0)
            {
                eliminationGameManager.addBlueScore();
            }
            //Give red team a score
            if (team == 1)
            {
                eliminationGameManager.addRedScore();
            }



            Debug.Log(this.gameObject.name + " Died with a score of: " + GetCumulativeReward());
            this.gameObject.SetActive(false);
        }

    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Wall")
    //    {
    //        AddReward(-0.1f);
    //    }
    //}


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

    public void setSpawn(Transform _spawn)
    {
        this.transform.localPosition = _spawn.position;
    }

    


    public void giveReward(float value)
    {
        AddReward(value);
    }


}
