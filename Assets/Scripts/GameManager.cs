using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TrashItemManager trashItemManager;
    public Vector3 trashLoc;

    public Animator conveyerBelt_anim;
    public RuntimeAnimatorController trash_anim;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            conveyerBelt_anim.SetBool("isNext", true);
            GenerateRandomTrashItem();
        }
        else
        {
            conveyerBelt_anim.SetBool("isNext", false);
        }
    }

    public void GenerateRandomTrashItem()
    {
        GameObject prefab = trashItemManager.GetRandomTrashItemPrefab();
        if (prefab != null)
        {
            GameObject instance = Instantiate(prefab, trashLoc, Quaternion.identity);

            // Add an Animator component to the instantiated GameObject if it doesn't already have one
            Animator animator = instance.GetComponent<Animator>();
            if (animator == null)
            {
                animator = instance.AddComponent<Animator>();
            }

            // Assign the Animator Controller to the Animator component
            animator.runtimeAnimatorController = trash_anim;
        }
    }
}
