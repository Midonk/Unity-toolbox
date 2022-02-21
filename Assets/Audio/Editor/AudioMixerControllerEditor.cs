using UnityEngine;
using UnityEditor;
using UnityEngine.Audio;
using System;
using TF.Utils;

[CustomEditor(typeof(AudioMixerController), true)]
public class AudioMixerControllerEditor : Editor 
{
    private void OnEnable() 
    {
        _mixer = serializedObject.FindProperty("_mixer");
        _generalVolume = serializedObject.FindProperty("_generalVolume");
        _musicVolume = serializedObject.FindProperty("_musicVolume");
        _ambianceVolume = serializedObject.FindProperty("_ambianceVolume");
        _sfxVolume = serializedObject.FindProperty("_sfxVolume");
        _voiceVolume = serializedObject.FindProperty("_voiceVolume");
        if(!_mixer.objectReferenceValue) return;

        var mixer = (AudioMixer)_mixer.objectReferenceValue;
        _exposedParameters = AudioUtils.GetExposedParameters(mixer);
        _generalIndex = Mathf.Max(0, Array.IndexOf(_exposedParameters, _generalVolume.stringValue));
        _musicIndex = Mathf.Max(0, Array.IndexOf(_exposedParameters, _musicVolume.stringValue));
        _ambianceIndex = Mathf.Max(0, Array.IndexOf(_exposedParameters, _ambianceVolume.stringValue));
        _sfxIndex = Mathf.Max(0, Array.IndexOf(_exposedParameters, _sfxVolume.stringValue));
        _voiceIndex = Mathf.Max(0, Array.IndexOf(_exposedParameters, _voiceVolume.stringValue));
    }

    public override void OnInspectorGUI() 
    {
        serializedObject.Update();

        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(_mixer);
        var serailizedMixer = _mixer.objectReferenceValue;
        if(EditorGUI.EndChangeCheck() && serailizedMixer)
        {
            var mixer = (AudioMixer)serailizedMixer;
            _exposedParameters = AudioUtils.GetExposedParameters(mixer);
        }

        if (serailizedMixer)
        {
            _generalIndex = EditorGUILayout.Popup(_generalVolume.displayName, _generalIndex, _exposedParameters);
            _musicIndex = EditorGUILayout.Popup(_musicVolume.displayName, _musicIndex, _exposedParameters);
            _ambianceIndex = EditorGUILayout.Popup(_ambianceVolume.displayName, _ambianceIndex, _exposedParameters);
            _sfxIndex = EditorGUILayout.Popup(_sfxVolume.displayName, _sfxIndex, _exposedParameters);
            _voiceIndex = EditorGUILayout.Popup(_voiceVolume.displayName, _voiceIndex, _exposedParameters);

            _generalVolume.stringValue = _exposedParameters[_generalIndex];
            _musicVolume.stringValue = _exposedParameters[_musicIndex];
            _ambianceVolume.stringValue = _exposedParameters[_ambianceIndex];
            _sfxVolume.stringValue = _exposedParameters[_sfxIndex];
            _voiceVolume.stringValue = _exposedParameters[_voiceIndex];
        }

        serializedObject.ApplyModifiedProperties();
    }

    private SerializedProperty _mixer;
    private SerializedProperty _generalVolume;
    private SerializedProperty _musicVolume;
    private SerializedProperty _ambianceVolume;
    private SerializedProperty _sfxVolume;
    private SerializedProperty _voiceVolume;
    private string[] _exposedParameters;
    private int _generalIndex;
    private int _musicIndex;
    private int _ambianceIndex;
    private int _sfxIndex;
    private int _voiceIndex;
}