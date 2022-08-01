using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private GameObject swordImpactPosition;
    private StarterAssetsInputs starterAssetsInputs;
    private Animator animator;
    private const int LAYER_SLASH = 1;
    private float slashTimeLeft;
    private bool isSlashing;
    private bool checkedObjectsToSmash;

    public bool IsSlashing { get => isSlashing; set => isSlashing = value; }

    void Awake()
    {
        swordImpactPosition = GameObject.Find("SwordImpactPosition");
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (slashTimeLeft>0)
        {
            slashTimeLeft -= Time.deltaTime;
            if (slashTimeLeft < 0.5 && !checkedObjectsToSmash) 
            {
                checkedObjectsToSmash = true;
                CheckForObjectsToShatter();
            }
            if (slashTimeLeft <= 0)
            {
                isSlashing = false;
                animator.SetLayerWeight(LAYER_SLASH, 0f);
                animator.SetLayerWeight(LAYER_SLASH, Mathf.Lerp(animator.GetLayerWeight(LAYER_SLASH), 0f, Time.deltaTime * 50f));
            }
        }
        else if (starterAssetsInputs.slash)
        {
            isSlashing = true;
            checkedObjectsToSmash = false;
            starterAssetsInputs.slash = false;
            slashTimeLeft = 1.2f;
            animator.Play("SwordSlash", LAYER_SLASH, 0f);
            animator.SetLayerWeight(LAYER_SLASH, Mathf.Lerp(animator.GetLayerWeight(LAYER_SLASH), 1f, Time.deltaTime * 50f));
        }
    }

    private void CheckForObjectsToShatter()
    {
        Collider[] colliders = Physics.OverlapSphere(swordImpactPosition.transform.position, 2.0f);

        foreach (Collider collider in colliders)
        {
            ShatterGameObjects shatter = collider.gameObject.GetComponent<ShatterGameObjects>();
            if (shatter == null && collider.transform.parent!=null)
            {
                shatter = collider.transform.parent.gameObject.GetComponent<ShatterGameObjects>();
            }

            if (shatter!=null)
            {
                shatter.ShatterObject();
            }
        }
    }
}
