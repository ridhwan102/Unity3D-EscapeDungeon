using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class player : MonoBehaviour
{
    /*public Rigidbody projectile; // objek referensi peluru
    public Transform shotPos; // objek referensi tempat meluncur peluru
    public float shotForce = 1000f; // kekuatan tembakan peluru*/
    public float moveSpeed = 5f;
    private Rigidbody rg;
    public FloatingJoystick joystick;
    
    void Start()
    {
        rg = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //transform.Translate(moveSpeed / 10 * Input.GetAxis("Horizontal"), 0f, moveSpeed / 10 * Input.GetAxis("Vertical"));
        transform.Translate(moveSpeed / 10 * joystick.Horizontal, 0f, moveSpeed / 10 * joystick.Vertical);
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.LeftControl))
        {
            moveSpeed += 0.5f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.LeftControl))
        {
            moveSpeed -= 0.5f;
        }

        
        /*deteksi tombol menembak
        if (Input.GetButton("Fire1"))
        {
            // instansiasi peluru sesuai posisi spawn
            Rigidbody shot = Instantiate(projectile, shotPos.position,
            shotPos.rotation) as Rigidbody;
            // force yang diberikan ke peluru
            shot.AddForce(shotPos.forward * shotForce);
        }*/
    }
    

}
