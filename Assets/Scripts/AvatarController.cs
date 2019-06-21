using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarController : MonoBehaviour
{
    public float _showAvatarTimer = 0f;       // switch weather time equal 0

    public GameObject _avatarObject; // Find Avatar1
    public GameObject _avatarObject1; // Find Avatar2
    public GameObject _avatarObject2; // Find Avatar3
    public GameObject _avatarObject3; // Find Avatar4
    public GameObject _doorObject; // Find Door
    public GameObject _doorObjectCopy; // Find Door copy
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
            _doorObject.SetActive(false);
            _doorObjectCopy.SetActive(true);
        }
        //Debug.Log("SwitchWeatherTimer ist aufgerufen");
        _showAvatarTimer  += Time.deltaTime; // decrease the timer by real time
        if (_showAvatarTimer < 0)
        {
            _showAvatarTimer = 0;
        }
        
        if (_showAvatarTimer >= 60)
        {
            _doorObject.SetActive(true);
            _doorObjectCopy.SetActive(false);
            _avatarObject.SetActive(true);
            _avatarObject1.SetActive(true);
            _avatarObject2.SetActive(true);
            _avatarObject3.SetActive(true);

        }

       
        
        
    }
}
