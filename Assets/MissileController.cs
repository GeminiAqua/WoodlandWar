using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour {
    
    [System.Serializable]
    public class SpecialAttack{
        public bool isSpecial;
        public GameObject specialObject;
        public Collider[] allTargets;
        public float radiusAoE = 5.0f;
    }
    
    public bool lookAtTarget;
	public Transform target;
    public Rigidbody rBody;
    public float damage = 0;
    public float speed = 20f;
    public float destroyDelay = 0f;
    public SpecialAttack specialAttack = new SpecialAttack();
    
    void Start(){
        rBody = gameObject.GetComponent<Rigidbody>();
    }
    
	void FixedUpdate () {
		seekTarget();
	}
    
    void OnTriggerEnter(Collider collider){
        if (collider.tag.Equals("Enemy")){
            if (specialAttack.isSpecial){
                findClosestTargets(collider);
            }
            collider.GetComponent<Health>().takeDamage(damage);
            Destroy(gameObject, destroyDelay);
        }
    }
    
    void findClosestTargets(Collider colliderSpecial){
        specialAttack.allTargets = Physics.OverlapSphere(gameObject.transform.position, specialAttack.radiusAoE);
        foreach (Collider enemy in specialAttack.allTargets){
            if (enemy.tag.Equals("Enemy")){
                colliderSpecial.GetComponent<Health>().takeDamage(damage);
                Instantiate(specialAttack.specialObject, enemy.transform);
            }
        }
    }
    
    void seekTarget(){
        if (lookAtTarget){
            transform.LookAt(target, new Vector3(0, 2.0f, 0));
        }
        rBody.velocity = transform.forward * speed;
        if (target == null){
            rBody.velocity = new Vector3(0, -1, 0) * speed;
            Destroy(gameObject, 0.5f);
        }
    }
    
    public void setTarget(Transform tar){
        target = tar;
    }
    
    public void setDamage(float amount){
        damage = amount;
    }
    
}
