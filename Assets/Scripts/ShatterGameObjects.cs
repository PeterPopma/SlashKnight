using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Drop this script on objects you want to shatter.
// Perform shatter by calling ShatterObject()
// objects must have a collider
// Read/Write must be allowed on meshes.
public class ShatterGameObjects : MonoBehaviour
{
//    public float delay = 3.0f;

    // Update is called once per frame
    void Update()
    {
//        delay -= Time.deltaTime;
//        if (delay < 0f)
//            CreateShatterObjects(transform);
    }

    public void ShatterObject()
    {
        ShatterObject(transform);
    }

    public void ShatterObject(Transform root)
    {
        foreach(Transform transform in root)
        {
            BoxCollider boxCollider = transform.gameObject.GetComponent<BoxCollider>();
            if (boxCollider == null)
            {
                transform.gameObject.AddComponent(typeof(BoxCollider));
            }
            Rigidbody rigidbody = transform.gameObject.GetComponent<Rigidbody>();
            if (rigidbody==null)
            {
                transform.gameObject.AddComponent(typeof(Rigidbody));
            }
     //       rigidbody.AddForce(new Vector3(Random.value, Random.value, Random.value)*5, ForceMode.Impulse);
     //       rigidbody.AddTorque(new Vector3(Random.Range(-500f, 500f), Random.Range(-500f, 500f), Random.Range(-500f, 500f)), ForceMode.VelocityChange); 
            transform.parent = transform.parent.parent;
            transform.gameObject.AddComponent(typeof(ShatterMesh));
            ShatterObject(transform);
        }
        Destroy(gameObject);
    }
}
