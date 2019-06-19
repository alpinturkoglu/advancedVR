using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarController : MonoBehaviour
{
    public float _showAvatarTimer = 0f;       // switch weather time equal 0

    public GameObject _avatarObject; // Find PlayerGameObject
    public GameObject _avatarObject1; // Find PlayerGameObject
    public GameObject _avatarObject2; // Find PlayerGameObject
    public GameObject _avatarObject3; // Find PlayerGameObject
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        showAvatar();
        

    }
    void showAvatar() {
       
        if (_showAvatarTimer < 60)
        {

            _avatarObject.SetActive(false);
            _avatarObject1.SetActive(false);
            _avatarObject2.SetActive(false);
            _avatarObject3.SetActive(false);
        }
        //Debug.Log("SwitchWeatherTimer ist aufgerufen");
        _showAvatarTimer  += Time.deltaTime; // decrease the timer by real time
        if (_showAvatarTimer < 0)
        {
            _showAvatarTimer = 0;
        }
        
        if (_showAvatarTimer >= 60)
        {

            _avatarObject.SetActive(true);
            _avatarObject1.SetActive(true);
            _avatarObject2.SetActive(true);
            _avatarObject3.SetActive(true);

        }

       
        
        
    }
}
