using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]

public class Weather : MonoBehaviour
{
	private Transform _player;
	private Transform _weather;                               // weather gameobject transform
	public float _weatherHeight;                        // Defines height from ground of the game object

    public float _skyboxBlendValue;                    //Value that defines our skybox blend
    public float _skyboxBlendTime=0.25f;                    //Rate at which  skybox blend
    private bool _sunnyState;                       // Defines the sunnyState is actived
    private bool _rainState;                       // Defines the rainState is actived
    private bool _overcastState;                       // Defines the overcastState is actived
    private bool _mistState;                       // Defines the mistState is actived
    private bool _snowState;                       // Defines the snowState is actived



    public ParticleSystem _sunCloudsParticalSystem;  // create slot in inspector to assign our cloud system
	public ParticleSystem _thunderStormParticalSystem;  // create slot in inspector to assign our thunder system
	public ParticleSystem _mistParticalSystem;  // create slot in inspector to assign our mist system
	public ParticleSystem _overcastParticalSystem;  // create slot in inspector to assign our overcast system
	public ParticleSystem _snowParticalSystem;  // create slot in inspector to assign our snow system
	
	private ParticleSystem.EmissionModule _sunClouds;
	private ParticleSystem.EmissionModule _thunder;
	private ParticleSystem.EmissionModule _mist;
	private ParticleSystem.EmissionModule _overcast;
	private ParticleSystem.EmissionModule _snow;

  

    public float _switchWeatherTimer = 0f;       // switch weather time equal 0
	public float _resetWeatherTimer = 60f;       // reset weather time equal 60s=1 min
	public WeatherStates _weatherState; //Define the naming convention of weathers
	private int _switchWeather;        // Define the switch range of the weathers
	
	public float _audioFadeTime = 0.25f;
	public AudioClip _sunnyAudio;
	public AudioClip _thunderStormAudio;
	public AudioClip _mistAudio;
	public AudioClip _overcastAudio;
	public AudioClip _snowAudio;
	
	public float _lightDimTime = 0.1f;
	public float _minimumIntensity = 0f;   //thunder
	public float _maximumIntensity = 1f;   // sunny
	public float _mistIntensity = 0.5f;
	public float _overcastIntensity = 0.25f;
	public float _snowIntensity = 0.75f;
	
	public Color _sunFog;
	public Color _thunderFog;
	public Color _mistFog;
	public Color _overcastFog;
	public Color _snowFog;
	public float _fogChangeSpeed = 0.1f;

    //Find Avatars
    

    public enum WeatherStates{        // Define all states of the weathers
		PickWeather,
		SunnyWeather,
		ThunderWeather,
		MistWeather,
        OvercastWeather,
		SnowWeather
	}
    // Start is called before the first frame update
    void Start()
    {
        
        GameObject _playerGameObject = GameObject.FindGameObjectWithTag("Player"); // Find PlayerGameObject
		_player = _playerGameObject.transform;       
        
		GameObject _weatherGameObject = GameObject.FindGameObjectWithTag("Weather"); // Find weather GameObject
		_weather= _weatherGameObject.transform;

      //_weatherState = WeatherStates.SunnyWeather;
        // Caches players position
        RenderSettings.fog = true ;   //enable the fog in rendersettings
		RenderSettings.fogMode = FogMode.ExponentialSquared;
		RenderSettings.fogDensity = 0.01f;
		
	
        StartCoroutine("WeatherFSM");// start final state machine
    }

    // Update is called once per frame
    void Update()
    {

        SwitchWeatherTimer();//count down for the weather changing

        _weather.transform.position = new Vector3(_player.position.x,
		_player.position.y+_weatherHeight,
		_player.position.z);

        Debug.Log("Player" + new Vector3(_player.position.x,
        _player.position.y ,
        _player.position.z));
        Debug.Log("Height" + new Vector3(_player.position.x,
        _weatherHeight,
        _player.position.z));
        Debug.Log("Weather"+ new Vector3(_player.position.x,
        _player.position.y + _weatherHeight,
        _player.position.z));
    }
	
