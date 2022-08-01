using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public class ShatterMesh : MonoBehaviour {

    public float delay = 0f;
    public int depth = 1;

    public GameObject[] Shatter(GameObject objectToShatter) {
        Renderer renderer = objectToShatter.GetComponent<Renderer>();
        Material crossSectionMaterial = renderer.material;
        return objectToShatter.SliceInstantiate(GetRandomPlane(),
                                                            new TextureRegion(0.0f, 0.0f, 1.0f, 1.0f),
                                                            crossSectionMaterial);
    }

    public EzySlice.Plane GetRandomPlane() {
        Vector3 randomPosition = Random.insideUnitSphere;
        Vector3 randomDirection = Random.insideUnitSphere.normalized;

        return new EzySlice.Plane(randomPosition, randomDirection);
    }

    public void ShatterThisObject()
    {
        PerformShatter(gameObject, depth);

        // finally delete the main object
        Destroy(gameObject);
    }

    public void Update()
    {
        delay -= Time.deltaTime;
        if (delay < 0f)
        {
            ShatterThisObject();
        }
    }

    private void PerformShatter(GameObject objectToShatter, int depthLeft) {
//        depthLeft--;

        GameObject[] shatters = Shatter(objectToShatter);
        if(shatters == null)
        {
            Debug.Log("shatters: 0");
        }
        else
        {
            Debug.Log("shatters: " + shatters.Length);
        }

        if (shatters != null && shatters.Length > 0) {
            depthLeft--;
            // add rigidbodies and colliders
            foreach (GameObject shatteredObject in shatters) {
                shatteredObject.name += " depth: " + depthLeft;
                shatteredObject.AddComponent<MeshCollider>().convex = true;
                shatteredObject.AddComponent(typeof(DeleteAfterDelay));
                Rigidbody rigidbody = shatteredObject.AddComponent<Rigidbody>();
                rigidbody.AddForce(new Vector3(Random.value, Random.value, Random.value)*15, ForceMode.Impulse);
                rigidbody.AddTorque(new Vector3(Random.Range(-500f, 500f), Random.Range(-500f, 500f), Random.Range(-500f, 500f)), ForceMode.VelocityChange);
                Debug.Log("created object at depth:" + depthLeft);
                if (depthLeft > 0)
                {
                    PerformShatter(shatteredObject, depthLeft);
//                    Destroy(shatteredObject);
                    Debug.Log("deleted object at depth:" + depthLeft);
                }
            }
        }
    }
}
