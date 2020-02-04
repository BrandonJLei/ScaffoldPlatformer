using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{

  public var Target : Transform;    
  public var Projectile : Transform;
  
  public var MaximumLookDistance : float = 30;
  public var MaximumAttackDistance : float = 10;
  public var MinimumDistanceFromPlayer : float = 2;
  
  public var rotationDamping : float = 0.5;
  public var shotTime : float = 0;
  
function Update ()  

{
  
      var distance = Vector3.Distance(Target.position, transform.position);
  
      if(distance <= MaximumLookDistance) 
	{
          LookAtTarget ();
  
          if(distance <= MaximumAttackDistance && (Time.time - shotTime) > shotInterval)
		{
		    Shoot();
		}
      }   
}
  
  
function LookAtTarget () 
{
      var dir = Target.position - transform.position;
      dir.y = 0;
      var rotation = Quaternion.LookRotation(dir);
      transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime *   rotationDamping);
}
  
function Shoot()
{
    shotTime = Time.time; 
    Instantiate(Projectile, transform.position + (Target.position - transform.position).normalized, Quaternion.LookRotation(Target.position - transform.position));
} 
}