	void SwitchWeatherTimer()
	{
		 //Debug.Log("SwitchWeatherTimer ist aufgerufen");
		 _switchWeatherTimer -= Time.deltaTime; // decrease the timer by real time
		 if(_switchWeatherTimer<0){
			 _switchWeatherTimer=0;
		 }
		 if(_switchWeatherTimer>0){
			 return;
		 }
		 if(_switchWeatherTimer==0){
            _weatherState = Weather.WeatherStates.SunnyWeather;
            //The weather is set always to be sunny by the line before, if you want to change it to be dynamic then use the line below.
            //_weatherState = Weather.WeatherStates.PickWeather;  // if timer is 0 then switch to pickweather
            
        }
        _switchWeatherTimer = _resetWeatherTimer;// switch timer equals to _resetWeatherTimer


    }	
	
	IEnumerator WeatherFSM(){
		while(true){//while the weather state machine is active
		
			switch(_weatherState){
				case WeatherStates.PickWeather:
				PickWeather();break;
				case WeatherStates.SunnyWeather:
				SunnyWeather();break;
				case WeatherStates.ThunderWeather:
			    ThunderWeather();break;
				case WeatherStates.MistWeather:
				MistWeather();break;
				case WeatherStates.OvercastWeather:
				OvercastWeather();break;
				case WeatherStates.SnowWeather:
				SnowWeather();break;
			}
			yield return null;
		}
	}
	
