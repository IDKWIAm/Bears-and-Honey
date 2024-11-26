using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Light2D))]
public class WorldLight : MonoBehaviour
{
    private Light _light;
    [SerializeField] private WorldTime _worldTime;
    [SerializeField] private Gradient _gradient;

    private void Awake()
    {
        _light = GetComponent<Light>();
        _worldTime.WorldTimeChanged += OnWorldTimeChanged;
    }

    private void OnDestroy()
    {
        _worldTime.WorldTimeChanged -= OnWorldTimeChanged;
    }

    private void OnWorldTimeChanged(object sender, TimeSpan newTime)
    {
        _light.color = _gradient.Evaluate(PrecentOfDay(newTime));
    }

    private float PrecentOfDay(TimeSpan timeSpan)
    {
        return (float) timeSpan.TotalMinutes % WorldTimeConstants.MinutesInDay / WorldTimeConstants.MinutesInDay;
    }
}