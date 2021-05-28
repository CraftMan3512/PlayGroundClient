using UnityEngine;

public class BasicPlayerControls : IPlayerControls
{
    GameObject head;
    public float speed = 3f;
    private Rigidbody rb;
    
   public BasicPlayerControls(GameObject _head, Rigidbody rb)
    {
        head = _head;
        this.rb = rb;

    }

    public void MovePlayer(GameObject player)
    {

        
        
        int down = Input.GetKey(KeyCode.S) ? 1 : 0;
        int up = Input.GetKey(KeyCode.W) ? 1 : 0;
        int left = Input.GetKey(KeyCode.A) ? 1 : 0;
        int right = Input.GetKey(KeyCode.D) ? 1 : 0;
        int sprint = Input.GetKey(KeyCode.LeftShift) ? 10 : 1;
        int flyUp = Input.GetKey(KeyCode.Space) ? 1 : 0;
        int flyDown = Input.GetKey(KeyCode.LeftControl) ? 1 : 0;
        Vector3 moveDelta = Quaternion.AngleAxis(head.transform.eulerAngles.y,Vector3.up)*(new Vector3((right - left), 0, (up - down)) * Time.deltaTime * speed*sprint);
        Vector3 jumpDelta = new Vector3(0, flyUp - flyDown, 0) * Time.deltaTime * speed * sprint;
        rb.MovePosition(rb.position+jumpDelta + moveDelta);
    }
}