using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    EnemyMover enemyMover;
    [SerializeField] Transform headLocator;
    // Start is called before the first frame update
    void Start()
    {
        enemyMover = FindObjectOfType<EnemyMover>();

    }

    // Update is called once per frame
    void Update()
    {
        if (enemyMover != null)
        {
            headLocator.LookAt(new Vector3(enemyMover.transform.position.x, transform.position.y, enemyMover.transform.position.z));
        }
    }
}
