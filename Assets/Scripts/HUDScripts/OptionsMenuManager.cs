using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using Cinemachine;

public class OptionsMenuManager : MonoBehaviour
{
    [Header("Audio Sliders")]
    public Slider masterVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;

    [Header("Camera Sliders")]
    public Slider cameraSensitivitySliderX;
    public Slider cameraSensitivitySliderY;

    [Header("Cinemachine Cameras")]
    public CinemachineFreeLook mainView;
    public CinemachineFreeLook dashView;
    public CinemachineFreeLook meleeView;

    public AudioMixer masterMixer;
    public AudioMixer musicMixer;
    public AudioMixer sfxMixer;

    private void Start()
    {
        LoadSettings();
        ApplyVolumeSettings();
        ApplyCameraSensitivity();

        masterVolumeSlider.onValueChanged.AddListener(_ => SetMasterVolume());
        musicVolumeSlider.onValueChanged.AddListener(_ => SetMusicVolume());
        sfxVolumeSlider.onValueChanged.AddListener(_ => SetSFXVolume());
        cameraSensitivitySliderX.onValueChanged.AddListener(_ => SetCameraSensitivity());
        cameraSensitivitySliderY.onValueChanged.AddListener(_ => SetCameraSensitivity());
    }

    private void LoadSettings()
    {
        masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1f);
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
        sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);
        cameraSensitivitySliderX.value = PlayerPrefs.GetFloat("CameraSensitivityX", 300f);
        cameraSensitivitySliderY.value = PlayerPrefs.GetFloat("CameraSensitivityY", 8f);
    }

    private void ApplyVolumeSettings()
    {
        AudioManager.instance.SetMasterVolume(masterVolumeSlider.value);
        AudioManager.instance.SetMusicVolume(musicVolumeSlider.value);
        AudioManager.instance.SetSFXVolume(sfxVolumeSlider.value);
    }

    private void ApplyCameraSensitivity()
    {
        float sensitivityX = cameraSensitivitySliderX.value;
        float sensitivityY = cameraSensitivitySliderY.value;

        SetCameraSpeed(mainView, sensitivityX, sensitivityY);
        SetCameraSpeed(dashView, sensitivityX, sensitivityY);
        SetCameraSpeed(meleeView, sensitivityX, sensitivityY);
    }

    private void SetCameraSpeed(CinemachineFreeLook cam, float x, float y)
    {
        if (cam == null) return;
        cam.m_XAxis.m_MaxSpeed = x;
        cam.m_YAxis.m_MaxSpeed = y;
    }

    public void SetMasterVolume()
    {
        AudioManager.instance.SetMasterVolume(masterVolumeSlider.value);
    }

    public void SetMusicVolume()
    {
        AudioManager.instance.SetMusicVolume(musicVolumeSlider.value);
    }

    public void SetSFXVolume()
    {
        AudioManager.instance.SetSFXVolume(sfxVolumeSlider.value);
    }

    public void SetCameraSensitivity()
    {
        ApplyCameraSensitivity();
    }

    private void SaveAllSettings()
    {
        PlayerPrefs.SetFloat("MasterVolume", masterVolumeSlider.value);
        PlayerPrefs.SetFloat("MusicVolume", musicVolumeSlider.value);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolumeSlider.value);
        PlayerPrefs.SetFloat("CameraSensitivityX", cameraSensitivitySliderX.value);
        PlayerPrefs.SetFloat("CameraSensitivityY", cameraSensitivitySliderY.value);
        PlayerPrefs.Save();
    }

    private void OnDisable()
    {
        SaveAllSettings();
    }

    private void OnApplicationQuit()
    {
        SaveAllSettings();
    }
}
