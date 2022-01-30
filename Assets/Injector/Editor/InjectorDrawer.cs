using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Injector), true)]
public class InjectorDrawer: PropertyDrawer 
{
    #region Unity API

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var targetObject = property.serializedObject.targetObject;
        _injector = (Injector)fieldInfo.GetValue(targetObject);
        _isTypeFree = _injector.SourceType is null;
        _elementCount = 0;

        DrawHeader(position, property.displayName);
        EditorGUI.BeginProperty(position, label, property);

        DrawSamplingSource(position, property);
        DrawSourceType(position);
        DrawSeparator(position);
        DrawInjectionTarget(position, property);
        DrawInjectionButton(position);

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return (EditorGUIUtility.singleLineHeight + PADDING) * _elementCount + MARGIN * 2;
    }

    #endregion


    #region Main

    private void DrawHeader(Rect position, string text)
    {
        var headerRect = new Rect(position.x, GetHeight(position, _elementCount), position.width, EditorGUIUtility.singleLineHeight);
        EditorGUI.LabelField(headerRect, text, EditorStyles.boldLabel);
    }

    private void DrawSamplingSource(Rect position, SerializedProperty property)
    {
        if(!_isTypeFree)
        {
            _sourceType = _injector.SourceType;
            return;
        }
        
        var lineHeight = EditorGUIUtility.singleLineHeight;
        var samplingSource = property.FindPropertyRelative("_sourceObject");

        FindMembers(samplingSource, ref _currentMembers, ResetSource);
        GeneratePopupOptions(_currentMembers);

        var samplingSourceRect = new Rect(position.x, GetHeight(position, _elementCount), position.width, lineHeight);
        var SampledMemberRect = new Rect(position.x, GetHeight(position, _elementCount), position.width, lineHeight);

        EditorGUI.BeginChangeCheck();
        EditorGUI.PropertyField(samplingSourceRect, samplingSource);
        if(EditorGUI.EndChangeCheck())
        {
            _injector.SelectedSourceIndex = 0;
            _injector.SelectedTargetIndex = 0;
            property.serializedObject.ApplyModifiedProperties();
        }

        EditorGUI.BeginChangeCheck();
        _injector.SelectedSourceIndex = EditorGUI.Popup(SampledMemberRect, "Source Members", _injector.SelectedSourceIndex, _currentPopupOptions);
        GUI.enabled = _injector.SelectedSourceIndex > 0;
        if(!EditorGUI.EndChangeCheck() || _currentMembers is null) return;

        var optionIndex = _injector.SelectedSourceIndex;
        _injector.sourceMember = optionIndex > 0 /* && _currentMembers.Length > 0 */ ? _currentMembers[optionIndex - 1] : null;
        var type = _injector.sourceMember.GetMemberType();
        if(type is null) return;
        if(type.Equals(_sourceType)) return;
        
        _injector.SelectedTargetIndex = 0;
        _sourceType = type;
        property.serializedObject.ApplyModifiedProperties();
    }

    private void DrawInjectionTarget(Rect position, SerializedProperty property)
    {
        var lineHeight = EditorGUIUtility.singleLineHeight;
        var injectionTarget = property.FindPropertyRelative("_targetObject");

        _targetObject = injectionTarget.objectReferenceValue;
        FindMembers(injectionTarget, ref _currentMembers, ResetTarget);
        GeneratePopupOptions(_currentMembers);

        var injectionTargetRect = new Rect(position.x, GetHeight(position, _elementCount), position.width, lineHeight);
        var injectedMemberRect = new Rect(position.x, GetHeight(position, _elementCount), position.width, lineHeight);

        EditorGUI.BeginChangeCheck();
        EditorGUI.PropertyField(injectionTargetRect, injectionTarget);
        if(EditorGUI.EndChangeCheck())
        {
            _injector.SelectedTargetIndex = 0;
            property.serializedObject.ApplyModifiedProperties();
        }

        _injector.SelectedTargetIndex = EditorGUI.Popup(injectedMemberRect, "Target Members", _injector.SelectedTargetIndex, _currentPopupOptions);

        if(_currentMembers is null) return;

        var optionIndex = _injector.SelectedTargetIndex;
        _injector.targetMember = optionIndex > 0 && _currentMembers.Length > 0 ? _currentMembers[optionIndex - 1] : null;
    }

    private void DrawSourceType(Rect position)
    {
        if(_isTypeFree) return;
        
        var typeBoxRect = new Rect(position.x, GetHeight(position, _elementCount), position.width, EditorGUIUtility.singleLineHeight);
        GUI.Box(typeBoxRect, _injector.SourceType.ToString());
    }
    
    private void DrawSeparator(Rect position)
    {
        var separatorRect = new Rect(position.x, GetHeight(position, _elementCount), position.width, EditorGUIUtility.singleLineHeight);
        GUI.Box(separatorRect, '\u21D3'.ToString());
    }

    private void DrawInjectionButton(Rect position)
    {
        var injectButtonRect = new Rect(position.x, GetHeight(position, _elementCount), position.width, EditorGUIUtility.singleLineHeight);
        GUI.enabled = _injector.CanInject;
        if (GUI.Button(injectButtonRect, "Inject"))
        {
            Undo.RecordObject(_targetObject, $"Injection ({_sourceType})");
            _injector.Inject();
        }
    }
         
    #endregion


    #region Plumbery
     
    private void GeneratePopupOptions(MemberInfo[] members)
    {
        var optionCount = members?.Length ?? 0;
        optionCount += 1;
        _currentPopupOptions = new string[optionCount];
        _currentPopupOptions[0] = "None";
        for (int i = 0; i < optionCount - 1; i++)
        {
            var option = members[i].Name;
            _currentPopupOptions[i + 1] = option;
        }
    }
    
    private void FindMembers(SerializedProperty property, ref MemberInfo[] members, Action resetCallback)
    {
        var propertyObject = property.objectReferenceValue;
        if(propertyObject is null)
        {
            resetCallback.Invoke();
            return;
        }

        var propertType = propertyObject.GetType();
        var filter = new MemberFilter(SearchMember);
        members = propertType.FindMembers(MEMBERTYPES, BINDINGFLAGS, filter, null);
    }

    private bool SearchMember(MemberInfo member, System.Object objSearch)
    {
        if(_isTypeFree && _elementCount == 1) return true;

        Type memberType = member.GetMemberType();
        
        if (_isTypeFree && memberType == _sourceType) return true;
        if(_injector.SourceType is null) return false;
        if (memberType == _injector.SourceType || _injector.SourceType.IsAssignableFrom(memberType)) return true;

        return false;
    }

    private void ResetSource()
    {
        _sourceType = null;
        _currentMembers = null;
        _injector.SelectedSourceIndex = 0;
    }

    private void ResetTarget()
    {
        _currentMembers = null;
        _injector.SelectedTargetIndex = 0;
    }

    #endregion


    #region Utils

    private float GetHeight(Rect position, int id)
    {
        _elementCount++;
        return position.y + MARGIN + (EditorGUIUtility.singleLineHeight + PADDING) * id;
    }

    #endregion

    
    #region Private Fields

    private Injector _injector;
    private string[] _currentPopupOptions;
    private MemberInfo[] _currentMembers;
    private int _elementCount;
    private bool _isTypeFree;
    private Type _sourceType;
    private UnityEngine.Object _targetObject;

    private const BindingFlags BINDINGFLAGS = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance;
    private const MemberTypes MEMBERTYPES = MemberTypes.Field | MemberTypes.Property;
    private const int PADDING = 2;
    private const int MARGIN = 12;
         
    #endregion
}