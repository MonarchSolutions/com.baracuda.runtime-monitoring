using System;
using Baracuda.Monitoring.Management;
using UnityEditor;
using UnityEngine;

namespace Baracuda.Monitoring.Editor
{
    public class MonitoringSettingsWindow : EditorWindow
    {
        private MonitoringSettings _settings;
        private MonitoringSettingsInspector _inspector;
        private Vector2 _scrollPosition;
        
        public static void Open()
        {
            GetWindow<MonitoringSettingsWindow>("Monitoring").Show();
        }

        private void OnEnable()
        {
            _settings = MonitoringSettings.Instance();
            _inspector = UnityEditor.Editor.CreateEditor(_settings) as MonitoringSettingsInspector;
        }

        private void OnDisable()
        {
            _inspector.SaveState();
        }

        private void OnGUI()
        {
            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);
            EditorGUI.indentLevel = 1;
            _inspector.OnInspectorGUI();
            EditorGUILayout.EndScrollView();
        }
    }
}
