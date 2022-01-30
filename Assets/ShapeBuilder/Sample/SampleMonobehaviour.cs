using System.Collections.Generic;
using UnityEngine;

public class SampleMonobehaviour : MonoBehaviour 
{
    [SerializeField] private Injector _injector = new Injector();
    [SerializeField] private float _float1;
    [SerializeField] private float _float2;
    [SerializeField] private Shape[] _shapes;
}