	void PickWeather(){
		_switchWeather = Random.Range(0,5);            // _switchWeather is equal to a random range between 0 to 5
		
		_sunCloudsParticalSystem.Stop();
	    _thunderStormParticalSystem.Stop();  // create slot in inspector to assign our thunder system
	    _mistParticalSystem.Stop();  // create slot in inspector to assign our mist system
	    _overcastParticalSystem.Stop();  // create slot in inspector to assign our overcast system
	    _snowParticalSystem.Stop();  // create slot in inspector to assign our snow system
        

     _sunnyState=false;                       
     _rainState=false;                       
     _overcastState=false;                       
     _mistState=false;                      
     _snowState=false;                       
		
		switch(_switchWeather){
			case 0:
			_weatherState = Weather.WeatherStates.SunnyWeather;break;
			case 1:
			_weatherState = Weather.WeatherStates.ThunderWeather;break;
			case 2:
			_weatherState = Weather.WeatherStates.MistWeather;break;
			case 3:
			_weatherState = Weather.WeatherStates.OvercastWeather;break;
			case 4:
			_weatherState = Weather.WeatherStates.SnowWeather;break;
		}
	}
	void SunnyWeather(){
		Debug.Log("SunnyWeather");
        _weatherHeight =100f;
        _thunderStormParticalSystem.Stop();  
	    _mistParticalSystem.Stop();  
	    _overcastParticalSystem.Stop(); 
	    _snowParticalSystem.Stop(); 
		_sunCloudsParticalSystem.Play();
        //_sunClouds.enabled =true; // disables sunclouds partical system
        _sunnyState = true;
        _rainState = false;
        _overcastState = false;
        _mistState = false;
        _snowState = false;


        if (GetComponent<Light>().intensity>_maximumIntensity){ //if  the light intensity is larger than max
			GetComponent<Light>().intensity -= Time.deltaTime * _lightDimTime; // then decrease the intensity be realtime * lightDimTime
		}
		if(GetComponent<Light>().intensity<_maximumIntensity){ //if  the light intensity is lower than max
			GetComponent<Light>().intensity += Time.deltaTime * _lightDimTime; // then increase the intensity be realtime * lightDimTime
		}
		
		if(GetComponent<AudioSource>().volume > 0&&GetComponent<AudioSource>().clip !=_sunnyAudio ){
		 GetComponent<AudioSource>().volume -=  Time.deltaTime*_audioFadeTime;
		}
		if(GetComponent<AudioSource>().volume < 0&&GetComponent<AudioSource>().clip ==_sunnyAudio ){
		 GetComponent<AudioSource>().volume +=  Time.deltaTime*_audioFadeTime;
		}
		
		if(GetComponent<AudioSource>().volume == 0){ // if  the audio clip from the previous weather state is 0
			GetComponent<AudioSource>().Stop();// stop the previous audio
			GetComponent<AudioSource>().clip = _sunnyAudio;
			GetComponent<AudioSource>().loop = true ;
			GetComponent<AudioSource>().Play();
		}
		Color _currentFogColor = RenderSettings.fogColor;          // get current Fog color
		//***************************
		RenderSettings.fogColor = Color.Lerp(_currentFogColor,_sunFog,_fogChangeSpeed*Time.deltaTime); //***Important Lerp 渐变色
	}
	void ThunderWeather(){
		Debug.Log("ThunderWeather");
        _weatherHeight = 30f;
        _sunCloudsParticalSystem.Stop();
	    _mistParticalSystem.Stop();  
	    _overcastParticalSystem.Stop();  
	    _snowParticalSystem.Stop();

        _sunnyState = false;
        _rainState = true;
        _overcastState = false;
        _mistState = false;
        _snowState = false;

        _thunderStormParticalSystem.Play();
		//_thunder.enabled = true; // disables thunder partical system
		
		if(GetComponent<Light>().intensity>_minimumIntensity){ //if  the light intensity is larger than max
			GetComponent<Light>().intensity -= Time.deltaTime * _lightDimTime; // then decrease the intensity be realtime * lightDimTime
		}
		if(GetComponent<Light>().intensity<_minimumIntensity){ //if  the light intensity is lower than max
			GetComponent<Light>().intensity += Time.deltaTime * _lightDimTime; // then increase the intensity be realtime * lightDimTime
		}
		
		if(GetComponent<AudioSource>().volume > 0&&GetComponent<AudioSource>().clip !=_thunderStormAudio ){
		 GetComponent<AudioSource>().volume -=  Time.deltaTime*_audioFadeTime;
		}
		if(GetComponent<AudioSource>().volume < 0&&GetComponent<AudioSource>().clip ==_thunderStormAudio ){
		 GetComponent<AudioSource>().volume +=  Time.deltaTime*_audioFadeTime;
		}
		if(GetComponent<AudioSource>().volume == 0){ // if  the audio clip from the previous weather state is 0
			GetComponent<AudioSource>().Stop();// stop the previous audio
			GetComponent<AudioSource>().clip = _thunderStormAudio;
			GetComponent<AudioSource>().loop = true ;
			GetComponent<AudioSource>().Play();
		}
		Color _currentFogColor = RenderSettings.fogColor;          // get current Fog color
		//***************************
		RenderSettings.fogColor = Color.Lerp(_currentFogColor,_thunderFog,_fogChangeSpeed*Time.deltaTime); //***Important Lerp 渐变色
	}
	void MistWeather(){
		Debug.Log("MistWeather");
        //Debug.Log(_mist.enabled);
        _weatherHeight = 5f;

        _sunCloudsParticalSystem.Stop();
	    _thunderStormParticalSystem.Stop();  
	    _overcastParticalSystem.Stop();  
	    _snowParticalSystem.Stop(); 
		_mistParticalSystem.Play();

        _sunnyState = false;
        _rainState = false;
        _overcastState = false;
        _mistState = true;
        _snowState = false;

        //_mist.enabled = true; // disables mist partical system
        //Debug.Log(_mist.enabled);


        if (GetComponent<Light>().intensity>_mistIntensity){ //if  the light intensity is larger than max
			GetComponent<Light>().intensity -= Time.deltaTime * _lightDimTime; // then decrease the intensity be realtime * lightDimTime
		}
		if(GetComponent<Light>().intensity<_mistIntensity){ //if  the light intensity is lower than max
			GetComponent<Light>().intensity += Time.deltaTime * _lightDimTime; // then increase the intensity be realtime * lightDimTime
		}
		
		if(GetComponent<AudioSource>().volume > 0&&GetComponent<AudioSource>().clip !=_mistAudio ){
		 GetComponent<AudioSource>().volume -=  Time.deltaTime*_audioFadeTime;
		}
		if(GetComponent<AudioSource>().volume < 0&&GetComponent<AudioSource>().clip ==_mistAudio ){
		 GetComponent<AudioSource>().volume +=  Time.deltaTime*_audioFadeTime;
		}
		
	if(GetComponent<AudioSource>().volume == 0){ // if  the audio clip from the previous weather state is 0
			GetComponent<AudioSource>().Stop();// stop the previous audio
			GetComponent<AudioSource>().clip = _mistAudio;
			GetComponent<AudioSource>().loop = true ;
			GetComponent<AudioSource>().Play();
		}
		Color _currentFogColor = RenderSettings.fogColor;          // get current Fog color
		//***************************
		RenderSettings.fogColor = Color.Lerp(_currentFogColor,_mistFog,_fogChangeSpeed*Time.deltaTime); //***Important Lerp 渐变色
		
	}
	void OvercastWeather(){
		Debug.Log("OvercastWeather");
        _weatherHeight = 60f;
        _sunCloudsParticalSystem.Stop();
	    _thunderStormParticalSystem.Stop(); 
	    _mistParticalSystem.Stop();
	    _snowParticalSystem.Stop(); 
		_overcastParticalSystem.Play();

        _sunnyState = false;
        _rainState = false;
        _overcastState = true;
        _mistState = false;
        _snowState = false;
        //_overcast.enabled = true;// disables overcast partical system

        if (GetComponent<Light>().intensity>_overcastIntensity){ //if  the light intensity is larger than max
			GetComponent<Light>().intensity -= Time.deltaTime * _lightDimTime; // then decrease the intensity be realtime * lightDimTime
		}
		if(GetComponent<Light>().intensity<_overcastIntensity){ //if  the light intensity is lower than max
			GetComponent<Light>().intensity += Time.deltaTime * _lightDimTime; // then increase the intensity be realtime * lightDimTime
		}
		
		if(GetComponent<AudioSource>().volume > 0&&GetComponent<AudioSource>().clip !=_overcastAudio ){
		 GetComponent<AudioSource>().volume -=  Time.deltaTime*_audioFadeTime;
		}
		if(GetComponent<AudioSource>().volume < 0&&GetComponent<AudioSource>().clip ==_overcastAudio ){
		 GetComponent<AudioSource>().volume +=  Time.deltaTime*_audioFadeTime;
		}
		
		if(GetComponent<AudioSource>().volume == 0){ // if  the audio clip from the previous weather state is 0
			GetComponent<AudioSource>().Stop();// stop the previous audio
			GetComponent<AudioSource>().clip = _overcastAudio;
			GetComponent<AudioSource>().loop = true ;
			GetComponent<AudioSource>().Play();
		}
		Color _currentFogColor = RenderSettings.fogColor;          // get current Fog color
		//***************************
		RenderSettings.fogColor = Color.Lerp(_currentFogColor,_overcastFog,_fogChangeSpeed*Time.deltaTime); //***Important Lerp 渐变色
	}
	void SnowWeather(){
		Debug.Log("SnowWeather");
        _weatherHeight = 20f;
        _sunCloudsParticalSystem.Stop();
	    _thunderStormParticalSystem.Stop(); 
	    _mistParticalSystem.Stop();  
	    _overcastParticalSystem.Stop();  
		_snowParticalSystem.Play();
        //_snow.enabled = true; // disables snow partical system
        _sunnyState = false;
        _rainState = false;
        _overcastState = false;
        _mistState = false;
        _snowState = true;


        if (GetComponent<Light>().intensity>_snowIntensity){ //if  the light intensity is larger than max
			GetComponent<Light>().intensity -= Time.deltaTime * _lightDimTime; // then decrease the intensity be realtime * lightDimTime
		}
		if(GetComponent<Light>().intensity<_snowIntensity){ //if  the light intensity is lower than max
			GetComponent<Light>().intensity += Time.deltaTime * _lightDimTime; // then increase the intensity be realtime * lightDimTime
		}
		
		if(GetComponent<AudioSource>().volume > 0&&GetComponent<AudioSource>().clip !=_snowAudio ){
		 GetComponent<AudioSource>().volume -=  Time.deltaTime*_audioFadeTime;
		}
		if(GetComponent<AudioSource>().volume < 0&&GetComponent<AudioSource>().clip ==_snowAudio ){
		 GetComponent<AudioSource>().volume +=  Time.deltaTime*_audioFadeTime;
		}
		
		if(GetComponent<AudioSource>().volume == 0){ // if  the audio clip from the previous weather state is 0
			GetComponent<AudioSource>().Stop();// stop the previous audio
			GetComponent<AudioSource>().clip = _snowAudio;
			GetComponent<AudioSource>().loop = true ;
			GetComponent<AudioSource>().Play();
		}
		Color _currentFogColor = RenderSettings.fogColor;          // get current Fog color
		//***************************
		RenderSettings.fogColor = Color.Lerp(_currentFogColor,_snowFog,_fogChangeSpeed*Time.deltaTime); //***Important Lerp 渐变色
	}

    void skyboxBlendManager() {
        Debug.Log("skyboxBlender");
        if (_sunnyState == true)
        {
        }
        if (_mistState == true)
        {
        }
        if (_overcastState == true)
        {
        }
        if (_rainState == true)
        {
        }
        if (_snowState == true)
        {
        }
    }
}
