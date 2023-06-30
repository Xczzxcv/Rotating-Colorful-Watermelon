using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

internal class ColorChangingObject : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer modelRenderer;
    [SerializeField]
    private int mainModelMaterialIndex;
    [SerializeField]
    private ParticleSystem particles;
    [field: SerializeField]
    public Collider ModelCollider { get; private set; }

    [Header("Color random settings")]
    [SerializeField, Range(0, 1)]
    private float hueMin;
    [SerializeField, Range(0, 1)]
    private float hueMax;

    [SerializeField, Range(0, 1)]
    private float saturationMin;
    [SerializeField, Range(0, 1)]
    private float saturationMax;

    [SerializeField, Range(0, 1)]
    private float valueMin;
    [SerializeField, Range(0, 1)]
    private float valueMax;

    private readonly List<Material> _materials = new();

    private void OnValidate()
    {
        if (!modelRenderer)
        {
            return;
        }

        var materialsCount = modelRenderer.sharedMaterials.Length;
        Debug.Assert(0 <= mainModelMaterialIndex && mainModelMaterialIndex <= materialsCount,
            $"{gameObject}'s '{nameof(mainModelMaterialIndex)}' set to {mainModelMaterialIndex} " +
            $"but model have only {materialsCount} materials");
    }

    public void ChangeColors()
    {
        modelRenderer.GetMaterials(_materials);
        foreach (var material in _materials)
        {
            var newColor = Random.ColorHSV(
                hueMin, hueMax,
                saturationMin, saturationMax,
                valueMin, valueMax
            );
            material.color = newColor;
        }

        var mainColor = mainModelMaterialIndex != -1
            ? _materials[mainModelMaterialIndex].color
            : Color.white;
        var mainModule = particles.main;
        mainModule.startColor = new ParticleSystem.MinMaxGradient(mainColor);
        particles.Play();
    }
}