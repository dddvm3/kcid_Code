using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockeShoot_Shoot : MonoBehaviour
{
    public static LockeShoot_Shoot Instance;
    private LockeShootMaster lockeshootMaster;
    public Transform myTransform;
    private Transform camTransform;
    private RaycastHit hit;
    public float range = 30;
    private float offsetFactor = 7;
    private Vector3 startPosition;

	void OnEnable()
	{
        SetInitialReferences();
        lockeshootMaster.EventPlayerInput += OpenFire;
	}

	void OnDisable()
	{
        lockeshootMaster.EventPlayerInput -= OpenFire;
    }
	
	void SetInitialReferences()
	{
        Instance = this;
        lockeshootMaster = GetComponent<LockeShootMaster>();      
	}

    void OpenFire()
    {
        Vector3 newpos = new Vector3(myTransform.localPosition.x, myTransform.localPosition.y, myTransform.localPosition.z + range);
        if (Physics.Raycast(myTransform.localPosition, myTransform.forward, out hit, range))
        {
            lockeshootMaster.CallNormalLockeShootHitDefault(hit.point, hit.transform);
            Debug.DrawRay(myTransform.localPosition, myTransform.forward, Color.red);
            Debug.Log(hit.collider.tag);

            if(hit.transform.CompareTag(GameManager_Reference._EnemyTag))
            {
                Debug.Log("Shoot Enemy");              
                lockeshootMaster.CallNormalLockeShootHitEnemy(hit.point, hit.transform);                              
            }
        }
    }
}