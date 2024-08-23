using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] float scaler;
    [SerializeField] float animTimer;

    float initialSize;

    Vector3 initialScale;
    Vector3 targetScale;

    public bool jumping = false;

    void Start()
    {
        initialSize = transform.localScale.x;

        initialScale = transform.localScale;
        targetScale = scaler * transform.localScale;
    }

    void Update()
    {
        
    }

    public IEnumerator Hop()
    {
        Animator animator = GetComponent<Animator>();
        animator.SetBool("Hop", true);
        yield return new WaitForSeconds(animTimer);
        animator.SetBool("Hop", false);
    }

    public IEnumerator ScaleCoroutine()
    {
        if(!jumping)
        {
            jumping = true;
        }
        else
        {
            float elapsedTime = 0f;

            while (elapsedTime < animTimer)
            {
                // Interpolate the scale between initial and target scale
                float t = elapsedTime / animTimer;
                transform.localScale = Vector3.Lerp(initialScale, targetScale, t);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Ensure the final scale is exactly the target scale
            transform.localScale = targetScale;

            // Wait for a moment (optional)
            yield return new WaitForSeconds(0.1f);

            // Now scale back to the initial size
            while (elapsedTime > 0f)
            {
                float t = elapsedTime / animTimer;
                transform.localScale = Vector3.Lerp(targetScale, initialScale, t);

                elapsedTime -= Time.deltaTime;
                yield return null;
            }

            // Ensure the final scale is exactly the initial scale
            transform.localScale = initialScale;

            jumping = false;
        }  
    }
}
