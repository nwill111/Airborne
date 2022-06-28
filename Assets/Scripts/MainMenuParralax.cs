
using UnityEngine;
using UnityEngine.UI;

//Code for parralax effect on the main menu.
public class MainMenuParralax : MonoBehaviour
{
    
    private Vector2 startPosition;
    public int speed;

    //Set the start position of the camera
    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        //Get mouse position
        Vector2 mousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);

        //Use LERP to move camera in respect to mouse
        float xpos = Mathf.Lerp(transform.position.x, startPosition.x + (mousePosition.x * speed), 1.5f * Time.deltaTime);
        float ypos = Mathf.Lerp(transform.position.y, startPosition.y + (mousePosition.y * speed), 1.5f * Time.deltaTime);

        //Set new location
        transform.position = new Vector3(xpos, ypos, 0);
    }
}
