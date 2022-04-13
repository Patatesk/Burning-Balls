using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class BounceAndMove : MonoBehaviour
{
    Transform instantiateTransform;
    Transform trans1;
    Transform trans2;

    Rigidbody rb;
    AudioSource audioSource;

    [SerializeField] GameManager manager;
    [SerializeField] GameObject ball;
    [SerializeField] Referances referances;
    [SerializeField] Camera ortho;
    [SerializeField] GameObject safe;


    int layer_mask;

    public int waitAll;
    public int number;
    int fireAll;

    Vector3 mousePos;
    Vector3 firstPos;
    Vector3 mouseDif;
    RaycastHit ates;

    bool instantiateLittle;
    bool dontRepeat;
    bool turn;

    GameObject x;

    void Awake()
    {

        layer_mask = LayerMask.GetMask("FinishLayer");
        trans1 = transform.GetChild(3).transform;
        trans2 = transform.GetChild(2).transform;
        referances.playerBallCount.gameObject.SetActive(false);
        DOTween.SetTweensCapacity(5000, 5000);
        GetComponent<Follo>().enabled = false;
        manager.isPlaying = false;
        dontRepeat = false;
        referances = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Referances>();
        manager.childCount = 0;
        rb = GetComponent<Rigidbody>();
        fireAll = manager.ballCountForFire;
        referances.manager.isWin = false;
        referances.manager.isLose = false;
        referances.manager.winPanel = false;
        referances.manager.stop = false;
        audioSource = gameObject.AddComponent<AudioSource>();
        manager.safe = false;

        if (GameObject.FindGameObjectWithTag("Ground").transform.GetChild(1).gameObject.tag == "Safe")
        {
            GameObject x;
            x = Instantiate(GameObject.FindGameObjectWithTag("Ground").transform.GetChild(1).gameObject, transform.position, Quaternion.identity);
            x.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            if (manager.level == 0)
            {
                //x.GetComponent<BoxCollider>().enabled = true;
            }
        }

        manager.stopBool = true;

    }

    private void Update()
    {
        if (manager.stopBool)
        {
            if (Input.GetMouseButtonDown(0))
            {
                manager.isPlaying = true;
            }
        }
       

        if (manager.isPlaying)
        {

            referances.playerBallCount.gameObject.SetActive(true);

            Move();

            firstPos = Vector3.Lerp(firstPos, mousePos, 0.1f);

            if (GetComponent<Follo>())
            {
                GetComponent<Follo>().enabled = true;
            }

            if (referances.manager.stopBool)
            {
                if (Input.GetMouseButtonDown(0))
                    MouseDown(Input.mousePosition);

                else if (Input.GetMouseButtonUp(0))
                    MouseUp();

                else if (Input.GetMouseButton(0))
                    MouseHold(Input.mousePosition);
            }


        }

        referances.playerBallCount.text = referances.littleOnes.Count.ToString();

        var ray = new Ray(transform.position, transform.TransformDirection(Vector3.down));
        var ray2 = new Ray(transform.position, trans1.transform.eulerAngles);
        var ray3 = new Ray(transform.position, trans2.transform.eulerAngles);

        RaycastHit hit;
        RaycastHit hit2;
        RaycastHit hit3;

        if (!Physics.Raycast(ray, out hit, 5, layer_mask) && !Physics.Raycast(ray2, out hit2, 5, layer_mask) && !Physics.Raycast(ray3, out hit3, 5, layer_mask))
        {
            if (transform.position.y < 0.55f && !manager.safe)
            {
                Debug.Log("YERdeðil");

                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * ates.distance, Color.green);
                GetComponent<Follo>().StopMovemant();
                GetComponent<Follo>().coroutineAllowed = false;
                GetComponent<Follo>().runing = false;

                if (transform.GetChild(0).gameObject.tag == "Little")
                {
                    Destroy(transform.GetChild(0).gameObject);
                }
            }
        }

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Debug.Log(hit.collider.tag);
        }

        if (referances.littleOnes.Count == 0)
        {
            manager.isLose = true;
            manager.isWin = false;
            manager.winPanel = false;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Safe")
        {
            manager.safe = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Safe")
        {
            manager.safe = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "x")
        {
            Vibrator.Vibrate(150);
            instantiateTransform = other.gameObject.transform;
            other.gameObject.transform.parent.GetComponent<PlatformLine>().STopMovemant();

            if (!dontRepeat)
            {
                StartCoroutine(InstanttiateDelay(other.gameObject.GetComponent<Multiplayer>().Math(), other.gameObject.GetComponent<Multiplayer>(), other.gameObject));
                dontRepeat = true;
                Invoke("Repeat", 0.2f);
            }

            if (manager.childCount >= manager.ballCountForFire)
            {
                other.gameObject.GetComponent<Multiplayer>().Shake();
            }

            
            //if (referances.littleOnes.Count >= 24)
            //{
            //    foreach (var item in referances.littleOnes)
            //    {
            //        item.transform.GetChild(1).transform.DOPunchScale(new Vector3(2.05f, 2.05f, 2.05f), 0.3f, 1);
            //    }
            //}

            //if (manager.childCount < manager.ballCountForFire)
            //{
            //    //other.gameObject.GetComponent<BoxCollider>().enabled = false;
            //}
        }

        if (other.gameObject.tag == "Ground")
        {
            audioSource.PlayOneShot(referances.bounce, manager.volume);

            Vibrator.Vibrate(150);

            if (!GetComponent<Follo>().runing)
            {
                GetComponent<Follo>().coroutineAllowed = true;
            }

            Instantiate(referances.jumpParticle, new Vector3(0, 0.5f, transform.position.z), Quaternion.Euler(90, 0, 0));

            other.gameObject.transform.GetChild(0).DOPunchScale(new Vector3(-2, -2, -2), 0.1f, 1);
        }

        if (other.gameObject.tag == "Ground2")
        {
            //audioSource.PlayOneShot(referances.bounce, manager.volume);
            if (!GetComponent<Follo>().runing)
            {
                GetComponent<Follo>().coroutineAllowed = true;
            }

            Instantiate(referances.jumpParticle, new Vector3(0, 0.5f, transform.position.z), Quaternion.Euler(90, 0, 0));

            other.gameObject.transform.GetChild(0).DOPunchScale(new Vector3(-2, -2, -2), 0.1f, 1);
            other.gameObject.transform.parent.GetComponent<PlatformLine>().STopMovemant();
        }

        if (other.gameObject.tag == "Finish")
        {
            manager.isPlaying = false;

            if (GetComponent<Follo>())
            {
                Destroy(referances.player.GetComponent<Follo>());
            }

            turn = true;

            //gameObject.GetComponent<Follo>().enabled = false;
        }

        if (other.gameObject.tag == "Safe")
        {
            manager.safe = true;
        }

        if (other.gameObject.tag == "Stop")
        {
            manager.stopBool = false;
            //other.gameObject.transform.parent.GetComponent<PlatformLine>().STopMovemant();

        }
        if (other.gameObject.tag == "Groundx")
        {

            manager.stopBool = false;

            audioSource.PlayOneShot(referances.bounce, manager.volume);

            Vibrator.Vibrate(150);

            if (!GetComponent<Follo>().runing)
            {
                GetComponent<Follo>().coroutineAllowed = true;
            }

            Instantiate(referances.jumpParticle, new Vector3(0, 0.5f, transform.position.z), Quaternion.Euler(90, 0, 0));

            other.gameObject.transform.GetChild(0).DOPunchScale(new Vector3(-2, -2, -2), 0.1f, 1);
        }

    }


    void Move()
    {

        if (manager.isPlaying)
        {
            Quaternion flipPlatform = Quaternion.Euler(0, 0, mouseDif.x);
            referances.level.transform.Rotate(new Vector3(0, 0, 1), mouseDif.x);
        }
    }

    private void MouseDown(Vector3 inputPos)
    {
        mousePos = ortho.ScreenToWorldPoint(inputPos);
        firstPos = mousePos;
    }

    private void MouseHold(Vector3 inputPos)
    {
        mousePos = ortho.ScreenToWorldPoint(inputPos);
        mouseDif = mousePos - firstPos;
        mouseDif *= manager.sensitivity;
    }

    private void MouseUp()
    {
        mouseDif = Vector3.zero;
    }

    IEnumerator InstanttiateDelay(int a, Multiplayer b, GameObject y)
    {
        if (a > 0)
        {
            //audioSource.PlayOneShot(referances.net, manager.volume);
            for (int i = 0; i < a; i++)
            {
                GameObject x;
                if (referances.littleOnes.Count >= 0)
                {
                    x = Instantiate(ball, new Vector3(referances.littleOnes.Last().transform.position.x, referances.littleOnes.Last().transform.position.y, referances.littleOnes.Last().transform.position.z - 0.2f), Quaternion.identity);
                    referances.littleOnes.Add(x);

                }

                //else if (referances.littleOnes.Count != 0)
                //{
                //    x = Instantiate(ball, instantiateTransform.position, Quaternion.identity);
                //    referances.littleOnes.Add(x);

                //}

                b.number--;

                if (b.number == 0)
                {
                    BreakDownGlass(y);
                    y.GetComponent<HoopExpolade>().Explode();
                }

                yield return new WaitForSeconds(0.10f);
            }

            InstantiateParticul();
        }

        else if (a < 0)
        {
            GetComponent<Follo>().StopMovemant();

            for (int i = 0; i < -a; i++)
            {
                Destroy(referances.littleOnes[0].gameObject);
                b.number--;
                manager.childCount--;
                yield return new WaitForSeconds(0.1f);
            }

            if (b.number == 0)
            {
                BreakDownGlass(y);
                y.GetComponent<HoopExpolade>().Explode();
                referances.player.GetComponent<Follo>().coroutineAllowed = true;
            }
        }

        else if (a == 0)
        {
            BreakDownGlass(y);
            y.GetComponent<HoopExpolade>().Explode();

        }
    }

    IEnumerator Effect(int a)
    {
        for (int i = 0; i < a; i++)
        {
            referances.littleOnes[i].transform.DOPunchScale(new Vector3(0.5f, 0.5f, 0.5f), 0.1f);
            yield return new WaitForSeconds(0.05f);

        }
    }

    void InstantiateParticul()
    {
        if (referances.littleOnes.Count >= manager.ballCountForFire && referances.littleOnes[1].transform.childCount == 1)
        {
            GameObject x;

            for (int i = 0; i < referances.littleOnes.Count; i++)
            {
                x = Instantiate(referances.torchFire, referances.littleOnes[i].transform.position, referances.littleOnes[i].transform.rotation);
                x.transform.parent = referances.littleOnes[i].transform;
                x.transform.localScale = new Vector3(2f, 2f, 2f);
            }
            //audioSource.PlayOneShot(referances.fire, manager.volume);
        }
    }

    void BreakDownGlass(GameObject glass)
    {
        List<GameObject> Galses = new List<GameObject>();

        for (int i = 1; i < 13; i++)
        {
            Galses.Add(glass.transform.GetChild(3).GetChild(i).gameObject);
        }

        foreach (var item in Galses)
        {
            item.AddComponent<Rigidbody>();
            item.GetComponent<Rigidbody>().AddForce(Random.Range(-4, 4), Random.Range(1, 5), Random.Range(-1, -4), ForceMode.Impulse);
        }

        glass.transform.GetChild(3).GetChild(13).gameObject.SetActive(false);
        //audioSource.PlayOneShot(referances.breaking, manager.volume);
    }

    void Repeat()
    {
        dontRepeat = false;
    }



}
