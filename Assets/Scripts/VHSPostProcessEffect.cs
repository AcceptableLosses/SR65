using UnityEngine;
using UnityEngine.Video;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/GlitchEffect")]
[RequireComponent(typeof(Camera))]
[RequireComponent(typeof(VideoPlayer))]
public class VHSPostProcessEffect : MonoBehaviour
{
    public Shader shader;
    public VideoClip VHSClip;

    private float _yScanline;
    private float _xScanline;
    private Material _material = null;
    private VideoPlayer _player;

    public AudioClip VHSStartup;
    AudioSource audioPlayer;
    bool playerToggle = false; //True for is toggled to play sound, false for not.

    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
        audioPlayer.clip = VHSStartup;
        audioPlayer.Play();
        playerToggle = true;
        _material = new Material(shader);
        _player = GetComponent<VideoPlayer>();
        _player.isLooping = true;
        _player.renderMode = VideoRenderMode.APIOnly;
        _player.audioOutputMode = VideoAudioOutputMode.None;
        _player.clip = VHSClip;
        _player.Play();
    }

    void Update()
    {
        if (playerToggle == false)
        {
            audioPlayer.Play();
            playerToggle = true;
        }
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        
        new WaitForSeconds(.1f);
        _material.SetTexture("_VHSTex", _player.texture);

        _yScanline += Time.deltaTime * 0.01f;
        _xScanline -= Time.deltaTime * 0.1f;

        if (_yScanline >= 1)
        {
            _yScanline = Random.value;
        }
        if (_xScanline <= 0 || Random.value < 0.05)
        {
            _xScanline = Random.value;
        }
        _material.SetFloat("_yScanline", _yScanline);
        _material.SetFloat("_xScanline", _xScanline);
        Graphics.Blit(source, destination, _material);
        
    }

    protected void OnDisable()
    {
        audioPlayer.Pause();
        playerToggle = false;
        if (_material)
        {
            // DestroyImmediate(_material);
        }
    }
}