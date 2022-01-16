using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Injector))]
public class InjectorDrawer: PropertyDrawer 
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var targetObject = property.serializedObject.targetObject;
        _injector = (Injector)fieldInfo.GetValue(targetObject);
        _isTypeFree = _injector.SourceType is null;
        _elementCount = 0;
        EditorGUI.BeginProperty(position, label, property);

        DrawSamplingSource(position, property);
        DrawInjectionTarget(position, property);
        DrawInjectionButton(position);

        EditorGUI.EndProperty();
    }

    private void DrawInjectionButton(Rect position)
    {
        var injectButtonRect = new Rect(position.x, GetHeight(position, _elementCount), position.width, EditorGUIUtility.singleLineHeight);
        GUI.enabled = _injector.CanInject;
        if (GUI.Button(injectButtonRect, "Inject"))
        {
            _injector.Inject();
        }
    }

    private void DrawSamplingSource(Rect position, SerializedProperty property)
    {
        if(!_isTypeFree) return;
        
        var lineHeight = EditorGUIUtility.singleLineHeight;
        var samplingSource = property.FindPropertyRelative("_sourceObject");
        var sampledMember = property.FindPropertyRelative("_sourceMemberName");

        FindMembers(samplingSource, ref _sourceMembers, ResetSource);
        GeneratePopupOptions(_sourceMembers);

        var samplingSourceRect = new Rect(position.x, GetHeight(position, _elementCount), position.width, lineHeight);
        var SampledMemberRect = new Rect(position.x, GetHeight(position, _elementCount), position.width, lineHeight);

        EditorGUI.ObjectField(samplingSourceRect, samplingSource);
        _injector.SelectedSourceIndex = EditorGUI.Popup(SampledMemberRect, "Source Members", _injector.SelectedSourceIndex, _targetPopupOptions);
        GUI.enabled = _injector.SelectedSourceIndex > 0;

        if(_sourceMembers is null) return;

        var memberIndex = _injector.SelectedSourceIndex - 1;
        if(memberIndex < 0) return;

        var selectedSourceMember = _sourceMembers[memberIndex];
        _sourceType = GetMemberType(selectedSourceMember);
    }

    private void DrawInjectionTarget(Rect position, SerializedProperty property)
    {
        var lineHeight = EditorGUIUtility.singleLineHeight;
        var injectionTarget = property.FindPropertyRelative("_targetObject");
        var injectedMember = property.FindPropertyRelative("_targetMemberName");

        FindMembers(injectionTarget, ref _targetMembers, ResetTarget);
        GeneratePopupOptions(_targetMembers);

        var injectionTargetRect = new Rect(position.x, GetHeight(position, _elementCount), position.width, lineHeight);
        var injectedMemberRect = new Rect(position.x, GetHeight(position, _elementCount), position.width, lineHeight);

        EditorGUI.ObjectField(injectionTargetRect, injectionTarget);
        _injector.SelectedTargetIndex = EditorGUI.Popup(injectedMemberRect, "Target Members", _injector.SelectedTargetIndex, _targetPopupOptions);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return (EditorGUIUtility.singleLineHeight + PADDING) * _elementCount;
    }

    #region Plumbery
     
    private void GeneratePopupOptions(MemberInfo[] members)
    {
        var optionCount = members?.Length ?? 0;
        optionCount += 1;
        _targetPopupOptions = new string[optionCount];
        _targetPopupOptions[0] = "None";
        for (int i = 0; i < optionCount - 1; i++)
        {
            var option = members[i].Name;
            _targetPopupOptions[i + 1] = option;
        }
    }
    
    private void FindMembers(SerializedProperty source, ref MemberInfo[] members, Action resetCallback)
    {
        var sourceObject = source.objectReferenceValue;
        if(sourceObject is null)
        {
            ResetSource();
            return;
        }

        var sourceType = sourceObject.GetType();
        var filter = new MemberFilter(SearchMember);
        members = sourceType.FindMembers(MEMBERTYPES, BINDINGFLAGS, filter, null);
    }

    private bool SearchMember(MemberInfo member, System.Object objSearch)
    {
        if(_isTypeFree && _elementCount < 3) return true;

        Type memberType = GetMemberType(member);

        if (_isTypeFree && memberType == _sourceType) return true;
        else if (memberType == _injector.SourceType) return true;

        return false;
    }

    private void ResetTarget()
    {
        _targetMembers = null;
        _injector.SelectedTargetIndex = 0;
    }

    private void ResetSource()
    {
        _sourceType = null;
        _sourceMembers = null;
        _injector.SelectedSourceIndex = 0;
    }

    #endregion


    #region Utils

    private Type GetMemberType(MemberInfo member)
    {
        Type memberType = null;
        switch (member.MemberType)
        {
            case MemberTypes.Field:
                memberType = ((FieldInfo)member).FieldType;
                break;

            case MemberTypes.Property:
                memberType = ((PropertyInfo)member).PropertyType;
                break;
        }

        return memberType;
    }

    private float GetHeight(Rect position, int id)
    {
        _elementCount++;
        return position.y + (EditorGUIUtility.singleLineHeight + PADDING) * id;
    }

    #endregion

    private Injector _injector;
    private string[] _targetPopupOptions;
    private string[] _sourcePopupOptions;
    private MemberInfo[] _targetMembers;
    private MemberInfo[] _sourceMembers;
    private int _elementCount;
    private bool _isTypeFree;
    private Type _sourceType;

    private const BindingFlags BINDINGFLAGS = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance;
    private const MemberTypes MEMBERTYPES = MemberTypes.Field | MemberTypes.Property;
    private const int PADDING = 2;
}