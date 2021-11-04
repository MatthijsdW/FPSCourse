using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    public int curHp;
    public int maxHp;
    public int scoreToGive;

    [Header("Movement")]
    public float moveSpeed;
    public float attackRange;
    public float yPathOffset;

    [Header("Drops")]
    public float pickupChance;
    public GameObject[] possiblePickups;

    private List<Vector3> path;

    private Weapon weapon;
    private GameObject target;

    public static List<Enemy> instances = new List<Enemy>();

    private void Start()
    {
        instances.Add(this);
        weapon = GetComponent<Weapon>();
        target = FindObjectOfType<Player>().gameObject;

        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);

        if (distance <= attackRange)
        {
            if (weapon.CanShoot())
                weapon.Shoot();
        }
        else
        {
            ChaseTarget();
        }

        Vector3 direction = (target.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

        transform.eulerAngles = Vector3.up * angle;
    }

    private void ChaseTarget()
    {
        if (path.Count == 0)
            return;

        transform.position = Vector3.MoveTowards(transform.position, path[0] + new Vector3(0, yPathOffset, 0), moveSpeed * Time.deltaTime);

        if (transform.position == path[0] + new Vector3(0, yPathOffset, 0))
            path.RemoveAt(0);
    }

    private void UpdatePath()
    {
        NavMeshPath navMeshPath = new NavMeshPath();
        NavMesh.CalculatePath(transform.position, target.transform.position, NavMesh.AllAreas, navMeshPath);

        path = navMeshPath.corners.ToList();
    }

    public void TakeDamage(int damage)
    {
        curHp -= damage;

        if (curHp <= 0)
            Die();
    }

    private void Die()
    {
        if (Random.value > pickupChance)
        {
            GameObject pickupDrop = Instantiate(possiblePickups[Random.Range(0, possiblePickups.Length)]);
            pickupDrop.transform.position = transform.position;
        }

        GameManager.instance.AddScore(scoreToGive);

        instances.Remove(this);
        Destroy(gameObject);
    }
}
