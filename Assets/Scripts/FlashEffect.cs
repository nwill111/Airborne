using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashEffect : MonoBehaviour
{

    public Material flashMaterial;

    private Material defaultMaterial;
    private SpriteRenderer sprite;
    private Coroutine flashRoutine;

    // Start is called before the first frame update
    // Assign sprite and material
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        defaultMaterial = sprite.material;
    }

    //Starts flash
    public void StartFlash()
    {
        //If flash is already started...
        if (flashRoutine != null)
            {
                //Stop flash
                StopCoroutine(flashRoutine);
            }
    
            //Start flash
            flashRoutine = StartCoroutine(SpriteFlash());
    }

    private IEnumerator SpriteFlash()
        {
           
           //Set material to flash material
            sprite.material = flashMaterial;

            //wait a bit...
            yield return new WaitForSeconds(0.125f);

            //Set it back
            sprite.material = defaultMaterial;
            flashRoutine = null;
        }
}
