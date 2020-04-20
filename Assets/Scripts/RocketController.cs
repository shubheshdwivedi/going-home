using UnityEngine.SceneManagement;
using UnityEngine;

public class RocketController : MonoBehaviour {
    [SerializeField] private float rcsThrust = 250f;
    [SerializeField] private float mainThrust = 50f;

    [SerializeField] private AudioClip mainEngineAudio;
    [SerializeField] private AudioClip successAudio;
    [SerializeField] private AudioClip deathAudio;
    
    [SerializeField] private ParticleSystem mainEngineParticles;
    [SerializeField] private ParticleSystem successParticles;
    [SerializeField] private ParticleSystem deathParticles;

    private Rigidbody _rigidbody;
    private AudioSource _audioSource;
    private int _scene;

    enum State{
        Alive, Transcending, Dying
        };

    private State _state = State.Alive;
   
    void Start() {
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
        _scene = SceneManager.GetActiveScene().buildIndex;
    }

   
    void Update() {
        PlayerPrefs.SetInt("scene", _scene);
        if (_state == State.Alive) {
            ProcessInput();
        }
    }

    

    private void OnCollisionEnter(Collision other) {
        if(_state != State.Alive)
            return;
        switch (other.gameObject.tag) {
            case "Friendly":
                print("Friendly");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartDeathSequence();
                break;
        }
    }

    private void StartDeathSequence() {
        _state = State.Dying;
        _audioSource.Stop();
        if (!deathParticles.isPlaying)
            deathParticles.Play();
        _audioSource.PlayOneShot(deathAudio);
        Invoke(nameof(LoadCurrentLevel), 1f);
    }

    private void StartSuccessSequence() {
        _state = State.Transcending;
        _audioSource.Stop();
        if (!successParticles.isPlaying)
            successParticles.Play();
        _audioSource.PlayOneShot(successAudio);
        successParticles.Play(); 
        Invoke(nameof(LoadNextScene), 1f);
    }

    private void LoadCurrentLevel() {
        SceneManager.LoadScene(_scene);
    }

    private void LoadNextScene() {
        PlayerPrefs.SetInt("scene", _scene + 1);
        SceneManager.LoadScene(_scene + 1);
    }

    private void ProcessInput() {
        ThrustInputResponse();
        RotateInputResponse();
    }

    private void RotateInputResponse() {
        float rotationPerFrame = Time.deltaTime * rcsThrust;
        _rigidbody.freezeRotation = true;
        if (Input.GetKey(KeyCode.A))
            transform.Rotate(Vector3.forward * rotationPerFrame);
        else if (Input.GetKey(KeyCode.D))
            transform.Rotate(Vector3.back * rotationPerFrame);
        _rigidbody.freezeRotation = false;
    }

    private void ThrustInputResponse() {
        if (Input.GetKey(KeyCode.S))
            ApplyThrust(-1);
        else if (Input.GetKey(KeyCode.Space))
            ApplyThrust(1);
        else {
            _audioSource.Stop();
            mainEngineParticles.Stop();
        }
    }

    private void ApplyThrust(int direction) {
        if(!mainEngineParticles.isPlaying)
            mainEngineParticles.Play();
        _rigidbody.AddRelativeForce(mainThrust * direction * Vector3.up);
        if (!_audioSource.isPlaying)
            _audioSource.PlayOneShot(mainEngineAudio);
    }
}
