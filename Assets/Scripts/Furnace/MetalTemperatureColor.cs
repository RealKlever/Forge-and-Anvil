using UnityEngine;
using System;
using System.Reflection;

[RequireComponent(typeof(Renderer))]
public class MetalTemperatureColor : MonoBehaviour
{
    [Header("Temperature Mapping")]
    [Tooltip("Temperature considered fully cold for color mapping.")]
    public float coldTemperature = 20f;

    [Tooltip("Temperature considered fully hot for color mapping.")]
    public float hotTemperature = 800f;

    [Tooltip("If true, the furnace can overwrite hotTemperature when the metal enters.")]
    public bool autoSetHotFromFurnace = true;

    [Header("Color Settings")]
    [Tooltip("Cold iron-like color.")]
    public Color coldColor = new Color(0.35f, 0.35f, 0.37f, 1f);

    [Tooltip("Heated metal color (orange-tinted but still gray/muted).")]
    public Color hotColor = new Color(0.75f, 0.38f, 0.18f, 1f);

    [Header("Emission (Optional)")]
    [Tooltip("If true, also adjusts emission color for a subtle glow.")]
    public bool affectEmission = true;

    [Tooltip("Multiplier for emission brightness.")]
    public float emissionIntensity = 1.5f;

    private Renderer _renderer;
    private MaterialPropertyBlock _mpb;

    private int _baseColorId;
    private int _colorId;
    private int _emissionColorId;

    // We reference Metal if it exists in the project (it does, since FurnaceHeat uses it).
    private Component _metalComponent;
    private FieldInfo _temperatureField;
    private PropertyInfo _temperatureProperty;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _mpb = new MaterialPropertyBlock();

        _baseColorId = Shader.PropertyToID("_BaseColor");     // URP/Lit
        _colorId = Shader.PropertyToID("_Color");             // Standard
        _emissionColorId = Shader.PropertyToID("_EmissionColor");

        _metalComponent = GetComponent<Metal>();
        CacheTemperatureAccessors();

        if (affectEmission && _renderer != null && _renderer.material != null)
        {
            var mat = _renderer.material; // instance for this renderer
            if (mat.HasProperty(_emissionColorId))
            {
                mat.EnableKeyword("_EMISSION");
            }
        }

        ValidateTemps();
        UpdateVisualFromTemperature(ReadTemperatureSafe());
    }

    private void OnValidate()
    {
        ValidateTemps();
        if (emissionIntensity < 0f) emissionIntensity = 0f;
    }

    private void ValidateTemps()
    {
        if (hotTemperature <= coldTemperature)
            hotTemperature = coldTemperature + 0.01f;
    }

    private void CacheTemperatureAccessors()
    {
        if (_metalComponent == null) return;

        Type t = _metalComponent.GetType();
        BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

        // Try common naming patterns
        _temperatureProperty =
            t.GetProperty("temperature", flags) ??
            t.GetProperty("Temperature", flags);

        _temperatureField =
            t.GetField("temperature", flags) ??
            t.GetField("Temperature", flags);
    }

    private float ReadTemperatureSafe()
    {
        if (_metalComponent == null)
            return coldTemperature;

        try
        {
            if (_temperatureProperty != null)
            {
                object value = _temperatureProperty.GetValue(_metalComponent);
                if (value != null) return Convert.ToSingle(value);
            }

            if (_temperatureField != null)
            {
                object value = _temperatureField.GetValue(_metalComponent);
                if (value != null) return Convert.ToSingle(value);
            }
        }
        catch
        {
            // If anything goes wrong, fall back to cold.
        }

        return coldTemperature;
    }

    private void Update()
    {
        float temp = ReadTemperatureSafe();
        UpdateVisualFromTemperature(temp);
    }

    private void UpdateVisualFromTemperature(float temperature)
    {
        float heat01 = Mathf.InverseLerp(coldTemperature, hotTemperature, temperature);
        Color current = Color.Lerp(coldColor, hotColor, heat01);

        _renderer.GetPropertyBlock(_mpb);

        // Support both common shader property names.
        _mpb.SetColor(_baseColorId, current);
        _mpb.SetColor(_colorId, current);

        if (affectEmission)
        {
            Color emission = current * emissionIntensity * heat01;
            _mpb.SetColor(_emissionColorId, emission);
        }

        _renderer.SetPropertyBlock(_mpb);
    }

    // Called by FurnaceHeat when entering.
    public void NotifyEnteredFurnace(FurnaceHeat furnace)
    {
        if (!autoSetHotFromFurnace || furnace == null) return;

        // Use furnace temperature as the top of the visual range.
        hotTemperature = Mathf.Max(coldTemperature + 0.01f, furnace.temperature);
    }

    // Called by FurnaceHeat when exiting (optional hook).
    public void NotifyExitedFurnace(FurnaceHeat furnace)
    {
        // You can add cooling/behavior here later if you want.
        // For now we do nothing so the color continues to reflect the metal's temperature value.
    }
}
