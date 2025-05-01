using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;  // For AudioMixers
using Cinemachine;  // For Cinemachine Camera Control

public class OptionsMenuManager : MonoBehaviour
{
    [Header("Audio Sliders")]
    public Slider masterVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;

    [Header("Camera Sliders")]
    public Slider cameraSensitivitySliderX;  // Slider for controlling camera sensitivity X
    public Slider cameraSensitivitySliderY;  // Slider for controlling camera sensitivity Y

    [Header("Cinemachine Cameras")]
    public CinemachineFreeLook mainView;
    public CinemachineFreeLook dashView;
    public CinemachineFreeLook meleeView;

    // References to AudioMixers
    public AudioMixer masterMixer;
    public AudioMixer musicMixer;
    public AudioMixer sfxMixer;

    private void Start()
    {
        // Load saved settings (if any)
        masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1f);
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
        sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);
        cameraSensitivitySliderX.value = PlayerPrefs.GetFloat("CameraSensitivity", 1f);
        cameraSensitivitySliderY.value = PlayerPrefs.GetFloat("CameraSensitivity", 1f);

        ApplyVolumeSettings();
        ApplyCameraSensitivity();

        // Add listeners for UI changes
        masterVolumeSlider.onValueChanged.AddListener(delegate { SetMasterVolume(); });
        musicVolumeSlider.onValueChanged.AddListener(delegate { SetMusicVolume(); });
        sfxVolumeSlider.onValueChanged.AddListener(delegate { SetSFXVolume(); });
        cameraSensitivitySliderX.onValueChanged.AddListener(delegate { SetCameraSensitivity(); });
        cameraSensitivitySliderY.onValueChanged.AddListener(delegate { SetCameraSensitivity(); });
    }

    // Master Volume
    public void SetMasterVolume()
    {
        AudioManager.instance.SetMasterVolume(masterVolumeSlider.value);
        PlayerPrefs.SetFloat("MasterVolume", masterVolumeSlider.value);
    }

    // Music Volume
    public void SetMusicVolume()
    {
        AudioManager.instance.SetMusicVolume(musicVolumeSlider.value);
        PlayerPrefs.SetFloat("MusicVolume", musicVolumeSlider.value);
    }

    // SFX Volume
    public void SetSFXVolume()
    {
        AudioManager.instance.SetSFXVolume(sfxVolumeSlider.value);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolumeSlider.value);
    }

    // Camera Sensitivity
    public void SetCameraSensitivity()
    {
        float sensitivityX = cameraSensitivitySliderX.value;
        float sensitivityY = cameraSensitivitySliderY.value;

        // Set the sensitivity on all the camera views (if they are using Cinemachine)
        mainView.m_XAxis.m_MaxSpeed = sensitivityX;
        mainView.m_YAxis.m_MaxSpeed = sensitivityY;

        dashView.m_XAxis.m_MaxSpeed = sensitivityX;
        dashView.m_YAxis.m_MaxSpeed = sensitivityY;

        meleeView.m_XAxis.m_MaxSpeed = sensitivityX;
        meleeView.m_YAxis.m_MaxSpeed = sensitivityY;

        PlayerPrefs.SetFloat("CameraSensitivityX", sensitivityX);
        PlayerPrefs.SetFloat("CameraSensitivityY", sensitivityY);
    }

    private void ApplyVolumeSettings()
    {
        AudioManager.instance.SetMasterVolume(masterVolumeSlider.value);
        AudioManager.instance.SetMusicVolume(musicVolumeSlider.value);
        AudioManager.instance.SetSFXVolume(sfxVolumeSlider.value);
    }

    private void ApplyCameraSensitivity()
    {
        float sensitivityX = PlayerPrefs.GetFloat("CameraSensitivity", 300f);
        float sensitivityY = PlayerPrefs.GetFloat("CameraSensitivityY", 8f);

        mainView.m_XAxis.m_MaxSpeed = sensitivityX;
        mainView.m_YAxis.m_MaxSpeed = sensitivityY;

        dashView.m_XAxis.m_MaxSpeed = sensitivityX;
        dashView.m_YAxis.m_MaxSpeed = sensitivityY;

        meleeView.m_XAxis.m_MaxSpeed = sensitivityX;
        meleeView.m_YAxis.m_MaxSpeed = sensitivityY;
    }

    private void Update()
    {
        //ApplyCameraSensitivity();
        //SetCameraSensitivity();
    }
}