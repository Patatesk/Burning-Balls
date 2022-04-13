using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Follo : MonoBehaviour
{
    [SerializeField]
    private Transform[] routes;

    private int routeToGo;

    private float tParam;

    private Vector3 objectPosition;

    private float speedModifier;

    public bool coroutineAllowed;

    public bool runing;

    // Start is called before the first frame update
    void Start()
    {
        routeToGo = 0;
        tParam = 0f;
        speedModifier = 0.6f;
        coroutineAllowed = true;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (coroutineAllowed)
        {
            StartCoroutine(GoByTheRoute(routeToGo));
        }
    }

    public IEnumerator GoByTheRoute(int routeNum)
    {
        coroutineAllowed = false;
        runing = true;

        Vector3 p0 = routes[routeNum].GetChild(0).position;
        Vector3 p1 = routes[routeNum].GetChild(1).position;
        Vector3 p2 = routes[routeNum].GetChild(2).position;
        Vector3 p3 = routes[routeNum].GetChild(3).position;

        while (tParam < 1)
        {
            tParam += Time.deltaTime * speedModifier;

            objectPosition = Mathf.Pow(1 - tParam, 3) * p0 + 3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 + 3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 + Mathf.Pow(tParam, 3) * p3;
            
            transform.position = objectPosition;
            //transform.position = Vector3.Lerp(transform.position,objectPosition.f);
            //transform.DOMove(objectPosition, 0.0001f);
            //yield return new WaitForEndOfFrame();
            yield return null;
        }

        tParam = 0f;

        routeToGo += 1;

        if (routeToGo > routes.Length - 1)
        {
            routeToGo = 0;
        }

        coroutineAllowed = true;
        runing = false;


    }
    public void StopMovemant()
    {
        StopAllCoroutines();
    }
}
