using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FirstBall : MonoBehaviour
{
    Referances referances;
    bool turn = false;
    AudioSource audioSource;
    bool forceadd = false;
    int how;
    float powerUp;
    GameObject x;
    private void Start()
    {
        referances = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Referances>();
        audioSource = gameObject.AddComponent<AudioSource>();
    }
    private void Update()
    {
        if (referances.manager.childCount < 50)
        {
            powerUp = 1;
        }

        else if (referances.manager.childCount >= 50)
        {
            powerUp = 1.40f;
        }

        else if (referances.manager.childCount >= 70)
        {
            powerUp = 1.70f;

        }

        else if (referances.manager.childCount >= 100)
        {
            powerUp = 1.75f;
        }

        if (transform.position.y <= 0.4f && referances.manager.isPlaying && !referances.manager.safe && gameObject.tag != "BigBall")
        {
            Destroy(gameObject);
        }

        if (turn)
        {
            transform.Rotate(new Vector3(1, 0, 0), Space.World);
        }

        if (turn && !referances.manager.isPlaying && referances.playerBallCount.text == "1" && how > 0)
        {
            gameObject.AddComponent<Rigidbody>();
            gameObject.GetComponent<Rigidbody>().useGravity = true;


            if (referances.manager.childCount <= 6 && referances.manager.level != 0)
            {
                gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, 18), ForceMode.Impulse);
            }

            if (referances.manager.childCount > 6 && referances.manager.level != 0)
            {
                gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, 2 * referances.manager.childCount * powerUp), ForceMode.Impulse);
            }

            if (referances.manager.level == 0)
            {
                gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, 120), ForceMode.Impulse);
            }

            turn = false;
            gameObject.GetComponent<SphereCollider>().isTrigger = false;
            referances.vcam.Follow = gameObject.transform;
            forceadd = true;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (this.gameObject.tag == "BigBall")
        {
            if (other.gameObject.tag == "Little")
            {
                audioSource.PlayOneShot(referances.finishv, referances.manager.volume);

                Destroy(other.gameObject);

                transform.DOScale(transform.localScale + new Vector3(0.05f, 0.05f, 0.05f), 0.1f);

                if (referances.manager.ballCountForFire < referances.littleOnes.Count)
                {

                    transform.GetChild(1).gameObject.transform.DOScale(transform.GetChild(1).localScale + new Vector3(0.05f, 0.05f, 0.05f), 0.1f);
                }
                how++;
                turn = true;
            }
        }

        if (other.gameObject.tag == "Finish")
        {
            turn = true;
        }
    }




    private void OnTriggerStay(Collider other)
    {

        if (this.gameObject.tag == "BigBall")
        {
            if (other.gameObject.tag == "Little")
            {
                //audioSource.PlayOneShot(referances.finishv, referances.manager.volume);

                Destroy(other.gameObject);

                transform.DOScale(transform.localScale + new Vector3(0.05f, 0.05f, 0.05f), 0.1f);

                how++;
                turn = true;
            }
        }

        if (other.gameObject.tag == "1X" && gameObject.GetComponent<Rigidbody>().velocity.z <= 0.3f)
        {
            if (x == null)
            {
                x = Instantiate(referances.confetti, new Vector3(transform.position.x, transform.position.y + 3, transform.position.z - 20), Quaternion.identity);
                x.transform.localScale = new Vector3(4, 4, 4);
            }
            referances.manager.isWin = true;
            referances.manager.winPanel = true;
            referances.manager.isLose = false;
            referances.manager.Multiplayer = 1;
        }

        if (other.gameObject.tag == "2X" && gameObject.GetComponent<Rigidbody>().velocity.z <= 0.3f)
        {
            if (x == null)
            {
                x = Instantiate(referances.confetti, new Vector3(transform.position.x, transform.position.y + 3, transform.position.z - 20), Quaternion.identity);
                x.transform.localScale = new Vector3(4, 4, 4);
            }
            referances.manager.isWin = true;
            referances.manager.isLose = false;
            referances.manager.Multiplayer = 3;
            referances.manager.winPanel = true;
        }

        if (other.gameObject.tag == "3X" && gameObject.GetComponent<Rigidbody>().velocity.z <= 0.3f)
        {
            if (x == null)
            {
                x = Instantiate(referances.confetti, new Vector3(transform.position.x, transform.position.y + 3, transform.position.z - 20), Quaternion.identity);
                x.transform.localScale = new Vector3(4, 4, 4);
            }
            referances.manager.isWin = true;
            referances.manager.isLose = false;
            referances.manager.Multiplayer = 5;
            referances.manager.winPanel = true;
        }

        if (other.gameObject.tag == "4X" && gameObject.GetComponent<Rigidbody>().velocity.z <= 0.3f)
        {
            if (x == null)
            {
                x = Instantiate(referances.confetti, new Vector3(transform.position.x, transform.position.y + 3, transform.position.z - 20), Quaternion.identity);
                x.transform.localScale = new Vector3(4, 4, 4);
            }
            referances.manager.isWin = true;
            referances.manager.isLose = false;
            referances.manager.Multiplayer = 7;
            referances.manager.winPanel = true;
        }

        if (other.gameObject.tag == "5X" && gameObject.GetComponent<Rigidbody>().velocity.z <= 0.3f)
        {
            if (x == null)
            {
                x = Instantiate(referances.confetti, new Vector3(transform.position.x, transform.position.y + 3, transform.position.z - 20), Quaternion.identity);
                x.transform.localScale = new Vector3(4, 4, 4);
            }
            referances.manager.isWin = true;
            referances.manager.isLose = false;
            referances.manager.Multiplayer = 9;
            referances.manager.winPanel = true;
        }

        if (other.gameObject.tag == "6X" && gameObject.GetComponent<Rigidbody>().velocity.z <= 0.3f)
        {
            if (x == null)
            {
                x = Instantiate(referances.confetti, new Vector3(transform.position.x, transform.position.y + 3, transform.position.z - 20), Quaternion.identity);
                x.transform.localScale = new Vector3(4, 4, 4);
            }
            referances.manager.isWin = true;
            referances.manager.isLose = false;
            referances.manager.Multiplayer = 11;
            referances.manager.winPanel = true;
        }

        if (other.gameObject.tag == "7X" && gameObject.GetComponent<Rigidbody>().velocity.z <= 0.3f)
        {
            if (x == null)
            {
                x = Instantiate(referances.confetti, new Vector3(transform.position.x, transform.position.y + 3, transform.position.z - 20), Quaternion.identity);
                x.transform.localScale = new Vector3(4, 4, 4);
            }
            referances.manager.isWin = true;
            referances.manager.isLose = false;
            referances.manager.Multiplayer = 13;
            referances.manager.winPanel = true;
        }

        if (other.gameObject.tag == "8X" && gameObject.GetComponent<Rigidbody>().velocity.z <= 0.3f)
        {
            if (x == null)
            {
                x = Instantiate(referances.confetti, new Vector3(transform.position.x, transform.position.y + 3, transform.position.z - 20), Quaternion.identity);
                x.transform.localScale = new Vector3(4, 4, 4);
            }
            referances.manager.isWin = true;
            referances.manager.isLose = false;
            referances.manager.Multiplayer = 15;
            referances.manager.winPanel = true;
        }

        if (other.gameObject.tag == "9X" && gameObject.GetComponent<Rigidbody>().velocity.z <= 0.3f)
        {
            if (x == null)
            {
                x = Instantiate(referances.confetti, new Vector3(transform.position.x, transform.position.y + 3, transform.position.z - 20), Quaternion.identity);
                x.transform.localScale = new Vector3(4, 4, 4);
            }
            referances.manager.isWin = true;
            referances.manager.isLose = false;
            referances.manager.Multiplayer = 17;
            referances.manager.winPanel = true;
        }

        if (other.gameObject.tag == "10X" && gameObject.GetComponent<Rigidbody>().velocity.z <= 0.3f)
        {
            if (x == null)
            {
                x = Instantiate(referances.confetti, new Vector3(transform.position.x, transform.position.y + 3, transform.position.z - 20), Quaternion.identity);
                x.transform.localScale = new Vector3(4, 4, 4);
            }
            referances.manager.isWin = true;
            referances.manager.isLose = false;
            referances.manager.Multiplayer = 19;
            referances.manager.winPanel = true;
        }
    }


    private void OnDestroy()
    {
        referances.littleOnes.Remove(gameObject);
    }
}
