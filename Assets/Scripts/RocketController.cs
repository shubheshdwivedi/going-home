using UnityEngine.SceneManagement;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    [SerializeField] private float rcsThrust = 250f;
    [SerializeField] private float mainThrust = 50f;
    private Rigidbody _rigidbody;
    private AudioSource _audioSource;

    enum State {
        Alive, Transcending, Dying
        };

    private State state = State.Alive;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(state == State.Alive)
            ProcessInput(); 
    }

    private void OnCollisionEnter(Collision other)
    {
        if(state != State.Alive)
            return;
        switch (other.gameObject.tag)
        {
            case "Friendly":
                print("Friendly");
                break;
            case "Finish":
                state = State.Transcending;
                Invoke(nameof(LoadNextScene), 1f);
                break;
            default:
                state = State.Dying;
                Invoke(nameof(LoadFirstLevel), 1f);
                break;
        }
    }

    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(1);
    }

    private void ProcessInput()
    {
        Thrust();
        Rotate();
    }

    private void Rotate()
    {
        float rotationPerFrame = Time.deltaTime * rcsThrust;
        _rigidbody.freezeRotation = true;
        if (Input.GetKey(KeyCode.A))
            transform.Rotate(Vector3.forward * rotationPerFrame);
        else if (Input.GetKey(KeyCode.D))
            transform.Rotate(Vector3.back * rotationPerFrame);
        _rigidbody.freezeRotation = false;
    }

    private void Thrust()
    {
        if ( (Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.LeftShift)))
        {
            _rigidbody.AddRelativeForce(Vector3.up * -mainThrust);
            if (!_audioSource.isPlaying) 
                _audioSource.Play();
        } else if (Input.GetKey(KeyCode.Space))
        {
            _rigidbody.AddRelativeForce(Vector3.up * mainThrust);
            if (!_audioSource.isPlaying) ;
                _audioSource.Play();
        }
        else
        {
            _audioSource.Stop();
        }
    }
}
