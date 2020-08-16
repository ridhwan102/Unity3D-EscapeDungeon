using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class pickUpSystem : MonoBehaviour
{
    public GameObject textKunci,textFinish,pickedUp,textHasKey;
    private bool hasKey = false;
    public Image fadingImage;
    public float alpha = 0f;
    public AudioClip jumpScareClip;
    AudioSource jumpScareSource;
    private enemy enemyScript;
    Transform Enemy;
    public TouchPanel touch;

    private void Start()
    {
        Enemy = GameObject.FindGameObjectWithTag("Enemy").transform;
        enemyScript = Enemy.GetComponent<enemy>();
        pickedUp.SetActive(false);
        jumpScareSource = GetComponent<AudioSource>();
        fadingImage.color = new Color(fadingImage.color.r, fadingImage.color.g, fadingImage.color.b, 0f);
    }
    void Update()
    {
        fadingImage.color = new Color(fadingImage.color.r, fadingImage.color.g, fadingImage.color.b, alpha);
        RaycastHit hit;
        var fwd = transform.TransformDirection(Vector3.forward);
        if (Physics.Raycast(transform.position, fwd, out hit, 5f))
        {
            if (hit.collider.gameObject.tag == "Kunci2")
            {
                textKunci.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E) || touch.Pressed)
                {
                    GameObject.FindGameObjectWithTag("Kunci2").SetActive(false);
                    hasKey = true;
                    Debug.Log("Pick Up Key");
                    pickedUp.SetActive(true);
                }

            }
            else
                textKunci.SetActive(false);

            if (hit.collider.gameObject.tag == "Finish")
            {
                textFinish.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E) || touch.Pressed)
                {
                    if (hasKey == true)
                    {
                        Debug.Log("Finish");
                        SceneManager.LoadScene("SampleScene");
                    }
                    else
                        textHasKey.SetActive(true);
                    Debug.Log("Belum punya Kunci");
                }
            }
            else
            {
                textFinish.SetActive(false);
                textHasKey.SetActive(false);
            }
        }

        if (Physics.Raycast(transform.position, fwd, out hit, 50f))
        {
            if (hit.collider.gameObject.tag == "EnemyCol")
            {
                jumpScareSource.PlayOneShot(jumpScareClip, 0.5f);
                alpha += Time.deltaTime;
            }
            else if (alpha > -0.01f || enemyScript.death==true)
                alpha -= Time.deltaTime/2;
        }
    }
    void OnDrawGizmosSelected()
    {
        // Draws a 5 unit long red line in front of the object
        Gizmos.color = Color.red;
        Vector3 direction = transform.TransformDirection(Vector3.forward) * 5;
        Gizmos.DrawRay(transform.position, direction);
    }
}
